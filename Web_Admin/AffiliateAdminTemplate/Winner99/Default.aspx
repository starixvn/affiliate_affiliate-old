<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Winner99 Affiliate Admin</title>
    <link runat="server" id="IconICO" rel="shortcut icon"/>
</head>

<frameset rows="60, *, 25" frameborder="no" framespacing="0">
    <frame id="fMenu" name="fMenu" frameborder="0" noresize="noresize" scrolling="no" src="Header.aspx" height="60px" />
    <frame id="fMain" name="fMain" noresize="noresize" frameborder="0" scrolling="auto" />        
    <frame id="fFooter" name="fFooter" frameborder="0" noresize="noresize" scrolling="no" src="Footer.aspx" height="20px" />
    <noframes>
        <body><h3><%=Fanex.Affiliate.Resources.Admin.NotHandleFrames %></h3></body>
    </noframes>
</frameset>
</html>
