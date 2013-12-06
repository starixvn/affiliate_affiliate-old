// -----------------------------------------------------------------------
// <copyright file="Status.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    /// <summary>
    /// Summary description for Status
    /// </summary>
    public class Status
    {
        private static string _pending = "Pending";
        private static string _disapproved = "Disapproved";
        private static string _approved = "Approved";
        public static string Pending
        {
            get
            {
                return _pending;
            }
        }
        public static string Disapproved
        {
            get
            {
                return _disapproved;
            }
        }
        public static string Approved
        {
            get
            {
                return _approved;
            }
        }
    }

}
