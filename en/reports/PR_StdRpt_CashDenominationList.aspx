<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_CashDenominationList.aspx.vb" Inherits="PR_StdRpt_CashDenominationList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>PAYROLL - CASH DENOMINATION LIST</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">	
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />		
			<asp:Label id="lblLocation" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">PAYROLL - CASH DENOMINATION LIST</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=15%>Employee Code From : </td>
					<td width=35%><asp:TextBox id="txtEmpIDFrom" maxlength=20 width="50%" runat="server" /> (leave blank for all)</td>
					<td width=15%>Employee Code To : </td>
					<td width=35%><asp:TextBox id="txtEmpIDTo" maxlength=20 width="50%" runat="server" /> (leave blank for all)</td>
				</tr>
				<tr>
					<td>Employee Status : </td>
					<td><asp:DropDownList id="ddlStatus" size=1 width="50%" runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>Gang Code : </td>
					<td><asp:TextBox id="txtGangCode" maxlength=8 width="50%" runat="server" /> (leave blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Dollar Note : </td>
					<td><asp:TextBox id="txtDN1" maxlength=21 width="50%" runat="server" /> <asp:label id="lblDN1"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN1" 
							ControlToValidate="txtDN1"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN2" size=1 width="50%" runat="server" /> <asp:label id="lblDN2"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN2" 
							ControlToValidate="txtDN2"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN3" size=1 width="50%" runat="server" /> <asp:label id="lblDN3"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN3" 
							ControlToValidate="txtDN3"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN4" size=1 width="50%" runat="server" /> <asp:label id="lblDN4"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN4" 
							ControlToValidate="txtDN4"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN5" size=1 width="50%" runat="server" /> <asp:label id="lblDN5"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN5" 
							ControlToValidate="txtDN5"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN6" size=1 width="50%" runat="server" /> <asp:label id="lblDN6"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN6" 
							ControlToValidate="txtDN6"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN7" size=1 width="50%" runat="server" /> <asp:label id="lblDN7"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN7" 
							ControlToValidate="txtDN7"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN8" size=1 width="50%" runat="server" /> <asp:label id="lblDN8"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN8" 
							ControlToValidate="txtDN8"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN9" size=1 width="50%" runat="server" /> <asp:label id="lblDN9"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN9" 
							ControlToValidate="txtDN9"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN10" size=1 width="50%" runat="server" /> <asp:label id="lblDN10"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN10" 
							ControlToValidate="txtDN10"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN11" size=1 width="50%" runat="server" /> <asp:label id="lblDN11"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN11" 
							ControlToValidate="txtDN11"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:TextBox id="txtDN12" size=1 width="50%" runat="server" /> <asp:label id="lblDN12"  Runat="server"/>
						<asp:RegularExpressionValidator id="revDN12" 
							ControlToValidate="txtDN12"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan="3"><asp:label id="lblDNMsg"  Visible = false forecolor=red Runat="server"/> </td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
