<%@ Page Language="vb" src="../../include/reports/AR_StdRpt_StmtOfAccount.aspx.vb" Inherits="AR_StdRpt_StmtOfAccount" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Receivables - Statement Of Account</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> ACCOUNT RECEIVABLES - STATEMENT OF ACCOUNT</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>	
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
						<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />
						<asp:Label id="lblErrAge" forecolor=red visible="false" text="Invalid range in " runat="server" />
					</td>
				</tr>						
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr id=TrSupp visible=false runat=server>
					<td>Suppress zero balance : </td>
					<td>
						<asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />			
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Cut-Off Date :* </td>
					<td width=39%>
						<asp:textbox id=txtCutOffDate maxlength=30 width=50% runat=server />
						<asp:RequiredFieldValidator 
							id=rfvCutOffDate
							display=dynamic 
							runat=server
							ControlToValidate=txtCutOffDate
							text="<br>Please specify the Cut-Off Date." />
						<a href="javascript:PopCal('txtCutOffDate');">
						<asp:Image id="btnCutOffDate" runat="server" ImageUrl="../Images/calendar.gif"/>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%><asp:label id=lblBillParty runat=server /> Code : </td>
					<td width=39%><asp:textbox id=txtBuyer maxlength=128 width=50% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Ageing Category 1 : </td>
					<td width=39%>
						<asp:Textbox id=txtFromAge1 text=1 width=20% runat=server readonly /> - 
						<asp:Textbox id=txtToAge1 width=20% runat=server/> Days
						<asp:RequiredFieldValidator 
							id=rfvToAge1
							display=dynamic 
							runat=server
							ControlToValidate=txtToAge1
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvToAge1"
							ControlToValidate="txtToAge1"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrToAge1" forecolor=red visible="false" text="Invalid Range " runat="server" />
					</td>	
					<td colspan=2>&nbsp;</td>		
				</tr>	
				<tr>
					<td width=17%>Ageing Category 2 : </td>
					<td width=39%>
						<asp:Textbox id=txtFromAge2 width=20% runat=server />
						<asp:RequiredFieldValidator 
							id=rfvFromAge2
							display=dynamic 
							runat=server
							ControlToValidate=txtFromAge2
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvFromAge2"
							ControlToValidate="txtFromAge2"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrFromAge2" forecolor=red visible="false" text="Invalid Range " runat="server" />-
						<asp:Textbox id=txtToAge2 width=20% runat=server/> Days
						<asp:RequiredFieldValidator 
							id=rfvToAge2
							display=dynamic 
							runat=server
							ControlToValidate=txtToAge2
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvToAge2"
							ControlToValidate="txtToAge2"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrToAge2" forecolor=red visible="false" text="Invalid Range " runat="server" />
					</td>	
					<td colspan=2>&nbsp;</td>		
				</tr>
				<tr>
					<td width=17%>Ageing Category 3 : </td>
					<td width=39%>
						<asp:Textbox id=txtFromAge3 width=20% runat=server />
						<asp:RequiredFieldValidator 
							id=rfvFromAge3
							display=dynamic 
							runat=server
							ControlToValidate=txtFromAge3
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvFromAge3"
							ControlToValidate="txtFromAge3"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrFromAge3" forecolor=red visible="false" text="Invalid Range " runat="server" />-
						<asp:Textbox id=txtToAge3 width=20% runat=server/> Days
						<asp:RequiredFieldValidator 
							id=rfvToAge3
							display=dynamic 
							runat=server
							ControlToValidate=txtToAge3
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvToAge3"
							ControlToValidate="txtToAge3"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrToAge3" forecolor=red visible="false" text="Invalid Range " runat="server" />
					</td>	
					<td colspan=2>&nbsp;</td>		
				</tr>
				<tr>
					<td width=17%>Ageing Category 4 : </td>
					<td width=39%>
						<asp:Textbox id=txtFromAge4 width=20% runat=server />
						<asp:RequiredFieldValidator 
							id=rfvFromAge4
							display=dynamic 
							runat=server
							ControlToValidate=txtFromAge4
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvFromAge4"
							ControlToValidate="txtFromAge4"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrFromAge4" forecolor=red visible="false" text="Invalid Range " runat="server" />-
						<asp:Textbox id=txtToAge4 width=20% runat=server/> Days
						<asp:RequiredFieldValidator 
							id=rfvToAge4
							display=dynamic 
							runat=server
							ControlToValidate=txtToAge4
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvToAge4"
							ControlToValidate="txtToAge4"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrToAge4" forecolor=red visible="false" text="Invalid Range " runat="server" />
					</td>	
					<td colspan=2>&nbsp;</td>		
				</tr>
				<tr>
					<td width=17%>Ageing Category 5 : </td>
					<td width=39%>
						<asp:Textbox id=txtFromAge5 width=20% runat=server />
						<asp:RequiredFieldValidator 
							id=rfvFromAge5
							display=dynamic 
							runat=server
							ControlToValidate=txtFromAge5
							text="Please enter number of days. " />
						<asp:RangeValidator 
							id="rgvFromAge5"
							ControlToValidate="txtFromAge5"
							MinimumValue="2"
							MaximumValue="999"
							Type="integer"
							EnableClientScript="True"
							Text="Invalid format or range. "
							runat="server" 
							display="dynamic"/>
						<asp:Label id="lblErrFromAge5" forecolor=red visible="false" text="Invalid Range " runat="server" /> Days and above
					</td>	
					<td colspan=2>&nbsp;</td>		
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" />
                    </td>					
				</tr>				
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id=lblAccCode visible=false runat=server />
		<asp:label id=lblBlkCode visible=false runat=server />
		<asp:label id=lblCode visible=false runat=server />
		<asp:label id=lblToAge1 visible=false runat=server />
		<asp:label id=lblFromAge2 visible=false runat=server />
		<asp:label id=lblToAge2 visible=false runat=server />
		<asp:label id=lblFromAge3 visible=false runat=server />
		<asp:label id=lblToAge3 visible=false runat=server />
		<asp:label id=lblFromAge4 visible=false runat=server />
		<asp:label id=lblToAge4 visible=false runat=server />
		<asp:label id=lblFromAge5 visible=false runat=server />
	</body>
</HTML>
