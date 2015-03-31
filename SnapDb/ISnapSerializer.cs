using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    public interface ISnapSerializer
    {
        string Serialize(object value);
        T Deserialize<T>(string serializedObject);
    }
}
