<%@ Page Language="vb" src="../../../include/GL_Data_Download_FlatFile_SaveTo.aspx.vb" Inherits="GL_Data_Download_FlatFile_SaveTo" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLData" src="../../menu/menu_GLData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download PeopleSoft ASCII Files</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%" align=center><UserControl:MenuGLData id=MenuGLData runat="server" /></td>
			</tr>
			<tr>
				<td width="100%">
				<form id=frmMain runat=server>
					
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD PEOPLESOFT ASCII FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">PMIS Flat File Generation</td>
						</tr>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
					
						<tr>
							<td width="100%" colspan="4">Please Right Click on the hyperlink to save the file.</td>
						</tr>
													
						<TR>
							<TD width="100%">List of files generated:</TD>
						</TR>
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						
						<td colspan=2 width=100%>
						
						<table id=tblFileList border="0" cellpadding="1" cellspacing="0" width="100%" runat=server>	
						
						<tr>
							<td colspan=5 width="50%">1 . Operational Detail</td>
							<td colspan=4><asp:LinkButton id=lbHQORD text = "HQORD.DAT" HRef="../../../ftp/HQORD.DAT" runat=server /></td>
						</tr>
						<tr>
							<td colspan=5 width="50%">2 . Operational Detail Control(YTD)</td>
							<td colspan=4><asp:LinkButton id=lbHQORC text = "HQORC.DAT" HRef="../../../ftp/HQORC.DAT" runat=server /></td>
						</tr>
						<tr>
							<td colspan=5 width="50%">3 . General Ledger Detail</td>
							<td colspan=4><asp:LinkButton id=lbHQGLD text = "HQGLD.DAT" HRef="../../../ftp/HQGLD.DAT" runat=server /></td>	
						</tr>
						<tr>
							<td colspan=5 width="50%">4 . General Ledger Detail Control (MTD)</td>
							<td colspan=4><asp:LinkButton id=lbHQGLC text = "HQGLC.DAT" HRef="../../../ftp/HQGLC.DAT" runat=server /></td>	
						</tr>
						<tr>
							<td colspan=5 width="50%">5 . Estimate Detail</td>
							<td colspan=4><asp:LinkButton id=lbHQXTD text = "HQXTD.DAT" HRef="../../../ftp/HQXTD.DAT" runat=server /></td>	
						</tr>
						<tr>
							<td colspan=5 width="50%">6 . Estimate Detail Control (12 months budget)</td>
							<td colspan=4><asp:LinkButton id=lbHQXTC text = "HQXTC.DAT" HRef="../../../ftp/HQXTC.DAT" runat=server /></td>	
						</tr>
						<tr>
							<td colspan=5 width="50%">7 . Revised Budget Detail</td>
							<td colspan=4><asp:LinkButton id=lbHQXRD text = "HQXRD.DAT" HRef="../../../ftp/HQXRD.DAT" runat=server /></td>
						</tr>
						<tr>
							<td colspan=5 width="50%">8 . Revised Budget Detail Control (12 months period & amendment only)</td>
							<td colspan=4><asp:LinkButton id=lbHQXRC text = "HQXRC.DAT" HRef="../../../ftp/HQXRC.DAT" runat=server /></td>
						</tr>
						<tr>
							<td colspan=5 width="50%">9 . General Ledger Opening Balance Detail</td>
							<td colspan=4><asp:LinkButton id=lbHQOBD text = "HQOBD.DAT" HRef="../../../ftp/HQOBD.DAT" runat=server /></td>
						</tr>
						<tr>
							<td colspan=5 width="50%">10. General Ledger Opening Balance Control</td>
							<td colspan=4><asp:LinkButton id=lbHQOBC text = "HQOBC.DAT" HRef="../../../ftp/HQOBC.DAT" runat=server /></td>
						</tr>
					
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
								
					</table>
				</td>
		</form>
	</body>
</html>
