using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapDb
{
    /// <summary>
    /// An interface representing a file that <see cref="FileSnapStore&lt; T &gt;"/> uses to read/write records.
    /// </summary>
    public interface ISnapDbFile
    {
        /// <summary>
        /// Opens the file for reading records.
        /// </summary>
        /// <returns>A Stream for reading records.</returns>
        Stream OpenRead();

        /// <summary>
        /// Opens the file for writing records.
        /// </summary>
        /// <returns>A Stream for writing records.</returns>
        Stream OpenWrite();
    }
}
