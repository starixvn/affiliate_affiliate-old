<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="UserInfo_CustomersInfo" Codebehind="CustomersInfo.aspx.cs" %>
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function NavigatePage(curPage) {
            location.href = "CustomersInfo.aspx?page=" + curPage;
        }
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        
                        <td width="170" class="in_top"><%=strCaption %></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <%=strContent1 %>
            </div>
        </div>
    </div>
</asp:Content>
