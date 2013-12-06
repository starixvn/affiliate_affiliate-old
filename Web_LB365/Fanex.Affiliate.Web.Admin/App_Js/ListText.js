// JScript File
var curBannerID = -1;
var curTR;
var curNoRow = -1;
function DeleteSelectedText() {

    var bannerIDList = "";
    var fileList = "";
    listChk = $$("div .chk");
    var i = 0;
    var count = 0;
    for (i = 0; i < listChk.length; i++) {
        if (listChk[i].hasClassName("ic-check")) {
            count++;
            var id = listChk[i].id;
            if (bannerIDList != "")
                bannerIDList += ",";
            bannerIDList += id;
        }
    }
    if (count == 0) {
        alert("Please select banner to delete.");
        return;
    }
    else {
        if (confirm($("hdConfirmDelete").value)) {
            DeleteMedia(bannerIDList, fileList);
        }
    }
}
function DeleteMedia(bannerIDList, fileList) {
    try {
        var params;
        params = "params=" + bannerIDList;
        var ajax = new Ajax.Request("../Handler/DelBannerText.ashx", {
            method: 'post',
            parameters: params,
            onComplete: function (data) {
                if (data.responseText != "") {
                    $("frmSearch").submit();
                    alert(data.responseText);
                }
                else {
                    NavigatePage(1);
                }
            }
        });

    }
    catch (e) {
        alert(e);
    }

}
function CheckUnCheckAll(chkAll) {
    var isChkAll = false;
    if (chkAll.checked) {
        isChkAll = true;
    }
    $("btnDelete").disabled = !isChkAll;
    for (var i = minNo; i <= maxNo; i++) {
        $("chk" + i).checked = isChkAll;
    }
}
function CheckUnCheck(chk) {
    if (!chk.checked) {
        if ($("chkAll").checked) {
            $("chkAll").checked = false;
        }
        for (var i = minNo; i <= maxNo; i++) {
            if ($("chk" + i).checked) {
                $("btnDelete").disabled = false;
                return;
            }
        }
        $("btnDelete").disabled = true;
        return;
    }

    if (chk.checked) {
        $("btnDelete").disabled = false;
        for (var i = minNo; i <= maxNo; i++) {
            if (!$("chk" + i).checked) {
                return;
            }
        }
    }
}
function NavigatePage(curPage) {
    SubmitSearchForm();
    //window.location.reload();
    //GetDataBannerList(curPage);
    //curBannerID = -1;
}

function GetDataBannerList(page) {
    var ajax = new Ajax.Request("../Handler/GetDataBannerText.ashx", {
        method: 'post',
        parameters: 'cate=' + $('cateoption').value + '&lang=' + $('langoption').value + '&currency=' + $('currencyoption').value + '&type=' + "&page=" + page + "&PageSize=" + $('slPageSize').value,
        onComplete: function (data) {
            ProcessResult(data.responseText);
        }
    });
}

function ProcessResult(data) {
    try {        
        var arr = data.split("$$$");
        minNo = parseInt(arr[0]);
        maxNo = parseInt(arr[1]);
        var sXmlBody = "<table><tbody id='result'>" + arr[2] + "</tbody></table>";
        var tempDiv = document.createElement("DIV");
        tempDiv.innerHTML = sXmlBody;
        $("tblResult").removeChild($("result"));
        $("tblResult").appendChild(tempDiv.firstChild.firstChild);
        InitDivChkBox();
        if (maxNo == 0) {
            if ($$("div .pagesize") != null && $$("div .divpageleft") != null && $$("div .header_01") != null) {
                $$("div .pagesize")[0].style.display = "none";
                $$("div .divpageleft")[0].style.display = "none";
                $$("div .header_01")[0].style.display = "none";
                $$("div .header_01")[1].style.display = "none";
            }
            $("btnDelete").style.display = "none";
        }
        else {
            $("btnDelete").style.display = "";
        }
    }
    catch (e) {
        alert(e);
    }
}

function IsValidFileExtension() {
    var filename = $('txtfile').value;
    var isOK = true;
    if (filename != "") {
        var ext = filename.substring(filename.lastIndexOf('.') + 1, filename.length).toLowerCase();

        isOK = (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "gif" || ext == "swf");
        if (!isOK) {
            alert($('hdErrFileExtension').value);
        }
        else {
            if (ext == "swf") {
                $('slImageType').value = "Flash";
            }
            else {
                $('slImageType').value = "Image";
            }
        }
    }
    if ($('btnuploadbanner')) {
        $('btnuploadbanner').disabled = !isOK;
    }
    return isOK;

}

function Cancelfunction() {
    setTimeout("ClosePopup();", 500);
}


function SubmitSearchForm() {
    $("frmSearch").method = "get";
    $("frmSearch").action = "ListText.aspx";
    $("frmSearch").target = "";
    $("page").value = "1";
    $("frmSearch").submit();
}
function SubmitPaging() {
    $("frmSearch").method = "get";
    $("frmSearch").action = "ListText.aspx";
    $("frmSearch").target = "";
    $("frmSearch").submit();
}
