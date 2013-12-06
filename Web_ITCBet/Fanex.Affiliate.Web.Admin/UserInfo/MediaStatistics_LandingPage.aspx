<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaStatistics_LandingPage.aspx.cs" Inherits="UserInfo_MediaStatistics_LandingPage" %>

<!--js-->
<link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>
<script type="text/javascript">
    var pageTimeOut;
    function PageNum(aID, txtID, isInputPage) {
        clearTimeout(pageTimeOut);
        pageTimeOut = null;
        if (isInputPage) {
            $(aID).hide();
            $(txtID).show();
            if ($(txtID))
                $(txtID).focus();
        }
        else
            pageTimeOut = setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
    }

    function ChangePage(e, txtID, maxPage, aID) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        aID = (aID == null) ? "aPageTop" : aID;
        if (key == 13) {
            var currPage = parseInt($(txtID).value);
            if (currPage > maxPage || isNaN(currPage) || currPage <= 0) {
                setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
                return false;
            }
            LoadDetail(currPage);
        }
        return false;
    }    
    function GotoPage(pageIndex, maxPage) {
        var currPage = parseInt($("page").value);
        if (currPage == 1 && pageIndex <= 1)
            return false;
        if (currPage == maxPage && pageIndex >= maxPage)
            return false;
        if (pageIndex > maxPage || pageIndex <= 0)
            return false;
        LoadDetail(pageIndex);
    }

    function LoadDetail(currPage) {
        //DelayReloadPage1();
        try {
            if ($("page"))
                $("page").value = currPage;
            if ($("pagesize") && $("slPageSize"))
                $("pagesize").value = $("slPageSize")[$("slPageSize").selectedIndex].value;
            ViewDetail($("tmpFromDate").value, $("tmpToDate").value, $("tmpProductCode").value, $("tmpLanguageCode").value, $("slPageSize").value, currPage)
        }
        catch (e) {
            alert('Invalid data!');
        }
    }


    function ViewDetail(fdate, tdate, productCode, languageCode, pageSize, pageIndex) {
        try {
            //Store old info in tmp hidden field
            $("tmpFromDate").value = fdate;
            $("tmpToDate").value = tdate;
            $("tmpProductCode").value = productCode;
            $("tmpLanguageCode").value = languageCode;

            var params;
            params = "fdate=" + fdate + "&tdate=" + tdate + "&productCode=" + productCode + "&languageCode=" + languageCode + "&pageSize=" + pageSize + "&pageIndex=" + pageIndex;

            var ajax = new Ajax.Request("../Handler/GetStatisticsDetail_New.ashx", {
                method: 'get',
                parameters: params,
                onComplete: function (data) {
                    ProcessResultDetail(data.responseText);
                    $("slPageSize").value = pageSize;
                    $("aPageBottom").focus();
                }
            });

        }
        catch (e) {
            alert(e);
        }
    }
    function ProcessResultDetail(data) {
        try {
            $("statisticsDetail").innerHTML = data;
            $("statisticsDetail").style.display = "";
        }
        catch (e) {
            alert(e);
        }
    }

    function searchData() {
        $("frmSearch").submit();
    }
</script>
<!--/js-->