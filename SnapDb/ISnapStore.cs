using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    /// <summary>
    /// An interface for loading and saving SnapDb records. Implement this interface to create custom SnapStore types that a
    /// SnapRepository can work with.
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public interface ISnapStore<T>
    {
        /// <summary>
        /// Loads all records from the store.
        /// </summary>
        /// <returns>An IEnumerable containing all records.</returns>
        IEnumerable<T> LoadRecords();

        /// <summary>
        /// Saves all records to the store.
        /// </summary>
        /// <param name="records">An IEnumerable containing all of the records to save.s</param>
        void SaveRecords(IEnumerable<T> records);
    }
}
