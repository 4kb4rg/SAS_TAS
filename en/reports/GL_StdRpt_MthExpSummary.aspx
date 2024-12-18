<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_MthExpSummary.aspx.vb" Inherits="GL_StdRpt_MthExpSummary" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Monthly Expenditure Report Summary</title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<input type=hidden id=hidActGrpText runat="server" NAME="hidActGrpText"/>
			<input type=hidden id=hidActGrpCode runat=server name=hidActGrpCode />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - <asp:label id=lblTitle runat=server/> </strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
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
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td colspan=2><asp:label id=lblErrActGrp forecolor=red visible=false runat=server /></td>
				</tr>
				<tr>
					<td valign=top><asp:label id=lblActGrp runat=server/> :</td>
					<td colspan=2>
						<table width=100% cellpadding=0 cellspadding=0 border=0>
							<tr>
								<td width=5% valign=top><asp:CheckBox text=All id="cbActGrpAll" OnCheckedChanged=Check_Clicked AutoPostBack=true runat=server /></td>
								<td width=95% valign=top><asp:CheckBoxList id="cblActGrp" OnSelectedIndexChanged=ActGrpCheckList AutoPostBack=True RepeatColumns="1" RepeatDirection="Vertical" runat=server /></td>			
							</tr>
						</table>
					</td>
				</tr>		
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td width=5%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" /></td>			
					<td width=70%><asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
				</tr>											
				<tr>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>					
				</tr>				
			</table>
                 </div>
            </td>
            </tr>
            </table>   
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat=server />
		<asp:label id="lblPleaseSelect" visible=false text="Please select " runat=server />
	</body>
</HTML>
