<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.i668bet.Admin.Exchange.AddExchanges" Codebehind="AddExchanges.aspx.cs" %>
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
        // float input
        $("#divContent input[type=number]").keypress(function (eve) {
            if ((eve.which != 46 || $(this).val().indexOf('.') != -1) &&
                (eve.which < 48 || eve.which > 57)) {
                eve.preventDefault();
            }
        });

        // this part is when left part of number is deleted and leaves a . in the leftmost position. For example, 33.25, then 33 is deleted
        $("#divContent input[type=number]").keyup(function (eve) {
            if ($(this).val().indexOf('.') == 0) {
                $(this).val($(this).val().substring(1));
            }
        });
        $("#divContent input[type=checkbox]").click(function () {
            if (this.checked)
                $(this).parents("tr").find("input[type=number]").removeAttr("disabled");
            else
                $(this).parents("tr").find("input[type=number]").attr("disabled", true);
        });

        $("#form1").submit(function () {
            $("#errMsg").removeAttr("class");
            arrItem.clear();
            arrError.clear();
            // build string data
            var str = "";
            $("#form1 input[type=checkbox]:checked").each(function () {
                str += $(this).attr("data-id") + ",{creator},";
                $(this).parents("tr").find("input[type=number]").each(function () {
                    var rate = $(this).val();
                    if (!rate)
                        rate = 0;
                    str += rate + ",";
                });
                str = str.substring(0, str.length - 1); // remove last comma
                str = str.replace(/\$/g, "'");
                str += "#";
            });

            $.ajax({ url: '../Handler/AddExchanges.ashx',
                type: 'post',
                data: {
                    currencies: $("#affcurrencies").val(),
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
                        location.reload();
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