<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="Fanex.Web.MYM.Customer.UserInfo.Account_Overview" Codebehind="Account_Overview.aspx.cs" %>
  <%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<script type="text/javascript" language="javascript" src="<%=AppSettings.FullDomainName%>/App_Js/swfobject.js?version=<%=AppSettings.JsVersion%>"></script>
<script type="text/javascript" language="javascript" >
    window.onload = Init;
    function Init() {
//        var p = document.getElementById("pop_up");
//        var dontshow = getCookie("dontshow" + "$" + $("custid").value);
//        if (dontshow == "true")
//            p.style.display = "none";
//        else
//            p.style.display = document.getElementById("showpopup").value;
    }

    function HidePopup() {
        var p = document.getElementById("pop_up");
        p.style.display = "none";
        //set cookie according to checkbox status
    }

    function SetPopupShow() {
        if($("dontshow").checked)
            setCookie("dontshow" + "$" + $("custid").value, "true", 30);
        else
            setCookie("dontshow" + "$" + $("custid").value, "false", 30);
    }
    function ChangeProduct(spId, selId, isSelDate) {
        var select = $(selId);
        if (select.options.length == 0)
            return;
        if (isSelDate) {
            $(spId).hide();
            select.show();
            select.value = $(spId).innerHTML;
            select.focus();

        }
        else {
            $(spId).show();
            select.hide();
            $("totalqualifiedmembers").innerHTML = select.options[select.selectedIndex].getAttribute("data-totalqualifiedmembers"); ;
            $(spId).innerHTML = select.options[select.selectedIndex].text;
        }


    }
</script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top">
                            <asp:Literal runat="server" ID="ltrAccOverview"></asp:Literal>
                        </td>                        
                    </tr>
                </table>
            </div>
            <br />
            <form action="" id="Register_form" name="Register_form" method="get">
            <div class="in_conts">
                <div class="box_conts_header">
                    <div class="flright">
                        <%= slMonth%>&nbsp;&nbsp;
                       <%= slYear %>
                        &nbsp;&nbsp;<input value="<%=Fanex.Affiliate.Resources.Translator.Get("Submit") %>" class="button" type="submit" />&nbsp;
                    </div>
                    <asp:Literal runat="server" ID="ltrStatic"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="5" style="margin-bottom: 5px">
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td style="width: 250px;">
                            <asp:Literal runat="server" ID="ltrNewSignup"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal runat="server" ID="ltrSignup"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTotalActive"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalActiveVal"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                         <td style="white-space: nowrap;">
                            <%=Fanex.Affiliate.Resources.Translator.Get("TotalQualifiedMembers")%> (<a href="javascript:void(0)" id="spProduct" onclick="ChangeProduct('spProduct', 'slProduct', true);return false;"><%=strProduct%></a><select id="slProduct" name="slProduct" style="display:none;"
                            onblur="ChangeProduct('spProduct', 'slProduct', false);" 
                            onchange="ChangeProduct('spProduct', 'slProduct', false);">
                                <%= slProduct%></select>):</td>
                        <td class="r" id="totalqualifiedmembers"><%= strTotalqualifedmembers%></td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td >
                            <asp:Literal runat="server" ID="ltrTotalDeposit"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalDepositVal"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <%=Fanex.Affiliate.Resources.Translator.Get("Turnover") %>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTurnoverMonth"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrStaticUp"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="5" style="margin-bottom: 5px">
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td style="width: 250px;">
                            <asp:Literal runat="server" ID="ltrTotalPlay"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal ID="ltrTotalPlayer" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTurnover"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal ID="ltrTurnoverVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <asp:Literal runat="server" ID="ltrDepositUp"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal ID="ltrDepositUpVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td style="width: 190px;">
                            <asp:Literal runat="server" ID="BalanceBroughtText"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal ID="BalanceBroughtVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrComm"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <div class="tbl-box">
                    <%=strTierSetting %>
                </div>
            </div>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        $("slMonth").value = "<%=strMonths%>";
        $("slYear").value = "<%=strYears%>";
    </script>

    <%--<div id="pop_up" style="display:none">
        <div class="top">
            <div class="Ltop">
            </div>
            <div class="Rtop">
            </div>
            <div class="Ctop">
                <h1>
                    <img id="close" " " src="../App_Themes/Default/Images/close.png" onclick="HidePopup()" /></h1>
            </div>
        </div>
        <div id="fl" class="flash">
        </div>
        <div class="bottom">
            <input name="dontshow" id="dontshow" type="checkbox" value="" onclick="SetPopupShow()"/>
            <%=Fanex.Affiliate.Resources.Translator.Get("Dontshowit")%>
        </div>
    </div>--%>
    <input type="hidden" name="flashpath" id="flashpath" value="<%=flashPath %>" />
    <input type="hidden" name="showpopup" id="showpopup" value="<%=showPopup %>" />
    <input type="hidden" name="custid" id="custid" value="<%=Fanex.Affiliate.Core.AppSession.CustID %>" />
    <%--<script type="text/javascript">
        var so = new SWFObject(""+$("flashpath").value, "flCommission", "420", "270", "9", "#000000");
        so.addVariable("afflink", "../ProgramDetails.aspx");
        so.write("fl");
    </script>
--%>
</asp:Content>
