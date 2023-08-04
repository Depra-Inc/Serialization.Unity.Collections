// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.Serialization;
using UnityEngine;

namespace Depra.Serialization.Runtime.Surrogates
{
	internal sealed record ColorSerializationSurrogate : ISerializationSurrogate
	{
		void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var color = (Color) obj;
			info.AddValue(nameof(Color.r), color.r);
			info.AddValue(nameof(Color.g), color.g);
			info.AddValue(nameof(Color.b), color.b);
			info.AddValue(nameof(Color.a), color.a);
		}

		object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo info, StreamingContext context,
			ISurrogateSelector selector)
		{
			var color = (Color) obj;
			color.r = (float) info.GetValue(nameof(Color.r), typeof(float));
			color.g = (float) info.GetValue(nameof(Color.g), typeof(float));
			color.b = (float) info.GetValue(nameof(Color.b), typeof(float));
			color.a = (float) info.GetValue(nameof(Color.a), typeof(float));
			obj = color;

			return obj;
		}
	}
}