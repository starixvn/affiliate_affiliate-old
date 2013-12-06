<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_MemberListAll" Codebehind="MemberListAll.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>  
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    window.onload = MyFocus;
    var time = 0;//set Timeout for clock refresh
    var timer = null;//set Timeout for choose refresh time
    var timePage = "";//set Timeout for go to page
    
    function NavigatePage(mypage){
        $("page").value = mypage
        $("frData").submit();
    }
    
    function MyFocus() {
        $('txtAffUsername').focus();
        $("slTime").value = $("timer").value;
        if($("slTime").value != "0")
        {
            time = parseInt($("slTime").value);
            setInterval("CountDown();", 1000);
        }
    }
    function CloseDiv() {    
        $('divChangePass').style.display ='none';            
    }
    function DoCal(url) {    
        ibcWnd.open(url, 'Assign member to affiliate', 150, 50, 350, 200, 'class1', 'class2');
    }
    function Refresh(url)
    {
        location.href = url;
    }
    function PreSearch(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            LoadData(1);
        }
    }

    function DeleteMember(username) {
        if (confirm("Are you sure you want to remove this member from his affiliate?")) {
            var param;
            param = "username=" + username;
            var ajax = new Ajax.Request("../Handler/DeleteMember.ashx", {
                method: "post",
                parameters: param,
                onComplete: function(data) {
                    if (data.responseText == "") {
                        SuccAlter("Member has been removed suseccfully");
                    }
                    else {
                        ErrAlter(data.responseText);
                    }
                    setTimeout("LoadData($('page').value);", 1000);
                }
            });
        }
    }  
</script>
<!--/js-->