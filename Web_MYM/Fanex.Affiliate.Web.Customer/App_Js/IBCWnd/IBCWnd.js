// IBCWnd v1.0
// Developed by Qui
// Reference: http://students.infoiasi.ro/~mishoo/site/calendar.epl

// Dragable window for IBCAgent2 


// class prototype
IBCWnd = function()
{
    this.is_ie = window.attachEvent?true:false;
    this.dragging = false;
    this.createdElement = false;
    
    var scripts = document.getElementsByTagName("script");
    this.baseURL = "../js/IBCWnd/";
    
    this.js = "IBCWnd.js";
    this.css = "IBCWnd.css";
    this.blank = "blank.html";
    
    
    if(scripts.length > 0)
    {
        for(var i=0; i<scripts.length; i++)
        {
            var idx = scripts[i].src.indexOf(this.js);
            if(idx != -1)
            {
                this.baseURL = scripts[i].src.substr(0, idx);
                break;
            }
        }
    }
    
    this.cssURL = this.baseURL + this.css;
    this.jsURL = this.baseURL + this.js;
    this.blankURL = this.baseURL + this.blank;
    
    var css = "<link href=\"" + this.cssURL + "\" type=\"text/css\" rel=\"stylesheet\">";
    document.writeln(css);
}

IBCWnd.prototype.open = function(url, titleText, left, top, width, height, windowCss, titleCss, closeBtnCss) {
    if (this.loaded) this.close();

    if (typeof left == 'undefined' ||
        typeof top == 'undefined' ||
        typeof width == 'undefined' ||
        typeof height == 'undefined' ||
        typeof url == 'undefined') {
        alert('IBCWnd.open(url, titleText, left, top, width, height, windowCss, titleCss, closeBtnCss)\r\n\r\n\   - Invalid parameters  ');
        return false;
    }

    this.url = url; this.titleText = titleText; this.left = left; this.top = top; this.width = width; this.height = height; this.windowCss = windowCss;
    this.titleCss = titleCss; this.closeBtnCss = closeBtnCss;

    IBCWnd._preWidth = 0;
    IBCWnd._preHeight = 0;

    if (IBCWnd.autoKeepPosition || this.autoKeepPosition) {
        if (typeof IBCWnd._preLeft != 'undefined') left = IBCWnd._preLeft;
        if (typeof IBCWnd._preTop != 'undefined') top = IBCWnd._preTop;
    }

    if (!this.createdElement || this.height != height || this.width != width) {
        this.mask = IBCWnd.createElement("div");
        this.mask.className = "IBCWndMask";
        this.mask.style.position = "absolute";
        this.mask.style.display = "none";
        this.mask.innerHTML = "<input class=\"IBCWndTitleMask\" readonly=\"true\"  type=\"text\" value=\"\" />";

        this.element = IBCWnd.createElement("div");
        this.element.className = (windowCss ? (windowCss) : "IBCWnd");
        this.element.style.position = "absolute";
        this.element.style.display = "none";
        this.element.style.zIndex = 1000;

        var table = IBCWnd.createElement("table", this.element);
        table.cellPadding = 0; table.cellSpacing = 0; table.className = "IBCWndTable"; table.border = 0;
        var thead = IBCWnd.createElement("thead", table);
        var tr = IBCWnd.createElement("tr", thead);
        tr.oncontextmenu = function() { return false; };
        tr.className = (titleCss ? (titleCss) : "IBCWndTitle");
        tr.style.height = "27px"; tr.style.overflow = "hidden"; tr.style.zIndex = 1000;

        var td1 = IBCWnd.createElement("td", tr);
        td1.className = "IBCWndTitleText"; td1.style.cursor = "move";
        IBCWnd.addEvent(td1, "mousedown", IBCWnd._dragStart);
        td1.innerHTML = "<input oncontextmenu=\"return false\" class=\"IBCWndTitleMask\" readonly=\"true\" type=\"text\" value=\"\" />";
        this.maskTitle = td1.firstChild;

        //td1.innerHTML = "<div oncontextmenu=\"return false\" title=\"Close\" class=\"" + (closeBtnCss?closeBtnCss:"IBCWndCloseButton") + "\" style=\"float:right\"></div>";
        var div1 = IBCWnd.createElement("div", td1); div1.style.height = "20px"; div1.style.display = "block"; div1.style.zIndex = 1000;
        div1.style.paddingLeft = "10px"; div1.style.lineHeight = "18px"; div1.className = "b"; div1.style.textAlign = "left";
        this.title = div1;

        var td2 = IBCWnd.createElement("td", tr); td2.className = "IBCWndTitleButton";
        td2.onclick =  function() { ibcWnd.close(); };
        td2.oncontextmenu = function() { return false; };
        td2.innerHTML = "<div oncontextmenu=\"return false\" title=\"Close\" class=\"" + (closeBtnCss ? closeBtnCss : "IBCWndCloseButton") + "\" style=\"float:right\"></div>";
        td2.vAlign = "center"; td2.align = "right";
        var tbody = IBCWnd.createElement("tbody", table);

        tr = IBCWnd.createElement("tr", tbody);
        td = IBCWnd.createElement("td", tr); td.colSpan = 2;
        td.align = "left"; td.vAlign = "top"; td.className = "IBCWndNoPadding";
        this.content = td;
        /*Start box content*/
        var divLLine = IBCWnd.createElement("div", td);
        divLLine.className = "lline";
        divLLine.style.height = "100%";

        var divRLine = IBCWnd.createElement("div", divLLine);
        divRLine.className = "rline";
        divRLine.style.height = "100%";



        this.iframe = IBCWnd._createIframe();
        divRLine.appendChild(this.iframe);

        /*End box content*/

        /*Start bottom line*/
        var divClear = IBCWnd.createElement("div", td);
        divClear.className = "clear";

        var divFooterLeft = IBCWnd.createElement("div", td);
        divFooterLeft.className = "fleft";
        divFooterLeft.innerHTML = "<div class=\"fright\"></div>"
        /*End bottom line*/
        document.body.appendChild(this.mask);
        document.body.appendChild(this.element);

        this.createdElement = true;
    }

    this.setRect(left, top, width, height);

    this.iframe.src = url;
    this.title.innerHTML = "<span class=\"iconpopup\">&nbsp;</span>" + titleText;
    this.element.style.display = 'block';
    this.loaded = true;

    // listen event
    IBCWnd.addEvent(this.is_ie ? document.body : window, "keydown", IBCWnd._keydown);
    IBCWnd.addEvent(window, "resize", IBCWnd.calculateViewport);

    // add public reference
    IBCWnd._W = this;
}


IBCWnd.prototype.close = function() {
    this.iframe.src = this.blankURL;
    IBCWnd.removeEvent(this.is_ie ? document.body : window, "keydown", IBCWnd._keydown);
    IBCWnd.removeEvent(window, "resize", IBCWnd.calculateViewport);

    this.element.style.display = 'none';
    this.viewport = null;

    this.loaded = false;
    if (this.onClosed && typeof (this.onClosed) == 'function') {
        this.onClosed();
    }
}

IBCWnd.prototype.setRect = function(left, top, width, height)
{
    if(!this.createdElement) return false;

    var scrOfX = 0;
    var scrOfY = 0;
    if( typeof( window.pageXOffset ) == 'number' ) {
        scrOfX = window.pageXOffset;
    } else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) ) {
        scrOfX = document.body.scrollLeft;
    } else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) ) {
        scrOfX = document.documentElement.scrollLeft;
    }
    if( typeof( window.pageYOffset ) == 'number' ) {
        scrOfY = window.pageYOffset;
    } else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) ) {
        scrOfY = document.body.scrollTop;
    } else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) ) {
        scrOfY = document.documentElement.scrollTop;
    }
    
    //nqminh
    scrOfY += 15;
    
    this.scrOfX = scrOfX;
    this.scrOfY = scrOfY;    
    
    if(!this.loaded)
    {        
        left += scrOfX;
        top += scrOfY;
    }        
    
    this.left = left;
    this.top = top;
    
    this.width = (width < 30) ? 30 : width;
    this.height = (height < 30) ? 30 : height;
    
    this.calculateViewport();
    
    
    this.mask.style.left = this.left + "px";
    this.mask.style.top = this.top + "px";
    this.mask.style.width = this.mask.firstChild.style.width = this.width + "px";
    this.mask.style.height = this.mask.firstChild.style.height = this.height + "px";

    this.title.style.width = this.maskTitle.style.width = (this.width-20) + 'px';
    this.content.style.width = (this.width) + "px";
    this.content.style.height = (this.height - 27) + "px";
    
    this.element.style.left = this.left + "px";
    this.element.style.top = this.top + "px";
    this.element.style.width = this.width + "px";
    this.element.style.height = this.height + "px";

    if(width <= 5 || height <= 5)
        this.element.style.visibility = "hidden";
    else
        this.element.style.visibility = "visible";
    
    return true;
}

IBCWnd.prototype.calculateViewport = function()
{
    this.viewport = [];
    if(!this.is_ie)
    {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.scrollWidth !=
         'undefined' && document.documentElement.scrollWidth != 0)
        {
           this.viewport.width = document.documentElement.scrollWidth;
           this.viewport.height = document.documentElement.scrollHeight;
        }        

        this.viewport.width = Math.max(this.viewport.width, (Math.max(document.body.scrollWidth, document.body.clientWidth)));
        this.viewport.height = Math.max(this.viewport.height, (Math.max(document.body.scrollHeight, document.body.clientHeight)));
    }
    else
    {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.clientWidth !=
         'undefined' && document.documentElement.clientWidth != 0)
        {
           this.viewport.width = document.documentElement.clientWidth;
           this.viewport.height = document.documentElement.clientHeight;
        }
        // older versions of IE
        else
        {
           this.viewport.width = document.getElementsByTagName('body')[0].clientWidth;
           this.viewport.height = document.getElementsByTagName('body')[0].clientHeight;
        }

        this.viewport.width = Math.max(document.body.scrollWidth, this.viewport.width);
        this.viewport.height = Math.max(document.body.scrollHeight, this.viewport.height);
    }
    
    this.maxLeft = this.viewport.width - this.width - 15;
    this.maxTop = this.viewport.height - this.height - 20;
}

// utility functions
IBCWnd.addEvent = function(el, evname, func) {
	if (el.attachEvent) { // IE
		el.attachEvent("on" + evname, func);
	} else if (el.addEventListener) { // Gecko / W3C
		el.addEventListener(evname, func, true);
	} else { // Opera (or old browsers)
		el["on" + evname] = func;
	}
}

IBCWnd.removeEvent = function(el, evname, func) {
	if (el.detachEvent) { // IE
		el.detachEvent("on" + evname, func);
	} else if (el.removeEventListener) { // Gecko / W3C
		el.removeEventListener(evname, func, true);
	} else { // Opera (or old browsers)
		el["on" + evname] = null;
	}
}

IBCWnd.createElement = function(type, parent) {
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

// static property
IBCWnd.autoKeepPosition = false;

IBCWnd._preLeft = 0;
IBCWnd._preTop = 0;
IBCWnd._preWidth = 0;
IBCWnd._preHeight = 0;

// static functions, were called in event
IBCWnd.calculateViewport = function()
{
    var wnd = IBCWnd._W;
    wnd.calculateViewport();
}

IBCWnd._createIframe = function()
{
    var iframe = document.createElement("iframe"); iframe.setAttribute("border", 0); iframe.className = "IBCWndNoPadding";
    iframe.setAttribute("frameborder", 0); iframe.style.backgroundColor = "#ffffff";
    iframe.width = "100%"; iframe.height = "100%";
    return iframe;
}

IBCWnd._keydown = function(ev)
{
    var wnd = IBCWnd._W;

	if (wnd.dragging) {
	    wnd.mask.style.display = "none";
	    wnd.dragging = false;
		return false;
	}
    
    if(wnd.is_ie) {
        if(window.event.keyCode == 27) wnd.close();
    }
    else {
        if(ev.which == 27) wnd.close(); 
    }
    
    return IBCWnd.stopEvent(ev);
}

IBCWnd._dragStart = function(ev) 
{    
    var wnd = IBCWnd._W;
    var leftMouse = 1;
    if(wnd.is_ie) {
        leftMouse = window.event.button;
    }
    else {
        leftMouse = ev.which;
    }
    
    if(leftMouse != 1) return false;
    wnd.mask.style.display = "block";
    wnd.mask.style.left = wnd.element.style.left;
    wnd.mask.style.top = wnd.element.style.top;
    
	if (wnd.dragging) {
		return false;
	}
	wnd.dragging = true;
	
	var posX;
	var posY;
	if (wnd.is_ie) {
		posY = window.event.clientY + document.body.scrollTop;
		posX = window.event.clientX + document.body.scrollLeft;
	} else {
		posY = ev.clientY + window.scrollY;
		posX = ev.clientX + window.scrollX;
	}
	
	var st = wnd.element.style;
	wnd.xOffs = posX - parseInt(st.left);
	wnd.yOffs = posY - parseInt(st.top);
	with (IBCWnd) {
		addEvent(document, "mousemove", calDragIt);
		addEvent(document, "mouseover", stopEvent);
		addEvent(document, "mouseup", calDragEnd);
	}
	
	IBCWnd.stopEvent(ev);
};

IBCWnd.calDragIt = function(ev) 
{
    var wnd = IBCWnd._W;
	if (!wnd.dragging) {
		return false;
	}
	var posX;
	var posY;
	if (wnd.is_ie) {
		posY = window.event.clientY + document.body.scrollTop;
		posX = window.event.clientX + document.body.scrollLeft;
	} else {
		posX = ev.pageX;
		posY = ev.pageY;
	}

    var left = (posX - wnd.xOffs);
    var top = (posY - wnd.yOffs);
    
    // prevent horizontal move verflow width
    if(wnd.width < wnd.viewport.width - 6)
    {
        if(left <= 1) left = 1; if(left >= wnd.maxLeft) left = wnd.maxLeft;
        wnd.mask.style.left = left + "px";
    }
    
    // prevent vertical move overflow height
    if(wnd.height < wnd.viewport.height)
    {
        if(top <= 1) top = 1; if(top >= wnd.maxTop) top = wnd.maxTop;
        wnd.mask.style.top = top + "px";
    }
    
    IBCWnd.stopEvent(ev);
};

IBCWnd.calDragEnd = function(ev) 
{
    var wnd = IBCWnd._W;
    wnd.mask.style.display = "none";
    
	if (!wnd.dragging) {
		return false;
	}
	
	wnd.dragging = false;
	
	wnd.left = IBCWnd._preLeft = parseInt(wnd.mask.style.left);
	wnd.top = IBCWnd._preTop = parseInt(wnd.mask.style.top);
	
	wnd.element.style.left = wnd.mask.style.left;
	wnd.element.style.top = wnd.mask.style.top;
	
	with (IBCWnd) {
		removeEvent(document, "mousemove", calDragIt);
		removeEvent(document, "mouseover", stopEvent);
		removeEvent(document, "mouseup", calDragEnd);
	}
};

IBCWnd.stopEvent = function(ev) 
{
    var wnd = IBCWnd._W;
	if (wnd.is_ie) {
		window.event.cancelBubble = true;
		window.event.returnValue = false;
	} else {
		ev.preventDefault();
		ev.stopPropagation();
	}
	return false;
};

// init object
var ibcWnd = new IBCWnd();