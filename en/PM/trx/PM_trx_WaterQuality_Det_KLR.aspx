<%@ Page Language="vb" Src="../../../include/PM_trx_WaterQuality_Det_KLR.aspx.vb" Inherits="PM_trx_WaterQuality_Det_KLR" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>WATER QUALITY DETAILS TRANSACTION</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<table cellpadding="2" cellspacing=0 width="100%" border="0">
 				
				<tr>
					<td class="mt-h" colspan="5" width="70%">WATER QUALITY DETAILS</td>
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
					<td>&nbsp;</td>
					<td>Min</td>
					<td>Max</td>
					<td>Oil wet</td>
					<td>Keterangan</td>
				</tr>
				<tr>
					<td colspan="4">
                        MUTU AIR UMPAN / FEED TANK</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">PH</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtPH1"  text="8.50" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtPH1" 
							ControlToValidate="txtPH1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtPH2"  text="9.00" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtPH2" 
							ControlToValidate="txtPH2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtPH3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtPH3" 
							ControlToValidate="txtPH3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtPH_K"  text="Sam Chem 101=   kg" runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Hardness</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtHardness1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revHardness1" 
							ControlToValidate="txtHardness1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtHardness2"  text="5.00"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revHardness2" 
							ControlToValidate="txtHardness2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtHardness3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revHardness3" 
							ControlToValidate="txtHardness3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtHardness_K"  text="Sam Chem 201=   kg" runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">M. Alkalinity</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtAlkalinity1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revAlkalinity1" 
							ControlToValidate="txtAlkalinity1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtAlkalinity2"  text="35.00"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revAlkalinity2" 
							ControlToValidate="txtAlkalinity2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtAlkalinity3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revAlkalinity3" 
							ControlToValidate="txtAlkalinity3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtAlkalinity_K"  text="Sam Chem 301=   kg" runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">TDS</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTDS1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revTDS1" 
							ControlToValidate="txtTDS1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTDS2"  text="50.00" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revTDS2" 
							ControlToValidate="txtTDS2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTDS3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revTDS3" 
							ControlToValidate="txtTDS3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtTDS_K"  text="Sam Chem 401=   kg" runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Silica</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtSilica1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revSilica1" 
							ControlToValidate="txtSilica1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSilica2"  text="50.00" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revSilica2" 
							ControlToValidate="txtSilica2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSilica3"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revSilica3" 
							ControlToValidate="txtSilica3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtSilica_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td colspan="3">MUTU AIR BOILER</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
					<tr>
					<td style="width:20%; height:25px">PH</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtPH21"  text="10.50" runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revPH21" 
							ControlToValidate="txtPH21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtPH22"  text="11.50" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revPH22" 
							ControlToValidate="txtPH22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtPH23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revPH23" 
							ControlToValidate="txtPH23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtPH2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Hardness</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtHardness21"  runat="server" width="70%" maxlength="8"/>
					<asp:RegularExpressionValidator id="revHardness21" 
							ControlToValidate="txtHardness21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtHardness22" text="5.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revHardness22" 
							ControlToValidate="txtHardness22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtHardness23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revHardness23" 
							ControlToValidate="txtHardness23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtHardness2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Caustic Alkalinity(P2)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtAlkalinity21"  runat="server" width="70%" maxlength="8"/><asp:RegularExpressionValidator id="revAlkalinity21" 
							ControlToValidate="txtAlkalinity21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtAlkalinity22"  text="300.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revAlkalinity22" 
							ControlToValidate="txtAlkalinity22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtAlkalinity23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revAlkalinity23" 
							ControlToValidate="txtAlkalinity23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtAlkalinity2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">TDS</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTDS21"  runat="server" width="70%" maxlength="8"/>
					<asp:RegularExpressionValidator id="revTDS21" 
							ControlToValidate="txtTDS21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTDS22"  text="2000.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revTDS22" 
							ControlToValidate="txtTDS22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTDS23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revTDS23" 
							ControlToValidate="txtTDS23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtTDS2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Silica</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtSilica21"  runat="server" width="70%" maxlength="8"/>
					<asp:RegularExpressionValidator id="revSilica21" 
							ControlToValidate="txtSilica21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSilica22"  text="150.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revSilica22" 
							ControlToValidate="txtSilica22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSilica23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revSilica23" 
							ControlToValidate="txtSilica23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtSilica2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">Sulfit</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtSulfit21"  text="30.00" runat="server" width="70%" maxlength="8"/>
					<asp:RegularExpressionValidator id="revSulfit21" 
							ControlToValidate="txtSulfit21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSulfit22"  text="50.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revSulfit22" 
							ControlToValidate="txtSulfit22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSulfit23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revSulfit23" 
							ControlToValidate="txtSulfit23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtSulfit2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Squesterant SC-401</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtSquest21"  text="100.00" runat="server" width="70%" maxlength="8"/>
					<asp:RegularExpressionValidator id="revSquest21" 
							ControlToValidate="txtSquest21"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSquest22"  text="200.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revSquest22" 
							ControlToValidate="txtSquest22"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSquest23"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revSquest23" 
							ControlToValidate="txtSquest23"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtSquest2_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">T. Alkalinity</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtAlkalinity31"  runat="server" width="70%" maxlength="8"/>
					<asp:RegularExpressionValidator id="revAlkalinity31" 
							ControlToValidate="txtAlkalinity31"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtAlkalinity32"  text="700.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revAlkalinity32" 
							ControlToValidate="txtAlkalinity32"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtAlkalinity33"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revAlkalinity33" 
							ControlToValidate="txtAlkalinity33"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtAlkalinity3_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td colspan="7" style="height:25px"></td>
				</tr>	
				
				<tr>
					<td colspan="3">KRITERIA ANGKA KERJA</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
					<tr>
					<td style="width:20%; height:25px">Tekanan Rebusan (Kg/cm)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtReb1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtReb1" 
							ControlToValidate="txtReb1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtReb2"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtReb2" 
							ControlToValidate="txtReb2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtReb3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtReb3" 
							ControlToValidate="txtReb3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtReb_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Waktu merebus (min)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtWak1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtWak1" 
							ControlToValidate="txtWak1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtWak2"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtWak2" 
							ControlToValidate="txtWak2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtWak3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtWak3" 
							ControlToValidate="txtWak3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtWak_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Temp Ketel digester(C)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtDig1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtDig1" 
							ControlToValidate="txtDig1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtDig2"  text="100.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtDig2" 
							ControlToValidate="txtDig2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtDig3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtDig3" 
							ControlToValidate="txtDig3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtDig_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Temp sludge tank(C)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtSlg1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtSlg1" 
							ControlToValidate="txtSlg1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSlg2"  text="90.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtSlg2" 
							ControlToValidate="txtSlg2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSlg3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtSlg3" 
							ControlToValidate="txtSlg3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtSlg_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Temp oil tank(C)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtOtk1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtOtk1" 
							ControlToValidate="txtOtk1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtOtk2"  text="90.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtOtk2" 
							ControlToValidate="txtOtk2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtOtk3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtOtk3" 
							ControlToValidate="txtOtk3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtOtk_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				
				<tr>
					<td style="width:20%; height:25px">Temp oil purifier(C)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtOpu1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtOpu1" 
							ControlToValidate="txtOpu1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtOpu2"  text="80.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtOpu2" 
							ControlToValidate="txtOpu2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtOpu3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtOpu3" 
							ControlToValidate="txtOpu3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtOpu_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Temp vacum drier(C)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtVdr1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtVdr1" 
							ControlToValidate="txtVdr1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtVdr2"  text="80.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtVdr2" 
							ControlToValidate="txtVdr2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtVdr3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtVdr3" 
							ControlToValidate="txtVdr3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtVdr_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">Temp kernel drier(C)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtKdr1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtKdr1" 
							ControlToValidate="txtKdr1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtKdr2"  text="100.00" runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtKdr2" 
							ControlToValidate="txtKdr2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtKdr3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtKdr3" 
							ControlToValidate="txtKdr3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtKdr_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				<tr>
					<td style="width:20%; height:25px">Tekanan vacum drier(Mm/Hg)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtTvd1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtTvd1" 
							ControlToValidate="txtTvd1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTvd2"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtTvd2" 
							ControlToValidate="txtTvd2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtTvd3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtTvd3" 
							ControlToValidate="txtTvd3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtTvd_K"  runat="server" width="70%" maxlength="25"/>
					</td>
				</tr>
				
				<tr>
					<td style="width:20%; height:25px">Silkus merebus (Min)</td>
					<td style="width:15%; height:25px">
					<asp:TextBox id="txtSmr1"  runat="server" width="70%" maxlength="8"/>
                        <asp:RegularExpressionValidator id="revtxtSmr1" 
							ControlToValidate="txtSmr1"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSmr2"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtSmr2" 
							ControlToValidate="txtSmr2"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td style="width:15%; height:25px">
					    <asp:TextBox id="txtSmr3"  runat="server" width="70%" maxlength="8"/>
					    <asp:RegularExpressionValidator id="revtxtSmr3" 
							ControlToValidate="txtSmr3"
							ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
							Display="Dynamic"
							text = "Maximum length 5 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td colspan="4">
					 <asp:TextBox id="txtSmr_K"  runat="server" width="70%" maxlength="25"/>
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
