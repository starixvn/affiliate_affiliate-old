<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_UserLog" Codebehind="UserLog.aspx.cs" %>

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

<script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

<script type="text/javascript">
    function LoadData(pageIndex) {
        $("page").value = pageIndex
        $("frmSearch").submit();
    }
    function toggleGroup(obj, className) {
        // Toggle image
        var expanding = jQuery(obj).hasClass("log_expand");

        if (expanding) {
            jQuery(obj).removeClass("log_expand");
            jQuery(obj).addClass("log_collapse");
            jQuery("." + className).fadeIn("slow");

        }
        else{
            jQuery(obj).removeClass("log_collapse");
            jQuery(obj).addClass("log_expand");

            jQuery("." + className).fadeOut('slow');

        }
    }

    jQuery.noConflict();
</script>

<!--/js-->
