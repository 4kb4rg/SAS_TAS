<%@ Page Language="vb" Inherits="PM_OilQuality_Det" Src="../../../include/PM_trx_OilQuality_Det.aspx.vb"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>OIL QUALITY TRANSACTION</title> 
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
					<td class="mt-h" colspan="3">OIL QUALITY DETAILS</td>
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
					<td width="15%">Period :
					</td>
					<td width="25%"><asp:Label id="lblPeriod" runat="server" /></td>
				</tr>
				<tr>
					<td>Oil in Final Effluent</td>
					<td></td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>% O/W</td>
					<td><asp:TextBox id="txtOW" runat="server" maxlength="20" /><br>
						<asp:RegularExpressionValidator id="revOW" ControlToValidate="txtOW" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvOW" ControlToValidate="txtOW" MinimumValue="0" MaximumValue="100" Type="double"
							EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>Updated By :
					</td>
					<td><asp:Label id="lblUpdateBy" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% D/W</td>
					<td><asp:TextBox id="txtDW" runat="server" /><br>
						<asp:RegularExpressionValidator id="revDW" ControlToValidate="txtDW" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic" runat="server" EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points." />
						<asp:RangeValidator id="rvDW" ControlToValidate="txtDW" MinimumValue="0" MaximumValue="100" Type="double"
							EnableClientScript="True" runat="server" display="dynamic" EnableViewState="False" ErrorMessage="The value is out of range." />
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% O/D</td>
					<td><asp:TextBox id="txtOD" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revOD" runat="server" ControlToValidate="txtOD" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvOD" runat="server" ControlToValidate="txtOD" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Oil Ex Pure Oil Tank</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td height="25">% Dirt</td>
					<td><asp:TextBox id="txtDirt0" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revDirt0" runat="server" ControlToValidate="txtDirt0" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvDirt0" runat="server" ControlToValidate="txtDirt0" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
					<td>&nbsp;</td>
					<td><U><B>CPO Tank</B></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist</td>
					<td><asp:TextBox id="txtMoist0" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revMoist0" runat="server" ControlToValidate="txtMoist0" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvMoist0" runat="server" ControlToValidate="txtMoist0" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% FFA ( Tank No.1 ) :*</td>
						<td><asp:TextBox id="txtCPOTankFFA1" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA1" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA1" 
							ControlToValidate="txtCPOTankFFA1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA1"
							ControlToValidate="txtCPOTankFFA1"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Oil ex Purifier No. 1</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.1 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist1" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist1" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist1" 
							ControlToValidate="txtCPOTankMoist1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist1"
							ControlToValidate="txtCPOTankMoist1"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Dirt</td>
					<td><asp:TextBox id="txtDirt1" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revDirt1" runat="server" ControlToValidate="txtDirt1" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvDirt1" runat="server" ControlToValidate="txtDirt1" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.1 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt1" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt1" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt1" 
							ControlToValidate="txtCPOTankDirt1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt1"
							ControlToValidate="txtCPOTankDirt1"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist</td>
					<td><asp:TextBox id="txtMoist1" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revMoist1" runat="server" ControlToValidate="txtMoist1" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvMoist1" runat="server" ControlToValidate="txtMoist1" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% FFA ( Tank No.2 ) :*</td>
						<td><asp:TextBox id="txtCPOTankFFA2" text="0" runat="server" width=50% maxlength="6"/>                       
							<asp:RequiredFieldValidator 
								id="rfvCPOTankFFA2" 
								runat="server"  
								ControlToValidate="txtCPOTankFFA2" 
								text = "Field cannot be blank"
								display="dynamic"/>
							<asp:RegularExpressionValidator id="revCPOTankFFA2" 
								ControlToValidate="txtCPOTankFFA2"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "Maximum length 2 digits and 2 decimal places"
								runat="server"/>
							<asp:RangeValidator id="rvCPOTankFFA2"
								ControlToValidate="txtCPOTankFFA2"
								MinimumValue="0"
								MaximumValue="999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Oil ex Purifier No. 2</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.2 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist2" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist2" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist2" 
							ControlToValidate="txtCPOTankMoist2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist2"
							ControlToValidate="txtCPOTankMoist2"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Dirt</td>
					<td><asp:TextBox id="txtDirt2" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revDirt2" runat="server" ControlToValidate="txtDirt2" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvDirt2" runat="server" ControlToValidate="txtDirt2" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.2 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt2" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt2" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt2" 
							ControlToValidate="txtCPOTankDirt2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt2"
							ControlToValidate="txtCPOTankDirt2"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist</td>
					<td><asp:TextBox id="txtMoist2" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revMoist2" runat="server" ControlToValidate="txtMoist2" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvMoist2" runat="server" ControlToValidate="txtMoist2" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% FFA ( Tank No.3 ) :*</td>
						<td><asp:TextBox id="txtCPOTankFFA3" text="0" runat="server" width=50% maxlength="6"/>                       
							<asp:RequiredFieldValidator 
								id="rfvCPOTankFFA3" 
								runat="server"  
								ControlToValidate="txtCPOTankFFA3" 
								text = "Field cannot be blank"
								display="dynamic"/>
							<asp:RegularExpressionValidator id="revCPOTankFFA3" 
								ControlToValidate="txtCPOTankFFA3"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "Maximum length 2 digits and 2 decimal places"
								runat="server"/>
							<asp:RangeValidator id="rvCPOTankFFA3"
								ControlToValidate="txtCPOTankFFA3"
								MinimumValue="0"
								MaximumValue="999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Oil ex Purifier No. 3</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.3 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist3" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist3" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist3" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist3" 
							ControlToValidate="txtCPOTankMoist3"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist3"
							ControlToValidate="txtCPOTankMoist3"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Dirt</td>
					<td><asp:TextBox id="txtDirt3" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revDirt3" runat="server" ControlToValidate="txtDirt3" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvDirt3" runat="server" ControlToValidate="txtDirt3" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% Dirt ( Tank No.3 ) :*</td>
						<td><asp:TextBox id="txtCPOTankDirt3" Text="0" runat="server" width=50% maxlength="6"/>                       
							<asp:RequiredFieldValidator 
								id="rfvCPOTankDirt3" 
								runat="server"  
								ControlToValidate="txtCPOTankDirt3" 
								text = "Field cannot be blank"
								display="dynamic"/>
							<asp:RegularExpressionValidator id="revCPOTankDirt3" 
								ControlToValidate="txtCPOTankDirt3"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "Maximum length 2 digits and 2 decimal places"
								runat="server"/>
							<asp:RangeValidator id="rvCPOTankDirt3"
								ControlToValidate="txtCPOTankDirt3"
								MinimumValue="0"
								MaximumValue="999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist</td>
					<td><asp:TextBox id="txtMoist3" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revMoist3" runat="server" ControlToValidate="txtMoist3" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvMoist3" runat="server" ControlToValidate="txtMoist3" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% FFA ( Tank No.4 ) :*</td>
						<td><asp:TextBox id="txtCPOTankFFA4" text="0" runat="server" width=50% maxlength="6"/>                       
							<asp:RequiredFieldValidator 
								id="rfvCPOTankFFA4" 
								runat="server"  
								ControlToValidate="txtCPOTankFFA4" 
								text = "Field cannot be blank"
								display="dynamic"/>
							<asp:RegularExpressionValidator id="revCPOTankFFA4" 
								ControlToValidate="txtCPOTankFFA4"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "Maximum length 2 digits and 2 decimal places"
								runat="server"/>
							<asp:RangeValidator id="rvCPOTankFFA4"
								ControlToValidate="txtCPOTankFFA4"
								MinimumValue="0"
								MaximumValue="999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Oil ex Purifier No. 4</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.4 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist4" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist4" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist4" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist4" 
							ControlToValidate="txtCPOTankMoist4"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist4"
							ControlToValidate="txtCPOTankMoist4"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Dirt</td>
					<td><asp:TextBox id="txtDirt4" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revDirt4" runat="server" ControlToValidate="txtDirt4" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvDirt4" runat="server" ControlToValidate="txtDirt4" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% Dirt ( Tank No.4 ) :*</td>
						<td><asp:TextBox id="txtCPOTankDirt4" text="0" runat="server" width=50% maxlength="6"/>                       
							<asp:RequiredFieldValidator 
								id="rfvCPOTankDirt4" 
								runat="server"  
								ControlToValidate="txtCPOTankDirt4" 
								text = "Field cannot be blank"
								display="dynamic"/>
							<asp:RegularExpressionValidator id="revCPOTankDirt4" 
								ControlToValidate="txtCPOTankDirt4"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "Maximum length 2 digits and 2 decimal places"
								runat="server"/>
							<asp:RangeValidator id="rvCPOTankDirt4"
								ControlToValidate="txtCPOTankDirt4"
								MinimumValue="0"
								MaximumValue="999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">% Moist</td>
					<td><asp:TextBox id="txtMoist4" runat="server"></asp:TextBox><br>
						<asp:RegularExpressionValidator id="revMoist4" runat="server" ControlToValidate="txtMoist4" Display="Dynamic" ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							EnableViewState="False" ErrorMessage="Maximum length 3 digits and 2 decimal points."></asp:RegularExpressionValidator>
						<asp:RangeValidator id="rvMoist4" runat="server" ControlToValidate="txtMoist4" Display="Dynamic" Type="Double"
							MaximumValue="100" MinimumValue="0" EnableViewState="False" ErrorMessage="The value is out of range."></asp:RangeValidator></td>
						<td>&nbsp;</td>
						<td height=25>% FFA ( Tank No.5 ) :*</td>
						<td><asp:TextBox id="txtCPOTankFFA5" text="0" runat="server" width=50% maxlength="6"/>                       
							<asp:RequiredFieldValidator 
								id="rfvCPOTankFFA5" 
								runat="server"  
								ControlToValidate="txtCPOTankFFA5" 
								text = "Field cannot be blank"
								display="dynamic"/>
							<asp:RegularExpressionValidator id="revCPOTankFFA5" 
								ControlToValidate="txtCPOTankFFA5"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "Maximum length 2 digits and 2 decimal places"
								runat="server"/>
							<asp:RangeValidator id="rvCPOTankFFA5"
								ControlToValidate="txtCPOTankFFA5"
								MinimumValue="0"
								MaximumValue="999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
						</td>
						<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.5 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist5" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist5" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist5" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist5" 
							ControlToValidate="txtCPOTankMoist5"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist5"
							ControlToValidate="txtCPOTankMoist5"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.5 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt5" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt5" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt5" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt5" 
							ControlToValidate="txtCPOTankDirt5"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt5"
							ControlToValidate="txtCPOTankDirt5"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.6 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA6" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA6" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA6" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA6" 
							ControlToValidate="txtCPOTankFFA6"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA6"
							ControlToValidate="txtCPOTankFFA6"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
					
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.6 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist6" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist6" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist6" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist6" 
							ControlToValidate="txtCPOTankMoist6"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist6"
							ControlToValidate="txtCPOTankMoist6"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.6 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt6" text="0" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt6" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt6" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt6" 
							ControlToValidate="txtCPOTankDirt6"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt6"
							ControlToValidate="txtCPOTankDirt6"
							MinimumValue="0"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td colspan="6" height="25">&nbsp;</td>
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
		</form>
	</body>
</HTML>
