using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Demo._00._01.Data.Conversions {

    class Program {

        static void Main(string[] args) {

            // strings as UTF8 
            Console.WriteLine("UTF8");
            string testUtf8 = @"Test";
            byte[] testUtf8Bytes = Encoding.UTF8.GetBytes(testUtf8);
            string testUtf8String = Encoding.UTF8.GetString(testUtf8Bytes);
            string testUtf8BytesString = BitConverter.ToString(testUtf8Bytes);
            Console.WriteLine($"{testUtf8} as bytes = {testUtf8BytesString}");
            Console.WriteLine($"{testUtf8BytesString} as string = {testUtf8String}");
            Console.WriteLine();

            // unicode strings...
            Console.WriteLine("Unicode");
            string testUnicode = @"Test⅑←";
            byte[] testUnicodeBytes = Encoding.Unicode.GetBytes(testUnicode);
            string testUnicodeString = Encoding.Unicode.GetString(testUnicodeBytes);
            string testUnicodeBytesString = BitConverter.ToString(testUnicodeBytes);
            Console.WriteLine($"{testUnicode} as bytes = {testUnicodeBytesString}");
            Console.WriteLine($"{testUnicodeBytesString} as string = {testUnicodeString}");
            Console.WriteLine();

            // DateTime
            Console.WriteLine("DateTime");
            DateTime testDateTime = DateTime.Now;
            byte[] testDateTimeTicksBytes = BitConverter.GetBytes(testDateTime.Ticks);
            string testDateTimeTicksBytesString = BitConverter.ToString(testDateTimeTicksBytes);
            DateTime testDateTimeBack = DateTime.FromBinary(BitConverter.ToInt64(testDateTimeTicksBytes,0));
            Console.WriteLine($"{testDateTime} ticks as bytes  = {testDateTimeTicksBytesString}");
            Console.WriteLine($"{testDateTimeTicksBytesString} as string = {testDateTimeBack}");

            Console.ReadKey();
        }
    }
}
