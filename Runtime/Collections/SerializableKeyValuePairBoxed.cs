// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depra.Serialization.Unity.Runtime.Collections
{
    /// <summary>
    /// This <see cref="SerializableKeyValuePairBoxed{TKey,TValue}"/> stores the Key and the Value(s)
    /// for the <see cref="SerializableDictionaryBoxed{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">This is the Key value. It must be unique.</typeparam>
    /// <typeparam name="TValue">This is the Value associated with the Key.</typeparam>
    /// <remarks>
    /// This alternate version boxes the values into a <see cref="System.Collections.Generic.List{T}"/>
    /// so we can bind the data to the List and effectively serialize types like
    /// (see: <see cref="UnityEngine.Vector3"/>).
    /// It is JSON serializable (Tested with <see cref="Newtonsoft.Json"/>).
    /// </remarks>
    [Serializable]
    public class SerializableKeyValuePairBoxed<TKey, TValue> : List<TValue>
    {
        [SerializeField] private TKey _key;
        [SerializeField] private List<TValue> _values;

        public SerializableKeyValuePairBoxed(TKey key, List<TValue> values)
        {
            _key = key;
            _values = values;
        }

        public SerializableKeyValuePairBoxed(TKey key, TValue value)
        {
            _key = key;
            _values = new List<TValue> {value};
        }

        public TKey Key => _key;
        public TValue Value => _values[0];
        public List<TValue> Values => _values;
    }
}