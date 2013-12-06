<%@ Page Language="C#" AutoEventWireup="true" Inherits="DepMemberDetailUpd" Codebehind="DepMemberDetailUpd.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Update 12Bet player status for affiliate</title>
    <script type="text/javascript" src="App_Js/prototype.js"></script>
    <script type="text/javascript" src="App_Js/common.js"></script>
</head>
<body>
    <form id="form1" method="get" onsubmit="return CheckForm();" target="frmData">
    <div>
        <table cellpadding="3" cellspacing="0" style="width: 300px;">
            <tr>
                <td colspan="3" align="center" style="font-size: 15px; font-weight: bold; color: Blue;">Update 12Bet player status</td>
            </tr>
            <tr>
                <td colspan="3" id="tdNote">Fields marked <span style="color: Red;">*</span> must be completed</td>
            </tr>
            <tr>
                <td>Deposit Username</td>
                <td style="color: Red; width: 15px;">*</td>
                <td><input type="text" id="txtDepUsername" name="txtDepUsername" value="<%=DepositUsername %>" /></td>
            </tr>
            <tr>
                <td>IBCCustID</td>
                <td style="color: Red;">*</td>
                <td><input type="text" id="txtIBCCustId" name="txtIBCCustId" value="<%=IBCCustId.ToString() %>" /></td>
            </tr>
            <tr>
                <td>IBCUsername</td>
                <td style="color: Red;">*</td>
                <td><input type="text" id="txtIBCUsername" name="txtIBCUsername" value="<%=IBCUsername %>" /></td>
            </tr>
            <tr>
                <td>AffId</td>
                <td style="color: Red;">*</td>
                <td><input type="text" id="txtAffId" name="txtAffId" value="<%=AffId.ToString() %>" /></td>
            </tr>
            <tr>
                <td colspan="3" style="color: Red;"><div id="divErr"></div></td>
            </tr>
            <tr>
                <td colspan="3" align="center"><input type="submit" value="Submit" /></td>
            </tr>
        </table>
    </div>
    </form>
    <iframe id="frmData" name="frmData" style="width: 0px; height: 0px; display: none;"></iframe>
    <script type="text/javascript">
        function CheckForm()
        {
            if(isEmpty($("txtDepUsername").value) || isEmpty($("txtIBCCustId").value) || isEmpty($("txtIBCUsername").value) || isEmpty($("txtAffId").value))
            {
                alert("Fields marked * must be completed");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
