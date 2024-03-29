// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Depra.Serialization.Collections.Runtime
{
	// ReSharper disable once InvalidXmlDocComment
	/// <summary>
	/// This <see cref="SerializableDictionaryBoxed{TKey,TValue}"/> works like a
	/// regular <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
	/// It does not require a new class for every data type.
	/// </summary>
	/// <typeparam name="TKey">This is the Key value. It must be unique.</typeparam>
	/// <typeparam name="TValue">This is the Value associated with the Key.</typeparam>
	/// <remarks>
	/// This alternate version boxes the values into a <see cref="System.Collections.Generic.List{T}"/>
	/// in the internal referenced custom class <see cref="SerializableDictionaryBoxed{TKey,TValue}"/>
	/// so we can bind the data to the List and effectively serialize types like
	/// (see: <see cref="UnityEngine.Vector3"/>).
	/// It can be viewed within the Unity Inspector Window (Editor).
	/// It is JSON serializable (Tested with <see cref="Newtonsoft.Json"/>).
	/// </remarks>
	[Serializable]
	public class SerializableDictionaryBoxed<TKey, TValue> :
		Dictionary<TKey, SerializableKeyValuePairBoxed<TKey, TValue>>,
		ISerializationCallbackReceiver
	{
		[SerializeField] private List<SerializableKeyValuePairBoxed<TKey, TValue>> _keys;

		public SerializableDictionaryBoxed() =>
			_keys = new List<SerializableKeyValuePairBoxed<TKey, TValue>>();

		private SerializableDictionaryBoxed(Dictionary<TKey, TValue> dictionary)
		{
			_keys = new List<SerializableKeyValuePairBoxed<TKey, TValue>>();
			foreach (var (key, value) in dictionary)
			{
				Add(key, value);
			}
		}

		/// <summary>
		/// Adds directly to the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> without needing to
		/// manually encapsulate the value in a <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> container.
		/// </summary>
		/// <param name="key">The key to add.</param>
		/// <param name="value">The value to add.</param>
		private void Add(TKey key, TValue value) =>
			base.Add(key, new SerializableKeyValuePairBoxed<TKey, TValue>(key, value));

		/// <summary>
		/// Adds directly to the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> without needing to
		/// manually encapsulate the value in a <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> container.
		/// </summary>
		/// <param name="key">The key to add.</param>
		/// <param name="value">The value to add.</param>
		public void Add(TKey key, List<TValue> value) =>
			base.Add(key, new SerializableKeyValuePairBoxed<TKey, TValue>(key, value));

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			// This protects us from having entries constantly added.
			if (Count <= _keys.Count)
			{
				return;
			}

			foreach (var kvp in this)
			{
				_keys.Add(kvp.Value);
			}
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Clear();
			foreach (var keyValuePairBoxed in from keyValuePairBoxed in _keys
			         where keyValuePairBoxed != null
			         where keyValuePairBoxed.Key != null
			         where keyValuePairBoxed.Values != null
			         where ContainsKey(keyValuePairBoxed.Key) == false
			         select keyValuePairBoxed)
			{
				base.Add(keyValuePairBoxed.Key, keyValuePairBoxed);

				// We set the range of the class' inherited List
				// and then we copy over the contents, making sure
				// that the internal List is converted first so it
				// can use the CopyTo function to bind the data.
				keyValuePairBoxed.AddRange(keyValuePairBoxed.Values);
				keyValuePairBoxed.Values.CopyTo(keyValuePairBoxed.ToArray());
			}
		}
	}
}