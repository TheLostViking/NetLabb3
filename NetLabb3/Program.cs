using System;
using System.Globalization;
using System.IO;

namespace NetLabb3
{
    class Program
    {
        static void Main(string[] args)
        {
            var fs = new FileStream(args[0], FileMode.Open);
            var picSize = (int)fs.Length;
            var picData = new byte[picSize];
            fs.Read(picData, 0, picSize);
            fs.Close();

            if (IsBMP(picData) == true)
            {
                Console.WriteLine("This is a .BMP-File!");
                Console.WriteLine($"The image resolution is {BMPWidth(picData)} * {BMPHeight(picData)} pixels.");
            }
            else if (IsPNG(picData) == true)
            {
                Console.WriteLine("This is a .PNG-File!");
                Console.WriteLine($"The image resolution is: {PNGWidth(picData)} * {PNGHeight(picData)} pixels.");
            }
            else
            {
                Console.WriteLine("This is neither a .BMP, nor a .PNG-File!");
            }

            static bool IsBMP(byte[] picData)
            {
                string isBMP = "";
                for (int i = 0; i < 2; i++)
                {
                    isBMP += picData[i].ToString("X2");
                }
                if (isBMP == "424D")
                {
                    return true;
                }
                return false;
            }

            static int BMPWidth(byte[] picData)
            {

                int bmpW = BitConverter.ToUInt16(picData, 18);
               
                return bmpW;
            }

            static int BMPHeight(byte[] picData)
            {
                int bmpH = BitConverter.ToUInt16(picData, 22);
                return bmpH;
            }

            static bool IsPNG(byte[] picData)
            {
                string isPNG = "";
                for (int i = 0; i < 8; i++)
                {
                    isPNG += picData[i].ToString("X2");
                }
                if (isPNG == "89504E470D0A1A0A")
                {
                    return true;
                }
                return false;
            }

            static int PNGWidth(byte[] picData)
            {
                string pngHexW = "";
                for (int i = 16; i < 20; i++)
                {
                    pngHexW += picData[i].ToString("X2");
                }
                int pngW = Int32.Parse(pngHexW, NumberStyles.HexNumber);
                return pngW;
            }

            static int PNGHeight(byte[] picData)
            {
                string pngHexH = "";
                for (int i = 21; i < 24; i++)
                {
                    pngHexH += picData[i].ToString("X2");
                }

                int pngH = Int32.Parse(pngHexH, NumberStyles.HexNumber);
                return pngH;
            }

            Console.ReadKey(true);


        }


    }
}





