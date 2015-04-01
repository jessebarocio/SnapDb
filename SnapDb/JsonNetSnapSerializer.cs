using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    /// <summary>
    /// A Json.NET implementation of ISnapSerializer.
    /// </summary>
    public class JsonNetSnapSerializer : ISnapSerializer
    {
        private readonly JsonSerializer serializer;


        /// <summary>
        /// Creates a new JsonNetSnapSerializer using a default Json.NET serializer.
        /// </summary>
        public JsonNetSnapSerializer()
            : this(new JsonSerializer() { TypeNameHandling = TypeNameHandling.Objects }) { }

        /// <summary>
        /// Creates a new JsonNetSnapSerializer using the given Json.NET serializer.
        /// </summary>
        /// <param name="serializer"></param>
        public JsonNetSnapSerializer(JsonSerializer serializer)
        {
            this.serializer = serializer;
        }


        /// <summary>
        /// Serializes and writes the given object to the output stream.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="output">The Stream to write the serialized object to.</param>
        public void Serialize(object value, Stream output)
        {
            using (var writer = new StreamWriter(output))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize( jsonWriter, value );
            }
        }

        /// <summary>
        /// Reads in and deserializes the input stream into the given type.
        /// </summary>
        /// <typeparam name="T">The desired output type.</typeparam>
        /// <param name="input">The stream to read in and deserialize.</param>
        /// <returns>An instance of <typeparamref name="T"/> deserialized from the input stream.</returns>
        public T Deserialize<T>(Stream input)
        {
            using (var reader = new StreamReader(input))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize<T>( jsonReader );
            }
        }
    }
}
