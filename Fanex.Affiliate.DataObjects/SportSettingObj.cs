// -----------------------------------------------------------------------
// <copyright file="ProductSettingObj.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.LiveBet365.DataObjects
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductSettingObj
    {
        public int id;
        public string productName;
        public bool isEnabled;
        public IList<DeductionTypeObj> deductions = new List<DeductionTypeObj>();
        public IList<Int64> minAmounts = new List<Int64>();
        public IList<Int64> maxAmounts = new List<Int64>();
        public IList<int> minQMs = new List<int>();
        public IList<int> maxQMs = new List<int>();
        public IList<string> commissions = new List<string>();
        public static ProductSettingObj GetInstance()
        {
            return new ProductSettingObj();
        }
    }
}
