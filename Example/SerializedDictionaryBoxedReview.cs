using System.Diagnostics.CodeAnalysis;
using Depra.Serialization.Unity.Runtime.Collections;
using UnityEngine;

namespace Depra.Serialization.Unity.Example
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    internal sealed class SerializedDictionaryBoxedReview : MonoBehaviour
    {
        [SerializeField] private SerializableDictionaryBoxed<string, string> _serializedDictionary;
    }
}