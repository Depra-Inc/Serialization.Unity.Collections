// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depra.Serialization.Runtime.Collections
{
    // ReSharper disable once InvalidXmlDocComment
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
        [field: SerializeField] public TKey Key { get; private set; }
        [field: SerializeField] public List<TValue> Values { get; private set; }

        public SerializableKeyValuePairBoxed(TKey key, List<TValue> values)
        {
            Key = key;
            Values = values;
        }

        public SerializableKeyValuePairBoxed(TKey key, TValue value)
        {
            Key = key;
            Values = new List<TValue> {value};
        }

        public TValue Value => Values[0];
    }
}