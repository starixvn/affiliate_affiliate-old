<%@ Page Language="C#" AutoEventWireup="true" Inherits="Footer" Codebehind="Footer.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html, body
        {
        	margin: 0px;
        	padding: 0px;
        }
        #footer{
            background:transparent url(App_Themes/DefaultV2/images/bg_footer.jpg) repeat-x scroll center top;
            height:18px;
            margin-top:0;
            padding:5px 15px;
            text-align:right;
            font-size: 11px;
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
</head>
<body>
    <div id="footer"><%=Fanex.Affiliate.Resources.TextExpressionBuilder.GetEvalData("Copyright").Replace("{year}", DateTime.Now.Year.ToString()) %></div>
</body>
</html>
