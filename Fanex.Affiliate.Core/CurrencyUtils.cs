// -----------------------------------------------------------------------
// <copyright file="CurrencyCodeMapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class CurrencyUtils
    {
        private const string NEGATIVE_FORMAT_STRING = "<span style='color: red'>{0}</span>";
        private static CultureInfo _ENG_CULTURE = new CultureInfo("en-US");
        private static CultureInfo defaultCulture = Thread.CurrentThread.CurrentCulture;

        private static readonly Dictionary<string, CultureInfo> SymbolsByCode = new Dictionary<string, CultureInfo>();

        public static string GetCurrencySymbol(string currencyCode = null)
        {
            if (currencyCode == null)
                return defaultCulture.NumberFormat.CurrencySymbol;
            if (SymbolsByCode.Count == 0)
                InitCurrencyCodeMapper();
            return SymbolsByCode[currencyCode].NumberFormat.CurrencySymbol;
        }
        public static void SetDefaultCurrency(string currencyCode)
        {
            defaultCulture = GetCulture(currencyCode);
        }
        public static void SetDefaultCurrency(CultureInfo culture)
        {
            defaultCulture = culture;
        }

        public static CultureInfo GetCulture(string currencyCode)
        {

            if (SymbolsByCode.Count == 0)
                InitCurrencyCodeMapper();
            if (string.IsNullOrEmpty(currencyCode))
                return SymbolsByCode["MYR"];
            if (currencyCode.ToUpper() == "UUS")
                return SymbolsByCode["USD"];



            return SymbolsByCode[currencyCode];
        }

        private static void InitCurrencyCodeMapper()
        {

            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var item in regions)
            {

                var key = new RegionInfo(item.LCID).ISOCurrencySymbol;
                if (!SymbolsByCode.ContainsKey(key))
                {
                    item.NumberFormat.CurrencyDecimalDigits = 2;

                    SymbolsByCode.Add(key, item);
                }
            }
        }

        public static string Format(object value, CultureInfo culture, string negativeFormat = null, int decimalDigits = 2)
        {
            double v = 0;
            Double.TryParse(value.ToString(), out v);
            var currencySymbol = culture.NumberFormat.CurrencySymbol ?? defaultCulture.NumberFormat.CurrencySymbol;
            var format = "#,##0";
            if (decimalDigits > 0)
            {
                format += ".".PadRight(decimalDigits+1,'0');
            }
           // var str = v.ToString(currencySymbol + "#,##0.00", culture.NumberFormat);// CultureInfo.CreateSpecificCulture("en-US"));
            var str = currencySymbol + v.ToString(format, _ENG_CULTURE);
            if (v < 0)
            {
                if (str.Contains("-"))
                    str = "(" + str.Replace("-", String.Empty) + ")";
                str = String.Format(negativeFormat ?? NEGATIVE_FORMAT_STRING, str);
            }

            return str;
        }

        public static string Format(object value, string currencyCode, string negativeFormat, int decimalDigits = 2)
        {
            return Format(value, CurrencyUtils.GetCulture(currencyCode), negativeFormat,decimalDigits);
        }

        public static string Format(object value, string negativeFormat)
        {
            return Format(value, defaultCulture, negativeFormat);
        }
        public static string Format(object value)
        {
            return Format(value, defaultCulture);
        }
    }
}
