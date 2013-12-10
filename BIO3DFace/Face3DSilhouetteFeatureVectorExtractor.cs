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
            //var mirrorImage = input.Image.Flip(Emgu.CV.CvEnum.FLIP.VERTICAL);
            

            var featureVector = new Face3DSilhouetteFeatureVector(100);

            for (int y = 0; y < 100; y++)
            {
                featureVector.Silhouette[y] = originalImage.Bitmap.GetPixel(50, y).G;
            }

            

            return featureVector;
        }
    }
}
