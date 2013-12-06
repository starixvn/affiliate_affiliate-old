using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DNAide.Web;
using ExcelUtil.Spreadsheet;
using System.Text;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;

public partial class Resources_UploadExcel : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string temp = TemplateHandler.LoadTemplate("Resources/UploadExcel.htm");
            TemplateHandler.Assign(ref temp, "File", Fanex.Affiliate.Resources.Translator.Get("ExcelTranslationTitle"));
            TemplateHandler.Assign(ref temp, "ErrFileExtension", Fanex.Affiliate.Resources.Translator.Get("ErrExcelFileExtension"));
            this.HtmlContent = temp;
        }
        
    }
}
