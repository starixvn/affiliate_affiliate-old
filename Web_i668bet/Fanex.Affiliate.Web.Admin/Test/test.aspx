<%@ Page Language="C#" AutoEventWireup="true" Inherits="test" Codebehind="test.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        function Submit(type)
        {
            var form = document.getElementById('frm');
            document.getElementById('type').value = type;
            form.submit();
        }
       
    </script>
</head>
<body>        
    <form id="frm"  method="post" action="Test.aspx">
    <table>        
        <tr>
            <td>FirstName</td>
            <td><input type="text" id="txtFirstName" name="txtFirstName" value="Tong" /></td>
        </tr>
        <tr>
            <td>LastName</td>
            <td><input type="text" id="txtLastName" name="txtLastName" value="Hoang Dung" /></td>
        </tr>
        <tr>
            <td>Your Mail</td>
            <td><input type="text" id="txtEmail" name="txtEmail" value="hoangdungtong@hotmail.com" /></td>
        </tr>
        <tr>
            <td>User Name</td>
            <td><input type="text" id="txtUserName" name="txtUserName" value="12BETAFF001" /></td>
        </tr>
        <tr>
            <td>Language</td>
            <td>
                <select id="lang" name="lang">
                    <option value="en-US">English</option>
                    <option value="zh-CN">Simplified</option>
                    <option value="zh-TW">Traditional</option>
                    <option value="th-TH">Thai</option>
                    <option value="es-ES">Espanol</option>
                    <option value="vi-VN">Vietnamese</option>
                    <option value="id-ID">Indonesian</option>
                </select>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <input type="button" value="Payment Approved" onclick="Submit('1');" />
                <input type="button" value="Payment Rejected" onclick="Submit('2');" />
                <input type="button" value="Approved" onclick="Submit('3');" />
                <input type="button" value="Disapproved" onclick="Submit('4');" />
                <input type="hidden" id="type" name="type" value="Approve Email" />
             </td>            
        </tr>
    </table>
    </form>
</body>
</html>
