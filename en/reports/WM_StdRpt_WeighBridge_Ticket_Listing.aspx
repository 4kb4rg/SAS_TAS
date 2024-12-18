<%@ Page Language="vb" src="../../include/reports/WM_StdRpt_WeighBridge_Ticket_Listing.aspx.vb" Inherits="WM_StdRpt_WeighBridge_Ticket_Listing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Weighing Management - WeighBridge Ticket Transaction Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblLocation" visible="false" runat="server" />
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />			
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> WEIGHING MANAGEMENT - WEIGHBRIDGE TICKET TRANSACTION LISTING</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:WM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>			
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">
				<tr>
					<td>Ticket No. : </td>
					<td><asp:textbox id="txtTicketNo" width="50%" maxlength="20" runat="server" /></td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>Transaction Type : </td>
					<td><asp:DropDownList id="lstTransactionType" width="50%" size="1" runat="server" /></td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>Product : </td>
					<td><asp:DropDownList id="lstProduct" width="50%" size="1" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>			
				<tr>
					<td>Supplier / <asp:Label id="lblBillParty1" runat=server/> : </td>
					<td><asp:textbox id="txtSuppBillParty" maxlength=8 width="50%"  runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>			
				<tr>
					<td>Supplier / <asp:Label id="lblBillParty2" runat=server/> Doc Ref No. : </td>
					<td><asp:textbox id="txtDocRefNo" maxlength=20  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td>Delivery Note No. : </td>
					<td><asp:textbox id="txtDeliveryNoteNo" maxlength=20  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td>PL3 No. : </td>
					<td><asp:textbox id="txtPL3No" maxlength=20  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td>Transporter: </td>
					<td><asp:TextBox id="txtTransporter" maxlength="16" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>							
				<tr>
					<td><asp:label id="lblVehicle" runat="server" /> : </td>
					<td><asp:textbox id="txtVehicle" maxlength=8  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td>Driver's Name : </td>
					<td><asp:textbox id="txtDriverName" maxlength=128 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td>Driver's I/C No. : </td>
					<td><asp:textbox id="txtDriverICNo" maxlength=18  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td>Planting Year : </td>
					<td><asp:textbox id="txtPlantingYear" maxlength=32  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>						
				<tr>
					<td><asp:label id="lblBlockTag" runat=server /> :</td>
					<td><asp:textbox id="txtBlock" maxlength=8  width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>						
				<tr>
					<td width="17%">Date In From : </td>
					<td width="39%"><asp:TextBox id="txtDateIn" maxlength="10"  width="50%" runat="server"/>
 					    <a href="javascript:PopCal('txtDateIn');">
						<asp:Image id="btnSelDateInFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
					<td width="4%">To : </td>
					<td width="40%"><asp:TextBox id="txtDateInTo" maxlength="10" width="50%" runat="server"/>
 					    <a href="javascript:PopCal('txtDateInTo');">
						<asp:Image id="btnSelDateInTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				
				<tr>
					<td>Time In From : </td>
					<td><asp:TextBox id="txtInHour" runat="server"  width="15%" EnableViewState=False  maxlength="2" size="3"/> :
						<asp:TextBox id="txtInMinute" runat="server" width="15%" EnableViewState=False maxlength="2" size="3"/> 
						<asp:DropDownList id="lstInAMPM" width="16%" size="1" runat="server" >
							<asp:listitem id="InAM" text="AM" value=1 runat="server" size="1" />
							<asp:listitem id="InPM" text="PM" value=2 runat="server" size="1" />							
						</asp:DropDownList>	HH:MM <BR>					
						<asp:CompareValidator id="ChcktxtInHour" runat="server" Display=Dynamic
							ControlToValidate="txtInHour" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtInHourRange"
							ControlToValidate="txtInHour"
							MinimumValue="1"
							MaximumValue="12"
							Type="Integer"
							Text="Value of hour is from 1 to 12 !"
							runat="server" Display=Dynamic/>													
						<asp:CompareValidator id="ChcktxtInMinute" runat="server" Display=Dynamic
							ControlToValidate="txtInMinute" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtInMinuteRange"
							ControlToValidate="txtInMinute"
							MinimumValue="0"
							MaximumValue="59"
							Type="Integer"
							Text="Value of minute is from 0 to 59 !"
							runat="server" Display=Dynamic/>	
						<asp:label id=lblErrTimeInHrFrom text="Hour cannot be blank." forecolor=red visible=false runat=server />					
						<asp:label id=lblErrTimeInMinFrom text="Minute cannot be blank." forecolor=red visible=false runat=server />					
					</td>
					<td>To : </td>
					<td><asp:TextBox id="txtInHourTo" runat="server" width="15%" EnableViewState=False  maxlength="2" size="3"/> :
						<asp:TextBox id="txtInMinuteTo" runat="server" width="15%" EnableViewState=False maxlength="2" size="3"/> 
						<asp:DropDownList id="lstInAMPMTo" width="16%" size="1" runat="server" >
							<asp:listitem id="InAMTo" text="AM" value=1 runat="server" size="1" />
							<asp:listitem id="InPMTo" text="PM" value=2 runat="server" size="1" />							
						</asp:DropDownList>	HH:MM <BR>					
						<asp:CompareValidator id="ChcktxtInHourTo" runat="server" Display=Dynamic
							ControlToValidate="txtInHourTo" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtInHourRangeTo"
							ControlToValidate="txtInHourTo"
							MinimumValue="1"
							MaximumValue="12"
							Type="Integer"
							Text="Value of hour is from 1 to 12 !"
							runat="server" Display=Dynamic/>													
						<asp:CompareValidator id="ChcktxtInMinuteTo" runat="server" Display=Dynamic
							ControlToValidate="txtInMinuteTo" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtInMinuteRangeTo"
							ControlToValidate="txtInMinuteTo"
							MinimumValue="0"
							MaximumValue="59"
							Type="Integer"
							Text="Value of minute is from 0 to 59 !"
							runat="server" Display=Dynamic/>						
						<asp:label id=lblErrTimeInHrTo text="Hour cannot be blank." forecolor=red visible=false runat=server />					
						<asp:label id=lblErrTimeInMinTo text="Minute cannot be blank." forecolor=red visible=false runat=server />					
					</td>
				</tr>				
				<tr>					
					<td>Date Out From : </td>
					<td><asp:TextBox id="txtDateOut" maxlength="10"  width="50%" runat="server"/>
  					    <a href="javascript:PopCal('txtDateOut');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
					<td>To: </td>
					<td><asp:TextBox id="txtDateOutTo" maxlength="10"  width="50%" runat="server"/>
  					    <a href="javascript:PopCal('txtDateOutTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				<tr>
					<td>Time Out From : </td>
					<td><asp:TextBox id="txtOutHour" runat="server"  width="15%" EnableViewState=False  maxlength="2" size="3"/> :
						<asp:TextBox id="txtOutMinute" runat="server" width="15%" EnableViewState=False maxlength="2" size="3"/> 
						<asp:DropDownList id="lstOutAMPM" width="16%" size="1" runat="server" >
							<asp:listitem id="OutAM" text="AM" value=1 runat="server" size="1" />
							<asp:listitem id="OutPM" text="PM" value=2 runat="server" size="1" />							
						</asp:DropDownList>	HH:MM <BR>					
						<asp:CompareValidator id="ChcktxtOutHour" runat="server" Display=Dynamic
							ControlToValidate="txtOutHour" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtOutHourRange"
							ControlToValidate="txtOutHour"
							MinimumValue="1"
							MaximumValue="12"
							Type="Integer"
							Text="Value of hour is from 1 to 12 !"
							runat="server" Display=Dynamic/>													
						<asp:CompareValidator id="ChcktxtOutMinute"  runat="server" Display=Dynamic
							ControlToValidate="txtOutMinute" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtOutMinuteRange"
							ControlToValidate="txtOutMinute"
							MinimumValue="0"
							MaximumValue="59"
							Type="Integer"
							Text="Value of minute is from 0 to 59 !"
							runat="server" Display=Dynamic/>						
						<asp:label id=lblErrTimeOutHrFrom text="Hour cannot be blank." forecolor=red visible=false runat=server />					
						<asp:label id=lblErrTimeOutMinFrom text="Minute cannot be blank." forecolor=red visible=false runat=server />					
					</td>
					<td>To: </td>
					<td><asp:TextBox id="txtOutHourTo" runat="server"  width="15%" EnableViewState=False  maxlength="2" size="3"/> :
						<asp:TextBox id="txtOutMinuteTo" runat="server" width="15%" EnableViewState=False maxlength="2" size="3"/> 
						<asp:DropDownList id="lstOutAMPMTo" width="16%" size="1" runat="server" >
							<asp:listitem id="OutAMTo" text="AM" value=1 runat="server" size="1" />
							<asp:listitem id="OutPMTo" text="PM" value=2 runat="server" size="1" />							
						</asp:DropDownList>	HH:MM <BR>					
						<asp:CompareValidator id="ChcktxtOutHourTo" runat="server" Display=Dynamic
							ControlToValidate="txtOutHourTo" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtOutHourRangeTo"
							ControlToValidate="txtOutHourTo"
							MinimumValue="1"
							MaximumValue="12"
							Type="Integer"
							Text="Value of hour is from 1 to 12 !"
							runat="server" Display=Dynamic/>													
						<asp:CompareValidator id="ChcktxtOutMinuteTo" runat="server" Display=Dynamic
							ControlToValidate="txtOutMinuteTo" Text="The value must whole number" 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtOutMinuteRangeTo"
							ControlToValidate="txtOutMinuteTo"
							MinimumValue="0"
							MaximumValue="59"
							Type="Integer"
							Text="Value of minute is from 0 to 59 !"
							runat="server" Display=Dynamic/>						
						<asp:label id=lblErrTimeOutHrTo text="Hour cannot be blank." forecolor=red visible=false runat=server />					
						<asp:label id=lblErrTimeOutMinTo text="Minute cannot be blank." forecolor=red visible=false runat=server />					
								
					</td>
				</tr>
				<tr>
					<td>Date Received From :</td>
					<td><asp:TextBox id="txtDateRcv" runat="server" width="50%" maxlength="10"/>
						<a href="javascript:PopCal('txtDateRcv');">
						<asp:Image id="btnSelRcvDate" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:Label id=lblErrDateRcv forecolor=red runat=server/>
						<asp:Label id=lblErrDateRcvMsg visible=false text="<br>Date Format should be in " runat=server/></td>
					<td>To :</td>
					<td><asp:TextBox id="txtDateRcvTo" runat="server"   width="50%" maxlength="10"/>
						<a href="javascript:PopCal('txtDateRcvTo');">
						<asp:Image id="btnSelRcvDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:Label id=lblErrDateRcvTo forecolor=red runat=server/>
						<asp:Label id=lblErrDateRcvToMsg visible=false text="<br>Date Format should be in " runat=server/></td>	
				</tr>	
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="lstStatus"  width="50%" size="1" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>
				</tr>				
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
