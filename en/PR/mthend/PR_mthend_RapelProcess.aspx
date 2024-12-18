<%@ Page Language="vb" src="../../../include/PR_mthend_RapelProcess.aspx.vb" Inherits="PR_mthend_RapelProcess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Rapel Process</title>
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
					<td colspan=4 class="mt-h" width="100%" >RAPEL PROCESS</td>
				</tr>	
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=4>
						<font color=red>Important notes before rapel process:</font><p> 
						Please ensure that Salary Adjustment Amount and Approval Month and Effective Month are completed before proceed.<br>
						Rapel for all or selected employee(s) will be updated according to the latest salary adjustment.<br>
					</td>
				</tr>				
				<tr>
					<td colspan=4 height=25>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Approval Month *:</td>
					<td><asp:DropDownList id=ddlAppMonth enabled=true width=35% autopostback=true onselectedindexchanged=onChange_AppMonth runat=server/>
						<asp:Label id=lblErrAppMonth visible=false forecolor=red text="<br>Please select one Approval Month." runat=server/>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Effective Month *:</td>
					<td><asp:DropDownList id=ddlEffMonth enabled=true width=35% autopostback=true onselectedindexchanged=onChange_EffMonth runat=server/>
						<asp:Label id=lblErrEffMonth visible=false forecolor=red text="<br>Please select one Effective Month." runat=server/>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25  width=20%>Process Type :</td>
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
					<td><asp:DropDownList id=ddlEmployee enabled=false width=35% runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
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
						<asp:Label id=lblSuccess text="Rapel has processed successfully." visible=false forecolor=blue runat=server />
						<asp:Label id=lblFailed text="Error while processing payroll. Kindly contact system administrator for assistance." visible=false forecolor=red runat=server />
						<asp:label id=lblErrCheckMonth forecolor=red visible=false text="Approval Month cannot be equal with Effective Month." runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>																
						<asp:ImageButton id=btnGenerate enabled=False onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
