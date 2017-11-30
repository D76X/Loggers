
using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace ReactiveExtensionsTester {

    /// <summary>
    /// Illustrates passing state with scheduler in RX.
    /// 
    /// Refs
    /// http://www.introtorx.com/content/v1.0.10621.0/15_SchedulingAndThreading.html
    /// </summary>
    public class RxSchedulersAndStateTest {

        private class State<T> {

            private readonly T value; 

            public State(T value) {

                this.value = value;
            }

            public T Value => this.value;

            public override string ToString() {
                return this.value.ToString();
            }
        }

        public void Run() {

            Console.WriteLine($"Start {nameof(RxSchedulersAndStateTest.Run)}");

            this.PassStateToScheduler();

            Console.WriteLine($"End {nameof(RxSchedulersAndStateTest.Run)}");
        }

        private void PassStateToScheduler() {

            var state = new State<string>(@"value");

            // this time the action is not specified when the scheduler is instantiated.
            IScheduler scheduler = new NewThreadScheduler();
                        
            // IScheduler.Schedule has several overloads and some are designed to pass
            // some state to the scheduler. In this example Schedule<State<string>>
            // takes two arguments.
            // 1- An argument of type State<string> that is the initial state.
            // 2- An action argument Action<State<string>,Action<State<string>>
            //    this action takes a state and another action with the same state
            //    this is the RX mechanism for recursion but in this example this aspect
            //    is ignored - the _ is used to highlight that this argument of the 
            //     overload is not used.
            scheduler.Schedule(
                state, 
                (s, _) => {
                    Console.WriteLine($"The passed state value = {s}");                    
            });

            //// the state is changed
            //state = new State<string>(@"new value"); 

            //// This is another override of IScheduler.Schedule that returns an IDisposable
            //scheduler.Schedule(
            //    state,
            //    (s,_) => {                                      
            //        Console.WriteLine($"The passed state value = {s}");
            //        return Disposable.Empty;
            //    });
        }
    }
}
