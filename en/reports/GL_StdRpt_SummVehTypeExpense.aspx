<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_SummVehTypeExpense.aspx.vb" Inherits="GL_StdRpt_SummVehTypeExpense" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Summarised Vehicle Type Expense</title>
        <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain" class="main-modul-bg-app-list-pu">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>

             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - SUMMARISED <asp:label id="lblTitle" runat="server" /> EXPENSE</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
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
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width=25%><asp:label id="lblVehCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				
				<tr>
					<td width=25%><asp:label id="lblVehTypeCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehTypeCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>

				<tr>
					<td width=25%><asp:label id="lblAccCode" runat="server" /> : </td>  
					<td colspan=2>
						<asp:TextBox id="txtSrchAccCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				
				<tr>
					<td><asp:label id="lblBlock" runat="server" /> Type :</td>
					<td colspan=2>
						<asp:DropDownList id="ddlBlkType" AutoPostBack=true width="25%" runat="server" />
					</td>
				</tr>
				<tr id=TrBlkGrp>
					<td><asp:label id="lblBlkGrpCode" runat="server" /> :</td>
					<td colspan=2>
						<asp:textbox id="txtSrchBlkGrpCode" maxlength="8" width="25%" runat="server" /> (blank for all)
					</td>
				</tr>	
				<tr id=TrBlk>
					<td width=25%><asp:label id="lblBlkCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:textbox id="txtSrchBlkCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr id=TrSubBlk>
					<td width=25%><asp:label id="lblSubBlkCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:textbox id="txtSrchSubBlkCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr>
					<td width=25%><asp:label id="lblVehExpCode" runat="server" /> : </td>  
					<td colspan=2>
						<asp:TextBox id="txtSrchVehExpCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td width=5%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" /></td>			
					<td width=70%><asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
				</tr>	
														
				<tr>
					<td colspa=3>&nbsp;</td>
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
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblHidCostLevel" visible="false" runat="server" />
	</body>
</HTML>
