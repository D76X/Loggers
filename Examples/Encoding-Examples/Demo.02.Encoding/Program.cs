using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo._02.Encoding {

    class Program {

        /// <summary>
        /// http://aiweb.cs.washington.edu/research/projects/xmltk/xmldata/
        /// http://xml.ascc.net/
        /// https://github.com/notepad-plus-plus/notepad-plus-plus/blob/master/PowerEditor/installer/nativeLang/chinese.xml
        /// https://stackoverflow.com/questions/5165347/what-use-is-the-encoding-in-the-xml-header
        /// https://stackoverflow.com/questions/427725/how-to-put-an-encoding-attribute-to-xml-other-that-utf-16-with-xmlwriter
        /// https://www.w3.org/International/questions/qa-html-encoding-declarations
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

            // -1
            // NotePadPlusMenu.xml 

            // This file is a text file with xml content. The binary format in which the 
            // file is saved in UTF-8. The attribute encoding="utf-8" is correctly set so
            // that an XML processor can use this as an hint to verify the encoding of the
            // binary content.

            // -2
            // NotePadPlusMenu.WrongEncoding.xml

            // In this documet the only change is that the value of the encoding attribute
            // of the xml element is now set to encoding="utf-16" instead of the original 
            // value encoding="utf-8". However, the binary content has not changed and it 
            // remains a UTF-8 encoded file. 

            // why does this still work? ...........................


            // -3
            // NotepadPlusMenu.NoEncoding.xml
            // In this document the encoding is missing.

            // why does this still work? ...........................

            // - 4
            // NotepadPlusMenu.US-ASCII.xml
            // This document is (permanetly) corrupted and will not be displayed properly
            // in text or xml editors. 
            // To produce this document I opened the NotePadPlusMenu.xml and saved it as a 
            // new document with the encoding US-ASCII and also changed the attribute encoding of 
            // the xml document to encoding="us-ascii". The problem is that the original encoding 
            // of the document is UTF-8 and it contains some characters that obviously have no 
            // single byte code in ASCII (chinese). By saving it with US-ASCII encoding the bytes 
            // of these characters fall out of the ASCII range and cannot be recovered unless a 
            // consumer could know or guess that the original encoding of the document was UTF-8.
        }
    }
}
