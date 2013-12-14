using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Core.FeatureVector;
using BIO.Framework.Extensions.Emgu.InputData;
using BIO.Framework.Extensions.Emgu.FeatureVector;
using BIO.Framework.Core.Comparator;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace BIO.Projekt.Face3D
{
    class Face3DSilhouetteComparator : IFeatureVectorComparator<Face3DSilhouetteFeatureVector, Face3DSilhouetteFeatureVector>
    {
        public MatchingScore computeMatchingScore(Face3DSilhouetteFeatureVector extracted, Face3DSilhouetteFeatureVector templated)
        {
            double sum = 0;

            int n = extracted.Silhouette.GetLength(0);
            PointF[] src = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                src[i].X = i;
                src[i].Y = extracted.Silhouette[i];
            }

            PointF[] dst = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                dst[i].X = i;
                dst[i].Y = templated.Silhouette[i];
            }

            HomographyMatrix matrix = CameraCalibration.FindHomography(src, dst, HOMOGRAPHY_METHOD.RANSAC,1);
            matrix.ProjectPoints(src);

            foreach (PointF p in src)
            {
                int i = (int)Math.Floor(p.X);
                if (i > n - 1 || i < 0)
                {
                    continue;
                }
                    
                sum += Math.Abs(p.Y - dst[i].Y);
            }
            return new MatchingScore(sum);
        }
    }
}
