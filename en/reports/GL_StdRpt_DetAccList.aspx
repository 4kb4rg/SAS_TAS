<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_DetAccList.aspx.vb" Inherits="GL_StdRpt_DetAccList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Detailed Account Listing Report</title>
               <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"   class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - DETAILED ACCOUNT LISTING</strong></td>
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
					<td width=25%><asp:label id=lblChartofAccCode runat=server /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchAccCode" width="25%" runat="server"/>
					</td>			
				</tr>	
				<tr>
					<td width=25%><asp:label id=lblBlkCode runat=server /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchBlkCode" width="25%" runat="server"/>
					</td>			
				</tr>
				<tr>
					<td width=25%><asp:label id=lblVehCode runat=server /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehCode" width="25%" runat="server"/>
					</td>			
				</tr>
				<tr>
					<td width=25%><asp:label id=lblVehExpCode runat=server /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehExpCode" width="25%" runat="server"/>
					</td>			
				</tr>
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td width=5%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" /></td>			
					<td width=70%><asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
				</tr>
				<tr>
					<td width=25%><asp:label id=lblChartofAccType runat=server /> Type : </td>
					<td colspan=2>
						<asp:CheckBoxList id="cblAccType" RepeatColumns="2" repeatdirection="horizontal" autopostback="true" runat="server">
							<asp:listitem id="option1" text=" Balance Sheet" value="1" runat="server"/>
							<asp:listitem id="option2" text=" Profit and Loss" value="2" runat="server"/>
						</asp:CheckBoxList>
					</td>			
				</tr>
				<tr>
					<td colspan=3>
						<asp:CheckBox id="cbEstExpense" text=" Display Estate Expenditure" value="Yes" runat="server"/>
					</td>
				</tr>										
				<tr>
					<td colspa=3>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>					
				</tr>				
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>
