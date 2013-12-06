<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.MYM.Admin.Exchange.AddAffCurrencies" Codebehind="AddAffCurrencies.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script src="../App_Js/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
  var arrItem = new Array();
    var arrError = new Array();
    var isUpdate = false;

    $.noConflict();
    jQuery(document).ready(function ($) {
        if ($("#form1 input[type=checkbox]").length == 0) {
            $("#errMsg").removeAttr("class");
            $("#divContent").hide();
            arrError.push(top.fMenu.Msg.NoInfo);
            arrItem.push("txtRate");
            $('#errMsg').show();
            ShowError(arrError, arrItem, "error_item");
             setTimeout("DelayReloadPage1();parent.ibcWnd.close();", 1000);
            return;
        }

        $("#form1").submit(function () {
            $("#errMsg").removeAttr("class");
            arrItem.clear();
            arrError.clear();
            // build string data
            var str = "";
            $("#form1 input[type=checkbox]:checked").each(function () {
                str += $(this).attr("value") + ",";
            });
            str = str.substr(0, str.length - 1);

            $.ajax({ url: '../Handler/AddAffCurrencies.ashx',
                type: 'post',
                data: {
                    data: str
                },
                success: function (res) {

                    if (res == "1") {
                        $("#errMsg").addClass("done");
                        arrError.push("Add Successfully!");
                        arrItem.push("errMsg");
                        isUpdate = true;
                        $("#errMsg").show();
                        ShowError(arrError, arrItem, "error_item");
                        setTimeout("DelayReloadPage1();parent.ibcWnd.close();parent.location.reload();", 1000);

                    }
                    else {
                        arrError.push(res.responseText);
                        arrItem.push("txtRate");
                        $('#errMsg').show();
                        ShowError(arrError, arrItem, "error_item");
                        HideDelayReloadPage();
                    }
                }
            });
            return false;
        });
    });
</script>
<!--/js-->