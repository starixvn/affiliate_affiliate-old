<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserControl_ListUserInDept" Codebehind="ListUserInDept.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_js/prototype.js"></script>
<script type="text/javascript" src="../App_js/Common.js"></script>
<script type="text/javascript" src="../App_js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_js/UserControl.js"></script>
<script type="text/javascript" src="../App_js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    window.onload = function() { Init(); };

    window.onunload = function() { UnLoad(); };

    function UnLoad() {      
        if($("isReload").value != "0")
        {            
            parent.ReloadPage();
        }
    }

    function Init() {
        if ($("totalAdmin").value > 0) {
            /*assign handler for div checkbox*/
            listChk = $$(".chk");
            if (listChk.length > 0) {
                for (var i = 0; i < listChk.length; i++) {
                    listChk[i].observe('click', chkOnClick);
                }
            }
        }
        else {
            $("btnUpdateTop").style.display = "none";
            $("header_01_top").style.display = "none";
            $("header_01_bottom").style.display = "none";
            $("btnUpdateBottom").style.display = "none";
        }
        $('alp').value = $('fletter').value;
    }

    function chkOnClick(event) {
        var element = Event.element(event);   
        changeDivChkState(element);
        /*status checkbox*/
        if (element.id.indexOf(",") > 0) {
                var userid = element.id.split(",")[0];
                var status = element.id.split(",")[1];

                if (!confirm($("ChangeStatusMess").value)) {
                    changeDivChkState(element);
                    return;
                }
                /*Checkbox header*/
                if (element.id == "chkAll") {
                    if (element.hasClassName("ic-check")) {
                        checkAll(true);
                    }
                    else {
                        checkAll(false);
                    }
                    return;
                }
                $("errMsg").className = "";
                $("isReload").value = "1";//Has updated data
                var ajax = new Ajax.Request("../Handler/UpdateStatus.ashx", {
                    method: 'post',
                    parameters: "cstid=" + userid + "&iscls=" + status,
                    onComplete: function(strdata) {                    
                    if (strdata.responseText == 'Ok') {
                        $("errMsg").className = "done";
                        ErrAlter("The status has been updated successfully!");
                        DelayReloadPage1();
                        setTimeout("$('btnsearch').click()", 2000);
//                        
//                        if (depid == 0 && depname == '') {
//                            DelayReloadPage2(2000);
//                        }
//                        else {
//                            DelayReloadPage1();
//                            setTimeout("ListUser(" + depid + ",'" + depname + "', true);", 2000);
                        //}
                    }
                    if (strdata.responseText != 'Ok' && strdata.responseText != 'Fail') {
                        ErrAlter(strdata.responseText);
//                        if (isclose == "False") {
//                            $(custid).checked = true;
//                        }
//                        else {
//                            $(custid).checked = false;
//                        }
                    }
                    }
                });
                
        }
    }

    function checkAll(state) {
        if (listChk != null) {
            for (var i = 0; i < listChk.length; i++) {
                if (listChk[i].id.indexOf(",")<0) {
                    if (state)
                        listChk[i].className = "chk ic-check";
                    else
                        listChk[i].className = "chk ic-uncheck";
                }
            }
        }
    }
    
    function Search()
    {
        window.onunload = "";
        return true;
    }
    
</script>
<!--/js-->