<%@ Page Language="vb" src="../../../include/PR_data_download_statutory.aspx.vb" Inherits="PR_data_download_statutory" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRData" src="../../menu/menu_PRData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download Statutory File</title>
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
					<table id=tblDownload border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD STATUTORY FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Payroll Statutory Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">Steps:</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">1.&nbsp; Check below checkbox to export the data.</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">2.&nbsp; Click "Generate" button to generate the file.</td>
						</tr>
						<tr>
							<td width="25%">&nbsp;</td>
							<td width="25%">&nbsp;</td>
							<td width="25%">&nbsp;</td>
							<td width="25%">&nbsp;</td>
						</tr>
						<tr id="TrMthYr">
							<td>Accounting Period : </td>
							<td colspan=2><asp:DropDownList id="lstAccMonth" size=1 width=15% runat=server>
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
							<td colspan=4>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td colspan=3 align="left" valign=top>
								<asp:radiobuttonlist id=rdStatutory onSelectedIndexChanged=RadioButtonChanged AutoPostBack=true runat=server>
									<asp:ListItem id=rdEPF runat=server value="1" text="EPF - Borang A" selected=true />
									<asp:ListItem id=rdTAX runat=server value="2" text="TAX - Borang CP39"/>
									<asp:ListItem id=rdSOCSO runat=server value="3" text="SOCSO - Borang 8A"/>
								</asp:radiobuttonlist>
							</td>
						</tr>
						<tr>
							<td colspan=4>&nbsp;</td>
						</tr>
						<tr>		
							<td><asp:Label id=lblTaxBranch runat=server text="Tax Branch : " visible=false /></td>
							<td><asp:DropDownList id=ddlTaxBranch width=100% visible=false runat=server /></td>
						</tr>
						<tr>
							<td colspan=4>&nbsp;<asp:Label id=lblErrGenerate visible=false forecolor=red text="Please tick at least one button." runat=server /></td>
						</tr>
						<tr>
							<td colspan="4"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					<table id=tblSave border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD STATUTORY FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Payroll Statutory Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">Steps:</td>
						</tr>
						<tr>
							<td width="100%" colSpan="4">3.&nbsp; Click "Save the file" to save the statutory files into your external device (ie Diskette, CD-R)</td>
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
					<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
					<asp:Label id=lblDownloadfile visible=true runat=server />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
