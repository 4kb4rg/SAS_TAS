<%@ Page Language="vb" src="../../../include/PR_mthend_payrollprocess.aspx.vb" Inherits="PR_mthend_payrollprocess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Payroll Process</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan=4>
						<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
					</td>
				</tr>
				<tr>
					<td colspan=4 class="mt-h" width="100%" >PAYROLL PROCESS</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=4>
						<font color=red>Important notes before payroll process:</font><p> 
						1. Please backup up the database before proceed.<br>
						2. Ensure no user is in the system.<br>
						3. All the payroll transaction reports have been generated.<br>
						4. Automate the confirmation process for documents.<br>
						5. Please run Daily Process before payroll process.
					</td>
				</tr>
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 height=25>
						<asp:CheckBox id=cbAC text=" Automatic confirming all active Attendance Checkroll" checked=true runat=server /><br>
					</td> 
				</tr>
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=30%><asp:Label id=lblPeriod runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Process Type :</td>
					<td>
						<asp:RadioButton id="rbAllEmp" 
							Checked="True"
							GroupName="AllEmp"
							Text="All Employees"
							AutoPostBack=True
							OnCheckedChanged=Check_Clicked
							TextAlign="Right"
							runat="server" /><br>
						<asp:RadioButton id="rbSelectedEmp" 
							Checked="False"
							GroupName="AllEmp"
							Text="Selected Employee"
							AutoPostBack=True
							OnCheckedChanged=Check_Clicked
							TextAlign="Right"
							runat="server" />					
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Employee Code :</td>
					<td><asp:DropDownList id=ddlEmployee enabled=false width=100% runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>No. of work day per month :</td>
					<td><asp:textbox id=txtNoWorkDay width=50% maxlength=2 runat=server/>
						<asp:RequiredFieldValidator 
								id=rfvNoWorkDay 
								display=dynamic runat=server 
								ErrorMessage="<br>Please enter number of work day per month."
								ControlToValidate=txtNoWorkDay />
						<asp:CompareValidator 
								id="cvNoWorkDay" 
								display=dynamic 
								runat=server 
								ControlToValidate="txtNoWorkDay" 
								Text="<br>The value must be a whole number. " 
								Type="integer" 
								Operator="DataTypeCheck"/>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>
						<asp:label id=lblCompleteSetup text="yes" visible=false runat=server />
						<asp:label id=lblErrSetup forecolor=red visible=false text="Please complete Payroll Process Setup before proceed." runat=server />
						<asp:Label id=lblNoRecord text="No employee to be processed. This could be all employees have been processed previously or, there is no employee in the system." visible=false forecolor=red runat=server />
						<asp:Label id=lblErrModule text="There are some modules waiting for month end process before Payroll can be processed." visible=false forecolor=red runat=server />
						<asp:Label id=lblSuccess text="Payroll has processed successfully. You are now safe to proceed with Payroll Month End." visible=false forecolor=blue runat=server />
						<asp:Label id=lblFailed text="Error while processing payroll. Kindly contact system administrator for assistance." visible=false forecolor=red runat=server />
						<asp:Label id=lblErrDaily text="Payroll hasn't processed yet. Kindly run the daily process first" visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>																
						<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
