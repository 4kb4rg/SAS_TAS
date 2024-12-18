<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_BankCreditList.aspx.vb" Inherits="PR_StdRpt_BankCreditList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Bank Crediting Listing</title>
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
					<td class="font9Tahoma">PAYROLL - BANK CREDITING LISTING</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2">
                        <hr style="width :100%" /> 
					</td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
                        <hr style="width :100%" /> 
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=17%>Bank Code : </td>
					<td width=39%><asp:DropDownList id=ddlBankCode AutoPostBack=true onSelectedIndexChanged=IndexBankChanged width="100%" runat=server/></td>
					<td width=4%>&nbsp;</td>
					<td width=40%><asp:TextBox id=txtProgramPath visible=false maxlength=20 width=50% runat=server /></td>
				</tr>
				<tr>
					<td>Bank Branch :</td>
					<td><asp:TextBox id="txtBankBranch" maxlength="100" width="30%" runat="server" />
						<asp:RequiredFieldValidator 
							id="validBankBranch" 
							ControlToValidate="txtBankBranch"
							ErrorMessage="Bank Branch is a required field."
							ForeColor="Red" 
							runat="server" />
						</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;&nbsp;
					<!-- Add by YWC on 3/6/2004 on BFR 00027 item (25) -->
					<asp:Label id=lblErrBankCode text='Printing is disallowed as the Bank Code is empty' ForeColor=Red visible=false runat=server />
					<!-- End Add -->
					</td>
				</tr>
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
