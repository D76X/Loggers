using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fclp;

namespace CommandLineFluent
{
    class Program
    {
        private class Arguments
        {
            public string Address { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
        static void Main(string[] args)
        {
            //var parser = new FluentCommandLineParser ();

            //parser.Setup<string>('a', "address")
            //      .Required()
            //      .Callback(text => Console.WriteLine(text));

            //parser.Setup<string>('u', "username")
            //     .Required()
            //     .Callback(text => Console.WriteLine(text));

            //parser.Setup<string>('p', "password")
            //     .Required()
            //     .Callback(text => Console.WriteLine(text));

            //parser.Parse(args);


            //GENERIC FLUENT CLP
            var parser2 = new FluentCommandLineParser<Arguments>();
            parser2
                .Setup(a => a.Address)
                .As('a', "address")
                .WithDescription("The URL to connect to the service.")
                .Required();            

            parser2
                .Setup(a => a.Username)
                .As('u', "username")
                .WithDescription("The username to use for authentication.");

            parser2
                .Setup(a => a.Password)
                .As('p', "password")
                .WithDescription("The password to use for authentication.")
                .Callback(text => Console.WriteLine(text));

              parser2.Parse(args);

             


        }
    }
}
