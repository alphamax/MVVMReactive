using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.Core.Reactive
{
    public class ImmutableDictionary<Key, Value> : IEnumerable<KeyValuePair<Key, Value>>
    {
        #region Private containers

        private Dictionary<Key, Value> Container { get; }

        #endregion Private containers

        #region Ctor

        public ImmutableDictionary()
        {
            Container = new Dictionary<Key, Value>();
        }

        private ImmutableDictionary(Dictionary<Key, Value> underlyingContainer)
        {
            Container = underlyingContainer;
        }

        #endregion Ctor

        #region Public proxy

        public Value this[Key key] { get => Container[key]; }

        public ICollection<Key> Keys => Container.Keys;

        public ICollection<Value> Values => Container.Values;

        public int Count => Container.Count;

        public bool IsReadOnly => true;

        public bool Contains(KeyValuePair<Key, Value> item) => Container.Contains(item);

        public bool ContainsKey(Key key) => Container.ContainsKey(key);

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator() => Container.GetEnumerator();

        public bool TryGetValue(Key key, out Value value) => Container.TryGetValue(key, out value);

        #endregion Public proxy

        #region Public Immutable version

        public ImmutableDictionary<Key, Value> Set(Key key, Value value)
        {
            var clone = Container.ToDictionary(entry => entry.Key, entry => entry.Value);
            clone[key] = value;
            return new ImmutableDictionary<Key, Value>(clone);
        }

        public ImmutableDictionary<Key, Value> Clear()
        {
            return new ImmutableDictionary<Key, Value>(new Dictionary<Key, Value>());
        }

        public ImmutableDictionary<Key, Value> Remove(Key key)
        {
            var clone = Container.ToDictionary(entry => entry.Key, entry => entry.Value);
            clone.Remove(key);
            return new ImmutableDictionary<Key, Value>(clone);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Container.GetEnumerator();
        }

        #endregion Public Immutable version
    }
}