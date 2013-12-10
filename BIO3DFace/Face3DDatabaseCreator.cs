using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIO.Framework.Core.Database;
using BIO.Framework.Extensions.Standard.Database.InputDatabase;
using System.IO;

namespace BIO.Projekt.Face3D
{
    class Face3DDatabaseCreator :
        IDatabaseCreator<StandardRecord<StandardRecordData>>
    {
        readonly string _databasePath;

        public Face3DDatabaseCreator(string databasePath)
        {
            _databasePath = databasePath;
        }

        public Database<StandardRecord<StandardRecordData>> createDatabase()
        {
            var database = new Database<StandardRecord<StandardRecordData>>();

            var di = new DirectoryInfo(_databasePath);
            var files = di.GetFiles("*-small-range.png");
            foreach (var f in files)
            {
                var parts = f.Name.Split(new[] { '-' });

                BiometricID bioID = new BiometricID(parts[0], "face");
                StandardRecordData data = new StandardRecordData(f.FullName);
                StandardRecord<StandardRecordData> record = new StandardRecord<StandardRecordData>(f.Name, bioID, data);

                database.addRecord(record);
            }

            return database;
        }
    }
}
