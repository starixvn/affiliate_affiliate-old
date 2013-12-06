<%@ Page Language="C#" AutoEventWireup="true" Inherits="ListUser" Codebehind="ListUser.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src="../App_Js/UserControl.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">        
        window.onload = function(){Focus();};
        function Focus() {
            $('txtKeyword').blur();
            $('txtKeyword').focus();            
            /*assign handler for status check-box*/
            listChk = $$("span.chk");                     
            if (listChk.length > 0) {
                for (var i = 0; i < listChk.length; i++) {
                    listChk[i].observe("click",chkOnClick);
                }
            }
            if ($('keyword').value != "Keyword")
                $('txtKeyword').value = $('keyword').value;
        }

        function chkOnClick(event) {
            var element = Event.element(event);
            divChkChangeState(element);
            var str = element.id;
            var custid = str.split(",")[0];
            var status = str.split(",")[1];
            var result = UpdateStatus(custid, status, 0, '');
            /*if update failed, change the checkbox to previous state*/
            if (result == 0) {
                divChkChangeState(element);
            }
        }
        
        function Search()
        {
            var url="ListUser.aspx?us=" + $('keyword').value + '&fl=' + $('alp').value;
            location.href = url;
        }  
        function CloseDiv()
        {    
            $('divChangePass').style.display ='none';    
        }
        
        function CloseDivUser() {    
            $('divChangeUser').style.display ='none';
        }
        
       
        function ListUser(depid,depname,notWait)
        {          
            var test = new Date();
           var ajax= new Ajax.Request("../Handler/ListUser.ashx",{
            method:'get',  
            parameters:{depid:depid,depname:depname},
            onComplete:function(strdata)
            {
               $('listPolicy').innerHTML='';
               $('listuser').innerHTML='';
               if(notWait)
               {
                    window.divMaskLoading.style.display = 'none';
                   $("listuser").innerHTML= strdata.responseText;
               }
               else
               {
                   DelayReloadPage('listuser',strdata.responseText);
                }
            }               
            });    
        }
        function SearchUser(depid,depname)
        {          
           var ajax= new Ajax.Request("../Handler/ListUser.ashx",{
            method:'get',  
            parameters:{depid:depid,depname:depname,us:$('keyword').value,fl:$('alp').value},
            onComplete:function(strdata)
            {
               $('listPolicy').innerHTML='';               
               DelayReloadPage('listuser',strdata.responseText);
                
            }               
            });
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
        function LoadData() {
            $('txtKeyword').value = $('txtKeyword').value.trim();
            try {
                if ($("frData") != null) {
                    $("frData").submit();
                }
            }
            catch (e) {
                alert('Invalid data!');
            }
        } 
    </script>  
<!--/js-->