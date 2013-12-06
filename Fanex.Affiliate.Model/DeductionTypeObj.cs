// -----------------------------------------------------------------------
// <copyright file="DeductionTypeObj.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Model
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeductionTypeObj
    {
        public int id;
        public string name;
        public bool isCheck;
        public double deductedPercentage;
        public static DeductionTypeObj GetInstance()
        {
            return new DeductionTypeObj();
        }
    }
}
