<%@ Page Language="vb" Src="../../../include/PM_trx_OilQuality_Det_KLR.aspx.vb" Inherits="PM_trx_OilQuality_Det_KLR" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>OIL QUALITY DETAILS TRANSACTION</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<table cellpadding="2" cellspacing=0 width="100%" border="0">
 				
				<tr>
					<td class="mt-h" colspan="5" width="70%">OIL QUALITY DETAILS</td>
					<td colspan="3" align="right"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan="8"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">Transaction Date :* </td>
					<td colspan="3" style="width:40%; height:25px">
						<asp:TextBox id="txtdate" runat="server" width="60%" maxlength="20"/>                       
						<a href="javascript:PopCal('txtdate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:RequiredFieldValidator 
							id="rfvDate" 
							runat="server"  
							ControlToValidate="txtdate" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:label id="lblDate" Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
						<asp:label id="lblFmt"  forecolor=red Visible = false Runat="server"/> 
						<asp:label id="lblDupMsg"  Text="Transaction on this date already exist" Visible=false forecolor=red Runat="server"/>								
					</td>
					<td style="width:5%; height:25px">&nbsp;</td>					
					<td style="width:15%; height:25px">Status :</td>
					<td style="width:15%; height:25px"><asp:Label id="lblStatus" runat="server"/></td>
					<td style="width:5%; height:25px">&nbsp;</td>					
				</tr>	
				<tr>
					<td style="height:25px">&nbsp;</td>
					<td style="height:25px" colspan="3">&nbsp;</td>
					<td style="height:25px"></td>
					<td style="height:25px">Updated By :</td>
					<td style="height:25px"><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td style="height:25px">&nbsp;</td>	
				</tr>
			
				<tr>
					<td style="height:25px">&nbsp;</td>
				    <td colspan="3" style="height:25px">&nbsp;</td>
					<td style="height:25px">&nbsp;</td>
					<td style="height:25px">Last Update :</td>
					<td style="height:25px"><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td style="height:25px">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">
                        MUTU MINYAK SAWIT</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td>&nbsp;</td>
					<td>Min</td>
					<td>Max</td>
					<td>Oil wet</td>
					<td>Keterangan</td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">Kadar Asam lemak Bebas(FFA) %</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtFFA1"  text="2.50" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtFFA1" 
							ControlToValidate="txtFFA1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtFFA2"  text="3.00" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtFFA2" 
							ControlToValidate="txtFFA2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtFFA3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtFFA3" 
							ControlToValidate="txtFFA3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtFFA_k"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Kadar Air(Vm) %</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtVM1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtVM1" 
							ControlToValidate="txtVM1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtVM2"  text="0.25" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtVM2" 
							ControlToValidate="txtVM2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtVM3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtVM3" 
							ControlToValidate="txtVM3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtVM_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Kadar Kotoran(Dirt) %</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtDirt1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtDirt1" 
							ControlToValidate="txtDirt1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtDirt2"  text="0.22" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtDirt2" 
							ControlToValidate="txtDirt2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtDirt3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtDirt3" 
							ControlToValidate="txtDirt3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtDirt_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				
				<tr>
					<td colspan="7" style="height:25px"></td>
				</tr>
				
				<tr>
					<td colspan="4">
                        CPO TANK</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>FFA</td>
					<td>Moist</td>
					<td>Dirt</td>
		            <td>&nbsp;</td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">Tank No. 1</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTank1FAA"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank1FAA" 
							ControlToValidate="txtTank1FAA"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank1Moist"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank1Moist" 
							ControlToValidate="txtTank1Moist"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank1Dirt"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank1Dirt" 
							ControlToValidate="txtTank1Dirt"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
				</tr>		
				<tr>
					<td style="width:20%; height:25px">Tank No. 2</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTank2FAA"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank2FAA" 
							ControlToValidate="txtTank2FAA"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank2Moist"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank2Moist" 
							ControlToValidate="txtTank2Moist"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank2Dirt"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank2Dirt" 
							ControlToValidate="txtTank2Dirt"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
				</tr>	
				
				<tr>
					<td style="width:20%; height:25px">Tank No. 3</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTank3FAA"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank3FAA" 
							ControlToValidate="txtTank3FAA"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank3Moist"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank3Moist" 
							ControlToValidate="txtTank3Moist"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank3Dirt"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank3Dirt" 
							ControlToValidate="txtTank3Dirt"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Tank No. 4</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTank4FAA"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank4FAA" 
							ControlToValidate="txtTank4FAA"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank4Moist"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank4Moist" 
							ControlToValidate="txtTank4Moist"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank4Dirt"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank4Dirt" 
							ControlToValidate="txtTank4Dirt"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
				</tr>	
				
				<tr>
					<td style="width:20%; height:25px">Tank No. 5</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTank5FAA"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank5FAA" 
							ControlToValidate="txtTank5FAA"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank5Moist"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank5Moist" 
							ControlToValidate="txtTank5Moist"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank5Dirt"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank5Dirt" 
							ControlToValidate="txtTank5Dirt"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
				</tr>			
				<tr>
					<td style="width:20%; height:25px">Tank No. 6</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTank6FAA"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank6FAA" 
							ControlToValidate="txtTank6FAA"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank6Moist"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank6Moist" 
							ControlToValidate="txtTank6Moist"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTank6Dirt"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTank6Dirt" 
							ControlToValidate="txtTank6Dirt"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
				</tr>					
							
								
				<tr>
					<td colspan="7" style="height:25px"></td>
				</tr>
				<tr>
					<td colspan="7">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" Visible=False CausesValidation=False />
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
