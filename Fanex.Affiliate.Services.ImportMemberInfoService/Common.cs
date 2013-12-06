// -----------------------------------------------------------------------
// <copyright file="Common.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Services
{
    using System;
    using System.Configuration;
    using System.Xml;
    using DNAide.Core;
    using Fanex.MailDeliver.MailClient.API;
    using Fanex.MailDeliver.Model;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;
    using System.Globalization;
    using System.Collections.Generic;
    using DNAide.Web;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Common
    {
        private static char CSV_SEPARATOR = '\t';
        private static string _systemName = null;

        public static int Interval
        {
            get { return CastAide.AsInt32(ConfigurationManager.AppSettings["interval"]); }
        }

        public static int DueTime
        {
            get { return CastAide.AsInt32(ConfigurationManager.AppSettings["DueTime"]); }
        }

        public static DateTime? FromDate
        {
            get
            {
                var s = ConfigurationManager.AppSettings["fromDate"];
                if (String.IsNullOrWhiteSpace(s))
                    return null;
                return CastAide.AsDateTime(s, null);
            }
        }
        public static DateTime? ToDate
        {
            get
            {
                var s = ConfigurationManager.AppSettings["toDate"];
                if (String.IsNullOrWhiteSpace(s))
                    return null;
                return CastAide.AsDateTime(s, null);
            }
        }

        public static string IsSendMail
        {
            get { return ConfigurationManager.AppSettings["isSendMail"]; }
        }
        public static string FromMail
        {
            get { return ConfigurationManager.AppSettings["fromMail"]; }
        }
        public static string ToMail
        {
            get { return ConfigurationManager.AppSettings["toMail"]; }
        }
        public static string CCMail
        {
            get { return ConfigurationManager.AppSettings["CCMail"]; }
        }
        public static string BCCMail
        {
            get { return ConfigurationManager.AppSettings["BCCMail"]; }
        }

        public static string SubjectMailError
        {
            get { return ConfigurationManager.AppSettings["Environment"] + "-" +  ConfigurationManager.AppSettings["subjectMailError"]; }
        }

        public static int AdminID
        {
            get { return CastAide.AsInt32(ConfigurationManager.AppSettings["adminID"]); }
        }

        public static XmlDocument GetFirstDepositDateData(IEnumerable<string> usernames)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);

            DNAideLog.Write("Get Deposit infor: " + string.Join(",", usernames.ToArray()));
            foreach (var item in usernames)
            {
                var x = ServiceWrapper.GetLicenseeCustInfo(AppSettings.SystemName, item);                
                
                if (x.Element("ReturnCode").Value == "0")
                {
                    try
                    {
                        DNAideLog.Write("Username: " + item
                                + "\r\nReturnCode: " + x.Element("ReturnCode").Value
                                + "\r\nIBCusername: " + x.Element("IBCusername").Value
                                + "\r\nbtag: " + x.Element("btag").Value
                                + "\r\nFirstDepositDate: " + x.Element("FirstDepositDate").Value
                                + "\r\nRegisterDate: " + x.Element("RegisterDate").Value
                                + "\r\nCurrency: " + x.Element("Currency").Value
                                );

                        var date = Common.GetFirstDepositDate(x.Element("ReturnCode").Value, x.Element("FirstDepositDate").Value);
                        if (date != null)
                        {
                            XmlElement transNode = doc.CreateElement("trans");
                            transNode.SetAttribute("username", item);
                            transNode.SetAttribute("ibcusername", x.Element("IBCusername").Value);
                            transNode.SetAttribute("date", date.ToString());

                            root.AppendChild(transNode);
                        }
                    }
                    catch (Exception ex)
                    {
                        DNAideLog.Write("username: " + item +
                            "\r\nXml element" + x.ToString() +
                            "\r\n" + ex.Message +
                            "\r\nSOURCE: " + ex.Source +
                            "\r\nTARGETSITE: " + ex.TargetSite +
                            "\r\nSTACKTRACE: " + ex.StackTrace);
                    }
                }
            }
            return doc;
        }

        private static DateTime? GetFirstDepositDate(string returnCode, string firstDepositDate)
        {
            if (returnCode == "0")
            {
                DateTime date = DateTime.MinValue;
                if (DateTime.TryParseExact(firstDepositDate, "d", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    return date;
            }
            return null;
        }
        public static XmlDocument GetData(DateTime date)
        {
            var xml = ServiceWrapper.GetRegList(AppSettings.SystemName, date);
            if (xml == null || xml.IsEmpty)
                return null;
            var returnCode = xml.Element("ReturnCode").Value;
            var count = int.Parse(xml.Element("Count").Value);
            var list = xml.Elements("Registration");


            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);

            foreach (var item in list)
            {
                XmlElement transNode = doc.CreateElement("trans");
                foreach (var node in item.Elements())
                {
                    var name = node.Name.ToString().ToLower();
                    transNode.SetAttribute(name, node.Value);
                    if (name=="btag")
                    {                        
                        var array = node.Value.Split('_');
                        var licensee = array[0];
                        var affid = array.Length > 1 ? array[1] : String.Empty;
                        var clickid = array.Length > 2 ? array[2] : String.Empty;
                        transNode.SetAttribute("affid", affid);
                        //transNode.SetAttribute("affid", affid);
                        transNode.SetAttribute("clickid", clickid);
                    }
                }
                root.AppendChild(transNode);     
            }
            DNAideLog.Write("Register list\r\n Return Code: " + returnCode + "\r\nCount " + count + " \r\nList " + doc.OuterXml);
            return doc;
        }

    }
}
