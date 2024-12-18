<%@ Page Language="vb" src="../../../include/GL_data_download.aspx.vb" Inherits="GL_data_download" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLData" src="../../menu/menu_GLData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download References File</title>
		<Script language="JavaScript">
		function CA() {
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if ((e.name != 'cbAll') && (e.type=='checkbox'))
					e.checked = document.frmMain.cbAll.checked;
			}
		}

		function CCA() {
			var TotalBoxes = 0;
			var TotalOn = 0;
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if ((e.name != 'cbAll') && (e.type=='checkbox')) {
					TotalBoxes++;
					if (e.checked) {
						TotalOn++;
					}
				}
			}
			if (TotalBoxes==TotalOn)
				{document.frmMain.cbAll.checked=true;}
			else
				{document.frmMain.cbAll.checked=false;}
		}
		</Script>		
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
					<table id=tblDownload border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD REFERENCE FILE</td>
						</tr>
						<tr>
							<td colspan=4><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="4">General Ledger Reference Data</td>
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
							<td colspan=2><asp:CheckBox id=cbAccClsGrp text="Account Class Group" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbAccCls text="Account Class" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbActGrp text="Activity Group" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbAct text="Activity" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbSubAct text="Sub Activity" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbExp text="Expense Code" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbVehExpGrp text="Vehicle Expense Group Code" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbVehExp text="Vehicle Expense Code" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbVehType text="Vehicle Type" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbVeh text="Vehicle" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbBlkGrp text="Block Group" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbBlk text="Block" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbSubBlk text="Sub Block" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbAccGrp text="Chart of Account Group" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbAcc text="Chart of Account" onclick="javascript:CCA();" runat=server /></td>
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
							<td width="100%" colspan="4">General Ledger Reference Data</td>
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
