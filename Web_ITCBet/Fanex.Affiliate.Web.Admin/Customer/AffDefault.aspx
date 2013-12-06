<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.ITCBet.Admin.Customer.Default" Codebehind="AffDefault.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Affiliate Admin</title>
    <script type="text/javascript" src="../App_Js/Common.js"></script>
    <script type="text/javascript" src="../App_Js/prototype.js"></script>
     <script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
</head>
    <frameset  cols="266,*" frameborder="no" framespacing="0" name="fCustomer" id="fCustomer">
        <frame id="fInfo" name="fInfo" frameborder="0" noresize="noresize" scrolling="auto" src="AffiliateInfo.aspx?username=<%=strUsername%>&pageid=<%=Pageid %>" />
        <frameset rows="175,*" frameborder="no" framespacing="0" id="fRightContent" name="fRightContent">
            <frame id="fStatistics" name="fStatistics" noresize="noresize" frameborder="0" scrolling="no" src="" />        
            <frame id="fContent" name="fContent" noresize="noresize" frameborder="0" scrolling="auto" src="" />        
        </frameset>
        <noframes>
            <body><h3><%=Fanex.Affiliate.Resources.Admin.NotHandleFrames %></h3></body>
        </noframes>
    </frameset>
</html>
