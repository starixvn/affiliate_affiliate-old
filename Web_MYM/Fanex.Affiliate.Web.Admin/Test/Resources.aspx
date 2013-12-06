<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resources.aspx.cs" Inherits="Fanex.Web.MYM.Admin.Test.Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Update Resource</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%=_langDropdown %>
            Key: <input id="key" name="key" value="<%=_key %>"/>
            Value: <input id="value" name="value" value="<%=_value %>" />
            <input type="submit" />
            <br />
            <br />
            <asp:DataGrid ID="gridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="875px">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Key">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container,"DataItem.Key") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Value" ItemStyle-Width="600">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container,"DataItem.Value") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="Small" Font-Names="verdana" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            </asp:DataGrid>
            <br />
        </div>
    </form>
</body>
</html>
