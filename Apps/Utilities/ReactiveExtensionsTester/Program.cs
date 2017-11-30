using System;
using System.Threading;

namespace ReactiveExtensionsTester {

    class Program {

        static void Main(string[] args) {

            Thread.CurrentThread.Name = "Main";

            var program = new Program();
            program.Run();

            Console.ReadKey();
        }

        private void Run() {

            var rxSchedulersAndThreadsTester = new RxSchedulersAndThreadsTester();
            rxSchedulersAndThreadsTester.Run();

            var rxSchedulersAndStateTest = new RxSchedulersAndStateTest();
            rxSchedulersAndStateTest.Run();
        } 
    }
}
