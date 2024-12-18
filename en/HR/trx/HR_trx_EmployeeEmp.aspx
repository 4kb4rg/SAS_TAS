<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeEmp.aspx.vb" Inherits="HR_EmployeeEmp" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Employment</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
       <%--<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />--%>
	</head>
	<body class="font9Tahoma">
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=lblSelect visible=false text="Please Select " runat="server" />
			<asp:Label id=lblCode visible=false text=" Code" runat="server" />
			<table border=0 cellspacing="0" cellpadding=1 width="100%" class="font9Tahoma">
				<tr>
					<td colspan=5>
						<UserControl:MenuHR id=MenuHR runat="server" />
					</td>
				</tr>
				<tr>
					<td  colspan="5"><strong> EMPLOYEE EMPLOYMENT </strong></td>
				</tr>
				<tr>
					<td colspan=5>  <hr style="width :100%" /></td>
				</tr>
				<tr>
					<td width=20% style="height: 25px">Employee Code :</td>
					<td width=25% style="height: 25px"><asp:Label id=lblEmpCode runat=server /></td>
					<td width=5% style="height: 25px">&nbsp;</td>
					<td width=25% style="height: 25px">Name :</td>
					<td width=25% style="height: 25px"><asp:Label id=lblEmpName runat=server /></td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td width=20% class="normalBold" height=25>Appointment</td>
								<td width=25%><asp:Label id=lblStatus visible=false runat=server /></td>
								<td width=5%>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Date Joined :</td>
								<td><asp:Label id=lblAppJoinDate runat=server /></td>
								<td>&nbsp;</td>
								<td width=25%><asp:label id="lblCP1" runat="server" /> Code :</td>
								<td width=25%><asp:DropDownList id=ddlAppCPCode enabled=False width=100% runat=server /></td>
							</tr>
							<tr>
								<td height=25>Point of Hired :*</td>
								<td><asp:DropDownList id=ddlPOH enabled=False width=100% runat=server />
									<asp:RequiredFieldValidator id=validatePOH display=dynamic runat=server 
											ErrorMessage="<br>Please enter Point of Hired." 
											ControlToValidate=ddlPOH />		
								</td>			
								<td>&nbsp;</td>
								<td>Join Group Date :*</td>
								<td><asp:TextBox id=txtAppJoinGrpDate width=50% runat=server />
									<a href="javascript:PopCal('txtAppJoinGrpDate');"><asp:Image id="btnSelAppJoinGrpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
									<asp:Label id=lblErrAppJoinGrpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
									<asp:RequiredFieldValidator id=validateAppJoinGrpDate display=dynamic runat=server 
										ErrorMessage="<br>Please enter Join Group Date." 
										ControlToValidate=txtAppJoinGrpDate />			
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td width=20% class="normalBold" height=25>Last <asp:label id="lblCP4" runat="server" /> Status</td>
								<td width=25%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=25%><asp:label id="lblCP2" runat="server" /> Code :</td>
								<td width=25%><asp:DropDownList id=ddlLastCPCode enabled=False width=100% runat=server /></td>
							</tr>
							<tr>
								<td height=25>Date From :</td>
								<td><asp:Label id=lblLastCPDateFrom runat=server /></td>
								<td>&nbsp;</td>
								<td>Date To :</td>
								<td><asp:Label id=lblLastCPDateTo runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td width=20% class="normalBold" height=25>Termination</td>
								<td width=25%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=25%>&nbsp;</td>
								<td width=25%>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Date :</td>
								<td><asp:Label id=lblTerminateDate runat=server /></td>
								<td>&nbsp;</td>
								<td><asp:label id="lblCP3" runat="server" /> Code :</td>
								<td><asp:DropDownList id=ddlTerminateCPCode enabled=False width=100% runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr visible=false runat=server >
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray >
							<tr>
								<td width=20% class="normalBold" height=25>Increment Date</td>
								<td width=25%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=25%>&nbsp;</td>
								<td width=25%>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Last Increment :</td>
								<td><asp:Label id=lblLastIncDate runat=server /></td>
								<td>&nbsp;</td>
								<td>Next Increment :</td>
								<td><asp:Label id=lblNextIncDate runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblCompany" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlCompCode enabled=False width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>Employee Category :</td>
					<td><asp:DropDownList id=ddlSalSchemeCode enabled=False width=100% runat=server /></td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblLocation" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlLocCode enabled=False width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>Salary Grade :</td>
					<td><asp:DropDownList id=ddlSalGradeCode enabled=False width=100% runat=server /></td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblDepartment" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlDeptCode enabled=False width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>Black Listed Date :</td>
					<td><asp:Label id=lblBlackListDate runat=server /></td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblLevel" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlLevelCode width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>Probation Period :</td>
					<td><asp:Label id=lblProbation runat=server /></td>
				</tr>
				<tr>
					<td height=25>Report To :</td>
					<td><asp:DropDownList id=ddlRptTo width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>Confirmation Date :</td>
					<td><asp:Label id=lblConfirmDate runat=server /></td>
				</tr>
				<tr>
					<td height=25>Position :</td>
					<td><asp:DropDownList id=ddlPosCode enabled=False width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>Off Day :</td>
					<td><asp:TextBox id=txtOffDay width=50% runat=server /></td>
				</tr>
				<tr>
					<td height=25>Shift :</td>
					<td><asp:DropDownList id=ddlShift width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Kerani Type :</td>
					<td><asp:DropDownList id=ddlKeraniType width=100% runat=server />
					<asp:Label id=lblErrKeraniType visible=False forecolor=red text="Employee can either be Gang Leader or Kerani Member" runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>				
					<td height=25>Workshop Mechanic ?</td>
					<td><asp:CheckBox id=cbMechInd text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>				
					<td height=25>Gang Leader ?</td>
					<td><asp:CheckBox id=cbGangLeader text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td width=20% class="normalBold" height=25>Leave Entitlement</td>
								<td width=25%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=25%>&nbsp;</td>
								<td width=25%>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Holiday Schedule :</td>
								<td><asp:DropDownList id=ddlHolSchedule width=100% runat=server /></td>
								<td>&nbsp;</td>
								<td>B/F Leave :</td>
								<td><asp:TextBox id=txtBFLeave width=50% runat=server />
									<asp:Label id=lblBFLeave visible=False runat=server />
								</td>
							</tr>
							<tr>
								<td height=25>Sick Leave :</td>
								<td><asp:TextBox id=txtSickLeave width=50% runat=server />
									<asp:Label id=lblSickLeave visible=False runat=server />
								</td>
								<td>&nbsp;</td>
								<td>Annual Leave :</td>
								<td><asp:TextBox id=txtAnnualLeave width=50% runat=server />
									<asp:Label id=lblAnnualLeave visible=False runat=server />
								</td>
							</tr>
							<tr>
								<td height=25>Sick Leave Balance :</td>
								<td><asp:TextBox id=txtSickLeaveBalance width=50% runat=server />
									<asp:Label id=lblSickLeaveBalance visible=False runat=server />
								</td>
								<td>&nbsp;</td>
								<td>Annual Leave Balance :</td>
								<td><asp:TextBox id=txtAnnualLeaveBalance width=50% runat=server />
									<asp:Label id=lblAnnualLeaveBalance visible=False runat=server />
									
								</td>
							</tr>
							<tr>
								<td height=25 colspan=2><asp:Label id=lblErrSickLeaveBalance visible=False forecolor=red text="No more sick leave for this year" runat=server />
											<asp:Label id=lblErrSickLeaveBalanceAmt visible=False forecolor=red text="Sick leave balance must be less or equal to sick leave" runat=server />
											<asp:Label id=lblErrUpdateSick visible=False forecolor=red text="Only sick leave OR sick leave balance can be updated at one time" runat=server />
								</td>
								
								<td>&nbsp;</td>
								<td colspan=2><asp:Label id=lblErrAnnualLeaveBalance visible=False forecolor=red text="No more annual leave for this year" runat=server />
									<asp:Label id=lblErrBF visible=False forecolor=red text="B/F must be less or equal to annual leave balance" runat=server />
									<asp:Label id=lblErrAnnualLeaveBalanceAmt visible=False forecolor=red text="Annual leave balance must be less or equal to annual leave" runat=server />
									<asp:Label id=lblErrUpdateAnnual visible=False forecolor=red text="Only annual leave OR annual leave balance can be updated at one time" runat=server />
								</td>
								
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td width=20% class="normalBold" height=25>Insurance 1</td>
								<!--td width=25%><asp:CheckBox id=cbInsuranceInd text=" Yes" textalign=right runat=server /></td-->
								<td width=30%>&nbsp;</td>
								<td width=25%>Insurance Ref. No :</td>
								<td width=25%><asp:TextBox id=txtInsuranceNo1 width=100% runat=server /></td>
							</tr>
							<tr>
								<td height=25>Insurance Name :</td>
								<td colspan=4><asp:TextBox id=txtRemark1 width=100% runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray>
							<tr>
								<td width=20% class="normalBold" height=25>Insurance 2</td>
								<td width=30%>&nbsp;</td>
								<td width=25%>Insurance Ref. No :</td>
								<td width=25%><asp:TextBox id=txtInsuranceNo2 width=100% runat=server /></td>
							</tr>
							<tr>
								<td height=25>Insurance Name :</td>
								<td colspan=4><asp:TextBox id=txtRemark2 width=100% runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray>
							<tr>
								<td width=20% class="normalBold" height=25>Insurance 3</td>
								<td width=30%>&nbsp;</td>
								<td width=25%>Insurance Ref. No :</td>
								<td width=25%><asp:TextBox id=txtInsuranceNo3 width=100% runat=server /></td>
							</tr>
							<tr>
								<td height=25>Insurance Name :</td>
								<td colspan=4><asp:TextBox id=txtRemark3 width=100% runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5><asp:Label id=lblErrEmpValidation visible=False forecolor=red text="This employee not qualified to have annual and sick leave entitlement" runat=server /></td>
				</tr>
				<tr>
					<td height=25 colspan="5">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save" onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" onclick=btnBack_Click runat=server />
					    <br />
					</td>
				</tr>
				<tr id=TrLink runat=server>
					<td colspan=5>
						<asp:LinkButton id=lbDetails text="Employee Details" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbPayroll text="Employee Payroll" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbStatutory text="Employee Statutory" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbFamily text="Employee Family" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbQualific text="Employee Qualification" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbSkill text="Employee Skill" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbCareerProg text="Career Progress" causesvalidation=false runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
			</table>
			<asp:Label id=lblRedirect visible=false runat=server/>
			<asp:label id=lblEmpStatus visible=false runat=server />
        </div>
        </td>
        </tr>
        </table>
		</form>    
	</body>
</html>
