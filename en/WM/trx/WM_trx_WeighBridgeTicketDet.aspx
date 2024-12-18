<%@ Page Language="vb" trace=false Src="../../../include/WM_trx_WeighBridgeTicketDet.aspx.vb" Inherits="WM_WeighBridgeTicket_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_WMtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Weighing Management - WeighBridge Ticket Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="Javascript">
			function calnetweight() {
				var doc = document.frmMain;
				var fw = parseFloat(doc.txtFirstWgt.value);
				var sw = parseFloat(doc.txtSecondWgt.value);
				if (doc.hidtranstype.value == '2')
					doc.rsl.value = sw - fw;
				else
					doc.rsl.value = fw - sw;
				
				if (doc.rsl.value == 'NaN') 
					doc.rsl.value = '';
				else
					doc.rsl.value = round(doc.rsl.value, 5);
				document.getElementById(nw.id).innerHTML = doc.rsl.value;
			}

			function calcustnetweight() {
				var doc = document.frmMain;
				var cfw = parseFloat(doc.txtCustFirstWgt.value);
				var csw = parseFloat(doc.txtCustSecondWgt.value);
				if (doc.hidtranstype.value == '2')
					doc.rsl.value = cfw - csw;
				else
					doc.rsl.value = csw - cfw;
				
				if (doc.rsl.value == 'NaN') 
					doc.rsl.value = '';
				else
					doc.rsl.value = round(doc.rsl.value, 5);
				document.getElementById(cnw.id).innerHTML = doc.rsl.value;
			}
			
			function shownetwgt() {
				var doc = document.frmMain;
				doc.rsl.value = doc.hidNetWgt.value;
								
				if (doc.rsl.value == 'NaN') 
					doc.rsl.value = '';
				else
					doc.rsl.value = round(doc.rsl.value, 5);
				document.getElementById(nw.id).innerHTML = doc.rsl.value;
			}
			
			function showcustnetwgt() {
				var doc = document.frmMain;
				doc.rsl.value = doc.hidCustNetWgt.value;
								
				if (doc.rsl.value == 'NaN') 
					doc.rsl.value = '';
				else
					doc.rsl.value = round(doc.rsl.value, 5);
				document.getElementById(cnw.id).innerHTML = doc.rsl.value;
			}			
		</script>		
	</head>
	<body onload="javascript:shownetwgt();showcustnetwgt();">	
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<Input Type=Hidden id=rsl value="" runat=server />
			<Input Type=Hidden id=hidtranstype runat=server />
			<Input Type=Hidden id=hidNetWgt runat=server />
			<Input Type=Hidden id=hidCustNetWgt runat=server />
			<Input Type=Hidden id=hidTicketNo runat=server />
			<Input Type=Hidden id=hidMatchingID runat=server />
			<!--<Input Type=Hidden id=hidDateRcv runat=server />-->
			<Input Type=Hidden id=hidFirstWgt runat=server />
			<Input Type=Hidden id=hidSecondWgt runat=server />
			<Input Type=Hidden id=hidCalNetWgt runat=server />
			<Input Type=Hidden id=hidCustFirstWgt runat=server />
			<Input Type=Hidden id=hidCustSecondWgt runat=server />
			<Input Type=Hidden id=hidCalCustNetWgt runat=server />
									
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<asp:Label id="lblBillPartyTag" runat="server" Visible="False"/>
			<table cellpadding="2" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6">
						<UserControl:MenuWMTrx id=MenuWMTrx runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4">WEIGHBRIDGE TICKET DETAILS</td>
					<td colspan="2" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   </td>
				</tr>
				<tr>
					<td>Ticket No :*</td>
					<td><asp:label id="lblTicketNo" Text="" runat="server" />                       
						<asp:label id="lblDupMsg"  Text="Ticket No. already exist" Visible=false forecolor=red Runat="server"/>								
					</td>
					<td>&nbsp;</td>
					<td colspan="2">Period : </td>
					<td><asp:Label id="lblPeriod" runat="server"/></td>
				</tr>
				<tr>
					<td>Transaction Type :*</td>
					<td colspan="2"><asp:radiobuttonlist id=rblTransType TextAlign="Right" RepeatColumns="4" RepeatLayout="Flow" AutoPostBack=true OnSelectedIndexChanged=Trans_Type runat=server /></td>
					<td colspan="2">Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
				</tr>
				
				<tr>
					<td>Purchase Type :</td>
					<td><asp:DropDownList id="lstPurchType" width=50% runat="server" AutoPostBack=true OnSelectedIndexChanged=Purchase_Type size="1"/></td>
					<td>&nbsp;</td>
					<td colspan="2">Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/></td>
				</tr>
				
				<tr>
					<td>Origin :*</td>
					<td><asp:DropDownList id="lstOrigin" width=50% runat="server" size="1" />
					<asp:label id="lblOriginErr"  Text="Plese Select Origin" Visible=false forecolor=red Runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td colspan="2">Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
				</tr>   
				<tr>
					<td>Destination :*</td>
					<td><asp:DropDownList id="lstDestination" width=50% runat="server" AutoPostBack=true OnSelectedIndexChanged=Destination_Change size="1"/>
					<asp:label id="lblDestinationErr"  Text="Plese Select Destination" Visible=false forecolor=red Runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td colspan="2">Update By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
				</tr>
				
				<tr>
					<td>Product :*</td>
					<td><asp:DropDownList id="lstProduct" width=50% runat="server" size="1"/></td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id="lblCustomerTag" Runat="server" /> :* </td>
					<td><asp:DropDownList id="lstCustomer" width=100% runat="server" size="1"/>
					
						<asp:label id=lblCustomerErr text="Please Fill Required Field" Visible=False 
						 forecolor=red Runat="server" />
						 					
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign=top><asp:label id="lblCustDocRefNoTag" Runat="server" /> Doc Ref No : </td>
					<td><asp:TextBox id="txtCustDocRefNo" runat="server" width=50% maxlength="20"/></td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Delivery Order :</td>
					<td colspan="4" >
						<asp:DropDownList id="lstDeliveryOrder" width=50% runat="server" size="1"/>
						<asp:label id="lblDeliveryOrderErr"  Text="Plese Select Delivery Order" Visible=false forecolor=red Runat="server"/>					
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>PL3 No. :</td>
					<td><asp:TextBox id="txtPL3No" runat="server" width=50% maxlength="20"/></td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Transporter : </td>
					<td><asp:DropDownList id="lstTransporter" width=100% runat="server" AutoPostBack=true onSelectedIndexChanged=OnTrans_Change size="1"/>
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td>Employee Code :* </td>
					<td colspan="2" >
					<asp:DropDownList id="lstEmpCode" Width=87% runat=server />
					<input type=button value=" ... " id="FindEmp" onclick="javascript:findcode('frmMain','','','','','','','','lstEmpCode','','','','','','','','','');" CausesValidation=False runat=server />
					<asp:label id=lblEmpErr text="Please select one Employee Code" Visible=False forecolor=red Runat="server" />
					</td>
					
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td><asp:label id="lblVehicleTag" Runat="server" /> :</td>
					<td><asp:TextBox id="txtVehicle" runat="server" width=50% maxlength="8"/></td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Driver's Name :</td>
					<td><asp:TextBox id="txtDriverName" runat="server" width=100% maxlength="128"/></td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Driver's I/C No. :</td>
					<td><asp:TextBox id="txtDriverICNo" runat="server" width=50% maxlength="18"/></td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Planting Year : </td>
					<td><asp:TextBox id="txtPlantingYear" runat="server" width=100% maxlength="32"/>
						<asp:label id="lblErrPlantYr" forecolor=red text="Please Enter Planting Year." runat="server" />												
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=20%><asp:label id="lblBlockTag" Runat="server" /> : </td>
					<td width=35%><asp:TextBox id="txtBlock" runat="server" width=50% maxlength="8"/></td>
					<td width=5%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
				</tr>
				<tr>
					<td>Date In :* </td>
					<td><asp:TextBox id="txtDateIn" runat="server" width=50% maxlength="10" />
						<a href="javascript:PopCal('txtDateIn');">
						<asp:Image id="btnSelInDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:ImageButton id="btnCurrInDtTime" imageurl="../../images/butt_curr_dttime.gif" onclick="btnCurrInDtTime_Click" runat="server" CausesValidation="False" AlternateText="Set Current Date/Time"/>
						<asp:Label id=lblErrDateIn forecolor=red runat=server/>
						<asp:Label id=lblErrDateInMsg visible=false text="<br>Date Format should be in " runat=server/>						
						<asp:RequiredFieldValidator id="validateDateIn" runat="server" 
							ErrorMessage="Date In cannot be blank."
							display="dynamic" 
							ControlToValidate="txtDateIn"/>
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Time In :* </td>
					<td><asp:TextBox id="txtInHour" runat="server" EnableViewState=False  maxlength="2" size="3"/> :
						<asp:TextBox id="txtInMinute" runat="server" EnableViewState=False maxlength="2" size="3"/> 
						<asp:DropDownList id="lstInAMPM" width=18% runat="server" >
							<asp:listitem id="InAM" text="AM" value=1 runat="server" size="1" />
							<asp:listitem id="InPM" text="PM" value=2 runat="server" size="1" />							
						</asp:DropDownList>	HH:MM <BR>					
						<asp:CompareValidator id="ChcktxtInHour" runat="server" Display=Dynamic
							ControlToValidate="txtInHour" Text="The value must be a whole number." 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtInHourRange"
							ControlToValidate="txtInHour"
							MinimumValue="1"
							MaximumValue="12"
							Type="Integer"
							Text="Value of hour is from 1 to 12 !"
							runat="server" Display=Dynamic/>													
						<asp:CompareValidator id="ChcktxtInMinute" runat="server" Display=Dynamic
							ControlToValidate="txtInMinute" Text="The value must be a whole number." 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtInMinuteRange"
							ControlToValidate="txtInMinute"
							MinimumValue="0"
							MaximumValue="59"
							Type="Integer"
							Text="Value of minute is from 0 to 59 !"
							runat="server" Display=Dynamic/>						
						<asp:RequiredFieldValidator id="validateTimeInHr" runat="server" 
							ErrorMessage="Time In (hour) cannot be blank."						
							display="dynamic" 
							ControlToValidate="txtInHour"/>					
						<asp:RequiredFieldValidator id="validateTimeInMin" runat="server" 
							ErrorMessage="Time In (minute) cannot be blank."						
							display="dynamic" 
							ControlToValidate="txtInMinute"/>					
					<asp:Label id=lblErrTimeIn forecolor=red text="Time In canont be blank if Date In is not blank." visible=false runat=server/>					
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
					<td>First Weight (Kg) :</td>
					<td><asp:TextBox id="txtFirstWgt" OnKeyUp="javascript:calnetweight();" runat="server" width=50% maxlength="21"/><br>
						<asp:RegularExpressionValidator id="revFirstWgt" 
							ControlToValidate="txtFirstWgt"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>   
						<asp:Label id=lblErrFirstWgt forecolor=red text="First Weight cannot be blank if Date In/Time In is not blank." visible=false runat=server/>								                                     
						<!--<asp:Label id=lblErrFirstWgtNotZero forecolor=red text="First Weight cannot be less than zero." visible=false runat=server/>-->
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>										
				</tr>
				<tr>
					<td>Date Out :</td>
					<td><asp:TextBox id="txtDateOut" runat="server" width=50% maxlength="10" />
						<a href="javascript:PopCal('txtDateOut');">
						<asp:Image id="btnSelOutDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:ImageButton id="btnCurrOutDtTime" imageurl="../../images/butt_curr_dttime.gif" onclick="btnCurrOutDtTime_Click" runat="server" CausesValidation="False" AlternateText="Set Current Date/Time"/>
						<asp:Label id=lblErrDateOut forecolor=red runat=server/>
						<asp:Label id=lblErrDateOutMsg visible=false text="<br>Date Format should be in " runat=server/>												
						<asp:Label id=lblErrDateOutBlank forecolor=red text="Date Out canont be blank if Time Out is not blank." visible=false runat=server/>						
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Time Out : </td>
					<td><asp:TextBox id="txtOutHour" runat="server" EnableViewState=False  maxlength="2" size="3"/> :
						<asp:TextBox id="txtOutMinute" runat="server" EnableViewState=False maxlength="2" size="3"/>
						<asp:DropDownList id="lstOutAMPM" width=18% runat="server" >
							<asp:listitem id="OutAM" text="AM" value=1 runat="server" size="1" />
							<asp:listitem id="OutPM" text="PM" value=2 runat="server" size="1" />							
						</asp:DropDownList>	HH:MM <BR>				
						<asp:CompareValidator id="ChcktxtOutHour" runat="server" Display=Dynamic
							ControlToValidate="txtOutHour" Text="The value must be a whole number." 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtOutHourRange"
								ControlToValidate="txtOutHour"
								MinimumValue="1"
								MaximumValue="12"
								Type="Integer"
								Text="Value of hour is from 1 to 12 !"
								runat="server" Display=Dynamic/>																				
						<asp:CompareValidator id="ChcktxtOutMinute" runat="server" Display=Dynamic
							ControlToValidate="txtOutMinute" Text="The value must be a whole number." 
							Type="integer" Operator="DataTypeCheck"/>
						<asp:RangeValidator id="txtOutMinuteRange"
								ControlToValidate="txtOutMinute"
								MinimumValue="0"
								MaximumValue="59"
								Type="Integer"
								Text="Value of minute is from 0 to 59 !"
								runat="server" Display=Dynamic/>	
					<asp:Label id=lblErrTimeOut forecolor=red text="Time Out canont be blank if Date Out is not blank." visible=false runat=server/>													
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Second Weight (Kg) : </td>
					<td><asp:TextBox id="txtSecondWgt" OnKeyUp="javascript:calnetweight();" runat="server" width=50% maxlength="21"/><br>
						<asp:RegularExpressionValidator id="revSecondWgt" 
								ControlToValidate="txtSecondWgt"
								ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								Display="Dynamic"
								text = "Maximum length 15 digits and 5 decimal points."
								runat="server"/> 
						<asp:Label id=lblErrSecWgt forecolor=red text="This field cannot be blank if Date Out/Time Out is not blank." visible=false runat=server/>								                                                     								                                       
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Net Weight (Kg) : </td>
					<td><span id=nw></span><br>
						<asp:Label id=lblErrNetWgt forecolor=red text="Net Weight cannot be less than zero." visible=false runat=server/>								                                                     								                                       					
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Date Received :</td>
					<td><asp:TextBox id="txtDateRcv" runat="server" width="50%" maxlength="10"/>
						<a href="javascript:PopCal('txtDateRcv');">
						<asp:Image id="btnSelRcvDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a><br>
						<asp:Label id=lblErrDateRcv forecolor=red runat=server/>
						<asp:Label id=lblErrDateRcvMsg visible=false text="<br>Date Format should be in " runat=server/>																		
						<asp:Label id=lblErrDateRcvBlank forecolor=red text="Date Received cannot be blank if Customer First Weight/Second Weight is not blank." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>                    
				</tr>
				<tr>
					<td>Customer First Weight (Kg) :</td>
					<td><asp:TextBox id="txtCustFirstWgt" OnKeyUp="javascript:calcustnetweight();" runat="server" maxlength="21" width=50%/><br>
						<asp:RegularExpressionValidator id="revCustFirstWgt" 
							ControlToValidate="txtCustFirstWgt"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points."
							runat="server"/>   
						<asp:Label id=lblErrCustFirstWgt forecolor=red text="Customer First Weight cannot be blank if Date Received is not blank." visible=false runat=server/>								                                                     
						<asp:Label id=lblErrCustFirstWgtZero forecolor=red text="Customer First Weight must be greater than zero." visible=false runat=server/>								                                                     
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Customer Second Weight (Kg) :</td>
					<td><asp:TextBox id="txtCustSecondWgt" OnKeyUp="javascript:calcustnetweight();" runat="server" maxlength="21" width=50%/><br>
						<asp:RegularExpressionValidator id="revCustSecondWgt" 
							ControlToValidate="txtCustSecondWgt"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points."
							runat="server"/>  							
						<asp:Label id=lblErrCustSecWgt forecolor=red text="Customer Second Weight cannot be blank if Date Received is not blank." visible=false runat=server/>								                                                     
						<asp:Label id=lblErrCustSecWgtZero forecolor=red text="Customer Second Weight must be greater than zero." visible=false runat=server/>								                                                     
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Customer Net Weight (Kg) :</td>
					<td><span id=cnw></span><br>
						<asp:Label id=lblErrCustNetWgt forecolor=red text="Customer Net Weight cannot be less than zero." visible=false runat=server/>
					</td>
					<td>&nbsp;</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Remarks :</td>
					<td colspan=5><asp:textbox id="txtRemarks" runat="server" maxlength=256 width=100%/></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>					
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="btnDelete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete"/>
						<asp:ImageButton id="btnUnDelete" imageurl="../../images/butt_undelete.gif" onclick=btnUnDelete_Click runat=server AlternateText="Undelete" CausesValidation="False" visible=false/>
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
						<asp:ImageButton id="btnPrint" runat="server" CausesValidation="False" ImageUrl="../../images/butt_print.gif" AlternateText="Print" OnClick="btnPrint_Click"></asp:ImageButton>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						&nbsp;</td>
				</tr>
			</table>

        <br />
        </div>
        </td>
        </tr>
        </table>

		</form>
	</body>
</html>
