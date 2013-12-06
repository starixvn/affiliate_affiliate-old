namespace Fanex.Web.Bwinning.Customer.Authorization
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Utils;
    using System.Drawing;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;


    public class LoginCaptcha : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            string _code = Encryption.RandomNumberString(4).ToUpperInvariant();
            AppSession.CaptchaCode = _code;
            CreateImage(_code, context);
        }

        private void CreateImage(string _code, HttpContext hc)
        {
            int iwidth = (int)(_code.Length * 10);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.White);
            g.Clear(Color.FromArgb(91, 63, 24));
            g.DrawString(_code, f, b, 2, 2);

            Pen blackPen = new Pen(Color.Black, 0);
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            for (int i = 0; i < 5; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            hc.Response.ClearContent();
            hc.Response.ContentType = "image/Jpeg";

            hc.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}