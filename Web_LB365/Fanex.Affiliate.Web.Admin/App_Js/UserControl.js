function GetCheck() {
    DelayReloadPage1();    
    setTimeout("UpdatePolicy();", 2000);
 }
function CancelUpd()
{    
    parent.ibcWnd.close();
}
function UpdatePolicy()
{
    document.fUpdatePolicy.submit();
    if(location.href.indexOf("UpdateMultiAdmins.aspx") >= 0)
    {
        setTimeout("parent.$('btnsearch').click();", 3000);
    }
    else
       setTimeout("parent.ibcWnd.close();",3000);
}
function ChkAll()
{
     var checkID = document.getElementsByName("chkItem");
    
     for(var i=0;i<checkID.length;i++)
     {
         checkID[i].checked=$('chkAll').checked;
     }
}
function ResetKeyWord()
{
    if($('keyword').value=='Keyword')
    {
        $('keyword').value='';
        return;
    }
    else if($('keyword').value=='')
    {
        $('keyword').value='Keyword';
        return;
    }            
}
function UpdateStatus(custid,isclose,depid,depname)
{
    if(!confirm($("ChangeStatusMess").value)) {
        return 0;
    }
    var ajax = new Ajax.Request("../Handler/UpdateStatus.ashx", {
        method: 'post',
        parameters: "cstid=" + custid + "&iscls=" + isclose,
        onComplete: function(strdata) {
            if (strdata.responseText == 'Ok') {
                SuccAlter("The status has been updated successfully!");
                if (depid == 0 && depname == '') {
                    DelayReloadPage2(2000);
                }
                else {
                    DelayReloadPage1();
                    setTimeout("ListUser(" + depid + ",'" + depname + "', true);", 2000);
                }
            }
            if (strdata.responseText != 'Ok' && strdata.responseText != 'Fail') {
                ErrAlter(strdata.responseText);
                if (isclose == "False") {
                    $(custid).checked = true;
                }
                else {
                    $(custid).checked = false;
                }
            }

        }
    });
    return 1;
} 
function PreSearch(e)
    {        
        e =(e) ?e :window.event;
        var key =(e.keyCode) ?e.keyCode :e.which;
        if (key ==13)		
        {
            LoadData(1);           
        }
    }
function ListPolicyUser(custid,depname,depid,islstuser)
{
    var ajax = new Ajax.Request("../Handler/AccountPolicy.ashx", {
        method: 'post',
        parameters: { custid: custid, depname: depname, depid: depid, isLstUser: islstuser },
        onComplete: function(strdata) {
            //alert(strdata.responseText);
            //DelayReloadPage('listPolicy', strdata.responseText);
            //setTimeout("location.href='#idfocus';", 1000);
        }
    });    
}
function LoadMultUpdate(arrCustId,arrUser,dept,deptid,isListUser)
{            
    /*var myajax= new Ajax.Request("../Handler/AccountMultPolicy.ashx",{
        asynchronous:true,
        method:'post',        
        parameters:{custId:arrCustId,user:arrUser,dept:dept,deptid:deptid,isLstUser:isListUser},
        onComplete:function(strdata)
        {
            DelayReloadPage('listPolicy',strdata.responseText);
        }               
    });*/    
    if(arrUser.length > 64)
    {
        arrUser = arrUser.substr(0, 60);        
        arrUser = arrUser.substr(0, arrUser.lastIndexOf(",")) + ", ...";        
    }
    else
        arrUser= arrUser.substring(0,arrUser.length - 2);
    DoCal('UpdateMultiAdmins.aspx?custId=' + arrCustId + '&user=' + arrUser + '&dept=' + dept + '&depid=' + deptid + '&isListUser=' + isListUser, arrUser, 450, 340);
}  
function OpenUpdateMult(deptid,dept,isListUser)
{
    var elements=$$("div .chk");
    var hasCheck=false;
    var strCustid="";
    var strUser="";
    var counter = 0;
    for (var i=0;i<elements.length;i++)
    {       
        counter++;         
        if(elements[i].id !="chkAll") {
            if (elements[i].hasClassName("ic-check") && elements[i].id.indexOf("_")>=0) 
            {                 
                var arr=elements[i].id.split("_");
                if(counter==elements.length)
                {
                    strCustid +=arr[0];  
                    strUser +=arr[1];                               
                }
                else
                {
                    strCustid +=arr[0]+",";  
                    strUser +=arr[1] + ", "; 
                }
                hasCheck=true;                                                
            }
        }
    } 
    
    if(hasCheck)        
    {  
       
        LoadMultUpdate(strCustid,strUser,dept,deptid,isListUser);
    }
    else
    {
        
        ErrAlter("Please choose at least one account.");                    
                        
    }
}

function OnCheckHost(funcId, subc) {
    var idH = "chkH";
    var idR = "chkR";
    var chkHost = $(idH + "_" + funcId);
    var chkRead = $(idR + "_" + funcId);
    var hdChkReadID = "hd" + idR + "_" + funcId;
    if (chkHost.hasClassName("ic-check")) {
        if (chkRead.hasClassName("ic-uncheck")) {
            chkRead.className = "chk ic-check";
            $(hdChkReadID).value = true;
        }
    }
}

function OnCheckRead(funcId, subc) {
    var idH = "chkH";
    var idR = "chkR";
    var chkHost = $(idH + "_" + funcId);
    var hdChkHostID = "hd" + idH + "_" + funcId;
    var chkRead = $(idR + "_" + funcId);
    if (chkRead.hasClassName("ic-uncheck")) {
        if (chkHost.hasClassName("ic-check")) {
            chkHost.className = "chk ic-uncheck";
            $(hdChkHostID).value = false;
        }
    }
}

function changeDivChkState(chk) {
    if (chk.hasClassName("ic-check"))
        chk.className = "chk ic-uncheck";
    else
        chk.className = "chk ic-check";
}   