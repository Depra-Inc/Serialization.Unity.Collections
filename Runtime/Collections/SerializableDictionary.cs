// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Depra.Serialization.Unity.Runtime.Collections
{
    /// <summary>
    /// This <see cref="SerializableDictionary{TKey,TValue}"/> works like a regular <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
    /// It does not require a new class for every data type.
    /// </summary>
    /// <typeparam name="TKey">This is the Key value. It must be unique.</typeparam>
    /// <typeparam name="TValue">This is the Value associated with the Key.</typeparam>
    /// <remarks> 
    /// It can be viewed within the Unity Inspector Window (Editor).
    /// It is JSON serializable (Tested with <see cref="Newtonsoft.Json"/>).
    /// </remarks>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, SerializableKeyValuePair<TKey, TValue>>,
        ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> _keys;

        public static implicit operator SerializableDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            SerializableDictionary<TKey, TValue> serializableDict = dictionary;
            return serializableDict;
        }

        public SerializableDictionary() => _keys = new List<SerializableKeyValuePair<TKey, TValue>>();

        /// <summary>
        /// Adds directly to the <seealso cref="SerializableDictionary{TKey,TValue}"/> without needing to manually encapsulate the value in a <seealso cref="SerializableKeyValuePair{K,V}"/> container.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        public void AddDirect(TKey key, TValue value) =>
            Add(key, new SerializableKeyValuePair<TKey, TValue>(key, value));

        /// <summary>
        /// Attempts to retrieve a value from a key, passing out the ref to the value provided.
        /// </summary>
        /// <param name="key">The key to search in the <seealso cref="SerializableDictionary{TKey,TValue}"/> with.</param>
        /// <param name="value">The desired value to retrieve. If it returns false, the value returned will be null.</param>
        /// <returns>The bool will return true if it successfully retrieves the value from the key provided.</returns>
        public bool TryGetValue(TKey key, ref TValue value)
        {
            var canGet = TryGetValue(key, out var defaultValue);
            if (defaultValue != null)
            {
                value = defaultValue.Value;
            }

            return canGet;
        }

        /// <summary>
        /// Attempts to retrieve a value from a key.
        /// </summary>
        /// <param name="key">The key to search in the <seealso cref="SerializableDictionary{TKey,TValue}"/> with.</param>
        /// <returns>If the key doesn't exist or the value is null a NullReferenceException will be raised.</returns>
        public TValue GetValue(TKey key)
        {
            TryGetValue(key, out var defaultValue);

            return defaultValue.Value;
        }

        public void OnBeforeSerialize()
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

        public void OnAfterDeserialize()
        {
            Clear();
            foreach (var keyValuePair in from keyValuePair in _keys
                     where keyValuePair != null
                     where keyValuePair.Key != null
                     where keyValuePair.Value != null
                     where ContainsKey(keyValuePair.Key) == false
                     select keyValuePair)
            {
                Add(keyValuePair.Key, keyValuePair);
            }
        }
    }
}