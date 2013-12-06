IBC.ApplyScroller = function()
{
    var text_news = document.getElementById('text_news');
        
    var scroller = document.getElementById('scroller');
    if(document.getElementById('topnews'))
    {
        var topnews = document.getElementById('topnews');
        
        topnews.onmouseover = function(){IBC.scrollerPaused = true;};
        topnews.onmouseout = function(){IBC.scrollerPaused = false;};    
        
        IBC.scroller = scroller;
        IBC.topnews = topnews;
        IBC.maskWidth = parseInt(text_news.offsetWidth);
        
        IBC.scrollInterval = setInterval(IBC.Scrolling, 20);
    }
    
}

IBC.Scrolling = function()
{
    if(IBC.scrollerPaused) return;    

    var left = parseInt(IBC.scroller.style.left);
    var width = parseInt(IBC.topnews.offsetWidth);
    
    if(left + width < -5) left = IBC.maskWidth + 5 ;
    
    IBC.scroller.style.left = (left - 1) + 'px';
}

IBC.addEvent(window, "load", IBC.ApplyScroller);