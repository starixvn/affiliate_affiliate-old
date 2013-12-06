// -----------------------------------------------------------------------
// <copyright file="GroupSettingObj.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.LiveBet365.DataObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GroupSettingObj
    {
        /*Approve session */
        public bool isApproved = false;
        public string fName = string.Empty;
        public string email = string.Empty;
        public string affName = string.Empty;
        public string language = string.Empty;
        public string password = string.Empty;
        /*Approve session */
        public bool isEdited = true;
        public int id;
        public int copyProductID = -1;
        public string groupName;
        public string description;
        public IList<ProductSettingObj> productSetting = new List<ProductSettingObj>();
        public static GroupSettingObj GetInstance()
        {
            return new GroupSettingObj();
        }
    }
}
