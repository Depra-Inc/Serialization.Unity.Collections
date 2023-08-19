// Copyright © 2022-2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using Depra.Serialization.Collections.Runtime;
using UnityEngine;

namespace Depra.Serialization.Collections.Samples
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    internal sealed class SerializedDictionaryReview : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, string> _serializedDictionary;
    }
}
