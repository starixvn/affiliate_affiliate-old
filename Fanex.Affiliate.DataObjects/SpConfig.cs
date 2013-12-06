// -----------------------------------------------------------------------
// <copyright file="SpConfig.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Fanex.Core.Data;
using System.Xml.Linq;
    using System.Web;
    using System.Configuration;
    using System.IO;
    using DNAide.Core;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpConfig
    {
        private static IEnumerable<XElement> _spList = null;

        /// <summary>
        /// get resource path point to external config data source
        /// </summary>
        /// <param name="config">NameValueCollection config</param>
        /// <returns>return absolute resource path</returns>
        private static string GetConfigPath()
        {
            var resourcePath = ConfigurationManager.AppSettings["SpConfigPath"];
            if (string.IsNullOrWhiteSpace(resourcePath))
            {
                throw new NullReferenceException("SpName path");
            }

            // check root path http://msdn.microsoft.com/query/dev10.query?appId=Dev10IDEF1&l=EN-US&k=k%28SYSTEM.IO.PATH.ISPATHROOTED%29;k%28SOLUTIONITEMSPROJECT%29;k%28TargetFrameworkMoniker-%22.NETFRAMEWORK%2cVERSION%3dV4.0%22%29;k%28DevLang-CSHARP%29&rd=true
            if (!Path.IsPathRooted(resourcePath))
            {
                if (resourcePath.StartsWith("~"))
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        resourcePath = System.Web.HttpContext.Current.Server.MapPath(resourcePath);
                    }
                }
                else
                {
                    // if got the problem with window service
                    // see http://haacked.com/archive/2004/06/29/706.aspx
                    resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourcePath);
                }
            }

            return resourcePath;
        }

        public static IEnumerable<XElement> SpList
        {
            get
            {
                if (_spList == null)
                {
                    _spList = MainCache.GetData("SpList") as IEnumerable<XElement>;

                    if (_spList == null)
                    {
                       
                       var path = GetConfigPath();
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
