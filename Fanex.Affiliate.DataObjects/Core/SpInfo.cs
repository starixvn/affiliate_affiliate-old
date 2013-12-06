// -----------------------------------------------------------------------
// <copyright file="SpInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpInfo
    {
        public SpInfo(string key, string spName, string dbName, int timeout)
        {
            this.Key = key;
            this.SpName = spName;
            this.DbName = dbName;
            this.TimeOut = timeout;
        }
        public string Key { get; private set; }
        public string SpName { get; private set; }
        public string DbName { get; private set; }
        public int TimeOut { get; private set; }
    }
}
