using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UrUtils.Inspector.GenericDictionary
{
    /// <summary>
    /// A Generic Serializable Dictionary for Unity without any boilerplate - simply declare
    /// your field and key value types and you're good to go. Requires a Unity version with
    /// generic serialization support (Unity 2020.1.X and above).
    /// </summary>
    [Serializable]
    public class GenericDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<KeyValue> List = new List<KeyValue>();
        [SerializeField, HideInInspector]
        Dictionary<TKey, TValue> Dictionary = new Dictionary<TKey, TValue>();
        [FormerlySerializedAs("keyCollision")] [SerializeField, HideInInspector]
        bool KeyCollision;

        /// <summary>
        /// Serializable KeyValue struct used as items in the dictionary. This is needed
        /// since the KeyValuePair in System.Collections.Generic isn't serializable.
        /// </summary>
        [Serializable]
        struct KeyValue
        {
            public TKey Key;
            public TValue Value;
            public KeyValue(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        public TValue this[TKey key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public bool IsReadOnly { get; set; }

        public ICollection<TKey> Keys => Dictionary.Keys;

        public ICollection<TValue> Values => Dictionary.Values;

        public int Count => Dictionary.Count;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        // Serialize dictionary into list representation.
        public void OnBeforeSerialize()
        {
            foreach (var pair in Dictionary)
            {
                var kv = new KeyValue(pair.Key, pair.Value);
                if (!List.Contains(kv))
                {
                    List.Add(kv);
                }
            }
        }

        // Deserialize dictionary from list while checking for key-collisions.
        public void OnAfterDeserialize()
        {
            KeyCollision = false;
            Dictionary = new Dictionary<TKey, TValue>(List.Count);
            foreach (var pair in List)
            {
                if (pair.Key != null && !ContainsKey(pair.Key))
                {
                    Add(pair.Key, pair.Value);
                }
                else
                {
                    // Redundant, but removes unused reference warning.
                    if (!KeyCollision)
                    {
                        KeyCollision = true;
                    }
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Dictionary.Clear();
            List.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (!Dictionary.TryGetValue(item.Key, out var value))
                return false;

            return EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "The starting array index cannot be negative.");
            if (array.Length - arrayIndex < Dictionary.Count)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            foreach (var pair in Dictionary)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }

        public bool Remove(TKey key)
        {
            if (!Dictionary.Remove(key))
                return false;

            KeyValue item = new KeyValue();
            foreach (var element in List)
            {
                if (EqualityComparer<TKey>.Default.Equals(element.Key, key))
                {
                    item = element;
                    break;
                }
            }

            List.Remove(item);
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!Dictionary.TryGetValue(item.Key, out var value))
                return false;

            bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, item.Value);
            if (!valueMatch)
                return false;

            Dictionary.Remove(item.Key);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }
    }
}