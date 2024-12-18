<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Page Language="vb" Inherits="PM_KernelQuality_Det" Src="../../../include/PM_trx_KernelQuality_Det.aspx.vb"%>
<HTML>
	<HEAD>
		<title>KERNEL QUALITY TRANSACTION</title> 
		<Preference:PrefHdl id="PrefHdl" runat="server" />
		<script language="javascript">
			function calDryKernel() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDKWholeNut.value);				
				var b = parseFloat(doc.txtDKBrokenNut.value);
				var c = parseFloat(doc.txtDKFreeShell.value);
				var d = parseFloat(doc.txtDKStone.value);				
				doc.txtDKTotalDirt.value = a + b + c + d;
				if (doc.txtDKTotalDirt.value == 'NaN')
					doc.txtDKTotalDirt.value = '';
				else
					doc.txtDKTotalDirt.value = round(doc.txtDKTotalDirt.value, 2);
			}
			
			function calWetKernel() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtWKHalfCrackedNut.value);				
				var b = parseFloat(doc.txtWKFreeShellNut.value);								
				doc.txtWKTotalDirt.value = a + b;				
				if (doc.txtWKTotalDirt.value == 'NaN')
					doc.txtWKTotalDirt.value = '';
				else
					doc.txtWKTotalDirt.value = round(doc.txtWKTotalDirt.value, 2);
			}
			
			function calWetShell() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtWSWholeNut.value);				
				var b = parseFloat(doc.txtWSBrokenNut.value);				
				var c = parseFloat(doc.txtWSBrokenKernel.value);								
				doc.txtWSKernelLoss.value = a + b + c;
				if (doc.txtWSKernelLoss.value == 'NaN')
					doc.txtWSKernelLoss.value = '';
				else
					doc.txtWSKernelLoss.value = round(doc.txtWSKernelLoss.value, 2);
			}			
		</script>
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:Label id="blnUpdate" runat="server" Visible="False" />
			<table cellpadding="2" cellspacing="0" width="100%" border="0">
				<tr>
					<td colspan="6"><UserControl:MenuPDTrx id="MenuPDTrx" runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="70%">KERNEL QUALITY DETAILS</td>
					<td colspan="3" align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
				</tr>
				<tr>
					<td width="25%" height="25">Transaction Date :*
					</td>
					<td width="25%">
						<asp:TextBox id="txtdate" runat="server" width="70%" maxlength="20" />
						<a href="javascript:PopCal('txtdate');">
							<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif" /></a><br>
						<asp:RequiredFieldValidator id="rfvDate" runat="server" ControlToValidate="txtdate" display="dynamic" EnableViewState="False"
							ErrorMessage="Field cannot be blank." />
						<asp:label id="lblDate" Text="Date Entered should be in the format " forecolor="red" Visible="false"
							Runat="server" />
						<asp:label id="lblFmt" forecolor="red" Visible="false" Runat="server" />
						<asp:label id="lblDupMsg" Text="Transaction on this date already exist." Visible="false" forecolor="red"
							Runat="server" />
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :
					</td>
					<td width="25%"><asp:Label id="lblPeriod" runat="server" /></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Processing Line No :*
					</td>
					<td><asp:DropDownList id="ddlProcessingLnNo" runat="server" maxlength="6" Width="100%" /><br>
						<asp:RequiredFieldValidator id="rfvProcessingLnNo" runat="server" ControlToValidate="ddlProcessingLnNo" display="dynamic"
							EnableViewState="False" ErrorMessage="Please select a Processing Line No." />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">DRY SEPARATOR
					</td>
					<td>
					</td>
					<td>&nbsp;</td>
					<td>Updated By :
					</td>
					<td><asp:Label id="lblUpdateBy" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">
						KERNEL DIRT
					</td>
					<td>
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% BK :
					</td>
					<td><asp:TextBox id="txtDrySepBK" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revDrySepBK" ControlToValidate="txtDrySepBK" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDrySepBK" ControlToValidate="txtDrySepBK" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">%&nbsp;L Shell&nbsp;:
					</td>
					<td colspan="5"><asp:TextBox id="txtDrySepLShell" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revDryBathLShell" ControlToValidate="txtDrySepLShell" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryBathLShell" ControlToValidate="txtDrySepLShell" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">%&nbsp;Shell /WN :
					</td>
					<td colspan="5"><asp:TextBox id="txtDrySepWN" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revDryBathWN" ControlToValidate="txtDrySepWN" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryBathWN" ControlToValidate="txtDrySepWN" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">%&nbsp;Shell /BN :
					</td>
					<td colspan="5"><asp:TextBox id="txtDrySepBN" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revDryBathBN" ControlToValidate="txtDrySepBN" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryBathBN" ControlToValidate="txtDrySepBN" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">%&nbsp;Total Dirt :
					</td>
					<td colspan="5"><asp:TextBox id="txtDrySepTotalDirt" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revDryBathTotalDirt" ControlToValidate="txtDrySepTotalDirt" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryBathTotalDirt" ControlToValidate="txtDrySepTotalDirt" MinimumValue="0"
							MaximumValue="100" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">FLOW RATE :
					</td>
					<td colspan="5"><asp:TextBox id="txtDrySepFlowRate" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revDryBathFlowRate" ControlToValidate="txtDrySepFlowRate" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryBathFlowRate" ControlToValidate="txtDrySepFlowRate" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">
						CLAY BATH
					</td>
					<td colspan="5">
					</td>
				</tr>
				<tr>
					<td height="25">
						KERNEL DIRT
					</td>
					<td colspan="5">
					</td>
				</tr>
				<tr>
					<td height="25">% BK :
					</td>
					<td colspan="5"><asp:TextBox id="txtClayBathBK" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revClayBathBK" ControlToValidate="txtClayBathBK" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvClayBathBK" ControlToValidate="txtClayBathBK" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">% L Shell :
					</td>
					<td colspan="5"><asp:TextBox id="txtClayBathLShell" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revClayBathLShell" ControlToValidate="txtClayBathLShell" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvClayBathLShell" ControlToValidate="txtClayBathLShell" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">% Shell /WN :
					</td>
					<td colspan="5"><asp:TextBox id="txtClayBathWN" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revClayBathWN" ControlToValidate="txtClayBathWN" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvClayBathWN" ControlToValidate="txtClayBathWN" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">% Shell /BN :
					</td>
					<td colspan="5"><asp:TextBox id="txtClayBathBN" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revClayBathBN" ControlToValidate="txtClayBathBN" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvClayBathBN" ControlToValidate="txtClayBathBN" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">% Total Dirt :
					</td>
					<td colspan="5"><asp:TextBox id="txtClayBathTotalDirt" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revClayBathTotalDirt" ControlToValidate="txtClayBathTotalDirt" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvClayBathTotalDirt" ControlToValidate="txtClayBathTotalDirt" MinimumValue="0"
							MaximumValue="100" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">FLOW RATE :
					</td>
					<td colspan="5"><asp:TextBox id="txtClayBathFlowRate" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revClayBathFlowRate" ControlToValidate="txtClayBathFlowRate" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvClayBathFlowRate" ControlToValidate="txtClayBathFlowRate" MinimumValue="0"
							MaximumValue="100" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25" colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server"	AlternateText="Save"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" Visible="False" CausesValidation="False"/>
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server"	AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
