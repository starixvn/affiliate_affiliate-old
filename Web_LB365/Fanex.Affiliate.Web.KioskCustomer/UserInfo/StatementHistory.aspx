<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="UserInfo_StatementHistory" Codebehind="StatementHistory.aspx.cs" %>
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function NavigatePage(curPage) {
            location.href = "StatementHistory.aspx?page=" + curPage;
        }
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        
                        <td width="170" class="in_top"><asp:Literal runat="server" ID="ltrStaHis"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
