using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Helper;


namespace Fanex.Web.Macaubet.Admin.Test
{
    public partial class Resources : System.Web.UI.Page
    {

        public string _langDropdown = string.Empty;
        public string _key = String.Empty;
        public string _value = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
             var lang = CastAide.AsString(Request["lang"], "en-US");
            _key  = CastAide.AsString(Request["key"], String.Empty);
            _value = CastAide.AsString(Request["value"], String.Empty);
            var culture = new CultureInfo(lang);

            
            _langDropdown = HtmlHelper.DropDownList(AppSettings.BaseLanguages, "lang", lang, false);
                       
            
            var resources = Fanex.Affiliate.Resources.Admin.ResourceManager.GetResourceSet(culture, true, true);
            if (resources == null)
                return;

            var key = _key.ToLower();
            var value= _value.ToLower();
            var dict = resources.Cast<DictionaryEntry>()
                .Where(r => (key == String.Empty || r.Key.ToString().ToLower().Contains(key))
                            && (value == String.Empty || r.Value.ToString().ToLower().Contains(value)))
                .ToDictionary(r => r.Key.ToString(), r => r.Value.ToString())
                .OrderBy(r => r.Key);



            gridView1.DataSource = dict;
            gridView1.DataBind();
        }

        protected void cmbResources_SelectedIndexChanged(object sender, EventArgs e)
        {

              
            
        }
    }
}
