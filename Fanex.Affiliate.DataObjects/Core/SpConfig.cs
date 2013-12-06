// -----------------------------------------------------------------------
// <copyright file="SpConfig.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Fanex.Core.Data;
    using System.Web;
    using System.IO;
    using System.Configuration;
    using DNAide.Core;
    using System.Data;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpConfig
    {
        private static IEnumerable<XElement> _spList = null;

        public static IEnumerable<XElement> SpList
        {
            get
            {
                if (_spList == null)
                {
                    _spList = MainCache.GetData("SpList") as IEnumerable<XElement>;

                    if (_spList == null)
                    {
                        var path = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["SpConfigPath"]);
                        if (File.Exists(path))
                        {
                            _spList = XDocument.Load(path).Element("Functions").Descendants("Setting");
                            MainCache.SetData("SpList", _spList);
                        }
                    }
                }
                return _spList;
            }
        }

        public static SpInfo GetSpInfo(string key)
        {

            var xe = SpConfig.SpList
                    .Where(x => x.Attribute("Key").Value.Equals(key, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();
            if (xe == null)
            {
                throw new Exception(String.Format("Key '{0}' doesn't exist in '{1}'.", key, ConfigurationManager.AppSettings["SpConfigPath"]));
            }
            else
            {
                var timeout = xe.Attribute("TimeOut");

                var dbName = DBType.GetDbName(CastAide.AsString(xe.Attribute("DbName").Value, String.Empty));

                var sp = new SpInfo(key,
                        CastAide.AsString(xe.Attribute("SpName").Value, String.Empty),
                        dbName,
                        timeout == null ? 0 : CastAide.AsInt32(timeout.Value, 200));
                return sp;
            }
           // return null;
        }
    }
}
