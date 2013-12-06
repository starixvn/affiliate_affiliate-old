<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Fanex.Web.i668bet.Customer.Recoverpw"
    CodeBehind="Recoverpw.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top" width="170">
                            <%=forgetpassword%>
                        </td>
                        <td class="in_right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="in_conts">
                <div class="text">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
            </div>
        </div>
    </div>
</asp:Content>
