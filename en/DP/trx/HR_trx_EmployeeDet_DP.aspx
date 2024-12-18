<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeDet.aspx.vb" Inherits="HR_EmployeeDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Details</title>
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
			<table border=0 cellspacing="1" width="100%" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="5">EMPLOYEE DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Employee Code :*</td>
					<td width=25%>
						<asp:DropDownList id=ddlEmpCode width=100% runat=server />
						<asp:Textbox id=txtEmpCode width=100% maxlength=15 visible=false runat=server />
						<asp:RequiredFieldValidator id=revEmpCode1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Employee Code." 
							ControlToValidate=txtEmpCode />			
						<asp:Label id=lblErrEmpCode visible=false forecolor=red text="<br>Please select/enter one Employee Code." runat=server/>
						<asp:Label id=lblErrDupEmpCode visible=false forecolor=red text="<br>This employee code has been used. Please try another employee code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=25%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td height=25>Name :*</td>
					<td><asp:TextBox id=txtEmpName width=100% maxlength=64 runat=server />
						<asp:RequiredFieldValidator id=validateEmpName display=dynamic runat=server 
							ErrorMessage="<br>Please enter Employee Name." 
							ControlToValidate=txtEmpName />			
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td height=25>Gender :*</td>
					<td><asp:DropDownList id=ddlSex width=100% runat=server/>
						<asp:Label id=lblErrSex visible=false forecolor=red text="<br>Please select one Gender." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>Marital Status :*</td>
					<td><asp:DropDownList id=ddlMarital width=100% runat=server/>
						<asp:Label id=lblErrMarital visible=false forecolor=red text="<br>Please select one Marital Status." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
				</tr>
				<tr>
					<td height=25>I.C Number :</td>
					<td><asp:TextBox id=txtNewICNo width=100% maxlength=18 runat=server />
					</td>
					<td>&nbsp;</td>
					<td>I.C Type :</td>
					<td><asp:DropDownList id=ddlNewICType width=100% runat=server /></td>
				</tr>
				<tr>
					<td height=25>Old I.C Number :</td>
					<td><asp:TextBox id=txtOldICNo width=100% maxlength=8 runat=server /></td>
					<td>&nbsp;</td>
					<td>Old I.C Type :</td>
					<td><asp:DropDownList id=ddlOldICType width=100% runat=server /></td>
				</tr>
				<tr>
					<td height=25>Koperasi No :</td>
					<td><asp:TextBox id=txtKoperasiNo width=100% maxlength=20 runat=server /></td>
					<td>&nbsp;</td>
					<td>Koperasi ID :</td>
					<td><asp:DropDownList id=ddlKoperasiID width=100% runat=server /></td>
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
					<td>&nbsp;</td>
					<td>Race :*</td>
					<td><asp:DropDownList id=ddlRace width=100% runat=server />
						<asp:Label id=lblErrRace visible=false forecolor=red text="<br>Please select one Race." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Blood Type :</td>
					<td><asp:textbox id=txtBloodType maxlength=3 width=100% runat=server/>
						<asp:Label id=lblErrBloodType visible=false forecolor=red text="<br>Please key in Blood Type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblReligion" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlReligion width=100% runat=server /></td>
				</tr>
				<tr>	
					<td height=25>State :</td>
					<td><asp:TextBox id=txtState width=50% runat=server /></td>
					<td>&nbsp;</td>
					<td>Bumiputra :</td>
					<td><asp:CheckBox id=cbBumi text=" Yes" textalign=right runat=server /></td>
				</tr>
				<tr>
					<td height=25>Nationality :*</td>
					<td><asp:DropDownList id=ddlNation width=100% runat=server />
						<asp:Label id=lblErrNation visible=false forecolor=red text="<br>Please select one Nationality." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td height=25>Pensiun No :</td>
					<td><asp:TextBox id=txtPensionNo width=100% maxlength=20 runat=server /></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>Recruitment type :*</td>
					<td><asp:DropDownList id=ddlRecruitment width=100% runat=server />
						<asp:Label id=lblErrRecruitment visible=false forecolor=red text="<br>Please select one Recruitment Type." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Passport No :</td>
					<td><asp:TextBox id=txtPassportNo width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Passport Expiry Date :</td>
					<td><asp:TextBox id=txtPassportExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtPassportExpDate');"><asp:Image id="btnSelPassportExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrPassportExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Workpass No :</td>
					<td><asp:TextBox id=txtWorkpassNo width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Workpass Expiry Date :</td>
					<td><asp:TextBox id=txtWorkpassExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtWorkpassExpDate');"><asp:Image id="btnSelWorkpassExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrWorkpassExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Driving Class :</td>
					<td><asp:TextBox id=txtDriveCls width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Driving Expiry Date :</td>
					<td><asp:TextBox id=txtDriveExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtDriveExpDate');"><asp:Image id="btnSelDriveExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrDriveExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Other Class :</td>
					<td><asp:TextBox id=txtOpClass width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Other Expiry Date :</td>
					<td><asp:TextBox id=txtOpExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtOpExpDate');"><asp:Image id="btnSelOpExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrOpExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
				    </td>
				</tr>
				
				
				<tr>
					<td height=25>Batch No :</td>
					<td><asp:TextBox id=txtBatchNo width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Medical Report Expiry Date :</td>
					<td><asp:TextBox id=txtMRExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtMRExpDate');"><asp:Image id="btnSelMRExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrMRExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
				    </td>
				</tr>		
				<tr>
					<td height=25>Labour License No :</td>
					<td><asp:TextBox id=txtLabLicNo width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Labour License Expiry Date :</td>
					<td><asp:TextBox id=txtLCExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtLCExpDate');"><asp:Image id="btnSelLCExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrLCExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
				    </td>
				</tr>
				<tr>
					<td height=25>Insurace Policy No :</td>
					<td><asp:TextBox id=txtInsPolNo width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Insurance Policy Expiry Date :</td>
					<td><asp:TextBox id=txtIPExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtIPExpDate');"><asp:Image id="btnSelIPExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrIPExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
				    </td>
				</tr>
				<tr>
					<td height=25>Bank Guarantee No :</td>
					<td><asp:TextBox id=txtBankGuaNo width=100% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>Bank Guarantee Expiry Date :</td>
					<td><asp:TextBox id=txtBGExpDate width=50% runat=server />
						<a href="javascript:PopCal('txtBGExpDate');"><asp:Image id="btnSelBGExpDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrBGExpDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
				    </td>
				</tr>

				
				<tr>
					<td height=25 valign=top>Residential Address :</td>
					<td valign=top>
						<textarea rows="6" id=txtResAddress width=100% cols="27" value="" runat=server></textarea>
						<asp:Label id=lblErrResAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td>&nbsp;</td>
					<td valign=top>Postal Address :</td>
					<td valign=top>
						<textarea rows="6" id=txtPostAddress cols="27" runat=server></textarea>
						<asp:Label id=lblErrPostAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25 valign=top>IC Address :</td>
					<td valign=top>
						<textarea rows="6" id=txtICAddress cols="27" runat=server></textarea>
						<asp:Label id=lblErrICAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Residential Tel No :</td>
					<td><asp:TextBox id=txtResTel width=100% maxlength=15 runat=server />
							<asp:RegularExpressionValidator id="revResNo" 
							ControlToValidate="txtResTel"
							ValidationExpression="[\d\-\(\)]{1,15}"
							Display="dynamic"
							ErrorMessage="Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Mobile Phone No :</td>
					<td><asp:TextBox id=txtMobileTel width=100% maxlength=15 runat=server />
							<asp:RegularExpressionValidator id="revMobileNo" 
							ControlToValidate="txtMobileTel"
							ValidationExpression="[\d\-\(\)]{1,15}"
							Display="dynamic"
							ErrorMessage="Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td>Tax Status :</td>
					<td><asp:DropDownList id=ddltaxstatus width=100% runat=server />
						<asp:Label id=lblErrTaxStatus visible=false forecolor=red text="<br>Please select one Tax Status." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Allowance Status :</td>
					<td><asp:DropDownList id=ddlallwstatus width=100% runat=server />
						<asp:Label id=lblErrAllwStatus visible=false forecolor=red text="<br>Please select one Allowance status to calculate catu beras." runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save" onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText="Delete" onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="UnDelete" onclick=btnUnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" onclick=btnBack_Click runat=server />
					</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
                                            &nbsp;</td>
				</tr>
				<tr id=TrLink runat=server>
					<td colspan=5>
						<asp:LinkButton id=lbPayroll text="Employee Payroll" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbEmployment text="Employee Employment" causesvalidation=false runat=server /> |
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
			<input type=hidden id=EmpCode value='' runat=server />
			<input type=hidden id=hidEmpName value='' runat=server />
			<input type=hidden id=hidStatus value=0 runat=server/>
			<asp:Label id=lblRedirect visible=false runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
