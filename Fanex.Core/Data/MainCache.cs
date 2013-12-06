// -----------------------------------------------------------------------
// <copyright file="MainCache.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Caching;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MainCache
    {
        private static CacheManager _cache = CacheFactory.GetCacheManager();
        public static object GetData(string key)
        {
            return _cache.GetData(key);
        }
        public static void SetData(string key, object data)
        {
            _cache.Add(key, data);
        }


    }
}
