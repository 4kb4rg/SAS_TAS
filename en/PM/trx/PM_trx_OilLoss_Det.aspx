<%@ Page Language="vb" Inherits="PM_OilLoss_Det" codefile="../../../include/PM_trx_OilLoss_Det.aspx.vb"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>OIL LOSSES TRANSACTION</title> 
		<%--<Preference:PrefHdl id="PrefHdl" runat="server" />--%>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:Label id="blnUpdate" runat="server" Visible="False" />
    <div class="kontenlist">
            <table cellpadding="2" cellspacing="0" width="100%" border="0" class="font9Tahoma">
	            <tr>
		            <td colspan="6"><UserControl:MenuPDTrx id="MenuPDTrx" runat="server" /></td>
	            </tr>
	            <tr>
		            <td class="mt-h" colspan="3">OIL LOSSES DETAILS</td>
		            <td colspan="3" align="right"><asp:label id="lblTracker" runat="server" /></td>
	            </tr>
	            <tr>
		            <td colspan="6"><hr size="1" noshade>
		            </td>
	            </tr>
	            <tr>
		            <td width="20%" height="25">Transaction Date :*
		            </td>
		            <td width="30%">
			            <asp:TextBox id="txtdate" runat="server" width="70%" maxlength="20" />
			            <a href="javascript:PopCal('txtdate');">
				            <asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif" /></a><br>
			            <asp:RequiredFieldValidator id="rfvDate" runat="server" ControlToValidate="txtdate" text="Field cannot be blank"
				            display="dynamic" />
			            <asp:label id="lblDate" Text="Date Entered should be in the format " forecolor="red" Visible="false"
				            Runat="server" />
			            <asp:label id="lblFmt" forecolor="red" Visible="false" Runat="server" />
			            <asp:label id="lblDupMsg" Text="Transaction on this date already exist" Visible="false" forecolor="red"
				            Runat="server" />
		            </td>
		            <td width="5%">&nbsp;</td>
					<td width="20%">Period :
					</td>
					<td width="20%"><asp:Label id="lblPeriod" runat="server" /></td>
					<td width="5%">&nbsp;</td>
	            </tr>
	            <tr>
		            <td height=25>&nbsp;</td>
				    <td>&nbsp;</td>
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
				</tr>
			    <tr>
			        <td height="25" style="font-weight: bolder; font-size: 11px;">Oil Losses</td>
			        <td>&nbsp;</td>
			        <td>&nbsp;</td>
			        <td  height="25" style="font-weight: bolder; font-size: 11px;">Kernel Losses</td>			       
			        <td>&nbsp;</td>
			    </tr>
			    <tr>
			        <td>% Draft Akhir</td>
			        <td><asp:TextBox id="txtOilDraftAkhir" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revMinStdWetOilRebus1" ControlToValidate="txtOilDraftAkhir" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvMinStdWetOilRebus1" ControlToValidate="txtOilDraftAkhir" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>% Fibre Cyclone</td>
			        <td><asp:TextBox id="txtPKFibre" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revMinStdWetOilRebus2" ControlToValidate="txtPKFibre" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvMinStdWetOilRebus2" ControlToValidate="txtPKFibre" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
			    </tr>
			     <tr>
			        <td>% Max. Jangkos Aftar EBP</td>
			        <td><asp:TextBox id="txtOilAfterEBP" runat="server" maxlength="6" text="1.00"/><br>
						<asp:RegularExpressionValidator id="revMaxStdWetOilRebus1" ControlToValidate="txtOilAfterEBP" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvMaxStdWetOilRebus1" ControlToValidate="txtOilAfterEBP" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>% LTDS 1</td>
			        <td><asp:TextBox id="txtPKLTDS1" runat="server" maxlength="6" text="1.00"/><br>
						<asp:RegularExpressionValidator id="revMaxStdWetOilRebus2" ControlToValidate="txtPKLTDS1" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvMaxStdWetOilRebus2" ControlToValidate="txtPKLTDS1" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
			    </tr>
			    <tr>
			        <td>% Fibre Press</td>
			        <td><asp:TextBox id="txtOilFibre" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revOilWetRebus1" ControlToValidate="txtOilFibre" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvOilWetRebus1" ControlToValidate="txtOilFibre" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>% LTDS 2</td>
			        <td><asp:TextBox id="txtPKLTDS2" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revOilWetRebus2" ControlToValidate="txtPKLTDS2" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvOilWetRebus2" ControlToValidate="txtPKLTDS2" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
			    </tr>
			    <tr>
			        <td>% Nut Press</td>
			        <td><asp:TextBox id="txtOilNutPres" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revVMRebus1" ControlToValidate="txtOilNutPres" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvVMRebus1" ControlToValidate="txtOilNutPres" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>% Shell Clybath</td>
			        <td><asp:TextBox id="txtPKShell" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revVMRebus2" ControlToValidate="txtPKShell" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvVMRebus2" ControlToValidate="txtPKShell" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
			    </tr>
			    <tr>
			        <td>% Solid Decantar</td>
			        <td><asp:TextBox id="txtOilSolid" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNOSRebus1" ControlToValidate="txtOilSolid" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvNOSRebus1" ControlToValidate="txtOilSolid" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>% Kernel Losses</td>
			        <td><asp:TextBox id="txtPKLosses" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revNOSRebus2" ControlToValidate="txtPKLosses" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvNOSRebus2" ControlToValidate="txtPKLosses" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
			    </tr>
			    <tr>
			        <td>% Oil Losses</td>
			        <td><asp:TextBox id="txtOilLosses" runat="server" maxlength="6" text="0"/><br>
						<asp:RegularExpressionValidator id="revOilDMRebus1" ControlToValidate="txtOilLosses" ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 5 digits and 2 decimal points." />
						<asp:RangeValidator  id="rvOilDMRebus1" ControlToValidate="txtOilLosses" MinimumValue="0" MaximumValue="100"
							Type="double" EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False"
							ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
			        <td>&nbsp;</td>
			    </tr>
			    <tr>
			        <td>&nbsp;</td>
			        <td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
			        <td>&nbsp;</td>
			    </tr>
			    <tr>
			        <td>&nbsp;</td>
			        <td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
			        <td>&nbsp;</td>
			    </tr>
			    
			    <tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				</tr>
			    
			    <tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				</tr>
			
	            <tr>
		            <td colspan="6" style="height: 25px">&nbsp;</td>
	            </tr>
	            <tr>
		            <td colspan="6">
			            <asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server"
				            AlternateText="Save" />
			            <asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server"
				            AlternateText="Delete" Visible="False" CausesValidation="False" />
			            <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server"
				            AlternateText="Back" CausesValidation="False" />
		            </td>
	            </tr>
            </table>
    </div>
		</form>
	</body>
</HTML>
