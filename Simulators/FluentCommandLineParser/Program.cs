using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineFluent
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<Arguments>();
            parser
                .Setup(a => a.Address)
                .As('a', "address")
                .WithDescription("The URL to connect to the service.")
        .Required();

            parser
                .Setup(a => a.Username)
                .As('u', "username")
                .WithDescription("The username to use for authentication.");

            parser
                .Setup(a => a.Password)
                .As('p', "password")
                .WithDescription("The password to use for authentication.");

            var parsedArgs = parser.Parse(args);

            Console.WriteLine("You entered  {0}  ", parser.Object.Password);
            ////Console.WriteLine("You entered  {0}  ",  parser.Object.Username);
            //Console.WriteLine("You entered  {0}  ", parser.Object.Address);
        }

        private class Arguments
        {
            public string Address { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}