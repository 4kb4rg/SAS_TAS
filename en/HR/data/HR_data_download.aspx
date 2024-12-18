<%@ Page Language="vb" src="../../../include/HR_data_download.aspx.vb" Inherits="HR_data_download" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRData" src="../../menu/menu_HRData.ascx"%>
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
				<td width="100%" align=center><UserControl:MenuHRData id=MenuHRData runat="server" /></td>
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
							<td width="100%" colspan="4">Human Resource Reference Data</td>
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
							<td colspan=2><asp:CheckBox id=cbEmpCode onclick="javascript:CCA();" text="Employee Code" forecolor=red runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbDeptCode onclick="javascript:CCA();" text="Department Code, Department" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbNationality onclick="javascript:CCA();" text="Nationality" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbFunction onclick="javascript:CCA();" text="Function, Position" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbLevel onclick="javascript:CCA();" text="Level" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbReligion onclick="javascript:CCA();" text="Religion" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbICType onclick="javascript:CCA();" text="IC Type" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbRace onclick="javascript:CCA();" text="Race" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbSkill onclick="javascript:CCA();" text="Skill" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbShift onclick="javascript:CCA();" text="Shift" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbQualification onclick="javascript:CCA();" text="Qualification" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbSubject onclick="javascript:CCA();" text="Subject" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbEvaluation onclick="javascript:CCA();" text="Evaluation" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbCP onclick="javascript:CCA();" text="Career Progress" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbSalScheme onclick="javascript:CCA();" text="Salary Scheme, Salary Grade" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbBank onclick="javascript:CCA();" text="Bank Format, Bank" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbTax onclick="javascript:CCA();" text="Tax, Tax Branch" runat=server /></td>
						</tr>
						<tr>
							<td colspan=2>&nbsp;</td>
							<td colspan=2><asp:CheckBox id=cbHoliday onclick="javascript:CCA();" text="General Public Holiday, Holiday Schedule" runat=server /></td>
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
