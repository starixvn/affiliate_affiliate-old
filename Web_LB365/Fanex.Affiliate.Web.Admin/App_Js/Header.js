var currentMenu = "menu1";

var strMonth = new Array('Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec');
function isDate(TDY, TDM, TDD) 
{
    var BegDate = new Date(parseFloat(TDY) + "/" + TDM + "/" + TDD);
    if (BegDate.getYear() < 200)
        var TBegYear = BegDate.getYear() + 1900
    else var TBegYear = BegDate.getYear()
    if (TBegYear != parseFloat(TDY) || BegDate.getMonth() + 1 != parseFloat(TDM) || BegDate.getDate() != parseFloat(TDD))
        return false;
    else return true;
}

function clock() {
    sec++;
    if (sec == 60) {
        sec = 0;
        min++;
    }
    if (min == 60) {
        min = 0;
        hrs++;
    }
    if (hrs == 24) {
        hrs = 0;
        sec = 0;
        min = 0;
        day++;
    }
    if (min < 10) {
        strmin = "0" + min;
    }
    else {
        strmin = min;
    }
    if (sec < 10) {
        strsec = "0" + sec;
    }
    else {
        strsec = sec;
    }

    if (!isDate(year, month, day))   
    {
        month++;
        day = 1;
    }
    if (!isDate(year, month, day))   
    {
        year++;
        month = 1;
        day = 1;
    }

    if (hrs < 10) {
        strhrs = "0" + hrs;
    }
    else {
        strhrs = hrs;
    }
    
    var str = strhrs + ":" + strmin + ":" + strsec + " " + strMonth[month - 1] + " " + day + ", " + year + gmt_offset;

    $('lblDateTime').innerHTML = str;
}

var _clickButton = 0;
var ctrlKey = false;
function MenuOnMouseDown(event) {
    ctrlKey = event.ctrlKey;
    _clickButton = event.which || event.button;    
}

function ActiveMenuHead(id) {
    try {
        
        ActiveMenu(id);
        
        if(_clickButton <2 && !ctrlKey) // Midde button : IE8 - 4, IE9 - 2
            parent.fMain.DelayReloadPage1();

    }
    catch (o) {
        console.log(o);
    }
}
function ActiveMenu(id) {
    if (id == currentMenu)
        return;
    var currMenuControl = $$('.current');
    if (currMenuControl.length > 0)
        currMenuControl[0].className = "";
    
    $(id).className = 'current';
    currentMenu = id;
}


function ExcLogout() {
    parent.location.href = "Handler/Logout.ashx";
}

function KickedOut() {    
    var ajax = new Ajax.Request("Auth/ReCheckIn.ashx", {
        method: 'get',    
        contentType:'application/html' ,
        onComplete: function (datastran) {
           
            if (datastran.responseText == 'Expired') {
                alert("Another session has logged in or your session is expired. Force to logout!");
                ExcLogout();
            } else if (datastran.responseText == 'UM') {
                parent.location.href = "UnderMaintenance.aspx";
            }
        }
    });
}
function Init() {
    
    setInterval("clock()", 1000);
    setInterval('KickedOut();', 15000);
}

window.onload = function(){ Init();};
