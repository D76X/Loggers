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
            var parser = new FluentCommandLineParser ();

            parser.Setup<string>('a', "address")
                  .Required();

            parser.Setup<string>('u', "username")
                 .Required();

            parser.Setup<string>('p', "password")
                 .Required();

            var parsedValues = parser.Parse(args);

           
            //    parser
            //        .Setup(a => a.Address)
            //        .As('a', "address")
            //        .WithDescription("The URL to connect to the service.")
            //.Required();

            //    parser
            //        .Setup(a => a.Username)
            //        .As('u', "username")
            //        .WithDescription("The username to use for authentication.");

            //    parser
            //        .Setup(a => a.Password)
            //        .As('p', "password")
            //        .WithDescription("The password to use for authentication.");

          
        }
    }
}
