using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Extensions.Standard.Evaluation.Block;
using BIO.Framework.Extensions.Emgu.InputData;
using BIO.Framework.Core.InputData;
using BIO.Framework.Extensions.Standard.Database.InputDatabase;

namespace BIO.Projekt.Face3D
{
    class Face3DEvaluationSettings :
        BlockEvaluationSettings<
        StandardRecord<StandardRecordData>, //standard database record
        EmguGrayImageInputData
    >
    {

        public Face3DEvaluationSettings()
        {
            this.addBlockToEvaluation(new Face3DSilhouetteProcessingBlockComponents().createBlock());

        }

        protected override IInputDataCreator<StandardRecord<StandardRecordData>, EmguGrayImageInputData> createInputDataCreator()
        {
            return new Face3DInputDataCreator();
        }
    }

}
