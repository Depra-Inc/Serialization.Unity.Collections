// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using UnityEngine;

namespace Depra.Serialization.Unity.Runtime.Collections
{
    /// <summary>
    /// This <see cref="SerializableKeyValuePair{TKey,TValue}"/> stores the Key and the Value(s)
    /// for the <see cref="SerializableDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">This is the Key value. It must be unique.</typeparam>
    /// <typeparam name="TValue">This is the Value associated with the Key.</typeparam>
    /// <remarks>
    /// It is JSON serializable (Tested with <see cref="Newtonsoft.Json"/>).
    /// </remarks>
    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey _key;
        [SerializeField] private TValue _value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public TKey Key => _key;
        public TValue Value => _value;
    }
}