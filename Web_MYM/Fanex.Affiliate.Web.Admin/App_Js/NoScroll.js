function setSize() {
        var myHeight = 0;
        if( typeof( window.innerWidth ) == 'number' ) {
        //Non-IE
            myHeight = window.innerHeight;
        } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
        //IE 6+ in 'standards compliant mode'
        myHeight = document.documentElement.clientHeight;
        } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
        //IE 4 compatible
        myHeight = document.body.clientHeight;
        }
        var obj = $$("div.ContentBox")[0];
        obj.style.height= (myHeight - 97) + "px";
    }