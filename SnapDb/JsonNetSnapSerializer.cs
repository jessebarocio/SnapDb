using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    class JsonNetSnapSerializer : ISnapSerializer
    {
        private static JsonSerializer serializer = new JsonSerializer()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public string Serialize(object value)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string serializedObject)
        {
            throw new NotImplementedException();
        }
    }
}
