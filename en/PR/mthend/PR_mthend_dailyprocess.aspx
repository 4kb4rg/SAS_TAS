<%@ Page Language="vb" src="../../../include/PR_mthend_dailyprocess.aspx.vb" Inherits="PR_mthend_dailyprocess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Daily Process</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmMain runat=server>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan=4>
						<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
					</td>
				</tr>
				<tr>
					<td colspan=4 class="mt-h" width="100%" >DAILY PROCESS</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=4>
						<font color=red>Important notes before payroll process:</font><p> 
						1. Please backup up the database before proceed.<br>
						2. Ensure no user is in the system.<br>
					</td>
				</tr>
				<tr>
					<td colspan=4 height=25 visible=false>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 height=25 >
						<asp:CheckBox id=cbAC visible=True text=" Automatic calculating Employee Advance Salary and Catu Beras." checked=False runat=server /><br>
					</td> 
				</tr>
				<tr>
					<td colspan=4 height=25 visible=false>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=30%><asp:Label id=lblPeriod runat=server/></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr valign=Top>				
					<td height=25>Process Date :*</td>
					<td>
						<asp:Textbox id=txtProDate width=50% maxlength=10 runat=server/>
						<asp:Label id=lblErrProcessDate visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrProcessDateDesc visible=false text="<br>Date format " runat=server/>
						<a href="javascript:PopCal('txtProDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Minimum No. Of Harvester :*</td>
					<td><asp:textbox id=txtNoHarvester width=50% maxlength=2 Text=25 runat=server/>
						<asp:RequiredFieldValidator 
								id=rfvNoHarvester 
								display=dynamic runat=server 
								ErrorMessage="<br>Please enter minimun number of Harvester."
								ControlToValidate=txtNoHarvester />
						<asp:CompareValidator 
								id="cvNoHarvester" 
								display=dynamic 
								runat=server 
								ControlToValidate="txtNoHarvester" 
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
						<asp:Label id=lblSuccess text="Daily Payroll has processed successfully. You are now safe to proceed with another Process Date." visible=false forecolor=blue runat=server />
						<asp:Label id=lblFailed text="Error while processing payroll. Kindly contact system administrator for assistance." visible=false forecolor=red runat=server />
						<asp:Label id=lblAdvSalary text="This could be all employees's Advance Salary or Catu Beras have been processed previously or, there is no employee in the system." visible=false forecolor=red runat=server />
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
