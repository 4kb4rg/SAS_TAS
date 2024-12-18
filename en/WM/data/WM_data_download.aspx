<%@ Page Language="vb" src="../../../include/WM_data_download.aspx.vb" Inherits="WM_data_download" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMData" src="../../menu/menu_WMData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download References File</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%" align=center><UserControl:MenuWMData id=MenuWMData runat="server" /></td>
			</tr>
			<tr>
				<td width="100%">
					<form id=frmDownload runat=server>
					<table id=tblDownload border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD REFERENCE FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Weighing Management Reference Data</td>
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
							<td>&nbsp;</td>
							<td colspan=3><asp:CheckBox id=cbTransporter text="Transporter" runat=server /></td>
						</tr>
						<tr>
							<td colspan=3>&nbsp;<asp:Label id=lblErrGenerate visible=false forecolor=red text="Please tick at least one checkbox." runat=server /></td>
						</tr>
						<tr>
							<td colspan="4"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					<table id=tblSave border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD REFERENCE FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">Weighing Management Reference Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<TR>
							<TD width="100%" colSpan="4">Steps:</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="4">3.&nbsp; Click "Save the file" to save the reference files into your external device (ie Diskette, CD-R)</TD>
						</TR>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						<TR>
							<TD width="100%" colSpan="4">
								<asp:Hyperlink id=lnkSaveTheFile text="Save the file" runat=server />
							</TD>
						</TR>
					</table>
					<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
					<asp:Label id=lblDownloadfile visible=true runat=server />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
