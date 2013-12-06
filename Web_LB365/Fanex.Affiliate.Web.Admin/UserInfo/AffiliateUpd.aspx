<%@ Page Language="C#" AutoEventWireup="true" Inherits="AffiliateUpd" Codebehind="AffiliateUpd.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>

<script type="text/javascript">
    window.onload=function(){
        CloseClick(); 
    };
    
    var arrItem = new Array();
    var arrError = new Array();
    function submitForm() {
        $$("input[type=text]").each(function (el) {
            el.value = el.value.trim();
        });
        $('errMsg').className = "";
        $("hdMoreurl").value = "";
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        var isSucc = CheckSuccess();
        if(isSucc == false)
            return false;
        var str = "";
        var firstItem = 0;
        $$("#siteUrl tr td input.sites").each(function (el) {
            if (firstItem == 0)
                firstItem = 1;
            else
                str += el.value + ";";
        });
         if (str.length > 0)
             str = str.substr(0, str.length - 1);

        $("hdMoreurl").value = str;
                
        DelayReloadPage1();
        return true;
    }
    function UpdateSuccess(msg, url, intReload)
    {   
        HideDelayReloadPage();
        if(parent.fInfo)
        {
            arrError.clear();
            arrItem.clear();
            arrError.push(msg);
            $('errMsg').className = "done";
            $('errMsg').show();
            arrItem.push("error_item");
            ShowError(arrError, arrItem, "error_item");
            //var tmp = "";
            parent.fInfo.$("isReload").value = "2";
            parent.fInfo.$("frmSearch").submit();
        }
        else
        {
            location.href=url;
        }        
    }
    function UpdateErr(msg, itemFocus)
    {        
        $('errMsg').show();        
        arrError.clear();
        arrItem.clear();
        arrError.push(msg);
        arrItem.push(itemFocus);
        ShowError(arrError, arrItem, "error_item");
        HideDelayReloadPage();
    }
    function CheckSuccess()
    {
        if(isEmpty($('txtFName').value))
        {
            arrError.push(top.fMenu.Msg.FName);
            arrItem.push("txtFName");
        }
        if(isEmpty($('txtLName').value))
        {
            arrError.push(top.fMenu.Msg.LName);
            arrItem.push("txtLName");
        }
        if (!document.getElementsByName("rdGender")[0].checked) {
            if(!document.getElementsByName("rdGender")[1].checked){
                arrItem.push("rdGender");
                arrError.push(top.fMenu.Msg.ErrGender);
            }
        }
        if(isEmpty($('txtAddress').value))
        {
            arrError.push(top.fMenu.Msg.Address);
            arrItem.push("txtAddress");
        }
        if(isEmpty($('txtCity').value))
        {
            arrError.push(top.fMenu.Msg.City);
            arrItem.push("txtCity");
        }
        if(isEmpty($('txtState').value))
        {
            arrError.push(top.fMenu.Msg.State);
            arrItem.push("txtState");
        }
        if(isEmpty($('txtPost').value))
        {
            arrError.push(top.fMenu.Msg.PostCode);
            arrItem.push("txtPost");
        }
        if(isEmpty($('selCountry').value))
        {
            arrError.push(top.fMenu.Msg.Country);
            arrItem.push("selCountry");
        }
        if(!checkMail($('txtEmail').value))
        {
            arrError.push(top.fMenu.Msg.EmailValid);
            arrItem.push("txtEmail");
        }
        if (isEmpty($('txtNumber').value)) {
            arrError.push(top.fMenu.Msg.ContactNumber);
            arrItem.push("txtNumber");
        }
        if (isEmpty($('txtMobile').value)) {
            arrError.push(top.fMenu.Msg.MobileNumber);
            arrItem.push("txtMobile");
        }             
        
        var listSites = $$(".sites");
        for(var i = 0; i < listSites.length; i++)
        {
            if(listSites[i])
            {
                if(isEmpty(listSites[i].value))
                {
                    arrError.push("Website URL " + (i==0 ? "" : (i + 1).toString()) + " is required");
                    arrItem.push(listSites[i].id);
                    break;
                }  
            }
        }
        //check12Bet();   
        if(arrItem.length > 0)
        {            
            $('errMsg').show();
            ShowError(arrError, arrItem, "error_item");
            return false;
        } 
        return true;
    }
    function check12Bet()
    {
        if(isEmpty($('txtBet12UserName').value))
        {
            arrError.push(top.fMenu.Msg.UserName12Bet);
            arrItem.push("txtBet12UserName");
        }
    }

    function CloseClick()
    {
        if($("chkStatus"))
        {
            $("slComm").disabled = $("chkStatus").checked;
            if($("chkStatus").checked)
            {
                $("spCommList").hide();
                $("spComm").show();
            }
            else
            {
                $("spCommList").show();
                $("spComm").hide();
                $("slComm").value = ($("spComm").innerHTML == "0.00" ? "0.30" : $("spComm").innerHTML);
            }
        }
    }    
    function AddOneMoreURL()
    {
        var listSites = $$(".sites");
        var siteIndex = listSites.length;
        if(isEmpty(listSites[siteIndex - 1].value))
        {
            alert("Please input Website URL " + siteIndex.toString());
            listSites[siteIndex - 1].focus();
            return;
        }
        siteIndex = siteIndex +1;
        var table = $("siteUrl");
        var tr = table.insertRow(table.rows.length);
        var td1 = IBCWnd.createElement("td", tr);
        var td2 = IBCWnd.createElement("td", tr);
        var td3 = IBCWnd.createElement("td", tr);
        tr.setAttribute("id", "tr" + siteIndex);
        td2.className = "cont12oe";
        td2.innerHTML = "*";
        td1.innerHTML = "Website URL " + siteIndex.toString();
        td3.innerHTML = "<input type='text' size='30' class='sites' id='txtSiteUrl" + siteIndex + "' name='txtSiteUrl" + siteIndex + "' />&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + siteIndex + ");\" class=\"buttonlogin\" value=\"Del\" />";
        if($("txtUrl" + siteIndex))
            $("txtUrl" + siteIndex).focus();
    }
    function DelUrl(rowNum)
    {        
        $("siteUrl").deleteRow(rowNum - 1);
        reorderRows(rowNum - 1);
    }
    function reorderRows(starting)
    {
        var tbl = $("siteUrl");
        if (tbl.tBodies[0].rows[starting]) {            
            for(var i = starting; i < tbl.tBodies[0].rows.length; i++)
            {          
                var tr = tbl.tBodies[0].rows[i];
                var tds = tr.getElementsByTagName("td");
                
                var index = i + 1;
                tds[0].innerHTML = "Website URL " + index.toString();
                tds[2].innerHTML = "<input type='text' size='30' value='" + tds[2].childNodes[0].value + "' class='sites' id='txtUrl" + index + "' name='txtUrl" + index + "' />&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + index + ");\" class=\"buttonlogin\" value=\"Del\" />";
            }
        }
    }
</script>
<!--/js-->
