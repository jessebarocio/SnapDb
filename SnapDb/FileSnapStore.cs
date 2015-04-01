using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    /// <summary>
    /// An implementation of ISnapStore that serializes records and saves them to a file.
    /// </summary>
    /// <typeparam name="T">The record type.</typeparam>
    public class FileSnapStore<T> : ISnapStore<T>
    {
        private ISnapDbFile dbFile;
        private ISnapSerializer serializer;


        /// <summary>
        /// Initializes a new FileSnapStore using a Json serializer at the given path.
        /// </summary>
        /// <param name="path">The path for the database file.</param>
        public FileSnapStore(string path)
            : this(new SnapDbFile(path), new JsonNetSnapSerializer()) { }

        /// <summary>
        /// Initializes a new FileSnapStore using the given SnapDbFile and SnapSerializer.
        /// </summary>
        /// <param name="snapDbFile">An implementation of ISnapDbFile used to read/write records.</param>
        /// <param name="snapSerializer">An implementation of ISnapSerializer used to serialize the records.</param>
        public FileSnapStore(ISnapDbFile snapDbFile, ISnapSerializer snapSerializer)
        {
            this.dbFile = snapDbFile;
            this.serializer = snapSerializer;
        }


        /// <summary>
        /// Loads and deserializes the records from the SnapDbFile.
        /// </summary>
        /// <returns>An IEnumerable of type <typeparamref name="T"/> containing all of the records.</returns>
        public IEnumerable<T> LoadRecords()
        {
            using (var stream = dbFile.OpenRead())
            {
                var result = serializer.Deserialize<IEnumerable<T>>(stream);
                return result ?? new List<T>();
            }
        }

        /// <summary>
        /// Serializes and saves the records to the SnapDbFile.
        /// </summary>
        /// <param name="records">An IEnumerable of type <typeparamref name="T"/> to serialize/write to the SnapDbFile.</param>
        public void SaveRecords(IEnumerable<T> records)
        {
            using (var stream = dbFile.OpenWrite())
            {
                serializer.Serialize(records, stream);
            }
        }
    }
}
