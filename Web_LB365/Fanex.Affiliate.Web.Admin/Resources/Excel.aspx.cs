using System;
using System.Web.UI;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;

public partial class Resources_Excel : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //try
            //{
            var query   = Request.QueryString["query"] == null ? null : Request.QueryString["query"].ToString();
            var value   = Request.QueryString["value"] == null ? null : Request.QueryString["value"].ToString();
            var type    = Request.QueryString["type"] == null ? null : Request.QueryString["type"].ToString();
            var lang    = Request.QueryString["lang"] == null ? null : Request.QueryString["lang"].ToString();
            var option  = Request.QueryString["option"] == null ? null : Request.QueryString["option"].ToString();

            var translations = Resource.GetResources(query, value, type, lang, option);
            string fileName = ExportExcel(translations, type);
            if (string.IsNullOrEmpty(fileName))
            {
                Response.Write("<h1>There is no information available.</h1>");
            }
            else
            {
                var pathFileExcel = Server.MapPath(string.Format("..\\ExcelFiles\\{0}", fileName));
                ResponseFile(fileName, pathFileExcel);
            }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }
    }
    public string ExportExcel(DNAide.Data.DbResult DbResult, string type)
    {
        string strFileName = string.Empty;
        int nRows;
        nRows = DbResult.ResultSet.Tables[0].Rows.Count;

        if (nRows > 0)
        {
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(Server.MapPath("~\\ExcelFiles\\template.xls"));

            //  ExcelWorksheetCollection worksheets = excelFile.Worksheets;
            ExportHeader(workbook.Worksheets[0], DbResult, nRows);
            ExportValue(workbook.Worksheets[0], DbResult, nRows);

            strFileName = String.Format("Translations_{0}_{1:ddMMyyyy_HHmmss}.xls", type, DateTime.Now);
            workbook.SaveAs(Server.MapPath(string.Format("~\\ExcelFiles\\{0}", strFileName)), SpreadsheetGear.FileFormat.Excel8);

            workbook.Save();
        }
        return strFileName;
    }
    public void ExportHeader(SpreadsheetGear.IWorksheet ws, DNAide.Data.DbResult DbResult, int nRows)
    {

        int ColumnWidth = int.Parse(Settings.GetSetting("ExcelWidth").ResultSet.Tables[0].Rows[0]["SValue"].ToString());
        ws.Cells[0, 0].Value = Fanex.Affiliate.Resources.Translator.Get("ExcelTranslationKey").ToString();
        //    ws.Cells[0, 0].Style.Font.Bold = true;

        ws.Cells[0, 1].Value = Fanex.Affiliate.Resources.Translator.Get("ExcelTranslationType").ToString();
        //    ws.Cells[0, 1].Style.Font.Bold = true;


        // remember to ensure that english is the first item

        int nColIndex = 2;//0: Key,1:Type, so start 2.
        for (int i = 0; i < AppSettings.BaseLanguages.Count; i++)
        {
            ws.Cells[0, nColIndex].Value =
                string.Format(Fanex.Affiliate.Resources.Translator.Get("ExcelTranslationLang").ToString(),
                AppSettings.BaseLanguages[i].Value, AppSettings.BaseLanguages[i].Id);

            //           ws.Cells[0, nColIndex].Style.Font.Bold = true;
            nColIndex++;
        }
    }
    public void ExportValue(SpreadsheetGear.IWorksheet ws, DNAide.Data.DbResult DbResult, int nRows)
    {
        string strResourceKey = string.Empty;
        int nRowIndex;//,nColIndex;

        nRowIndex = 0;
        // nColIndex = 0;

        var table = DbResult.ResultSet.Tables[0];
        for (int i = 0; i < nRows; i++)
        {
            var lang = table.Rows[i]["LanguageCode"].ToString();
            if (!string.Equals(strResourceKey, DbResult.ResultSet.Tables[0].Rows[i]["ResourceKey"].ToString()))
            {

                nRowIndex++;
                strResourceKey = DbResult.ResultSet.Tables[0].Rows[i]["ResourceKey"].ToString();
                ws.Cells[nRowIndex, 0].Value = strResourceKey;
                ws.Cells[nRowIndex, 1].Value = DbResult.ResultSet.Tables[0].Rows[i]["RefID"];
                // ws.Cells[nRowIndex, 2].Value = DbResult.ResultSet.Tables[0].Rows[i]["ResourceValue"].ToString();
                //nColIndex = 3;   
            }

            var col = 2;
            for (int j = 0; j < AppSettings.BaseLanguages.Count; j++)
            {
                if (lang.Equals(AppSettings.BaseLanguages[j].Id, StringComparison.OrdinalIgnoreCase))
                {
                    col += j;
                    break;
                }
            }
            var text = DbResult.ResultSet.Tables[0].Rows[i]["ResourceValue"].ToString();
            if (text.Length < 32768)
                ws.Cells[nRowIndex, col].Value = DbResult.ResultSet.Tables[0].Rows[i]["ResourceValue"];
        }
    }
    public void ImportExcel()
    {

    }
    public void ResponseFile(string fileName, string pathFileExcel)
    {
        System.IO.Stream iStream = null;
        byte[] buffer = new Byte[10000];
        int length;
        long dataToRead;

        //try
        //{
        iStream = new System.IO.FileStream(pathFileExcel, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        dataToRead = iStream.Length;

        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

        while (dataToRead > 0)
        {
            if (Response.IsClientConnected)
            {
                length = iStream.Read(buffer, 0, 10000);

                Response.OutputStream.Write(buffer, 0, length);
                Response.Flush();

                buffer = new Byte[10000];
                dataToRead = dataToRead - length;
            }
            else
            {
                dataToRead = -1;
            }
        }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write("Error : " + ex.Message);
        //}
        //finally
        //{
        if (iStream != null)
        {
            iStream.Close();
        }
        Response.Close();
        //}

    }
}
