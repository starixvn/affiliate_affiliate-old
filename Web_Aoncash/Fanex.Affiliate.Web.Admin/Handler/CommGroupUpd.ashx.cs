namespace Fanex.Web.Aoncash.Admin.Handler
{
    using System;
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Web.SessionState;
    using System.IO;
    using System.Text;
    using System.Runtime.Serialization.Json;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Model;

    public class CommGroupUpd : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string groupSetting = context.Request.Params["params"];
                GroupSettingObj obj = GetInstanceFromJSON(groupSetting);
                obj.groupName = HttpUtility.HtmlDecode(obj.groupName);
                obj.description = HttpUtility.HtmlDecode(obj.description);

                DbResult result;
                try
                {
                    var currency = CastAide.AsInt32( context.Request["currency"],0);
                    var qm = decimal.Parse(context.Request["qualifiedAmount"]);
                    if (obj.isEdited)
                    {
                        result = CommGroup.CommGroupUpd(obj, context.Request.UserHostAddress, currency, qm, Fanex.Affiliate.Core.AppSession.Language);
                    }
                    else
                    {
                        if (obj.isApproved)
                        {
                            obj.password = DNAide.Utils.Encryption.RandomString(6);
                            result = CommGroup.CommGroupIns(obj, context.Request.UserHostAddress, currency, qm, Fanex.Affiliate.Core.AppSession.Language);
                            if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                            {
                                var MailOption = CastAide.AsBoolean(result.Outputs["mailoption"], true);//Send relevant email format
                                var affType = CastAide.AsInt32(result.Outputs["afftype"], 1);
                                AdminCommon.SendEmailApproved(obj.id, obj.language, obj.fName, obj.affName, obj.email, obj.password, AppCommon.EmailSuffix(MailOption), affType);
                            }

                        }
                        else
                            result = CommGroup.CommGroupIns(obj, context.Request.UserHostAddress, currency, qm, Fanex.Affiliate.Core.AppSession.Language);

                    }
                    context.Response.Write(result.Outputs["errmsg"]);

                }
                catch (Exception e)
                {
                    context.Response.Write(e.Message);
                    Fanex.Affiliate.Core.AppCommon.SendEmail(e);
#if DEBUG
                    throw;

#else
                DNAide.Web.DNAideLog.Write(e.Message);
#endif

                }


            }

        }
        public GroupSettingObj GetInstanceFromJSON(string json)
        {

            using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GroupSettingObj));

                GroupSettingObj obj = (GroupSettingObj)serializer.ReadObject(memoryStream);

                memoryStream.Close();

                return (GroupSettingObj)obj;

            }

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
