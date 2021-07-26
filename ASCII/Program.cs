using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ASCII
{
    class Program
    {
        static void Main(string[] args)
        {

            //Ascii.WriteChar("../../../../../ASCII Table.txt");
            //Bitmap bitmap = new Bitmap("../../../../../Ascci list.png");
            //Ascii.CalculateAverage(bitmap);
            //AsciiDico.CreateDico("../../../../../Ascii_dico.txt");

            Bitmap bitmap;
            bitmap = new Bitmap("../../../../../Mandelbrot.png");
            
            
            Ascii.ToASCII(bitmap, 350, "../../../../../Mandelbrot.txt", false);
            //Bitmap newmap = Image_Filters.ApplyFilter(bitmap, Image_Filters.Yellow, 4);
            //newmap.Save("../../../../../Cyclotron - Cycloteam.png", ImageFormat.Png);
            
          
            Console.WriteLine("Done");
        }

        static void PrintPyArr()
        {
            foreach (KeyValuePair<int[],string> tuple in AsciiDico.AsciiDictionary)
            {
                Console.WriteLine($"[{tuple.Key[0]}, {tuple.Key[1]}, '{tuple.Value}'], ");
            }
        }
    }
}