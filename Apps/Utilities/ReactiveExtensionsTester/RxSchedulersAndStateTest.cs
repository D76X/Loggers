
using System;
using System.Reactive.Concurrency;

namespace ReactiveExtensionsTester {

    /// <summary>
    /// Illustrates passing state with scheduler in RX.
    /// 
    /// Refs
    /// http://www.introtorx.com/content/v1.0.10621.0/15_SchedulingAndThreading.html
    /// </summary>
    public class RxSchedulersAndStateTest : IRxExampleBaseTest {

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

        public string Description => @"Illustrates passing state with scheduler in RX.";

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
            // some state to the scheduler.
            scheduler.Schedule(state, (s, _) => {

                Console.WriteLine($"The passed state value = {s}");
            });
        }
    }
}
