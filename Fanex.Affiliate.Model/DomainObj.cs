// -----------------------------------------------------------------------
// <copyright file="DomainObj.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects.Model
{
    using Fanex.Affiliate.Model.Attributes;
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DomainObj
    {
        [Key]
        public int DomainID {get; set;}
        [Value]
        public string DomainName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Creator { get; set; }        

    }
}
