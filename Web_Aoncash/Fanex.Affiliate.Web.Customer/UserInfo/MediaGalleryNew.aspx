<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="MediaGalleryNew.aspx.cs" 
    Inherits="Fanex.Web.Aoncash.Customer.UserInfo.MediaGalleryNew" %>
    <%@ Import Namespace="Fanex.Affiliate.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="<%=AppSettings.FullDomainName%>/App_Js/jquery/jquery-1.4.2.min.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
<script type ="text/javascript">
    var jQ = jQuery.noConflict();

    jQ(function () {
        //EnableDisableInputTrackingCode();
        //jQ('#trSubCategoryMedia').css("visibility", "visible");
        function getSubCategory(category, currentSubCategory) {
            try {
                var td = $("trSubCategoryMedia").childNodes[1];
                while (td.childNodes.length >= 1) {
                    td.removeChild(td.firstChild);
                }

                var loadingSpan = document.createElement("span");
                loadingSpan.innerHTML = '<img width="24px" height="24px" src="../App_Themes/Default/Images/loading.gif" alt="Loading ....." />';
                td.appendChild(loadingSpan);
                var currentSubCategory = jQ('#hdCurrentSubCategory').val();
                var ajax = new Ajax.Request("../Handler/GetSubCategory.ashx", {
                    method: 'post',
                    parameters: 'genAll=true&category=' + category + '&activeValue=' + currentSubCategory,
                    onComplete: function (data) {
                        var tempDiv = document.createElement("DIV");
                        tempDiv.innerHTML = data.responseText;
                        var select = tempDiv.firstChild;

                        while (td.childNodes.length >= 1) {
                            td.removeChild(td.firstChild);
                        }

                        td.appendChild(select);
                    }
                });

            }
            catch (e) {
                alert(e);
            }
        }
        jQ('#slCategory').change(function () {
            
            var category = jQ(this).val();
            
            var haschildren = jQ(this).find("option:selected").attr("data-haschildren");
            if (haschildren=="1") {
                jQ('#trSubCategoryMedia').show();
                getSubCategory(category);
            }
            else {
                jQ('#trSubCategoryMedia').hide();
            }
        }).change();
        
        /*
        var category = jQ('#slCategory').val();
        var currentSubCategory = jQ('#hdCurrentSubCategory').val();
        if (category != "Casino" && category != "Sportsbook") {
        jQ('#trSubCategoryMedia').hide();
        }
        else {
        getSubCategory(category, currentSubCategory);
        jQ('#trSubCategoryMedia').show();
        }
        */

    });

  

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
//    function EnableDisableInputTrackingCode() {
//        clock();
//        setInterval("clock();", 1000);
//    }
</script>
<%=htmlContent %>
</asp:Content>