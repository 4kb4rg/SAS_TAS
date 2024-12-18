<%@ Page Language="vb" src="../../../include/NU_data_download.aspx.vb" Inherits="NU_data_download" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUData" src="../../menu/menu_NUData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download References File</title>
		<Script language="JavaScript">
		function CA() {
			for (var i=0;i<document.frmDownload.elements.length;i++) {
				var e = document.frmDownload.elements[i];
				if ((e.name != 'cbAll') && (e.type=='checkbox'))
					e.checked = document.frmDownload.cbAll.checked;
			}
		}

		function CCA() {
			var TotalBoxes = 0;
			var TotalOn = 0;
			for (var i=0;i<document.frmDownload.elements.length;i++) {
				var e = document.frmDownload.elements[i];
				if ((e.name != 'cbAll') && (e.type=='checkbox')) {
					TotalBoxes++;
					if (e.checked) {
						TotalOn++;
					}
				}
			}
			if (TotalBoxes==TotalOn)
				{document.frmDownload.cbAll.checked=true;}
			else
				{document.frmDownload.cbAll.checked=false;}
		}
		</Script>		
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%" align=center><UserControl:MenuNUData id=MenuNUData runat="server" /></td>
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
							<td width="100%" colspan="4">Nursery Reference Data</td>
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
							<td colspan=3><asp:CheckBox id=cbAll onclick="javascript:CA();" text="All Data" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbCullType onclick="javascript:CCA();" text="Culling Type" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbAccCls onclick="javascript:CCA();" text="Account Classification" runat=server /></td>
						</tr>
						<tr>
							<td colspan=4>&nbsp;<asp:Label id=lblErrGenerate visible=false forecolor=red text="Please tick at least one checkbox." runat=server /></td>
						</tr>
						<tr>
							<td colspan="4"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					<asp:Label id=lblDownloadfile visible=true runat=server />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
