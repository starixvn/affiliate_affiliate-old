namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using ExcelUtil.Spreadsheet;
    using System.Collections;
    using System.Text;
    using Fanex.Core.Culture;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;

    public class UploadExcel : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/html";
                string filePath = context.Server.MapPath("../") + "ExcelFiles//";
                string strDst = string.Empty;
                int errcode = 0;
                string folderName = "Translation";
                string fileName = string.Empty;

                Common.UploadFileExcel(context, folderName, filePath, ref errcode, ref fileName);

                if (errcode == 1)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ExcelErrmsgEmpty") + "');</script>");
                    return;
                }
                else if (errcode == 2)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ExcelErrmsgFile") + "');</script>");
                    return;
                }
                /*Read file excel*/

                ExcelFile excelFile = new ExcelFile();
                ReadFileExcel(context, excelFile, fileName);
            }
        }

        // Joseph: Using SpreadsheetGear instead of ExcelUtil
        public void ReadFileExcel(HttpContext context, ExcelFile excelFile, string fileName)
        {
            string ext = fileName.Substring(fileName.LastIndexOf('.') + 1, fileName.Length - fileName.LastIndexOf('.') - 1);

            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(fileName);
            var dataset = workbook.GetDataSet(SpreadsheetGear.Data.GetDataFlags.FormattedText);
            var table = dataset.Tables[0];
            
            string strSQL = string.Empty;
            string strKey = string.Empty;
            int indexRow, indexColumn;
            
            int rowCount = table.Rows.Count; 
            DNAide.Data.DbResult dbResult;
            Hashtable htHeader = new Hashtable();
            StringBuilder sbXMLData = new StringBuilder();

            if (rowCount > 0)
            {
              
                if (table.Columns.Count == 0)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ExcelUploadFail") + "');</script>");
                    return;
                }
                ///
                /// <root><res><key></key><type></type><value></value><code></code><des></des></res></root>
                /// 
                sbXMLData.Append("<root>");
                for (indexRow = 0; indexRow < rowCount; indexRow++)
                {
                    for (indexColumn = 2; indexColumn < table.Columns.Count; indexColumn++)
                    {
                        sbXMLData.Append("<res>");
                        sbXMLData.Append(string.Format("<key>{0}</key>", 
                            HttpUtility.HtmlEncode(table.Rows[indexRow][0].ToString())));
                        sbXMLData.Append(string.Format("<type>{0}</type>", 
                            HttpUtility.HtmlEncode(table.Rows[indexRow][1].ToString())));
                        sbXMLData.Append(string.Format("<value>{0}</value>",
                            HttpUtility.HtmlEncode(table.Rows[indexRow][indexColumn].ToString())));
                        sbXMLData.Append(string.Format("<code>{0}</code>", 
                            HttpUtility.HtmlEncode(table.Columns[indexColumn].ColumnName.Split('_')[1])));
                        sbXMLData.Append(string.Format("<des></des>"));
                        sbXMLData.Append("</res>");
                    }
                }
                sbXMLData.Append("</root>");
                var xml = sbXMLData.ToString();
                dbResult = Resource.InsertOrUpdateResources(xml);
                if (dbResult.Outputs["errcode"].ToString() != "0")
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ExcelUploadFail") + "');</script>");

                context.Response.Write("<script type=\"text/javascript\">alert('" + Fanex.Affiliate.Resources.Translator.Get("ExcelUploadSuccessful") + "');parent.ClosePopup();</script>");

            }
        }
        //public void ReadFileExcel(HttpContext context, ExcelFile excelFile, string fileName)
        //{
        //    string ext = fileName.Substring(fileName.LastIndexOf('.') + 1, fileName.Length - fileName.LastIndexOf('.') - 1);

        //   
        //    
        //    if (ext.Equals("xls"))
        //        excelFile.LoadXls(fileName, XlsOptions.PreserveAll);
        //    else
        //        excelFile.LoadXlsxFromDirectory(fileName, XlsxOptions.PreserveKeepOpen);
        //    ExcelWorksheet workSheet = excelFile.Worksheets[0];
        //    
        //    string strSQL = string.Empty;
        //    string strKey = string.Empty;
        //    int indexRow, indexColumn;
        //    int indexRegex;
        //    int headerCount = 0;
        //    int indexEnUs = 0;
        //    int rowCount = workSheet.Rows.Count;
        //    DNAide.Data.DbResult dbResult;
        //    Hashtable htHeader = new Hashtable();
        //    StringBuilder sbXMLData = new StringBuilder();

        //    if (rowCount > 1)
        //    {
        //        while (workSheet.Cells[0, headerCount].Value != null)
        //        {
        //            strKey = workSheet.Cells[0, headerCount].Value.ToString();
        //            indexRegex = strKey.LastIndexOf('_');
        //            htHeader[headerCount] = strKey.Substring(indexRegex + 1, strKey.Length - indexRegex - 1);
        //            if (htHeader[headerCount].ToString().ToLower() == "en-us")
        //            {
        //                indexEnUs = headerCount;
        //            }
        //            headerCount++;
        //        }
        //        if (headerCount == 0)
        //        {
        //            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ExcelUploadFail + "');</script>");
        //            return;
        //        }
        //        ///
        //        /// <root><res><key></key><type></type><value></value><code></code><des></des></res></root>
        //        /// 
        //        sbXMLData.Append("<root>");
        //        for (indexRow = 1; indexRow < rowCount; indexRow++)
        //        {
        //            for (indexColumn = 2; indexColumn < headerCount; indexColumn++)
        //            {
        //                sbXMLData.Append("<res>");
        //                sbXMLData.Append(string.Format("<key>{0}</key>", HttpUtility.HtmlEncode(workSheet.Cells[indexRow, 0].Value.ToString())));
        //                sbXMLData.Append(string.Format("<type>{0}</type>", HttpUtility.HtmlEncode(workSheet.Cells[indexRow, 1].Value.ToString())));
        //                sbXMLData.Append(string.Format("<value>{0}</value>", HttpUtility.HtmlEncode(workSheet.Cells[indexRow, indexColumn].Value == null ? workSheet.Cells[indexRow, indexEnUs].Value.ToString() : workSheet.Cells[indexRow, indexColumn].Value.ToString())));
        //                sbXMLData.Append(string.Format("<code>{0}</code>", HttpUtility.HtmlEncode(htHeader[indexColumn].ToString())));
        //                sbXMLData.Append(string.Format("<des></des>"));
        //                sbXMLData.Append("</res>");
        //            }
        //        }
        //        sbXMLData.Append("</root>");
        //        dbResult = Resource.InsertOrUpdateResources(sbXMLData.ToString());
        //        if (dbResult.Outputs["errcode"].ToString() != "0")
        //            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ExcelUploadFail + "');</script>");

        //        context.Response.Write("<script type=\"text/javascript\">alert('" + Fanex.Affiliate.Resources.Translator.Get("ExcelUploadSuccessful + "');parent.ClosePopup();</script>");

        //    }
        //}
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
