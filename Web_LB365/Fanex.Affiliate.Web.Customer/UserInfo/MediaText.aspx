<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaText.aspx.cs" Inherits="Fanex.Web.LiveBet365.Customer.UserInfo.MediaText" 
     MasterPageFile="~/Master.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script type ="text/javascript">
    //window.onload = function () { EnableDisableInputTrackingCode(); };
    function NavigatePage(curPage) {
        GetDataBannerList(curPage, false);
    }
    function GetDataBannerList(page, isGetTextLink) {
        var param = 'cate=' + $('slCategory').value + '&lang=' + $('slLanguage').value + '&currency=' + $('slCurrency').value + '&type=' + $('slImageType').value + "&size=" + $('slSize').value + "&page=" + page;

        var ajax = new Ajax.Request("../Handler/GetDataBanner.ashx", {
            method: 'post',
            parameters: param,

            onComplete: function (data) {
                ProcessResult(data.responseText);

            }
        });
    }

    function ProcessResult(data) {
        try {
            var arr = data.split("$$$");
            var sXmlBody = "<table><tbody id='result'>" + arr[0] + "</tbody></table>";
            var tempDiv = document.createElement("DIV");
            tempDiv.innerHTML = sXmlBody;
            $("tblResult").removeChild($("result"));
            $("tblResult").appendChild(tempDiv.firstChild.firstChild);
            if (arr.length > 1) {
                $("textlink").innerHTML = arr[1];
            }
        }
        catch (e) {
            alert(e);
        }
    }
    /*function EnableDisableInputTrackingCode() {

        clock();
        setInterval("clock();", 1000);
    }*/
    function checkData() {
    }
</script>
<%=htmlContent %>
</asp:Content>