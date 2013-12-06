<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_Adjustment" Codebehind="Adjustment.aspx.cs" %>

<!--js-->

<script type="text/javascript" src="../App_Js/prototype.js"></script>

<script type="text/javascript" src="../App_Js/Common.js"></script>

<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>

<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
    var isEdit = false;
    function Adjust()
    {
        if(isEdit)
            return false;
        isEdit = true;
        arrItem.clear();
        arrError.clear();
        if(isEmpty($("txtAdjust").value.trim()))
        {
            ErrAlter("The amount is required");
            isEdit = false;
            return false;
        }
        var txtAdj = 0;        
        var adjVal = 0;
        try
        {
            txtAdj = toNumber($("txtAdjust").value)
            adjVal = parseFloat(txtAdj);
            var maxAmount = parseInt($("hdMaxAmount").value);
            if(!(txtAdj.isNumber()) || adjVal == 0 || Math.abs(adjVal) > maxAmount)
            {
                ErrAlter("The amount is invalid");
                isEdit = false;
                return false;
            }          
        }
        catch(o){ErrAlter("The amount is invalid"); return false;}
        var ajax = new Ajax.Request('../Handler/Adjustment.ashx', {
            method: 'post',
            parameters: { custid: $('custid').value, adj: adjVal, des: $('txtDescript').value },
            onComplete: function(strdata) {
                var result = strdata.responseText.split("@");
                if (result[0] == '0') {
                    //reload this page and lef panel
                    $("errMsg").className = "done";
                    SuccAlter(result[1]);
                    DelayReloadPage1();
                    parent.fInfo.$("isReload").value = "4";
                    setTimeout("parent.fInfo.$('frmSearch').submit();", 2500);
                }
                else {
                    ErrAlter(result[1]);
                    isEdit = false;
                }
            }
        });  
        return false;
    }
</script>

<!--/js-->