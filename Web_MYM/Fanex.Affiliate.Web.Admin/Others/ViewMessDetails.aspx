<%@ Page Language="C#" AutoEventWireup="true" Inherits="ViewMessDetails" Codebehind="ViewMessDetails.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript">
    window.onload = Init;
    var arrItem = new Array();
    var arrErr = new Array();

    function Init() {
        /*assign handler for checkbox div*/
        chkList = $$("div .chk");
        if (chkList.length > 0)
            chkList[0].observe('click', chkOnClick);
        
        if ($("hdMode").value == "view") {
            SetViewMode();
            if ($("hdImportant").value == "on")
                $$("div .chk")[0].className = "chk ic-check";
            else
                $$("div .chk")[0].className = "chk ic-uncheck";
        }
        else {
            ShowAddNewMessage();
        }
    }

    function chkOnClick(event) {
        if ($("hdMode").value == "view")
            return;
        var element = Event.element(event);
        if (element.hasClassName("chk ic-uncheck"))
            element.className = "chk ic-check";
        else
            element.className = "chk ic-uncheck";
    }
    
    function CheckForm()
    {
        return true;
    } 
    
    var paththeme = "{paththeme}";
    function DeleteSelectedMessage() {
        if (confirm($("ConfirmDeleteMess").value)) {
            var params = "";
            for (var i = minNo; i <= maxNo; i++) {
                if ($("chk" + i).checked) {
                    params += $("td" + i).innerHTML;
                    params += ",";
                }
            }
            DeleteMessage(params);
        }
    }
    function DeleteMessage(msgList) {
        try {
            var params;
            params = "params=" + msgList;

            var ajax = new Ajax.Request("../Handler/DeleteMessageList.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {
                if (data.responseText != "") {
                        alert(data.responseText);
                }
                ClosePopup();
                }
            });

        }
        catch (e) {
            alert(e);
        }
    }
   
    function DeleteCurrentMessage() {
        if (confirm($("ConfirmDeleteMess").value)) {
            DeleteMessage($("mid").value + ",");
        }
    }

    function SetViewMode() {        
        $$("#content textarea").each(function (el) {
            el.readOnly = true;
        });
        $("btnAdd").style.display = "none";
        $("btnUpdate").style.display = "none";
        $("btnEdit").style.display = "";
        $("btnDeleteCur").style.display = "";
        $("hdMode").value = "view";

    }
    function SetEditMode() {
        $$("#content textarea").each(function (el) {
            el.readOnly = false;
        });
        $("btnAdd").style.display = "none";
        $("btnEdit").style.display = "none";
        $("btnUpdate").style.display = "";
        $("btnDeleteCur").style.display = "";
        $("hdMode").value = "edit";
    }

    function ShowAddNewMessage() {
        $$("#content textarea").value = ""; 
        $("btnAdd").style.display = "";
        $("btnUpdate").style.display = "none";
        $("btnEdit").style.display = "none";
        $("btnDeleteCur").style.display = "none";

    }
    
    function UpdateMessage() {
        var hasValue = false;        
        var txt = $$("textarea");
        for (var i in txt) {
            if (txt[i].value && txt[i].value.trim() != "") {
                hasValue = true;
                break;
            }
        }
        if (!hasValue) {
            alert($("AddEmptyMessError").value);
            return;
        }
        if (confirm($("ConfirmUpdateMess").value)) {
            try {
                
                var curMsgID = $("mid").value;
                
                var params = "";

                var important;
                if ($$("div .chk")[0].hasClassName("chk ic-check"))
                    important = 1;
                else
                    important = 0;
                $$("#content textarea").each(function (element) {
                    /// params += element.id + '=' + encodeURIComponent(element.value) + "$$$";
                    params += element.id + '=' + encodeURIComponent(element.value) + "$$$";
                    
                });
                
                var ajax = new Ajax.Request("../Handler/AddMessage.ashx", {
                    method: 'post',
                    parameters: {                        
                        id : curMsgID,
                        important: important,
                        params: params
                    },
                    onComplete: function (data) {

                        if (data.responseText != "") {
                            alert(data.responseText);
                        }
                        ClosePopup();
                    }
                });

            }
            catch (e) {
                alert(e);
            }
        }
    }

    function AddNewMessage() {
        try {
            var params = "";
            var hasValue = false;
            var txt = $$("textarea");
            for (var i in txt) {
                if (txt[i].value && txt[i].value.trim() != "") {
                    hasValue = true;
                    break;
                }
            }
            if (!hasValue) {
                alert($("AddEmptyMessError").value);
                return;
            }
            
            var important;
            if ($$("div .chk")[0].hasClassName("chk ic-check"))
                important = 1;
            else
                important = 0;
            $$("#content textarea").each(function (element) {
                /// params += element.id + '=' + encodeURIComponent(element.value) + "$$$";
                params += element.id + '=' + element.value + "$$$";
                
            });
              
            
            var ajax = new Ajax.Request("../Handler/AddMessage.ashx", {
                method: 'post',
                parameters: {
                    important: important,
                    params: encodeURIComponent(params)
                },
                onComplete: function (data) {
                    if (data.responseText != "") {
                        alert(data.responseText);
                    }
                    ClosePopup();
                }
            });

        }
        catch (e) {
            alert(e);
        }
    }
</script>
<!--/js-->
