<%@ Page Language="vb" src="../../../include/WS_data_download.aspx.vb" Inherits="WS_data_download" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSData" src="../../menu/menu_WSData.ascx"%>
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
				<td width="100%" align=center><UserControl:MenuWSData id=MenuWSData runat="server" /></td>
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
							<td width="100%" colspan="4">Workshop Reference Data</td>
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
							<td colspan=2><asp:CheckBox id=cbProdType onclick="javascript:CCA();" text="Product Type" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbProdBrand onclick="javascript:CCA();" text="Product Brand" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbProdModel onclick="javascript:CCA();" text="Product Model" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbProdCat onclick="javascript:CCA();" text="Product Category" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbProdMat onclick="javascript:CCA();" text="Product Material" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbStockAnalysis onclick="javascript:CCA();" text="Stock Analysis" runat=server /></td>
						</tr>
						<!-- PRM 21 Jul 2006 Millware 2.9 ; combine within stock and workshop, so have to remove Workshop Item Master from checklist. -->
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbItemPart onclick="javascript:CCA();" text="Workshop Item Parts" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbDirectChargeItem onclick="javascript:CCA();" text="Direct Charge Item Master" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbWorkCode onclick="javascript:CCA();" text="Work Code, Workshop Service Type" runat=server /></td>
						</tr>
						<tr>
							<td colspan=4>&nbsp;<asp:Label id=lblErrGenerate visible=false forecolor=red text="Please tick at least one checkbox." runat=server /></td>
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
							<td width="100%" colspan="4">Workshop Reference Data</td>
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
