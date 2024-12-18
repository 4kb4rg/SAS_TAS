<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Page Language="vb" Inherits="PM_KernelLoss_Det" Src="../../../include/PM_trx_KernelLoss_Det.aspx.vb"%>
<HTML>
	<HEAD>
		<title>KERNEL LOSSES TRANSACTION</title> 
		<Preference:PrefHdl id="PrefHdl" runat="server" />
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
					<td class="mt-h" colspan="3" width="70%">KERNEL LOSSES DETAILS</td>
					<td colspan="3" align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
				</tr>
				<tr>
					<td width="20%" height="25">Transaction Date :*
					</td>
					<td width="20%">
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
					<td width="20%">Period :
					</td>
					<td width="20%"><asp:Label id="lblPeriod" runat="server" /></td>
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
					<td height=25>&nbsp;</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>	
					<td>Updated By :
					</td>
					<td><asp:Label id="lblUpdateBy" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>	
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>	
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" style="height: 25px;font-weight: bolder; font-size: medium; font-style: italic; text-align: center;">LTDS</td>
				</tr>
				
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">LTDS 1
					</td>
					<td>
					</td>
				</tr>
				
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrLTDS1" ControlToValidate="txtSampelGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrLTDS1" ControlToValidate="txtSampelGrLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsLTDS1" ControlToValidate="txtSampelPsLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsLTDS1" ControlToValidate="txtSampelPsLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrLTDS1" ControlToValidate="txtNutUtuhGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrLTDS1" ControlToValidate="txtNutUtuhGrLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsLTDS1" ControlToValidate="txtNutUtuhGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsLTDS1" ControlToValidate="txtNutUtuhGrLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrLTDS1" ControlToValidate="txtNutPecahGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrLTDS1" ControlToValidate="txtNutPecahGrLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsLTDS1" ControlToValidate="txtNutPecahPsLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsLTDS1" ControlToValidate="txtNutPecahPsLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrLTDS1" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrLTDS1" ControlToValidate="txtKernelBulatGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrLTDS1" ControlToValidate="txtKernelBulatGrLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsLTDS1" ControlToValidate="txtKernelBulatPsLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsLTDS1" ControlToValidate="txtKernelBulatPsLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrLTDS1" ControlToValidate="txtKernelPecahGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrLTDS1" ControlToValidate="txtKernelPecahGrLTDS1" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsLTDS1" ControlToValidate="txtKernelPecahPsLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsLTDS1" ControlToValidate="txtKernelPecahPsLTDS1" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kerugian Kernel (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelRugiGrLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiGrLTDS1" ControlToValidate="txtKernelRugiGrLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryKernelRugiGrLTDS1" ControlToValidate="txtKernelRugiGrLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kerugian Kernel (%) :
					</td>
					<td><asp:TextBox id="txtKernelRugiPsLTDS1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiPsLTDS1" ControlToValidate="txtKernelRugiPsLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelRugiPsLTDS1" ControlToValidate="txtKernelRugiPsLTDS1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">LTDS 2
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrLTDS2" ControlToValidate="txtSampelGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrLTDS2" ControlToValidate="txtSampelGrLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsLTDS2" ControlToValidate="txtSampelPsLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsLTDS2" ControlToValidate="txtSampelPsLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrLTDS2" ControlToValidate="txtNutUtuhGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrLTDS2" ControlToValidate="txtNutUtuhGrLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsLTDS2" ControlToValidate="txtNutUtuhGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsLTDS2" ControlToValidate="txtNutUtuhGrLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrLTDS2" ControlToValidate="txtNutPecahGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrLTDS2" ControlToValidate="txtNutPecahGrLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsLTDS2" ControlToValidate="txtNutPecahPsLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsLTDS2" ControlToValidate="txtNutPecahPsLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrLTDS2" runat="server" maxlength="6" /><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrLTDS2" ControlToValidate="txtKernelBulatGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrLTDS2" ControlToValidate="txtKernelBulatGrLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsLTDS2" ControlToValidate="txtKernelBulatPsLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsLTDS2" ControlToValidate="txtKernelBulatPsLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrLTDS2" ControlToValidate="txtKernelPecahGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrLTDS2" ControlToValidate="txtKernelPecahGrLTDS2" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsLTDS2" ControlToValidate="txtKernelPecahPsLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsLTDS2" ControlToValidate="txtKernelPecahPsLTDS2" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kerugian Kernel (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelRugiGrLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiGrLTDS2" ControlToValidate="txtKernelRugiGrLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryKernelRugiGrLTDS2" ControlToValidate="txtKernelRugiGrLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kerugian Kernel (%) :
					</td>
					<td><asp:TextBox id="txtKernelRugiPsLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiPsLTDS2" ControlToValidate="txtKernelRugiPsLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelRugiPsLTDS2" ControlToValidate="txtKernelRugiPsLTDS2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" style="height: 25px;font-weight: bolder; font-size: medium; font-style: italic; text-align: center;">CLAYBATH</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">CLAYBATH
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrCB" ControlToValidate="txtSampelGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrCB" ControlToValidate="txtSampelGrCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsCB" ControlToValidate="txtSampelPsCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsCB" ControlToValidate="txtSampelPsCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrCB" ControlToValidate="txtNutUtuhGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrCB" ControlToValidate="txtNutUtuhGrCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsCB" ControlToValidate="txtNutUtuhGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsCB" ControlToValidate="txtNutUtuhGrCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrCB" ControlToValidate="txtNutPecahGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrCB" ControlToValidate="txtNutPecahGrCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsCB" ControlToValidate="txtNutPecahPsCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsCB" ControlToValidate="txtNutPecahPsCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrCB" ControlToValidate="txtKernelBulatGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrCB" ControlToValidate="txtKernelBulatGrCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsCB" ControlToValidate="txtKernelBulatPsCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsCB" ControlToValidate="txtKernelBulatPsCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrCB" ControlToValidate="txtKernelPecahGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrCB" ControlToValidate="txtKernelPecahGrCB" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsCB" ControlToValidate="txtKernelPecahPsCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsCB" ControlToValidate="txtKernelPecahPsCB" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kerugian Kernel (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelRugiGrCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiGrCB" ControlToValidate="txtKernelRugiGrCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryKernelRugiGrCB" ControlToValidate="txtKernelRugiGrCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kerugian Kernel (%) :
					</td>
					<td><asp:TextBox id="txtKernelRugiPsCB" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiPsCB" ControlToValidate="txtKernelRugiPsCB" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelRugiPsCB" ControlToValidate="txtKernelRugiPsCB" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" style="height: 25px;font-weight: bolder; font-size: medium; font-style: italic; text-align: center;">PRESSCAKE</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">PRESSCAKE NO. 1
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrPC1" ControlToValidate="txtSampelGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrPC1" ControlToValidate="txtSampelGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsPC1" ControlToValidate="txtSampelPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsPC1" ControlToValidate="txtSampelPsPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrPC1" ControlToValidate="txtNutUtuhGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrPC1" ControlToValidate="txtNutUtuhGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsPC1" ControlToValidate="txtNutUtuhGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsPC1" ControlToValidate="txtNutUtuhGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrPC1" ControlToValidate="txtNutPecahGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrPC1" ControlToValidate="txtNutPecahGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsPC1" ControlToValidate="txtNutPecahPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsPC1" ControlToValidate="txtNutPecahPsPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrPC1" ControlToValidate="txtKernelBulatGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrPC1" ControlToValidate="txtKernelBulatGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsPC1" ControlToValidate="txtKernelBulatPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsPC1" ControlToValidate="txtKernelBulatPsPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrPC1" ControlToValidate="txtKernelPecahGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrPC1" ControlToValidate="txtKernelPecahGrPC1" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsPC1" ControlToValidate="txtKernelPecahPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsPC1" ControlToValidate="txtKernelPecahPsPC1" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrPC1" ControlToValidate="txtCangkangGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrPC1" ControlToValidate="txtCangkangGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsPC1" ControlToValidate="txtCangkangPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsPC1" ControlToValidate="txtCangkangPsPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Nut Pecah/Total Nut (Gr) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalGrPC1" ControlToValidate="txtPecahPerTotalGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalGrPC1" ControlToValidate="txtPecahPerTotalGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Nut Pecah/Total Nut (%) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalPsPC1" ControlToValidate="txtPecahPerTotalPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalPsPC1" ControlToValidate="txtPecahPerTotalPsPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Total Nut/Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelGrPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelGrPC1" ControlToValidate="txtTotalPerSampelGrPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelGrPC1" ControlToValidate="txtTotalPerSampelGrPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Total Nut/Sampel (%) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelPsPC1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelPsPC1" ControlToValidate="txtTotalPerSampelPsPC1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelPsPC1" ControlToValidate="txtTotalPerSampelPsPC1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">PRESSCAKE NO. 2
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrPC2" ControlToValidate="txtSampelGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrPC2" ControlToValidate="txtSampelGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsPC2" ControlToValidate="txtSampelPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsPC2" ControlToValidate="txtSampelPsPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrPC2" ControlToValidate="txtNutUtuhGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrPC2" ControlToValidate="txtNutUtuhGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsPC2" ControlToValidate="txtNutUtuhGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsPC2" ControlToValidate="txtNutUtuhGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrPC2" ControlToValidate="txtNutPecahGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrPC2" ControlToValidate="txtNutPecahGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsPC2" ControlToValidate="txtNutPecahPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsPC2" ControlToValidate="txtNutPecahPsPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrPC2" ControlToValidate="txtKernelBulatGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrPC2" ControlToValidate="txtKernelBulatGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsPC2" ControlToValidate="txtKernelBulatPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsPC2" ControlToValidate="txtKernelBulatPsPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrPC2" ControlToValidate="txtKernelPecahGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrPC2" ControlToValidate="txtKernelPecahGrPC2" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsPC2" ControlToValidate="txtKernelPecahPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsPC2" ControlToValidate="txtKernelPecahPsPC2" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrPC2" ControlToValidate="txtCangkangGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrPC2" ControlToValidate="txtCangkangGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsPC2" ControlToValidate="txtCangkangPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsPC2" ControlToValidate="txtCangkangPsPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Nut Pecah/Total Nut (Gr) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalGrPC2" ControlToValidate="txtPecahPerTotalGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalGrPC2" ControlToValidate="txtPecahPerTotalGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Nut Pecah/Total Nut (%) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalPsPC2" ControlToValidate="txtPecahPerTotalPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalPsPC2" ControlToValidate="txtPecahPerTotalPsPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Total Nut/Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelGrPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelGrPC2" ControlToValidate="txtTotalPerSampelGrPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelGrPC2" ControlToValidate="txtTotalPerSampelGrPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Total Nut/Sampel (%) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelPsPC2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelPsPC2" ControlToValidate="txtTotalPerSampelPsPC2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelPsPC2" ControlToValidate="txtTotalPerSampelPsPC2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">PRESSCAKE NO. 3
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrPC3" ControlToValidate="txtSampelGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrPC3" ControlToValidate="txtSampelGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsPC3" ControlToValidate="txtSampelPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsPC3" ControlToValidate="txtSampelPsPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrPC3" ControlToValidate="txtNutUtuhGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrPC3" ControlToValidate="txtNutUtuhGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsPC3" ControlToValidate="txtNutUtuhGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsPC3" ControlToValidate="txtNutUtuhGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrPC3" ControlToValidate="txtNutPecahGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrPC3" ControlToValidate="txtNutPecahGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsPC3" ControlToValidate="txtNutPecahPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsPC3" ControlToValidate="txtNutPecahPsPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrPC3" ControlToValidate="txtKernelBulatGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrPC3" ControlToValidate="txtKernelBulatGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsPC3" ControlToValidate="txtKernelBulatPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsPC3" ControlToValidate="txtKernelBulatPsPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrPC3" ControlToValidate="txtKernelPecahGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrPC3" ControlToValidate="txtKernelPecahGrPC3" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsPC3" ControlToValidate="txtKernelPecahPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsPC3" ControlToValidate="txtKernelPecahPsPC3" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrPC3" ControlToValidate="txtCangkangGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrPC3" ControlToValidate="txtCangkangGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsPC3" ControlToValidate="txtCangkangPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsPC3" ControlToValidate="txtCangkangPsPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Nut Pecah/Total Nut (Gr) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalGrPC3" ControlToValidate="txtPecahPerTotalGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalGrPC3" ControlToValidate="txtPecahPerTotalGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Nut Pecah/Total Nut (%) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalPsPC3" ControlToValidate="txtPecahPerTotalPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalPsPC3" ControlToValidate="txtPecahPerTotalPsPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Total Nut/Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelGrPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelGrPC3" ControlToValidate="txtTotalPerSampelGrPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelGrPC3" ControlToValidate="txtTotalPerSampelGrPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Total Nut/Sampel (%) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelPsPC3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelPsPC3" ControlToValidate="txtTotalPerSampelPsPC3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelPsPC3" ControlToValidate="txtTotalPerSampelPsPC3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">PRESSCAKE NO. 4
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrPC4" ControlToValidate="txtSampelGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrPC4" ControlToValidate="txtSampelGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsPC4" ControlToValidate="txtSampelPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsPC4" ControlToValidate="txtSampelPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrPC4" ControlToValidate="txtNutUtuhGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrPC4" ControlToValidate="txtNutUtuhGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsPC4" ControlToValidate="txtNutUtuhGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsPC4" ControlToValidate="txtNutUtuhGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrPC4" ControlToValidate="txtNutPecahGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrPC4" ControlToValidate="txtNutPecahGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsPC4" ControlToValidate="txtNutPecahPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsPC4" ControlToValidate="txtNutPecahPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrPC4" ControlToValidate="txtKernelBulatGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrPC4" ControlToValidate="txtKernelBulatGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsPC4" ControlToValidate="txtKernelBulatPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsPC4" ControlToValidate="txtKernelBulatPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrPC4" ControlToValidate="txtKernelPecahGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrPC4" ControlToValidate="txtKernelPecahGrPC4" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsPC4" ControlToValidate="txtKernelPecahPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsPC4" ControlToValidate="txtKernelPecahPsPC4" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kerugian Kernel (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelRugiGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiGrPC4" ControlToValidate="txtKernelRugiGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDryKernelRugiGrPC4" ControlToValidate="txtKernelRugiGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kerugian Kernel (%) :
					</td>
					<td><asp:TextBox id="txtKernelRugiPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelRugiPsPC4" ControlToValidate="txtKernelRugiPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelRugiPsPC4" ControlToValidate="txtKernelRugiPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrPC4" ControlToValidate="txtCangkangGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrPC4" ControlToValidate="txtCangkangGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsPC4" ControlToValidate="txtCangkangPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsPC4" ControlToValidate="txtCangkangPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Nut Pecah/Total Nut (Gr) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalGrPC4" ControlToValidate="txtPecahPerTotalGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalGrPC4" ControlToValidate="txtPecahPerTotalGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Nut Pecah/Total Nut (%) :
					</td>
					<td><asp:TextBox id="txtPecahPerTotalPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revPecahPerTotalPsPC4" ControlToValidate="txtPecahPerTotalPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvPecahPerTotalPsPC4" ControlToValidate="txtPecahPerTotalPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Total Nut/Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelGrPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelGrPC4" ControlToValidate="txtTotalPerSampelGrPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelGrPC4" ControlToValidate="txtTotalPerSampelGrPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Total Nut/Sampel (%) :
					</td>
					<td><asp:TextBox id="txtTotalPerSampelPsPC4" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revTotalPerSampelPsPC4" ControlToValidate="txtTotalPerSampelPsPC4" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvTotalPerSampelPsPC4" ControlToValidate="txtTotalPerSampelPsPC4" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" style="height: 25px;font-weight: bolder; font-size: medium; font-style: italic; text-align: center;">RIPPLE MILL</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">RIPPLE MILL NO. 1
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrRM1" ControlToValidate="txtSampelGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrRM1" ControlToValidate="txtSampelGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsRM1" ControlToValidate="txtSampelPsRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsRM1" ControlToValidate="txtSampelPsRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrRM1" ControlToValidate="txtNutUtuhGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrRM1" ControlToValidate="txtNutUtuhGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsRM1" ControlToValidate="txtNutUtuhGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsRM1" ControlToValidate="txtNutUtuhGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrRM1" ControlToValidate="txtNutPecahGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrRM1" ControlToValidate="txtNutPecahGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsRM1" ControlToValidate="txtNutPecahPsRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsRM1" ControlToValidate="txtNutPecahPsRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrRM1" ControlToValidate="txtKernelBulatGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrRM1" ControlToValidate="txtKernelBulatGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsRM1" ControlToValidate="txtKernelBulatPsRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsRM1" ControlToValidate="txtKernelBulatPsRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrRM1" ControlToValidate="txtKernelPecahGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrRM1" ControlToValidate="txtKernelPecahGrRM1" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsRM1" ControlToValidate="txtKernelPecahPsRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsRM1" ControlToValidate="txtKernelPecahPsRM1" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrRM1" ControlToValidate="txtCangkangGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrRM1" ControlToValidate="txtCangkangGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsRM1" ControlToValidate="txtCangkangPsRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsRM1" ControlToValidate="txtCangkangPsRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Effiesiensi (Gr) :
					</td>
					<td><asp:TextBox id="txtEffisiensiGrRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revEffisiensiGrRM1" ControlToValidate="txtEffisiensiGrRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvEffisiensiGrRM1" ControlToValidate="txtEffisiensiGrRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Effiesiensi (%) :
					</td>
					<td><asp:TextBox id="txtEffisiensiPsRM1" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revEffisiensiPsRM1" ControlToValidate="txtEffisiensiPsRM1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvEffisiensiPsRM1" ControlToValidate="txtEffisiensiPsRM1" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">RIPPLE MILL NO. 2
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrRM2" ControlToValidate="txtSampelGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrRM2" ControlToValidate="txtSampelGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsRM2" ControlToValidate="txtSampelPsRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsRM2" ControlToValidate="txtSampelPsRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrRM2" ControlToValidate="txtNutUtuhGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrRM2" ControlToValidate="txtNutUtuhGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsRM2" ControlToValidate="txtNutUtuhGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsRM2" ControlToValidate="txtNutUtuhGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrRM2" ControlToValidate="txtNutPecahGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrRM2" ControlToValidate="txtNutPecahGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsRM2" ControlToValidate="txtNutPecahPsRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsRM2" ControlToValidate="txtNutPecahPsRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrRM2" ControlToValidate="txtKernelBulatGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrRM2" ControlToValidate="txtKernelBulatGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsRM2" ControlToValidate="txtKernelBulatPsRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsRM2" ControlToValidate="txtKernelBulatPsRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrRM2" ControlToValidate="txtKernelPecahGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrRM2" ControlToValidate="txtKernelPecahGrRM2" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsRM2" ControlToValidate="txtKernelPecahPsRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsRM2" ControlToValidate="txtKernelPecahPsRM2" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrRM2" ControlToValidate="txtCangkangGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrRM2" ControlToValidate="txtCangkangGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsRM2" ControlToValidate="txtCangkangPsRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsRM2" ControlToValidate="txtCangkangPsRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Effiesiensi (Gr) :
					</td>
					<td><asp:TextBox id="txtEffisiensiGrRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revEffisiensiGrRM2" ControlToValidate="txtEffisiensiGrRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvEffisiensiGrRM2" ControlToValidate="txtEffisiensiGrRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Effiesiensi (%) :
					</td>
					<td><asp:TextBox id="txtEffisiensiPsRM2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revEffisiensiPsRM2" ControlToValidate="txtEffisiensiPsRM2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvEffisiensiPsRM2" ControlToValidate="txtEffisiensiPsRM2" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				<td>&nbsp;&nbsp;&nbsp;</td>
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 12px;">RIPPLE MILL NO. 3
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td height="25">Berat Sampel (Gr) :
					</td>
					<td><asp:TextBox id="txtSampelGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelGrRM3" ControlToValidate="txtSampelGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelGrRM3" ControlToValidate="txtSampelGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>					
					<td>&nbsp;</td>
					<td height="25">Berat Sampel (%) :
					</td>
					<td><asp:TextBox id="txtSampelPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revSampelPsRM3" ControlToValidate="txtSampelPsRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvSampelPsRM3" ControlToValidate="txtSampelPsRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Utuh (Gr):
					</td>
					<td><asp:TextBox id="txtNutUtuhGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhGrRM3" ControlToValidate="txtNutUtuhGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhGrRM3" ControlToValidate="txtNutUtuhGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Utuh (%):
					</td>
					<td><asp:TextBox id="txtNutUtuhPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutUtuhPsRM3" ControlToValidate="txtNutUtuhGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutUtuhPsRM3" ControlToValidate="txtNutUtuhGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Dari Nut Pecah (Gr):
					</td>
					<td><asp:TextBox id="txtNutPecahGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahGrRM3" ControlToValidate="txtNutPecahGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahGrRM3" ControlToValidate="txtNutPecahGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Dari Nut Pecah (%):
					</td>
					<td><asp:TextBox id="txtNutPecahPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNutPecahPsRM3" ControlToValidate="txtNutPecahPsRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvNutPecahPsRM3" ControlToValidate="txtNutPecahPsRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Bulat (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelBulatGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatGrRM3" ControlToValidate="txtKernelBulatGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatGrRM3" ControlToValidate="txtKernelBulatGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Bulat (%) :
					</td>
					<td><asp:TextBox id="txtKernelBulatPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelBulatPsRM3" ControlToValidate="txtKernelBulatPsRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelBulatPsRM3" ControlToValidate="txtKernelBulatPsRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Kernel Pecah (Gr) :
					</td>
					<td><asp:TextBox id="txtKernelPecahGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahGrRM3" ControlToValidate="txtKernelPecahGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahGrRM3" ControlToValidate="txtKernelPecahGrRM3" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Kernel Pecah (%) :
					</td>
					<td><asp:TextBox id="txtKernelPecahPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revKernelPecahPsRM3" ControlToValidate="txtKernelPecahPsRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvKernelPecahPsRM3" ControlToValidate="txtKernelPecahPsRM3" MinimumValue="0"
							MaximumValue="10000" Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Cangkang (Gr) :
					</td>
					<td><asp:TextBox id="txtCangkangGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangGrRM3" ControlToValidate="txtCangkangGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangGrRM3" ControlToValidate="txtCangkangGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Cangkang (%) :
					</td>
					<td><asp:TextBox id="txtCangkangPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revCangkangPsRM3" ControlToValidate="txtCangkangPsRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvCangkangPsRM3" ControlToValidate="txtCangkangPsRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				<tr>
					<td height="25">Effiesiensi (Gr) :
					</td>
					<td><asp:TextBox id="txtEffisiensiGrRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revEffisiensiGrRM3" ControlToValidate="txtEffisiensiGrRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvEffisiensiGrRM3" ControlToValidate="txtEffisiensiGrRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td height="25">Effiesiensi (%) :
					</td>
					<td><asp:TextBox id="txtEffisiensiPsRM3" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revEffisiensiPsRM3" ControlToValidate="txtEffisiensiPsRM3" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator id="rvEffisiensiPsRM3" ControlToValidate="txtEffisiensiPsRM3" MinimumValue="0" MaximumValue="10000"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
				</tr>
				
				
				
				<tr>
					<td colspan="6" style="height: 25px">&nbsp;</td>
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
