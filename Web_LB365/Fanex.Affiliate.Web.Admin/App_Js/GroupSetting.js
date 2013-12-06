window.onload = Initialize;
var time = 0; //set Timeout for clock refresh
var timer = null; //set Timeout for choose refresh time
var timePage = ""; //set Timeout for go to page
function Initialize() {

    $("slTime").value = $("timer").value;

    if ($("slTime").value != "0") {
        time = parseInt($("slTime").value);
        setInterval("CountDown();", 1000);
    }
    
   /* EnableDisableSearchDate($("hdchkDate").value);

    $("fdate").value = $("hdfDate").value;
    $("tdate").value = $("hdtDate").value;*/


    //Event Handler for related controls in the web page
    var listEdit = document.getElementsByClassName("icedit");
    for (var i = 0; i < listEdit.length; i++) {
        listEdit[i].observe('click', editOnClick);
    }
    var listDelete = document.getElementsByClassName("icdelete");
    for (var i = 0; i < listDelete.length; i++) {
        if (listDelete[i].id != '') {
            listDelete[i].observe('click', deleteOnClick);
        }
    }

    var listTR = document.getElementsByClassName("TR");
    for (var i = 0; i < listTR.length; i++) {
        var arr = listTR[i].id.split('_');
        if(arr[1]=="NoComm") continue;
        listTR[i].style.backgroundColor = arr[1];
        listTR[i].observe('mouseout', function(event) { var t = this.id.split('_'); this.style.backgroundColor = t[1]; });
        listTR[i].observe('mouseover', function(event) { this.style.backgroundColor = overColor; });
    }

   var link=document.getElementsByClassName("link");
   for (var i = 0; i < link.length; i++) {
        link[i].observe('click', linkOnClick);        
    }

    /*$("chkDate").observe('click', EnableDisableSearchDate);
        $("fdate").observe('blur', FixValidDate);
    $("tdate").observe('blur', FixValidDate);
     */
    $("txtGroupName").observe('keypress', PreSearch);    
    $('txtGroupName').blur();
    $('txtGroupName').focus();


}
function linkOnClick(event) {
    var id = "";
    if (this.id.indexOf("_NoComm") > 0) {
        id = this.id.split('_')[0].substring(1, this.id.length);
        location.href = "../UserInfo/AffiliateList.aspx?groupid=" + id + "&txtGroupName=" + this.parentNode.previousSibling.innerHTML.trim() + "&isClosed=true";
    }
    else {
        id = this.id.substring(1, this.id.length);
        location.href = "../UserInfo/AffiliateList.aspx?groupid=" + id + "&txtGroupName=" + this.parentNode.previousSibling.innerHTML.trim();
    }
    
}

function AddNewGroup() {
    ibcWnd.open("AddGroup.aspx?groupid=", 'Add a Commission Group', 150, 50, 755, 529, 'class1', 'class2');

}

function editOnClick(event) {
    var element = Event.element(event);
    element.readOnly = true;
    /*
        Firefox width/height = 740/491
        IE =740/492
    */
    ibcWnd.open("EditGroup.aspx?groupid=" + element.id.substring(1, element.id.length ), 'Edit Commission Group', 150, 50, 755, 529, 'class1', 'class2');
    
    element.readOnly = false;
    }
    function deleteOnClick(event) {    
    this.readOnly = true;
    var arr = this.id.split('_');    
    if (confirm('Are you sure you want to delete the commission group "'+ $("g" + arr[0]).innerHTML + '" ?')) {
    DelayReloadPage1();
    var ajax = new Ajax.Request("../Handler/CommGroupDel.ashx", {
    method: 'post',
    parameters: "params=" + arr[0],
    onComplete: function(data) {
    var result = data.responseText;
    if (result != "") {
    alert(result);
    }
    else {

        location.href = location.href.replace("page=" + $("page").value, "page=1");
    }
    HideDelayReloadPage();
    }
    });
        
        
    }    
    this.readOnly = false;
    }
    function EnableDisableSearchDate(arg) {
    /*Enable search by date */
    var isEnabled = 0;
    var chkbox = $("chkDate");
    if (typeof (arg) == "string") {
        isEnabled = arg;
    }
    else {

        if (chkbox.hasClassName("chkbox-on"))
            isEnabled = 0;
        else
            isEnabled = 1;
    }
    if (isEnabled > 0) {

        chkbox.className = "chkbox-on";
        $("hdchkDate").value = 1;
        $("fdate").disabled = false;
        $("tdate").disabled = false;
        $("fdateTrigger").disabled = false;
        $("tdateTrigger").disabled = false;

        Calendar.setup({
            inputField: "fdate",
            ifFormat: "mm/dd/y",
            button: "fdateTrigger",
            singleClick: true,
            showsTime: false
        });

        Calendar.setup({
            inputField: "tdate",
            ifFormat: "mm/dd/y",
            button: "tdateTrigger",
            singleClick: true,
            showsTime: false
        });

    }
    else {/*Disable search by date */

        $("hdchkDate").value = 0;
        chkbox.className = "chkbox-off";
        $("fdate").disabled = true;
        $("tdate").disabled = true;
        $("fdateTrigger").disabled = true;
        $("tdateTrigger").disabled = true;
        $("fdateTrigger").onclick = "";
        $("tdateTrigger").onclick = "";
    }


}
function IsDate(str) {
    try {
        var myDate = new Date(dateString);
        return true;
    } catch (e) {
        return false;
    }
}
function FixValidDate(event) {
    var obj = Event.element(event);
    if (!IsDate(obj.value)) {
        obj.value = ctrInput;
    }
}



function DoCal(url) {
    ibcWnd.open(url, 'Message Details', 150, 50, 500, 320, 'class1', 'class2');
}

function PreSearch(e) {
    e = (e) ? e : window.event;
    var key = (e.keyCode) ? e.keyCode : e.which;
    if (key == 13) {
        LoadData(1);
    }
}
    
