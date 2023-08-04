// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record Vector3IntSerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var vector3Int = (Vector3Int) obj;
			info.AddValue(nameof(Vector3Int.x), vector3Int.x);
			info.AddValue(nameof(Vector3Int.y), vector3Int.y);
			info.AddValue(nameof(Vector3Int.z), vector3Int.z);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var vector3Int = (Vector3Int) obj;
			vector3Int.x = (int) info.GetValue(nameof(Vector3Int.x), typeof(int));
			vector3Int.y = (int) info.GetValue(nameof(Vector3Int.y), typeof(int));
			vector3Int.z = (int) info.GetValue(nameof(Vector3Int.z), typeof(int));
			obj = vector3Int;

			return obj;
		}
	}
}