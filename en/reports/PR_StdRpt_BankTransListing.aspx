<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_BankTransListing.aspx.vb" Inherits="PR_StdRpt_BankTransListing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Bank Transfer Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>PAYROLL - BANK TRANSFER LISTING</strong> </td>
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
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td style="WIDTH: 118px; HEIGHT: 23px">Bank :</td>
					<td style="HEIGHT: 23px">
						<asp:RadioButton id="rbtBCA" runat="server" Text="Bank BCA" GroupName="grpBank" Checked="True"></asp:RadioButton></td>
				</tr>
				<tr>
					<td style="WIDTH: 118px"></td>
					<td>
						<asp:RadioButton id="rbtLain" runat="server" Text="Bank Lain" GroupName="grpBank" ></asp:RadioButton></td>
				</tr>
				<tr>
					<td style="WIDTH: 118px; HEIGHT: 9px">
						S&amp;B :</td>
					<td style="HEIGHT: 9px">
						<asp:TextBox id="txtSB" runat="server" Width="75%" MaxLength="25"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 118px; HEIGHT: 9px">
						Management :</td>
					<td style="HEIGHT: 9px">
						<asp:TextBox id="txtManagement" runat="server" Width="75%" MaxLength="25"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />
                        <br />
&nbsp;<asp:Button 
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
	</body>
</HTML>
