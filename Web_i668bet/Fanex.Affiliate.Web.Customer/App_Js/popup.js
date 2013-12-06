function viewdatapopup(id)
{
   var ajax= new Ajax.Request("Handler/GetDataPopup.ashx",{
   method:'get',  
   parameters:'id=' + id,     
   onComplete:function(datastran)
       {
        $('cont').innerHTML=datastran.responseText;
       }               
   });
}
/*function viewdata(data)
{
    $('cont').innerHTML=data.responseText;
}
*/
function viewdatapopupsub(id)
{
   var ajax= new Ajax.Request("Handler/GetDataPopupSub.ashx",{
   method:'get',  
   parameters:'id=' + id,     
   onComplete:function(datastran)
    {
        $('contsub').innerHTML=datastran.responseText;
    }               
   });
}
/*function viewdatasub(data)
{
    $('contsub').innerHTML=data.responseText;
}
*/
function viewdatapopupgeneralrules(id)
{
   var ajax= new Ajax.Request("Handler/GetDataPopupgeneralrules.ashx",{
   method:'get',  
   parameters:'id=' + id,     
   onComplete:function(datastran)
   {
    $('contsub').innerHTML=datastran.responseText;   
   }               
   });
}
/*function viewdatageneralrules(data)
{
    $('contsub').innerHTML=data.responseText;
}*/
function smenuChange(k){
    k=parseInt(k);
    for(i=0;i<document.getElementsByName('question').length;i++) {
        document.getElementsByName('question')[i].className='qn';
        document.getElementsByName('answer')[i].style.display='none';
   }
        document.getElementsByName('question')[k].className='qn-down'; 
        document.getElementsByName('answer')[k].style.display='';
   document.getElementById('smenuValue').value=k;
}
