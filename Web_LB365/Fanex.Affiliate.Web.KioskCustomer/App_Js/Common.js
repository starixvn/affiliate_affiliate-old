var arrItem = new Array();
var arrError = new Array();
var timeDelay = "";

var Common = {};

function obj(id){
	return document.getElementById(id);
}

String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };

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

Common.ValidateHTMLAndSQLInjection = function (inputVal) {
    var message = { ErrCode: 0, ErrMsg: '' };

    if (/((\%3D)|(=)|(\%27)|(\')|(\-\-)|(\%3B)|(;))()/i.test(inputVal) === true) {
        message.ErrMsg = gMsg.Errmsgsqlcharacter;
        message.ErrCode = 1
        return message;
    }

    //Cross site script attack
    if (/((\%3C)|<)*[a-z0-9\%]+((\%3E)|>)/i.test(inputVal) === true) {
        message.ErrMsg = gMsg.Errmsghtmlsyntax;
        message.ErrCode = 2
        return message;
    }

    //SQL Injection 
    if (/\w*((\s)|(\%32))((\%6F)|o|(\%4F))((\%72)|r|(\%52))((\s)|(\%32))/i.test(inputVal) === true) {
        message.ErrMsg = gMsg.Errmsgsqlinjection;
        message.ErrCode = 3
        return message;
    }

    return message;
}

Array.prototype.clear =function(){
	if (this.length >0) this.splice(0,this.length);  
}
String.prototype.isNumber =function() {        
    var regs= new RegExp(/^-?\d+(\.\d+)?\s*$/);    
    return (regs.test(this));
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
function checkUsername(value) {
    var filter = /^[a-z][-a-z0-9_]{0,49}$/i;
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
//--------------
function MM_openBrWindow(theURL,winName,features)
{ 
    window.open(theURL,winName,features);
}
function validate_email(field)
{
    with (field)
    {
        apos=value.indexOf("@");
        dotpos=value.lastIndexOf(".");
        if (apos<1||dotpos-apos<2) 
        {
          return false;
        }
        else 
        {
            return true;
        }
    }
} 
function AddError(objID, errMsg)
{
    arrItem.push(objID);
    arrError.push(errMsg)
}
function ShowError(errMsgs,tagIds,placeHolder) {	
    
	window.scroll(0,0); 
	parent.scroll(0,0); 
	var errHtml="";	
	$(placeHolder).style.display=(errMsgs.length==0)?'none':'';
	for(var i=0;i<errMsgs.length;++i){
	    errHtml+="<li><a class='err-msg' href='' onclick=\"$('"+tagIds[i]+"').focus(); return false;\">"+errMsgs[i]+"</a></li>";
	}
	$(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
	//$(placeHolder).focus();
}
function ShowErrorNotLink(errMsgs,tagIds,placeHolder)
{	
	window.scroll(0,0); 
	parent.scroll(0,0); 
	var errHtml="";	
	$(placeHolder).style.display=(errMsgs.length==0)?'none':'';
	for(var i=0;i<errMsgs.length;++i){
	    errHtml+="<li><span class='err-msg b' href=''>"+errMsgs[i]+"</span></li>";
	}
	$(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
	//$(placeHolder).focus();
}
function DisplayMsg(isErr) {    
    if(!isErr)
    {
        ShowError(arrError, arrItem, "error_item");        
        $('errMsg').show();
        if($('succMsg'))
            $('succMsg').hide();
    }
    else
    {
        ShowError(arrError, arrItem, "error_item");        
        $('errMsg').hide();
        if($('succMsg'))
            $('succMsg').show();
    }
}
function RefreshPage(time)
{
    setTimeout("location.href = location.href;", time * 1000);
}
/*Manipulating the head and bottom paging. When mouse clicked on the page number, the text changed to textbox*/
var pageTimeOut = null;
function PageNum(aID, txtID, isInputPage) {
    clearTimeout(pageTimeOut);
    pageTimeOut = null;
    if (isInputPage) {
        $(aID).hide();
        $(txtID).show();
        if ($(txtID))
            $(txtID).focus();
    }
    else
        pageTimeOut = setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
}

function ChangePage(e, txtID, maxPage, aID) {
    e = (e) ? e : window.event;
    var key = (e.keyCode) ? e.keyCode : e.which;
    aID = (aID == null) ? "aPageTop" : aID;
    if (key == 13) {
        var currPage = parseInt($(txtID).value);
        if (currPage > maxPage || isNaN(currPage) || currPage <= 0) {
            setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
            return false;
        }
        LoadData(currPage);
    }
}

function GotoPage(pageIndex, maxPage)
{
    var currPage = parseInt($("page").value);
    if(currPage == 1 && pageIndex <=1)
        return false;
    if(currPage == maxPage && pageIndex >= maxPage)
        return false;
    if(pageIndex > maxPage || pageIndex <= 0)
        return false;
    LoadData(pageIndex);
}
/*End paging processing functions*/
/* when paging enable, used to load data of specific page to the grid*/
function LoadData(currPage) {
    try {
        if($("page"))
            $("page").value = currPage;
        $("frmData").submit();
    }
    catch (e) {
        alert('Invalid data!');
    }
}
function GetDataFromUrl(fieldName)
{
    var url = location.href;
    var start = url.indexOf(fieldName, 0);
    var end = url.indexOf("&", start);
    if(end == -1)
        end = url.length;
    if(start < 0 || end < 0)
        return "";
    start = start + fieldName.length + 1;
    return url.substr(start, end - start);
}
//-------------------------------------------------------------------------
// JScript File
var IE = document.all ? true : false;
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

IBC.calculateViewport = function() {
    window.viewport = [];
    if (!this.is_ie) {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.scrollWidth !=
         'undefined' && document.documentElement.scrollWidth != 0) {
            window.viewport.width = document.documentElement.scrollWidth;
            window.viewport.height = document.documentElement.scrollHeight;
        }

        window.viewport.width = Math.max(window.viewport.width, (Math.max(document.body.scrollWidth, document.body.clientWidth)));
        window.viewport.height = Math.max(window.viewport.height, (Math.max(document.body.scrollHeight, document.body.clientHeight)));
    }
    else {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.clientWidth !=
         'undefined' && document.documentElement.clientWidth != 0) {
            window.viewport.width = document.documentElement.clientWidth;
            window.viewport.height = document.documentElement.clientHeight;
        }
        // older versions of IE
        else {
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
function DelayReloadPage1() {
    clearTimeout(timeDelay);
    timeDelay = "";

    if (!window.divMaskLoading)
        CreateMaskLoadingDiv();
    if (!window.viewport)
        IBC.calculateViewport();
    window.divMaskLoading.style.width = (window.viewport.width - 10) + 'px';
    window.divMaskLoading.style.height = window.viewport.height + 'px';
    var div1 = IBC.createElement("div");
    window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width / 2) + 'px';
    window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';
    window.divMaskLoading.style.display = 'block';
}

function CreateMaskLoadingDiv() {

    window.divMaskLoading = IBC.createElement("div");
    var div1 = IBC.createElement("div");

    window.divMaskLoading.className = "divframeloading"

    window.divMaskLoading.style.filter = 'alpha(opacity=50)';
    window.divMaskLoading.style.opacity = '0.5';
    window.divMaskLoading.appendChild(div1);
    document.body.appendChild(window.divMaskLoading);

}

// Cookie
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function setCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function IsAvalilableNickname() {
    var Nickname = $("txtNickname").value.trim();
    $("spCheckNickname").className = "red";

    if (isEmpty(Nickname))
        return false;

    if (!checkUsername(Nickname)) {
        $("spCheckNickname").innerHTML = "<br />Nickname only contains letters, numbers, and underscores.";
        return false;
    }

    var ajax = new Ajax.Request(ErrMsg.RootUrl + "/Handler/CheckNickname.ashx?nickname=" + Nickname, {
        method: 'get',
        onComplete: function(datastran) {
            var result = datastran.responseText;
            if (result == 'ok') {
                $("spCheckNickname").className = "blue";
                $("spCheckNickname").innerHTML = Nickname + " is available.";                
                return true;
            }
            else {
                $("spCheckNickname").innerHTML = (result == 'error' ? Nickname + " is not available." : result);
                return false;
            }
        }
    });

}