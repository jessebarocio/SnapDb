using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    /// <summary>
    /// An interface for the record serializer used by <see cref="FileSnapStore&lt; T &gt;"/> to serialize/deserialize records.
    /// </summary>
    public interface ISnapSerializer
    {
        /// <summary>
        /// Serializes the given object and writes it to an output stream.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="output">The output stream.</param>
        void Serialize(object value, Stream output);

        /// <summary>
        /// Deserializes the contents of the input stream into the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="input">The input stream.</param>
        /// <returns>An instance of <typeparamref name="T"/> deserialized from the input stream.</returns>
        T Deserialize<T>(Stream input);
    }
}
