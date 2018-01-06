using System;
using System.Threading;

namespace ReactiveExtensionsTester {

    class Program {

        static void Main(string[] args) {

            Thread.CurrentThread.Name = "Main";

            var program = new Program();

            program.Tests = new IRxExampleBaseTest[] {
                new RxSchedulersAndThreadsTest(),
                new RxSchedulersAndStateTest(),
                new RxHotAndColdObservablesTest(),
            };

            Console.WriteLine("Menu \r\n");

            for (int i = 0; i < program.Tests.Length; i++) {
                Console.WriteLine($"Test {i + 1} \r\n");
                Console.WriteLine(program.Tests[i].Description);
                Console.WriteLine();
            }

            Console.WriteLine("Enter          :  run next");
            Console.WriteLine("S              :  skip next");
            Console.WriteLine("N              :  run only test N");
            Console.WriteLine("A              :  run all");            
            Console.WriteLine("Any other key  :  exit");

            program.Run();
            Console.ReadKey();
        }

        private IRxExampleBaseTest[] Tests;
        
        private const string keySkipNext = @"s";
        private const string keyRunAll = @"a";

        private void Run() {

            int length = this.Tests.Length;
            
            while (Console.ReadKey().Key == ConsoleKey.Enter) {

            }

            foreach (var test in this.Tests) {

                //test.Run();
            }

            //var rxSchedulersAndThreadsTester = new RxSchedulersAndThreadsTest();
            //rxSchedulersAndThreadsTester.Run();

            //var rxSchedulersAndStateTest = new RxSchedulersAndStateTest();
            //rxSchedulersAndStateTest.Run();
        } 
    }
}
