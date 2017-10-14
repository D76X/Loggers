using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace ReactiveExtensionsTester {

    /// <summary>
    /// 
    /// Refs 
    /// .NET Reactive Extensions Fundamentals 1.0 by Dan Sullivan 
    /// https://app.pluralsight.com/player?course=reactive-extensions&author=dan-sullivan&name=concurrency-reactive-extensions&clip=1&mode=live
    /// 
    /// Introduction to RX book by Lee Campbell - Concurrency
    /// http://www.introtorx.com/content/v1.0.10621.0/15_SchedulingAndThreading.html
    /// </summary>
    public class RxThreadsTester {


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
        /// observable sequence. 
        /// 
        /// --------------------------
        /// 2-Observation delegates
        /// --------------------------
        /// 
        /// Observation delegates are the delagates OnNext, OnError, OnComplete.
        /// It is good practice to have subscription delegates to return quickly.
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
            
            //this.ObservableOnSinlgeThread();
            //Thread.Sleep(1000);
            //Console.WriteLine("***");

            //this.ObservableOnOtherThread();
            //Thread.Sleep(1000);
            //Console.WriteLine("***");

            this.ObserveOnSubscribeOnDifferentThreads();
            //Thread.Sleep(1000);
            //Console.WriteLine("***");
        }

        /// <summary>
        /// I am not sure I understand the results from this test!
        /// I would expect the thread of the ProcessNumber to be different from the 
        /// thread that runs the method and the thread of teh observation instead it
        /// turns out to be the same.
        /// Why?
        /// </summary>
        private void ObserveOnSubscribeOnDifferentThreads() {

            Console.WriteLine($"started {nameof(ObserveOnSubscribeOnDifferentThreads)} on thread\t{Thread.CurrentThread.ManagedThreadId}");

            var numbers = from n in new int[] { 1, 2, 3 } select this.ProcessNumber(n);

            var source = numbers.ToObservable(ImmediateScheduler.Instance);

            // the subscription delegate si force to run on a new foreground thread
            // this is the thread where the events from the observable are generated
            // that is where the ProcessNumber runs
            source.SubscribeOn(ImmediateScheduler.Instance);
            source.Subscribe(
                n => { Console.WriteLine($"OnNext {n} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                e => { Console.WriteLine($"OnError {e} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                () => { Console.WriteLine($"OnCompleted on thread\t{Thread.CurrentThread.ManagedThreadId}"); });

            // the observation delegates will be run on a new dedicated foreground thread
            // this is the thread where the code in OnNext, OnError, OnComplete runs.
            source.ObserveOn(ImmediateScheduler.Instance);           

            Console.WriteLine($"finished {nameof(ObserveOnSubscribeOnDifferentThreads)} on thread\t{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
        }

        /// <summary>
        /// 1- The thread on which the observable is created 
        /// is different from the thread that calls .ToBservable
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
            var source = numbers.ToObservable(NewThreadScheduler.Default);

            //---------------------------------------------------------------------------
            // Other available schedulers options
            //var source = numbers.ToObservable(ThreadPoolScheduler.Instance);
            //var source = numbers.ToObservable(ImmediateScheduler.Instance);
            //var source = numbers.ToObservable(CurrentThreadScheduler.Instance);
            //---------------------------------------------------------------------------

            source.Subscribe(
                n => { Console.WriteLine($"OnNext {n} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                e => { Console.WriteLine($"OnError {e} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                () => { Console.WriteLine($"OnCompleted on thread\t{Thread.CurrentThread.ManagedThreadId}"); });

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
            var source = numbers.ToObservable();

            source.Subscribe(
                n => { Console.WriteLine($"OnNext {n} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                e => { Console.WriteLine($"OnError {e} on thread\t{Thread.CurrentThread.ManagedThreadId}"); },
                () => { Console.WriteLine($"OnCompleted on thread\t{Thread.CurrentThread.ManagedThreadId}"); });

            Console.WriteLine($"finished {nameof(ObservableOnSinlgeThread)} on thread\t{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
        }

        private int ProcessNumber(int number) {

            Console.WriteLine($"{nameof(ProcessNumber)} with {number} on thread\t{Thread.CurrentThread.ManagedThreadId}");       
            return number;
        }
    }
}
