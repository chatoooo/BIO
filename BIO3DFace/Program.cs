using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Project;
using BIO.Framework.Extensions.Standard.Database.InputDatabase;

namespace BIO.Projekt.Face3D
{
    class Program
    {
        static void Main(string[] args)
        {

            var settings = new ProjectSettings();
            var project = new StandardProject<StandardRecord<StandardRecordData>>(settings);
            var results = project.run();
        }
    }
}
