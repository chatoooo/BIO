using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Core.FeatureVector;
using BIO.Framework.Extensions.Emgu.InputData;
using BIO.Framework.Extensions.Emgu.FeatureVector;
using System.Drawing;

//Emgu.CV.UI.ImageViewer.Show(new Image<Gray, Byte>(bestRotatedBitmap), "Ahoj");

namespace BIO.Projekt.Face3D
{
    class Face3DSilhouetteFeatureVectorExtractor : IFeatureVectorExtractor<EmguGrayImageInputData, Face3DSilhouetteFeatureVector>
    {
        // hranicne hodnoty rotacie
        public const int MIN_ROTATION_ANGLE = -5;
        public const int MAX_ROTATION_ANGLE = 5;

        public Face3DSilhouetteFeatureVector extractFeatureVector(EmguGrayImageInputData input)
        {
            if (input.Image.Width != 100 ||
                input.Image.Height != 100)
                throw new InvalidOperationException("Image size has to be 100x100 pixels");

            var originalImage = input.Image;
            // tu bude ulozena vysledna rotacia, z ktorej dostaneme os symetrie
            Bitmap bestRotatedBitmap = null;

            // koeficient symetrie pre uhol otocenia je suctom abs. hodnot rozdielou brightness hodnot symetrickych vyznacnych bodov
            int bestDiffSum = int.MaxValue;

            // proces vyhodnotenia symetrie na zaklade rotacie obrazku, vysledkom je snimok, ktoreho rovina symetrie je rovnobezna s osou Y
            for (int angle = MIN_ROTATION_ANGLE; angle < MAX_ROTATION_ANGLE + 1; angle++)
            {
                // natocenie povodneho obrazka o zadany uhol so zachovanim velkosti
                var rotatedBitmap = originalImage.Rotate(angle, new Gray(255), true).ToBitmap();
                
                int diffSum = 0;

                // porovnavanie vyznacnych bodov v symetrii
                diffSum += symetryPointsDiff(rotatedBitmap, 20, 20, 80, 20);
                diffSum += symetryPointsDiff(rotatedBitmap, 20, 25, 80, 25);
                diffSum += symetryPointsDiff(rotatedBitmap, 25, 33, 75, 33);
                diffSum += symetryPointsDiff(rotatedBitmap, 25, 39, 75, 39);
                diffSum += symetryPointsDiff(rotatedBitmap, 25, 45, 75, 45);
                diffSum += symetryPointsDiff(rotatedBitmap, 25, 51, 75, 51);
                diffSum += symetryPointsDiff(rotatedBitmap, 44, 33, 56, 33);
                diffSum += symetryPointsDiff(rotatedBitmap, 44, 36, 56, 36);
                diffSum += symetryPointsDiff(rotatedBitmap, 45, 45, 55, 45);
                diffSum += symetryPointsDiff(rotatedBitmap, 40, 65, 60, 65);

                // ak je novy koeficient symetrie lepsi, ulozi sa rotovana bitmapa
                if (diffSum < bestDiffSum)
                {
                    bestDiffSum = diffSum;
                    bestRotatedBitmap = rotatedBitmap;
                }
            }

            var symmetryPlaneX = getSymmetryPlaneXCoord(bestRotatedBitmap);
            
            var featureVector = new Face3DSilhouetteFeatureVector(100);

            //Bitmap newBmp222 = new Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //using (Graphics gfx = Graphics.FromImage(newBmp222))
            //{
            //    gfx.DrawImage(bestRotatedBitmap, 0, 0);
            //}

            // zbehni v smere y po linii x = 50
            for (int y = 0; y < 100; y++)
            {
                featureVector.Silhouette[y] = bestRotatedBitmap.GetPixel(symmetryPlaneX, y).G;
            //    newBmp222.SetPixel(symmetryPlaneX, y, System.Drawing.Color.Black);
            }


            //Emgu.CV.UI.ImageViewer.Show(new Image<Gray, Byte>(newBmp222), "Ahoj");

            return featureVector;
        }

        /**
         * Vypocita absolutnu hodnotu rozdielu hodnot brightness dvoch bodov obrazka
         */
        public static int symetryPointsDiff(Bitmap btmp, int x1, int y1, int x2, int y2)
        {
            var leftPixValue = btmp.GetPixel(x1, y1).R;
            var rightPixValue = btmp.GetPixel(x2, y2).R;
            return pointBrightnessDiff(leftPixValue, rightPixValue);
        }

        /**
         * Vypocet absolutnej hodnoty rozdielu hodnoty farebnej zlozky dvoch bodov
         */
        public static int pointBrightnessDiff(int leftPixValue, int rightPixValue)
        {
            return Math.Abs(leftPixValue - rightPixValue);
        }

        public static int getSymmetryPlaneXCoord(Bitmap bitmap)
        {
            int x1 = 30;
            int y1 = 75;
            int x2 = 70;
            int y2 = 75;

            while (bitmap.GetPixel(x1, y1).G < 200)
            {
                x1 += 2;
            }
            while (bitmap.GetPixel(x2, y2).G < 200)
            {
                x2 -= 2;
            }

            return (x1 + x2) / 2;
        }
    }
}
