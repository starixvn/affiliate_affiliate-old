using Fanex.Affiliate.Model.Attributes;
using System;
// -----------------------------------------------------------------------
// <copyright file="Currency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Model
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    
    public class CurrencyObj : BaseObj
    {
        public string Prefix { get; set; }
        public string Name { get; set; }
        public int MultipliedBy { get; set; }       
    }
}
