// -----------------------------------------------------------------------
// <copyright file="IconDelete.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.UI
{
    public class IconDelete
    {
        public string Enabled = "icdelete";
        public string Disabled = "icdelete_disable";
        public static IconDelete GetInstance()
        {
            return new IconDelete();
        }
    }
}
