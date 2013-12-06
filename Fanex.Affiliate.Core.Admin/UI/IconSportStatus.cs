// -----------------------------------------------------------------------
// <copyright file="IconProductStatus.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.UI
{
    public class IconProductStatus
    {
        public string Enabled = "i_enableProduct";
        public string Disabled = "i_disableProduct";
        public static IconProductStatus GetInstance()
        {
            return new IconProductStatus();
        }
    }
}
