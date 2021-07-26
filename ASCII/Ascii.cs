using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ASCII
{
    public class Ascii
    {
        /// <summary>
        /// Gives the average color of a grayscale image
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="x">Width of the the bottom left coordinate</param>
        /// <param name="y">Height of the the bottom left coordinate</param>
        /// <param name="xTo">Width of the the top right coordinate</param>
        /// <param name="yTo">Height of the the top right coordinate</param>
        /// <returns></returns>
        private static int Average(Bitmap image, int x, int y, int xTo, int yTo)
        {
            int nbpixel = 0;
            int sum = 0;
            for (int i = x; i <= xTo; i++)
            {
                for (int j = y; j <= yTo; j++)
                {
                    sum += image.GetPixel(i, j).R;
                    nbpixel += 1;
                }
            }

            return sum / nbpixel;
        }

        private static string FindLetter(int average)
        {
            string res = "";
            foreach (int[] key in AsciiDico.AsciiDictionary.Keys)
            {
                (int min, int max) = (key[0], key[1]);
                if (min <= average && average <= max)
                {
                    res = AsciiDico.AsciiDictionary[key];
                }
            }

            return res;

        }
        
        /// <summary>
        /// Write the ascii code of an image in a file
        /// </summary>
        /// <param name="image"></param>
        /// <param name="nbchar">the number of ascii chars in width</param>
        /// <param name="path">Where the file will go</param>
        public static void ToASCII(Bitmap image, int nbchar, string path, bool inversed)
        {
            if (!IsGrayScale(image, 20))
                image = ToGrayScale(image);
            
            StreamWriter writer = new StreamWriter(path);
            int dx = image.Width / nbchar;
            int dy = 2 * dx;
            for (int i = 0; i < image.Height; i += dy)
            {
                int iTo = i + dy;
                if (iTo >= image.Height)
                    iTo = image.Height - 1;
                for (int j = 0; j < image.Width; j += dx)
                {
                    int jTo = j + dx;
                    if (jTo >= image.Width)
                        jTo = image.Width - 1;
                    int average = Average(image, j, i, jTo, iTo);
                    if (inversed)
                        average = 255 - average;
                    writer.Write(FindLetter(average));
                }
                writer.WriteLine();
            }
            writer.Close();

        }

        /// <summary>
        /// Extracts the chars from a ascii dico (could be replaced by a loop from 33 to 126)
        /// </summary>
        /// <param name="path"></param>
        public static void WriteChar(string path)
        {
            StreamReader reader = new StreamReader(path);
            string s;
            List<string> Inputs = new List<string>();
            while ((s = reader.ReadLine()) != null)
            {
                string[] chara = s.Split("	");
                Console.WriteLine(chara[4]);
            }
        }

        /// <summary>
        /// Calculate the average for the dico creation
        /// </summary>
        /// <param name="bitmap"> the image containing all the chars from '33' to '126'</param>
        private static void CalculateAverage(Bitmap bitmap)
        {
            int deltay = bitmap.Height / 94;
            for (int i = 0; i < 93; i++)
            {
                int y = i * deltay;
                int yto = (i + 1) * deltay;
                int average = Average(bitmap, 0, y, bitmap.Width - 1, yto);
                Console.WriteLine($"{(char)(i+33)},{average}");
            }
            
        }

        private static bool IsGrayScale(Bitmap bitmap, int precision)
        {
            for (int i = 0; i < precision; i++)
            {
                Color pixel = bitmap.GetPixel(new Random().Next(bitmap.Width), new Random().Next(bitmap.Height));
                if (pixel.R != pixel.B || pixel.R != pixel.G)
                {
                    Console.WriteLine("Not a GrayScale");
                    return false;
                }
            }
            return true;
        }

        public static Bitmap ToGrayScale(Bitmap bitmap)
        {
            Bitmap newbm = new Bitmap(bitmap.Width, bitmap.Height);
            
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixel = bitmap.GetPixel(i, j);
                    int value = (21 * pixel.R) / 100 + (72 * pixel.G) / 100 + (7 * pixel.B) / 100;
                    newbm.SetPixel(i, j, Color.FromArgb(value, value, value));
                }
            }

            return newbm;
        }

        private static void TestFunc()
        {
            Bitmap bitmap = new Bitmap(10, 10, PixelFormat.Alpha);
        }
    }
}