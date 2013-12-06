<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_EarningsHistory" Codebehind="EarningsHistory.aspx.cs" %>
<!--js-->
<link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>
<script type="text/javascript">
    window.onload=function(){getAll();};
        var test = "123";
    
    function UpdSucc()
    {
        ibcWnd.close();
        DelayReloadPage1();
        setTimeout("ReloadPage();", 1000);
    }
    function ReloadPage()
    {
        var fInfo = parent.fInfo;
        if(fInfo)
        {
            fInfo.$("isReload").value = "3";
            fInfo.$("frmSearch").submit();
        }
        location.href = "EarningsHistory.aspx";
    }
    function getAll() {
        listChk = $$("div .chk");
        if ($("getAll").value != "True") {
            listChk[0].className = "chk ic-uncheck";
            $("fdate").disabled = false;
            $("tdate").disabled = false;
            $("fdateTrigger").disabled = false;
            $("tdateTrigger").disabled = false;
            Calendar.setup({
                inputField: "fdate",
                ifFormat: "%m/%d/%Y",
                button: "fdateTrigger",
                singleClick: true,
                showsTime: false,
                showOthers : true,
                step : 1
            });
            Calendar.setup({
                inputField: "tdate",
                ifFormat: "%m/%d/%Y",
                button: "tdateTrigger",
                singleClick: true,
                showsTime: false,
                showOthers : true,
                step : 1
            });
        }
        else {
            listChk[0].className = "chk ic-check";
            $("fdate").disabled = true;
            $("tdate").disabled = true;
            $("fdateTrigger").disabled = true;
            $("tdateTrigger").disabled = true;
            $("fdateTrigger").onclick = "";
            $("tdateTrigger").onclick = "";
        }
        for (var i = 0; i < listChk.length; i++) {
            listChk[i].observe('click', EarningChkOnClick);
        }
    }

    function EarningChkOnClick(event) {
        var element = Event.element(event);
        if (element.hasClassName("ic-check")) {
            element.className = "chk ic-uncheck";
            $("getAll").value = "false";            
            $("fdate").disabled = false;
            $("tdate").disabled = false;
            $("fdateTrigger").disabled = false;
            $("tdateTrigger").disabled = false;

            Calendar.setup({
                inputField: "fdate",
                ifFormat: "%m/%d/%Y",
                button: "fdateTrigger",
                singleClick: true,
                showsTime: false,
                showOthers : true,
                step : 1
            });

            Calendar.setup({
                inputField: "tdate",
                ifFormat: "%m/%d/%Y",
                button: "tdateTrigger",
                singleClick: true,
                showsTime: false,
                showOthers : true,
                step : 1
            });
     
        }
        else {
            element.className = "chk ic-check";
            $("getAll").value = "true";
            $("fdate").disabled = true;
            $("tdate").disabled = true;
            $("fdateTrigger").disabled = true;
            $("tdateTrigger").disabled = true;
            $("fdateTrigger").onclick = "";
            $("tdateTrigger").onclick = "";
        }
    }
    function NavigatePage(mypage){
        $("page").value = mypage
        $("frSearch").submit();
    }
</script>
<!--/js-->