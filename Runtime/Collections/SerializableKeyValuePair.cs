// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using UnityEngine;

namespace Depra.Serialization.Unity.Runtime.Collections
{
    // ReSharper disable once InvalidXmlDocComment
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
        [field: SerializeField] public TKey Key { get; private set; }
        [field: SerializeField] public TValue Value { get; private set; }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}