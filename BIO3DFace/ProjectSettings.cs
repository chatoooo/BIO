using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Core.Database;
using BIO.Framework.Core.Evaluation.Block;
using BIO.Framework.Extensions.Standard.Database.InputDatabase;
using BIO.Framework.Extensions.Emgu.InputData;
using BIO.Project;

namespace BIO.Projekt.Face3D
{
    class ProjectSettings :
        ProjectSettings<StandardRecord<StandardRecordData>, EmguGrayImageInputData>,
        IStandardProjectSettings<StandardRecord<StandardRecordData>>
    {

        /// <summary>
        /// Počet vstupních vzorků na osobu pro tvorbu šablony
        /// </summary>
        public int TemplateSamples
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Vytvoření databáze
        /// </summary>
        public override IDatabaseCreator<StandardRecord<StandardRecordData>> getDatabaseCreator()
        {
            return new Face3DDatabaseCreator(@"c:\Users\Honza\Documents\Skola\BIO\frgc");
        }

        /// <summary>
        /// Registrace bloku
        /// </summary>
        protected override IBlockEvaluatorSettings<StandardRecord<StandardRecordData>, EmguGrayImageInputData>
            getEvaluatorSettings()
        {
            return new Face3DEvaluationSettings();
        }
    }
}
