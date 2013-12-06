// -----------------------------------------------------------------------
// <copyright file="Utility.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Core.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Text.RegularExpressions;
    using System.Security.Cryptography;
    using System.IO;
    using DNAide.Core;
    using DNAide.Utils;
    using System.Collections;
    using System.IO.Compression;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Common
    {

        /*smith*/
        public static string Snippet(string longString, int maxLength)
        {
            string str = HtmlToPlainText(longString);
            if (str.Length <= maxLength)
                return str;
            else
            {
                string snippet = str.Substring(0, maxLength);
                for (int i = maxLength - 1; i >= 0; i--)
                    if (snippet[i] != ' ')
                        snippet = snippet.Substring(0, snippet.Length - 1);
                    else
                    {
                        snippet += "...";
                        break;
                    }
                return snippet;
            }
        }
        /*smith*/
        public static string HtmlToPlainText(string htmlText)
        {
            return Regex.Replace(htmlText, "<[^>]*>", string.Empty);
        }
        /*smith*/
        public static string GetRandomString(int length)
        {
            //returns random string of length specified with characters including: 0-9, a-z, A-Z
            char[] ca = new char[length];
            byte[] random = new Byte[length];
            //RNGCryptoServiceProvider is an implementation of a random number generator.

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(random); // The array is now filled with cryptographically strong random bytes.

            for (int i = 0; i < length; i++)
            {
                bool found = false;
                int rand = (int)random[i];
                while (!found)
                {
                    if (((rand >= 48) && (rand <= 57)) || ((rand >= 65) && (rand <= 90)) || ((rand >= 97) && (rand <= 122)))
                    {
                        found = true;
                    }
                    else
                    {
                        //get a new random int. 
                        byte[] single = new byte[1];
                        rng.GetBytes(single);
                        rand = single[0];
                    }
                }
                char ci = (char)rand;
                ca[i] = ci;
            }
            string s = new string(ca);
            return s;
        }
    
        public static IList<string> GetStringIList(string stringArray, string sep)
        {
            string[] separator = { sep };
            string[] result = stringArray.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            IList<string> data = new List<string>();
            foreach (var item in result)
            {
                data.Add(item);
            }
            return data;
        }
        public static string CutLongText(string strContent, int numChar)
        {
            StringBuilder strBuild = new StringBuilder();
            int txtLength = strContent.Length;
            for (int i = 0; i <= (txtLength / numChar); i++)
            {
                string strTmp = strContent.Substring(i * numChar, ((i + 1) * numChar > txtLength ? txtLength - (i * numChar) : numChar));
                int start = strTmp.IndexOf(" ");
                if (start < 0)
                {
                    strTmp = strTmp + " ";
                }
                else
                {
                    int last = strTmp.LastIndexOf(" ");
                    if (last < (numChar / 2))//the space is too far with last character
                    {
                        strTmp = strTmp + " ";
                    }
                }
                strBuild.Append(strTmp);
            }
            return strBuild.ToString();
        }

        public static string RootURL()
        {
            string strURL = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath);
            if (strURL.LastIndexOf("/") == strURL.Length - 1)
                strURL = strURL.Substring(0, strURL.Length - 1);
            return strURL;
        }
        public static string HTMLEncode(string text)
        {
            return text.Replace("\r\n", "").Replace("'", "&rsquo;");
        }

        public static void UploadFileExcel(HttpContext context, string folderName, string path, ref int errcode, ref string fileName)
        {
            try
            {
                errcode = 0;
                string file = "";
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                    {
                        file = context.Request.Files[i].FileName;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(file))
                {
                    if (Directory.Exists(path + folderName) == false)
                    {
                        Directory.CreateDirectory(path + folderName);

                    }
                    string Extension = Path.GetExtension(file);
                    if (Extension.ToLower().Equals(".xls") == true || Extension.ToLower().Equals(".xlsx") == true)
                    {
                        for (int i = 0; i < context.Request.Files.Count; i++)
                        {
                            if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                            {
                                int nPos1 = file.LastIndexOf('\\');
                                int nPos2 = file.LastIndexOf('/');
                                fileName = path + folderName + "\\" + Guid.NewGuid().ToString() + Extension;
                                context.Request.Files[i].SaveAs(fileName);
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Extension fail
                        errcode = 2;
                    }
                }
                else
                {
                    // file is null
                    errcode = 1;
                }
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Split long string into many smaller lines DungTong@20090319
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nChars"></param>
        /// <returns></returns>
        public static string BreakLongString(string str, int nChars)
        {

            if (str.Length <= nChars)
            {
                return str;
            }
            StringBuilder text = new StringBuilder();
            int totalLines = str.Length / nChars;
            if (str.Length % nChars != 0)
                totalLines++;
            int i = 0;
            for (i = 0; i < totalLines - 1; i++)
            {
                text.Append(str.Substring(i * nChars, nChars));
                text.Append("\n\r");
            }
            text.Append(str.Substring(i * nChars, str.Length - i * nChars));

            return text.ToString();
        }

     
        public static string FormatNumber1(object number, bool isMoneyAmount)
        {
            var dcNumber = CastAide.AsDecimal(number);
            if (isMoneyAmount)
            {
                return NumberFormat.DecFormat(dcNumber, 0);
            }
            return NumberFormat.DecFormat(dcNumber, 0);
        }
        public static string FormatDate(object dtmDate)
        {
            var date = CastAide.AsDateTime(dtmDate);
            return date.ToString("MM/dd/yyyy HH:mm:ss");
        }
       
        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
                file.Delete();
        }

        public static void PostFile(string path, string file, HttpContext context)
        {
            // Get size of POST data
            int nPostSize = context.Request.TotalBytes;
            if (nPostSize == 0) return;

            byte[] bytesPostData;
            int nReadSize = 1024;
            FileStream fsData = new FileStream(file, FileMode.Create);
            BinaryWriter wData = new BinaryWriter(fsData);

            // Read POST data in 1K chunks
            int nBytesRead = 0;
            for (int i = 0; i < (nPostSize / nReadSize); i++)
            {
                bytesPostData = context.Request.BinaryRead(nReadSize);

                wData.Write(bytesPostData);
                nBytesRead = nBytesRead + nReadSize;
            }

            // Read remaining fraction of 1K
            nReadSize = nPostSize - nBytesRead;
            if (nReadSize != 0)
            {
                bytesPostData = context.Request.BinaryRead(nReadSize);
                nBytesRead = nBytesRead + nReadSize;
            }
            wData.Close();
            fsData.Close();
        }

        public static string GenerateSelectObj(string strID, ArrayList arrValue, ArrayList arrText, string strValueSelected)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<select id=\"" + strID + "\" name=\"" + strID + "\">");
            for (int i = 0; i < arrValue.Count; i++)
            {
                if (string.Compare(strValueSelected, arrValue[i].ToString()) == 0)
                    strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\" selected>" + arrText[i].ToString() + "</option>");
                else
                    strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\">" + arrText[i].ToString() + "</option>");
            }
            strBuild.Append("</select>");
            return strBuild.ToString();
        }

        public static string GenDropdownList(IList<string> listValue, IList<string> listText, string ddlName, bool isDisable, string firstOption, string selectedValue)
        {
            var strBuild = new StringBuilder();
            strBuild.Append("<select id=\"" + ddlName + "\" name= \"" + ddlName + "\" " + (isDisable ? "disabled" : string.Empty) + ">");
            if (!string.IsNullOrEmpty(firstOption))
                strBuild.Append("<option value=\"\">" + firstOption + "</option>");
            for (int i = 0; i < listValue.Count; i++)
            {
                if (string.Compare(listValue[i], selectedValue, true) == 0)
                    strBuild.Append("<option value=\"" + listValue[i] + "\" selected='selected'>" + listText[i] + "</option>");
                else
                    strBuild.Append("<option value=\"" + listValue[i] + "\">" + listText[i] + "</option>");
            }
            strBuild.Append("</select>");
            return strBuild.ToString();
        }
        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
        ///Johnny@20130221: Get suffix for sending mail template
        public static string EmailSuffix(bool MailOption)
        {
            return (MailOption ? "" : "_html");
        }

    }
}
