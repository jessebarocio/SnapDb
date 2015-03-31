using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    class FileSnapStore<T> : ISnapStore<T>
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


        public IEnumerable<T> LoadRecords()
        {
            using (var stream = dbFile.OpenRead())
            {
                var result = serializer.Deserialize<IEnumerable<T>>(stream);
                return result ?? new List<T>();
            }
        }

        public void SaveRecords(IEnumerable<T> records)
        {
            using (var stream = dbFile.OpenWrite())
            {
                serializer.Serialize(records, stream);
            }
        }
    }
}
