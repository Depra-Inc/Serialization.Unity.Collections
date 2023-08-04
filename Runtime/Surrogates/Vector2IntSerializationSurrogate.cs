// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record Vector2IntSerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var vector2Int = (Vector2Int) obj;
			info.AddValue(nameof(Vector2Int.x), vector2Int.x);
			info.AddValue(nameof(Vector2Int.y), vector2Int.y);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var vector2Int = (Vector2Int) obj;
			vector2Int.x = (int) info.GetValue(nameof(Vector2Int.x), typeof(int));
			vector2Int.y = (int) info.GetValue(nameof(Vector2Int.y), typeof(int));
			obj = vector2Int;

			return obj;
		}
	}
}