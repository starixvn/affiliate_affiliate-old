using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Fanex.Affiliate.DataObjects;
using DNAide.Data;
using System.Xml;
using System.Data;
using Fanex.Affiliate.Model;

namespace Fanex.Web.MYM.Admin.Test
{
    public partial class BuilSpConfigXmlFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            DbResult result = Fanex.Affiliate.DataObjects.DbHelper.Execute("Test_GetAllSP", DBType.AFFILIATE);

            var table = result.ResultSet.Tables[0];

            XmlDocument doc = new XmlDocument();            
            XmlElement root = doc.CreateElement("Functions");
            doc.AppendChild(root);
            

            foreach (DataRow row in table.Rows)
            {
                var p = doc.CreateElement("Setting");
                p.SetAttribute("Key", row["name"].ToString());
                p.SetAttribute("SpName", row["name"].ToString());
                p.SetAttribute("DbName", "affiliate");

                root.AppendChild(p);
            }

            doc.Save("C:/SpName.xml");
        }
    }
}