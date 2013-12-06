<%@ Control Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.LiveBet365.Affiliate.Controls_Footer" Codebehind="Footer.ascx.cs" %>

<script type="text/JavaScript">
    function MM_openBrWindow(theURL,winName,features) { //v2.0
      window.open(theURL,winName,features);
    }

    function MM_goToURL() { //v3.0
      var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
      for (i=0; i<(args.length-1); i+=2) eval(args[i]+".location='"+args[i+1]+"'");
    }
</script>

<div class="browser">&nbsp;</div>
<div class="copyright"><%= Fanex.Affiliate.Core.Customer.AffCommon.GetCopyRight()%></div>
<div class="button_footer"><asp:HyperLink runat="server" CssClass="f_button" ID="hlkProgram" NavigateUrl="~/Root.aspx"></asp:HyperLink> | <asp:HyperLink runat="server" CssClass="f_button" ID="hlkCondition" NavigateUrl="~/Condition.aspx"></asp:HyperLink></div>
<script type="text/javascript">
    <%=strMsg %>
</script>