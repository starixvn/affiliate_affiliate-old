using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.DataObjects;
using System.Configuration;

namespace Fanex.Web.LiveBet365.AffLink.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Link(int AffId, string PageId, string Lang)
        {
            string affDomain = Request.Url.ToString();
            string redirectPage = ConfigurationManager.AppSettings["DefaultURL"];
            string urlReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : null;
            string domainReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.Host : null; ;

            if (string.IsNullOrEmpty(urlReferrer))
            {
                if (!string.IsNullOrEmpty(Request["ref"]))
                {
                    urlReferrer = Request["ref"];
                    Uri referrerURI = new Uri(urlReferrer);
                    domainReferrer = string.IsNullOrEmpty(referrerURI.Host) ? null : referrerURI.Host;
                }
            }

            if (TrackingSystem.InsertStatistic(AffId, PageId, Lang, affDomain, Request, urlReferrer, domainReferrer, out redirectPage))
            {
                Response.Redirect(redirectPage);
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["DefaultURL"]);
            }

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

    }
}
