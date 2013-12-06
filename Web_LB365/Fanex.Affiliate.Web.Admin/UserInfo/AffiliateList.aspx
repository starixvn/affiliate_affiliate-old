<%@ Page Language="C#" AutoEventWireup="true" Inherits="AffiliateList" Codebehind="AffiliateList.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    window.onload = MyFocus;
    //DelayReloadPage1();
    var time = 0;//set Timeout for clock refresh
    var timer = null;//set Timeout for choose refresh time
    var timePage = "";//set Timeout for go to page
    function MyFocus() {                 
        
        //$("slPageSize").value = $("pagesize").value;
        $("slTime").value = $("timer").value;
        if($("slTime").value != "0")
        {
            time = parseInt($("slTime").value);
            setInterval("CountDown();", 1000);
        }        
        //use for add favorite affiliate
        var listFavor = $$("div .favor");
        for(var i = 0; i < listFavor.length; i++)
        {
            listFavor[i].observe('click', favorOnClick);
        }
        //disable bottom button if no mem found
        /*if ($("nummem").value == 0) {
            $("btnCGroupBottom").style.display = "none";
        }*/
        //Use for checkbox icon
        CheckBox_AddEvent();
        if ($('txtGroupName') != null && $('txtGroupName') != 'undefined') {
            $('txtGroupName').focus();
        }
        else {            
            $('txtUsername').focus();
        }      
        
        //
        var status = getParam('status');
        if (status.length > 0)
            $(status).selected = true;
    }
    function ViewGroup(groupid) {       
        if (groupid > 1000)
            ibcWnd.open("../GroupSetting/EditGroup.aspx?groupid=" + groupid, 'Edit Commission Group for ' + $("u"+groupid).innerHTML, 150, 50, 755, 529, 'class1', 'class2');
        else
            ibcWnd.open("../GroupSetting/EditGroup.aspx?groupid=" + groupid, 'Edit Commission Group', 150, 50, 755, 529, 'class1', 'class2');        
            
    }
    //Start favorite 
    function favorOnClick(event)
    {
        var element = Event.element(event);
        element.readOnly = true;
        if(element.hasClassName("favor-check"))
        {            
            element.className = "favor favor-uncheck";
            //Call ajax to remove this aff in favorite list
            var ajax = new Ajax.Request("../Handler/Favorite.ashx", {
                method: 'post',
                parameters: "affid=" + element.id + "&action=0",
                onComplete: function(data) {
                    var result = data.responseText.split(";");
                    if(result[0] != "0")
                    {
                        alert(result[1]);
                        element.className = "favor favor-check";
                    }
                }
            });
        }
        else
        {
            element.className = "favor favor-check";
            //Call ajax to add this aff in favorite list
            var ajax = new Ajax.Request("../Handler/Favorite.ashx", {
                method: 'post',
                parameters: "affid=" + element.id + "&action=1",
                onComplete: function(data) {
                    var result = data.responseText.split(";");
                    if (result[0] != "0")
                    {
                        alert(result[1]);
                        element.className = "favor favor-uncheck";
                    }
                }
            });
        }
        element.readOnly = false;
    }
    //End favorite     
    function ChangeGroup()
    {
        listChk = $$("div.chk");
        listChecked = $$("div.chk.ic-check");
        var i = 0;
        var count = 0;
        var affid = 0;
        for(i = 0; i < listChk.length; i++)
        {
            if (listChk[i].hasClassName("ic-check")) {
                affid = listChk[i].id;
                count++;
            }
        }
        if(count == 0)
        {
            alert('Please select Affiliate(s) to change group!');
            return;
        }
        /*get currency*/
        var currency = document.getElementById('c' + affid).innerHTML;
        
        if (count == 1) {
            ibcWnd.open("../GroupSetting/AssignGroup.aspx?affid=" + affid + "&currency=" + currency, 'Change Commission Group', 150, 50, 585, 270, 'class1', 'class2');
        }
        else if (count > 1) {
        
            /*check currency*/
            theFirst = document.getElementById('c' + listChecked[0].id).innerHTML;
            for (i = 0; i < listChecked.length; i++) {
                var theNext = document.getElementById('c' + listChecked[i].id).innerHTML;
                if(theNext != theFirst)
                {
                    alert("Currencies of affiliates must be same!");
                    return;
                }
            }

            ibcWnd.open("../GroupSetting/AssignGroup.aspx?affid=" + affid + "&currency=" + currency + "&ismultiselect=true", 'Change Commission Group', 150, 50, 585, 270, 'class1', 'class2');
        }
        else {
            alert('Please select Affiliate(s) to change group!');
        }
            
    }
    
    function LockedOutGroup()
    {
        listChk = $$("div .chk");
        var i = 0;
        var listAffID = new Array();
        for(i = 0; i < listChk.length; i++)
        {
            if (listChk[i].hasClassName("ic-check")) {
                listAffID.push(listChk[i].id);
            }
        }
        if(listAffID.length > 0)
        {
            var answer = confirm("Are you sure you want to unlock selected affiliate(s)?")
	        if (answer){
		        var ajax = new Ajax.Request("../Handler/ToggleLockedOut.ashx", {
                    method: 'post',
                    parameters: "AffList=" + listAffID,
                    onComplete: function(data) {
                        if(data.responseText == "True")
                        {
                            document.location = "AffiliateList.aspx?status=approved&isLockedOut=true";
                        }
                        else
                        {
                            alert('Error. Please try again!');
                        }
                    }
                });
	        }
	        else{
	        }
	    }
    }
    
    //End change group block
    function CloseDiv() {    
        $('divChangePass').style.display ='none';            
    }
    function DoCal(url) {    
        ibcWnd.open(url, 'Change Password', 150, 50, 350, 175, 'class1', 'class2');
    }
    function Refresh(url)
    {
        location.href = url;
    }
    function setvaluefocus(e,isok,id) {          
        if(isok ==1) {       
            if(e.value ==$(id).value) 
                e.value=''; 
        }
        else 
            if(e.value=='')
                 e.value =$(id).value;
    }
    function checkData() {
        $("txtID").value = $("txtID").value.trim();
        if ($("txtID").value.toLowerCase() == "id" || $("txtID").value.trim() == '') {
            LoadData(1);
            return false;
        }
        if(!$("txtID").value.isNumber())
        {
            alert("ID is not number. Please input number!");
            $("txtID").focus();
            return false;
        }
        LoadData(1);
        return false;
    } 
    
    function getParam(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return results[1];
    }

    function chkOnclick(event) {
        var element = Event.element(event);

        if (element.hasClassName("ic-check")) {
            element.className = "chk ic-uncheck";
        }
        else {
            var listChecked = $$('div.chk.ic-check');
            var oldCurrency;
            var newCurrency;

            if (listChecked.length == 0) {
                element.className = "chk ic-check";
                return;
            }

            oldCurrency = $('c' + listChecked[0].id).innerHTML;
            newCurrency = $('c' + element.id).innerHTML;

            if (oldCurrency == newCurrency) {
                element.className = "chk ic-check";
            }
            else {
                if (confirm("The select Affiliate's Currency is different. If you want to continue, all other selected rows will be uncheck.\nDo you want to continue?")) {
                    for (i = 0; i < listChecked.length; i++)
                        listChecked[i].className = "chk ic-uncheck";
                    element.className = "chk ic-check";
                }
            }
        }
    }
</script>
<!--/js-->