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
            var extrSilhouette = extracted.Silhouette;
            var templateSilhouette = templated.Silhouette;
            for (int i = 0; i < extrSilhouette.Length; i++)
            {
                sum += Math.Abs(extrSilhouette[i] - templateSilhouette[i]);
            }

            return new MatchingScore(sum);
        }
    }
}
