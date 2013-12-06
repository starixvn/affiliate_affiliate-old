using System;
using System.Text;
using System.Collections;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class Report_MemberStatisticsReportDetail : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/MemberStatisticsReportDetail.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "date", Fanex.Affiliate.Resources.Translator.Get("Date"));
        TemplateHandler.Assign(ref strTpl, "aff-txt", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Fanex.Affiliate.Resources.Translator.Get("From"));
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Fanex.Affiliate.Resources.Translator.Get("To"));
        TemplateHandler.Assign(ref strTpl, "search", Fanex.Affiliate.Resources.Translator.Get("Search"));
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "memberstatistics", Fanex.Affiliate.Resources.Translator.Get("MemberStatistics"));
        TemplateHandler.Assign(ref strTpl, "affiliate", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
        TemplateHandler.Assign(ref strTpl, "active", Fanex.Affiliate.Resources.Admin.Active);
        TemplateHandler.Assign(ref strTpl, "dormant", Fanex.Affiliate.Resources.Translator.Get("Dormant"));
        TemplateHandler.Assign(ref strTpl, "deposit", Fanex.Affiliate.Resources.Admin.FirstDeposit);
        TemplateHandler.Assign(ref strTpl, "nondeposit", Fanex.Affiliate.Resources.Translator.Get("NonDeposit"));
        TemplateHandler.Assign(ref strTpl, "totalregister", Fanex.Affiliate.Resources.Admin.TotalRegister);
        TemplateHandler.Assign(ref strTpl, "total", Fanex.Affiliate.Resources.Translator.Get("Total"));
        TemplateHandler.Assign(ref strTpl, "totalclick", Fanex.Affiliate.Resources.Translator.Get("TotalClicks"));
        TemplateHandler.Assign(ref strTpl, "totalunique", Fanex.Affiliate.Resources.Translator.Get("UniqueClicks"));
        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("MemberStatisticsDetail"));
        TemplateHandler.Assign(ref strTpl, "GetAll", Fanex.Affiliate.Resources.Admin.GetAll);
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "AdminCommon.GetAffUsername(1166364)"
        int width = 500;
        int intAffId = CastAide.AsInt32(Request["affid"]);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intPage = CastAide.AsInt32(Request["page"], 1);

        TemplateHandler.Assign(ref strTpl, "width", width);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "affname", Request["affname"]);
        TemplateHandler.Assign(ref strTpl, "affid", intAffId);

        //Get data from Deposit site
        DbResult rsTotal = Affiliate.SelAffiliateStatisticsDetail(intAffId, dtmFDate, dtmTDate, isGetAll, intPageSize, intPage);

        DataTable dtb = rsTotal.ResultSet.Tables.Count > 0 ? rsTotal.ResultSet.Tables[0] : new DataTable();

        //decimal dctotalPage = dtb.Rows.Count / intPageSize;
        int totalPage = CastAide.AsInt32(rsTotal.Outputs["totalpages"]);
        int nummem = CastAide.AsInt32(rsTotal.Outputs["nrows"]);

        if (dtb.Rows.Count > 0)
        {
            int intCount = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;
            StringBuilder strListRow = new StringBuilder();

            foreach (DataRow dr in dtb.Rows)                
            {
                string strTmp = strRowTpl;
                intCount++;

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                DateTime dtmCreatedDate = CastAide.AsDateTime(dr["date"], DateTime.Now);

                TemplateHandler.Assign(ref strTmp, "date-val", dtmCreatedDate.ToString("dd-MMM-yyyy"));
                TemplateHandler.Assign(ref strTmp, "active-val", dr["active"]);
                //TemplateHandler.Assign(ref strTmp, "dormant-val", dr["dormant"]);
                TemplateHandler.Assign(ref strTmp, "totalregister-val", dr["totalregistered"]);
                TemplateHandler.Assign(ref strTmp, "total-val", dr["total"]);
                //TemplateHandler.Assign(ref strTmp, "totalclick-val", dr["TotalClick"]);
                //TemplateHandler.Assign(ref strTmp, "totalunique-val", dr["UniqueClick"]);
                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(7));
        }

        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 2, nummem, SubMenu.ReportText[2], "i_report.png"));
        //Assign Paging template
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, 0, "aPageTop", "txtPageTop", width, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPage, intPage, intPageSize, 0, "aPageBottom", "txtPageBottom", width, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
    }

    #region Convert datatable to Arraylist
    public ArrayList DataSetToArrayList(int ColumnIndex, DataTable dataTable)
    {
        ArrayList output = new ArrayList();

        foreach (DataRow row in dataTable.Rows)
            output.Add(row[ColumnIndex]);

        return output;
    }
    #endregion

    #region Full outer join 2 datatables
    public DataTable myJoinMethod(DataTable LeftTable, DataTable RightTable,
            String LeftPrimaryColumn, String RightPrimaryColumn)
    {
        //first create the datatable columns 
        DataSet mydataSet = new DataSet();
        mydataSet.Tables.Add("  ");
        DataTable myDataTable = mydataSet.Tables[0];

        //add left table columns 
        DataColumn[] dcLeftTableColumns = new DataColumn[LeftTable.Columns.Count];
        LeftTable.Columns.CopyTo(dcLeftTableColumns, 0);

        foreach (DataColumn LeftTableColumn in dcLeftTableColumns)
        {
            if (!myDataTable.Columns.Contains(LeftTableColumn.ToString()))
                myDataTable.Columns.Add(LeftTableColumn.ToString());
        }

        //now add right table columns 
        DataColumn[] dcRightTableColumns = new DataColumn[RightTable.Columns.Count];
        RightTable.Columns.CopyTo(dcRightTableColumns, 0);

        foreach (DataColumn RightTableColumn in dcRightTableColumns)
        {
            if (!myDataTable.Columns.Contains(RightTableColumn.ToString()))
            {
                if (RightTableColumn.ToString() != RightPrimaryColumn)
                    myDataTable.Columns.Add(RightTableColumn.ToString());
            }
        }

        //add left-table data to mytable 
        foreach (DataRow LeftTableDataRows in LeftTable.Rows)
        {
            myDataTable.ImportRow(LeftTableDataRows);
        }

        ArrayList var = new ArrayList(); //this variable holds the id's which have joined 

        ArrayList LeftTableIDs = new ArrayList();
        LeftTableIDs = this.DataSetToArrayList(0, LeftTable);

        //import righttable which having not equal Id's with lefttable 
        foreach (DataRow rightTableDataRows in RightTable.Rows)
        {
            if (LeftTableIDs.Contains(rightTableDataRows[0]))
            {
                string wherecondition = "[" + myDataTable.Columns[0].ColumnName + "]='"
                        + rightTableDataRows[0].ToString() + "'";
                DataRow[] dr = myDataTable.Select(wherecondition);
                int iIndex = myDataTable.Rows.IndexOf(dr[0]);

                foreach (DataColumn dc in RightTable.Columns)
                {
                    if (dc.Ordinal != 0)
                        myDataTable.Rows[iIndex][dc.ColumnName.ToString().Trim()] =
                rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                }
            }
            else
            {
                int count = myDataTable.Rows.Count;
                DataRow row = myDataTable.NewRow();
                row[0] = rightTableDataRows[0].ToString();
                myDataTable.Rows.Add(row);
                foreach (DataColumn dc in RightTable.Columns)
                {
                    if (dc.Ordinal != 0)
                        myDataTable.Rows[count][dc.ColumnName.ToString().Trim()] =
                rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                }
            }
        }

        return myDataTable;
    }
    #endregion
}
