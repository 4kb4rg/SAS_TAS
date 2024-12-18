<%@ Page Language="vb" Inherits="PM_WasteWaterQuality_Det" Src="../../../include/PM_trx_WasteWaterQuality_Det.aspx.vb"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>WASTED WATER QUALITY TRANSACTION</title> 
		<PREFERENCE:PREFHDL id="PrefHdl" runat="server"></PREFERENCE:PREFHDL>
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<asp:label id="lblErrMessage" runat="server" Text="Error while initiating component." visible="false"></asp:label><asp:label id="blnUpdate" runat="server" Visible="False"></asp:label>
			<table cellSpacing="0" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6"><USERCONTROL:MENUPDTRX id="MenuPDTrx" runat="server"></USERCONTROL:MENUPDTRX></td>
				</tr>
				<tr>
					<td class="mt-h" colSpan="3">WASTED WATER QUALITY DETAILS</td>
					<td align="right" colSpan="3"><asp:label id="lblTracker" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="6">
						<hr noShade SIZE="1">
					</td>
				</tr>
				<tr>
					<td width="20%" height="25">Transaction Date :*
					</td>
					<td width="20%"><asp:textbox id="txtdate" runat="server" maxlength="20" width="70%"></asp:textbox><A href="javascript:PopCal('txtdate');"><asp:image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"></asp:image></A><br>
						<asp:requiredfieldvalidator id="rfvDate" runat="server" display="dynamic" text="Field cannot be blank" ControlToValidate="txtdate"
							EnableViewState="False">Field cannot be blank</asp:requiredfieldvalidator><asp:label id="lblDate" Text="Date Entered should be in the format " Visible="false" Runat="server"
							forecolor="red"></asp:label><asp:label id="lblFmt" Visible="false" Runat="server" forecolor="red"></asp:label></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :
					</td>
					<td width="25%"><asp:label id="lblPeriod" runat="server"></asp:label></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Test Sample Code :*
					</td>
					<td><asp:dropdownlist id="ddlTestSampleCode" runat="server" maxlength="20" width="100%"></asp:dropdownlist><asp:requiredfieldvalidator id="rfvTestSampleCode" runat="server" display="dynamic" text="Please select a test sample code."
							ControlToValidate="ddlTestSampleCode" EnableViewState="False">Please select a test sample code.</asp:requiredfieldvalidator></td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:label id="lblStatus" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Pond No.&nbsp;:*
					</td>
					<td><asp:dropdownlist id="ddlPondNo" runat="server"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvPondNo" runat="server" ControlToValidate="ddlPondNo" ErrorMessage="Please select a Pond No."
							EnableViewState="False" Display="Dynamic"></asp:requiredfieldvalidator></td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:label id="lblCreateDate" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">PH :
					</td>
					<td><asp:textbox id="txtPH" runat="server"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revPH" runat="server" ControlToValidate="txtPH" ErrorMessage="Maximum length 3 digits and 2 decimal points."
							EnableViewState="False" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvPH" runat="server" display="dynamic" ControlToValidate="txtPH" ErrorMessage="The value is out of range."
							EnableViewState="False" EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0"></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>Date&nbsp;Update :</td>
					<td><asp:label id="lblLastUpdate" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">T.A :</td>
					<td><asp:textbox id="txtTA" runat="server"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revTA" runat="server" ControlToValidate="txtTA" ErrorMessage="Maximum length 3 digits and 2 decimal points."
							EnableViewState="False" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTA" runat="server" ControlToValidate="txtTA" ErrorMessage="The value is out of range."
							EnableViewState="False" Display="Dynamic" Type="Double" MaximumValue="100" MinimumValue="0"></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>Updated By&nbsp;:</td>
					<td><asp:label id="lblUpdateBy" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">VFA :</td>
					<td colSpan="5"><asp:textbox id="txtVFA" runat="server"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revVFA" runat="server" ControlToValidate="txtVFA" ErrorMessage="Maximum length 3 digits and 2 decimal points."
							EnableViewState="False" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvVFA" runat="server" ControlToValidate="txtVFA" ErrorMessage="The value is out of range."
							EnableViewState="False" Display="Dynamic" Type="Double" MaximumValue="100" MinimumValue="0"></asp:rangevalidator></td>
				</tr>
				<tr>
					<td height="25">VFA/TA :</td>
					<td colSpan="5"><asp:textbox id="txtVFATA" runat="server"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revVFATA" runat="server" ControlToValidate="txtVFATA" ErrorMessage="Maximum length 3 digits and 2 decimal points."
							EnableViewState="False" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvVFATA" runat="server" ControlToValidate="txtVFATA" ErrorMessage="The value is out of range."
							EnableViewState="False" Display="Dynamic" Type="Double" MaximumValue="100" MinimumValue="0"></asp:rangevalidator></td>
				</tr>
				<tr>
					<td colSpan="6" height="25"><asp:label id="lblDupMsg" Text="Production for selected date, test sample code and Pond No. already exist."
							Visible="false" Runat="server" forecolor="red">Production for selected date, test sample code and Pond No. already exist.</asp:label></td>
				</tr>
				<tr>
					<td colSpan="6" height="25">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:imagebutton id="Save" onclick="btnSave_Click" runat="server" AlternateText="Save" imageurl="../../images/butt_save.gif"></asp:imagebutton><asp:imagebutton id="Delete" onclick="btnDelete_Click" runat="server" Visible="False" AlternateText="Delete"
							imageurl="../../images/butt_delete.gif" CausesValidation="False"></asp:imagebutton><asp:imagebutton id="Back" onclick="btnBack_Click" runat="server" AlternateText="Back" imageurl="../../images/butt_back.gif"
							CausesValidation="False"></asp:imagebutton></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
