<%@ Page Language="C#" AutoEventWireup="true" Inherits="ListDepartment" Codebehind="ListDepartment.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/UserControl.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script language="javascript" type="text/javascript">
    function CloseDiv() { 
        ibcWnd.close();   
    }
    
    function DelDepartment(depid,depname)
    {
        if(confirm($('cfmchoose').value))
        {
            var ajax = new Ajax.Request('../Handler/UpdateDepartment.ashx', {
                method: 'post',
                parameters: { depid: depid, sta: 'Del' },
                onComplete: function(strdata) {

                    if (strdata.responseText == 'Ok') {
                        location.href = location.href;
                    }
                    if (strdata.responseText != 'Ok') {
                        ErrAlter(strdata.responseText);
                    }

                }
            });  
         }
    }
    function ListPolicyDep(depid,depname)
        {
           var ajax= new Ajax.Request("../Handler/GroupPolicy.ashx",{
            method:'post',  
            parameters:'depid=' + depid + '&depname=' + depname,
            onComplete:function(strdata)
            {
                DelayReloadPage('listPolicy',strdata.responseText);
            }               
            });    
        }
 
    function ListUser(depid,depname)
    {          
            var ajax= new Ajax.Request("../Handler/ListUser.ashx",{
            method:'post',  
            parameters:{depid:depid,depname:depname},
            onComplete:function(strdata)
            {
               DelayReloadPage('listPolicy',strdata.responseText);
            }               
        });    
    }    
   
    function CloseDivUser() {    
            $('divChangeUser').style.display ='none';    
    }
   
     function SearchUser(depid,depname)
    {          
       var ajax= new Ajax.Request("../Handler/ListUser.ashx",{
        method:'post',  
        parameters:{depid:depid,depname:depname,us:$('keyword').value,fl:$('alp').value},
        onComplete:function(strdata)
        {                              
           DelayReloadPage('listPolicy',strdata.responseText);                
        }               
        });    
    }   
 
</script>
<!--/js-->
