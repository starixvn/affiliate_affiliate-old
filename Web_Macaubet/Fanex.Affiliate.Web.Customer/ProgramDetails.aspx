<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" 
    CodeBehind="ProgramDetails.aspx.cs" Inherits="Fanex.Web.Macaubet.Customer.ProgramDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top">
                            <div class="title">
                                <%=Fanex.Affiliate.Resources.Translator.Get("ProgDetails") %></div>
                            <div class="title_detail">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="in_conts">
                <%=Fanex.Affiliate.Resources.Translator.Get("ProgramDetails", "AffiliateContent")%>
            </div>
        </div>
    </div>    
    
</asp:Content>

