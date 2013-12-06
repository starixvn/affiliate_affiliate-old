Common = window.Common || {};

Common.isValidUserName = function (str) {
    return str.match(/^[a-zA-Z0-9]+$/) != null;
}
function openMailLink(url) {
    window.open(url);

}
function obj(id){
	return document.getElementById(id);
}
Common.updateURLParameter = function (uri, key, value) {
    var re = new RegExp("([?|&])" + key + "=.*?(&|$)", "i");
    separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
};


Array.prototype.clear =function(){
	if (this.length >0) this.splice(0,this.length);  
}
String.prototype.isNumber =function() {        
    var regs= new RegExp(/^-?\d+(\.\d+)?\s*$/);    
    return (regs.test(this));
}
function setvaluefocus(e, isok, id) {
    if (isok == 1) {
        if (e.value == $(id).value)
            e.value = '';
    }
    else
        if (e.value == '')
            e.value = $(id).value;
}

StringBuilder=function(initialText) { 
	var _parts=new Array();
	if (initialText) _parts.append(initialText);		  
	this.append=function(text){
		if(text) _parts.push(text);
	}
	this.appendLine=function(text){
		this.append(text);
		this.append("\r\n");
	}
	this.clear =function(){ 
		_parts.clear();
	}	
	this.isEmpty=function(){ 
		return (_parts.length==0); 
	}
	this.toString=function(delimiter){
		delimiter=delimiter || '';
		return _parts.join(delimiter);
	}
}

Number.prototype.toDecimals=function(n){
    n=(isNaN(n)) ? 2: n;
    var num=Math.pow(10,n);
    function pad(s) {
        s=s||'.';
        return (s.length>2) ? s: pad(s+'0');
    }
    return (isNaN(this)) ? this: (new String(Math.round(this*num)/num)).replace(/(\.\d*)?$/,pad);
}

String.prototype.startsWith =function(prefix){
  return (this.substr(0,prefix.length)==prefix);
}

String.prototype.endsWith =function(suffix){
  return (this.substr(this.length -suffix.length)==suffix);
}

String.prototype.contains =function(item){
	return (this.indexOf(item) >=0);
}

String.prototype.trimLeft =function(){
  return this.replace(/^\s*/,"");
}

String.prototype.trimRight =function(){
  return this.replace(/\s*$/,"");
}

String.prototype.trim =function(){
  return this.trimRight().trimLeft();
}

String.prototype.compare =function(args, isIgnoreCase){		
	if(isIgnoreCase) {
		return ((new String(this.toLowerCase())==(new String(args)).toLowerCase()) ?0 :1);		
	}
	return ((this ==args) ?0 :1);
}

String.format =function(format){
  for (var i =1;i <arguments.length;i++){
    format =format.replace("{"+(i -1)+"}",arguments[i]);
  }
  return format;
}
function checkMail(value)
{
	var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	if (filter.test(value))
	    return true;
	else 
	    return false;
}
function isEmpty(value)
{
    if(value == null || value.trim() == "")
        return true;
    else
        return false;
}
function indexOfRadioChecked(name)
{
    var radios = document.getElementsByName(name);
    var length = radios.length;
    for(i = 0; i < length; i++)
        if(radios[i].checked ==1)
            return i;
}
function valueOfRadioChecked(name)
{
    var radios = document.getElementsByName(name);
    var length = radios.length;
    for(i = 0; i < length; i++)
        if(radios[i].checked == 1)
            return i;
}
function formatNumber(number){
	var str =String(number);	
	var end_i =str.indexOf('.');
	
	if (str.indexOf(',') >0) return str;	
	var endstr ="";
	if (end_i !=-1){
		endstr =str.substring(end_i);
		var t =str.substring(0, str.length -end_i);
		if (t.length <4) return str;		
	}
	else {
		end_i =str.length;
	}
	
	var count =0;
	var result ="";
	for (i =end_i -1; i >=0; i--){		
		if (count >2){		
			result =str.charAt(i) +"," +result;
			count =0;
		}
		else{
			result =str.charAt(i) +result;		
		}
		count++;
	}	
	return (result +endstr);
}

/**/
function toNumber(str){
	var value ="";
	for (i =0; i <str.length; i++) {
		var c =str.charAt(i);
		if (c !=',') value +=c;		
	}
	return value;
}
//-----show date
function showdate()
{
    if($('day').innerHTML!=null)
    {
        var strMonth = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
        var dtime=new Date();
        var time =(dtime.toTimeString().substr(0,8)).toString(); 
        var d=strMonth[dtime.getMonth()] + " " + dtime.getDate() + ", " + dtime.getFullYear() + " GMT +8"; 
        dtime=time.split(':');
        var AP='AM';
        if(dtime[0] >11)AP='PM';
        if(dtime[0] ==0) dtime[0] =12;       
        if(dtime[0] >12) dtime[0] -=12;
        $('day').innerHTML=dtime.join(':') + " " + AP + " " + d; 
        setTimeout("showdate();",1000);
    }
    else
    {
        setTimeout("showdate();",10000);
    }
}
//--------------
function MM_openBrWindow(theURL,winName,features)
{ 
    window.open(theURL,winName,features);
}
function ShowError(errMsgs,tagIds,placeHolder)
{	
	window.scroll(0,0); 
	//parent.scroll(0,0); 
	var errHtml="";	
	$(placeHolder).style.display=(errMsgs.length==0)?'none':'';
	for(var i=0;i<errMsgs.length;++i){
	    errHtml+="<li><a class='err-msg' href='' onclick=\"$('"+tagIds[i]+"').focus(); return false;\">"+errMsgs[i]+"</a></li>";
	}
	$(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
	setTimeout(function(){$('errMsg').hide()},3000);
}
function ShowError1(errMsgs,tagIds,placeHolder)
{	
	window.scroll(0,0); 
	var errHtml="";	
	$(placeHolder).style.display=(errMsgs.length==0)?'none':'';
	for(var i=0;i<errMsgs.length;++i){
	    errHtml+="<li><a class='err-msg' href='' onclick=\"$('"+tagIds[i]+"').focus(); return false;\">"+errMsgs[i]+"</a></li>";
	}
	$(placeHolder).innerHTML = "<ul>" + errHtml + "</ul><br />";
}

//-------------------------------------------------------------------------
// JScript File
var IE = document.all?true:false;
// utility functions --------------------------
IBC = [];
IBC.addEvent = function(el, evname, func) {
	if (el.attachEvent) { // IE
		el.attachEvent("on" + evname, func);
	} else if (el.addEventListener) { // Gecko / W3C
		el.addEventListener(evname, func, true);
	} else { // Opera (or old browsers)
		el["on" + evname] = func;
	}
}

IBC.removeEvent = function(el, evname, func) {
	if (el.detachEvent) { // IE
		el.detachEvent("on" + evname, func);
	} else if (el.removeEventListener) { // Gecko / W3C
		el.removeEventListener(evname, func, true);
	} else { // Opera (or old browsers)
		el["on" + evname] = null;
	}
}

IBC.calculateViewport = function()
{
    window.viewport = [];
    if(!this.is_ie)
    {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.scrollWidth !=
         'undefined' && document.documentElement.scrollWidth != 0)
        {
           window.viewport.width = document.documentElement.scrollWidth;
           window.viewport.height = document.documentElement.scrollHeight;
        }        

        window.viewport.width = Math.max(window.viewport.width, (Math.max(document.body.scrollWidth, document.body.clientWidth)));
        window.viewport.height = Math.max(window.viewport.height, (Math.max(document.body.scrollHeight, document.body.clientHeight)));
    }
    else
    {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.clientWidth !=
         'undefined' && document.documentElement.clientWidth != 0)
        {
           window.viewport.width = document.documentElement.clientWidth;
           window.viewport.height = document.documentElement.clientHeight;
        }
        // older versions of IE
        else
        {
           window.viewport.width = document.getElementsByTagName('body')[0].clientWidth;
           window.viewport.height = document.getElementsByTagName('body')[0].clientHeight;
        }

        window.viewport.width = Math.max(document.body.scrollWidth, window.viewport.width);
        window.viewport.height = Math.max(document.body.scrollHeight, window.viewport.height);
    }
}

IBC.createElement = function(type, parent) {
	var el = null;
	if (document.createElementNS) {
		// use the XHTML namespace; IE won't normally get here unless
		// _they_ "fix" the DOM2 implementation.
		el = document.createElementNS("http://www.w3.org/1999/xhtml", type);
	} else {
		el = document.createElement(type);
	}
	if (typeof parent != "undefined") {
		parent.appendChild(el);
	}
	return el;
}

//--------------------------------------------
var timeDelay = "";
function CreateMaskLoadingDiv()
{

    window.divMaskLoading = IBC.createElement("div");
    var div1 = IBC.createElement("div");
    
    window.divMaskLoading.className = "divframeloading"
    
    window.divMaskLoading.style.filter = 'alpha(opacity=50)';
    window.divMaskLoading.style.opacity = '0.5';
    window.divMaskLoading.appendChild(div1);
    document.body.appendChild(window.divMaskLoading);
    
}

function DelayReloadPage(id,data)
{
    if(!window.divMaskLoading) 
        CreateMaskLoadingDiv();
    if(!window.viewport)
        IBC.calculateViewport();
    window.divMaskLoading.style.width = (window.viewport.width - 20) + 'px';
    window.divMaskLoading.style.height = window.viewport.height + 'px';
    var div1 = IBC.createElement("div");
    window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width/2) + 'px';
    window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';
    
    window.divMaskLoading.style.display = 'block';    
    setTimeout(function(){window.divMaskLoading.style.display = 'none';$(id).innerHTML= data;},500);
}
function DelayReloadPage1()
{
    clearTimeout(timeDelay);
    timeDelay = "";
    
    if(!window.divMaskLoading) 
        CreateMaskLoadingDiv();
    if(!window.viewport)
        IBC.calculateViewport();
    window.divMaskLoading.style.width = (window.viewport.width - 3) + 'px';
    window.divMaskLoading.style.height = window.viewport.height + 'px';
     var div1 = IBC.createElement("div");
    window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width/2) + 'px';
    window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';
    window.divMaskLoading.style.display = 'block'; 
}
function DelayReloadPage2(time)
 {
    if(!window.divMaskLoading) 
        CreateMaskLoadingDiv();
    if(!window.viewport)
        IBC.calculateViewport();
    window.divMaskLoading.style.width = (window.viewport.width - 20) + 'px';
    window.divMaskLoading.style.height = window.viewport.height + 'px';
    window.divMaskLoading.style.display = 'block';  
    var div1 = IBC.createElement("div");
    window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width/2) + 'px';
    window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';
    setTimeout(function(){window.divMaskLoading.style.display = 'none';location.href = location.href;},time);
 }    
function HideDelayReloadPage()
{
    clearTimeout(timeDelay);
    timeDelay = "";    
    if(window.divMaskLoading) 
        setTimeout("window.divMaskLoading.style.display = 'none';", 400);
}   

/*-----End script-----*/
IBC.addEvent(window, "unload", _end);
function _end()
{
    HideDelayReloadPage();
}

function ErrAlter(msg) {
    $('errMsg').className = "";
    $('errMsg').show();
     if($('error_msg'))
        $('error_msg').innerHTML=msg;
     window.scroll(0, 0);
     setTimeout("HideErr()",3000);
 }
 function SuccAlter(msg) {
     $("errMsg").className = "done";
     $('errMsg').show();
     if ($('error_msg'))
        $('error_msg').innerHTML = msg;
     window.scroll(0, 0);
     setTimeout("HideErr()", 3000);
 }
function ErrAlter2(msg)
{
     $('errMsg2').style.display='block';                        
     $('error_msg2').innerHTML=msg;
     setTimeout("$('errMsg2').style.display='none';",3000);
}
function HideErr()
{
     $('errMsg').style.display='none';
}   
 
 function InitDivChkBox() {
     var listChk = $$("div .chk");
     if (listChk.length > 0) {
         for (var i = 0; i < listChk.length; i++) {
             listChk[i].observe('click', chkOnclick);
         }
     }
 }

 function chkOnclick(event) {
     var element = Event.element(event);
     divChkChangeState(element);
 }

 function divChkChangeState(chk) {
     if (chk.hasClassName("ic-check"))
         chk.className = "chk ic-uncheck";
     else
         chk.className = "chk ic-check";
 }

 function ClosePopup() {
     if (parent.ibcWnd) {
        var url = parent.location.href;        
         parent.location.href = url.substr(0, url.indexOf("#") > 0 ? url.indexOf("#") : url.length);
         parent.ibcWnd.close();
     }
     else
         window.close();
 }

function ClosePopupNoReload() {
     if (parent.ibcWnd) {
         parent.ibcWnd.close();
     }
     else
         window.close();
 }

 function DoCal(url, title, width, heigh) {
     
    width = width || 350;
    heigh = heigh || 200;

    var left = (window.innerWidth - width) / 2;
    left = left || 100;
    ibcWnd.open(url, title, left, 50, width, heigh, 'class1', 'class2');     
 }
 function ReloadPage()
 {
    var url = location.href;
    location.href = url.substr(0, url.indexOf("#") > 0 ? url.indexOf("#") : url.length);
 }
function EnableCalendar(isEnable)
{
    if(isEnable)
    {
        $("fdate").style.backgroundColor = "";
        $("tdate").style.backgroundColor = "";
        $("fdateTrigger").disabled = false;
        $("tdateTrigger").disabled = false;

        Calendar.setup({
            inputField: "fdate",
            ifFormat: "%m/%d/%Y",
            button: "fdateTrigger",
            singleClick: true,
            showsTime: false,
            showOthers : true,
            step : 1
        });

        Calendar.setup({
            inputField: "tdate",
            ifFormat: "%m/%d/%Y",
            button: "tdateTrigger",
            singleClick: true,
            showsTime: false,
            showOthers : true,
            step : 1
        });
    }
    else
    {
        $("fdate").style.backgroundColor = "#ECE9D8";
        $("tdate").style.backgroundColor = "#ECE9D8";
        $("fdateTrigger").disabled = true;
        $("tdateTrigger").disabled = true;
        $("fdateTrigger").onclick = "";
        $("tdateTrigger").onclick = "";
    }
}

/*smith*/
function removeSign(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\&|\#|\[|\]|~/g, "-");
    str = str.replace(/-+-/g, "-"); //change -- become -
    str = str.replace(/^\-+|\-+$/g, ""); //remove - at begin and end of string
    return str;
} 

// start up script ---------------------------
IBC.addEvent(window, "load", _startup);

function _startup() {
    HideDelayReloadPage();
    IBC.calculateViewport();
//    var lstSubmenu = $$("li a span");
//    if (lstSubmenu.length > 0) {
//        for (var i = 0; i < lstSubmenu.length; i++) {
//            if (lstSubmenu[i]) {
//                lstSubmenu[i].observe('click', DelayReloadPage1);
//            }
//        }
//    }

    var itemFocus = "";
    if (top.fMenu) {
        var url = location.href;
        var arrMenu = top.fMenu.arrMenu;
        for (var i = 0; i < arrMenu.length; i++) {
            var menuItem = arrMenu[i].split("@");
            var listItem = menuItem[1].split(",");
            for (var j = 0; j < listItem.length; j++) {
                var originURL = listItem[j].substr(0, listItem[j].indexOf("?") > 0 ? listItem[j].indexOf("?") : listItem[j].length);
                if (url.indexOf(originURL.trim()) >= 0) {
                    itemFocus = menuItem[0];
                    break;
                }
            }
        }
        if (itemFocus != "")
            top.fMenu.ActiveMenu(itemFocus);
    }
}

