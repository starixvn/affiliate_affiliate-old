// -----------------------------------------------------------------------
// <copyright file="Zip.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Zip
    {
        private static UnicodeEncoding _encoding=new UnicodeEncoding();

        static byte[] GetBytes(string str)
        {
            byte[] data = new byte[str.Length * 2];
            for (int i = 0; i < str.Length; ++i)
            {
                char ch = str[i];
                data[i * 2] = (byte)(ch & 0xFF);
                data[i * 2 + 1] = (byte)((ch & 0xFF00) >> 8);
            }

            return data;
        }

        static string GetString(byte[] arr)
        {
            char[] ch = new char[arr.Length / 2];
            for (int i = 0; i < ch.Length; ++i)
            {
                ch[i] = (char)((int)arr[i * 2] + (((int)arr[i * 2 + 1]) << 8));
            }
            return new String(ch);
        }

        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string Compress(string s)
        {
            var bytes = GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return GetString(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return GetString(mso.ToArray());
            }
        }
    }
}
