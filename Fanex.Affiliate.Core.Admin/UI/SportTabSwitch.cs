// -----------------------------------------------------------------------
// <copyright file="ProductTabSwitch.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.UI
{
    public class ProductTabSwitch
    {
        public string Expand = "";
        public string Collapse = "none";
        public static ProductTabSwitch GetInstance()
        {
            return new ProductTabSwitch();
        }
    }
}
