using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    /// <summary>
    /// A file system implementation of ISnapDbFile.
    /// </summary>
    public class SnapDbFile : ISnapDbFile
    {
        string dbFilePath;


        /// <summary>
        /// Creates a new SnapDbFile at the given path.
        /// </summary>
        /// <param name="path"></param>
        public SnapDbFile(string path)
        {
            this.dbFilePath = path;
        }


        /// <summary>
        /// Opens the file for reading.
        /// </summary>
        /// <returns>An open FileStream.</returns>
        public Stream OpenRead()
        {
            return File.Open(dbFilePath, FileMode.OpenOrCreate, FileAccess.Read);
        }

        /// <summary>
        /// Opens the file for writing.
        /// </summary>
        /// <returns>An open FileStream.</returns>
        public Stream OpenWrite()
        {
            return File.Open(dbFilePath, FileMode.Create);
        }
    }
}
