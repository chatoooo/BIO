using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Core.FeatureVector;
using BIO.Framework.Extensions.Emgu.InputData;
using BIO.Framework.Extensions.Emgu.FeatureVector;
using System.Drawing;

namespace BIO.Projekt.Face3D
{
    class Face3DSilhouetteFeatureVectorExtractor : IFeatureVectorExtractor<EmguGrayImageInputData, Face3DSilhouetteFeatureVector>
    {
        public Face3DSilhouetteFeatureVector extractFeatureVector(EmguGrayImageInputData input)
        {
            if (input.Image.Width != 100 ||
                input.Image.Height != 100)
                throw new InvalidOperationException("Image size has to be 100x100 pixels");

            var originalImage = input.Image;
            var mirrorImage = input.Image.Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);

            if (input.FileName.Equals("d:\\db\\face\\2D\\frgc\\2463-1-small-range.png"))
            {

            }

            // vytvorenie deketoru SURF
            SURFDetector detector = new SURFDetector(500, false);

            // detekcia SURF v originalnom snimku
            ImageFeature[] origImageSURF = detector.DetectFeatures(originalImage, null);

            if (input.FileName.Equals("d:\\db\\face\\2D\\frgc\\2463-1-small-range.png"))
            {
                Bitmap origBitmap = originalImage.ToBitmap();
                foreach (ImageFeature p in origImageSURF)
                {
                    origBitmap.SetPixel((int)p.KeyPoint.Point.X, (int)p.KeyPoint.Point.Y, System.Drawing.Color.Red);
                }
                Image<Emgu.CV.Structure.Gray, Byte> origImageCopy = new Image<Emgu.CV.Structure.Gray, Byte>(origBitmap);

                Emgu.CV.UI.ImageViewer.Show(origImageCopy, "SURF");
            }

            // // detekcia SURF v zrkadlovo otocenom snimku
            ImageFeature[] mirrorImageSURF = detector.DetectFeatures(mirrorImage, null);

            PointF x = origImageSURF[1].KeyPoint.Point;
            
            var featureVector = new Face3DSilhouetteFeatureVector(100);

            for (int y = 0; y < 100; y++)
            {
                featureVector.Silhouette[y] = originalImage.Bitmap.GetPixel(50, y).G;
            }

            return featureVector;
        }
    }
}
