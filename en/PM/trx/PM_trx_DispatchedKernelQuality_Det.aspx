<%@ Page Language="vb" Src="../../../include/PM_trx_DispatchedKernelQuality_Det.aspx.vb" Inherits="PM_DispatchedKernelQuality_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>DISPATCHED KERNEL QUALITY TRANSACTION</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function calAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtUncrackedNut.value);				
				var b = parseFloat(doc.txtHalfCrackedNut.value);
				var c = parseFloat(doc.txtFreeShell.value);
				var d = parseFloat(doc.txtStone.value);
				doc.lblTotalDirt.value = a + b + c + d;
				if (doc.lblTotalDirt.value == 'NaN')
					doc.lblTotalDirt.value = '';
				else
					doc.lblTotalDirt.value = round(doc.lblTotalDirt.value, 2);
			}

		</script>		

	</head>
	
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<table cellpadding="2" cellspacing=0 width="100%" border="0">
 				<tr>
					<td colspan="6"><UserControl:MenuPDTrx id=MenuPDTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="70%">DISPATCHED KERNEL QUALITY DETAILS</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="20%" height=25>Transaction Date :* </td>
					<td width="30%">
						<asp:TextBox id="txtdate" runat="server" width=70% maxlength="20"/>                       
						<a href="javascript:PopCal('txtdate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:RequiredFieldValidator 
							id="rfvDate" 
							runat="server"  
							ControlToValidate="txtdate" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
						<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
						<asp:label id="lblDupMsg"  Text="Transaction on this date already exist" Visible=false forecolor=red Runat="server"/>								
					</td>
					<td width="5%">&nbsp;</td>					
					<td width="15%">Period : </td>
					<td width="25%"><asp:Label id="lblPeriod" runat="server"/></td>
					<td width="5%">&nbsp;</td>					
				</tr>									
				<tr>
					<td height=25>Broken Kernel (%) :</td>
					<td><asp:TextBox id="txtBrokenKernel" runat="server" width=70% maxlength="6"/>                       						
						<asp:RegularExpressionValidator id="revtxtBrokenKernel" 
							ControlToValidate="txtBrokenKernel"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvBrokenKernel"
							ControlToValidate="txtBrokenKernel"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Uncracked Nut (%) :*</td>
					<td><asp:TextBox id="txtUncrackedNut" OnKeyUp="javascript:calAmount();" runat="server" width=70% maxlength="6"/>                       						
						<asp:RegularExpressionValidator id="revUncrackedNut" 
							ControlToValidate="txtUncrackedNut"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvUncrackedNut"
							ControlToValidate="txtUncrackedNut"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvUncrackedNut" 
							runat="server"  
							ControlToValidate="txtUncrackedNut" 
							text = "Field cannot be blank"
							display="dynamic"/>						
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Half-Cracked Nut (%) :* </td>
					<td><asp:TextBox id="txtHalfCrackedNut" OnKeyUp="javascript:calAmount();" runat="server" width=70% maxlength="6"/>	
						<asp:RegularExpressionValidator id="revHalfCrackedNut" 
							ControlToValidate="txtHalfCrackedNut"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvHalfCrackedNut"
							ControlToValidate="txtHalfCrackedNut"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvHalfcrackedNut" 
							runat="server"  
							ControlToValidate="txtHalfCrackedNut" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Free Shell (%) :* </td>
					<td><asp:TextBox id="txtFreeShell" OnKeyUp="javascript:calAmount();" runat="server" width=70% maxlength="6"/>                       						
						<asp:RegularExpressionValidator id="revFreeShell" 
							ControlToValidate="txtFreeShell"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvFreeShell"
							ControlToValidate="txtFreeShell"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvFreeShell" 
							runat="server"  
							ControlToValidate="txtFreeShell" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Stone (%) :* </td>
					<td><asp:TextBox id="txtStone" OnKeyUp="javascript:calAmount();" runat="server" width=70% maxlength="6" editable=false/>                       						
						<asp:RegularExpressionValidator id="revStone" 
							ControlToValidate="txtStone"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvStone"
							ControlToValidate="txtStone"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvStone" 
							runat="server"  
							ControlToValidate="txtStone" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Total Dirt (%) :* </td>
					<td><asp:TextBox id="lblTotalDirt" runat="server" width=70% readonly maxlength="6"/>
						<asp:RegularExpressionValidator id="revTotalDirt" 
							ControlToValidate="lblTotalDirt"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvTotalDirt"
							ControlToValidate="lblTotalDirt"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvTotalDirt" 
							runat="server"  
							ControlToValidate="lblTotalDirt" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>											
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Moist (%) :* </td>
					<td><asp:TextBox id="txtMoist" runat="server" width=70% maxlength="6"/>                       						
						<asp:RegularExpressionValidator id="revMoist" 
							ControlToValidate="txtMoist"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvMoist"
							ControlToValidate="txtMoist"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvMoist" 
							runat="server"  
							ControlToValidate="txtMoist" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Oil Content (%) :* </td>
					<td><asp:TextBox id="txtOilContent" runat="server" width=70% maxlength="6"/>	
						<asp:RegularExpressionValidator id="revOilContent" 
							ControlToValidate="txtOilContent"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvOilContent"
							ControlToValidate="txtOilContent"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvOilContect" 
							runat="server"  
							ControlToValidate="txtOilContent" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>FFA (%) :* </td>
					<td><asp:TextBox id="txtFFA" runat="server" width=70% maxlength="6"/>                       						
						<asp:RegularExpressionValidator id="revFFA" 
							ControlToValidate="txtFFA"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvFFA"
							ControlToValidate="txtFFA"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						<asp:RequiredFieldValidator 
							id="rfvFFA" 
							runat="server"  
							ControlToValidate="txtFFA" 
							text = "Field cannot be blank"
							display="dynamic"/>	
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>									
				<tr>
					<td colspan="6" height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" Visible=False CausesValidation=False />
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
