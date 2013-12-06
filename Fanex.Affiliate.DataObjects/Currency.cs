// -----------------------------------------------------------------------
// <copyright file="Currency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Collections.Generic;
    using DNAide.Data;
    using Fanex.Affiliate.Model;
    using DNAide.Core;
    using System.Data;
    using System.Collections;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Currency
    {

        public static int CountCurrenciesNotInExchangeTable()
        {
            DbResult result= DbHelper.Execute("Aff_CountCurrenciesNotInExchange", DBType.AFFILIATE);
            
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                return CastAide.AsInt32(result.ResultSet.Tables[0].Rows[0]["count"],0);
            }
            return 0;
        }
        public static IList<CurrencyObj> GetCurrenciesNotInExchangeTable()
        {
            DbResult result= DbHelper.Execute("Aff_GetCurrenciesNotInExchange", DBType.AFFILIATE);
            IList<CurrencyObj> list = new List<CurrencyObj>();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    CurrencyObj obj = new CurrencyObj();

                    obj.Id= CastAide.AsString(row["CurrencyID"]);
                    obj.Value = CastAide.AsString(row["CurrencyCode"]);
                    obj.Name = CastAide.AsString(row["CurrencyName"]);                    
                    
                    list.Add(obj);
                }
            }
            return list;
        }

        public static CurrencyObj GetSystemCurrency()
        {
            DbResult result= DbHelper.Execute("Aff_SystemCurrencySel", DBType.AFFILIATE);
            CurrencyObj obj = new CurrencyObj();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count>0)
            { 
                var row=result.ResultSet.Tables[0].Rows[0];
                obj.Id = CastAide.AsString(row["ExchangeID"]);
                obj.Value= CastAide.AsString(row["Code"]);
                obj.Prefix = CastAide.AsString(row["Prefix"]);
                obj.Name = CastAide.AsString(row["Code"]);
            }
            return obj;
        }
        public static IList<BaseObj> GetAffSystemSupportedCurrencies()
        {
            // from SystemSettings
            DbResult result = DbHelper.Execute("Aff_SystemSupportedCurrencies", DBType.AFFILIATE);
            IList<BaseObj> list = new List<BaseObj>();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    CurrencyObj obj = new CurrencyObj();
                    obj.Id = CastAide.AsString(row["ExchangeID"]);
                    obj.Value = CastAide.AsString(row["Code"]);
                    obj.Prefix = CastAide.AsString(row["Prefix"]);
                    obj.MultipliedBy = CastAide.AsInt32(row["MultipliedBy"],1);
                    list.Add(obj);
                }
            }
            return list;

        }
        public static IList<BaseObj> GetAffiliateCurrencies()
        {
            // from SystemSettings
            DbResult result = DbHelper.Execute("Aff_AffiliateCurrenciesSel", DBType.AFFILIATE);
            IList<BaseObj> list = new List<BaseObj>();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    CurrencyObj obj = new CurrencyObj();
                    obj.Id = CastAide.AsString(row["ExchangeID"]);
                    obj.Value = CastAide.AsString(row["Code"]);
                    obj.Prefix =  CastAide.AsString(row["Prefix"]);
                    obj.MultipliedBy = CastAide.AsInt32(row["MultipliedBy"],1);
                    list.Add(obj);

                }
            }
            return list;

        }

        public static IList<BaseObj> GetSupportedBaseCurrencies()
        {
            // from SystemSettings
            DbResult result = DbHelper.Execute("Aff_BaseCurrenciesSel", DBType.AFFILIATE);
            IList<BaseObj> list = new List<BaseObj>();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    CurrencyObj obj = new CurrencyObj();
                    obj.Id = CastAide.AsString(row ["ExchangeID"]);
                    obj.Value = CastAide.AsString(row["Code"]);
                    obj.Prefix = CastAide.AsString(row["Prefix"]);
                    obj.MultipliedBy = CastAide.AsInt32(row["MultipliedBy"], 1);
                    list.Add(obj);

                }
            }
            return list;

        }

        public static IList<BaseObj> GetAffPlayerCurrencies()
        {
            // from SystemSettings
            DbResult result = DbHelper.Execute("Aff_PlayersCurrenciesSel", DBType.AFFILIATE);
            IList<BaseObj> list = new List<BaseObj>();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    CurrencyObj obj = new CurrencyObj();
                    obj.Id = CastAide.AsString(row["CurrencyID"]);
                    obj.Value = CastAide.AsString(row["CurrencyCode"]);                    
                    list.Add(obj);
                }
            }
            return list;

        }

        public static IList<BaseObj> GetAll()
        {
            // from Exchange
            DbResult result = DbHelper.Execute("Aff_CurrencyListSel", DBType.AFFILIATE);
            IList<BaseObj> list = new List<BaseObj>();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    CurrencyObj obj = new CurrencyObj();
                    obj.Id = CastAide.AsString(row["ExchangeID"]);
                    obj.Value = CastAide.AsString(row["Code"]);                    
                    list.Add(obj);

                }
            }
            return list;

        }


      
    
       
    }
}
