using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Affiliate.Model.Attributes;
using Fanex.Affiliate.Resources;
using System.Data;

namespace Fanex.Affiliate.Core.Helper
{
    public class HtmlHelper
    {
        public static string OptionList(
            DataTable table,            
            string textColumn,
            string valueColumn,
            object selectedValue = null,
            string firstItem = null,            
            bool hasAllColumns = false)
        {
            return HtmlHelper.DropDownList(table,null, textColumn, valueColumn, selectedValue, null, null, false, null, false, hasAllColumns);
        }
        public static string DropDownList(
            DataTable table,
            string id,
            string textColumn,
            string valueColumn,            
            object selectedValue = null,
            string firstItem = null,
            string className = null,
            bool isDisabled = false,           
            string attributes = null,
            bool hasSelectTag = true,
            bool hasAllColumns = false)
        {

            string selValue = selectedValue == null ? null : selectedValue.ToString();
            var s = new StringBuilder();
            if(hasSelectTag)
                s.AppendFormat("<select id='{0}' name='{0}' class='{1}' {2} {3}>", id, (isDisabled ? "disabled" : String.Empty), attributes);
            if (!String.IsNullOrEmpty(firstItem))
                s.AppendFormat("<option value=''>{0}</option>", firstItem);

            if (table.Rows.Count > 0)
            {                
                foreach (DataRow row in table.Rows)
                {
                    var value = row[valueColumn].ToString();

                    var attr = new StringBuilder();

                    if (hasAllColumns)
                    {
                        
                        foreach (DataColumn col in table.Columns)
                        {
                            if (col.ColumnName != textColumn && col.ColumnName != valueColumn)
                            {
                                var v = row[col];
                                if (v != null)
                                {
                                    if (v is bool)
                                        attr.AppendFormat("data-{0}=\"{1}\"", col.ColumnName.ToLower(), (bool)v ? 1 : 0);
                                    else
                                        attr.AppendFormat("data-{0}=\"{1}\"", col.ColumnName.ToLower(), v.ToString());
                                }
                            }
                        }
                    }
                    
                    s.AppendFormat("<option value='{0}' {1} {2}>{3}</option>",
                                        value,
                                        selectedValue != null ?
                                        (value.Equals(selValue, StringComparison.OrdinalIgnoreCase) ? "selected" : String.Empty) : String.Empty,
                                        attr.ToString(),
                                        row[textColumn]);
                    
                }
            }
            if (hasSelectTag)
                s.Append("</select>");
            return s.ToString();
        }
        public static string DropDownList<T>(
            IList<T> list,
            string id,
            object selectedValue = null,
            bool hasAll = false,
            string className = null,
            bool isDisabled = false,
            string attributes = null)
        {
            var all = Translator.Get("All");
            return DropDownList(list,id,selectedValue, (hasAll ? all: null),className,isDisabled,attributes);
        }

        public static string DropDownList<T>(
            IList<T> list, 
            string id,             
            object selectedValue = null,
            string firstItem = null,
            string className = null,
            bool isDisabled = false,
            string attributes = null){

            string selValue = selectedValue == null ? null : selectedValue.ToString();
            var s = new StringBuilder();            
            s.AppendFormat("<select id='{0}' name='{0}' class='{1}' {2} {3}>", id,className,(isDisabled? "disabled":String.Empty),attributes);
            if (!String.IsNullOrEmpty(firstItem))
                s.AppendFormat("<option value=''>{0}</option>",firstItem);
            
            if (list.Count > 0)
            {
                var properties = list[0].GetType().GetProperties(); // typeof(T).GetProperties();
                var valueProp = properties.Where(
                    prop => Attribute.IsDefined(prop, typeof(Key))).First();
                var textProp =  properties.Where( 
                    prop => Attribute.IsDefined(prop, typeof(Value))).First();
                
                
                foreach (var item in list)
                {
                    var value = valueProp.GetValue(item, null).ToString();
                    var attr = new StringBuilder();
                    foreach (var p in properties)
                    {
                        if (p.Name != valueProp.Name && p.Name != textProp.Name)
                        {
                            var v = p.GetValue(item, null);
                            if (v != null)
                            {
                                if(v is bool)
                                    attr.AppendFormat("data-{0}=\"{1}\"", p.Name.ToLower(), (bool)v ? 1 : 0);
                                else
                                    attr.AppendFormat("data-{0}=\"{1}\"", p.Name.ToLower(), v.ToString());
                            }
                        }
                    }
                    s.AppendFormat("<option value='{0}' {1} {2}>{3}</option>",
                                        value,
                                        selectedValue!= null ?                                        
                                        (value.Equals(selValue, StringComparison.OrdinalIgnoreCase) ? "selected" : String.Empty) : String.Empty,
                                        attr.ToString(),
                                        textProp.GetValue(item, null));
                }
            }
            s.Append("</select>");
            return s.ToString();
        }
    }
}
