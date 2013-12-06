namespace Fanex.LiveBet365.Core
{
    using DNAide.Data;
    using System.Data;
    using System.Text;
    using Fanex.LiveBet365.DataObjects;

    /// <summary>
    /// Summary description for Language
    /// </summary>
    public static class Languageaa
    {
        private static string _english = "en-US";
        private static string _chineseCS = "zh-CN";//Simplified Chinese 
        private static string _chineseCT = "zh-TW";//Traditional Chinese
        private static string _spanish = "es-ES";
        private static string _thai = "th-TH";
        private static string _vietnamese = "vi-VN";
        private static string _indonesia = "id-ID";

        private static string _chinese = "zh-CN";
        // ... more language

        public static string CHINESE
        {
            get
            {
                return _chinese;
            }
        }

        public static string ENGLISH
        {
            get
            {
                return _english;
            }
        }

        public static string CHINESE_CS
        {
            get
            {
                return _chineseCS;
            }
        }

        public static string CHINESE_CT
        {
            get
            {
                return _chineseCT;
            }
        }
        public static string SPANISH
        {
            get
            {
                return _spanish;
            }
        }
        public static string THAI
        {
            get
            {
                return _thai;
            }
        }
        public static string VIETNAMESE
        {
            get
            {
                return _vietnamese;
            }
        }
        public static string INDONESIA
        {
            get
            {
                return _indonesia;
            }
        }
    }
}