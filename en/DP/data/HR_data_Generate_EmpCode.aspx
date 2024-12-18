<%@ Page Language="vb" src="../../../include/HR_data_Generate_EmpCode.aspx.vb" Inherits="HR_data_Generate_EmpCode" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRData" src="../../menu/menu_hrdata.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Generate Employee ID</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmPayroll runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=1 width=100%>
				<tr>
					<td colspan="2">
						<UserControl:MenuHRData id=MenuHRData runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan=2>GENERATE EMPLOYEE ID</td>
				</tr>
				<tr>
					<td colspan=2><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="100%" colspan="2">Human Resource Employee ID</td>
				</tr>
				<tr>
					<td width="100%" colspan="2">&nbsp;</td>
				</tr>
				<TR>
					<TD width="100%" colspan="2"><B><U>Steps to Generate Employee Code :</B></U></TD>
				</TR>
				<TR>
					<TD width="100%" colspan="2">1.&nbsp; Please enter the number of Employee ID to be generated.</TD>
				</TR>
				<TR>
					<TD width="100%" colspan="2">2.&nbsp; Click "Generate" button to create.</TD>
				</TR>
				<tr>
					<td width="100%" colspan="2">&nbsp;</td>
				</tr>
				<TR>
					<TD width="100%" colspan="2"><B><U>Steps to Download Employee Code :</B></U></TD>
				</TR>
				<TR>
					<TD width="100%" colspan="2">1.&nbsp; Please carry out steps 1 & 2 above.</TD>
				</TR>
				<TR>
					<TD width="100%" colspan="2">2.&nbsp; Click "Download" button to download.</TD>
				</TR>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td height=26 width=30%>Last Employee ID :</td>
					<td width=70%><asp:Label id=lblLastEmpId runat=server/>
						<asp:Label id=lblResult forecolor=blue runat=server/>
						<asp:Label id=lblResultText visible=false text=" Employee ID in total has been generated." runat=server/>&nbsp;</td>
				</tr>
				<tr>
					<td height=26>&nbsp;</td>
					<td><B>(Note : Last Employee ID shown here does not include Prefix(2 digits) + Year(2 digits).
										     Only Running Digit is shown here)</B></td>
				</tr>
				<tr>
					<td height=26>From :</td>
					<td><asp:DropDownList id=ddlLocCode autopostback=true onselectedindexchanged=OnChange_Location runat=server/>
						<asp:Label id=lblErrLoc visible=true forecolor=red text="Please select location." runat=server/></td>
				</tr>
				<tr>
					<td height=26>Number of ID to create :*</td>
					<td><asp:Textbox id=txtNumberId maxlength=5 width=20% runat=server/>
						<asp:RequiredFieldValidator id=rfvNumberId display=Dynamic runat=server 
							ErrorMessage="Please enter number of ID to be created."
							ControlToValidate=txtNumberId />
						<asp:RangeValidator id="RangeNumberId"
							ControlToValidate="txtNumberId"
							MinimumValue="1"
							MaximumValue="99999"
							Type="Integer"
							EnableClientScript="True"
							Text="The value must be from 1 to 99999"
							runat="server"/>
						<asp:CompareValidator id="rvNumberId" display=dynamic runat="server" 
							ControlToValidate="txtNumberId" Text="The value must whole number." 
							Type="Integer" Operator="DataTypeCheck"/>
					</td>
				</tr>				
				<tr>
					<td colspan="2">
						<asp:ImageButton id=GenerateBtn AlternateText="Generate" imageurl="../../images/butt_generate.gif" onclick=Button_Click CommandArgument=Generate runat=server /> 
						<asp:ImageButton id=DownloadBtn AlternateText="Download" CausesValidation=False imageurl="../../images/butt_download.gif" onclick=Button_Click CommandArgument=Download runat=server />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
