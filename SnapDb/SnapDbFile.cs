using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    internal class SnapDbFile : ISnapDbFile
    {
        string dbFilePath;

        public SnapDbFile(string path)
        {
            this.dbFilePath = path;
        }

        public Stream OpenRead()
        {
            return File.OpenRead(dbFilePath);
        }

        public Stream OpenWrite()
        {
            return File.Open(dbFilePath, FileMode.Create);
        }
    }
}
