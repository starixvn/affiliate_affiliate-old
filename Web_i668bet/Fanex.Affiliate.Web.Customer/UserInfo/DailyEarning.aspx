<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" Inherits="DailyEarning"
    CodeBehind="DailyEarning.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top" width="144">
                            <asp:Literal runat="server" ID="ltrDailyEarning"></asp:Literal>
                        </td>
                        <td class="in_right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="in_conts">
                <form method="get" id="frmData" name="frmData" action="">
                <%=strListProducts %>
                <div class="clear">
                </div>
                <br />
                <input type="hidden" id="monthdate" name="monthdate" value="<%=Request["monthdate"] %>" />
                </form>
                <asp:Literal runat="server" ID="ltrViewDaily"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
