// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record QuaternionSerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var quaternion = (Quaternion) obj;
			info.AddValue(nameof(Quaternion.x), quaternion.x);
			info.AddValue(nameof(Quaternion.y), quaternion.y);
			info.AddValue(nameof(Quaternion.z), quaternion.z);
			info.AddValue(nameof(Quaternion.w), quaternion.w);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var quaternion = (Quaternion) obj;
			quaternion.x = (float) info.GetValue(nameof(Quaternion.x), typeof(float));
			quaternion.y = (float) info.GetValue(nameof(Quaternion.y), typeof(float));
			quaternion.z = (float) info.GetValue(nameof(Quaternion.z), typeof(float));
			quaternion.w = (float) info.GetValue(nameof(Quaternion.w), typeof(float));
			obj = quaternion;

			return obj;
		}
	}
}