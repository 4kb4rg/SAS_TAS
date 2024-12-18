<%@ Page Language="vb" src="../../../include/PR_data_wages_download.aspx.vb" Inherits="PR_data_wages_download" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRData" src="../../menu/menu_PRData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download References File</title>
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
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD WAGES PAYMENT</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Payroll Wages Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<TR>
							<TD width="100%" colSpan="4">Steps:</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="4">1.&nbsp; Check below checkbox to export the data.</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="4">2.&nbsp; Click "Generate" button to generate the file.</TD>
						</TR>
						<TR>
							<TD width="5%">&nbsp;</TD>
							<TD width="5%">&nbsp;</TD>
							<TD width="55%">&nbsp;</TD>
							<TD width="35%">&nbsp;</TD>
						</TR>
						<tr>
							<td colspan=2>Period : </td>
							<td colspan=2><asp:Label id=lblWagesPeriod runat=server/></td>
						</tr>
						<tr>
							<td colspan="4">&nbsp;</td>
						</tr>
						<tr>
							<td colspan="4"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
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
