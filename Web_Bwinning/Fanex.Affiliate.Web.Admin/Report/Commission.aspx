<%@ Page Language="C#" AutoEventWireup="true" Inherits="Report_Commission" Codebehind="Commission.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" language="javascript">
    window.onload = Init;

    function Init() {
        $("slmonth").value = $("month").value;
        $("slyear").value = $("year").value;
        if (!isEmpty($("hdLanguage").value))
            $("slPreferLang").value = $("hdLanguage").value;
        else
            $("slPreferLang").selectedIndex = 0;
    }
    function Search() {
        $("frmSearch").submit();
        DelayReloadPage1();
    }

    function Focus() {
        if ($("affname").value == "[All]") {
            $("affname").value = "";
        }
        else {

        }
    }

    function Blur() {
        if ($("affname").value == "") {
            $("affname").value = $("all").value;
        }
    }
</script>
<!--/js-->