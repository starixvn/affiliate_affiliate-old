namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using System.Data;
    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;

    /// <summary>
    /// Summary description for OnlineList
    /// </summary>
    public class OnlineList
    {
        public static string GenerateOnlineListTable(int page, int pagesize)
        {
            string content = TemplateHandler.LoadTemplate("Home.htm");
            string table = TemplateHandler.CopyBlock(content, "tblOnline");
            TemplateHandler.Assign(ref table, "Username", Resources.Admin.Username);
            TemplateHandler.Assign(ref table, "LoggedIn", Resources.Admin.LoggedIn);
            TemplateHandler.Assign(ref table, "LastRequest", Resources.Admin.LastRequest);
            TemplateHandler.Assign(ref table, "Role", Resources.Admin.Role);
            TemplateHandler.Assign(ref table, "IP", Resources.Admin.IP);
            //DbResult ret = DataObjects.Home.GetOnlineList();
            int Nrows = 0, totalpages = 0;
            int NrowOnCurPage = 0;
            DbResult olRs;
            DataTable oldtb = new DataTable();

            while (NrowOnCurPage == 0)
            {
                olRs = null;
                olRs = DataObjects.Home.GetOnlineList(pagesize, page);
                totalpages = CastAide.AsInt32(olRs.Outputs["totalpages"]);
                Nrows = CastAide.AsInt32(olRs.Outputs["NRows"]);
                oldtb = olRs.ResultSet.Tables[0];
                if (page == 1)//Break out if page is equal 1
                    NrowOnCurPage = -1;
                else
                    NrowOnCurPage = oldtb.Rows.Count;
                if (NrowOnCurPage == 0)
                {
                    page = totalpages;
                }
            }
            string olRows = string.Empty;
            string olRow = TemplateHandler.CopyBlock(table, "OLRow");
            int No = 0;
            foreach (DataRow r in oldtb.Rows)
            {
                string temp = olRow;
                TemplateHandler.Assign(ref temp, "No-val", ++No + (page - 1) * pagesize);
                TemplateHandler.Assign(ref temp, "logged-in-val", AdminCommon.IBCDateTimeFormat(r["LoggedIn"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref temp, "lastrequest-val", AdminCommon.IBCDateTimeFormat(r["LastRequest"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref temp, "IP-val", r["IP"].ToString());
                TemplateHandler.Assign(ref temp, "ipchecker-url", AdminCommon.GetIPCheckerURL(r["IP"].ToString()));
                if (r["RoleName"].ToString() == "Admin")
                {
                    TemplateHandler.Assign(ref temp, "Username-val", r["username"].ToString());
                    TemplateHandler.Assign(ref temp, "Type-val", Resources.Admin.Administrator);
                }
                else
                {
                    TemplateHandler.Assign(ref temp, "Username-val", "<a href=\"../Customer/AffDefault.aspx?user=" + r["username"].ToString() + "\"> " + r["username"].ToString() + "</a>");
                    TemplateHandler.Assign(ref temp, "Type-val", Resources.Admin.Affiliate);
                }
                if ((No % 2) == 0)
                    TemplateHandler.Assign(ref temp, "color", AdminCommon.RowBgEven);
                else
                    TemplateHandler.Assign(ref temp, "color", AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref temp, "over", AdminCommon.RowBgOver);
                TemplateHandler.Assign(ref temp, "themepath", AppSettings.ThemeDir);
                olRows += temp;
            }
            TemplateHandler.ReplaceBlock(ref table, "OLRow", olRows);
            TemplateHandler.Assign(ref table, "paging-top", AdminCommon.GenPaging(totalpages, page, pagesize, Nrows, "aPageTop", "txtPageTop", 950, false, true));
            TemplateHandler.Assign(ref table, "paging-bottom", AdminCommon.GenPaging(totalpages, page, pagesize, Nrows, "aPageBottom", "txtPageBottom", 950, false, false));
            return table;
        }
    }
}