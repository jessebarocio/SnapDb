using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    internal class JsonNetSnapSerializer : ISnapSerializer
    {
        private static JsonSerializer serializer = new JsonSerializer()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };


        public void Serialize(object value, Stream output)
        {
            using (var writer = new StreamWriter(output))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, value);
            }
        }

        public T Deserialize<T>(Stream input)
        {
            using (var reader = new StreamReader(input))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
