// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record Vector3SerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var vector3 = (Vector3) obj;
			info.AddValue(nameof(Vector3.x), vector3.x);
			info.AddValue(nameof(Vector3.y), vector3.y);
			info.AddValue(nameof(Vector3.z), vector3.z);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var vector3 = (Vector3) obj;
			vector3.x = (float) info.GetValue(nameof(Vector3.x), typeof(float));
			vector3.y = (float) info.GetValue(nameof(Vector3.y), typeof(float));
			vector3.z = (float) info.GetValue(nameof(Vector3.z), typeof(float));
			obj = vector3;

			return obj;
		}
	}
}