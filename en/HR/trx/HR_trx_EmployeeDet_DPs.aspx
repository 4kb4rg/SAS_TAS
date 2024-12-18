<%@ Page Language="vb"  %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>

                           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<table border=0 cellspacing="1" width="100%" class="font9Tahoma">
				<tr>
					<td   colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> EMPLOYEE DETAILS </strong> </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update :<asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdateBy runat=server />&nbsp;| Old Member Code :<asp:Label id=lblOldMemberCode runat=server />
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25>Member Code :*</td>
					<td width=25%>
						<asp:Textbox id=txtMemCode width=100% maxlength=25 runat=server CssClass="font9Tahoma" />
						<asp:RequiredFieldValidator id=revMemCode1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Member Code." 
							ControlToValidate=txtEmpCode />			
						<asp:Label id=lblErrMemCode visible=false forecolor=red text="<br>Please select/enter one Employee Code." runat=server/>
						<asp:Label id=lblErrDupMemCode visible=false forecolor=red text="<br>This employee code has been used. Please try another employee code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25>Employee Code :*</td>
					<td width=25%>
						<asp:DropDownList id=ddlEmpCode width=100% visible=false runat=server CssClass="font9Tahoma"/>
						<asp:Textbox id=txtEmpCode width=100% maxlength=25 runat=server CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=revEmpCode1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Employee Code." 
							ControlToValidate=txtEmpCode />			
						<asp:Label id=lblErrEmpCode visible=false forecolor=red text="<br>Please select/enter one Employee Code." runat=server/>
						<asp:Label id=lblErrDupEmpCode visible=false forecolor=red text="<br>This employee code has been used. Please try another employee code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Unit :*</td>
					<td><asp:DropDownList id=ddlUnit width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrUnit visible=false forecolor=red text="<br>Please select one Unit Code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Name :*</td>
					<td><asp:TextBox id=txtEmpName width=100% maxlength=64 runat=server CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=validateEmpName display=dynamic runat=server 
							ErrorMessage="<br>Please enter Employee Name." 
							ControlToValidate=txtEmpName />			
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Gender :*</td>
					<td><asp:DropDownList id=ddlSex width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrSex visible=false forecolor=red text="<br>Please select one Gender." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Place of Birth :</td>
					<td><asp:TextBox id=txtPOB width=100% runat=server CssClass="font9Tahoma"/>
					</td>
				</tr>
				<tr>
					<td height=25>Date of Birth :*</td>
					<td><asp:TextBox id=txtDOB width=50% runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtDOB');"><asp:Image id="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrDOB visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=validateDOB display=dynamic runat=server 
							ErrorMessage="<br>Please enter Date Of Birth." 
							ControlToValidate=txtDOB />			
					</td>
					<td>&nbsp;</td>
					<td height=25>Age :</td>
					<td><asp:TextBox id=txtAge width=20% runat=server CssClass="font9Tahoma"/></td>
				</tr>
				
				<tr>
					<td height=25>Start Working Date :*</td>
					<td><asp:TextBox id=txtWorkDate width=50% runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtWorkDate');"><asp:Image id="Image1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=Label1 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=RequiredFieldValidator1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Start Working Date." 
							ControlToValidate=txtWorkDate />			
					</td>
					<td>&nbsp;</td>
					<td height=25>Working Period :</td>
					<td><asp:TextBox id=txtWorkPeriod width=20% runat=server  CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td height=25>Membership Date :*</td>
					<td><asp:TextBox id=txtMemberDate width=50% runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtMemberDate');"><asp:Image id="Image2" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=Label2 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=RequiredFieldValidator2 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Membership Date." 
							ControlToValidate=txtMemberDate />			
					</td>
					<td>&nbsp;</td>
					<td height=25>Membership Period :</td>
					<td><asp:TextBox id=txtMemberPeriod width=20% runat=server CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td height=25>Promotion Date :</td>
					<td><asp:TextBox id=txtProDate width=50% runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtProDate');"><asp:Image id="Image3" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=Label3 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=RequiredFieldValidator3 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Membership Date." 
							ControlToValidate=txtProDate />			
					</td>
				</tr>
				<tr>
					<td height=25>Level/Golongan :*</td>
					<td><asp:DropDownList id=ddlGol width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrGol visible=false forecolor=red text="<br>Please select one Level/Golongan." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>PhDP :*</td>
					<td><asp:TextBox id=txtPhdp width=75% runat=server CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td height=25>Employee Contribution :</td>
					<td><asp:TextBox id=txtEmpeCo width=25% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>Employee Contribution Amount :</td>
					<td><asp:TextBox id=txtEmpeCoAmount width=75% runat=server CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td height=25>Employer Contribution :</td>
					<td><asp:TextBox id=txtEmprCo width=25% runat=server CssClass="font9Tahoma" /></td>
					<td>&nbsp;</td>
					<td>Employer Contribution Amount :</td>
					<td><asp:TextBox id=txtEmprCoAmount width=75% runat=server  CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Total Contribution Amount :</td>
					<td><asp:TextBox id=txtTotCoAmount width=75% runat=server CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>PERSONNAL INFORMATION</td>
				</tr>
				<tr>
					<td height=25>Marital Status :*</td>
					<td><asp:DropDownList id=ddlMarital width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrMarital visible=false forecolor=red text="<br>Please select one Marital Status." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Religion :</td>
					<td><asp:DropDownList id=ddlReligion width=100% runat=server CssClass="font9Tahoma"/></td>
				</tr>
				<tr>
					<td>Tax Status :*</td>
					<td><asp:DropDownList id=ddltaxstatus width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrTaxStatus visible=false forecolor=red text="<br>Please select one Tax Status." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Race :</td>
					<td><asp:DropDownList id=ddlRace width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrRace visible=false forecolor=red text="<br>Please select one Race." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>NPWP :</td>
					<td><asp:TextBox id=txtNPWP width=100% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>Nationality :</td>
					<td><asp:DropDownList id=ddlNation width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrNation visible=false forecolor=red text="<br>Please select one Nationality." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>I.C Type :</td>
					<td><asp:DropDownList id=ddlICType width=100% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				    <td valign=top>I.C Number :</td>
					<td valign=top><asp:TextBox id=txtNewICNo width=100% maxlength=18 runat=server CssClass="font9Tahoma" />
					</td>
				</tr>
				<tr>
				    <td height=25 valign=top>Residential Address :</td>
					<td valign=top>
						<textarea rows="6" id=txtResAddress width=100% cols="37" value="" runat=server></textarea>
						<asp:Label id=lblErrResAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td>&nbsp;</td>
					<td valign=top>IC Address :</td>
					<td valign=top>
						<textarea rows="6" id=txtICAddress width=100% cols="37" runat=server></textarea>
						<asp:Label id=lblErrICAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Residential Tel No :</td>
					<td><asp:TextBox id=txtResTel width=100% maxlength=15 runat=server CssClass="font9Tahoma"/>
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
					<td><asp:TextBox id=txtMobileTel width=100% maxlength=15 runat=server CssClass="font9Tahoma" />
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
					<td height=25 valign=top>Postal Address :</td>
					<td valign=top>
						<textarea rows="6" id=txtPostAddress width=100% cols="37" runat=server></textarea>
						<asp:Label id=lblErrPostAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
				</tr>
				
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25 colspan="5">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText="Delete"  runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="UnDelete"  runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" runat=server />
					    <br />
					</td>
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
        </div>
        </td>
        </tr>
        </table>
		</form>    
	</body>
</html>
