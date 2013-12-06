<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserControl_EditGroupPolicy" Codebehind="EditGroupPolicy.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_js/prototype.js"></script>
<script type="text/javascript" src="../App_js/Common.js"></script>
<script type="text/javascript" src="../App_js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript" src="../App_js/UserControl.js"></script>
<script type="text/javascript">
    window.onload = Init;

    function Init() {
        /*Initialize div check box*/
        listChk = $$("div.chk");
        if (listChk.length > 0) {
            for (var i = 0; i < listChk.length; i++) {
                listChk[i].observe('click', chkOnClick);
                /*set value for hidden field according to chk div class*/
                var hdID = "hd" + listChk[i].id;
                if (listChk[i].hasClassName("ic-check")) {
                    $(hdID).value = true;
                }
                else {
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
            var num_sub = element.id.split("_")[2];
            if (element.id.indexOf("chkR") >= 0) {
                OnCheckRead(funcid,num_sub);
            }
            /*Host permision checkbox*/
            else if (element.id.indexOf("chkH") >= 0) {
                OnCheckHost(funcid,num_sub);
            }
        }
    }
</script>
<!--/js-->
