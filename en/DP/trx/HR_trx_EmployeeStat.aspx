<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeStat.aspx.vb" Inherits="HR_trx_EmployeeStat" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Statutory</title>
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
			<table border=0 cellspacing="0" cellpadding=1 width="100%" class="font9Tahoma">
				<tr>
					<td colspan=5>
						<UserControl:MenuHR id=MenuHR runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">EMPLOYEE STATUTORY</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=15% height=25>Employee Code :</td>
					<td width=32%><asp:Label id=lblEmpCode runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Name :</td>
					<td width=30%><asp:Label id=lblEmpName runat=server /></td>
				</tr>
				<tr>
					<td colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td class="normalBold" height=25 colspan=5><asp:label id=lblJamsostek runat=server /></td>
							</tr>
							<tr>
								<td width=15% height=25>Reference No :*</td>
								<td width=32%>
										<asp:textbox id=txtJamsostekNo width=100% runat=server />
										<asp:RequiredFieldValidator id=rfvJamsostekNo
											display=Dynamic 
											runat=server
											ControlToValidate=txtJamsostekNo />
								</td>
								<td width=55% colspan=3>&nbsp;</td>
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
								<td width=50% class="normalBold" colspan=3 height=25><asp:label id=lblJKK runat=server /></td>
								<td width=15%>Reference No :</td>
								<td width=30%>
									<asp:TextBox id=txtJKKRefNo maxlength=20 width=100% runat=server />
								</td>
							</tr>
							<tr>
								<td width=15% height=25>Scheme Code : </td>
								<td width=32%><asp:DropDownList id=ddlJKKCode width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>ID :</td>
								<td width=30%>
									<asp:TextBox id=txtJKKID maxlength=20 width=100% runat=server />
									<asp:label id=lblErrJKKID text="Please enter ID. " forecolor=red visible=false runat=server />
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
								<td width=50% class="normalBold" colspan=3 height=25><asp:label id=lblJK runat=server /></td>
								<td width=15%>Reference No :</td>
								<td width=30%>
									<asp:TextBox id=txtJKRefNo maxlength=20 width=100% runat=server />
								</td>
							</tr>
							<tr>
								<td width=15% height=25>Scheme Code : </td>
								<td width=32%><asp:DropDownList id=ddlJKCode width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>ID :</td>
								<td width=30%>
									<asp:TextBox id=txtJKID maxlength=20 width=100% runat=server />
									<asp:label id=lblErrJKID text="Please enter ID. " forecolor=red visible=false runat=server />
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
								<td width=50% class="normalBold" colspan=3 height=25><asp:label id=lblJHT runat=server /></td>
								<td width=15%>Reference No :</td>
								<td width=30%>
									<asp:TextBox id=txtJHTRefNo maxlength=20 width=100% runat=server />
								</td>
							</tr>
							<tr>
								<td width=15% height=25>Scheme Code : </td>
								<td width=32%><asp:DropDownList id=ddlJHTCode width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>ID :</td>
								<td width=30%>
									<asp:TextBox id=txtJHTID maxlength=20 width=100% runat=server />
									<asp:label id=lblErrJHTID text="Please enter ID. " forecolor=red visible=false runat=server />
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
								<td width=50% class="normalBold" colspan=3 height=25><asp:label id=lblJPK runat=server /></td>
								<td width=15%>Reference No :</td>
								<td width=30%>
									<asp:TextBox id=txtJPKRefNo maxlength=20 width=100% runat=server />
								</td>
							</tr>
							<tr>
								<td width=15% height=25>Scheme Code : </td>
								<td width=32%><asp:DropDownList id=ddlJPKCode width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>ID :</td>
								<td width=30%>
									<asp:TextBox id=txtJPKID maxlength=20 width=100% runat=server />
									<asp:label id=lblErrJPKID text="Please enter ID. " forecolor=red visible=false runat=server />
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
								<td width=50% class="normalBold" colspan=3 height=25>Tax</td>
								<td width=15%>Reference No :</td>
								<td width=30%>
									<asp:TextBox id=txtTaxRefNo maxlength=20 width=100% runat=server />
									<asp:label id=lblErrTaxRefNo text="Please enter Reference No. " forecolor=red visible=false runat=server />
								</td>
							</tr>
							<tr>
								<td width=15% height=25>Scheme Code : </td>
								<td width=32%> <asp:DropDownList id=ddlTaxCode width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>Branch :</td>
								<td width=30%>
									<asp:DropDownList id=ddlTaxBranch width=100% runat=server />
									<asp:label id=lblErrTaxBranch text="Please select Tax Branch. " forecolor=red visible=false runat=server />
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
								<td width=50% class="normalBold" colspan=3 height=25>Levy</td>
							</tr>
							<tr>
								<td width=15% height=25>Port of Entry :</td>
								<td width=32%><asp:TextBox id=txtLevyPort maxlength=20 width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>Arrival Date :</td>
								<td width=30%><asp:TextBox id=txtLevyArriveDate maxlength=20 width=50% runat=server />
									<a href="javascript:PopCal('txtLevyArriveDate');"><asp:Image id="btnSelLevyArriveDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
									<asp:Label id=lblErrLevyArriveDate visible=False forecolor=red text="<br>Date format should be in " runat=server /></td>
							</tr>
							<tr>
								<td width=15% height=25>Lab Card No :</td>
								<td width=32%><asp:TextBox id=txtLevyCardNo maxlength=20 width=100% runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=20%>Immigration Ref :</td>
								<td width=30%><asp:TextBox id=txtLevyImgNo maxlength=20 width=100% runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save" onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" onclick=btnBack_Click runat=server />
					</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
                                            &nbsp;</td>
				</tr>
				<tr id=TrLink runat=server>
					<td colspan=5>
						<asp:LinkButton id=lbDetails text="Employee Details" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbPayroll text="Employee Payroll" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbEmployment text="Employee Employment" causesvalidation=false runat=server /> |
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
			<asp:label id=lblJKKCode visible=false runat=server/>
			<asp:label id=lblJKCode visible=false runat=server/>
			<asp:label id=lblJHTCode visible=false runat=server/>
			<asp:label id=lblJPKCode visible=false runat=server/>
			<asp:label id=lblSelect text="Select " visible=false runat=server/>
			<asp:label id=lblCode text=" Code" visible=false runat=server/>
			<asp:label id=lblPleaseEnterRefNo text="Please enter Reference No for " visible=false runat=server />


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
