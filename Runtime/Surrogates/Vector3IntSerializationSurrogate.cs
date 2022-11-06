using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Unity.Runtime.Surrogates
{
    internal sealed class Vector3IntSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3Int = (Vector3Int) obj;
            info.AddValue("x", vector3Int.x);
            info.AddValue("y", vector3Int.y);
            info.AddValue("z", vector3Int.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var vector3Int = (Vector3Int) obj;
            vector3Int.x = (int) info.GetValue("x", typeof(int));
            vector3Int.y = (int) info.GetValue("y", typeof(int));
            vector3Int.z = (int) info.GetValue("z", typeof(int));
            obj = vector3Int;

            return obj;
        }
    }
}