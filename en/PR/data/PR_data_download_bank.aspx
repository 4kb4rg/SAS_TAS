<%@ Page Language="vb" src="../../../include/PR_data_download_bank.aspx.vb" Inherits="PR_data_download_bank" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRData" src="../../menu/menu_PRData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download Bank File</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%" align=center><UserControl:MenuPRData id=MenuPRData runat="server" /></td>
			</tr>
			<tr>
				<td width="100%">
					<form id=frmDownload runat=server>
					<table id=tblDownload border="0" cellpadding="2" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD BANK FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Payroll Bank Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">Steps:</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">1.&nbsp; Select Accounting Period, Bank Code and key in Crediting Date.</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">2.&nbsp; Click "Generate" button to generate the file.</td>
						</tr>
						<tr>
							<td colspan=4>&nbsp;</td>
						</tr>
						<tr id="TrMthYr">
							<td>Accounting Period : </td>
							<td colspan=3><asp:DropDownList id="lstAccMonth" size=1 width=15% runat=server>
												<asp:ListItem text="1" value="1" />
												<asp:ListItem text="2" value="2" />
												<asp:ListItem text="3" value="3" />
												<asp:ListItem text="4" value="4" />
												<asp:ListItem text="5" value="5" />
												<asp:ListItem text="6" value="6" />
												<asp:ListItem text="7" value="7" />
												<asp:ListItem text="8" value="8" />
												<asp:ListItem text="9" value="9" />
												<asp:ListItem text="10" value="10" />
												<asp:ListItem text="11" value="11" />
												<asp:ListItem text="12" value="12" />										
										  </asp:DropDownList>&nbsp;
										  <asp:DropDownList id="lstAccYear" size=1 width=15% runat=server /></td>
						</tr>	
						<tr>
							<td width=17%>Bank Code :* </td>
							<td width=39%>
								<asp:DropDownList id=ddlBankCode AutoPostBack=true onSelectedIndexChanged=IndexBankChanged width="100%" runat=server/>
								<asp:RequiredFieldValidator id=rfvBankCode display=dynamic runat=server 
									ErrorMessage="Please select one bank code" 
									ControlToValidate=ddlBankCode />
							</td>
							<td width=4%>&nbsp;</td>
							<td width=40%><asp:TextBox id=txtProgramPath visible=false maxlength=20 width=50% runat=server /></td>
						</tr>
						<tr>
							<td>Crediting Date :* </td>
							<td colspan=2><asp:TextBox id=txtCreditDate width=50% maxlength=10 runat=server />
								<a href="javascript:PopCal('txtCreditDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
								<asp:Label id=lblErrCreditDate visible=False forecolor=red text="<br>Invalid date format." runat=server /></td>
							<td><asp:RequiredFieldValidator id=rfvCreditDate display=dynamic runat=server 
									ErrorMessage="Please enter crediting date. " 
									ControlToValidate=txtCreditDate />			
							</td>
						</tr>
						<tr>
							<td colspan=4>&nbsp;</td>
						</tr>
						<tr>
							<td colspan="4"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					<table id=tblSave border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD BANK FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Payroll Bank Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">Steps:</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">2.&nbsp; Click "Save the file" to save the bank files into your external device (ie Diskette, CD-R)</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">
								<asp:Hyperlink id=lnkSaveTheFile text="Save the file" runat=server />
							</td>
						</tr>
					</table>
					<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
					<asp:Label id=lblDownloadfile visible=true runat=server />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
