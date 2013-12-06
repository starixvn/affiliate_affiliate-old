<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_Note" Codebehind="Note.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    //window.onresize=function(){setSize();};
    window.onload=function(){CheckBox_AddEvent();};
    function showForm()
    {
        if($("noteForm").style.display == "none" || $("btnUpdNote").style.display == "")
        {
            $("noteForm").style.display = "block";
            $("txtNote").focus();
            $("txtNote").value = "";
            $("btnAddNote").show();
            //$("btnReset").show();
            $("btnUpdNote").hide();
        }
        else
            $("noteForm").style.display = "none"
    }
    function hideForm()
    {
        $("noteForm").style.display = "none";
        clearNote();
    }
    function clearNote()
    {
        $("txtNote").value = "";
    }
    function AddNote()
    {
        ibcWnd.open("AddNote.aspx?action=1", 'Add Note', 150, 50, 370, 230, 'class1', 'class2');
    } 
    
    function editNote(noteid)
    {
        ibcWnd.open("AddNote.aspx?action=2&noteid=" + noteid, 'Edit Note', 150, 50, 370, 230, 'class1', 'class2');
    }
    function DelNote()
    {
        $("errMsg").className = "";
        listChk = $$("div.chk");
        var listIds = "";
        for(var i = 0; i < listChk.length; i++)
        {
            if(listChk[i].hasClassName("ic-check"))
                listIds += listChk[i].id + ";";
        }
        if(listIds.length == 0)
        {
            alert(top.fMenu.Msg.SelectNoteDel);
            return;
        }
        else
            listIds = listIds.substr(0, listIds.length - 1);

        if (!confirm(top.fMenu.ConfirmDelNote))
            return;        
        DelayReloadPage1();
        var pagesize = $("slPageSize").value;        
        var ajax= new Ajax.Request('../Handler/Note.ashx',{
           method:'post',  
           parameters:{status:3,id:listIds,custid:$("custid").value, pagesize:pagesize},
           onComplete:function(strdata)//Get list note here
           {
               var returnData = strdata.responseText.split("@");
               if(returnData[0]=='ok')
               {         
                    //$("divNote").innerHTML = returnData[1];
                    ErrAlter(top.frames[0].Msg.NoteDel); 
                    $("errMsg").className = "done";
                    $("slPageSize").value = pagesize;
                    setTimeout("LoadData(1);", 1500);
               }
               else
               {
                    ErrAlter(returnData[1]);     
                    HideDelayReloadPage();                              
               }
               
           }               
           });
    }
    function deleteNote(noteid)
    {   
        $("errMsg").className = "";
        if(!confirm(top.frames[0].Msg.NoteConfirm))
            return;
        DelayReloadPage1();
        var pagesize = $("slPageSize").value;  
        var ajax= new Ajax.Request('../Handler/Note.ashx',{
           method:'post',  
           parameters:{status:3,id:noteid,custid:$("custid").value},
           onComplete:function(strdata)//Get list note here
           {
               var returnData = strdata.responseText.split("@");
               if(returnData[0]=='ok')
               {         
                    ErrAlter(top.frames[0].Msg.NoteDel);
                    $("errMsg").className = "done"; 
                    $("slPageSize").value = pagesize;
                    setTimeout("LoadData(1);", 1500);
               }
               else
               {
                    ErrAlter(returnData[1]);                                   
                    HideDelayReloadPage();
               }
               
           }               
           });
    }
    function NavigatePage(mypage){
        $("page").value = mypage
        $("frSearch").submit();
    }
</script>
<!--/js-->