<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeCPDet.aspx.vb" Inherits="HR_EmployeeCPDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Career Progress Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select one Allowance & Deduction code for " runat="server" />
			<asp:label id=lblBenefit visible=false text="Benefit in Kind" runat="server" />
			<table border=0 cellspacing="0" cellpadding=1 width="100%">
				<tr>
					<td colspan=5>
						<UserControl:MenuHR id=MenuHR runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5"><asp:label id="lblTitle" runat="server" /> DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td valign=top width=20% height=25>Employee Code :</td>
					<td valign=top width=35%><asp:Label id=lblEmpCode runat=server /></td>
					<td valign=top width=1%>&nbsp;</td>
					<td valign=top width=15%>Name :</td>
					<td valign=top width=30%><asp:Label id=lblEmpName runat=server /></td>
				</tr>
				<tr>
					<td valign=top height=25>Transaction ID :</td>
					<td valign=top><asp:Label id=lblCPID runat=server /></td>
					<td valign=top>&nbsp;</td>
					<td valign=top>&nbsp;</td>
					<td valign=top><asp:Label id=lblEmpStatus visible=false runat=server /></td>
				</tr>
				<tr>
					<td valign=top height=25><asp:label id="lblCP" runat="server" /> Code :*</td>
					<td valign=top><asp:DropDownList id=ddlCPCode width=100% runat=server AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged"/>
							<asp:RequiredFieldValidator id=revCP display=dynamic runat=server 
								ErrorMessage="Please select Career Progress Code."
								ControlToValidate=ddlCPCode /></td>											
					<td valign=top>&nbsp;</td>
					<td valign=top>Date Created :</td>
					<td valign=top><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td valign=top height=25>Date From :*</td>
					<td valign=top><asp:TextBox id=txtDateFrom width=87% runat=server />
						<a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrDateFrom visible=False forecolor=red text="<br>Date format should be in " runat=server />
						<asp:RequiredFieldValidator id=validateDateFrom display=dynamic runat=server 
							ErrorMessage="<br>Please enter Date From." 
							ControlToValidate=txtDateFrom /></td>
					<td valign=top>&nbsp;</td>
					<td valign=top>Last Updated :</td>
					<td valign=top><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td valign=top height=25>Date To :</td>
					<td valign=top><asp:TextBox id=txtDateTo width=87% enabled=false runat=server />
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:Label id=lblErrDateTo visible=False forecolor=red text="<br>Date format should be in " runat=server />
						<asp:Label id=lblErrReqDateTo visible=False forecolor=red text="<br>Please enter Date To." runat=server /></td>
					<td valign=top>&nbsp;</td>
					<td valign=top>Updated By :</td>
					<td valign=top><asp:Label id=lblUpdateBy runat=server /></td>
				</tr>
				<tr>
					<td valign=topheight=25>Cease Date :</td>
					<td valign=top><asp:TextBox id=txtCeaseDate width=87% enabled=false runat=server />
						<a href="javascript:PopCal('txtCeaseDate');"><asp:Image id="btnSelCeaseDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:Label id=lblErrCeaseDate visible=False forecolor=red text="<br>Date format should be in " runat=server />
						<asp:Label id=lblErrReqCeaseDate visible=False forecolor=red text="<br>Please enter Cease Date." runat=server /></td>
					<td valign=top colspan=1>&nbsp;</td>
					<td valign=top><asp:Label id=lblCPType visible=false text=0 runat=server /></td>
					<td valign=top><asp:Label id=lblPeriodInd visible=False text=0 runat=server /></td>
				</tr>
				<tr>
					<td valign=top height=25>Remark :</td>
					<td valign=top colspan=4><asp:TextBox id=txtRemark width=99% runat=server /></td>
				</tr>
				<tr>
					<td valign=top colspan=5>&nbsp;</td>
				</tr>
				
				<tr>
					<td valign=top colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr>
								<td valign=top width=20% height=25><asp:label id="lblCompany" runat="server" /> :*</td>
								<td valign=top width=35%><asp:DropDownList id=ddlCompCode width=100% enabled=false runat=server />
									<asp:RequiredFieldValidator id=revCompany display=dynamic runat=server 
										ErrorMessage="Please select Company." 
										ControlToValidate=ddlCompCode /></td>
								<td valign=top width=1%>&nbsp;</td>
								<td valign=top width=15%>Probation Period :</td>
								<!-- Modified BY ALIM ,width = 75%-->
								<td valign=top width=30%><asp:TextBox id=txtProbation width=100% runat=server /></td>
							</tr>							
							<tr>
								<td valign=top height=25><asp:label id="lblLocation" runat="server" /> :*</td>
								<td valign=top><asp:DropDownList id=ddlLocCode width=100% enabled=false runat=server />
									<asp:RequiredFieldValidator id=revLocation display=dynamic runat=server 
										ErrorMessage="Please select Location."
										ControlToValidate=ddlLocCode /></td>								
								<td valign=top>&nbsp;</td>
								<td valign=top>Pay Type :*</td>
								<td valign=top>
									<asp:DropDownList id=ddlPayType width=100% onSelectedIndexChanged=onChg_PayType autopostback=true runat=server />
									<asp:label id=lblErrPayType text="Please select Pay Type." visible=false forecolor=red runat=server />
								</td>																
							</tr>							
							<tr>
								<td valign=top height=25><asp:label id="lblDepartment" runat="server" /> :*</td>																																
								<td valign=top><asp:DropDownList id=ddlDeptCode width=100% runat=server />
									<asp:RequiredFieldValidator id=revDept display=dynamic runat=server 
										ErrorMessage="Please select Department."
										ControlToValidate=ddlDeptCode /></td>								
								<td valign=top>&nbsp;</td>
								<td valign=top>Current Amount :*</td>
								<td valign=top><asp:Label id=lblCurrentRate text=0 runat=server /></td>																
							</tr>							
							<tr>
								<td valign=top height=25><asp:label id="lblLevel" runat="server" /> :</td>
								<td valign=top><asp:DropDownList id=ddlLevelCode width=100% runat=server /></td>								
								<td valign=top>&nbsp;</td>
								<td valign=top>Adjustment Amount :*</td>
								<!-- Modified BY ALIM maxlength = 22, width = 75%-->
								<td valign=top><asp:TextBox id=txtIncrementAmt value=0 width=100% maxlength=22 runat=server />
									<asp:RegularExpressionValidator id="revIncrementAmt" 
										ControlToValidate="txtIncrementAmt"
										ValidationExpression="^[-]?\d{1,19}\.\d{1,2}|^[-]?\d{1,19}"
										Display="Dynamic"
										text = "<br>Maximum length 19 digits and 2 decimal points"
										runat="server"/>
								<!-- End of Modified BY ALIM -->		
									<asp:label id=lblErrIncrementAmt Visible=False forecolor=red Runat="server" />
								</td>
							</tr>							
							<tr>
								<td valign=top height=25>Report To :</td>
								<td valign=top>
									<asp:DropDownList id=ddlRptTo width=90% runat=server /> 
									<input id=btnFind type="button" value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlRptTo','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
								</td>
								<td valign=top>&nbsp;</td>								
								<td valign=top height=25>Daily Quota Level :</td>
								<td valign=top rowspan=4>
									<asp:radiobutton id=rbNoQuota groupname=rbQuotaLevel text=" Not Applicable" checked=True OnCheckedChanged="onChg_QuotaLevel" autopostback=true runat=server/><br>
									<asp:radiobutton id=rbActivity groupname=rbQuotaLevel OnCheckedChanged="onChg_QuotaLevel" autopostback=true runat=server/><br>
									<asp:radiobutton id=rbBlock groupname=rbQuotaLevel OnCheckedChanged="onChg_QuotaLevel" autopostback=true runat=server/><br>
									<asp:radiobutton id=rbIndividual groupname=rbQuotaLevel text=" Individual" OnCheckedChanged="onChg_QuotaLevel" autopostback=true runat=server/>
								</td>
							</tr>							
							<!--
							<tr>								
								<td valign=top height=25>Employee Category :*</td>
								<td valign=top><asp:DropDownList id=ddlCategory width=100% runat=server />
									<comment>Modified By BHL</comment>
									<asp:label id=lblErrCategory text="Please select Employee Category." visible=false forecolor=red runat=server />
									<comment>End Modified</comment>
								</td>
								<td valign=top colspan=2>&nbsp;</td>
							</tr>
							-->
							<!-- Change the Label "Salary Scheme" to "Employee Cateogry" -->
							<tr>
								<td valign=top height=25>Employee Category :*</td>
								<td valign=top><asp:DropDownList id=ddlSalSchemeCode width=100% runat=server />
									<asp:RequiredFieldValidator id=revSalScheme display=dynamic runat=server 
										ErrorMessage="Please select Salary Scheme."
										ControlToValidate=ddlSalSchemeCode />
								</td>
								<td valign=top colspan=2>&nbsp;</td>
							</tr>							
							<tr>
								<td valign=top height=25>Salary Grade :</td>
								<td valign=top><asp:DropDownList id=ddlSalGradeCode width=100% runat=server />
								<asp:RequiredFieldValidator id=revSalGradeCode display=dynamic runat=server 
										ErrorMessage="Please select Salary Grade."
										ControlToValidate=ddlSalGradeCode />
								</td>
								<td valign=top colspan=2>&nbsp;</td>
							</tr>
							
							<tr>
								<td valign=top height=25>Position :</td>
								<td valign=top><asp:DropDownList id=ddlPosCode width=100% runat=server /></td>
								<td valign=top colspan=2>&nbsp;</td>
							</tr>
							<!-- Modified By BHL -->
							<tr>
								<td valign=top height=25>Evaluation :*</td>
								<td valign=top><asp:DropDownList id=ddlEvalCode width=100% runat=server />
									<asp:RequiredFieldValidator id=revEvalCode display=dynamic runat=server 
										ErrorMessage="Please select Evaluation."
										ControlToValidate=ddlEvalCode />
								</td>
								<td valign=top colspan=2>&nbsp;</td>
							</tr>							
							<tr><td colspan=5><u><b>For Individual Daily Quota (if applicable)</b></u><td></tr>
							
							<tr>
								<td valign=top height=25>Daily Quota :</td>
								<td><asp:textbox id=txtIndDailyQuota maxlength=22 text=0 width=100% runat=server />
									<!-- Modified BY ALIM, max length = 22, width = 75% -->
									<asp:RegularExpressionValidator 
										id=revDailyQuota
										ControlToValidate=txtIndDailyQuota
										ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
										Display="Dynamic"
										text = "<br>Maximum length 19 digits and 2 decimal points. "
										runat="server"/>
								</td>
								<td valign=top colspan=3>&nbsp;</td>
							</tr>
							
							<tr>							
								<td valign=top height=25>Quota is measured by :</td>
								<td><asp:radiobutton id=rbByHour groupname=rbQuotaBy text="Hour" checked=true runat=server />&nbsp;
									<asp:radiobutton id=rbByVolume groupname=rbQuotaBy text="Volume" runat=server />
								</td>
								<td valign=top colspan=3>&nbsp;</td>
							</tr>
							
							<tr>
								<td valign=top height=25><asp:label id=lblQuotaInc runat=server/> Rate :</td>
								<!-- Modified BY ALIM, max length = 22, width = 75% -->
								<td><asp:textbox id=txtIndQuotaIncRate text=0 width=100% maxlength=22 runat=server />
									<asp:RegularExpressionValidator 
										id="revIndQuotaIncRate" 
										ControlToValidate="txtIndQuotaIncRate"
										ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
										Display="Dynamic"
										text = "<br>Maximum length 19 digits and 2 decimal points"
										runat="server" />	
								</td>
								<td valign=top colspan=3>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr><td colspan=5>&nbsp;</td></tr>
				<comment>Start Added By LKC</comment>							
				<tr>
					<td valign=top colspan=5 bgcolor=black>
						<table width=100% border=0 cellpadding=2 cellspacing=0 bgcolor=DarkGray class="font9Tahoma">
							<tr valign=top>
								<td width=20%>Monthly Salary :* </td>
								<td width=35%>
									<asp:Dropdownlist id=ddlSalary width=100% runat=server/>
									<asp:Label id=lblErrSalary visible=false forecolor=red text="Please select one Allowance & Deduction code for Monthly Salary." runat=server/>
								</td>
								<td width=1%>&nbsp;</td>
								<td width=15%>&nbsp;</td>
								<td width=30%>&nbsp;</td>
							</tr>
							<tr valign=top>
								<td>Daily Rate :*</td>
								<td>
									<asp:Dropdownlist id=ddlDaily width=100% runat=server/>
									<asp:Label id=lblErrDaily visible=false forecolor=red text="Please select one Allowance & Deduction code for Daily Rate." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr valign=top>
								<td>Piece Rate :*</td>
								<td>
									<asp:Dropdownlist id=ddlPiece width=100% runat=server/>
									<asp:Label id=lblErrPiece visible=false forecolor=red text="Please select one Allowance & Deduction code for Piece Rate." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td></td>
								<td></td>
							</tr>
							<tr valign=top>
								<td colspan=2 height=25><u><b>Allowance</b></u></td>
								<td>&nbsp;</td>
								<td colspan=2><u><b>Deduction</b></u></td>
							</tr>
							<tr valign=top>
								<td>Bonus :*</td>
								<td>
									<asp:Dropdownlist id=ddlBonus width=100% runat=server/>
									<asp:Label id=lblErrBonus visible=false forecolor=red text="Please select one Allowance & Deduction code for Bonus." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Advance Salary :*</td>
								<td>
									<asp:Dropdownlist id=ddlAdvSalary width=100% runat=server/>
									<asp:Label id=lblErrAdvSalary visible=false forecolor=red text="Please select one Allowance & Deduction code for inventory deduction" runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>THR Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlTHR width=100% runat=server/>
									<asp:Label id=lblErrTHR visible=false forecolor=red text="Please select one Allowance & Deduction code for THR Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Inventory Deduction :*</td>
								<td>
									<asp:Dropdownlist id=ddlIN width=100% runat=server/>
									<asp:Label id=lblErrIN visible=false forecolor=red text="Please select one Allowance & Deduction code for inventory deduction" runat=server/>
								</td>
							</tr>

							<tr valign=top>
								<td>Housing Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlHouse width=100% runat=server/>
									<asp:Label id=lblErrHouse visible=false forecolor=red text="Please select one Allowance & Deduction code for Housing Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Canteen Deduction :*</td>
								<td>
									<asp:Dropdownlist id=ddlCT width=100% runat=server/>
									<asp:Label id=lblErrCT visible=false forecolor=red text="Please select one Allowance & Deduction code for canteen deduction." runat=server/>
								</td>
							</tr>

							<tr valign=top>
								<td>Hardship Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlHardShip width=100% runat=server/>
									<asp:Label id=lblErrHardShip visible=false forecolor=red text="Please select one Allowance & Deduction code for Hardship Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Workshop Deduction :*</td>
								<td>
									<asp:Dropdownlist id=ddlWS width=100% runat=server/>
									<asp:Label id=lblErrWS visible=false forecolor=red text="Please select one Allowance & Deduction code for workshop deduction." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>Incentive Award :*</td>
								<td>
									<asp:Dropdownlist id=ddlIncAward width=100% runat=server/>
									<asp:Label id=lblErrIncAward visible=false forecolor=red text="Please select one Allowance & Deduction code for Incentive Award." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Workshop Refund :*</td>
								<td>
									<asp:Dropdownlist id=ddlWSRefund width=100% runat=server/>
									<asp:Label id=lblErrWSRefund visible=false forecolor=red text="Please select one Allowance & Deduction code for workshop refund." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>Transport Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlTransport width=100% runat=server/>
									<asp:Label id=lblErrTransport visible=false forecolor=red text="Please select one Allowance & Deduction code for Transport Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Loan :*</td>
								<td>
									<asp:Dropdownlist id=ddlLoan width=100% runat=server/>
									<asp:Label id=lblErrLoan visible=false forecolor=red text="Please select one Allowance & Deduction code for Loan." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>House Rent Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlHouseRent width=100% runat=server/>
									<asp:Label id=lblErrHouseRent visible=false forecolor=red text="Please select one Allowance & Deduction code for House Rent Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Catu Beras Deduction :*</td>
								<td>
									<asp:Dropdownlist id=ddlRiceDeduction width=100% runat=server/>
									<asp:Label id=lblErrRiceDeduction visible=false forecolor=red text="Please select one Allowance & Deduction code for Rice Deduction." runat=server/>
								</td>		
							<tr valign=top>
								<td>Medical Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlMedical width=100% runat=server/>
									<asp:Label id=lblErrMedical visible=false forecolor=red text="Please select one Allowance & Deduction code for Medical Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Brought Forward Payment :*</td>
								<td>
									<asp:Dropdownlist id=ddlBFEmp width=100% runat=server/>
									<asp:Label id=lblErrBF visible=false forecolor=red text="Please select one Brought Forward Payment for Employee." runat=server/>
								</td>	
							<tr valign=top>
								<td>Meal Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlMeal width=100% runat=server/>
									<asp:Label id=lblErrMeal visible=false forecolor=red text="Please select one Allowance & Deduction code for Meal Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Outstanding Payment :*</td>
								<td>
								
									<asp:Dropdownlist id=ddlOutPayEmp width=100% runat=server/>
									<asp:Label id=lblErrOutPay visible=false forecolor=red text="Please select one Allowance & Deduction code." runat=server/>
								</td>
							<tr valign=top>
								<td>Leave Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlLeave width=100% runat=server/>
									<asp:Label id=lblErrLeave visible=false forecolor=red text="Please select one Allowance & Deduction code for Leave Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Absent Days :*</td>
								<td>
									<asp:Dropdownlist id=ddlAbsent width=100% runat=server/>
									<asp:Label id=lblErrAbsent visible=false forecolor=red text="Please select one Allowance & Deduction code for Absent Days." runat=server/>
								</td>
							<tr valign=top>
								<td>Air Fare/Bus Ticket :*</td>
								<td>
									<asp:Dropdownlist id=ddlAirBus width=100% runat=server/>
									<asp:Label id=lblErrAirBus visible=false forecolor=red text="Please select one Allowance & Deduction code for Air Fare/Bus Ticket Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td>Hutang :*</td>
								<td>
									<asp:Dropdownlist id=ddlHutang width=100% runat=server/>
									<asp:Label id=lblErrHutang visible=false forecolor=red text="Please select one Allowance & Deduction code for Hutang." runat=server/>
								</td>
							<tr valign=top>
								<td>Maternity Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlMaternity width=100% runat=server/>
									<asp:Label id=lblErrMaternity visible=false forecolor=red text="Please select one Allowance & Deduction code for Maternity Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>					
								<td colspan=2 height=25><u><b>Iuran</b></u></td>	
							<tr valign=top>
								<td>Tax Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlTax width=100% runat=server/>
									<asp:Label id=lblErrTax visible=false forecolor=red text="Please select one Allowance & Deduction code for Tax Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td align="left">Potongan SPSI Koperasi :*</td>
								<td align="left">
									<asp:Dropdownlist id=ddlSPSICOP width=100% runat=server/>
									<asp:Label id=lblErrSPSICOP visible=false forecolor=red text="Please select one Allowance & Deduction code for Potongan SPSI Koperasi." runat=server/>
								</td>	
							<tr valign=top>
								<td>Dana Pensiun :*</td>
								<td>
									<asp:Dropdownlist id=ddlDanaPensiun width=100% runat=server/>
									<asp:Label id=lblErrDanaPensiun visible=false forecolor=red text="Please select one Allowance & Deduction code for Dana Pensiun." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td align="left" valign=top>Iuran Koperasi :*</td>
								<td align="left" valign=top>
									<asp:Dropdownlist id=ddlLuranCOP width=100% runat=server/>
									<asp:Label id=lblErrLuranCOP visible=false forecolor=red text="Please select one Allowance & Deduction code for Luran Koperasi." runat=server/>
								</td>
							<tr valign=top>
								<td>Relocation Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlRelocation width=100% runat=server/>
									<asp:Label id=lblErrRelocation visible=false forecolor=red text="Please select one Allowance & Deduction code for Relocation Allowance." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td align="left" valign=top>Others :*</td>
								<td align="left" valign=top>
									<asp:Dropdownlist id=ddlOther width=100% runat=server/>
									<asp:Label id=lblErrOther visible=false forecolor=red text="Please select one Allowance & Deduction code for Others." runat=server/>
								</td>
							<tr valign=top>
								<td>Rapel :*</td>
								<td>
									<asp:Dropdownlist id=ddlRapel width=100% runat=server/>
									<asp:Label id=lblErrRapel visible=false forecolor=red text="Please select one Allowance & Deduction code for Rapel." runat=server/>
								</td>	
								<td>&nbsp;</td>					
								<td colspan=2 height=25><u><b>Levy</b></u></td>													
													
							<tr valign=top>
								<td>Overtime :*</td>
								<td>
									<asp:Dropdownlist id=ddlOvertime width=100% runat=server/>
									<asp:Label id=lblErrOvertime visible=false forecolor=red text="Please select one Allowance & Deduction code for Overtime." runat=server/>
								</td>	
								<td>&nbsp;</td>
								<td align="left">Hold (Levy) :*</td>
								<td align="left">
									<asp:Dropdownlist id=ddlHold width=100% runat=server/>
									<asp:Label id=lblErrHold visible=false forecolor=red text="Please select one Allowance & Deduction code for Levy Hold." runat=server/>
								</td>												
							</tr>
							<tr valign=top>
								<td>Trip :*</td>
								<td>
									<asp:Dropdownlist id=ddlTrip width=100% runat=server/>
									<asp:Label id=lblErrTrip visible=false forecolor=red text="Please select one Allowance & Deduction code for Trip." runat=server/>
								</td>	
								<td>&nbsp;</td>
								<td align="left" valign=top>Payment (Levy) :*</td>
								<td align="left" valign=top>
									<asp:Dropdownlist id=ddlPayment width=100% runat=server/>
									<asp:Label id=lblErrPayment visible=false forecolor=red text="Please select one Allowance & Deduction code for Payment." runat=server/>
								</td>							
							<tr valign=top>
								<td><asp:label id=lblRiceRation runat=server/> :*</td>
								<td>
									<asp:dropdownlist id=ddlRiceRation width=100% runat=server/>
									<asp:label id=lblErrRice visible=false forecolor=red runat=server />
								</td>
								<td>&nbsp;</td>
								<td align="left" valign=top>Subsidy :*</td>
								<td align="left" valign=top>
									<asp:Dropdownlist id=ddlSubsidy width=100% runat=server/>
									<asp:Label id=lblErrSubsidy visible=false forecolor=red text="Please select one Allowance & Deduction code for Subsidy." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td><asp:label id=lblIncentive runat=server/> :*</td>
								<td>
									<asp:dropdownlist id=ddlIncentive width=100% runat=server/>
									<asp:label id=lblErrIncentive visible=false forecolor=red runat=server />
								</td>							
								<td>&nbsp;</td>
								<td align="left" valign=top>Deficit :*</td>
								<td align="left" valign=top>
									<asp:Dropdownlist id=ddlDeficit width=100% runat=server/>
									<asp:Label id=lblErrDeficit visible=false forecolor=red text="Please select one Allowance & Deduction code for Deficit." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td><asp:label id=lblQuotaIncCode runat=server/> :*</td>
								<td>
									<asp:dropdownlist id=ddlQuotaIncCode width=100% runat=server/>
									<asp:label id=lblErrQuotaIncCode visible=false forecolor=red text="Please select one Allowance & Deduction code for " runat=server />
								</td>
								<td>&nbsp;</td>
								<td align="left" valign=top>Adjustment (Levy) :*</td>
								<td align="left" valign=top>
									<asp:Dropdownlist id=ddlLevyAdj width=100% runat=server/>
									<asp:Label id=lblErrLevyAdj visible=false forecolor=red text="Please select one Allowance & Deduction code for Levy Adjustment." runat=server/>
								</td>												
							</tr>
							<tr valign=top>
								<td>Contract Payment :*</td>
								<td>
									<asp:Dropdownlist id=ddlContractPay width=100% runat=server/>
									<asp:Label id=lblErrContractPay visible=false forecolor=red text="Please select one Allowance & Deduction code for Contract Payment." runat=server/>
								</td>
								<td>&nbsp;</td>
								<td align="left" valign=top>Deduction Method (Levy) :*</td>
								<td align="left" valign=top>
									<asp:RadioButton id="rbAccumulate" 
										Checked="True"
										GroupName="LevyDeduction"
										Text=" Accumulate"
										TextAlign="Right"
										runat="server" />
									<asp:RadioButton id="rbPush" 
										Checked="False"
										GroupName="LevyDeduction"
										Text=" Push"
										TextAlign="Right"
										runat="server" />
								</td>													
							</tr>
									
							<tr valign=top>
								<td>Accommodation Benefit in Kind :*</td>
								<td>
									<asp:Dropdownlist id=ddlBIKAccom width=100% runat=server/>
									<asp:Label id=lblErrBIKAccom visible=false forecolor=red text="Please select one Allowance & Deduction code for Accommodation Benefit in Kind." runat=server/>
								</td>
								
							</tr>
							<tr valign=top>
								<td><asp:label id="lblVehicle" runat="server" /> Benefit in Kind :*</td>
								<td>
									<asp:Dropdownlist id=ddlBIKVeh width=100% runat=server/>
									<asp:Label id=lblErrBIKVeh visible=false forecolor=red text="Please select one Allowance & Deduction code for Vehicle Benefit in Kind." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>Mobile Phone Benefit in Kind :*</td>
								<td>
									<asp:Dropdownlist id=ddlBIKHP width=100% runat=server/>
									<asp:Label id=lblErrBIKHP visible=false forecolor=red text="Please select one Allowance & Deduction code for Mobile Phone Benefit in Kind." runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>Gratuity :*</td>
								<td>
									<asp:Dropdownlist id=ddlGratuity width=100% runat=server/>
									<asp:Label id=lblErrGratuity visible=false forecolor=red text="Please select one Allowance & Deduction code for Gratuity." runat=server/>
								</td>								
							</tr>
							<tr valign=top>
								<td>Retrenchment Compensation :*</td>
								<td>
									<asp:Dropdownlist id=ddlRetrench width=100% runat=server/>
									<asp:Label id=lblErrRetrench visible=false forecolor=red text="Please select one Allowance & Deduction code for Retrenchment Compensation." runat=server/>
								</td>											
							</tr>
							<tr valign=top>
								<td>Employee Share Option Scheme :*</td>
								<td>
									<asp:Dropdownlist id=ddlESOS width=100% runat=server/>
									<asp:Label id=lblErrESOS visible=false forecolor=red text="Please select one Allowance & Deduction code for Employee Share Option Scheme." runat=server/>
								</td>								
							</tr>							
							<tr valign=top>
								<td>Attendance Sub Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlAttAllow width=100% runat=server/>
									<asp:Label id=lblErrAttAllow visible=false forecolor=red text="Please select one Allowance & Deduction code for attendance sub-allowance" runat=server/>
								</td>								
							</tr>
							<tr valign=top>
								<td>Staff Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlStaff width=100% runat=server/>
									<asp:Label id=lblErrStaff visible=false forecolor=red text="Please select one Allowance & Deduction code for Staff Allowance" runat=server/>
								</td>
							</tr>							
							<tr valign=top>
								<td>Functional Allowance :*</td>
								<td>
									<asp:Dropdownlist id=ddlFunctional width=100% runat=server/>
									<asp:Label id=lblErrFunctional visible=false forecolor=red text="Please select one Allowance & Deduction code for Functional Allowance" runat=server/>
								</td>
							</tr>
							<tr valign=top>
								<td>&nbsp;</td>
								<td>&nbsp;</td>								
							</tr>							
							<tr valign=top>
								<td>&nbsp;</td>
								<td>&nbsp;</td>								
							</tr>
							<tr valign=top>
								<td>&nbsp;</td>
								<td>&nbsp;</td>								
							</tr>
						</table>
					</td>
				</tr>
				<comment>End Added By LKC</comment>
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
						<asp:LinkButton id=lbStatutory text="Employee Statutory" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbFamily text="Employee Family" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbQualific text="Employee Qualification" causesvalidation=false runat=server /> |
						<asp:LinkButton id=lbSkill text="Employee Skill" causesvalidation=false runat=server />
					</td>
				</tr>
			</table>
			<input type=Hidden id=EmpCode runat=server />
			<input type=Hidden id=EmpName runat=server />
			<input type=Hidden id=ProbationPeriod runat=server />
			<input type=Hidden id=IncrementAmt runat=server />
			<asp:Label id=lblRedirect visible=false runat=server/>
			<asp:Label id=lblHidQuotaLevel text="1" visible=false runat=server/>
			<asp:Label id=lblHidIndQuota text="0" visible=false runat=server/>
			<asp:Label id=lblHidIndQuotaMethod text="1" visible=false runat=server/>
			<asp:Label id=lblHidIndQuotaIncRate text=0 visible=false runat=server />

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
