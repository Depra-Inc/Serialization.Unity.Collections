// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record Vector2SerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var vector2 = (Vector2) obj;
			info.AddValue(nameof(Vector2.x), vector2.x);
			info.AddValue(nameof(Vector2.y), vector2.y);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var vector2 = (Vector2) obj;
			vector2.x = (float) info.GetValue(nameof(Vector2.x), typeof(float));
			vector2.y = (float) info.GetValue(nameof(Vector2.y), typeof(float));
			obj = vector2;

			return obj;
		}
	}
}