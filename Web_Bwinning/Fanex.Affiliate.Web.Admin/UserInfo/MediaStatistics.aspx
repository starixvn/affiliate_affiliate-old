<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_MediaStatistics" Codebehind="MediaStatistics.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    
    function ViewStatisticsAll() {

        try {
            var params;
            params = "params=" + $("slTrackingCode").value;

            var ajax = new Ajax.Request("../Handler/GetClickStatisticsList.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {                   
                    var sXmlBody = "<table><tbody id='result'>" + data.responseText + "</tbody></table>";
                    var tempDiv = document.createElement("DIV");
                    tempDiv.innerHTML = sXmlBody;
                     $("tblResult").removeChild($("result"));
                    $("tblResult").appendChild(tempDiv.firstChild.firstChild);
                    $("statisticsDetail").style.display = "none";                   
                }
            });

        }
        catch (e) {
            alert(e);
        }

    }
    
    function ViewStatisticsDetail(month) {
        try {
            var params;
            params = "params=" + $("slTrackingCode").value + ";" + month;

            var ajax = new Ajax.Request("../Handler/GetStatisticsListDetail.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {               
                    var sXmlBody = "<table><tbody id='resultDetail'>" + data.responseText + "</tbody></table>";
                    var tempDiv = document.createElement("DIV");
                    tempDiv.innerHTML = sXmlBody;
                    $("tblResultDetail").removeChild($("resultDetail"));
                    $("tblResultDetail").appendChild(tempDiv.firstChild.firstChild);
                    $("statisticsDetail").style.display = "";
                }
            });

        }
        catch (e) {
            alert(e);
        }
    }
    
</script>
<!--/js-->