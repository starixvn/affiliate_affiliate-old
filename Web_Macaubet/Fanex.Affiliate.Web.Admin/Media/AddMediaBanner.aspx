<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.Macaubet.Admin.Media.AddMediaBanner" Codebehind="AddMediaBanner.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../App_Js/ListBanner.js" type="text/javascript"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>

<script type="text/javascript">
    var jQ = jQuery.noConflict();

    function getSubCategory(category) {
        try {
            var td = $("trSubCategoryMedia").childNodes[1];
            while (td.childNodes.length >= 1) {
                td.removeChild(td.firstChild);
            }
            var loadingSpan = document.createElement("span");
            loadingSpan.innerHTML = '<img width="24px" height="24px" src="../App_Themes/DefaultV2/Images/loading.gif" alt="Loading ....." />';
            td.appendChild(loadingSpan);

            var ajax = new Ajax.Request("../Handler/GetSubCategory.ashx", {
                method: 'post',
                parameters: 'category=' + category + '&genAll=false',
                onComplete: function (data) {
                    var tempDiv = document.createElement("DIV");
                    tempDiv.innerHTML = data.responseText;
                    var select = tempDiv.firstChild;

                    //var td = $("trSubCategoryMedia").childNodes[1];
                    while (td.childNodes.length >= 1) {
                        td.removeChild(td.firstChild);
                    }

                    td.appendChild(select);


                    var subCat = jQ("#hidSubCategory").val();
                    jQ('#slSubCategoryMedia').val(subCat);

                }

            });

        }
        catch (e) {
            alert(e);
        }
    }

    jQ(function () {

        var category = jQ("#slCategory").val();
        var haschildren = jQ("#slCategory").find("option:selected").attr("data-haschildren");

        if (haschildren == 1) {
            jQ('#trSubCategoryMedia').show();
            getSubCategory(category);

        }
        else
            jQ('#trSubCategoryMedia').hide();


        jQ('#slCategory').change(function () {
            var $this = jQ(this);
            var category = $this.val();

            var haschildren = $this.find("option:selected").attr("data-haschildren");

            if (haschildren == 1) {
                jQ('#trSubCategoryMedia').show();
                getSubCategory(category);
            }
            else {
                jQ('#trSubCategoryMedia').hide();
            }
        });

        jQ('#txtfile').change(function () {
            var fullFileName = jQ(this).val();
            var temp = fullFileName.split("\\");
            var fileName = temp[temp.length - 1];

            jQ("#inpFileName").val(fileName);

            var index = fileName.indexOf(".");
            if (index > -1) {
                var ext = fileName.substr(index, fileName.length);

                if (ext == ".swf") {

                    jQ("#slImageType").find("option[text='Flash']").attr("selected", true)
                    jQ("#hdImageType").val(2);
                } else {
                    jQ("#slImageType").find("option[text='Image']").attr("selected", true)
                    jQ("#hdImageType").val(1);
                }



            }
            //alert(fileName);
        });
    });

</script>
<!--/js-->
