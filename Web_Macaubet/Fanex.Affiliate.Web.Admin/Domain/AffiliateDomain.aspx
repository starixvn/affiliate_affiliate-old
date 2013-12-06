<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AffiliateDomain.aspx.cs" Inherits="Fanex.Web.Macaubet.Admin.Domain.AffiliateDomain" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/UserControl.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script language="javascript" type="text/javascript">
    function CloseDiv() { 
        ibcWnd.close();   
    }

    function DelAffDomain(affDomainId, affDomainName)
    {
        if(confirm($('cfmchoose').value))
        {
            var ajax = new Ajax.Request('../Handler/UpdateAffDomain.ashx', {
                method: 'post',
                parameters: { affDomainId: affDomainId, affDomainName: affDomainName, sta: 'Del' },
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
