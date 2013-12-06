function _affChangeMediaText{systemname}{id}() {
    var _affscript = document.createElement('script');
    _affscript.type = 'text/javascript';
    _affscript.id = '_affScript{systemname}{id}';
    _affscript.src = '{url}/{id}?signuplink={afflink}' + encodeURIComponent(location.href);
    var _affcross = document.getElementsByTagName('script')[0];
    var _affscriptExist = document.getElementById('_affScript{systemname}{id}');
    if (_affscriptExist != null) {
        _affcross.parentNode.replaceChild(_affscript, _affcross);
    }
    else {
        _affcross.parentNode.insertBefore(_affscript, _affcross);
    }
}
_affChangeMediaText{systemname}{id}();
if(!window.affTimers)    window.affTimers = {};
if(!window.affTimers.t{systemname}{id})
    window.affTimers.t{systemname}{id} = window.setInterval("_affChangeMediaText{systemname}{id}()", {requesttime});