﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Common.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Further_Net8_Common.Caches
{
    public class MemoryCacheManager : IMemoryCache
    {
        private readonly IOptions<MemoryCacheOptions> _optionsAccessor;

        private HashSet<object> _keys = new HashSet<object>();

        private IMemoryCache _inner;

        public MemoryCacheManager(IOptions<MemoryCacheOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            _inner = new MemoryCache(_optionsAccessor);
        }

        public void Dispose() => _inner.Dispose();

        public ICacheEntry CreateEntry(object key)
        {
            _keys.Add(key);
            return _inner.CreateEntry(key);
        }

        public void Remove(object key) {
            _keys.Remove(key);
            _inner.Remove(key);
        }

        public bool TryGetValue(object key, out object value) => _inner.TryGetValue(key, out value);

        public void Reset()
        {
            lock (_optionsAccessor)
            {
                var old = _inner;
                _inner = new MemoryCache(_optionsAccessor);
                _keys.Clear();
                old.Dispose();
            }
        }

        public IEnumerable<string?> GetAllKeys()
        {
            foreach (var key in _keys)
            {
                yield return key?.ToString();
            }
        }
    }
}
