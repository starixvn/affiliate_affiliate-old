<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="PaymentRequest" Codebehind="PaymentRequest.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        RefreshPage(300);
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        
                        <td width="155" class="in_top"><%=Fanex.Affiliate.Resources.Translator.Get("PaymentRequest") %></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">                                
                <form id="frUpdate" name="frUpdate" method="post" onsubmit="return RequestPayout();" target="frUpdateData" action="../Handler/RequestPayout.ashx">
                    <div class="box_conts_header">
                        <%=Fanex.Affiliate.Resources.Translator.Get("PaymentInfor") %>
                    </div>
                    <div class="box_conts_line"></div>
                    <br />
                    <div id="errMsg" style="display: none;" class="msg-wid">
                        <div id="divErr">
                            <table  width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 40px;" valign="top">
                                        <div class="warning">
                                            &nbsp;</div>
                                    </td>
                                    <td valign="top">
                                        <div id="error_item">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="succMsg" style="display: none;" class="msg-wid">
                        <div id="divSuccess">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td style="width: 40px;" valign="top">
                                        <div class="icsuccess">
                                            &nbsp;</div>
                                    </td>
                                    <td valign="top">
                                        <ul>
                                            <li><span class="success" id="succ_item">
                                                <%=Fanex.Affiliate.Resources.Translator.Get("RequestPayoutSucc") %></span></li>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <table width="100%" cellspacing="0" cellpadding="3">
                       
                        <tr>
                            <td style="width: 185px;">
                                <%=Fanex.Affiliate.Resources.Translator.Get("BalForward") %>
                            </td>
                            <td id="spBalance" class="greater0 r" style="width: 130px;">
                                <%=strBalance%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=Fanex.Affiliate.Resources.Translator.Get("MinWithdraw")%>
                            </td>
                            <td id="spMinAmt" class="greater0 r">
                                <%=strMinAmt%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td><%=Fanex.Affiliate.Resources.Translator.Get("RequestAmt") %></td>
                            <td class="r"><input type="text" id="txtAmt" name="txtAmt" class="inputbox" style="width: 90px; text-align: right;" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="c">
                                <input type="submit" class="button" id="btnRequest" name="btnRequest" value="<%=Fanex.Affiliate.Resources.Translator.Get("RequestPayout") %>" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    
                    <input type="hidden" id="hsBalance" value="<%=strBalance%>" />                    
                    <input type="hidden" id="hsMinAmt" value="<%=strMinAmt%>" />
                    <input type="hidden" id="hdBalance" name="hdBalance" value="<%=dBalance%>" />                    
                    <input type="hidden" id="hdMinAmt" name="hdMinAmt" value="<%=dMinAmt%>" />
                    <input type="hidden" id="hdMethod" name="hdMethod" value="<%=strPaymentMethod%>" />
                </form>
                <br />
                <div id="divListRequest">
                    <asp:Literal ID="ltrListRequest" runat="server"></asp:Literal>
                </div>
                <iframe id="frUpdateData" name="frUpdateData" class="hidden" style="height: 0px;
                    width: 0px; display: none;"></iframe>
            </div>
        </div>
    </div>
        <script type="text/javascript">
        window.onload = function() {
            if ($("hdMethod").value != "Player") {
                $("frUpdate").disable();
                $("btnRequest").className = "disabled";
                AddError("error_item", $("hdMethod").value);
                $('errMsg').show();
                ShowErrorNotLink(arrError, arrItem, "error_item");
                return;
            }
            var Balance = parseFloat($("hdBalance").value);
            var MinAmt = parseFloat($("hdMinAmt").value);
            
            if (Balance <= 0 || Balance < MinAmt) {
                $("frUpdate").disable();
                $("btnRequest").className = "disabled";
                AddError("error_item", "<%=Fanex.Affiliate.Resources.Translator.Get("Balance0") %>");
                DisplayMsg(false);
            }
            else
                $("frUpdate").enable();
                
        };
        function ShowError1(msg) {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
            AddError("error_item", msg);
            $('errMsg').show();
            ShowErrorNotLink(arrError, arrItem, "error_item");
        }
        function RequestPayout() {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
            if ($("isPending").value == "1") {
                AddError("error_item", "<%=Fanex.Affiliate.Resources.Translator.Get("Err3001") %>");
                DisplayMsg(false);
                return false;
            }
            $("txtAmt").value = $("txtAmt").value.replace(/,/g,"");
            if (!$("txtAmt").value.isNumber()) {
                AddError("txtAmt", "<%=Fanex.Affiliate.Resources.Translator.Get("AmtErr") %> " + $("hsMinAmt").value + " <%=Fanex.Affiliate.Resources.Translator.Get("And") %> " + $("hsBalance").value);
                DisplayMsg(false);
                return false;
            }
            var amtRequest = parseFloat($("txtAmt").value);
            var amtBalance = parseFloat($("hdBalance").value);
            var MinAmt = parseFloat($("hdMinAmt").value);
            
            if(amtBalance < 0){
                AddError("txtAmt", "<%=Fanex.Affiliate.Resources.Translator.Get("ErrBalanceNegative")%>");                
            }
            if (amtRequest > amtBalance) {                
                AddError("txtAmt", "<%=Fanex.Affiliate.Resources.Translator.Get("ErrBalanceForward")%>");
                
            }
            if (MinAmt > amtRequest) {
                if (arrItem.length == 0)
                    AddError("txtAmt", "<%=Fanex.Affiliate.Resources.Translator.Get("ErrMinAmt") %>");
                else
                    AddError("error_item", "<%=Fanex.Affiliate.Resources.Translator.Get("ErrMinAmt") %>");
            }
            if (arrItem.length > 0 || arrError.length > 0) {
                DisplayMsg(false);
                return false;
            }
            return true;
        }
        function cancelRequest() {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
        }
        //var tblink = null;
        function UpdateSuccess(Msg, balance, minAmt,sBalance,sMinamt, listPayment) {
            AddError("error_item", Msg);
            $("succ_item").innerHTML = Msg;
            DisplayMsg(true);
            $("spBalance").innerHTML = sBalance;
            $("hdBalance").value = balance;
            $("hsBalance").value = sBalance;
            $("spMinAmt").innerHTML = sMinamt;
            $("hdMinAmt").value = minAmt;
            $("hsMinAmt").value = sMinamt;
            $("txtAmt").value = "";
            $("divListRequest").innerHTML = listPayment;
            //tblink = setTimeout("ClearBlink();", 5000);
        }
        function UpdateErr(Msg) {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
            AddError("error_item", Msg);
            parent.DisplayMsg(false);
        }
    </script>

</asp:Content>
