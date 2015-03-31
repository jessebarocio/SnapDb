using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    class FileSnapStore : ISnapStore
    {
        private ISnapDbFile dbFile;
        private ISnapSerializer serializer;


        public FileSnapStore(string path)
            : this(new SnapDbFile(path), new JsonNetSnapSerializer()) { }

        internal FileSnapStore(ISnapDbFile snapDbFile, ISnapSerializer snapSerializer)
        {
            this.dbFile = snapDbFile;
            this.serializer = snapSerializer;
        }


        public IEnumerable<T> LoadRecords<T>()
        {
            using (var stream = dbFile.OpenRead())
            {
                var result = serializer.Deserialize<IEnumerable<T>>(stream);
                return result ?? new List<T>();
            }
        }

        public void SaveRecords<T>(IEnumerable<T> records)
        {
            using (var stream = dbFile.OpenWrite())
            {
                serializer.Serialize(records, stream);
            }
        }
    }
}
