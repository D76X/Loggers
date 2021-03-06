﻿using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace ReactiveExtensionsTester {

    /// <summary>
    /// Illustrates the usage and properties of of schedulers with RX.
    ///  
    /// Refs 
    /// .NET Reactive Extensions Fundamentals 1.0 by Dan Sullivan 
    /// https://app.pluralsight.com/player?course=reactive-extensions&author=dan-sullivan&name=concurrency-reactive-extensions&clip=1&mode=live
    /// 
    /// Introduction to RX book by Lee Campbell - Concurrency
    /// http://www.introtorx.com/content/v1.0.10621.0/15_SchedulingAndThreading.html
    /// </summary>
    public class RxSchedulersAndThreadsTest : IRxExampleBaseTest {

        public string Description => @"Illustrates the usage and properties of of schedulers with RX.";

        /// <summary>
        /// RX distinguishes between two kinds of delegates.
        /// RX creates these delegates for you.
        /// 
        /// 1-Subscription delegates 
        /// 2-Observation delegates
        /// 
        /// --------------------------
        /// 1-Subcription delegate
        /// --------------------------
        /// 
        /// A subscription delegate is the delegate that gets the next value from an
        /// observable sequence. It is good practice to have subscription delegates to 
        /// return quickly.
        /// 
        /// --------------------------
        /// 2-Observation delegates
        /// --------------------------
        /// 
        /// Observation delegates are the delegates OnNext, OnError, OnComplete.
        /// 
        /// If the values from the observable need processing that should happen
        /// in the subscription delegate. 
        /// 
        /// -----------------
        /// -3 ToObservable
        /// -----------------
        /// 
        /// ToObservable can be invoke with an IScheduler parameter to tell RX 
        /// which thread to use to run the observation delegates. If no scheduler
        /// is pecified then RX will use the current thread scheduler for the 
        /// observation delegates.
        /// 
        /// The scheduler specified to ToOservable is also used fro the subscription 
        /// delegate.
        /// 
        /// ------------------------------
        /// -4 SubscribeOn & ObserveOn
        /// ------------------------------
        /// 
        /// RX offers also an API that allows to specify the subscription and observation 
        /// schedulers indipendent of each another 
        /// 
        /// --------------------------------
        /// RX implementations of IScheduler  
        /// --------------------------------
        /// 
        /// ---------------------------
        /// 1-NewThreadScheduler 
        /// ---------------------------
        /// 
        /// schedules subscription or observation delegates on a Foreground thread.
        /// 
        /// ---------------------------
        /// 2-ThreadPoolScheduler 
        /// ---------------------------
        /// 
        /// schedules subscription or observation delegates on a Background thread.
        /// 
        /// The difference between Foregorund and Background threads is that a Foreground thread
        /// will prevent the invoking thread i.e. the application thread from terminating until 
        /// its work is done while a background thread does not.
        /// 
        /// ---------------------------
        /// 3-ImmediateScheduler   
        /// 4-CurrentThreadScheduler
        /// https://social.msdn.microsoft.com/Forums/en-US/7f75482f-eff2-4938-9491-47fe870989e8/currentthreadscheduler-vs-immediatescheduler?forum=rx
        /// ---------------------------
        /// 
        /// These two are very similar to each other as they both cause the subscription and 
        /// observation delegates to run on the same thread that creates the observable i.e.
        /// by means of the .ToObservable extension method or any other RX creation pattern.
        /// There are however implementation differences between these two scheduler that 
        /// might have considerable effects under certain circumstances...
        ///
        /// ------------------------------
        /// 5-DispatcherScheduler.Current
        /// ------------------------------
        ///  
        /// </summary>
        public void Run() {

            Console.WriteLine($"Start {nameof(RxSchedulersAndThreadsTest.Run)}");

            this.ObservableOnSinlgeThread();
            Thread.Sleep(1000);
            Console.WriteLine("***");

            this.ObservableOnOtherThread();
            Thread.Sleep(1000);
            Console.WriteLine("***");

            this.StackOverflowQuestion7579237Test();
            Thread.Sleep(1000);
            Console.WriteLine("***");

            this.ObserveOnSubscribeOnDifferentThreads();
            Thread.Sleep(1000);
            Console.WriteLine("***");

            Console.WriteLine($"End {nameof(RxSchedulersAndThreadsTest.Run)}");
        }

        private void StackOverflowQuestion7579237Test() {

            Console.WriteLine($"started {nameof(StackOverflowQuestion7579237Test)} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}");

            IScheduler thread1 = new NewThreadScheduler(x => new Thread(x) { Name = "Thread1" });
            IScheduler thread2 = new NewThreadScheduler(x => new Thread(x) { Name = "Thread2" });

            Observable.Create<int>(o =>
            {
                Console.WriteLine($"Subscribing on\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}");
                o.OnNext(1);
                return Disposable.Create(() => { });
            })
            .SubscribeOn(thread1)
            .ObserveOn(thread2)
            .Subscribe(x => Console.WriteLine($"Observing {x} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}"));

            Console.WriteLine($"finished {nameof(StackOverflowQuestion7579237Test)} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}");
        }

        /// <summary>
        /// I am not sure I understand the results from this test!
        /// I would expect the thread of the ProcessNumber to be different from the 
        /// thread that runs the method and the thread of teh observation instead it
        /// turns out to be the same.
        /// Why?
        /// </summary>
        private void ObserveOnSubscribeOnDifferentThreads() {

            Console.WriteLine($"started {nameof(ObserveOnSubscribeOnDifferentThreads)} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}");

            var numbers = from n in new int[] { 1, 2, 3 } select this.ProcessNumber(n);

            // the subscription delegate si force to run on a new foreground thread
            // this is the thread where the events from the observable are generated
            // that is where the ProcessNumber runs

            IScheduler subscriptionThread = new NewThreadScheduler(ts =>
                new Thread(ts) {
                    Name = $"{nameof(subscriptionThread)}",
                    IsBackground = true
                });

            IScheduler observationThread = new NewThreadScheduler(ts =>
                new Thread(ts) {
                    Name = $"{nameof(observationThread)}",
                    IsBackground = true
                });

            // ThreadPoolScheduler.Instance in .ToObservable creates the observable on a new background thread 
            // different from the application's main thread which is a foreground thread. The Subscription runs 
            // on dedicated background thread as some heavy processing of the events generated by the observable 
            // i.e. in ProcessNumber must be offloaded from the main thread. The observations run on a separate
            // thread different from the main thread and the subscription thread. In application with an UI the 
            // oservation thread shoud ne the main application thread that is the UI thread i.e. in a WPF app it
            // would be DispatcherScheduler.Current
            var source = numbers.ToObservable(ThreadPoolScheduler.Instance).
                SubscribeOn(subscriptionThread).
                ObserveOn(observationThread).
                Subscribe(
                n => { Console.WriteLine($"OnNext {n} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}"); },
                e => { Console.WriteLine($"OnError {e} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}"); },
                () => { Console.WriteLine($"OnCompleted on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}"); });

            Console.WriteLine($"finished {nameof(ObserveOnSubscribeOnDifferentThreads)} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}");
            Console.WriteLine();
        }

        /// <summary>
        /// 1- The thread on which the observable is created 
        /// is different from the thread that calls .ToObservable
        /// 2- The thread that calls .ToObservable is no longer
        /// blocked by the call to .Subcribe
        /// </summary>
        private void ObservableOnOtherThread() {

            Console.WriteLine($"started {nameof(ObservableOnOtherThread)} on thread\t{Thread.CurrentThread.ManagedThreadId}");

            var numbers = from n in new int[] { 1, 2, 3 } select this.ProcessNumber(n);

            // this time the observable is created on a separate thread withe respect to the thread
            // that makes the call to .ToObservable. The delegates of teh observable will be executed
            // on the new thread too but the .Subscribe will no longer block the the thread that has
            // made the call to .ToObservable.

            // NewThreadScheduler.Default schedules the subscription and observation delegates on a 
            // new Foregound Thread. The ThreadPoolScheduler can be used to do the same on a background
            // thread.
            var source = numbers.
                ToObservable(NewThreadScheduler.Default).
                Subscribe(
                n => { Console.WriteLine($"OnNext {n} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                e => { Console.WriteLine($"OnError {e} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                () => { Console.WriteLine($"OnCompleted on thread\t{Thread.CurrentThread.ManagedThreadId}"); });

            //---------------------------------------------------------------------------
            // Other available schedulers options
            //var source = numbers.ToObservable(ThreadPoolScheduler.Instance);
            //var source = numbers.ToObservable(ImmediateScheduler.Instance);
            //var source = numbers.ToObservable(CurrentThreadScheduler.Instance);
            //---------------------------------------------------------------------------

            Console.WriteLine($"finished {nameof(ObservableOnOtherThread)} on thread\t{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
        }

        /// <summary>
        /// 1- Everything on a single thread sequentially.
        /// 2- The thread of subscription and delegates is 
        /// the same thread that calles .ToObservable. 
        /// 3- The calls .Subscription blocks.
        /// </summary>
        private void ObservableOnSinlgeThread() {

            Console.WriteLine($"started {nameof(ObservableOnSinlgeThread)} on thread\t{Thread.CurrentThread.ManagedThreadId}");

            var numbers = from n in new int[] { 1, 2, 3 } select this.ProcessNumber(n);

            // this creates the observable on the same thread that invokes the call to ToObservable
            // any delegates will also run on the same thread one after the other sequencially in a
            // blocking fashion
            numbers.
            ToObservable().
            Subscribe(
            n => { Console.WriteLine($"OnNext {n} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
            e => { Console.WriteLine($"OnError {e} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
            () => { Console.WriteLine($"OnCompleted on thread\t{Thread.CurrentThread.ManagedThreadId}"); });

            Console.WriteLine($"finished {nameof(ObservableOnSinlgeThread)} on thread\t{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
        }

        private int ProcessNumber(int number) {

            Console.WriteLine($"{nameof(ProcessNumber)} with {number} on thread\t{Thread.CurrentThread.ManagedThreadId}\t{Thread.CurrentThread.Name}");       
            return number;
        }
    }
}
