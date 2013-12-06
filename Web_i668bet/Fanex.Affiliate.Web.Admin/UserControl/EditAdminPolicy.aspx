<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserControl_EditAdminPolicy" Codebehind="EditAdminPolicy.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src="../App_Js/UserControl.js" language="javascript" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    window.onload = Init;

    function Init() {
        /*Initialize div check box*/
        listChk = $$("div.chk");
        if (listChk.length > 0) {
            for (var i = 0; i < listChk.length; i++) {
                listChk[i].observe('click', chkOnClick);
                /*set value for hidden field according to chk div class*/
                var hdID = "hd" + listChk[i].id;
                if(listChk[i].hasClassName("ic-check")){
                    $(hdID).value = true;
                }
                else{
                    $(hdID).value = false;
                }
            }
        }
    }

    function chkOnClick(event) {
        var element = Event.element(event);
        if (element != null) {
            changeDivChkState(element);
            /*change the hidden field dedicated to this checkbox accordingly*/
            var hdID = "hd" + element.id;
            if (element.hasClassName("ic-check")) {
                $(hdID).value = true;
            }
            else {
                $(hdID).value = false;
            }
            /*Read permision checkbox*/
            var funcid = element.id.split("_")[1];
            var num_sub = element.id.split("_")[0];
            if (element.id.indexOf("chkR") >= 0) {
                OnCheckRead(funcid, num_sub);
            }
            /*Host permision checkbox*/
            else if (element.id.indexOf("chkH") >= 0) {
                OnCheckHost(funcid, num_sub);
            }
        }
    }
</script>
<!--/js-->