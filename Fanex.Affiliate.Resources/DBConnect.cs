using System.Configuration;
using DNAide.Data;

namespace Fanex.Affiliate.Resources
{
    public static class DbConnect
    {
        private static string dBType;
        private static string spName;
        static DbConnect()
        {
            dBType = ConfigurationManager.AppSettings["TextExpressionBuilder.Database"];
            spName = ConfigurationManager.AppSettings["TextExpressionBuilder.SP"];
        }
        public static DbResult GetData()
        {
            return DbHelper.Execute(spName, dBType);
        }
    }
}
