<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.Aoncash.Admin.Customer.Statistics" Codebehind="Statistics.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript">
    function ChangeProduct(spId, selId, isSelDate) {
        var select = $(selId);
        if (select.options.length == 0)
            return;
        if (isSelDate) {
            $(spId).hide();
            select.show();
            select.value = $(spId).innerHTML;
            select.focus();
                    
        }
        else {
            $(spId).show();
            select.hide();
            $("totalqualifiedmembers").innerHTML = select.options[select.selectedIndex].getAttribute("data-totalqualifiedmembers"); ;
            $(spId).innerHTML = select.options[select.selectedIndex].text;    
        }

       
    }
    function ChangeDate(spId, selId, isSelDate)
    {
        if(isSelDate)
        {
            $(spId).hide();
            $(selId).show();
            $(selId).focus();
        }
        else
        {
            $(spId).show();
            $(selId).hide();
        }
    }
    function Search()
    {
        DelayReloadPage1();
        $('frmSearch').submit();
    }
    function ShowPage(isShow)
    {
        if(!isShow)
        {
            $("tblContent").hide();
            $("divExpend").show();
            parent.document.getElementById("fRightContent").rows="35,*";
        }
        else
        {
            $("tblContent").show();
            $("divExpend").hide();
            parent.document.getElementById("fRightContent").rows="175,*";
        }
    }
</script>
<!--/js-->