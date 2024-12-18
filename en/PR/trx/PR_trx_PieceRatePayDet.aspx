<%@ Page Language="vb" src="../../../include/PR_trx_PieceRatePayDet.aspx.vb" Inherits="PR_trx_PieceRatePayDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Harvester Production Payment Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		//start new requirement LVY
			function calRate() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtTotalUnits.value);
				var b = parseFloat(doc.txtAmount.value);
				if (a == 0) {
					doc.txtRate.value = '';
				}
				else {
					//doc.txtRate.value = round((b/a),5);  // Remarked BY ALIM
					doc.txtRate.value = round((b/a),2);
					doc.txtTotalUnits.value = round(a,2);  // Added BY ALIM
				}
				//if (doc.txtRate.vallue == 'NaN') // remarked BY ALIM
				if (doc.txtRate.value == 'NaN')
					doc.txtRate.value = '';
			}
		//end new requirement by LVY
		
			function calAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtTotalUnits.value);
				var b = parseFloat(doc.txtRate.value);
				doc.txtAmount.value = round((a * b),2);
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
			}
			
			function calTotalBunch() {
				//need to retrive the bunch ratio for the block selected, 
				//then multiply with the intial Total Bunch Value.
				var doc = document.frmMain;
				var a = parseFloat(doc.txtRipeBunch.value);
				var b = parseFloat(doc.txtUnripeBunch.value);
				var ratio = parseFloat(doc.hidBunchRatio.value);
				doc.txtBunch.value = Math.round((a + b)*ratio);
				if ((doc.txtBunch.value == 'NaN')||(a == 'NaN')||(b == 'NaN'))
					doc.txtBunch.value = '';
			}

		</script>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">   
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="lblErrSelect" visible="false" text="Please select one " runat="server" />
			<asp:label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			<asp:label id=lblTxLnID visible=false runat=server />					
			<Input Type=Hidden id=payid runat=server />
			<input type=hidden id=hidHarvAutoWeight value=2 runat=server />
			<input type=hidden id=hidHarvGroupWeight value=2 runat=server />
			<input type=hidden id=hidHarvDailyWeight value=2 runat=server />
			<input type=hidden id=hidBunchRatio value=1 runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><strong>HARVESTER PRODUCTION PAYMENT DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />  </td>
				</tr>
				<tr>
					<td width=20% height=25>Harvester Production ID : </td>
					<td width=30%><asp:Label id=lblPieceRateID runat=server/></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Period : </td>
					<td width=25%><asp:Label id=lblPeriod runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description / Harvester Production No. :</td>
					<td><asp:textbox id=txtDesc width=100% maxlength=64 runat=server/></td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign=top height=25 >Date of Payment :*</td>
					<td valign=top width=30%>
						<asp:Textbox id=txtDate width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrDate text="<br>Please enter Date of Payment." visible=false forecolor=red runat=server />
						<asp:Label id=lblErrDateFmt forecolor=red runat=server/>
						<asp:Label id=lblErrDateFmtMsg visible=false text="<br>Date Format should be in " runat=server/>
					</td>					
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr class="font9Tahoma">
					<td colspan=6>
						<table id="tblSelection" width="100%" class="sub-add" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% runat=server class="font9Tahoma">
										<tr>
											<td valign=top height=25 width=20%>Employee Code :*</td>
											<td valign=top width=80% colspan=5>
												<asp:DropDownList id=ddlEmployee width=90% runat=server/> 
												<input type="button" id=btnFind2 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												<asp:Label id=lblErrEmployee visible=false forecolor=red text="Please select one Employee code." runat=server/>
											</td>
										</tr>
										<tr>											
											<td valign=top height=25 width=20%>Harvesting Incentive Scheme :*</td>
											<td valign=top width=80% colspan=5>
												<comment>Modified By BHL</comment>
												<asp:DropDownList id=ddlHarvInc width=100% AutoPostBack=True OnSelectedIndexChanged=ddlHarvInc_OnSelectedIndexChanged runat=server/> 
												<comment>
												<input type="button" id=btnFind4 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlHarvInc','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												</comment>
												<asp:Label id=lblErrHarvInc visible=false forecolor=red text="Please select one Harvesting Incentive Scheme." runat=server/>
												<comment>End Modified</comment>
											</td>											
										</tr>										
										<tr>											
											<td valign=top height=25 width=20%>Denda Code :</td>
											<td valign=top width=80% colspan=5>
												<comment>Modified By BHL</comment>
												<asp:DropDownList id=ddlDenda width=100% runat=server/> 
												<comment>
												<input type="button" id=btnFind5 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlDenda','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												</comment>
												<asp:Label id=lblErrDenda visible=false forecolor=red text="Please select one Denda code." runat=server/>
												<comment>End Modified</comment>
											</td>											
										</tr>										
										<tr id="RowChargeTo" class="mb-c">
							                <td valign=top height=25 width=20%>Charge To :*</td>
							                <td valign=top width=80% colspan=5>
								                <asp:DropDownList id="ddlLocation" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server /> 
								                <asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
							                </td>
						                </tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id="lblAccount" runat="server" /> (DR) :*</td>
											<td valign=top width=80% colspan=5>
												<asp:DropDownList id=ddlAccount width=90% onselectedindexchanged=onSelect_Account autopostback=true runat=server/> 
												<input type="button" id=btnFind3 value=" ... " onclick="javascript:findcode('frmMain','','ddlAccount','9',(hidBlockCharge.value==''? 'ddlBlock': 'ddlPreBlock'),'','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												<asp:Label id=lblErrAccount visible=false forecolor=red text="Please select one Account Code." runat=server/>
											</td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c">
											<td valign=top height=25 width=20%>Charge Level :*</td>
											<td valign=top width=80% colspan=5>
												<asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> 
											</td>
										</tr>
										<tr id="RowPreBlock" class="mb-c">
											<td valign=top height=25 width=20%><asp:label id=lblPreBlock Runat="server"/> :</td>
											<td valign=top width=80% colspan=5>
												<asp:DropDownList id="ddlPreBlock" Width=100% onselectedindexchanged=onSelect_Block autopostback=true runat=server />
												<asp:label id=lblErrPreBlock Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr id="RowBlock">
											<td valign=top height=25 width=20%><asp:label id="lblBlock" runat="server" /> :</td>
											<td valign=top width=80% colspan=5>
												<asp:DropDownList id=ddlBlock width=100% onselectedindexchanged=onSelect_Block autopostback=true runat=server/>
												<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id="lblVehicle" runat="server" /> :</td>
											<td valign=top width=80% colspan=5>
												<asp:Dropdownlist id=ddlVehCode width=100% runat=server/>
												<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td valign=top width=80% colspan=5>
												<asp:Dropdownlist id=ddlVehExpCode width=100% runat=server/>
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>												
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%>Group Reference : </td>
											<td valign=top width=80% colspan=5>
												<asp:textbox id=txtGroupRef width=25% runat=server/>											
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%>Reference Date : </td>
											<td valign=top width=80% colspan=5>
												<asp:textbox id=txtRefDate width=25% runat=server/>
												<a href="javascript:PopCal('txtRefDate');"><asp:Image id=btnSelDate runat=server ImageUrl="../../images/calendar.gif"/></a>
												<asp:label id=lblErrRefDate forecolor=red visible=false runat=server />
												<asp:label id=lblErrRefDateDesc visible=false text="<br>Date format should be in " runat=server/>											
											</td>
										</tr>										
										<tr>
											<td valign=top height=25 visible=True width=20%>Mandays :*</td>
											<td valign=top width=80% colspan=5>
												<!-- Modified BY ALIM maxlength = 7 -->
												<asp:textbox id=txtMandays visible=True text=0 width=25% maxlength=7 runat=server/>
												<asp:RequiredFieldValidator id=rfvMandays display=Dynamic runat=server 
													ControlToValidate=txtMandays 
													text="Please enter Mandays."/>	
												<asp:RegularExpressionValidator id=revMandays 
													ControlToValidate="txtMandays"
													ValidationExpression="\d{1,5}\.\d{1,1}|\d{1,5}"
													Display="Dynamic"
													text = "Maximum length 5 digits and 1 decimal point. "
													runat="server"/>
											</td>
										</tr>	
										
										<tr>
											<td valign=top height=25 width=20%>(&nbsp;x Bunches : </td>
											<td valign=top width=15%>
												<asp:Textbox id=txtRipeBunch text=0 width=100% onkeyup="javascript:calTotalBunch();" maxlength=8 runat=server/>
												<asp:RequiredFieldValidator id=rfvRipeBunch display=Dynamic runat=server 
													ControlToValidate=txtRipeBunch 
													text="Please enter number of Ripe Bunches."/>
												<asp:RegularExpressionValidator id=revRipeBunch 
														ControlToValidate="txtRipeBunch"
														ValidationExpression="\d{1,8}"
														Display="Dynamic"
														text = "The value must be a whole number with maximum length of 8 digits without decimals."
														runat="server"/>
												<asp:Label id=lblErrRipeBunch visible=false forecolor=red text="Please enter number of Ripe Bunches." runat=server/>
											</td>
											<td valign=top height=25 width=15% align=center> + Unripe Bunches </td>
											<td valign=top width=15%>
												<asp:Textbox id=txtUnripeBunch text=0 width=100% maxlength=8 onkeyup="javascript:calTotalBunch();" runat=server/>
												<asp:RequiredFieldValidator id=rfvUnripeBunch display=Dynamic runat=server 
													ControlToValidate=txtUnripeBunch 
													text="Please enter number of Unripe Bunches."/>
												<asp:RegularExpressionValidator id=revUnripeBunch 
														ControlToValidate="txtUnripeBunch"
														ValidationExpression="\d{1,8}"
														Display="Dynamic"
														text = "The value must be a whole number with maximum length of 8 digits without decimals."
														runat="server"/>
												<asp:Label id=lblErrUnripeBunch visible=false forecolor=red text="Please enter number of Unripe Bunches." runat=server/>
											</td>
											<td valign=top height=25 width=20% align=center>) X <asp:label id=lblBunchRatio text=1 runat=server />&nbsp;&nbsp;=&nbsp;&nbsp;Bunches </td>
											<td valign=top width=15%>
												<asp:Textbox id=txtBunch text=0 width=100% readonly=true maxlength=8 runat=server/>
												<asp:RegularExpressionValidator id=revBunch 
														ControlToValidate="txtBunch"
														ValidationExpression="\d{1,8}"
														Display="Dynamic"
														text = "The value must be a whole number with maximum length of 8 digits without decimals."
														runat="server"/>
												<asp:Label id=lblErrBunch visible=false forecolor=red text="Please enter the Bunch." runat=server/>
											</td>
										</tr>
										<tr>
											<td valign=top height=25>Total Units (MT) :*</td>
											<td valign=top>
												<!-- Modified BY ALIM maxlength = 22 -->
												<asp:Textbox id=txtTotalUnits text=0 OnKeyUp="javascript:calAmount();" width=100% maxlength=15 runat=server/>
												<asp:RequiredFieldValidator id=rfvTotalUnits display=Dynamic runat=server 
													ControlToValidate=txtTotalUnits 
													text="Please enter Total Units."/>
												<asp:CompareValidator id="cvTotalUnits" display=dynamic runat="server" 
													ControlToValidate="txtTotalUnits" Text="The value must whole number or with decimal. " 
													Type="Double" Operator="DataTypeCheck"/>
												<!--Modified BY ALIM -->	
												<asp:RegularExpressionValidator id=revTotalUnits 
													ControlToValidate="txtTotalUnits"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 9 digits and 5 decimal points. "
													runat="server"/>
												<asp:Label id=lblErrTotalUnits visible=false forecolor=red text="Please enter Total Units." runat=server/>
											</td>
											<td valign=top height=25 width=20% align=center> Rate :* </td>
											<td valign=top >
												<asp:Textbox id=txtRate text=0 width=100% OnKeyUp="javascript:calAmount();" maxlength=15 runat=server/>																								
												<asp:RequiredFieldValidator id=rfvRate display=Dynamic runat=server 
													ControlToValidate=txtRate 
													text="Please enter Rate."/>
												<asp:CompareValidator id="cvRate" display=dynamic runat="server" 
													ControlToValidate="txtRate" Text="The value must whole number or with decimal. " 
													Type="Double" Operator="DataTypeCheck"/>												
												<asp:RegularExpressionValidator id=revRate 
													ControlToValidate="txtRate"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 9 digits and 5 decimal points. "
													runat="server"/>
												<asp:Label id=lblErrRate visible=false forecolor=red text="Please enter Lose Fruit." runat=server/>
											</td>
											<td valign=top height=25 align=center> Amount </td>
											<td valign=top>
												<!-- Modified BY ALIM maxlength = 22 -->
												<asp:Textbox id=txtAmount Enabled=False text=0 width=100% OnKeyUp="javascript:calRate();" maxlength=22 runat=server/>
												<asp:RequiredFieldValidator id=rfvAmount display=Dynamic runat=server 
													ControlToValidate=txtAmount 
													text="Please enter Amount."/>
												<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
													ControlToValidate="txtAmount" Text="The value must whole number or with decimal. " 
													Type="Double" Operator="DataTypeCheck"/>
												<!-- Modified BY ALIM -->	
												<asp:RegularExpressionValidator id=revAmount 
													ControlToValidate="txtAmount"
													ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 2 decimal points. "
													runat="server"/>
												<asp:Label id=lblErrAmount visible=false forecolor=red text="Please enter the Amount." runat=server/>
											</td>
										</tr>
										<tr>
											<td valign=top height=25>Denda Quantity :</td>
											<td valign=top >												
												<asp:Textbox id=txtDendaQty text=0 width=100% maxlength=19 runat=server/>
												<asp:RequiredFieldValidator id=rfvPenaltyQty display=Dynamic runat=server 
													ControlToValidate=txtDendaQty 
													text="Please enter Denda Quantity."/>
												<asp:CompareValidator id="cvPenaltyQty" display=dynamic runat="server" 
													ControlToValidate="txtDendaQty" Text="The value must whole number or with decimal. " 
													Type="Integer" Operator="DataTypeCheck"/>												
												<asp:RegularExpressionValidator id=revPenaltyQty
													ControlToValidate="txtDendaQty"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 5 decimal points. "
													runat="server"/>
												<asp:Label id=lblErrPenaltyQty visible=false forecolor=red text="Please enter Denda Quantity." runat=server/>
											</td>
											<td valign=top height=25 width=20% align=center><asp:Label id=lblLoseFruit text=" Lose Fruit " width=100% runat=server/></td>
											<td valign=top >
												<asp:Textbox id=txtLoseFruit text=0 width=100% maxlength=15 runat=server/>
												<asp:RequiredFieldValidator id=rfvLoseFruit display=Dynamic runat=server 
													ControlToValidate=txtLoseFruit 
													text="Please enter Lose Fruit."/>
												<asp:CompareValidator id="cvLoseFruit" display=dynamic runat="server" 
													ControlToValidate="txtLoseFruit" Text="The value must whole number or with decimal. " 
													Type="Double" Operator="DataTypeCheck"/>												
												<asp:RegularExpressionValidator id=revLoseFruits 
													ControlToValidate="txtLoseFruit"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text = "Maximum length 9 digits and 5 decimal points. "
													runat="server"/>
												<asp:Label id=lblErrLoseFruit visible=false forecolor=red text="Please enter Lose Fruit." runat=server/>
											</td>											
											<td>&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%>Remark :</td>
											<td valign=top width=80% colspan=5>
												<asp:textbox id=txtLnDesc width=100% maxlength=128 runat=server/>
											</td>
										</tr>
										
										<tr class="mb-c">
											<TD vAlign="top" colspan=6 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />&nbsp;
											<asp:ImageButton text="Save" id="btnUpdate" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnUpdate_Click" Runat="server" /></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
							OnEditCommand="DEDR_Edit"
							OnCancelCommand="DEDR_Cancel"							
							Pagerstyle-Visible=False CssClass="font9Tahoma"
							AllowSorting="True">
							
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<Columns>						
								<asp:TemplateColumn HeaderText="Employee">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("EmpCode") & "(" & Container.DataItem("EmpName") & ")" %> id="lblEmpCode" runat="server" />
										<asp:Label Text=<%# Container.DataItem("EmpCode") %> visible = false id="lblEmp" runat="server" />																				
									</ItemTemplate>
								</asp:TemplateColumn>																
								<asp:TemplateColumn HeaderText="Havesting Incentive Scheme">
									<ItemTemplate>
										<comment>Modified By BHL</comment>
										<asp:Label Text=<%# Container.DataItem("HarvIncCode") %> id="lblHarvIncCode" runat="server" />
										<comment>End Modified</comment>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Penalty Code">
									<ItemTemplate>
										<comment>Modified By BHL</comment>
										<asp:Label Text=<%# Container.DataItem("DendaCode") %> id="lblDendaCode" runat="server" />
										<comment>End Modified</comment>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Charge To">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ChargeLocCode") %> id="lblChargeLocCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Account">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Block">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlock" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Vehicle">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVeh" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Vehicle Expense">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehExpenseCode") %> id="lblVehExp" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Group Reference">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("GroupRef") %> id="lblGroupRef" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reference Date">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("RefDate") %> id="lblRefDate" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn visible=False HeaderText="Mandays" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label visible=False Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Mandays")) %> id="lblMandays" runat="server" />   <!-- Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Ripe Bunches" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("RipeBunch"), 0) %> id="lblRipeBunch" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Unripe Bunches" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("UnripeBunch"), 0) %> id="lblUnripeBunch" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Bunches" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("TotalBunch"), 0) %> id="lblBunch" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>						
								<asp:TemplateColumn HeaderText="Units" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<comment>Modified By BHL</comment>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Unit")) %> id="lblUnit" runat="server" /> <!-- Modified BY ALIM -->
										<asp:Label Text=<%# Container.DataItem("Unit") %> visible = false id="lblHUnit" runat="server" />
										<comment>End Modified</comment>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Lose Fruit" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<comment>Modified By BHL</comment>
										<asp:Label Text=<%# IIf(IsDBnull(Trim(Container.DataItem("LoseFruit"))),Container.DataItem("LoseFruit"),ObjGlobal.GetIDDecimalSeparator_FreeDigit(Val(Container.DataItem("LoseFruit")), 5)) %> id="lblLoseFruit" runat="server" /> 
										<asp:Label Text=<%# Container.DataItem("LoseFruit") %> visible = false id="lblHLoseFruit" runat="server" />
										<comment>End Modified</comment>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Denda Qty" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<comment>Added By BHL</comment>
										<asp:Label Text=<%# IIf(IsDBnull(Trim(Container.DataItem("DendaQty"))),Container.DataItem("DendaQty"),ObjGlobal.GetIDDecimalSeparator_FreeDigit(Val(Container.DataItem("DEndaQty")), 5)) %> id="lblDendaQty" runat="server" /> 
										<asp:Label Text=<%# Container.DataItem("DendaQty") %> visible = false id="lblHDendaQty" runat="server" />
										<comment>End Added</comment>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Rate" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Rate")) %> id="lblRate" runat="server" /> <!-- Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmount" runat="server" /> <!-- Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Remark" HeaderStyle-HorizontalAlign=Center >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblLnDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label id=lblPieceRateLnId visible=false text=<%# Container.DataItem("PieceRateLnId") %> runat=server/>
										<asp:Label id=lblEmpPayrollInd visible=false text=<%# Container.DataItem("PayrollInd") %> runat=server/>
										<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />										
										<asp:LinkButton id=lbDelete CommandName=Delete CausesValidation=False Text=Delete runat=server />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label id=lblPieceRateLnId visible=false text=<%# Container.DataItem("PieceRateLnId") %> runat=server/>
										<asp:Label id=lblEmpPayrollInd visible=false text=<%# Container.DataItem("PayrollInd") %> runat=server/>
										<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
										<!--<asp:LinkButton id=lbDelete CommandName=Delete CausesValidation=False Text=Delete runat=server />-->
									</EditItemTemplate>										
								</asp:TemplateColumn>	
								
								
								
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>					
				<tr>
					<td ColSpan="6">
					<table border=0 cellspacing=0 cellpadding=2 width=100%>
						<tr>
							<td colspan=5 width=60%>&nbsp;</td>
							<td colspan=2 align=right><hr style="width :100%" />  </td>
						</tr>
						<tr>
							<td colspan=5 width=60%>&nbsp;</td>											
							<td align=right>Total Unit : <asp:Label ID=lblTotalUnit Runat=server /></td>											
							<td align=right>Total Amount : <asp:Label ID=lblTotalAmount Runat=server /></td>							
						</tr>						
					</table>
					</td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
								<asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=CancelBtn AlternateText=" Cancel " CausesValidation=False imageurl="../../images/butt_cancel.gif" onclick=Button_Click CommandArgument=Cancel runat=server />
						<asp:ImageButton id=NewBtn AlternateText="  New Transaction  " CausesValidation=False imageurl="../../images/butt_new.gif" onclick=Button_Click CommandArgument=New runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidAllowCancel value="no" runat=server />
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
