<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceApi.aspx.cs" Inherits="Fanex.Web.i668bet.Admin.Test.ServiceApi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=site %></title>
    <script src="../App_js/jquery-1.8.3.min.js"></script>
    <script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js?lang=xml&skin=sunburst""></script>
</head>
<body>
    
    <form id="form1" runat="server">
    
        
        <table>

        <tr>
        <td>Method:
        </td>
        <td><select id="method" name="method">
            <option value="GetRegList">GetRegList</option>
            <option value="UpdateBtage">UpdateBtage</option>
            <option value="GetLicenseeCustInfo">GetLicenseeCustInfo</option>
            <option value="Transfer">Transfer</option>            
        </select></td>
        </tr>
        
        <tr id="GetRegList" class="tab">               
            <td>Date: </td><td><input type="date" name="date" value="<%=strDate %>" />        </td>
        </tr>
        <tr id="UpdateBtage" class="tab">            
            <td>Licensee: </td><td><input type="text" name="licensee" value="<%=licensee %>" /></td>        
            <td>New Btag: </td><td><input type="text" name="btag" value="<%=btag %>" /></td>
        </tr>
        <tr id="GetLicenseeCustInfo" class="tab">            
            <td>Licensee: </td><td><input type="text" name="licensee" value="<%=licensee %>" />       </td>     
        </tr>
        <tr id="Transfer" class="tab">            
            <td>Licensee: </td><td><input type="text" name="licensee" value="<%=licensee %>" />       </td>
            <td>Amount: </td><td><input type="number" name="amount" value="<%=amount %>" />       </td>
            <td>Dummy Account: </td><td><input type="number" name="dummyacount" value="<%=dummyacount %>" />  </td>
        </tr>
        </table>
        <input type="submit" />
    
    </form>
    <pre class="prettyprint"><%=responseText %></pre>
    <script>
        $(function () {

            $("#method").change(function () {
                $("#form1 tr.tab").hide().find("input").attr("disabled", true);
                $("#form1 tr.tab[id=" + $(this).val() + "]").show().find("input").removeAttr("disabled");
            });
            $("#method").val("<%=method %>").change();
        });
    </script>
</body>
</html>
