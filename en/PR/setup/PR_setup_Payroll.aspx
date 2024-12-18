<%@ Page Language="vb" src="../../../include/PR_setup_Payroll.aspx.vb" Inherits="PR_setup_Payroll" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Payroll Process Configuration</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmPayroll runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select one Allowance & Deduction code for " runat="server" />
			<asp:label id=lblBenefit visible=false text="Benefit in Kind" runat="server" />
			<asp:label id=lblAvgBunchWeightInd visible=false runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">PAYROLL PROCESS CONFIGURATION</td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /> </td>
				</tr>
				<tr valign=top>
					<td width=20%>Monthly Salary :* </td>
					<td width=25%>
						<asp:Dropdownlist id=ddlSalary width=100% runat=server/>
						<asp:Label id=lblErrSalary visible=false forecolor=red text="Please select one Allowance & Deduction code for Monthly Salary." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=25%>Last Updated : </td>
					<td colspan=3><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr valign=top>
					<td>Daily Rate :*</td>
					<td>
						<asp:Dropdownlist id=ddlDaily width=100% runat=server/>
						<asp:Label id=lblErrDaily visible=false forecolor=red text="Please select one Allowance & Deduction code for Daily Rate." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td colspan=3><asp:Label id=lblUpdatedBy runat=server /></td>
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
					<comment>Modified By BHL</comment>
					<td>THR Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlTHR width=100% runat=server/>
						<asp:Label id=lblErrTHR visible=false forecolor=red text="Please select one Allowance & Deduction code for THR Allowance." runat=server/>
					</td>
					<comment>End Modified</comment>
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
					<comment>Start Modified By ALIM 30 June 2006</comment>
					<td>Catu Beras Deduction :*</td>
					<td>
						<asp:Dropdownlist id=ddlRiceDeduction width=100% runat=server/>
						<asp:Label id=lblErrRiceDeduction visible=false forecolor=red text="Please select one Allowance & Deduction code for Catu Beras Deduction." runat=server/>
					</td>
					<comment>End Modified By ALIM 30 June 2006</comment>
								
				<tr valign=top>
					<td>Meal Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlMeal width=100% runat=server/>
						<asp:Label id=lblErrMeal visible=false forecolor=red text="Please select one Allowance & Deduction code for Meal Allowance." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Brought Forward Payment :*</td>
					<td>
						<asp:Dropdownlist id=ddlBFEmp width=100% runat=server/>
						<asp:Label id=lblErrBF visible=false forecolor=red text="Please select one Brought Forward Payment for Employee." runat=server/>
					</td>			
								
				<tr valign=top>
					<td>Leave Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlLeave width=100% runat=server/>
						<asp:Label id=lblErrLeave visible=false forecolor=red text="Please select one Allowance & Deduction code for Leave Allowance." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Outstanding Payment :*</td>
					<td>
						<asp:Dropdownlist id=ddlOutPayEmp width=100% runat=server/>
						<asp:Label id=lblErrOutPay visible=false forecolor=red text="Please select one Allowance & Deduction code." runat=server/>
					</td>
								
				<tr valign=top>
					<td>Air Fare/Bus Ticket Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlAirBus width=100% runat=server/>
						<asp:Label id=lblErrAirBus visible=false forecolor=red text="Please select one Allowance & Deduction code for Air Fare/Bus Ticket Allowance." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Absent Days :*</td>
					<td>
						<asp:Dropdownlist id=ddlAbsent width=100% runat=server/>
						<asp:Label id=lblErrAbsent visible=false forecolor=red text="Please select one Allowance & Deduction code for Absent Days." runat=server/>
					</td>
				<tr valign=top>
					<td>Medical Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlMedical width=100% runat=server/>
						<asp:Label id=lblErrMedical visible=false forecolor=red text="Please select one Allowance & Deduction code for Medical Allowance." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Hutang :*</td>
					<td>
						<asp:Dropdownlist id=ddlHutang width=100% runat=server/>
						<asp:Label id=lblErrHutang visible=false forecolor=red text="Please select one Allowance & Deduction code for Hutang." runat=server/>
					</td>
					<td>&nbsp;</td>
								
				<tr valign=top>
					<td>Maternity Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlMaternity width=100% runat=server/>
						<asp:Label id=lblErrMaternity visible=false forecolor=red text="Please select one Allowance & Deduction code for Maternity Allowance." runat=server/>
					</td>
								
				<tr valign=top>
					<td>Tax Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlTax width=100% runat=server/>
						<asp:Label id=lblErrTax visible=false forecolor=red text="Please select one Allowance & Deduction code for Tax Allowance." runat=server/>
					</td>
					<td>&nbsp;</td>					
					<td colspan=2 height=25><u><b>Iuran</b></u></td>	
								
				<tr valign=top>
					<td>Dana Pensiun :*</td>
					<td>
						<asp:Dropdownlist id=ddlDanaPensiun width=100% runat=server/>
						<asp:Label id=lblErrDanaPensiun visible=false forecolor=red text="Please select one Allowance & Deduction code for Dana Pensiun." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td align="left">Potongan SPSI Koperasi :*</td>
					<td align="left">
						<asp:Dropdownlist id=ddlSPSICOP width=100% runat=server/>
						<asp:Label id=lblErrSPSICOP visible=false forecolor=red text="Please select one Allowance & Deduction code for Potongan SPSI Koperasi." runat=server/>
					</td>			
				<tr valign=top>
					<td>Relocation Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlRelocation width=100% runat=server/>
						<asp:Label id=lblErrRelocation visible=false forecolor=red text="Please select one Allowance & Deduction code for Relocation Allowance." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td align="left" valign=top>Iuran Koperasi :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlLuranCOP width=100% runat=server/>
						<asp:Label id=lblErrLuranCOP visible=false forecolor=red text="Please select one Allowance & Deduction code for Luran Koperasi." runat=server/>
					</td>
								
				<tr valign=top>
					<td>Rapel :*</td>
					<td>
						<asp:Dropdownlist id=ddlRapel width=100% runat=server/>
						<asp:Label id=lblErrRapel visible=false forecolor=red text="Please select one Allowance & Deduction code for Rapel." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td align="left" valign=top>Others :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlOther width=100% runat=server/>
						<asp:Label id=lblErrOther visible=false forecolor=red text="Please select one Allowance & Deduction code for Others." runat=server/>
					</td>
				
				<tr valign=top>
					<td>Overtime :*</td>
					<td>
						<asp:Dropdownlist id=ddlOvertime width=100% runat=server/>
						<asp:Label id=lblErrOvertime visible=false forecolor=red text="Please select one Allowance & Deduction code for Overtime." runat=server/>
					</td>
					<td>&nbsp;</td>					
					<td colspan=2 height=25><u><b>Levy</b></u></td>	
				</tr>
				
				<tr valign=top>
					<td>Trip :*</td>
					<td>
						<asp:Dropdownlist id=ddlTrip width=100% runat=server/>
						<asp:Label id=lblErrTrip visible=false forecolor=red text="Please select one Allowance & Deduction code for Trip." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td align="left">Hold (Levy) :*</td>
					<td align="left">
						<asp:Dropdownlist id=ddlHold width=100% runat=server/>
						<asp:Label id=lblErrHold visible=false forecolor=red text="Please select one Allowance & Deduction code for Levy Hold." runat=server/>
					</td>				
				</tr>
				
				<tr valign=top >
					<td><asp:label id=lblRiceRation runat=server/> :*</td>
					<td>
						<asp:dropdownlist id=ddlRiceRation width=100% runat=server/>
						<asp:label id=lblErrRice visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>
					<td align="left" valign=top>Payment (Levy) :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlPayment width=100% runat=server/>
						<asp:Label id=lblErrPayment visible=false forecolor=red text="Please select one Allowance & Deduction code for Payment." runat=server/>
					</td>
				</tr>
				
				<tr valign=top >
					<td><asp:label id=lblIncentive runat=server/> :*</td>
					<td>
						<asp:dropdownlist id=ddlIncentive width=100% runat=server/>
						<asp:label id=lblErrIncentive visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>
					<td align="left" valign=top>Subsidy :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlSubsidy width=100% runat=server/>
						<asp:Label id=lblErrSubsidy visible=false forecolor=red text="Please select one Allowance & Deduction code for Subsidy." runat=server/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<comment>Start Modified By LKC</comment>
				<tr valign=top >
					<td><asp:label id=lblQuotaInc runat=server/> :*</td>
					<td>
						<asp:dropdownlist id=ddlQuotaInc width=100% runat=server/>
						<asp:label id=lblErrQuotaInc visible=false forecolor=red text="Please select one Allowance & Deduction code for " runat=server />
					</td>
					<td>&nbsp;</td>
					<td align="left" valign=top>Deficit :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlDeficit width=100% runat=server/>
						<asp:Label id=lblErrDeficit visible=false forecolor=red text="Please select one Allowance & Deduction code for Deficit." runat=server/>
					</td>			
				</tr>
				<tr valign=top >
					<td>Contract Payment :*</td>
					<td>
						<asp:Dropdownlist id=ddlContractPay width=100% runat=server/>
						<asp:Label id=lblErrContractPay visible=false forecolor=red text="Please select one Allowance & Deduction code for Contract Payment." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td align="left" valign=top>Adjustment (Levy) :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlLevyAdj width=100% runat=server/>
						<asp:Label id=lblErrLevyAdj visible=false forecolor=red text="Please select one Allowance & Deduction code for Levy Adjustment." runat=server/>
					</td>		
				</tr>
						
				<tr valign=top >
					<td>Accommodation Benefit in Kind :*</td>
					<td>
						<asp:Dropdownlist id=ddlBIKAccom width=100% runat=server/>
						<asp:Label id=lblErrBIKAccom visible=false forecolor=red text="Please select one Allowance & Deduction code for Accommodation Benefit in Kind." runat=server/>
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
				
				<tr valign=top >
					<td><asp:label id="lblVehicle" runat="server" /> Benefit in Kind :*</td>
					<td>
						<asp:Dropdownlist id=ddlBIKVeh width=100% runat=server/>
						<asp:Label id=lblErrBIKVeh visible=false forecolor=red text="Please select one Allowance & Deduction code for Vehicle Benefit in Kind." runat=server/>
					</td>
					<td>&nbsp;</td>
					<comment>Added by BHL </comment>
					<td colspan=2><u><b>Catu Beras</b></u></td>					
					<comment>
						<td colspan=2><u><b>&nbsp;</b></u></td>
						<td colspan=2><u><b>Harvester Pay by Piece Rate Payment</b></u></td>
					</comment>
				</tr>
				
				<tr valign=top lsy>
					<td>Mobile Phone Benefit in Kind :*</td>
					<td>
						<asp:Dropdownlist id=ddlBIKHP width=100% runat=server/>
						<asp:Label id=lblErrBIKHP visible=false forecolor=red text="Please select one Allowance & Deduction code for Mobile Phone Benefit in Kind." runat=server/>
					</td>
					<td>&nbsp;</td>
					<comment>Added by BHL </comment>
					<td>Catu Beras Inventory :*</td>
					<td>
						<asp:Dropdownlist id=ddlInvCatuBeras width=100% runat=server/>
						<asp:Label id=lblErrInvCatuBeras visible=false forecolor=red text="Please select Catu Beras Inventory." runat=server/>
					</td>						
					<comment>
						<td >Automate Piece Rate Payment Unit from Year of Planting Yield : </td>
						<td ><asp:checkbox id=cbHarvAutoWeight text="Yes" onCheckedChanged="onCheck_HarvAutoWeight" autopostback=true runat=server />
					</comment>
					</td>
				</tr>
				
				<tr valign=top>
					<td>Gratuity :*</td>
					<td>
						<asp:Dropdownlist id=ddlGratuity width=100% runat=server/>
						<asp:Label id=lblErrGratuity visible=false forecolor=red text="Please select one Allowance & Deduction code for Gratuity." runat=server/>
					</td>	
					<td>&nbsp;</td>
					<comment>Added by dian </comment>
					<td colspan=2><u><b>Harvester Incentive</b></u></td>
				</tr>
				
				<tr valign=top>
					<td>Retrenchment Compensation :*</td>
					<td>
						<asp:Dropdownlist id=ddlRetrench width=100% runat=server/>
						<asp:Label id=lblErrRetrench visible=false forecolor=red text="Please select one Allowance & Deduction code for Retrenchment Compensation." runat=server/>
					</td>		
					<td>&nbsp;</td>
					<comment>Added by dian </comment>
					<td>Harvester Incentive Account:*</td>
					<td width="75%">
						<asp:Dropdownlist id=ddlHarvestInc width=100% runat=server/>
						<asp:Label id=lblErrHarvestInc visible=false forecolor=red text="Please select Harvester Incentive Account." runat=server/>
					</td>
				</tr>
				
				<tr valign=top>
					<td>Employee Share Option Scheme :*</td>
					<td width="35%">
						<asp:Dropdownlist id=ddlESOS width=100% runat=server/>
						<asp:Label id=lblErrESOS visible=false forecolor=red text="Please select one Allowance & Deduction code for Employee Share Option Scheme." runat=server/>
					</td>
					<td>&nbsp;</td>
					<comment>Added by smn </comment>
					<td colspan=2><u><b>Cash Account</b></u></td>	
				</tr>		
									
				<tr valign=top>
					<td>Attendance Sub Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlAttAllow width=100% runat=server/>
						<asp:Label id=lblErrAttAllow visible=false forecolor=red text="Please select one Allowance & Deduction code for attendance sub-allowance" runat=server/>
					</td>
					<td>&nbsp;</td>
					<comment>Added by simon </comment>
					<td>Cash Account:*</td>
					<td width="75%">
						<asp:Dropdownlist id=ddlCashAccount width=100% runat=server/>
						<asp:Label id=lblErrCashAccount visible=false forecolor=red text="Please select Cash Account." runat=server/>
					</td>
				</tr>
				
				<tr valign=top>
					<td>Staff Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlStaff width=100% runat=server/>
						<asp:Label id=lblErrStaff visible=false forecolor=red text="Please select one Allowance & Deduction code for Staff Allowance" runat=server/>
					</td>
					<td>&nbsp;</td>
					<comment>Added by smn </comment>
					<td colspan=2><u><b>Bank Code</b></u></td>
					
				</tr>
				<tr valign=top>
					<td>Functional Allowance :*</td>
					<td>
						<asp:Dropdownlist id=ddlFunctional width=100% runat=server/>
						<asp:Label id=lblErrFunctional visible=false forecolor=red text="Please select one Allowance & Deduction code for Functional Allowance" runat=server/>
					</td>
					<td>&nbsp;</td>
					<comment>Added by simon </comment>
					<td>Bank Code:*</td>
					<td width="75%">
						<asp:Dropdownlist id=ddlBankCode width=100% runat=server/>
						<asp:Label id=lblErrBankCode visible=false forecolor=red text="Please select Bank Code." runat=server/>
					</td>
				</tr>

				<comment>End Modified By LKC</comment>
				<tr><td colspan=5>&nbsp;</td></tr>
				<tr valign=top>
					<td colspan=2><u><b>Wages Calculation</b></u></td>
				</tr>
				<tr valign=top>					
					<td>Daily Working Hour :*</td>
					<td>
						<asp:Textbox id=txtDailyHour width=50% text=8 maxlength=2 runat=server/> (default = 8 hours)
						<asp:RequiredFieldValidator id=rfvDailyHour display=Dynamic runat=server 
								ErrorMessage="<br>Please enter the number of working hour in a day."
								ControlToValidate=txtDailyHour />
						<asp:CompareValidator id="cvDailyHour" display=dynamic runat="server" 
								ControlToValidate="txtDailyHour" Text="<br>The value must whole number." 
								Type="Integer" 
								Operator="DataTypeCheck"/>
					</td>
				</tr>
				
				<tr valign=top>
					<td>No. of work day per month :*</td>
					<td>
						<asp:Textbox id=txtWorkDay width=50% text=26 maxlength=2 runat=server/>
						<asp:RequiredFieldValidator id=rfvWorkDay display=Dynamic runat=server 
								ErrorMessage="<br>Please enter the number of working day a month."
								ControlToValidate=txtWorkDay />
						<asp:RangeValidator id="rvWorkDay"
								ControlToValidate="txtWorkDay"
								MinimumValue="1"
								MaximumValue="31"
								Type="Integer"
								EnableClientScript="True"
								Text="<br>The value must be from 1 to 31"
								runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td colspan=2>&nbsp;</td>	
					<comment>
						<td>Piece Rate Unit will be calculated by : </td>
						<td>
							<asp:checkbox id=cbAutoWeightGroup text="Group" runat=server />&nbsp;
							<asp:checkbox id=cbAutoWeightDaily text="Daily" runat=server />
						</td>
					</comment>					
				</tr>
				<tr valign=top>
					<comment>Added by BHL </comment>
					<td>Basic Total Pay Days per month :*</td>
					<td>
						<asp:Textbox id=txtBasicPayDay width=50% text=8 maxlength=2 runat=server/>
						<asp:RequiredFieldValidator id=rfvBasicPayDay display=Dynamic runat=server 
								ErrorMessage="<br>Please enter the number of basic pay day a month."
								ControlToValidate=txtBasicPayDay />
						<asp:RangeValidator id="rvBasicPayDay"
								ControlToValidate="txtBasicPayDay"
								MinimumValue="1"
								MaximumValue="31"
								Type="Integer"
								EnableClientScript="True"
								Text="<br>The value must be from 1 to 31"
								runat="server"/>
					</td>					
					<td colspan=3>&nbsp;</td>	
				</tr>
				<tr valign=top>
					<comment>Added by BHL </comment>
					<td>Prorate Basic Total Pay Days per month :*</td>
					<td>
						<asp:Textbox id=txtProratePayDay width=50% text=30 maxlength=2 runat=server/>
						<asp:RequiredFieldValidator id=rfvProratePayDay display=Dynamic runat=server 
								ErrorMessage="<br>Please enter the number of prorate basic pay day a month."
								ControlToValidate=txtProratePayDay />
						<asp:RangeValidator id="rvProratePayDay"
								ControlToValidate="txtProratePayDay"
								MinimumValue="1"
								MaximumValue="31"
								Type="Integer"
								EnableClientScript="True"
								Text="<br>The value must be from 1 to 31"
								runat="server"/>
					</td>					
					<td colspan=3>&nbsp;</td>	
				</tr>
				<tr valign=top>
					<comment>Added by BHL </comment>
					<td>Advance Payment Amount SKU Harian :*</td>
					<td><asp:textbox id=txtAdvPytSKUHarian text=0 width=50% maxlength=19 runat=server/>
						<asp:RequiredFieldValidator id=rfvAdvPytSKUHarian 
								display=Dynamic 
								runat=server 
								ErrorMessage="<br>Please enter Advance Payment Amount for SKU Harian."
								ControlToValidate=txtAdvPytSKUHarian />
						<asp:CompareValidator id="cvAdvPytSKUHarian" display=dynamic runat="server" 
								ControlToValidate="txtAdvPytSKUHarian" Text="<br>The value must whole number or with decimal. " 
								Type="Double" Operator="DataTypeCheck"/>								
						<asp:RegularExpressionValidator id=revAdvPytSKUHarian 
								ControlToValidate="txtAdvPytSKUHarian"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 0 decimal points. "
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>					
				</tr>
				<tr valign=top>
					<comment>Added by BHL </comment>				
					<td>No. of Work Days after Absence SKU Harian :*</td>
					<td>
						<asp:Textbox id=txtWorkDaySKUHarian width=50% text=8 maxlength=2 runat=server/>
						<asp:RequiredFieldValidator id=rfvWorkDaySKUHarian display=Dynamic runat=server 
								ErrorMessage="<br>Please enter the number of working day a month for SKU Harian."
								ControlToValidate=txtWorkDaySKUHarian />
						<asp:RangeValidator id="rvWorkDaySKUHarian"
								ControlToValidate="txtWorkDaySKUHarian"
								MinimumValue="1"
								MaximumValue="31"
								Type="Integer"
								EnableClientScript="True"
								Text="<br>The value must be from 1 to 31"
								runat="server"/>
					</td>				
				</tr>
				<tr valign=top>
					<comment>Added by BHL </comment>
					<td>Advance Payment Amount SKU Bulanan :*</td>
					<td><asp:textbox id=txtAdvPytSKUBulanan text=0 width=50% maxlength=19 runat=server/>
						<asp:RequiredFieldValidator id=rfvAdvPytSKUBulanan 
								display=Dynamic 
								runat=server 
								ErrorMessage="<br>Please enter Advance Payment Amount for SKU Bulanan."
								ControlToValidate=txtAdvPytSKUBulanan />
						<asp:CompareValidator id="cvAdvPytSKUBulanan" display=dynamic runat="server" 
								ControlToValidate="txtAdvPytSKUBulanan" Text="<br>The value must whole number or with decimal. " 
								Type="Double" Operator="DataTypeCheck"/>								
						<asp:RegularExpressionValidator id=revAdvPytSKUBulanan 
								ControlToValidate="txtAdvPytSKUBulanan"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 0 decimal points. "
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>		
				<tr valign=top>
					<comment>Added by BHL </comment>
					<td>No. of Work Days after Absence SKU Bulanan :*</td>
					<td>
						<asp:Textbox id=txtWorkDaySKUBulanan width=50% text=8 maxlength=2 runat=server/>
						<asp:RequiredFieldValidator id=rfvWorkDaySKUBulanan display=Dynamic runat=server 
								ErrorMessage="<br>Please enter the number of working day a month for SKU Bulanan."
								ControlToValidate=txtWorkDaySKUBulanan />
						<asp:RangeValidator id="rvWorkDaySKUBulanan"
								ControlToValidate="txtWorkDaySKUBulanan"
								MinimumValue="1"
								MaximumValue="31"
								Type="Integer"
								EnableClientScript="True"
								Text="<br>The value must be from 1 to 31"
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>							
				</tr>		
				<tr valign=top>
					<td>Rest Day Rate for Daily Rated work :*</td>
					<td>
						<asp:Textbox id=txtOffPayRate width=50% text=2 maxlength=5 runat=server/>
						<asp:RequiredFieldValidator id=rfvOffDayRate display=Dynamic runat=server 
								ErrorMessage="<br>Please enter a rate."
								ControlToValidate=txtOffPayRate />
						<asp:CompareValidator id="cvOffDayRate" display=dynamic runat="server" 
								ControlToValidate="txtOffPayRate" Text="<br>The value must whole number or with decimal. " 
								Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revOffDayRate 
								ControlToValidate="txtOffPayRate"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "<br>Maximum length 2 digits and 2 decimal points. "
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td>Public Holiday Rate for Daily Rated work :*</td>
					<td>
						<asp:Textbox id=txtHolidayPayRate width=50% text=3 maxlength=5 runat=server/>
						<asp:RequiredFieldValidator id=rfvHolidayDayRate display=Dynamic runat=server 
								ErrorMessage="<br>Please enter a rate."
								ControlToValidate=txtHolidayPayRate />
						<asp:CompareValidator id="cvHolidayDayRate" display=dynamic runat="server" 
								ControlToValidate="txtHolidayPayRate" Text="<br>The value must whole number or with decimal. " 
								Type="Double" Operator="DataTypeCheck"/>												
						<asp:RegularExpressionValidator id=revHolidayDayRate 
								ControlToValidate="txtHolidayPayRate"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "<br>Maximum length 2 digits and 2 decimal points. "
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td>Rest Day Rate for Monthly Rated work :*</td>
					<td>
						<asp:Textbox id=txtOffMonthRate width=50% text=1 maxlength=5 runat=server/>
						<asp:RequiredFieldValidator id=rfvOffMonthRate display=Dynamic runat=server 
								ErrorMessage="<br>Please enter a rate."
								ControlToValidate=txtOffMonthRate />
						<asp:CompareValidator id="cvOffMonthRate" display=dynamic runat="server" 
								ControlToValidate="txtOffMonthRate" Text="<br>The value must whole number or with decimal. " 
								Type="Double" Operator="DataTypeCheck"/>												
						<asp:RegularExpressionValidator id=revOffMonthRate 
								ControlToValidate="txtOffMonthRate"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "<br>Maximum length 2 digits and 2 decimal points. "
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td>Public Holiday Rate for Monthly Rated work :*</td>
					<td>
						<asp:Textbox id=txtHolidayMonthRate width=50% text=3 maxlength=5 runat=server/>
						<asp:RequiredFieldValidator id=rfvHolidayMonthRate display=Dynamic runat=server 
								ErrorMessage="<br>Please enter a rate."
								ControlToValidate=txtHolidayMonthRate />
						<asp:CompareValidator id="cvHolidayMonthRate" display=dynamic runat="server" 
								ControlToValidate="txtHolidayMonthRate" Text="<br>The value must whole number or with decimal. " 
								Type="Double" Operator="DataTypeCheck"/>												
						<asp:RegularExpressionValidator id=revHolidayMonthRate 
								ControlToValidate="txtHolidayMonthRate"
								ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
								Display="Dynamic"
								text = "<br>Maximum length 2 digits and 2 decimal points."
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td>Overtime Rate (Price of Rice) :*</td>
					<!-- Modified BY ALIM maxlength = 22 -->
					<td><asp:textbox id=txtOTRate text=0 width=50% maxlength=22 runat=server/>
						<asp:RequiredFieldValidator id=rfvOTRate 
								display=Dynamic 
								runat=server 
								ErrorMessage="<br>Please enter Overtime Rate."
								ControlToValidate=txtOTRate />
						<!-- Modified BY ALIM -->		
						<asp:RegularExpressionValidator id=revOTRate 
								ControlToValidate="txtOTRate"
								ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 2 decimal points. "
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td>Maximum Deduction %:*<br>(Please key in 0 for unlimited deduction)</td>
					<td><asp:textbox id=txtMaxDeduct text=0 maxlength=5 width=50% runat=server />
						<br>(NOT including net wages percentage deduction)
						<asp:RequiredFieldValidator id=rfvMaxDeduct 
								display=Dynamic 
								runat=server 
								ErrorMessage="<br>Please enter Maximum Deduction %."
								ControlToValidate=txtMaxDeduct />
						<asp:RegularExpressionValidator id=revMaxDeduct 
								ControlToValidate="txtMaxDeduct"
								ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,5}"
								Display="Dynamic"
								text = "<br>Maximum length 5 digits and 2 decimal points. "
								runat="server"/>
						<asp:RangeValidator id="rvMaxDeduct"
								ControlToValidate="txtMaxDeduct"
								MinimumValue="0"
								MaximumValue="100"
								Type="Double"
								EnableClientScript="True"
								Text="<br>The value must be from 0 to 100."
								runat="server"/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				<tr><td colspan=5>&nbsp;</td></tr>	
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
					</td>
				</tr>
			</table>

        <br />
        </div>
        </td>
        </tr>
        </table>

		</form>
	</body>
</html>
