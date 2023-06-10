// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Depra.Serialization.Unity.Runtime.Collections
{
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

        /// <summary>
        /// Adds directly to the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> without needing to
        /// manually encapsulate the value in a <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> container.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        public void AddDirect(TKey key, TValue value) =>
            Add(key, new SerializableKeyValuePairBoxed<TKey, TValue>(key, value));

        /// <summary>
        /// Adds directly to the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> without needing to
        /// manually encapsulate the value in a <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> container.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        public void AddDirect(TKey key, List<TValue> value) =>
            Add(key, new SerializableKeyValuePairBoxed<TKey, TValue>(key, value));

        /// <summary>
        /// Attempts to retrieve an index 0 direct value from a key, passing out the ref to the value provided.
        /// </summary>
        /// <param name="key">The key to search in the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> with.</param>
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
        /// Attempts to retrieve an index 0 direct value from a key.
        /// </summary>
        /// <param name="key">The key to search in the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> with.</param>
        /// <returns>If the key doesn't exist or the value is null a NullReferenceException will be raised.</returns>
        public TValue GetValue(TKey key)
        {
            TryGetValue(key, out var defaultValue);

            return defaultValue!.Value;
        }

        /// <summary>
        /// Attempts to retrieve a boxed value from a key, passing out the ref to the value provided.
        /// </summary>
        /// <param name="key">The key to search in the <see cref="SerializableDictionaryBoxed{TKey,TValue}"/> with.</param>
        /// <param name="values">The desired value to retrieve. If it returns false, the value returned will be null.</param>
        /// <returns>The bool will return true if it successfully retrieves the value from the key provided.</returns>
        public bool TryGetValues(TKey key, ref List<TValue> values)
        {
            var canGet = TryGetValue(key, out var defaultValue);
            if (defaultValue != null)
            {
                values = defaultValue.Values;
            }

            return canGet;
        }

        /// <summary>
        /// Attempts to retrieve a boxed value from a key.
        /// </summary>
        /// <param name="key">The key to search in the <seealso cref="SerializableDictionaryBoxed{TKey,TValue}"/> with.</param>
        /// <returns>If the key doesn't exist or the value is null a NullReferenceException will be raised.</returns>
        public List<TValue> GetValues(TKey key)
        {
            TryGetValue(key, out var defaultValue);

            return defaultValue!.Values;
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
            foreach (var keyValuePairBoxed in from keyValuePairBoxed in _keys
                     where keyValuePairBoxed != null
                     where keyValuePairBoxed.Key != null
                     where keyValuePairBoxed.Values != null
                     where !ContainsKey(keyValuePairBoxed.Key)
                     select keyValuePairBoxed)
            {
                Add(keyValuePairBoxed.Key, keyValuePairBoxed);

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