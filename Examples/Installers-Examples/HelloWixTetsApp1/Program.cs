using HelloWixTestLib1;
using System;

namespace HelloWixTetsApp1 {
    class Program {
        static void Main(string[] args) {
            var program = new Program();
            program.Run();
            Console.WriteLine(nameof(HelloWixTetsApp1)+" completed");
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }

        private void Run() {

            var greeater1 = new HelloWixClass1();
            var greeater2 = new HelloWixClass1("Have a good day!");
            Console.WriteLine(greeater1.Message);
            Console.WriteLine(greeater2.Message);
        }
    }
}
