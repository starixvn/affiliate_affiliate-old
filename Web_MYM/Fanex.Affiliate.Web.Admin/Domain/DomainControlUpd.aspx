<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomainControlUpd.aspx.cs" Inherits="Fanex.Web.MYM.Admin.Domain.Domain_DomainControlUpd" %>

<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();

    window.onload = InitEvent;
    function InitEvent() {
        CheckBox_AddEvent();
    }

    function UpdDomainControl() {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();

        DelayReloadPage1();
        var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
            method: 'post',
            parameters: {
                id: $("id").value,
                country: $("slCountry").value,
                domainAff: $("slAffDomain").value,
                domainMember: $("slMemberDomain").value,
                status: $("chkStatus").hasClassName("ic-check"),
                action: "upd"
            },
            onComplete: function (res) {
                if (res.responseText == "1") {
                    $("errMsg").className = "done";
                    arrError.push(top.fMenu.Msg.UpdateSucc);
                    arrItem.push("errMsg");
                    $("errMsg").show();
                    ShowError(arrError, arrItem, "error_item");
                    setTimeout("DelayReloadPage1();parent.ibcWnd.close();parent.location.href=parent.location.href;", 3000);
                }
                else {
                    arrError.push(res.responseText);
                    arrItem.push("slCountry");
                    $('errMsg').show();
                    ShowError(arrError, arrItem, "error_item");
                    HideDelayReloadPage();
                }
            }
        });
        return false;
    }
</script>
<!--/js-->