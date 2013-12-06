<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Fanex.Web.MYM.Customer.Recoverpw" Codebehind="Recoverpw.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        
                        <td class="in_top"><%=forgetpassword%></td>                        
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

