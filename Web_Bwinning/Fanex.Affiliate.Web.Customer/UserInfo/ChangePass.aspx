<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="ChangePass" Codebehind="ChangePass.aspx.cs" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript" src="<%=AppSettings.FullDomainName%>/App_Js/wz_tooltip.js?version=<%=AppSettings.JsVersion%>"></script>

    <script type="text/javascript">
        function submitForm() {
            arrError.clear();
            arrItem.clear();
            $("errMsg").hide();
            if (isEmpty($('txtCurrPass').value))
                AddError("txtCurrPass", ErrMsg.OldPass);
            if (isEmpty($('txtNewPass').value))
                AddError("txtNewPass", ErrMsg.NewPass);
            else if ($('txtConfirmPass').value != $('txtNewPass').value)
                AddError("txtConfirmPass", ErrMsg.RePass);
            if (arrItem.length > 0) {
                ShowError(arrError, arrItem, "error_item");
                $("errMsg").show();
                return false;
            }
            if (!checkpass($("txtNewPass"), "td_pass") || !checkpass($("txtConfirmPass"), "td_confirm")) {
                AddError("txtNewPass", $("hdPassCheck").value);
                ShowError(arrError, arrItem, "error_item");
                $('errMsg').show();
                return false;
            }
            return true;
            //$("Update_form").submit();
        }
        function check(str) {
            numbers = new String("0123456789");
            var i = 0;
            var nCount = 0;
            while (i < str.length) {
                (numbers.indexOf(str.substring(i, i + 1)) >= 0) ? nCount++ : false;
                i++;
            }
            return ((nCount > (str.length - 2)) || nCount < 2) ? false : true;
        }
        function createCheck(id) {
            t = document.createElement("img");
            t.setAttribute("id", id);
            t.width = 15; t.height = 11; t.border = 0;
            t.src = "<%=AppSettings.FullDomainName%>/<%= Fanex.Affiliate.Core.AppSettings.ThemeDir %>/Images/1.gif?version=<%=AppSettings.JsVersion%>";
            return t;
        }

        function checkpass(obj, tdid) {
            if (!tdid) tdid = 'td_pass';
            var bol = false;
            var pass = obj.value;
            var t = $("pass_" + tdid);
            var td = $(tdid);
            if (pass.length < 6) {
                if (!t) {
                    t = createCheck("pass_" + tdid);
                    td.appendChild(t);
                }
                else {
                    t.src = "<%=AppSettings.FullDomainName%>/<%= Fanex.Affiliate.Core.AppSettings.ThemeDir %>/Images/1.gif?version=<%=AppSettings.JsVersion%>";
                    t.title = $("hdPassCheck").value;
                }
            }
            else {
                bol = check(pass);
                if (bol) {
                    if (!t) {
                        td = $(tdid);
                        t = createCheck("pass_" + tdid);
                        td.appendChild(t);
                    }
                    t.src = "<%=AppSettings.FullDomainName%>/<%= Fanex.Affiliate.Core.AppSettings.ThemeDir %>/Images/2.gif?version=<%=AppSettings.JsVersion%>";
                    t.title = "";
                }
            }
            return bol;
        }
    </script>

    <form id="Update_form" name="Update_form" onsubmit="return submitForm();" action="../Handler/ChangePass.ashx"
    method="post" target="frChangePass">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td width="144" class="in_top"><%=Fanex.Affiliate.Resources.Translator.Get("ChangePass") %></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <div id="errMsg" style="display: none; width:450px" class="msg-wid">
                    <div id="divErr">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 40px;" valign="top">
                                    <div class="warning">
                                        &nbsp;
                                    </div>
                                </td>
                                <td valign="top">
                                    <div id="error_item">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table cellpadding="2" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="dotted_line">
                            <asp:Literal runat="server" ID="ltrTextRemind"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                            <table>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Literal ID="ltrCurrPass" runat="server"></asp:Literal>
                                    </td>
                                    <td class="cont12oe" style="width: 11px">
                                        *
                                    </td>
                                    <td style="width: 160px;">
                                        <input name="txtCurrPass" style="width: 150px;" class="inputbox" id="txtCurrPass"
                                            value="" size="30" type="password" />
                                    </td>
                                    <td class="cont12oe" style="width: 30px">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltrNewPass" runat="server"></asp:Literal>
                                    </td>
                                    <td class="cont12oe">
                                        *
                                    </td>
                                    <td>
                                        <input name="txtNewPass" class="inputbox" style="width: 150px;" id="txtNewPass" value=""
                                            size="30" onkeyup="return checkpass(this,'td_pass');" type="password" onmouseover="Tip($('hdPassCheck').value.replace(',', ',<br />'), BGCOLOR, '#eeeeee', FONTCOLOR, '#da3614', SHADOW, true, SHADOWCOLOR, '#530208', DELAY, 10);" />
                                    </td>
                                    <td id="td_pass" style="vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltrComfimPass" runat="server"></asp:Literal>
                                    </td>
                                    <td class="cont12oe">
                                        *
                                    </td>
                                    <td>
                                        <input name="txtConfirmPass" style="width: 150px;" class="inputbox" id="txtConfirmPass"
                                            value="" size="30" onkeyup="return checkpass(this,'td_confirm');" type="password" />
                                    </td>
                                    <td id="td_confirm" style="vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="c">
                                        <input name="btnSubmit" id="btnSubmit" type="submit" class="button" value="<%=Fanex.Affiliate.Resources.Translator.Get("Submit") %>" />
                                        <input name="btnCancel" id="btnCancel" value="<%=Fanex.Affiliate.Resources.Translator.Get("Cancel") %>" class="button" type="button" onclick="javascript:history.back();" />
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <input type="hidden" value="<%=Fanex.Affiliate.Resources.Translator.Get("PassCheck") %>" id="hdPassCheck" name="hdPassCheck" />
    </form>
    <iframe id="frChangePass" name="frChangePass" class="hide" style="height: 0px; width: 0px;"></iframe>
</asp:Content>
