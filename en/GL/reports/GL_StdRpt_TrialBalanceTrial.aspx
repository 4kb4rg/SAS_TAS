<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_TrialBalanceTrial.aspx.vb" Inherits="GL_StdRpt_TrialBalanceTrial" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Trial Balance Report</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">GENERAL LEDGER - TRIAL BALANCE</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:GL_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" runat=server>
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode" runat="server" /> : </td>
					<td colspan="4"> <GG:AutoCompleteDropDownList id="lstAccCode" Width="70%"  runat="server" /> 	
							              <input type="button" value=" ... " id="Find" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode', 'False');" CausesValidation=False runat=server />  		   	
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode2" runat="server" /> : </td>
					<td colspan="4"> <GG:AutoCompleteDropDownList id="lstAccCode2" Width="70%"  runat="server" /> 
							              <input type="button" value=" ... " id="Find2" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode2', 'False');" CausesValidation=False runat=server />  
							              
						</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				
				
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td colspan=2><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td width=25%><asp:label id=lblChartofAccType runat=server /> : </td>
					<td colspan=2>
						<asp:CheckBoxList id="cblAccType" RepeatColumns="2" repeatdirection="horizontal" autopostback="true" runat="server">
							<asp:listitem id="option1" text=" Balance Sheet" value="1" runat="server"/>
							<asp:listitem id="option2" text=" Profit and Loss" value="2" runat="server"/>
						</asp:CheckBoxList>
					</td>
					<td colspan=3>&nbsp;</td>			
				</tr>	
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblType" visible="false" text=" Type" runat="server" />
		<asp:label id="lblLocation" visible="false" runat="server" />
		<asp:label id="lblAccDesc" visible="false" runat="server" />
	</body>
</HTML>
