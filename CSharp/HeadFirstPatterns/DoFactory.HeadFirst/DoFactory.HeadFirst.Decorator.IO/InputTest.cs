using System;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

namespace DoFactory.HeadFirst.Decorator.IO
{
    // InputTest test application

    class InputTest
    {
        static void Main()
        {
            // Get fully qualified file names
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            path = path.Substring(0, path.IndexOf(@"\bin") + 1);

            string inFile  = path + "MyInFile.txt";
            string outFile = path + "MyOutFile.txt";
            string encFile = path + "MyEncFile.txt";

            FileStream fin = new FileStream(inFile, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outFile, FileMode.OpenOrCreate, FileAccess.Write);

            // Clear output file
            fout.SetLength(0);
       
            //Create variables to help with read and write.
            int rdlen = 0;              
            long totlen = fin.Length;   
            byte[] bin = new byte[100]; 
            int len;

            //Read from the input file, then write directly output file.
            while(rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                fout.Write(bin,0,len);
                rdlen += len;
            }
            fout.Close();
            fin.Close();                   

            Console.WriteLine(@"Created unencrypted MyOutFile.txt");

            // -- Now use CryptoStream as Decorator --

            fin = new FileStream(inFile, FileMode.Open, FileAccess.Read);
            fout = new FileStream(encFile, FileMode.OpenOrCreate, FileAccess.Write);

            // Clear output file
            fout.SetLength(0);

            // Setup Triple DES encryption
            TripleDESCryptoServiceProvider des3 = new TripleDESCryptoServiceProvider();
            byte[] key = HexToBytes("EA81AA1D5FC1EC53E84F30AA746139EEBAFF8A9B76638895");
            byte[] IV = HexToBytes("87AF7EA221F3FFF5");

            // CryptoStream 'decorates' output stream
            Console.WriteLine("\nDecorate output stream with CryptoStream...");
            CryptoStream fenc = new CryptoStream(
                fout, des3.CreateEncryptor(key, IV), CryptoStreamMode.Write);

            // Read from the input file, then write encrypted to the output file
            rdlen = 0;
            while(rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                fenc.Write(bin,0,len);
                rdlen += len;
            }

            fin.Close();
            fout.Close();

            Console.WriteLine(@"Created encrypted MyEncFile.txt");
                
            // Wait for user
            Console.ReadKey();
        }

        // Utility method: convert hex string to bytes

        public static byte[] HexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
            {
                string code = hex.Substring(i*2,2);
                bytes[i] = byte.Parse(code, System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }
    }
}