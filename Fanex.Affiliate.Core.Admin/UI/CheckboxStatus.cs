// -----------------------------------------------------------------------
// <copyright file="CheckboxStatus.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.UI
{
    public class CheckboxStatus
    {
        public string OnEnable = "chkbox_on";
        public string OffEnable = "chkbox_off";
        public string OnDisable = "chkbox_on_disable";
        public string OffDisable = "chkbox_off_disable";
        public static CheckboxStatus GetInstance()
        {
            return new CheckboxStatus();
        }
    }
}
