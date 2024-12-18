<%@ Page Language="vb" src="../../../include/HR_trx_EmployeePay.aspx.vb" Inherits="HR_trx_EmployeePay" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Payroll</title>
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
			<table border=0 cellspacing="1" cellpadding=1 width="100%" class="font9Tahoma">
				<tr>
					<td colspan=5><UserControl:MenuHR id=MenuHR runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">EMPLOYEE PAYROLL</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=23% height=25>Employee Code :</td>
					<td width=30%><asp:Label id=lblEmpCode runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=17%>Name :</td>
					<td width=25%><asp:Label id=lblEmpName runat=server /></td>
				</tr>
				<tr>
					<td height=25>Pay Type :</td>
					<td><asp:DropDownList id=ddlPayType enabled=false width=100% runat=server />
						<asp:Label id=lblErrPayType visible=false forecolor=red text="<br>Please select one Pay Type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Pay Rate :</td>
					<td><asp:Label id=lblPayRate runat=server /></td>
				</tr>
				<tr>
					<td height=25>Pay Mode :</td>
					<td><asp:DropDownList id=ddlPayMode AutoPostBack=True OnSelectedIndexChanged=CallCheckPayMode width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td><asp:label id=lblRiceRation runat=server/> :</td>
					<td><asp:label id=lblRiceRationValue text=0 runat=server/></td>
				</tr>
				<tr>
					<td height=25>					
					<b>Bank 1</b>
					</td>
					<td>&nbsp;</td>		
					<td>&nbsp;</td>				
					<td colspan=2><b><u>Month To Date </u></b></td>
				</tr>
				<tr>
					<td height=25>Bank Code :</td>
					<td><asp:DropDownList id=ddlBankCode1 width=100% runat=server />
						<asp:Label id=lblErrBankInfo1 forecolor=red visible=false text="Please select a bank and fill up bank account number." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Allowance :</td>
					<td><asp:Label id=lblAllowance text=0 runat=server /></td>
				</tr>
				<tr>
					<td height=25>Bank Account No : </td>
					<td><asp:textbox id=txtBankAccNo1 maxlength=32 width=100% runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Deduction :</td>
					<td><asp:Label id=lblDeduction text=0 runat=server /></td>				
				</tr>
				<tr>
					<td height=25>Portion Rate : </td>
					<td><asp:textbox id=txtPortionRate1 maxlength=3 width=100% runat=server />
						<asp:RegularExpressionValidator id="RegularExpressionValidatorPortionRate1" 
								ControlToValidate="txtPortionRate1"
								ValidationExpression="\d{1,3}"
								Display="Dynamic"
								text = "<br>Maximum length 3 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator 
								id="Range1"
								ControlToValidate="txtPortionRate1"
								MinimumValue="0"
								MaximumValue="100"
								Type="Integer"
								EnableClientScript="True"
								Text="The value is out of acceptable range (0-100)"
								runat="server" display="dynamic"/>								

					</td>
					<td>&nbsp;</td>
					<td colspan=2><b><u>Quota Details </u></b></td>									
				</tr>
				<tr>
					<td height=25>Portion Amount : </td>
					<td><asp:textbox id=txtPortionAmount1 maxlength=32 width=100% runat=server />
					<asp:Label id=lblErrEmpty1 forecolor=red visible=false text="Bank Code has been selected. Please enter portion rate or portion amount." runat=server /></td>
					<td>&nbsp;</td>
					<td>Quota Level :</td>
					<td><asp:label id=lblQuotaLevelDesc runat=server/></td>								
				</tr>
				
				<tr>
					<td height=25>Account Holder : </td>
					<td><asp:textbox id=txtAccHdl1 maxlength=32 width=100% runat=server />
					<asp:Label id=lblErrAccHdlEmpty1 forecolor=red visible=false text="Please fill up account holder." runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				
				<tr>
					<td height=25><b>Bank 2</b></td>
					<td>&nbsp;</td>		
					<td>&nbsp;</td>				
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Bank Code :</td>
					<td><asp:DropDownList id=ddlBankCode2 width=100% runat=server />
						<asp:Label id=lblErrBankInfo2 forecolor=red visible=false text="Please select first bank and fill up first bank account number." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					
				</tr>
				<tr>
					<td height=25>Bank Account No : </td>
					<td><asp:textbox id=txtBankAccNo2 maxlength=32 width=100% runat=server />
					<asp:Label id=lblErrAccEmpty2 forecolor=red visible=false text="Please fill up bank account number." runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td height=25>Portion Rate : </td>
					<td><asp:textbox id=txtPortionRate2 maxlength=3 width=100% runat=server />
							<asp:RegularExpressionValidator id="RegularExpressionValidatorPortionRate2" 
								ControlToValidate="txtPortionRate2"
								ValidationExpression="\d{1,3}"
								Display="Dynamic"
								text = "<br>Maximum length 3 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator 
								id="Range2"
								ControlToValidate="txtPortionRate2"
								MinimumValue="0"
								MaximumValue="100"
								Type="Integer"
								EnableClientScript="True"
								Text="The value is out of acceptable range (0-100)"
								runat="server" display="dynamic"/>		
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>
				<tr>
					<td height=25>Portion Amount : </td>
					<td><asp:textbox id=txtPortionAmount2 maxlength=32 width=100% runat=server /><asp:Label id=lblErrEmpty2 forecolor=red visible=false text="Bank Code has been selected. Please enter portion rate or portion amount." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>							
				</tr>
				
				<tr>
					<td height=25>Account Holder : </td>
					<td><asp:textbox id=txtAccHdl2 maxlength=32 width=100% runat=server />
					<asp:Label id=lblErrAccHdlEmpty2 forecolor=red visible=false text="Please fill up account holder." runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				
				
				<tr>
					<td height=25><b>Bank 3</b></td>
					<td>&nbsp;</td>		
					<td>&nbsp;</td>				
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Bank Code :</td>
					<td><asp:DropDownList id=ddlBankCode3 width=100% runat=server />
						<asp:Label id=lblErrBankInfo3 forecolor=red visible=false text="Please select second bank and fill up second bank account number." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					
				</tr>
				<tr>
					<td height=25>Bank Account No : </td>
					<td><asp:textbox id=txtBankAccNo3 maxlength=32 width=100% runat=server />
					<asp:Label id=lblErrAccEmpty3 forecolor=red visible=false text="Please fill up bank account number." runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td height=25>Portion Rate : </td>
					<td><asp:textbox id=txtPortionRate3 maxlength=3 width=100% runat=server />
							<asp:RegularExpressionValidator id="RegularExpressionValidatorPortionRate3" 
								ControlToValidate="txtPortionRate3"
								ValidationExpression="\d{1,3}"
								Display="Dynamic"
								text = "<br>Maximum length 3 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator 
								id="Range3"
								ControlToValidate="txtPortionRate3"
								MinimumValue="0"
								MaximumValue="100"
								Type="Integer"
								EnableClientScript="True"
								Text="The value is out of acceptable range (0-100)"
								runat="server" display="dynamic"/>		
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>
				<tr>
					<td height=25>Portion Amount : </td>
					<td><asp:textbox id=txtPortionAmount3 maxlength=32 width=100% runat=server /><asp:Label id=lblErrEmpty3 forecolor=red visible=false text="Bank Code has been selected. Please enter portion rate or portion amount." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>							
				</tr>
				
				<tr>
					<td height=25>Account Holder : </td>
					<td><asp:textbox id=txtAccHdl3 maxlength=32 width=100% runat=server />
					<asp:Label id=lblErrAccHdlEmpty3 forecolor=red visible=false text="Please fill up account holder." runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				
				<tr>
				<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehCode" runat="server" /> :</td>
					<td>
						<asp:DropDownList id=ddlVehCode width=87% runat=server /> 
						<input type="button" id=btnFind value=" ... " onclick="javascript:findcode('frmMain','','','','','','ddlVehCode','','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Deferred Payment : </td>
					<td><asp:CheckBox id=cbDeferPayInd text=" Yes" textalign=right runat=server /></td>
					<td>&nbsp;</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Deferred Remark : </td>
					<td><asp:TextBox id=txtDeferRemark maxlength=64 width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>

				</tr>
				<tr id="TrTranInc" runat="server">
					<td height=25><asp:label id=lblTranIncCode runat=server/> :</td>
					<td><asp:DropDownList id=ddlTranIncCode width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="TrHarvInc" runat="server">
					<td height=25>Harvesting Incentive Code :</td>
					<td><asp:DropDownList id=ddlHarvIncCode width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td><asp:label id=lblIndQuotaTag text="Daily Quota :" runat=server/></td>
					<td><asp:label id=lblIndQuotaValue text="0" runat=server /></td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblRiceCode runat=server/> :</td>
					<td><asp:dropdownlist id=ddlRiceRation width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td><asp:label id=lblIndQuotaMethodTag text="Quota is measured by :" runat=server /></td>
					<td><asp:label id=lblIndQuotaMethodDesc runat=server/></td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblIncentiveCode runat=server/> :</td>
					<td><asp:dropdownlist id=ddlIncentive width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td><asp:label id=lblQuotaIncRateTag runat=server /></td>
					<td><asp:label id=lblIndQuotaIncRate text=0 runat=server/></td>
				</tr>
				<tr>
					<td height=25>Denda Code :</td>
					<td><asp:dropdownlist id=ddlDenda width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Default Attendance :</td>
					<td colspan="3"><asp:dropdownlist id=ddlDefAccCode onSelectedIndexChanged=onSelect_DefAccount width=90% runat=server /></td>
					<td><asp:Label id=lblErrDefAccCode visible=false 
					forecolor=red text="<br>Please select one" runat=server/>
                    </td>
				</tr>
				<tr>
					<td height=25>Default Charge :</td>
					<td><asp:dropdownlist id=ddlDefBlkCode width=100% runat=server /></td>
					<td colspan="3"><asp:Label id=lblErrDefBlkCode visible=false forecolor=red 
					    text="<br>Please select one" runat=server/>
                    </td>
				</tr>
				
				<tr>
					<td height=25>Levy :</td>
					<td><asp:CheckBox id=cbLevyInd text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Overtime :</td>
					<td><asp:CheckBox id=cbOTInd text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<!--tr>
					<td height=25>Attendance Incentive Code :</td>
					<td><asp:DropDownList id=ddlAttdIncCode width=100% runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr-->
				<tr>
					<td height=25>Deduction for Absent Day(s)? </td>
					<td><asp:checkbox id=cbAbsDeductInd text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Incentive and Overtime Claim is paid on 15th of subsequent month :</td>
					<td><asp:checkbox id=cbTwicePayInd text=" Yes" textalign=right runat=server /></td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Payslip Addition Note :</td>
					<td colspan=4><asp:TextBox id=txtPayslipNote maxlength=64 width=100% runat=server /></td>
				</tr>
				<tr>
					<td colspan=5><asp:Label id=lblErrPortion forecolor=red visible=false text="<br>Total Portion Rate must be 100%." runat=server />
					<asp:Label id=lblErrRateAmount forecolor=red visible=false text="<br>Please enter portion rate or portion amount only." runat=server />
					</td>
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
			<asp:Label id=lblRedirect visible=false runat=server/>
			<asp:Label id=lblEmpStatus text="0" visible=false runat=server />
			<asp:label id="lblSelect" visible=false text="Please select " runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblNotApplicable visible=false text="Not Applicable" runat=server/>
			<asp:label id=lblActivity visible=false runat=server/>
			<asp:label id=lblBlock visible=false runat=server/>
			<asp:label id=lblIndividual text="Individual" visible=false runat=server/>
			<asp:label id=lblQuotaLevelValue text="1" visible=false runat=server/>
			<asp:label id=lblIndQuotaMethodValue text="1" visible=false runat=server/>
			<asp:label id=lblPayRateHid visible=false runat=server/>
			<asp:label id=lblAllowanceHid visible=false runat=server/>
			<asp:label id=lblDeductionHid visible=false runat=server/>
			<Input type=hidden id=hidPayType value=0 runat=server/>
			<Input type=hidden id=hidPayMode value=0 runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
