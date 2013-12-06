// -----------------------------------------------------------------------
// <copyright file="ServiceWrapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServiceWrapper
    {
        private static Fanex.Affiliate.Core.MemberSiteService.ServiceSoapClient _client = null;

        private static Fanex.Affiliate.Core.MemberSiteService.ServiceSoapClient Client
        {
            get
            {
                if (_client == null)
                    _client = new Fanex.Affiliate.Core.MemberSiteService.ServiceSoapClient("ServiceSoap");                
                
                return _client;
            }
        }

        public static XElement GetRegList(string siteName ,DateTime targetDate)
        {
            return Client.GetRegList(siteName, targetDate); 
        }

        public static XElement UpdateBtage(string siteName, string licenseeUserName, string newBtag)
        {
            return Client.UpdateBtage(siteName, licenseeUserName, newBtag);
        }

        public static XElement GetLicenseeCustInfo(string siteName, string licenseeUserName)
        {
            return Client.GetLicenseeCustInfo(siteName, licenseeUserName);
        }
        public static XElement Transfer(string siteName, string licenseeUserName, double amount, int dummyAccount)
        {
            return Client.Transfer(siteName, licenseeUserName,amount,dummyAccount);
        }

    }
}
