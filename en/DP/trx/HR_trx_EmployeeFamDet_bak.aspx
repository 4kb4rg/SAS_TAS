<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeFamDet.aspx.vb" Inherits="HR_EmployeeFamDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Family</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu"> 

        <table cellpadding="0" cellspacing="0" style="width: 100%"  >
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
					<td class="mt-h" colspan="5">EMPLOYEE FAMILY</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Employee Code :</td>
					<td width=25%><asp:Label id=lblEmpCode runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Name :</td>
					<td width=30%><asp:Label id=lblEmpName runat=server /></td>
				</tr>
				<tr>
					<td height=25>Member Name :*</td>
					<td><asp:TextBox id=txtFamName maxlength=64 width=100% runat=server />
						<asp:RequiredFieldValidator id=validateFamName display=dynamic runat=server 
							ErrorMessage="<br>Please enter Member Name." 
							ControlToValidate=txtFamName />	
					</td>		
					<td>&nbsp;</td>
					<td>Member Employee Code :</td>
					<td><asp:DropDownList id=ddlFamEmpCode width=80% runat=server /> 
						<input type="button" id=btnFindEmp value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlFamEmpCode','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Sex :*</td>
					<td><asp:DropDownList id=ddlSex width=100% runat=server/>
						<asp:Label id=lblErrSex visible=false forecolor=red text="<br>Please select one Gender." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=lblFamilyID visible=false runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Relationship :*</td>
					<td><asp:DropDownList id=ddlRelationship width=100% runat=server/>
						<asp:Label id=lblErrRelationship visible=false forecolor=red text="<br>Please select one Relationship." runat=server/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Date of Birth :*</td>
					<td><asp:TextBox id=txtDOB width=50% runat=server />
						<a href="javascript:PopCal('txtDOB');"><asp:Image id="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrDOB visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=validateDOB display=dynamic runat=server 
							ErrorMessage="<br>Please enter Date Of Birth." 
							ControlToValidate=txtDOB />			
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Telephone No :</td>
					<td><asp:TextBox id=txtTelNo width=100% maxlength=14 runat=server />
						<asp:CompareValidator id="validateIntTelNo" display=dynamic runat="server" 
							ControlToValidate="txtTelNo" Text="The value must whole number." 
							Type="integer" Operator="DataTypeCheck"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Emergency Contact ?</td>
					<td><asp:CheckBox id=cbEmergContactInd text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Working ?</td>
					<td><asp:CheckBox id=cbWorkInd text=" Yes" textalign=right runat=server /></td>
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
			</table>
			<asp:Label id=lblRedirect visible=false runat=server/>
			<asp:label id=lblEmpStatus visible=false runat=server />
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
