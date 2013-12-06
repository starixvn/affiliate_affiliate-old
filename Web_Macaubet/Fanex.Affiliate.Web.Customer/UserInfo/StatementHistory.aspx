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
                        
                        <td class="in_top">
                            <div class="title"><span><asp:Literal runat="server" ID="ltrStaHis"></asp:Literal></span></div>
                            <div class="title_detail"></div>
                        </td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
