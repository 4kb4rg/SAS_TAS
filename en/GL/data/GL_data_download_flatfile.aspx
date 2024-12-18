<%@ Page Language="vb" src="../../../include/GL_data_Download_FlatFile.aspx.vb" Inherits="GL_Data_Download_FlatFile" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLData" src="../../menu/menu_GLData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download PeopleSoft ASCII Files</title>
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
					
					<table id=tblSelect border="0" cellpadding="1" cellspacing="0" width="100%" runat=server>	
						<TR>
							<TD width="100%" colSpan="4">Steps:</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="4">1.&nbsp; Choose Accounting Period, specify Company Code; Currency Code and Crop Code.</TD>
						</TR>						
						<TR>
							<TD width="100%" colSpan="4">2.&nbsp; Check below checkbox to export the data in ASCII format.</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="4">3.&nbsp; Click "Generate" button to generate the file.</TD>
						</TR>
						<TR>
							<TD width="5%">&nbsp;</TD>
							<TD width="5%">&nbsp;</TD>
							<TD width="55%">&nbsp;</TD>
							<TD width="35%">&nbsp;</TD>
						</TR>												
						<TR>
							<TD width=30%>Accounting Period :</TD>
							<TD>
								<asp:DropDownList id=ddlAccMonth runat=server />  /
								<asp:DropDownList id=ddlAccYear runat=server /> 
							</TD>
						</TR>
						
						<TR>
							<TD width=30%>Company Code :</TD>
							<TD>
								<asp:TextBox id=txtCompCode text = "MSC" maxlength=3 runat=server /> 
								<asp:Label id=lblErrCompCode visible=false forecolor=red text="Please enter Company Code." runat=server />
							</TD>
						</TR>
						
						<TR>
							<TD width=30%>Currency :</TD>
							<TD>
								<asp:TextBox id=txtCurrency text = "RM" maxlength=3 runat=server /> 
								<asp:Label id=lblErrCurrency visible=false forecolor=red text="Please enter Currency." runat=server />
							</TD>
						</TR>
						
						<TR>
							<TD width=30%>Crop Code :</TD>
							<TD>
								<asp:TextBox id=txtCropCode text = "O" maxlength=1 runat=server /> 
								<asp:Label id=lblErrCropCode visible=false forecolor=red text="Please enter Crop Code." runat=server />
							</TD>
						</TR>
						
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
					</table>
					
					<table id=tblDownload border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td>&nbsp;</td>
							<td colspan=3><asp:CheckBox id=cbAll onclick="javascript:CA();" text="All Files" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQORD text="Operational Detail - HQORD.DAT" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQORC text="Operational Detail Control - HQORC.DAT(YTD)" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQGLD text="General Ledger Detail - HQGLD.DAT" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQGLC text="General Ledger Detail Control - HQGLC.DAT(MTD)" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQXTD text="Estimate Detail - HQXTD.DAT" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQXTC text="Estimate Detail Control - HQXTC.DAT(12 months budget)" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQXRD text="Revised Budget Detail - HQXRD.DAT" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQXRC text="Revised Budget Detail Control - HQXRC.DAT(12 months period & amendment only)" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQOBD text="General Ledger Opening Balance Detail - HQOBD.DAT" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHQOBC text="General Ledger Opening Balance Control - HQOBC.DAT" onclick="javascript:CCA();" runat=server /></td>
						</tr>
						
						
						<tr>
							<td width="100%" colspan="4">&nbsp;</td>
						</tr>
						
						
						<tr>
							<td colspan=3>&nbsp;<asp:Label id=lblErrGenerate visible=false forecolor=red text="Please tick at least one checkbox." runat=server /></td>
						</tr>
						<tr>
							<td colspan="4"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
