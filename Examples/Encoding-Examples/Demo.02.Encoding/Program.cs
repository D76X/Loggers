using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace Demo._02.Encoding {

    class Program {

        /// <summary>
        /// http://aiweb.cs.washington.edu/research/projects/xmltk/xmldata/
        /// http://xml.ascc.net/
        /// https://github.com/notepad-plus-plus/notepad-plus-plus/blob/master/PowerEditor/installer/nativeLang/chinese.xml
        /// https://stackoverflow.com/questions/5165347/what-use-is-the-encoding-in-the-xml-header
        /// https://stackoverflow.com/questions/13743250/meaning-of-xml-version-1-0-encoding-utf-8/27398439#27398439
        /// https://stackoverflow.com/questions/427725/how-to-put-an-encoding-attribute-to-xml-other-that-utf-16-with-xmlwriter
        /// https://www.w3.org/International/questions/qa-html-encoding-declarations
        /// https://stackoverflow.com/questions/7007427/does-a-valid-xml-file-require-an-xml-declaration
        /// https://stackoverflow.com/questions/1026247/check-well-formed-xml-without-a-try-catch/15835251
        /// https://stackoverflow.com/questions/29915467/there-is-no-unicode-byte-order-mark-cannot-switch-to-unicode
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

            string basePath = @"..\..";

            //string basePath = Path.Combine(basePath,);

            string[] xmlFiles = Directory.GetFiles(basePath, "*.xml");
            Exception e = null;

            foreach (var xmlFile in xmlFiles) {

                if (IsXmlWellFormed(xmlFile, out e)) {
                    Console.WriteLine($"well formed : {xmlFile}");
                }
                else {
                    Console.WriteLine($"not well formed : {xmlFile}");
                    Console.WriteLine($"{e.Message}");
                }

                Console.WriteLine();
                e = null;
            }

            //--------------------------------------------------------------------------------

            // 01.NotePadPlusMenu.xml 

            // This file is a text file with xml content. The binary format in which the 
            // file is saved in UTF-8. The attribute encoding="utf-8" is correctly set so
            // that an XML processor can use this as an hint to verify the encoding of the
            // binary content.

            //--------------------------------------------------------------------------------

            // 02.NotePadPlusMenu.WrongEncoding.xml

            // In this documet the only change is that the value of the encoding attribute
            // of the xml element is now set to encoding="utf-16" instead of the original 
            // value encoding="utf-8". However, the binary content has not changed and it 
            // remains a UTF-8 encoded file. The parser detects that the encoding is UTF-8
            // by using a default algorithm and fids this to be in constrast to the value
            // assigned to the attribute encoding of the document "utf-16".

            //--------------------------------------------------------------------------------

            // 03.NotepadPlusMenu.NoEncoding.xml
            // In this document the encoding attribute is missing but this is still all right
            // because in version 1.0 of the xml specification the attribute encoding is optional.

            //--------------------------------------------------------------------------------

            // 04.NotepadPlusMenu.US-ASCII.xml
            // 05.NotepadPlusMenu.US-ASCII.NoEncoding.xml

            // This document is (permanetly) corrupted and will not be displayed properly
            // in text or xml editors, yet it is well formed. The parser runs the default 
            // algorithm to figure out the encoding but it cannot determine what the right 
            // encoding should be.

            // To produce this document I opened the NotePadPlusMenu.xml and saved it as a 
            // new document with the encoding US-ASCII. 

            // The problem is that the original encoding of the document is UTF-8 and it 
            // contains some characters that obviously have no single byte code in ASCII 
            // (chinese). By saving it with US-ASCII encoding the bytes of these characters 
            // fall out of the ASCII range and cannot be recovered unless a consumer could 
            // know or guess that the original encoding of the document was UTF-8.

            //--------------------------------------------------------------------------------

            Console.ReadKey();
        }

        private static bool IsXmlWellFormed(string xmlFilePath, out Exception exc) {

            XmlReader xmlrd = new XmlTextReader(xmlFilePath);            

            exc = null;

            try {
                while (xmlrd.Read()) ;
                return true;
            }
            catch (Exception e) {

                exc = e;
                return false;
            }
            finally {
                xmlrd.Dispose();
            }
        }       
    }
}
