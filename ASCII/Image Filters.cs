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
    public class Image_Filters
    {
        /// <summary>
        /// As the name states it apply a filter function on each pixel of the image
        /// </summary>
        /// <param name="image"> The image to modify</param>
        /// <param name="filter"> The function to apply on each pixel</param>
        public static Bitmap ApplyFilter(Bitmap image, Func<Color, Bitmap, int, int, int, Color> filter, int coef)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    
                    bitmap.SetPixel(i, j, filter(image.GetPixel(i, j), image, i, j, coef));
                    //Console.WriteLine($"Applying filter on the pixel {i},{j}, with the color from : {image.GetPixel(i, j)} to {bitmap.GetPixel(i, j)}");
                }
            }

            return bitmap;
        }
        /// <summary>
        /// A Black and White filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color BlackAndWhite(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            if ((color.R + color.G + color.B) / 3 >= 127)
                return Color.White;
            else
            {
                return Color.Black;
            }
        }

        /// <summary>
        /// A Yellow filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Yellow(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            return Color.FromArgb(color.R, color.G, 0);
        }

        /// <summary>
        /// A Grayscale filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Grayscale(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            int colour = (21 * color.R) / 100 + (72 * color.G) / 100 + (7 * color.B) / 100;
            return Color.FromArgb(colour, colour, colour);
        }

        /// <summary>
        /// A Negative filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Negative(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }

        /// <summary>
        /// Remove the maxes of the composants of the color
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color RemoveMaxes(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            (int r, int g, int b) = (color.R, color.G, color.B);
            (int rCal, int gCal, int bCal) = (1, 1, 1);
            if (r >= g && r >= b)
                rCal = 0;
            if (g >= r && g >= b)
                gCal = 0;
            if (b >= g && b >= r)
                bCal = 0;
            return Color.FromArgb(r * rCal, g * gCal, b * bCal);
        }
        
        public static Color Blur(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            int nbpixel = 0;
            (int R, int G, int B) sum = (0, 0, 0);
            int i   = x - coef;
            int iTo = x + coef;
            i = i < 0 ? 0 : i;
            iTo = iTo >= bitmap.Width ? bitmap.Width - 1 : iTo;
            for (; i <= iTo; i++)
            {
                int j = y - coef;
                int jTo = y + coef;
                j = j < 0 ? 0 : j;
                jTo = jTo >= bitmap.Height ? bitmap.Height - 1 : jTo;
                for (; j <= jTo; j++)
                {
                    sum.R += bitmap.GetPixel(i, j).R;
                    sum.G += bitmap.GetPixel(i, j).G;
                    sum.B += bitmap.GetPixel(i, j).B;
                    nbpixel += 1;
                }
            }
            Color Mycolor = Color.FromArgb(sum.R / nbpixel, sum.G / nbpixel,sum.B / nbpixel);
            return Mycolor;
        }
        
        public static Color Pixelise(Color color, Bitmap bitmap, int x, int y, int coef)
        {
            int nbpixel = 0;
            (int R, int G, int B) sum = (0, 0, 0);
            int i   = x/coef * coef;
            int iTo =(x/coef + 1) * coef;
            i = i < 0 ? 0 : i;
            iTo = iTo >= bitmap.Width ? bitmap.Width - 1 : iTo;
            for (; i <= iTo; i++)
            {
                int j = y/coef * coef;
                int jTo = (y/coef + 1) * coef;
                j = j < 0 ? 0 : j;
                jTo = jTo >= bitmap.Height ? bitmap.Height - 1 : jTo;
                for (; j <= jTo; j++)
                {
                    sum.R += bitmap.GetPixel(i, j).R;
                    sum.G += bitmap.GetPixel(i, j).G;
                    sum.B += bitmap.GetPixel(i, j).B;
                    nbpixel += 1;
                }
            }
            Color Mycolor = Color.FromArgb(sum.R / nbpixel, sum.G / nbpixel,sum.B / nbpixel);
            return Mycolor;
        }
        
    }
}