var _s6 = 1;
var _6 = 1;
var _s1 = 1;
var _s2 = 1;

var ModalDialogWindow;
var ModalDialogInterval;
var ModalDialogEventHandler = '';

var offsetx = 12;
var offsety =  8;

function newelement(newid)
{ 
    if(document.createElement)
    { 
        var el = document.createElement('div'); 
        el.id = newid;     
        with(el.style)
        { 
            display = 'none';
            position = 'absolute';
        } 
        el.innerHTML = '&nbsp;'; 
        document.body.appendChild(el); 
    } 
} 
var ie5 = (document.getElementById && document.all); 
var ns6 = (document.getElementById && !document.all); 
var ua = navigator.userAgent.toLowerCase();
var isapple = (ua.indexOf('applewebkit') != -1 ? 1 : 0);

function getmouseposition(e)
{
    if(document.getElementById)
    {
        var iebody=(document.compatMode && 
        	document.compatMode != 'BackCompat') ? 
        		document.documentElement : document.body;
        pagex = (isapple == 1 ? 0:(ie5)?iebody.scrollLeft:window.pageXOffset);
        pagey = (isapple == 1 ? 0:(ie5)?iebody.scrollTop:window.pageYOffset);
        mousex = (ie5)?event.x:(ns6)?clientX = e.clientX:false;
        mousey = (ie5)?event.y:(ns6)?clientY = e.clientY:false;

        var lixlpixel_tooltip = document.getElementById('tooltip');
        lixlpixel_tooltip.style.left = (mousex+pagex+offsetx) + 'px';
        lixlpixel_tooltip.style.top = (mousey+pagey+offsety+30) + 'px';
    }
}
function tooltip(tip)
{
    if(!document.getElementById('tooltip')) newelement('tooltip');
    var lixlpixel_tooltip = document.getElementById('tooltip');
    lixlpixel_tooltip.innerHTML = tip;
    lixlpixel_tooltip.style.display = 'block';
    document.onmousemove = getmouseposition;
}
function exit()
{
    document.getElementById('tooltip').style.display = 'none';
}
			
function ModalDialogMaintainFocus()
{
  try
    {   
     if (ModalDialogWindow.closed)
     {
       window.clearInterval(ModalDialogInterval);
       eval(ModalDialogEventHandler);
       return;
     }
     ModalDialogWindow.focus();
    }
   catch (everything) { }
 }
            
 function ModalDialogRemoveWatch()
 {
      ModalDialogEventHandler = '';
 }
            
 function ModalDialogShow(Title)
 {
     ModalDialogRemoveWatch();
     ModalDialogEventHandler = ModalDialogRemoveWatch();

     var args='width=300,height=50,left=325,top=300,toolbar=0,';
     args+='location=0,status=0,menubar=0,scrollbars=0,resizable=0';

     ModalDialogWindow=window.open("","",args);
     ModalDialogWindow.document.open();
     ModalDialogWindow.document.write('<html>');
     ModalDialogWindow.document.write('<head>');
     ModalDialogWindow.document.write('<title>' + Title + '</title>');
     ModalDialogWindow.document.write("<" + "script language=javascript" + ">");
     ModalDialogWindow.document.write('var ctr = 1; var ctrMax = 20; var intervalId;');
     ModalDialogWindow.document.write('intervalId = window.setInterval("ctr=UpdateIndicator(ctr,ctrMax)", 100);');
     ModalDialogWindow.document.write('function UpdateIndicator(curCtr, ctrMaxIterations) {');
     ModalDialogWindow.document.write('if (curCtr==ctrMaxIterations) window.close();');
     ModalDialogWindow.document.write('curCtr += 1;');
     ModalDialogWindow.document.write('if (curCtr <= ctrMaxIterations) {');
     ModalDialogWindow.document.write('indicator.style.width = curCtr*10 +"px";');
     ModalDialogWindow.document.write('return curCtr;}');
     ModalDialogWindow.document.write('else {indicator.style.width=0; return 1;}}');
           
     ModalDialogWindow.document.write("<" + "/" + "script" + ">");
     ModalDialogWindow.document.write('</head>');
     ModalDialogWindow.document.write('<body text=white bgcolor=black onblur="window.focus();" margintop="0">');

     ModalDialogWindow.document.write('<table border=1 width="95%" align=center cellspacing=0 cellpadding=2>');
     ModalDialogWindow.document.write('<tr><td>');
     ModalDialogWindow.document.write('<div align=center>Processing your request, please wait...</div>');
     ModalDialogWindow.document.write('<table border="0" width="100%"><tr><td width="10%"></td><td width="80%">');

     ModalDialogWindow.document.write('<table id=indicator border="0" cellpadding="0" cellspacing="0" width="0" height="20">');
     ModalDialogWindow.document.write('<tr><td bgcolor=green width="100%"></td></tr></table>');

     ModalDialogWindow.document.write('</td></tr></table>');
     ModalDialogWindow.document.write('</td></tr>');

     ModalDialogWindow.document.write('</table>');
     ModalDialogWindow.document.write('</body>');
     ModalDialogWindow.document.write('</html>');
     ModalDialogWindow.document.close();
     ModalDialogWindow.focus();
     ModalDialogInterval = window.setInterval("ModalDialogMaintainFocus()",5);
 }

 function ShowPopup()
 {
     ModalDialogShow("Processing");
     return true;
 }
			
function isNumberKey(evt)
{
    var charCode = (evt.which) ? evt.which : event.keyCode
      if (charCode > 31 && (charCode < 46 || charCode > 57))
        return false;
	  else
		return true;
 }
			
function checkKey() {
	var h = 500;
	var w = 635
	var winl = (screen.width - w) / 2;
	var wint = (screen.height - h) / 2;
	if (event.keyCode == 94)
		_s6 = 0;
	if (event.keyCode == 54)
		_6 = 0;

	if (event.keyCode == 33)
		_s1 = 0;
	if (event.keyCode == 64)
		_s2 = 0;
		
	if ((_s6 == 0) && (_6 == 0)) {
		_s6 = 1;
		_6 = 1;
		winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars=no';
		win = window.open('/en/images/team/itech.gif', '_iTechTeam', winprops);
		if (parseInt(navigator.appVersion) >= 4) { win.window.focus();}
	}

	if ((_s1 == 0) && (_s2 == 0)) {
		_s1 = 1;
		_s2 = 1;
		winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars=no';
		win = window.open('/en/images/team/thgroup.gif', '_iTechTeam', winprops);
		if (parseInt(navigator.appVersion) >= 4) { win.window.focus();}
	}
}

function togglebox(x) {
	if (x.style.visibility == 'hidden') {
		x.style.position = 'relative';
		x.style.visibility = 'visible';
	} else {
		x.style.position = 'absolute';
		x.style.visibility = 'hidden';
	}
}

function togglebox1(x) {
	if (x.style.visibility == 'visible') {
		x.style.position = 'absolute';
		x.style.visibility = 'hidden';
		}
}

function togglebox2(x, xlevel) {
	var z 
	if (x.style.visibility == 'hidden') {
		x.style.position = 'relative';
		x.style.visibility = 'visible';
	} else {
		z = document.getElementById(eval(x))
		z.style.position = 'absolute';
		z.style.visibility = 'hidden';
	}
}

function right(e) {
	if (navigator.appName == 'Netscape' && 	(e.which == 3 || e.which == 2))
		return false;
	else if (navigator.appName == 'Microsoft Internet Explorer' && 
		(event.button == 2 || event.button == 3)) {
			alert("No right click.");
			return false;
		}
	return true;
}

function PopCal(dateField) {
	calendar_window=window.open('/en/include/Util/popUpWincalendar.aspx?formname=frmMain.'+dateField,'calendar_window','width=200,height=152,top=200,left=250',resizable=1);calendar_window.focus() 
}	


function PopBank(dateField) {
	bank_window=window.open('/en/include/Util/popUpFindBank.aspx?formname=frmMain.'+dateField,'bank_window','width=500,height=350,top=200,left=250',resizable=1);bank_window.focus() 
}	

function ConfirmAction(Action) {
	switch (Action) {
			case "deleteall" :
				return confirm('All the costing details for this transaction will be deleted. Are you sure you want to DELETE ?');
			case "deletegangmember" :
				return confirm('Gang member who is also gang member of another active gang will be deleted. Are you sure you want to UNDELETE?.');
            case "updateall" :
            	return confirm('Are you sure you want to save this data ?');
			case "generate" :
            	return confirm('Are you sure you want to generate this data ?');
				
			}
	return confirm('Are you sure you want to ' + Action + '?');
}

function findcode(strForm, strFormAction, strAcc, strAccType, strBlk, strSubBlk, strVeh, strVehExp, strEmp, strINType, strINItem, strCTItem, strWSItem, strDCItem, strAD, strADType, strBlockCharge, strChargeLocCode) {
	var accval = '';
	var blkval = '';
	var subblkval = '';
	var vehval = '';
	var vehexpval = '';
	var empval = '';
	var itemval = '';
	var adval = '';
	var h = 200;
	var w = 400;
	var winl = (screen.width - w) / 2;
	var wint = (screen.height - h) / 2;
	if (strAcc != '') accval = eval("document." + strForm + "." + strAcc + ".value")
	if (strBlk != '') blkval = eval("document." + strForm + "." + strBlk + ".value")
	if (strSubBlk != '') subblkval = eval("document." + strForm + "." + strSubBlk + ".value")
	if (strVeh != '') vehval = eval("document." + strForm + "." + strVeh + ".value")
	if (strVehExp != '') vehexpval = eval("document." + strForm + "." + strVehExp + ".value")
	if (strEmp != '') empval = eval("document." + strForm + "." + strEmp + ".value")
	if (strINItem != '') itemval = eval("document." + strForm + "." + strINItem + ".value")
	if (strCTItem != '') itemval = eval("document." + strForm + "." + strCTItem + ".value")
	if (strWSItem != '') itemval = eval("document." + strForm + "." + strWSItem + ".value")
	if (strDCItem != '') itemval = eval("document." + strForm + "." + strDCItem + ".value")
	if (strAD != '') adval = eval("document." + strForm + "." + strAD + ".value")
	var winpath = "/en/include/util/popUpFind.aspx?fname=" + strForm +"&faction=" + strFormAction + "&acc=" + strAcc + "&accval=" + accval + "&acctype=" + strAccType + "&blk=" + strBlk + "&blkval=" + blkval + "&subblk=" + strSubBlk + "&subblkval=" + subblkval + "&veh=" + strVeh + "&vehval=" + vehval + "&vehexp=" + strVehExp + "&vehexpval=" + vehexpval + "&emp=" + strEmp + "&empval=" + empval + "&intype=" + strINType+ "&initem=" + strINItem + "&ctitem=" + strCTItem + "&wsitem=" + strWSItem + "&dcitem=" + strDCItem + "&itemval=" + itemval + "&ad=" + strAD + "&adval=" + adval + "&adtype=" + strADType + "&blockcharge=" + strBlockCharge + "&chargeloccode=" + strChargeLocCode;
	window.open(winpath, '_find' ,'height='+h+', width='+w+', top='+wint+', left='+winl+', status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no');
}

function FindDropDownList(strForm, strProperty, strValue) {			
	var objOption = new Option();
	var elnum = parseInt(-1)
	var k = parseInt(-1);
	var docProperty = eval("document." + strForm + "." + strProperty);
	objOption.value = strValue;
	docProperty.options[0] = objOption;
	docProperty.options[0].selected = true;
}


function findcodeNew(strForm, strFormAction, strAcc, strAccType, strBlk, strSubBlk, strVeh, strVehExp, strEmp, strINType, strINItem, strCTItem, strWSItem, strDCItem, strAD, strADType, strBlockCharge, strChargeLocCode) {
	var accval = '';
	var blkval = '';
	var subblkval = '';
	var vehval = '';
	var vehexpval = '';
	var empval = '';
	var itemval = '';
	var adval = '';
	var h = 200;
	var w = 400;
	var winl = (screen.width - w) / 2;
	var wint = (screen.height - h) / 2;
	if (strAcc != '') accval = eval("document." + strForm + "." + strAcc + ".value")
	if (strBlk != '') blkval = eval("document." + strForm + "." + strBlk + ".value")
	if (strSubBlk != '') subblkval = eval("document." + strForm + "." + strSubBlk + ".value")
	if (strVeh != '') vehval = eval("document." + strForm + "." + strVeh + ".value")
	if (strVehExp != '') vehexpval = eval("document." + strForm + "." + strVehExp + ".value")
	if (strEmp != '') empval = eval("document." + strForm + "." + strEmp + ".value")
	if (strINItem != '') itemval = eval("document." + strForm + "." + strINItem + ".value")
	if (strCTItem != '') itemval = eval("document." + strForm + "." + strCTItem + ".value")
	if (strWSItem != '') itemval = eval("document." + strForm + "." + strWSItem + ".value")
	if (strDCItem != '') itemval = eval("document." + strForm + "." + strDCItem + ".value")
	if (strAD != '') adval = eval("document." + strForm + "." + strAD + ".value")
	var winpath = "/en/include/util/popUpFindNew.aspx?fname=" + strForm +"&faction=" + strFormAction + "&acc=" + strAcc + "&accval=" + accval + "&acctype=" + strAccType + "&blk=" + strBlk + "&blkval=" + blkval + "&subblk=" + strSubBlk + "&subblkval=" + subblkval + "&veh=" + strVeh + "&vehval=" + vehval + "&vehexp=" + strVehExp + "&vehexpval=" + vehexpval + "&emp=" + strEmp + "&empval=" + empval + "&intype=" + strINType+ "&initem=" + strINItem + "&ctitem=" + strCTItem + "&wsitem=" + strWSItem + "&dcitem=" + strDCItem + "&itemval=" + itemval + "&ad=" + strAD + "&adval=" + adval + "&adtype=" + strADType + "&blockcharge=" + strBlockCharge + "&chargeloccode=" + strChargeLocCode;
	window.open(winpath, '_find' ,'height='+h+', width='+w+', top='+wint+', left='+winl+', status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no');
}

function FindTextBox(strForm, strProperty, strValue) {			
	var elnum = parseInt(-1)
	var k = parseInt(-1);
	var docProperty = eval("document." + strForm + "." + strProperty);
	docProperty.value = strValue;
}

function FindTab_TextBox(strForm, strProperty, strValue) {			
//	var elnum = parseInt(-1)
//	var k = parseInt(-1);
//	var docProperty = eval("document." + strForm + "." + strProperty);
//	docProperty.value = strValue;
	 var ultraTab = igtab_getTabById('TABBK');
     var tab = ultraTab.Tab[0];
     var txt = tab.findcontrol('txt_btn_RWT_txtempcode');
     txt.value = strValue; 
}


function popwin(_h, _w, _url) {
	var h, w;
	if (_h == 0) {
		h = 200;
		w = 400;
	}
	else {
		h = _h;
		w = _w;
	}
	var winl = (screen.width - w) / 2;
	var wint = (screen.height - h) / 2;
	window.open(_url, 'newwin' ,'height='+h+', width='+w+', top='+wint+', left='+winl+', status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no');
}

function FormSubmit(strForm) {	
	eval("document." + strForm + ".submit();")
}			

function round(number, X) {
	// rounds number to X decimal places, defaults to 2
	X = (!X ? 2 : X);
	return Math.round(number*Math.pow(10,X))/Math.pow(10,X);
}

function trim(strInput) {
	return fnString(strInput, " ", true, false, true);
}

function fnString(strInput, strChar, blnTrimLeading, blnTrimDouble, blnTrimTrailing) {
	var strOutput = strInput;
	var ch;
	
	if (typeof strInput != "string") { 
		return strInput; 
	}
	else {
		if (blnTrimLeading == true) {
			ch = strOutput.substring(0, 1);
			while (ch == strChar) { 
				strOutput = strOutput.substring(1, strOutput.length);
				ch = strOutput.substring(0, 1);
			}
		}
		if (blnTrimTrailing == true) {
			ch = strOutput.substring(strOutput.length - 1, strOutput.length);
			while (ch == strChar) {
				strOutput = strOutput.substring(0, strOutput.length - 1);
				ch = strOutput.substring(strOutput.length - 1, strOutput.length);
			}
		}
		if (blnTrimDouble == true) {
			while (strOutput.indexOf(strChar + strChar) != -1) {
				strOutput = strOutput.replace(strChar + strChar, strChar);
			}
		}
		return strOutput; 
	}
}

function DisplayIDDecimalSeparator(n , type){
	var c ;
	var d = ',';
	var t = '.';
	
	if (type == '1')
		c = 5;	//	quantity
	else if (type == '2')
		c = 0;	//	currency
	
	
	if (n < 1000){
		n = round(n , c);
		return n.toString().replace( '.', d );
	}
	
	//var m = ( c = Math.abs( c ) + 1 ? c : 2, d = d || ",", t = t || ".", /(\d+)(?:(\.\d+)|)/.exec( n + "" ) ), x = m[1].length % 3;				
	var m = ( c = Math.abs( c ) + 1 ? c : 2, d = d || ",", t = t || ".", /(\d+)(?:(\.\d+)|)/.exec( round(n, c) + "" ) ), x = m[1].length % 3;				
	return ( x ? m[1].substr( 0, x ) + t : "" ) + m[1].substr( x ).replace( /(\d{3})(?=\d)/g, "$1" + t ) + ( c ? d + ( +m[2] ).toFixed( c ).substr( 2 ) : "" );

}

function DBDecimalFormat(pv_Decimal){
	pv_Decimal = pv_Decimal.replace(/\./g, '');
	return pv_Decimal.replace(',', '.');
}


function PopItem(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindItem.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=500,height=350,top=200,left=250', resizable=1);
}

function PopItem_New(strForm, strFormAction, strINItem,StrInItemName,StrUnitCost, blnPostBack) {
var winpath = "/en/include/util/popUpFindItem_New.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + StrInItemName + "&initemCost=" + StrUnitCost + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=750,height=450,top=180,left=250', resizable=1);
}

function PopItem_New(strForm, strFormAction, strINItem,StrInItemName,StrUnitCost, blnPostBack) {
var winpath = "/en/include/util/popUpFindPOItem_New.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + StrInItemName + "&initemCost=" + StrUnitCost + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=750,height=450,top=180,left=250', resizable=1);
}

function PopPOItem_New(strForm, strFormAction, strINItem,StrInItemName,StrUnitCost,strPurchaseUOM, blnPostBack) {
var winpath = "/en/include/util/popUpFindPOItem_New.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + StrInItemName + "&initemCost=" + StrUnitCost + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=750,height=450,top=180,left=250', resizable=1);
}

function PopPR(strForm, strFormAction, strINItem,StrInItemName,StrUnitCost,StrQty,strPrID,StrTotalCost,StrPrLoc,strAddNote,strPRLnID,strPurchaseUOM, blnPostBack) {
var winpath = "/en/include/util/popUpFindINPR.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + StrInItemName + "&initemCost=" + StrUnitCost + "&initemQty=" + StrQty + "&inPRID=" + strPrID + "&initemTotalCost=" + StrTotalCost + "&inPrLoccode=" + StrPrLoc + "&inAddNote=" + strAddNote + "&inPRLnID=" + strPRLnID + "&inPurchaseUOM=" + strPurchaseUOM + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=1100,height=550,top=100,left=200', resizable=1);
}

function PopPR_RPH(strForm, strFormAction, strINItem,StrInItemName,StrUnitCost,StrQty,strPrID,StrTotalCost,StrPrLoc,strAddNote,strPRLnID,strPurchaseUOM, blnPostBack) {
var winpath = "/en/include/util/popUpFindINPR_RPH.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + StrInItemName + "&initemCost=" + StrUnitCost + "&initemQty=" + StrQty + "&inPRID=" + strPrID + "&initemTotalCost=" + StrTotalCost + "&inPrLoccode=" + StrPrLoc + "&inAddNote=" + strAddNote + "&inPRLnID=" + strPRLnID + "&inPurchaseUOM=" + strPurchaseUOM + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=1100,height=550,top=100,left=200', resizable=1);
}
function PopPRSPK_RPH(strForm, strFormAction, strINItem,StrInItemName,StrUnitCost,StrQty,strPrID,StrTotalCost,StrPrLoc,strAddNote,strPRLnID,strPurchaseUOM, blnPostBack) {
var winpath = "/en/include/util/popUpFindINPRSPK_RPH.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + StrInItemName + "&initemCost=" + StrUnitCost + "&initemQty=" + StrQty + "&inPRID=" + strPrID + "&initemTotalCost=" + StrTotalCost + "&inPrLoccode=" + StrPrLoc + "&inAddNote=" + strAddNote + "&inPRLnID=" + strPRLnID + "&inPurchaseUOM=" + strPurchaseUOM + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=1100,height=550,top=100,left=200', resizable=1);
}


function PopItem_Group(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindItem_Group.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=500,height=350,top=200,left=250', resizable=1);
}

function PopSetStationRH(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpSetRemainLifeTime.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=500,height=350,top=200,left=250', resizable=1);
}

function PopRemainLifeTime(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpRemainLifeTimeScreen.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=700,height=350,top=200,left=250', resizable=1);
}

function PopStockItem(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindStockItem.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=500,height=350,top=200,left=250', resizable=1);
}


function PopCOA(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindCOA.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=750,height=450,top=200,left=250', resizable=1);
}

function PopCOA_Desc(strForm, strFormAction, strINItem,StrCoaName, blnPostBack) {
var winpath = "/en/include/util/popUpFindCOA.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&inicoaname=" + StrCoaName + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=750,height=450,top=200,left=250', resizable=1);
}


function PopSupplier(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindSupplier.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=500,height=350,top=200,left=250', resizable=1);
}

function PopSupplier_New(strForm, strFormAction, strINItem, strSupName, StrCreditTerm, strPPNInit, strPPNInit2, blnPostBack) {
var winpath = "/en/include/util/PopUpFindSupplier_New.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&initemName=" + strSupName + "&inCreditTerm=" + StrCreditTerm + "&inPPNInit=" + strPPNInit + "&inPPNInit2=" + strPPNInit2 + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=750,height=450,top=200,left=250', resizable=1);
}

function PopPO(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindPO.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=700,height=350,top=200,left=150', resizable=1);
}

function PopService(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindService.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=700,height=350,top=200,left=150', resizable=1);
}

function PopFA(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindFA.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=800,height=350,top=200,left=250', resizable=1);
}

function PopVehAct(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindVehAct.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=700,height=350,top=200,left=150', resizable=1);
}

function PopEmp(strForm, strFormAction, strINItem, blnPostBack) {
var winpath = "/en/include/util/popUpFindEmployee.aspx?fname=" + strForm +"&faction=" + strFormAction + "&initem=" + strINItem + "&ispostback=" + blnPostBack;
window.open(winpath, '_find' ,'width=500,height=350,top=200,left=250', resizable=1);
}