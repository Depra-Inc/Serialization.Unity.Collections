// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record Vector4SerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var vector4 = (Vector4) obj;
			info.AddValue(nameof(Vector4.x), vector4.x);
			info.AddValue(nameof(Vector4.y), vector4.y);
			info.AddValue(nameof(Vector4.z), vector4.z);
			info.AddValue(nameof(Vector4.w), vector4.w);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var vector4 = (Vector4) obj;
			vector4.x = (float) info.GetValue(nameof(Vector4.x), typeof(float));
			vector4.y = (float) info.GetValue(nameof(Vector4.y), typeof(float));
			vector4.z = (float) info.GetValue(nameof(Vector4.z), typeof(float));
			vector4.w = (float) info.GetValue(nameof(Vector4.w), typeof(float));
			obj = vector4;

			return obj;
		}
	}
}