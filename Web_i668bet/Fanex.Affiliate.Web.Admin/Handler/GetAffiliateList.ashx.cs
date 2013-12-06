namespace Fanex.Web.i668bet.Admin.Handler
{
    using System;
    using System.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using System.Text;
    public class GetAffiliateList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {

                context.Response.ContentType = "text/plain";
                string[] param = context.Request.Params["params"].Split(";".ToCharArray());

                string username = string.Empty, email = string.Empty, status = string.Empty;
                int curpage = 0, nummem = 0, custid = -1;
                StringBuilder result = ValidateData(param, ref username, ref email, ref custid, ref status, ref curpage);
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    //result.Append(AffAdmin.GenerateHTML.AffiliateList.GetAffList(username,email,custid,status,curpage,AppSettings.PageSize,out nummem));
                    result.Append("$$$");
                    result.Append(nummem.ToString());
                }
                context.Response.Write(result.ToString());


            }

        }

        private StringBuilder ValidateData(string[] param, ref string username, ref string email, ref int custid, ref string status, ref int curPage)
        {

            StringBuilder err = new StringBuilder("");

            try
            {
                if (param.Length == 5)
                {
                    username = param[0].Trim();
                    email = param[1].Trim();
                    string t = param[2].Trim();
                    status = param[3].Trim();
                    curPage = CastAide.AsInt32(param[4]);
                    if (username.Equals("All", StringComparison.InvariantCultureIgnoreCase))
                    {
                        username = string.Empty;
                    }
                    if (email.Equals("All", StringComparison.InvariantCultureIgnoreCase))
                    {
                        email = string.Empty;
                    }
                    if (t.Equals("All", StringComparison.InvariantCultureIgnoreCase) || t == string.Empty)
                    {
                        custid = -1;
                    }
                    else
                    {
                        try
                        {
                            custid = Convert.ToInt32(t);
                        }
                        catch
                        {
                            custid = -2;
                        }
                    }


                }

            }
            catch (Exception e)
            {
                return err.Append(e.Message);

            }
            return err;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}