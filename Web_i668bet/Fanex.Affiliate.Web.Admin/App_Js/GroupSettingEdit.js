
window.onload = Initialize;

var StringBuilder = function () {
    var buffer = [];
    if (arguments.length === 1 && typeof arguments[0] === "string")
        buffer.push(arguments[0]);
    this.append = function (str) {
        buffer.push(str);
    };
    this.clear = function () {
        buffer = null;
        buffer = [];
    };
    this.insert = function (index, str) {
        if (buffer[index]) { buffer[index] = str; }
        else { return false; }
    };
    this.replace = function (find, replace) {
        var length = buffer.length;
        for (var i = 0; i < length; i++) {
            var exp = new RegExp(find, 'gm');
            buffer[i] = buffer[i].replace(exp, replace);
        }
    };
    this.remove = function () {
        if (typeof arguments[0] === "string") {
            var length = buffer.length;
            for (var i = 0; i < length; i++) {
                if (arguments[0] === buffer[i]) { buffer.splice(i, 1); }
            }
        }
        else {
            var index = arguments[0], length = (arguments.length > 1) ? arguments[1] : 1;
            buffer.splice(index, length);
        }
    };
    this.toString = function () {
        return buffer.join("");
    };
};



function Initialize() {    
    //Add Event Handler for each control
    $("chkAll").observe('click', SwitchAllProductsStatus);
    
    $("btnSave").observe('click', SaveSetting);
    
    for (var i = 0; i < GroupSetting.productSetting.length; i++) {
        //ProductIcon
        
        $("icon_enable" + GroupSetting.productSetting[i].id).observe('click', ChangeProductStatus);
        
        $("collapse_expand" + GroupSetting.productSetting[i].id).observe('click', SwitchProductTab);
        //Copy All Checkbox
        $("chkCopy" + GroupSetting.productSetting[i].id).observe('click', CopySetting);
        //Add Tier button
        $("btnAddTier" + GroupSetting.productSetting[i].id).observe('click', AddTier);
        //Deduction Event Handler
        for (var j = 0; j < GroupSetting.productSetting[i].deductions.length; j++) {
            $("deduction_" + GroupSetting.productSetting[i].id + "_" + GroupSetting.productSetting[i].deductions[j].id).observe('click', ChangeDeductionStatus);
        }
        //Deduction Event Handler for each control in each Tier
        for (var k = 1; k <= GroupSetting.productSetting[i].minAmounts.length; k++) {
            AddEventHandlerForOneTierRow(GroupSetting.productSetting[i].id, k);
        }

    }
    //Disable check box copy when have only one product enabled
    var enabledProductID = -1, count = 0;
    for (var i = 0; i < GroupSetting.productSetting.length; i++) {
        if (GroupSetting.productSetting[i].isEnabled) {
            count++;
            enabledProductID = GroupSetting.productSetting[i].id;
        }
    }
    if (count == 1) {
        $("chkCopy" + enabledProductID).className = CheckboxStatus.OffDisable;
        GroupSetting.copyProductID = -1;
    }
    //Active all product for Default
    if (!GroupSetting.isEdited) {
        fireEvent($("chkAll"), 'click');
    }
    if ($("txtGroupName") != null && $("txtGroupName") != 'undefined') {
        $("txtGroupName").observe('blur', GroupNameChanged);
        $("txtDescription").observe('blur', DescriptionChanged);
        if ($("txtGroupName").disabled == "") {
            $("txtGroupName").focus();
        }
        else {
            $("txtDescription").focus();
        }
    }

    //$("qualifiedAmount").observe('blur', MaxAmountBlur);
    $("qualifiedAmount").observe('keypress', IsNumericKey);
    $("qualifiedAmount").observe('change', FixNumberAmountOnly);
    $("qualifiedAmount").observe('keyup', FixNumberAmountOnly);

    $("currencies").onchange = function () {
        var multipliedby = this.options[this.selectedIndex].getAttribute("data-multipliedby")
        if (multipliedby > 1) {
            var currencycode = this.options[this.selectedIndex].text;
            var ajax = new Ajax.Request("../Handler/GetCurrencyMultipliedByNotification.ashx", {
                method: 'post',
                parameters: "currencycode=" + currencycode + "&multipliedby=" + multipliedby,
                onComplete: function (data) {
                    var result = data.responseText;
                    if (result != "") {
                        $("currNotify").innerHTML = result;
                        $("currNotify").style.display = "block";
                    }
                }
            });
        }
        else
            $("currNotify").style.display = "none";
    };

       $("currencies").onchange();
}


function GroupNameChanged(event) {
    GroupSetting.groupName = trim(this.value);
}
function DescriptionChanged(event) {
    GroupSetting.description = trim(this.value);
}
function SaveSetting(event) {    
    if (IsValidSetting()) {

        if (GroupSetting.isEdited) {
            var alertText = new StringBuilder("Are you sure you want to update ");
            if (parent.ibcWnd.titleText.indexOf('Edit Commission Group for') >= 0) {
                alertText.append(parent.ibcWnd.titleText.substring('Edit Commission Group for '.length, parent.ibcWnd.titleText.length));
                alertText.append("'s commission group ?");
            }
            else
                alertText = Msg.ConfirmUpdateGroup.replace("{a}", GroupSetting.groupName);
            if (!confirm(alertText))
                return;
        }

        var currency = encodeURIComponent(document.getElementById('currencies').value);
        var qualifiedAmount = encodeURIComponent($('qualifiedAmount').value);

        DelayReloadPage1();

        GroupSetting.groupName = encodeURIComponent(GroupSetting.groupName);
        GroupSetting.description = encodeURIComponent(GroupSetting.description);

        var ajax = new Ajax.Request("../Handler/CommGroupUpd.ashx", {
            method: 'post',
            parameters: "params=" + Object.toJSON(GroupSetting) + "&currency=" + currency + "&qualifiedAmount=" + qualifiedAmount,
            onComplete: function (data) {
                var result = data.responseText;
                if (result != "")
                    alert(result);
                else {
                    if (!GroupSetting.isApproved) {
                        if (parent.location.href.indexOf("GroupSetting.aspx") > 1 || parent.location.href.indexOf("AffiliateList.aspx") > 1) {
                            parent.location.href = parent.location.href.replace(/#/, "");
                        } else
                            parent.parent.location.href = parent.parent.location.href.replace(/#/, "");
                    }
                }
                if (GroupSetting.isApproved) {
                    parent.parent.location.href = parent.parent.location.href.replace(/#/, "");
                }
                HideDelayReloadPage();
            }
        });
    }
}

function fireEvent(element, event) {

    if (document.createEvent) { // DOM Level 2 standard
        evt = document.createEvent("MouseEvents");
        evt.initMouseEvent("click", true, true, window,
      0, 0, 0, 0, 0, false, false, false, false, 0, null);

        element.dispatchEvent(evt);
    } else if (element.fireEvent) { // IE
        element.fireEvent('onclick');
    }
    /*
    if (element.fireEvent) {
        var e = document.createEventObject();
        element.fireEvent('on' + event);        
    }
    else {
        var e = document.createEvent('MouseEvents');
        e.initEvent(event, true, true);
        element.dispatchEvent(e);
    }*/
}



function AddEventHandlerForOneTierRow(ProductID, tierID) {
    $("txtMinAmount_" + ProductID + "_" + tierID).observe('blur', MinBlur);
    $("txtMinAmount_" + ProductID + "_" + tierID).observe('keypress', IsNumericKey);
    $("txtMinAmount_" + ProductID + "_" + tierID).observe('change', FixNumberAmount);
    $("txtMinAmount_" + ProductID + "_" + tierID).observe('keyup', FixNumberAmount);

    $("txtMaxAmount_" + ProductID + "_" + tierID).observe('blur', MaxAmountBlur);
    $("txtMaxAmount_" + ProductID + "_" + tierID).observe('keypress', IsNumericKey);
    $("txtMaxAmount_" + ProductID + "_" + tierID).observe('change', FixNumberAmount);
    $("txtMaxAmount_" + ProductID + "_" + tierID).observe('keyup', FixNumberAmount);

    $("txtMinQM_" + ProductID + "_" + tierID).observe('blur', MinBlur);
    $("txtMinQM_" + ProductID + "_" + tierID).observe('keypress', IsNumericKey);
    $("txtMinQM_" + ProductID + "_" + tierID).observe('change', FixNumberQM);
    $("txtMinQM_" + ProductID + "_" + tierID).observe('keyup', FixNumberQM);

    $("txtMaxQM_" + ProductID + "_" + tierID).observe('blur', MaxQMBlur);
    $("txtMaxQM_" + ProductID + "_" + tierID).observe('keypress', IsNumericKey);
    $("txtMaxQM_" + ProductID + "_" + tierID).observe('change', FixNumberQM);
    $("txtMaxQM_" + ProductID + "_" + tierID).observe('keyup', FixNumberQM);

    $("slCommission_" + ProductID + "_" + tierID).observe('change', CommissionChange);

    $("chkMaxAmount_" + ProductID + "_" + tierID).observe('click', SetLastTier);
    $("chkMaxQM_" + ProductID + "_" + tierID).observe('click', SetLastTier);
    $("btnDelete_" + ProductID + "_" + tierID).observe('click', DeleteTier);

}
function DeleteTier(event) {
    var arr = this.id.split("_");
    if (this.className == IconDelete.Enabled) {

        //Enable previous Delete Tier if Tier>1
        if (arr[2] > 1) {
            $("btnDelete_" + arr[1] + "_" + (arr[2] - 1)).className = IconDelete.Enabled;
            $("chkMaxAmount_" + arr[1] + "_" + (arr[2] - 1)).className = CheckboxStatus.OffEnable;
            $("chkMaxQM_" + arr[1] + "_" + (arr[2] - 1)).className = CheckboxStatus.OffEnable;
        }
        //Enable button Add Tier
        $("btnAddTier" + arr[1]).className = IconAddTier.Enabled;

        //delete this Tier
        var productSetting = GetProductSetting(arr[1]);
        productSetting.minAmounts.splice(arr[2] - 1, 1);
        productSetting.minAmounts.splice(arr[2] - 1, 1);
        productSetting.maxAmounts.splice(arr[2] - 1, 1);
        productSetting.minQMs.splice(arr[2] - 1, 1);
        productSetting.maxQMs.splice(arr[2] - 1, 1);
        productSetting.commissions.splice(arr[2] - 1, 1);

        var table = $("tblTier" + arr[1]);
        table.deleteRow(arr[2]);
        var rowCount = table.rows.length;
        if (arr[2] == 1) {
            var sXmlBody = "<table><tbody id='tierList" + arr[1] + "'>" + noInfo + "</tbody></table>";
            var tempDiv = document.createElement("DIV");
            tempDiv.innerHTML = sXmlBody;
            $("tblTier" + arr[1]).removeChild($("tierList" + arr[1]));
            $("tblTier" + arr[1]).appendChild(tempDiv.firstChild.firstChild);
        }
    }

}
function SetLastTier(event) {
    var arr = this.id.split("_");
    var productSetting = GetProductSetting(arr[1]);

    if (this.className == CheckboxStatus.OnEnable) { //This Is Last Tier so uncheck it
        var maxAmount = productSetting.minAmounts[arr[2] - 1];
        var maxQM = productSetting.minQMs[arr[2] - 1];
        $("txtMaxAmount_" + arr[1] + "_" + arr[2]).disabled = "";
        $("txtMaxAmount_" + arr[1] + "_" + arr[2]).value = FormatNumber(maxAmount);
        $("txtMaxQM_" + arr[1] + "_" + arr[2]).disabled = "";
        $("txtMaxQM_" + arr[1] + "_" + arr[2]).value = FormatNumber(maxQM);
        $("chkMaxAmount_" + arr[1] + "_" + arr[2]).className = CheckboxStatus.OffEnable;
        $("chkMaxQM_" + arr[1] + "_" + arr[2]).className = CheckboxStatus.OffEnable;
        if (maxAmount >= maximumAmountNumber - 1 || maxQM >= maximumQMNumber - 1) {
            $("btnAddTier" + arr[1]).className = IconAddTier.Disabled;
        }
        else {
            $("btnAddTier" + arr[1]).className = IconAddTier.Enabled;
        }

        productSetting.maxAmounts[arr[2] - 1] = maxAmount;
        productSetting.maxQMs[arr[2] - 1] = maxQM;

    }
    else if (this.className == CheckboxStatus.OffEnable) { //Check it as Lastier
        $("txtMaxAmount_" + arr[1] + "_" + arr[2]).disabled = "disabled";
        $("txtMaxQM_" + arr[1] + "_" + arr[2]).disabled = "disabled";
        $("txtMaxAmount_" + arr[1] + "_" + arr[2]).value = "∞";
        $("txtMaxQM_" + arr[1] + "_" + arr[2]).value = "∞";
        $("chkMaxAmount_" + arr[1] + "_" + arr[2]).className = CheckboxStatus.OnEnable;
        $("chkMaxQM_" + arr[1] + "_" + arr[2]).className = CheckboxStatus.OnEnable;
        $("btnAddTier" + arr[1]).className = IconAddTier.Disabled;

        productSetting.maxAmounts[arr[2] - 1] = maximumAmountNumber;
        productSetting.maxQMs[arr[2] - 1] = maximumQMNumber;
    }


}


function AddTier(event) {
    if (this.className == IconAddTier.Enabled) {
        var ProductID = GetProductIDFromControlID(this, "btnAddTier");
        var productSetting = GetProductSetting(ProductID);
        var noTier = productSetting.commissions.length;
        var minAmount, maxAmount, minQM, maxQM, commission, newTierID;
        var sXmlBody = new StringBuilder("<table><tbody>")
        sXmlBody.append(TierRowTpl);
        sXmlBody.append("</tbody></table>");
        sXmlBody.replace("{ProductID_val}", ProductID);
        sXmlBody.replace("{disabled_txtMaxAmount_val}", "");
        sXmlBody.replace("{disabled_txtMaxQM_val}", "");

        if (noTier >= 50) {
            alert(Msg.InvalidSettingFor.replace('{item}',productSetting.productName) + ' ' + Msg.MaxTierAllow);
            return;
        }

        if (noTier == 0) {
            minAmount = maxAmount = minQM = maxQM = 0;
            commission = "0.00";
            newTierID = 1;
            sXmlBody.replace("{disabled_txtMinAmount_val}", "");
            sXmlBody.replace("{disabled_txtMinQM_val}", "");
            $("tblTier" + ProductID).deleteRow(1);
        }
        else {
            minAmount = productSetting.maxAmounts[noTier - 1] + 1;
            maxAmount = minAmount;
            minQM = productSetting.maxQMs[noTier - 1] + 1;
            maxQM = minQM;
            if (minAmount >= maximumAmountNumber - 1 || minQM >= maximumQMNumber - 1) {
                $("btnAddTier" + ProductID).className = IconAddTier.Disabled;
            }
            commission = productSetting.commissions[noTier - 1];
            newTierID = noTier + 1;
            sXmlBody.replace("{disabled_txtMinAmount_val}", "disabled");
            sXmlBody.replace("{disabled_txtMinQM_val}", "disabled");
            //Disable Delete button for previous Tier
            $("btnDelete_" + ProductID + "_" + noTier).className = IconDelete.Disabled;
            $("chkMaxAmount_" + ProductID + "_" + noTier).className = CheckboxStatus.OffDisable;
            $("chkMaxQM_" + ProductID + "_" + noTier).className = CheckboxStatus.OffDisable;
        }

        sXmlBody.replace("{tierID_val}", newTierID);
        sXmlBody.replace("{txtMinAmount_val}", FormatNumber(minAmount));
        sXmlBody.replace("{txtMaxAmount_val}", FormatNumber(maxAmount));
        sXmlBody.replace("{txtMinQM_val}", FormatNumber(minQM));
        sXmlBody.replace("{txtMaxQM_val}", FormatNumber(maxQM));
        sXmlBody.replace("{Commission_val}", commission);
        sXmlBody.replace("{chkMaxClass_val}", CheckboxStatus.OffEnable);

        sXmlBody.replace("{icon_delete_status_val}", IconDelete.Enabled);

        var tempDiv = document.createElement("DIV");
        tempDiv.innerHTML = sXmlBody.toString();
        $("tblTier" + ProductID).appendChild(tempDiv.firstChild.firstChild);
        delete tempDiv;
        delete sXmlBody;

        SetDropDownList($("slCommission_" + ProductID + "_" + newTierID), commission);
        //Cai Event Handler cho cac control
        AddEventHandlerForOneTierRow(ProductID, newTierID);
        //Update data
        productSetting.minAmounts.push(minAmount);
        productSetting.maxAmounts.push(maxAmount);
        productSetting.minQMs.push(minQM);
        productSetting.maxQMs.push(maxQM);
        productSetting.commissions.push(commission);

    }
}

function IsValidProductSetting(ProductID) {
    /*product duoc enabled phai co it nhat 1 tier, neu copy duoc check thi chi check copyproduct, tier cuoi cung co cac gia tri max= ∞ */
    if (GroupSetting.copyProductID != -1) {
        ProductID = GroupSetting.copyProductID;
    }
    var productSetting = GetProductSetting(ProductID);
    if (productSetting.commissions.length > 0) {

        if (productSetting.commissions.length > 50) {
            alert(Msg.InvalidSettingFor.replace('{item}', productSetting.productName) + ' ' + Msg.MaxTierAllow);
            return false;
        }
        //Check the last tier for maxAmount and maxQM
        if (productSetting.maxAmounts[productSetting.commissions.length - 1] < maximumAmountNumber || productSetting.maxQMs[productSetting.commissions.length.length - 1] < maximumQMNumber) {
            alert('Invalid setting for ' + productSetting.productName + ' ' + Msg.LastTierSettingHaveMaxValue);
            return false;
        }
    }
    else {
        alert(Msg.InvalidSettingFor.replace('{item}', productSetting.productName) + ' ' + Msg.TierSettingAtLeastTier);
        return false;
    }

    return true;
}
function IsValidSetting() {
    if (trim(GroupSetting.groupName) == "") {
        alert(Msg.GroupNameEmpty);
        $("txtGroupName").focus();
        return false;
    }

    //var qualifiedAmount = encodeURIComponent($('qualifiedAmount').value);
    var qualifiedAmount = ToFloat($('qualifiedAmount').value);

    if (qualifiedAmount == '' || qualifiedAmount == null || isNaN(qualifiedAmount) || qualifiedAmount < 0) {
        alert(Msg.ValidQualifiedAmount);
        $('qualifiedAmount').focus()
        return;
    }

    var count = 0;
    for (var i = 0; i < GroupSetting.productSetting.length; i++) {
        if (GroupSetting.productSetting[i].isEnabled) {
            count++;
            if (!IsValidProductSetting(GroupSetting.productSetting[i].id))
                return false;
        }
    }
    if (count == 0) {
        alert(Msg.EnableProduct);
        return false;
    }
    return true;
}

function SetDropDownList(elementRef, valueToSetTo) {
    var isFound = false;

    for (var i = 0; i < elementRef.options.length; i++) {
        if (elementRef.options[i].value == valueToSetTo) {
            elementRef.options[i].selected = true;
            isFound = true;
            return;
        }
    }
    if (isFound == false)
        elementRef.options[0].selected = true;
}

function CommissionChange(event) {
    //Gia tri thay doi phai luon nho hon bang gia tri cua tier truoc do  

    var previousCommissionVal = 0;
    var arr = this.id.split("_");
    var productSetting = GetProductSetting(arr[1]);
    var index = arr[2] - 1;
    var comm = this.options[this.selectedIndex].value;
    /*alert(this.options.length);*/
    if (index > 0) {
        previousCommissionVal = productSetting.commissions[index - 1];
    }
    if (comm < previousCommissionVal) {
        SetDropDownList(this, previousCommissionVal);
        comm = previousCommissionVal;
    }

    //Neu gia tri thay doi lon hon gia tri cua cac tier sau thi cac tier sau se mang gia tri nay

    for (var k = index + 1; k < productSetting.commissions.length; k++) {
        if (productSetting.commissions[k] < comm) {

            SetDropDownList($("slCommission_" + arr[1] + "_" + (k + 1)), comm);
            productSetting.commissions[k] = comm;
        }

    }
    productSetting.commissions[index] = comm;

}
function ChangeDeductionStatus(event) {
    var arr = this.id.split("_");
    var isEnabled = false;
    if (this.className == CheckboxStatus.OffEnable) {
        this.className = CheckboxStatus.OnEnable;
        isEnabled = true;
    }
    else {
        this.className = CheckboxStatus.OffEnable;
    }
    var productSetting = GetProductSetting(arr[1]);
    for (var j = 0; j < productSetting.deductions.length; j++) {
        if (productSetting.deductions[j].id == arr[2]) {
            productSetting.deductions[j].isCheck = isEnabled;
            return;
        }
    }

}
function CopySetting(event) {
    //Control phai duoc enable
    if (this.className == CheckboxStatus.OnEnable) //Khong copy setting, tra lai trang thai cho cac product enable
    {
        this.className = CheckboxStatus.OffEnable;
        var ProductID = GroupSetting.copyProductID;
        GroupSetting.copyProductID = -1;
        //cai lai event handler cho cac tab khac, icon khac ma co product enable
        //Thay doi mau cac product
        for (i = 0; i < GroupSetting.productSetting.length; i++) {
            if (ProductID != GroupSetting.productSetting[i].id && GroupSetting.productSetting[i].isEnabled) {
                $("icon_enable" + GroupSetting.productSetting[i].id).observe('click', ChangeProductStatus);
                $("collapse_expand" + GroupSetting.productSetting[i].id).observe('click', SwitchProductTab);
                $("chkCopy" + GroupSetting.productSetting[i].id).className = CheckboxStatus.OffEnable;
                $("product_color" + GroupSetting.productSetting[i].id).className = ProductStatusColor.Enabled;
            }
        }

    }
    else if (this.className == CheckboxStatus.OffEnable) //Copy Setting, remove copy cu neu co, va cac product enable o tinh trang khong edit duoc
    {
        this.className = CheckboxStatus.OnEnable;
        GroupSetting.copyProductID = GetProductIDFromControlID(this, "chkCopy");
        //Expand Current Product Tab
        $("tab_content" + GroupSetting.copyProductID).style.display = ProductTabSwitch.Expand;
        $("collapse_expand" + GroupSetting.copyProductID).className = ProductTabSwitchIcon.Expand;
        for (i = 0; i < GroupSetting.productSetting.length; i++) {
            if (GroupSetting.copyProductID != GroupSetting.productSetting[i].id && GroupSetting.productSetting[i].isEnabled) {
                $("icon_enable" + GroupSetting.productSetting[i].id).observe('click', DoNothing);
                $("collapse_expand" + GroupSetting.productSetting[i].id).observe('click', DoNothing);
                $("chkCopy" + GroupSetting.productSetting[i].id).className = CheckboxStatus.OffDisable;
                $("product_color" + GroupSetting.productSetting[i].id).className = ProductStatusColor.Disabled;
                $("tab_content" + GroupSetting.productSetting[i].id).style.display = ProductTabSwitch.Collapse;
                $("collapse_expand" + GroupSetting.productSetting[i].id).className = ProductTabSwitchIcon.Collapse;
            }
        }
    }
}
function DoNothing(event) {
    Event.stop(event);
}
function GetProductIDFromControlID(control, name) {
    return control.id.substring(name.length, control.id.length);
}
function ChangeProductStatus(event) {
    var ProductID = GetProductIDFromControlID(this, "icon_enable");
    
    Event.stop(event);
    
    var productSetting = GetProductSetting(ProductID);
    if (productSetting != null) {
        if (productSetting.isEnabled) { //Disable This Product
            var msg = Msg.ConfirmRemoveFromGroup;
            msg = msg.replace("{item}", productSetting.productName);

            if (confirm(msg)) {
                var tabContent = $("tab_content" + ProductID);
                tabContent.style.display = ProductTabSwitch.Collapse;
                $("collapse_expand" + ProductID).className = ProductTabSwitchIcon.Collapse;
                $("product_color" + ProductID).className = ProductStatusColor.Disabled;
                this.className = IconProductStatus.Disabled;
                productSetting.isEnabled = false;
                this.title = EnableProductExplainTpl.replace(/{ProductName_val}/, productSetting.productName);
                $("chkCopy" + ProductID).className = CheckboxStatus.OffDisable;
                $("chkAll").className = CheckboxStatus.OffEnable;
                //This Product Is for Copy Setting --> Reenable other Copy Checkbox and Tab
                if (GroupSetting.copyProductID == ProductID) {
                    for (var i = 0; i < GroupSetting.productSetting.length; i++) {
                        if (ProductID != GroupSetting.productSetting[i].id && GroupSetting.productSetting[i].isEnabled) {
                            $("icon_enable" + GroupSetting.productSetting[i].id).observe('click', ChangeProductStatus);
                            $("collapse_expand" + GroupSetting.productSetting[i].id).observe('click', SwitchProductTab);
                            $("chkCopy" + GroupSetting.productSetting[i].id).className = CheckboxStatus.OffEnable;
                            $("product_color" + GroupSetting.productSetting[i].id).className = ProductStatusColor.Enabled;
                        }
                    }
                    GroupSetting.copyProductID = -1;
                }
                //Disable check box copy when have only one product enabled
                var enabledProductID = -1, count = 0;
                for (var i = 0; i < GroupSetting.productSetting.length; i++) {
                    if (GroupSetting.productSetting[i].isEnabled) {
                        count++;
                        enabledProductID = GroupSetting.productSetting[i].id;
                    }
                }
                if (count == 1) {
                    $("chkCopy" + enabledProductID).className = CheckboxStatus.OffDisable;
                    GroupSetting.copyProductID = -1;
                }
            }


        }
        else //Enable this product
        {
            productSetting.isEnabled = true;
            this.className = IconProductStatus.Enabled;
            this.title = DisableProductExplainTpl.replace(/{ProductName_val}/, productSetting.productName);
            if (GroupSetting.copyProductID == -1) {

                $("product_color" + ProductID).className = ProductStatusColor.Enabled;
                var tabContent = $("tab_content" + ProductID);
                tabContent.style.display = ProductTabSwitch.Expand;
                $("collapse_expand" + ProductID).className = ProductTabSwitchIcon.Expand;
                //Enable other checkbox copy when this product is enabled   
                var count = 0;
                for (var i = 0; i < GroupSetting.productSetting.length; i++) {
                    if (GroupSetting.productSetting[i].isEnabled) {
                        count++;
                    }
                }
                if (count > 1) {
                    for (var i = 0; i < GroupSetting.productSetting.length; i++) {
                        if (GroupSetting.productSetting[i].isEnabled) {
                            $("chkCopy" + GroupSetting.productSetting[i].id).className = CheckboxStatus.OffEnable;
                        }
                    }
                }
                else {
                    $("chkCopy" + ProductID).className = CheckboxStatus.OffDisable;
                }
            }

            if (IsAllProductEnabled()) {
                $("chkAll").className = CheckboxStatus.OnEnable;
            }
        }
    }


}
function SwitchProductTab(event) {
    var ProductID = GetProductIDFromControlID(this, "collapse_expand");
    var productSetting = GetProductSetting(ProductID);
    if ((GroupSetting.copyProductID == ProductID || GroupSetting.copyProductID == -1) && productSetting != null && productSetting.isEnabled) {
        var tabContent = $("tab_content" + ProductID);
        if (tabContent.style.display == ProductTabSwitch.Collapse) {
            tabContent.style.display = ProductTabSwitch.Expand;
            this.className = ProductTabSwitchIcon.Expand;
        }
        else {
            this.className = ProductTabSwitchIcon.Collapse;
            tabContent.style.display = ProductTabSwitch.Collapse;
        }
    }
}
function GetProductSetting(ProductID) {
    for (i = 0; i < GroupSetting.productSetting.length; i++) {
        if (GroupSetting.productSetting[i].id == ProductID) {
            return GroupSetting.productSetting[i];
        }
    }
    return null;
}
function IsAllProductEnabled() {
    var count = 0;
    for (var i = 0; i < GroupSetting.productSetting.length; i++) {
        if (GroupSetting.productSetting[i].isEnabled) {
            count++;
        }
    }
    return (GroupSetting.productSetting.length == count);
}
function SwitchAllProductsStatus(event) {    
    //Disable all products    
    if (this.className == CheckboxStatus.OnEnable) {
        var i;
        for (i = 0; i < GroupSetting.productSetting.length; i++) {
            if (GroupSetting.productSetting[i].isEnabled) {
                fireEvent($("icon_enable" + GroupSetting.productSetting[i].id), "click");
            }
        }
        var count = 0;
        for (i = 0; i < GroupSetting.productSetting.length; i++) {
            if (!GroupSetting.productSetting[i].isEnabled) {
                count++;
            }
        }
        if (count == GroupSetting.productSetting.length) {
            this.className = CheckboxStatus.OffEnable;

        }
    }
    else { //Enable all products
        
        var i;
        for (i = 0; i < GroupSetting.productSetting.length; i++) {
            if (!GroupSetting.productSetting[i].isEnabled) {
                fireEvent($("icon_enable" + GroupSetting.productSetting[i].id), "click");
            }
        }
        var count = 0;
        for (i = 0; i < GroupSetting.productSetting.length; i++) {
            if (!GroupSetting.productSetting[i].isEnabled) {
                count++;
            }
        }
        if (count == GroupSetting.productSetting.length) {
            this.className = CheckboxStatus.OnEnable;

        }
    }
}
function FixNumberAmount(event) {
    FixNumber(this, maximumAmountNumber);
}

function FixNumberAmountOnly(event) {
    FixNumberOnly(this, maximumAmountNumber);
}

function FixNumberQM(event) {
    FixNumber(this, maximumQMNumber);
}
function ReverseString(text) {
    var splitext = text.split("");
    var revertext = splitext.reverse();
    var reversed = revertext.join("");
    return reversed;
}

function FixNumber(obj, max) {
    var result = '';
    for (var i = 0; i < obj.value.length; i++) {
        if (!isNaN(obj.value.charAt(i)))
            result += obj.value.charAt(i);
    }
    obj.value = eval(result);
    var arr = obj.id.split("_");
    var productSetting = GetProductSetting(arr[1]);
    var m = max - 1 - (productSetting.commissions.length - arr[2]);
    if (eval(obj.value) > m)
        obj.value = m;
    obj.value = FormatNumber(obj.value);

}

function FixNumberOnly(obj, max) {
    var result = '';
    for (var i = 0; i < obj.value.length; i++) {
        if (!isNaN(obj.value.charAt(i)))
            result += obj.value.charAt(i);
    }
    obj.value = eval(result);
    //var arr = obj.id.split("_");
    //var productSetting = GetProductSetting(arr[1]);
    //var m = max - 1 - (productSetting.commissions.length - arr[2]);
    if (eval(obj.value) > max)
        obj.value = max;
    obj.value = FormatNumber(obj.value);

}

function FormatNumber(value) {
    //Them dau ,
    if (value == 'undefined') {
        return "";
    }
    var result = '';
    result += value;
    var t = '';
    if (result.length > 0) {
        var count = 0;
        for (var k = result.length - 1; k >= 0; k--) {
            t += result.charAt(k);
            count++;
            if (count != result.length && count % 3 == 0)
                t += ',';
        }
    }
    return ReverseString(t);
}
function IsNumericKey(e) {
    e = (e) ? e : window.event;
    var key = (e.keyCode) ? e.keyCode : e.which;
    if (key == 8 || key == 9 || key == 13 || key == 17 || key == 18 || (key >= 37 && key <= 40) || key == 46 || (key >= 48 && key <= 57))
        bol = true;
    else
        bol = false;

    return bol;
}

function trim(stringToTrim) {
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}
function ltrim(stringToTrim) {
    return stringToTrim.replace(/^\s+/, "");
}
function rtrim(stringToTrim) {
    return stringToTrim.replace(/\s+$/, "");
}


function MinBlur(event) {
    //Kiem tra gia tri phai co neu khong gan no bang 0 hoac day la tier dau tien thi gia tri luon bang 0  
    var arr = this.id.split("_");
    if (trim(this.value) == "" || arr[2] == 1) {
        this.value = 0;
    }
    var productSetting = GetProductSetting(arr[1]);
    if (this.id.indexOf("txtMinAmount") > -1) {
        productSetting.minAmounts[arr[2] - 1] = ToFloat(this.value);
    }
    else {
        productSetting.minQMs[arr[2] - 1] = ToInt32(this.value);
    }
}
function MaxAmountBlur(event) {
    //Gia tri MaxAmount phai luon lon hon hoac bang gia tri Min Amount va nho hon gia tri maximum
    var arr = this.id.split("_");
    var productSetting = GetProductSetting(arr[1]);
    if (trim(this.value) == "") {
        this.value = FormatNumber(productSetting.maxAmounts[arr[2] - 1]);
        return;
    }

    if (ToFloat(this.value) == productSetting.maxAmounts[arr[2] - 1])
        return;
    var minAmount = productSetting.minAmounts[arr[2] - 1];
    if (ToFloat(this.value) < minAmount || ToFloat(this.value) >= maximumAmountNumber) {
        this.value = FormatNumber(minAmount);
    }
    productSetting.maxAmounts[arr[2] - 1] = ToFloat(this.value);
    //Tang gia tri cua min Amount sau len 1 neu co
    var previousMaxAmount = productSetting.maxAmounts[arr[2] - 1];

    for (var k = parseInt(arr[2]); k < productSetting.maxAmounts.length; k++) {
        if (productSetting.minAmounts[k] != (previousMaxAmount + 1)) {
            productSetting.minAmounts[k] = ++previousMaxAmount;
            var tierID = k + 1;
            //alert(previousMaxAmount);
            $("txtMinAmount_" + arr[1] + "_" + tierID).value = FormatNumber(productSetting.minAmounts[k]);
            if (productSetting.maxAmounts[k] != maximumAmountNumber && productSetting.maxAmounts[k] < productSetting.minAmounts[k]) {
                productSetting.maxAmounts[k] = previousMaxAmount;
                $("txtMaxAmount_" + arr[1] + "_" + tierID).value = FormatNumber(productSetting.maxAmounts[k]);
            }
            else {
                return;
            }
            previousMaxAmount = productSetting.maxAmounts[k];
        }
    }
    if (productSetting.minAmounts[productSetting.maxAmounts.length - 1] >= maximumAmountNumber - 1) {
        $("btnAddTier" + arr[1]).className = IconAddTier.Disabled;
    }
}

function MaxQMBlur(event) {
    //Gia tri MaxQM phai luon lon hon hoac bang gia tri Min QM va nho hon gia tri maximum
    var arr = this.id.split("_");
    var productSetting = GetProductSetting(arr[1]);
    if (trim(this.value) == "") {
        this.value = FormatNumber(productSetting.maxQMs[arr[2] - 1]);
        return;
    }

    if (ToInt32(this.value) == productSetting.maxQMs[arr[2] - 1])
        return;
    var minQM = productSetting.minQMs[arr[2] - 1];
    if (ToInt32(this.value) < minQM || ToInt32(this.value) >= maximumQMNumber) {
        this.value = FormatNumber(minQM);
    }
    productSetting.maxQMs[arr[2] - 1] = ToInt32(this.value);
    //Tang gia tri cua min Amount sau len 1 neu co
    var previousMaxQM = productSetting.maxQMs[arr[2] - 1];

    for (var k = parseInt(arr[2]); k < productSetting.maxQMs.length; k++) {
        if (productSetting.minQMs[k] != (previousMaxQM + 1)) {
            productSetting.minQMs[k] = ++previousMaxQM;
            var tierID = k + 1;
            //alert(previousMaxAmount);
            $("txtMinQM_" + arr[1] + "_" + tierID).value = FormatNumber(productSetting.minQMs[k]);
            if (productSetting.maxQMs[k] != maximumQMNumber && productSetting.maxQMs[k] < productSetting.minQMs[k]) {
                productSetting.maxQMs[k] = previousMaxQM;
                $("txtMaxQM_" + arr[1] + "_" + tierID).value = FormatNumber(productSetting.maxQMs[k]);
            }
            else {
                return;
            }
            previousMaxQM = productSetting.maxQMs[k];
        }
    }
    if (productSetting.minQMs[productSetting.maxQMs.length - 1] >= maximumQMNumber - 1) {
        $("btnAddTier" + arr[1]).className = IconAddTier.Disabled;
    }
}
function ToInt32(value) {
    return parseInt(ReplaceAll(value, ',', ''));
}
function ToFloat(value) {
    return parseFloat(ReplaceAll(value, ',', ''));
}

function ReplaceAll(str, findStr, replacingStr) {
    return str.replace(eval('/' + findStr + '/g'), replacingStr);
}