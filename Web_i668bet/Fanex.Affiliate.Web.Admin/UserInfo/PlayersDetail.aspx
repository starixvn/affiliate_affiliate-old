<%@ Page Language="C#" AutoEventWireup="true" Inherits="PlayersDetail" Codebehind="PlayersDetail.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    function NavigatePage(mypage){
        $("page").value = mypage
        $("frSearch").submit();
    }
</script>
<!--/js-->