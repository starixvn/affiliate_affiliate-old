<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomainControlIns.aspx.cs" Inherits="Fanex.Web.Aoncash.Admin.Domain.Domain_DomainControlIns" %>

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

    function AddDomainControl() {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();

        DelayReloadPage1();

        var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
            method: 'post',
            parameters: {
                        country: $("slCountry").value,
                        domainAff: $("slAffDomain").value,
                        domainMember: $("slMemberDomain").value,
                        status: $("chkStatus").hasClassName("ic-check"),
                        action: "ins" 
                        },
            onComplete: function (res) {
                if (res.responseText == "1") {
                    $("errMsg").className = "done";
                    arrError.push("Insert Successfully!");
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