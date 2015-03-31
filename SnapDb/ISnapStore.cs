using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    public interface ISnapStore
    {
        IEnumerable<T> LoadRecords<T>();
        void SaveRecords<T>(IEnumerable<T> records);
    }
}
