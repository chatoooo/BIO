using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Extensions.Standard.Block;
using BIO.Framework.Extensions.Emgu.InputData;
using BIO.Framework.Extensions.Emgu.FeatureVector;
using BIO.Framework.Extensions.Standard.Template;
using BIO.Framework.Core.FeatureVector;
using BIO.Framework.Core.Comparator;
using BIO.Framework.Extensions.Standard.Comparator;

namespace BIO.Projekt.Face3D
{
    class Face3DSilhouetteProcessingBlockComponents : InputDataProcessingBlockSettings<
        EmguGrayImageInputData,
        Face3DSilhouetteFeatureVector,
        Template<Face3DSilhouetteFeatureVector>,
        Face3DSilhouetteFeatureVector
        >
    {

        /// <summary>
        /// Inicializace bloku
        /// </summary>
        public Face3DSilhouetteProcessingBlockComponents()
            : base("3D Face Silhouette")
        {

        }

        /// <summary>
        /// Extraktor vektoru rysů, který bude použit jako šablona ze vstupních dat.
        /// </summary>
        protected override IFeatureVectorExtractor<EmguGrayImageInputData, Face3DSilhouetteFeatureVector>
            createTemplatedFeatureVectorExtractor()
        {
            return new Face3DSilhouetteFeatureVectorExtractor();
        }

        /// <summary>
        /// Extraktor vektoru rysů ze vstupních dat.
        /// </summary>
        protected override IFeatureVectorExtractor<EmguGrayImageInputData, Face3DSilhouetteFeatureVector>
            createEvaluationFeatureVectorExtractor()
        {
            return new Face3DSilhouetteFeatureVectorExtractor();
        }

        /// <summary>
        /// Registrace porovnávacího algoritmu. Porovnává se šablona v databázi s aktuálním vektorem rysů.
        /// </summary>
        protected override IComparator<Face3DSilhouetteFeatureVector, Template<Face3DSilhouetteFeatureVector>, Face3DSilhouetteFeatureVector>
            createComparator()
        {
            return
                new Comparator<Face3DSilhouetteFeatureVector, Template<Face3DSilhouetteFeatureVector>, Face3DSilhouetteFeatureVector>(
                    CreateFeatureVectorComparator(), CreateScoreSelector());
        }

        /// <summary>
        /// Registrace porovnávacího algoritmu. Porovnává se šablona v databázi s aktuálním vektorem rysů.
        /// </summary>
        private static IFeatureVectorComparator<Face3DSilhouetteFeatureVector, Face3DSilhouetteFeatureVector>
            CreateFeatureVectorComparator()
        {
            return new Face3DSilhouetteComparator();
        }

        /// <summary>
        /// Jako výsledné skóre porovnání je vybrána nejmenší hodnota
        /// </summary>
        /// <returns></returns>
        private static IScoreSelector CreateScoreSelector()
        {
            return new MinScoreSelector();
        }
    }
}
