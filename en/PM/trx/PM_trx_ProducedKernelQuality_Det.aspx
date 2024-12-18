<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Page Language="vb" Inherits="PM_ProducedKernelQuality_Det" Src="../../../include/PM_trx_ProducedKernelQuality_Det.aspx.vb"%>
<HTML>
	<HEAD>
		<title>PRODUCED KERNEL QUALITY TRANSACTION</title> 
		<PREFERENCE:PREFHDL id="PrefHdl" runat="server"></PREFERENCE:PREFHDL>
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
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<asp:label id="lblErrMessage" runat="server" Text="Error while initiating component." visible="false"></asp:label><asp:label id="blnUpdate" runat="server" Visible="False"></asp:label>
			<table cellSpacing="0" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6"><USERCONTROL:MENUPDTRX id="MenuPDTrx" runat="server"></USERCONTROL:MENUPDTRX></td>
				</tr>
				<tr>
					<td class="mt-h" width="70%" colSpan="3">PRODUCED KERNEL QUALITY DETAILS</td>
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
					<td width="30%"><asp:textbox id="txtdate" runat="server" maxlength="20"></asp:textbox><A href="javascript:PopCal('txtdate');"><asp:image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"></asp:image></A><br>
						<asp:requiredfieldvalidator id="rfvDate" runat="server" display="dynamic" ControlToValidate="txtdate" EnableViewState="False"
							ErrorMessage="Field cannot be blank."></asp:requiredfieldvalidator><asp:label id="lblDate" Text="Date Entered should be in the format " Visible="false" Runat="server"
							forecolor="red"></asp:label><asp:label id="lblFmt" Visible="false" Runat="server" forecolor="red"></asp:label><asp:label id="lblDupMsg" Text="Transaction on this date already exist" Visible="false" Runat="server"
							forecolor="red"></asp:label></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period :
					</td>
					<td width="25%"><asp:label id="lblPeriod" runat="server"></asp:label></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="2" height="25">KERNEL SILO NO.1
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:label id="lblStatus" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% BK :
					</td>
					<td><asp:textbox id="txtSiloBK1" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloBK1" runat="server" ControlToValidate="txtSiloBK1" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloBK1" runat="server" display="dynamic" ControlToValidate="txtSiloBK1" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>Update By :
					</td>
					<td><asp:label id="lblUpdateBy" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist :
					</td>
					<td><asp:textbox id="txtSiloMoist1" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloMoist1" runat="server" ControlToValidate="txtSiloMoist1" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloMoist1" runat="server" display="dynamic" ControlToValidate="txtSiloMoist1"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:label id="lblCreateDate" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% L Shell :
					</td>
					<td><asp:textbox id="txtSiloLShell1" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloLShell1" runat="server" ControlToValidate="txtSiloLShell1" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloLShell1" runat="server" display="dynamic" ControlToValidate="txtSiloLShell1"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:label id="lblLastUpdate" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Shell /WN :
					</td>
					<td><asp:textbox id="txtSiloWN1" runat="server" maxlength="6" ></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloWN1" runat="server" ControlToValidate="txtSiloWN1" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloWN1" runat="server" display="dynamic" ControlToValidate="txtSiloWN1" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Shell /BN :
					</td>
					<td><asp:textbox id="txtSiloBN1" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloBN1" runat="server" ControlToValidate="txtSiloBN1" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloBN1" runat="server" display="dynamic" ControlToValidate="txtSiloBN1" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Total Dirt :
					</td>
					<td><asp:textbox id="txtSiloTotalDirt1" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloTotalDirt" runat="server" ControlToValidate="txtSiloTotalDirt1" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloTotalDirt1" runat="server" display="dynamic" ControlToValidate="txtSiloTotalDirt1"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="2" height="25">KERNEL SILO NO.2
					</td>
					<td>&nbsp;</td>
					<td colSpan="2">DIRT IN PRODUCTION KERNEL</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% BK :
					</td>
					<td><asp:textbox id="txtSiloBK2" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloBK2" runat="server" ControlToValidate="txtSiloBK2" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloBK2" runat="server" display="dynamic" ControlToValidate="txtSiloBK2" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">% BK :
					</td>
					<td><asp:textbox id="txtProdBK" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdBK" runat="server" ControlToValidate="txtProdBK" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdBK" runat="server" display="dynamic" ControlToValidate="txtProdBK" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist :
					</td>
					<td><asp:textbox id="txtSiloMoist2" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloMoist2" runat="server" ControlToValidate="txtSiloMoist2" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloMoist2" runat="server" display="dynamic" ControlToValidate="txtSiloMoist2"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">% Moist :
					</td>
					<td><asp:textbox id="txtProdMoist" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdMoist" runat="server" ControlToValidate="txtProdMoist" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdMoist" runat="server" display="dynamic" ControlToValidate="txtProdMoist"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% L Shell :
					</td>
					<td><asp:textbox id="txtSiloLShell2" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloLShell2" runat="server" ControlToValidate="txtSiloLShell2" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloLShell2" runat="server" display="dynamic" ControlToValidate="txtSiloLShell2"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">% L Shell :
					</td>
					<td><asp:textbox id="txtProdLShell" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdLShell" runat="server" ControlToValidate="txtProdLShell" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdLShell" runat="server" display="dynamic" ControlToValidate="txtProdLShell"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Shell /WN :
					</td>
					<td><asp:textbox id="txtSiloWN2" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloWN2" runat="server" ControlToValidate="txtSiloWN2" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloWN2" runat="server" display="dynamic" ControlToValidate="txtSiloWN2" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">% Shell /WN :
					</td>
					<td><asp:textbox id="txtProdWN" runat="server" maxlength="6" ></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdWN" runat="server" ControlToValidate="txtProdWN" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdWN" runat="server" display="dynamic" ControlToValidate="txtProdWN" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Shell /BN :
					</td>
					<td><asp:textbox id="txtSiloBN2" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloBN2" runat="server" ControlToValidate="txtSiloBN2" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloBN2" runat="server" display="dynamic" ControlToValidate="txtSiloBN2" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">% Shell /BN :
					</td>
					<td><asp:textbox id="txtProdBN" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdBN" runat="server" ControlToValidate="txtProdBN" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdBN" runat="server" display="dynamic" ControlToValidate="txtProdBN" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Total Dirt :
					</td>
					<td><asp:textbox id="txtSiloTotalDirt2" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloTotalDirt2" runat="server" ControlToValidate="txtSiloTotalDirt2" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloTotalDirt2" runat="server" display="dynamic" ControlToValidate="txtSiloTotalDirt2"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">% Total Dirt :
					</td>
					<td><asp:textbox id="txtProdTotalDirt" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdTotalDirt" runat="server" ControlToValidate="txtProdTotalDirt" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdTotalDirt" runat="server" display="dynamic" ControlToValidate="txtProdTotalDirt"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25" colspan="2">KERNEL SILO NO.3
					</td>
					<td>&nbsp;</td>
					<td height="25">
						%&nbsp;FFA :
					</td>
					<td><asp:textbox id="txtProdFFA" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdFFA" runat="server" ControlToValidate="txtProdFFA" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdFFA" runat="server" display="dynamic" ControlToValidate="txtProdFFA" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% BK :
					</td>
					<td><asp:textbox id="txtSiloBK3" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloBK3" runat="server" ControlToValidate="txtSiloBK3" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloBK3" runat="server" display="dynamic" ControlToValidate="txtSiloBK3" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td height="25">
						%&nbsp;O/D :
					</td>
					<td><asp:textbox id="txtProdOD" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revProdOD" runat="server" ControlToValidate="txtProdOD" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvProdOD" runat="server" display="dynamic" ControlToValidate="txtProdOD" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist :
					</td>
					<td><asp:textbox id="txtSiloMoist3" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloMoist3" runat="server" ControlToValidate="txtSiloMoist3" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloMoist3" runat="server" display="dynamic" ControlToValidate="txtSiloMoist3"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% L Shell :
					</td>
					<td><asp:textbox id="txtSiloLShell3" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloLShell3" runat="server" ControlToValidate="txtSiloLShell3" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloLShell3" runat="server" display="dynamic" ControlToValidate="txtSiloLShell3"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Shell /WN :
					</td>
					<td><asp:textbox id="txtSiloWN3" runat="server" maxlength="6" ></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloWN3" runat="server" ControlToValidate="txtSiloWN3" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloWN3" runat="server" display="dynamic" ControlToValidate="txtSiloWN3" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Shell /BN :
					</td>
					<td><asp:textbox id="txtSiloBN3" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloBN3" runat="server" ControlToValidate="txtSiloBN3" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloBN3" runat="server" display="dynamic" ControlToValidate="txtSiloBN3" EnableClientScript="True"
							Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Total Dirt :
					</td>
					<td><asp:textbox id="txtSiloTotalDirt3" runat="server" maxlength="6"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revSiloTotalDirt3" runat="server" ControlToValidate="txtSiloTotalDirt3" Display="Dynamic"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:regularexpressionvalidator><asp:rangevalidator id="rvSiloTotalDirt3" runat="server" display="dynamic" ControlToValidate="txtSiloTotalDirt3"
							EnableClientScript="True" Type="double" MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:rangevalidator></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
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
