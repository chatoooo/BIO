using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Core.FeatureVector;

namespace BIO.Projekt.Face3D
{
    [Serializable]
    class Face3DSilhouetteFeatureVector : IFeatureVector
    {
        public Face3DSilhouetteFeatureVector(int size)
        {
            this.Silhouette = new int[size];
        }

        public int[] Silhouette;
    }
}
