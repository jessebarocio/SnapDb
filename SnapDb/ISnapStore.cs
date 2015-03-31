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
        IEnumerable<T> LoadRecords();
        void SaveRecords(IEnumerable<T> records);
    }
}
