// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Model
{
    using Fanex.Affiliate.Model.Attributes;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    
    public class BaseObj : ICloneable
    {        
        [Key]
        public string Id { get; set; }
        [Value]
        public string Value { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
       
    }
}
