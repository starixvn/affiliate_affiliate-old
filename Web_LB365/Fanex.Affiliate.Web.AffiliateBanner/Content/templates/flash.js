var el = AC_FL_RunContent(
     'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0',
	 'width', '{width}',
	 'height', '{height}',
	 'src', '{srcnoswf}',
	 'quality', 'high',
	 'pluginspage', 'http://www.adobe.com/go/getflashplayer_tw',
	 'align', 'middle',
	 'play', 'true',
	 'loop', 'true',
	 'scale', 'showall',
	 'wmode', 'transparent',
	 'devicefont', 'false',
	 'id', '{systemname}{id}',
	 'bgcolor', '#ffffff',
	 'name', '{systemname}{id}',
	 'menu', 'true',
	 'allowFullScreen', 'false',
	 'allowScriptAccess', 'sameDomain',
	 'movie', '{srcnoswf}',
	 'salign', '',
	 'flashvars', 'link={afflink}');

replaceHtml('div','f_{systemname}{id}', el);