// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
    public static class SurrogateSelectorExtension
    {
        public static void AddAllUnitySurrogate(this SurrogateSelector surrogateSelector)
        {
            var streamingContext = new StreamingContext(StreamingContextStates.All);
            
            var colorSerializationSurrogate = new ColorSerializationSurrogate();
            var quaternionSerializationSurrogate = new QuaternionSerializationSurrogate();
            var vector2IntSerializationSurrogate = new Vector2IntSerializationSurrogate();
            var vector2SerializationSurrogate = new Vector2SerializationSurrogate();
            var vector3IntSerializationSurrogate = new Vector3IntSerializationSurrogate();
            var vector3SerializationSurrogate = new Vector3SerializationSurrogate();
            var vector4SerializationSurrogate = new Vector4SerializationSurrogate();
            
            surrogateSelector.AddSurrogate(typeof(Color), streamingContext, colorSerializationSurrogate);
            surrogateSelector.AddSurrogate(typeof(Quaternion), streamingContext, quaternionSerializationSurrogate);
            surrogateSelector.AddSurrogate(typeof(Vector2Int), streamingContext, vector2IntSerializationSurrogate);
            surrogateSelector.AddSurrogate(typeof(Vector2), streamingContext, vector2SerializationSurrogate);
            surrogateSelector.AddSurrogate(typeof(Vector3Int), streamingContext, vector3IntSerializationSurrogate);
            surrogateSelector.AddSurrogate(typeof(Vector3), streamingContext, vector3SerializationSurrogate);
            surrogateSelector.AddSurrogate(typeof(Vector4), streamingContext, vector4SerializationSurrogate);
        }
    }
}