using System;
using System.Globalization;
using DNAide.Web;
using DNAide.Core;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;
/// <summary>
/// Class to display Hot news content to customer
/// written by: Tong Hoang Dung on 19/03/2009
/// </summary>
public partial class Message : BasePageLogIn
{
    public string hotNewsContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        hotNewsContent = TemplateHandler.LoadTemplate("UserInfo/HotNews.htm");
        TemplateHandler.Assign(ref hotNewsContent, "MessageHistory", Translator.Get("MessageHistory"));
        TemplateHandler.Assign(ref hotNewsContent, "Search", Translator.Get("Search"));
        TemplateHandler.Assign(ref hotNewsContent, "All", Translator.Get("All"));
        TemplateHandler.Assign(ref hotNewsContent, "Normal", Translator.Get("Normal"));
        TemplateHandler.Assign(ref hotNewsContent, "Important", Translator.Get("Important"));
        TemplateHandler.Assign(ref hotNewsContent, "From", Translator.Get("From"));
        TemplateHandler.Assign(ref hotNewsContent, "To", Translator.Get("To"));
        TemplateHandler.Assign(ref hotNewsContent, "Submit", Translator.Get("Submit"));
        TemplateHandler.Assign(ref hotNewsContent, "ID", Translator.Get("ID"));
        TemplateHandler.Assign(ref hotNewsContent, "PostDate", Translator.Get("PostDate"));
        TemplateHandler.Assign(ref hotNewsContent, "Message", Translator.Get("Message"));
        TemplateHandler.Assign(ref hotNewsContent, "CurrentYear", DateTime.Now.Year);
        TemplateHandler.Assign(ref hotNewsContent, "StartYear", AppSettings.SystemStartDate.Year);
             
        int intTotalPage = 0;
        int intPageIndex = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 10);

        int intImportant = CastAide.AsInt32(Request["slSearchOption"], -1);
        CultureInfo cult = new CultureInfo("en-US");
        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        if (dtmFrom != null || dtmTo != null)
        {
            TemplateHandler.Assign(ref hotNewsContent, "fdate", AffCommon.ConvertDateTime(dtmFrom, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "ServerDateTime", AffCommon.ConvertDateTime(dtmFrom, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "tdate", AffCommon.ConvertDateTime(dtmTo, "/", true, false));
        }
        else
        {
            TemplateHandler.Assign(ref hotNewsContent, "fdate", AffCommon.ConvertDateTime(DateTime.Now, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "ServerDateTime", AffCommon.ConvertDateTime(DateTime.Now, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "tdate", AffCommon.ConvertDateTime(DateTime.Now, "/", true, false));
        }

        TemplateHandler.ReplaceBlock(ref hotNewsContent, "Content", Fanex.Affiliate.Core.Customer.GenerateHTML.HotNews.GetHotNewsContentList(intImportant, dtmFrom, dtmTo, intPageIndex, intPageSize, ref intTotalPage));
        TemplateHandler.Assign(ref hotNewsContent, "page", intPageIndex);
        if (intTotalPage > 0)
        {
            TemplateHandler.Assign(ref hotNewsContent, "paging-top", AppCommon.GenPaging(intTotalPage, intPageIndex, intPageSize, 0, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
            TemplateHandler.Assign(ref hotNewsContent, "paging-bottom", AppCommon.GenPaging(intTotalPage, intPageIndex, intPageSize, 0, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref hotNewsContent, "paging-top", string.Empty);
            TemplateHandler.Assign(ref hotNewsContent, "paging-bottom", string.Empty);
        }
    }
}
