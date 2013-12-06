<%@ Page Language="C#" AutoEventWireup="true" Inherits="Report_AffiliateReportDetail" Codebehind="AffiliateReportDetail.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>
<script type="text/javascript">
    window.onload=function()
                {
                    EnableCalendar($("chkDate").className == "ic-uncheck" ? true : false);
                };
    function chkOnclick()
    {
        if($("chkDate").className == "ic-uncheck")
        {
            $("chkDate").className = "ic-check";
            $("isGetAll").value = "ic-check";
            EnableCalendar(false);
        }
        else
        {
            $("chkDate").className = "ic-uncheck"
            $("isGetAll").value = "ic-uncheck";
            EnableCalendar(true);
        }
    }
    function Search()
    {   
        if(isEmpty($("affname").value))
        {
            ErrAlter("Invalid Affiliate")
            return false;
        }
        $("frmSearch").submit();
        DelayReloadPage1();
    }
</script>
<!--/js-->