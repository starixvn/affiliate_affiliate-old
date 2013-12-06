<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDomain.aspx.cs" Inherits="Fanex.Web.ITCBet.Admin.Domain.MemberDomain" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/UserControl.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script language="javascript" type="text/javascript">
    function CloseDiv() { 
        ibcWnd.close();   
    }

    function Del12BetDomain(_12BetDomainId, _12BetDomainName)
    {
        if(confirm($('cfmchoose').value))
        {
            var ajax = new Ajax.Request('../Handler/UpdateMemberDomain.ashx', {
                method: 'post',
                parameters: { _12BetDomainId: _12BetDomainId, _12BetDomainName: _12BetDomainName, sta: 'Del' },
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

</script>
<!--/js-->
