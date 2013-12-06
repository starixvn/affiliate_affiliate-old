using System;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using System.Text;

public partial class ViewMessDetails : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("Others/ViewMess.htm");

        int messId = CastAide.AsInt32(Request["id"], -2);

        var languages = Language.Get();
        TemplateHandler.Assign(ref content, "MessageInformation", Fanex.Affiliate.Resources.Translator.Get("MessageInformation"));
        TemplateHandler.Assign(ref content, "Important", Fanex.Affiliate.Resources.Translator.Get("Important"));

        var str = new StringBuilder();
        foreach (var item in languages)
        {
            str.AppendFormat(@"<tr> <td class='r'>{0}</td><td>
                    <textarea style='width: 418px; height: 64px;' id='{1}'>{2}</textarea>
            </td></tr>", item.Value, item.Id, "{"+item.Id+"}");
        }
        TemplateHandler.Assign(ref content, "Language-rows", str);

        AppCommon.AssignToAdminTemplate(ref content,
        "Edit", "Delete", "Update", "Cancel", "Add",
        "ConfirmDeleteMessage","ConfirmUpdateMessage", "AddEmptyMessError");

        if (messId != -2)
        {
            TemplateHandler.Assign(ref content, "hdMode-val", "view");

            DbResult r = HotNews.GetHotNews(messId);

            if (r.ResultSet.Tables[0].Rows.Count > 0)
            {
                 var rw = r.ResultSet.Tables[0].Rows[0];
                TemplateHandler.Assign(ref content, "hdImportant-val", 
                    CastAide.AsBoolean(rw["important"], false) ?  "on" : String.Empty);
                TemplateHandler.Assign(ref content, "MsgID-val", CastAide.AsInt32(rw["msgid"]));
                for (int i = 0; i < rw.Table.Columns.Count; i++)
                {
                    var column = rw.Table.Columns[i];
                    if(column.ColumnName.Contains("-"))
                        TemplateHandler.Assign(ref content, column.ColumnName, rw[column.ColumnName].ToString());    
                }	            
            }
        }
        else
        {
            TemplateHandler.Assign(ref content, "hdImportant-val", "");
            TemplateHandler.Assign(ref content, "MsgID-val", Fanex.Affiliate.Resources.Translator.Get("NewMessage"));
            TemplateHandler.Assign(ref content, "hdMode-val", "add");
            foreach (var item in languages)
            {            
                TemplateHandler.Assign(ref content, item.Id, string.Empty);
            }            
        }

        this.HtmlContent = content;
    }
}
