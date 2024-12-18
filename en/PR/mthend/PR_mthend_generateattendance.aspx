<%@ Page Language="vb" src="../../../include/PR_Trx_GenerateAttendance.aspx.vb" Inherits="PR_Trx_GenerateAttendance" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_MenuPRTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Generate Daily Attendance</title>
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
					<td colspan=4 class="mt-h" width="100%" >GENERATE DAILY ATTENDANCE</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=4>
						<font color=red>Important notes before generate daily attendance:</font><p> 
						1. Please backup up the database before proceed.<br>
						2. Ensure no user is in the system.<br>
					</td>
				</tr>
				<tr>
					<td colspan=4 height=25 visible=false>&nbsp;</td>
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
					<td height=25>Process Date From:*</td>
					<td>
						<asp:Textbox id=txtProDateFrom width=50% maxlength=10 runat=server/>
						<asp:Label id=lblErrProcessDateFrom visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrProcessDateDescFrom visible=false text="<br>Date format " runat=server/>
						<a href="javascript:PopCal('txtProDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../images/calendar.gif"/></a>
					</td>
					<td height=25>To:*</td>
					<td>
						<asp:Textbox id=txtProDateTo width=50% maxlength=10 runat=server/>
						<asp:Label id=lblErrProcessDateTo visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrProcessDateDescTo visible=false text="<br>Date format " runat=server/>
						<a href="javascript:PopCal('txtProDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../../images/calendar.gif"/></a>
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Department :*</td>
					<td>
					    <asp:dropdownlist id=ddlDeptCode width=100% runat=server />
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
