using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    public interface ISnapSerializer
    {
        void Serialize(object value, Stream output);
        T Deserialize<T>(Stream input);
    }
}
