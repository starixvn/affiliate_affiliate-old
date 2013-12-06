// -----------------------------------------------------------------------
// <copyright file="ProductStatusColor.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.UI
{
    public class ProductStatusColor
    {
        public string Enabled = "enableProductColor";
        public string Disabled = "disableProductColor";
        public static ProductStatusColor GetInstance()
        {
            return new ProductStatusColor();
        }
    }
}
