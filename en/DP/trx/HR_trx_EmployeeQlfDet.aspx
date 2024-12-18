<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeQlfDet.aspx.vb" Inherits="HR_EmployeeQlfDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Qualification</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">


        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<table border=0 cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan=5>
						<UserControl:MenuHR id=MenuHR runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">EMPLOYEE QUALIFICATION</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Employee Code :</td>
					<td width=25%><asp:Label id=lblEmpCode runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=25%>Name :</td>
					<td width=25%><asp:Label id=lblEmpName runat=server /></td>
				</tr>
				<tr>
					<td>Qualification Code :*</td>
					<td>
						<asp:DropDownList id=ddlQualificationCode width=100% runat=server />
						<asp:Label id=lblErrQualification visible=false forecolor=red text="<br>Please select qualifcation." runat=server/>
					</td>
					<td colspan=2>&nbsp;</td>
					<td><asp:Label id=lblQualificationID visible=false runat=server /></td>
				</tr>
				<tr>
					<td>Subject Code :*</td>
					<td>
						<asp:DropDownList id=ddlSubjectCode width=100% runat=server />
						<asp:Label id=lblErrSubject visible=false forecolor=red text="<br>Please select subject." runat=server/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Remark :</td>
					<td colspan=4><asp:TextBox id=txtRemark maxlenght=64 width=100% runat=server /></td>
				</tr>
				<tr>
					<td height=25 colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save" onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" AlternateText="Delete" onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" onclick=btnBack_Click runat=server />
					</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
                                            &nbsp;</td>
				</tr>
				<tr id=TrLink runat=server>
					<td colspan=5>
						<asp:LinkButton id=lbDetails text="Employee Details" runat=server /> |
						<asp:LinkButton id=lbPayroll text="Employee Payroll" runat=server /> |
						<asp:LinkButton id=lbEmployment text="Employee Employment" runat=server /> |
						<asp:LinkButton id=lbStatutory text="Employee Statutory" runat=server /> |
						<asp:LinkButton id=lbFamily text="Employee Family" runat=server /> |
						<asp:LinkButton id=lbSkill text="Employee Skill" runat=server /> |
						<asp:LinkButton id=lbCareerProg text="Career Progress" runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
			</table>
			<asp:Label id=lblRedirect visible=false runat=server/>
			<asp:Label id=lblEmpStatus visible=false text=0 runat=server/>


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
