using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Demo._07.Sink.With.KillSignal {

    internal class JobProgress {

        readonly string Origin;
        readonly string JobId;
        readonly int TotalWokload;
        readonly int BatchSize;

        private int progress;
        private int updates;
        private Stopwatch stopwatch = new Stopwatch();

        internal JobProgress(
            string endPoint,
            string jobId,
            int totalWorkload,
            int batchSize) {

            this.Origin = endPoint;
            this.JobId = jobId;
            this.TotalWokload = totalWorkload;
            this.BatchSize = batchSize;
        }

        internal void Update(int workDone) {
            Thread.Sleep(workDone);
            progress += workDone;
            updates += 1;
        }

        internal bool Completed => progress >= TotalWokload;
        internal int Progress => this.progress;
        internal int Updates => this.updates;

        public override string ToString() {
            return $"jobId = {JobId}, progress = {Progress}/{TotalWokload}, updates = {Updates}/{BatchSize}, completed={Completed}";
        }
    }

    internal class Jobs {

        Dictionary<string, JobProgress> jobs = new Dictionary<string, JobProgress>();

        internal JobProgress Update(string updateMessage) {

            // expectred message format
            // $"{endPoint} {jobName} {totalWorkLoad} {batchSize} {workLoad}"

            var split = updateMessage.Split();

            string endPoint = split[0];
            string jobId = split[1];
            int totalWorkload = int.Parse(split[2]);
            int batchSize = int.Parse(split[3]);
            int progressDone = int.Parse(split[4]);

            JobProgress job;

            if (!jobs.ContainsKey(jobId)) {

                jobs.Add(jobId, new JobProgress(
                    endPoint,
                    jobId,
                    totalWorkload,
                    batchSize));
            }

            job = jobs[jobId];
            job.Update(progressDone);

            if (job.Completed) {
                jobs.Remove(jobId);
            }

            return job;
        }

        public bool IsAllDone {
            get {
                return true;
            }
        }
    }

    class Program {

        // The sink node.
        // It binds or connect to an upstream endpoint.
        // It PULLs work-done messages from the upstream endpoint. 
        // It compiles stats on the work done based on the collected messages.
        // It sends a kill message to any of the workers that were allocated to its pipeline.

        const string defaultUpstreamEndPoint = "tcp://localhost:5580";
        const string defaultKillSignalEndPoint = "tcp://localhost:5600";
        const string jobCompletedMarker = ">>>";

        static void Main(string[] args) {

            string upstreamEndPoint = defaultUpstreamEndPoint;
            bool bindPullSocket = true;

            string killSignalEndPoint = defaultKillSignalEndPoint;
            bool bindKillSignalPublisher = true;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5680           
            if (args.Length > 0) {

                upstreamEndPoint = args[0];
                bindPullSocket = upstreamEndPoint.Contains("*");
            }

            // tcp://localhost:5680 tcp://*:5600
            if (args.Length > 1) {

                killSignalEndPoint = args[1];
                bindKillSignalPublisher = killSignalEndPoint.Contains("*");
            }

            using (PullSocket pullSocket = new PullSocket())
            using (PublisherSocket publisherSocket = new PublisherSocket()) {

                publisherSocket.Options.SendHighWatermark = 1;

                if (bindKillSignalPublisher) {
                    publisherSocket.Bind(killSignalEndPoint);
                    Console.WriteLine($"sink binds kill signal publisher to {killSignalEndPoint}");
                }
                else {
                    publisherSocket.Connect(killSignalEndPoint);
                    Console.WriteLine($"sink connects kill signal publisher to {killSignalEndPoint}");
                }

                if (bindPullSocket) {
                    pullSocket.Bind(upstreamEndPoint);
                    Console.WriteLine($"sink binds upstream with pull socket to {upstreamEndPoint}");
                }
                else {
                    pullSocket.Connect(upstreamEndPoint);
                    Console.WriteLine($"sink connects upstream with pull socket to {upstreamEndPoint}");
                }

                //Console.WriteLine("continue...");
                //Console.ReadKey();

                var jobs = new Jobs();
                int completedAllJobsCount = 0;

                while (true) {

                    // this is blocking
                    // read the work done by any worker that is using this endpoint as its sink.
                    string frame = pullSocket.ReceiveFrameString();
                    //Console.WriteLine(frame);

                    // Add StopWatch...

                    JobProgress job = jobs.Update(frame);
                    string status = job.ToString();
                    string msg = job.Completed ? $"\n\r{status}\n\r" : status;
                    Console.WriteLine(msg);

                    // this is simplistic but for this example it will do.

                    completedAllJobsCount = jobs.IsAllDone ?
                        completedAllJobsCount + 1 :
                        0;

                    if (completedAllJobsCount > 10) {

                        completedAllJobsCount = 0;

                        // kill the connected workers so that they can 
                        // go on with other work if it is the case...
                        publisherSocket.SignalOK();
                        Console.WriteLine("Sent kill signal!");
                    }
                }

                //// the sink must wait for a start signal before doing anything else.
                //// the start signal comes from the ventilator once it has handed out 
                //// the work to the worker nodes.
                //// the call to receive frame blocks.
                //Frame frame = sink.ReceiveFrame();
                //Console.WriteLine($"received frame {BitConverter.ToString(frame.Buffer)}");

                //// the sink receives the expected workload for this batch
                //frame = sink.ReceiveFrame();
                //Console.WriteLine($"received frame {BitConverter.ToString(frame.Buffer)} = {BitConverter.ToInt64(frame.Buffer, 0)}");

                //// the sink receives the expected batch size for this batch
                //frame = sink.ReceiveFrame();
                //int batchSize = BitConverter.ToInt32(frame.Buffer, 0);
                //Console.WriteLine($"received frame {BitConverter.ToString(frame.Buffer)} = {batchSize}");

                //var stopwatch = new Stopwatch();
                //stopwatch.Start();
                //int totalWorkDone = 0;
                //for (int i = 0; i < batchSize; i++) {

                //    frame = sink.ReceiveFrame();
                //    var workLoad = BitConverter.ToInt32(frame.Buffer, 0);
                //    totalWorkDone += workLoad;
                //    Console.WriteLine($"worker completed workload {BitConverter.ToString(frame.Buffer)} = {workLoad}");
                //}

                //stopwatch.Stop();
                //Console.WriteLine($"All work {totalWorkDone} done in {stopwatch.ElapsedMilliseconds}");
                //Console.ReadKey();de
            }
        }
    }
}
