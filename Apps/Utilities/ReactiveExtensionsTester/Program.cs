using System;

namespace ReactiveExtensionsTester {

    class Program {

        static void Main(string[] args) {

            var program = new Program();
            program.Run();

            Console.ReadKey();
        }

        private void Run() {

            var rxThreadsTester = new RxThreadsTester();
            rxThreadsTester.Run();
        } 
    }
}
