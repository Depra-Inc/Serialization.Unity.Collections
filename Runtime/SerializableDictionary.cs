// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Depra.Serialization.Collections.Runtime
{
	/// <summary>
	/// This <see cref="SerializableDictionary{TKey,TValue}"/> works like a
	/// regular <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
	/// It does not require a new class for every data type.
	/// </summary>
	/// <typeparam name="TKey">This is the Key value. It must be unique.</typeparam>
	/// <typeparam name="TValue">This is the Value associated with the Key.</typeparam>
	/// <remarks>
	/// It can be viewed within the Unity Inspector Window (Editor).
	/// It is JSON serializable (Tested with Newtonsoft.Json).
	/// </remarks>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> :
		Dictionary<TKey, SerializableKeyValuePair<TKey, TValue>>,
		ISerializationCallbackReceiver
	{
		[SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> _keys;

		public SerializableDictionary() => _keys = new List<SerializableKeyValuePair<TKey, TValue>>();

		private SerializableDictionary(Dictionary<TKey, TValue> dictionary)
		{
			_keys = new List<SerializableKeyValuePair<TKey, TValue>>();
			foreach (var (key, value) in dictionary)
			{
				Add(key, value);
			}
		}

		/// <summary>
		/// Adds directly to the <seealso cref="SerializableDictionary{TKey,TValue}"/> without needing to
		/// manually encapsulate the value in a <seealso cref="SerializableKeyValuePair{K,V}"/> container.
		/// </summary>
		/// <param name="key">The key to add.</param>
		/// <param name="value">The value to add.</param>
		public void Add(TKey key, TValue value) =>
			base.Add(key, new SerializableKeyValuePair<TKey, TValue>(key, value));

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			// This protects us from having entries constantly added.
			if (Count <= _keys.Count)
			{
				return;
			}

			foreach (var (_, value) in this)
			{
				_keys.Add(value);
			}
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Clear();
			foreach (var keyValuePair in from keyValuePair in _keys
			         where keyValuePair != null
			         where keyValuePair.Key != null
			         where keyValuePair.Value != null
			         where ContainsKey(keyValuePair.Key) == false
			         select keyValuePair)
			{
				base.Add(keyValuePair.Key, keyValuePair);
			}
		}
	}
}