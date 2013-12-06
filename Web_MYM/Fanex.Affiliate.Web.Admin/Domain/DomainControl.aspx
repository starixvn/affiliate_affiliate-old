<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomainControl.aspx.cs" Inherits="Fanex.Web.MYM.Admin.Domain.Domain_DomainControl" %>

<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    function CloseDiv() {
        ibcWnd.close();
    }
    function AddDomainControl() {
        DoCal("DomainControlIns.aspx", "Add New Domain Control", 450, 250);
    }
    function DomainControlDel(id) {
        if (confirm(top.fMenu.Msg.ConfirmDelDomainSetting)) {
            DelayReloadPage1();
            var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
                method: 'post',
                parameters: {
                    id: id,
                    action: "del"
                },
                onComplete: function (res) {
                    if (res.responseText == "1") {
                        location.href = location.href;
                        //alert("Delete Successfully!");
                        //setTimeout("location.href=location.href;", 3000);
                    }
                    else {
                        ErrAlter(res.responseText);
                        //HideDelayReloadPage();
                    }
                }
            });
        }
    }
</script>
<!--/js-->