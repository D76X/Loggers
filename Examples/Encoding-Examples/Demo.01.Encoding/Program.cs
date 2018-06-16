using System;
using System.Text;

namespace Demo._01.EncodingFoundamentals {

    /// <summary>
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-encoding
    /// https://msdn.microsoft.com/en-us/library/system.text.encoding(v=vs.110).aspx
    /// https://www.dotnetperls.com/ascii-table
    /// https://www.codeproject.com/Questions/286404/How-do-i-convert-from-ansi-to-unicode-in-Csharp
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/char
    /// https://stackoverflow.com/questions/5750203/how-to-write-unicode-characters-to-the-console
    /// </summary>
    class Program {

        static void Main(string[] args) {

            Encoding asciiEncoding = Encoding.ASCII;

            // the valid ASCII characters cover the first 128 bytes
            // from 0x00 to 0x7F

            Console.WriteLine("Byte representations of the ASCII set.");
            Console.WriteLine();

            byte asciiCharByte;
            byte[] asciiCharacterBytes = new byte[128];

            char asciiChar;
            char[] asciiCharacters = new char[128];

            // this is the default for UTF-16 Unicode encoding
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            //Console.OutputEncoding = System.Text.Encoding.BigEndianUnicode;

            //Console.InputEncoding = System.Text.Encoding.ASCII;
            //Console.OutputEncoding = System.Text.Encoding.ASCII;

            //Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < asciiCharacterBytes.Length; i++) {

                asciiCharByte = (byte)i;
                asciiCharacterBytes[i] = asciiCharByte;
                Console.Write(asciiCharByte.ToString("X2"));
                Console.Write(" ");

                asciiChar = Convert.ToChar(asciiCharByte);
                asciiCharacters[i] = asciiChar;
            }

            Console.WriteLine("TEST!");
            Console.WriteLine(BitConverter.ToChar(new byte[] { 0x02, 0x00 }, 0));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The first 32 characters of the ASCII set.");

            for (int i = 0; i < 32; i++) {

                Console.Write(asciiCharacters[i]);
                Console.Write(" ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The printable characters of the ASCII set from 32 to 126.");

            for (int i = 32; i < asciiCharacters.Length - 1; i++) {

                Console.Write(asciiCharacters[i]);
                Console.Write(" ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The character DEL at position 127 in ASCII set is non printable.");
            Console.Write(asciiCharacters[127]);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Unicode representations of the ASCII set.");
            Console.WriteLine();

            for (int i = 0; i < asciiCharacters.Length; i++) {

                Console.WriteLine($"U+{Convert.ToInt16(asciiCharacters[i]):X4}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Complete table for the ASCII set.");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
