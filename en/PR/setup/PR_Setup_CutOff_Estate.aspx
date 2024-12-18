<%@ Page Language="vb" src="../../../include/PR_setup_CutOff_Estate.aspx.vb" Inherits="PR_setup_AttdList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance Code List</title>
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
		
	<script language='javascript'>
	function LastDayOfMonth(Year, Month)
      {
          var doc = document.frmMain;
          var i;
           
           if (doc.txtqty.value == '')
           i = 0;
           else
           i = doc.txtqty.value; 
                                
           mydate = new Date((new Date(Year, Month+1,1))-1).getDate()-i
           
           if (Month < 9)
           mymonth='0'+(Month+1);
           else
           mymonth=(Month+1);
           
           myyear=Year;
           
           return(mydate+'/'+mymonth+'/'+myyear)
           
      }
    
    function setall()
     {
     var doc = document.frmMain;
     doc.txtyr1.value = doc.txtAttdCode.value;doc.txtdt1.value = LastDayOfMonth(doc.txtyr1.value,0);        
     doc.txtyr2.value = doc.txtAttdCode.value;doc.txtdt2.value = LastDayOfMonth(doc.txtyr1.value,1);
     doc.txtyr3.value = doc.txtAttdCode.value;doc.txtdt3.value = LastDayOfMonth(doc.txtyr1.value,2);
     doc.txtyr4.value = doc.txtAttdCode.value;doc.txtdt4.value = LastDayOfMonth(doc.txtyr1.value,3);
     doc.txtyr5.value = doc.txtAttdCode.value;doc.txtdt5.value = LastDayOfMonth(doc.txtyr1.value,4);
     doc.txtyr6.value = doc.txtAttdCode.value;doc.txtdt6.value = LastDayOfMonth(doc.txtyr1.value,5);
     doc.txtyr7.value = doc.txtAttdCode.value;doc.txtdt7.value = LastDayOfMonth(doc.txtyr1.value,6);
     doc.txtyr8.value = doc.txtAttdCode.value;doc.txtdt8.value = LastDayOfMonth(doc.txtyr1.value,7);
     doc.txtyr9.value = doc.txtAttdCode.value;doc.txtdt9.value = LastDayOfMonth(doc.txtyr1.value,8);
     doc.txtyr10.value = doc.txtAttdCode.value;doc.txtdt10.value = LastDayOfMonth(doc.txtyr1.value,9);
     doc.txtyr11.value = doc.txtAttdCode.value;doc.txtdt11.value = LastDayOfMonth(doc.txtyr1.value,10);
     doc.txtyr12.value = doc.txtAttdCode.value;doc.txtdt12.value = LastDayOfMonth(doc.txtyr1.value,11);
     } 
     
	</script>
      
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
<tr>
	<td style="width: 100%; height: 1500px" valign="top">
	<div class="kontenlist">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding=1 width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="3">
                      <strong>CUT OFF SETTING</strong>  </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td height="26" style="width: 7%">
                                    Years :<BR><asp:TextBox id=txtAttdCode width=100% maxlength="8" runat="server" /></td>
								<td height="26" style="width: 7%">
                                    Qty :<BR><asp:TextBox id=txtqty width="53%" maxlength="2" runat="server" /> Days</td>
								<td height="26" style="width: 14%" valign="bottom">
                                    &nbsp;<asp:Button id=SearchBtn Text="Setting" class="button-small"  runat="server"/></td>
								<td width="20%" height="26"></td>
								<td width="10%" height="26" valign=bottom align=right></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>
                    	<table width="30%" cellspacing="0" cellpadding="3" border="0" align="left" class="font9Tahoma">
							 <tr class="mb-t">
								<td width="25%" height="26">Month</td>
								<td width="20%" height="26">Year</td>
								<td height="26" style="width: 9%">Date</td>
                             </tr>
                             <tr>
								<td width="25%" height="26">Jan</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr1 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt1 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt1');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Feb</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr2 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt2 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt2');"><asp:Image id="Image1" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Mar</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr3 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt3 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt3');"><asp:Image id="Image2" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Apr</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr4 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt4 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt3');"><asp:Image id="Image3" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">May</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr5 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt5 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt5');"><asp:Image id="Image4" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Jun</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr6 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt6 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt6');"><asp:Image id="Image5" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Jul</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr7 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt7 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt7');"><asp:Image id="Image6" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Aug</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr8 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt8 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt8');"><asp:Image id="Image7" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Sep</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr9 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt9 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt9');"><asp:Image id="Image8" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Oct</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr10 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt10 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt10');"><asp:Image id="Image9" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Nov</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr11 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt11 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt11');"><asp:Image id="Image10" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                             
                              <tr>
								<td width="25%" height="26">Des</td>
								<td width="20%" height="26"><asp:TextBox id=txtyr12 Width="100%" runat=server /></td>
								<td width="40%" height="26"><asp:TextBox id=txtdt12 Width="70%" runat=server />
                                <a href="javascript:PopCal('txtdt12');"><asp:Image id="Image11" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
                             </tr>
                         </table>           		   					
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
                        <asp:ImageButton ID="btnsave" runat="server" onclick="btnsave_click" AlternateText="Save" ImageUrl="../../images/butt_save.gif"
                             />
                        &nbsp;&nbsp;
					</td>
				</tr>
			</table>
     	</div>
        </td>
        </tr>
        </table>

		</FORM>
	</body>
</html>
