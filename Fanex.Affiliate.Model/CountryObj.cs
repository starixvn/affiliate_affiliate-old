// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Model
{
    using Fanex.Affiliate.Model.Attributes;
    using System;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class CountryObj: BaseObj
    {        
        public string Description { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsEnabled { get; set; }

    }
}
