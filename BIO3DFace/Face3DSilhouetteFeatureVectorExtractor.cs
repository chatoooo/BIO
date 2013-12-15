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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

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

            int max = originalImage.Bitmap.GetPixel(49, 49).B;
            int x = 49,y = 49;
            int nextX = 0, nextY = 0;
            while (true)
            {
                
                for (int ix = -4; ix < 5; ix++)
                {
                    for (int iy = -4; iy < 5; iy++)
                    {
                        int val = originalImage.Bitmap.GetPixel(x+ix, y+iy).B;
                        if (val > max)
                        {
                            max = val;
                            nextX = x + ix;
                            nextY = y + iy;
                        }
                    }
                }
                if (nextX == x && nextY == y)
                {
                    break;
                }
                x = nextX;
                y = nextY;
            }
            var featureVector = new Face3DSilhouetteFeatureVector(200);
            for (int yp = 0; yp < 100; yp++)
            {
                int pos = x;
                if(pos < 0)
                    pos += 100;
                featureVector.Silhouette[yp] = originalImage.Bitmap.GetPixel(pos, yp).G;
            }
            for (int yp = 0; yp < 100; yp++)
            {
                int pos = y;
                if (pos < 0)
                    pos += 100;
                featureVector.Silhouette[100+yp] = originalImage.Bitmap.GetPixel(yp, pos).G;
            }
            return featureVector;
        }
    }
}
