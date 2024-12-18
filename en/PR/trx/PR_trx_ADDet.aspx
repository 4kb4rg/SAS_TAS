<%@ Page Language="vb" src="../../../include/PR_trx_ADDet.aspx.vb" Inherits="PR_trx_ADDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Allowance and Deduction Entry Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style3
            {
                width: 20%;
            }
        </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="<br>Please select " runat="server" />
			<asp:label id=lblSelect visible=false text="Select " runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=0 width=100% class="font9Tahoma">
            <tr>
            <td class="style3">
        
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                    ALLOWANCE AND DEDUCTION ENTRY DETAILS</td>
                                <td class="font9Header" style="text-align: right">
                                    Period : <asp:Label id=lblPeriod runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| 
                                    Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 class="style3">Allowance & Deduction ID : </td>
					<td width=30%><asp:Label id=lblADTrxID runat=server/></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 class="style3">Description :</td>
					<td><asp:Textbox id=txtDesc width=100% maxlength=128 runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 class="style3">Employee Code :*</td>
					<td><asp:DropDownList id=ddlEmployee width=80% runat=server onselectedindexchanged=onChange_Employee autopostback=true/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmp visible=false forecolor=red text="Please select one Employee." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 class="style3">Transaction Type :*</td>
					<td><asp:Dropdownlist id=ddlTrxType width=100% autopostback=true onselectedindexchanged=onSelect_ChangeTrxType runat=server>
							<asp:ListItem value="" selected>Select one Transaction Type</asp:ListItem>
							<asp:ListItem value="1">One Time</asp:ListItem>
							<asp:ListItem value="2">Recurring</asp:ListItem>
							<asp:ListItem value="3">Permanent</asp:ListItem>
							<asp:ListItem value="4">Reducing</asp:ListItem>
							<asp:ListItem value="5">Percentage Deduction on Net Wages</asp:ListItem>
							<asp:ListItem value="6">Daily</asp:ListItem>
						</asp:DropDownList>
						<asp:Label id=lblErrTrxType visible=false forecolor=red text="Please select Transaction Type." runat=server/>
						<asp:Label id=lblTrxTypeInd visible=false forecolor=red runat=server/>
					</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 class="style3"><asp:Label id=lblEffDate runat=server visible=false>Effective Date :*</asp:label></td>
					<td>
						<asp:Textbox id=txtEffDate onTextChanged=onChg_EffDate AutoPostBack=True width=50% maxlength=10 runat=server visible=false/>
						<a href="javascript:PopCal('txtEffDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif" visible=false/></a>
						<asp:Label id=lblRfvEffDate text="<br>Please enter Effective Date. " visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrEffDate visible=false forecolor=red runat=server/>	
						<asp:Label id=lblErrEffDateDesc visible=false text="<br>Date format should be in " runat=server/>					
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr id=trSpacer1 runat=server visible=false>
					<td height=2 colpsan=6 class="style3">&nbsp;</td>					
				</tr>
                    </td>
            </tr>
                </table>
               <table   width="100%" cellspacing="0" cellpadding="0" border="0" align="center" class="font9Tahoma"  runat=server>
				<tr id=trDetailDaily class="font9Tahoma" runat=server>
					<td colspan=6>
						<table id="tblDetailDaily"  width="100%" cellspacing="0" cellpadding="0" border="0" align="center" class="font9Tahoma" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% class="font9Tahoma">											
										<tr>
											<td height=25 width=20%>Allowance & Deduction Code :*</td>
											<td><asp:Dropdownlist id=ddlADCode width=60% autopostback=true onselectedindexchanged=onChange_ADCode runat=server/> 
												<input type="button" id=btnFind2 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlADCode','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												<asp:Label id=lblErrADCode visible=false forecolor=red text="<br>Please select one Allowance and Deduction Code." runat=server/>
												<asp:Label id=lblhouseind visible=false runat=server/>
												<asp:Label id=lblTransportInd visible=false runat=server/>
												<asp:Label id=lblMealInd visible=false runat=server/>
												<asp:Label id=lblMaternityInd visible=false runat=server/>
												<asp:Label id=lblMedicalInd visible=false runat=server/>
												<asp:Label id=lblRelocationInd visible=false runat=server/>
												<asp:label id=lblADCodeNotExist visible=false forecolor=red text="<br>Allowance and Deduction Code entered is invalid for the selected transaction type." runat=server />
												<asp:label id=lblMedicalOnTypeAD visible=false forecolor=red text="<br>Please select Recurring as AD Type for Medical Allowance !" runat=server />
												<asp:label id=lblErrTypeAD visible=false forecolor=red text="<br>Please select One Time as Transaction Type for selected AD Code !" runat=server />
											</td>									
											 
										</tr>
										<tr>
											<td height=25><asp:label id="lblAccount" runat="server" /> :*</td>
											<td><asp:DropDownList id=ddlAccount width=60% onselectedindexchanged=onSelect_Account autopostback=true runat=server/> 
												<input type="button" id=btnFind3 value=" ... " onclick="javascript:findcode('frmMain','','ddlAccount','9','ddlBlock','','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
											</td>																					
										</tr>
										<tr>
											<td height=25><asp:label id="lblBlock" runat="server" /> :</td>
											<td><asp:DropDownList id=ddlBlock width=60% runat=server/>
												<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
											</td>																						
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
											<td><asp:Dropdownlist id=ddlVehCode width=60% runat=server/>
												<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/>
											</td>																					
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td><asp:Dropdownlist id=ddlVehExpCode width=60% runat=server/>
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>
											</td>																						
										</tr>
										<tr id=trSpacer2 runat=server>
											<td height=2 colpsan=2>&nbsp;</td>					
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>	
				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="0" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100%>										
										<tr id=trPeriod runat=server>
											<td height=25>Effective Period :*</td>
											<td><asp:Textbox id=txtEffPeriod width=50% maxlength=7 runat=server/>&nbsp;&nbsp;MM/YYYY
												<asp:Label id=lblErrEffPeriod visible=false forecolor=red text="<br>Effective Period must be in MM/YYYY format." runat=server/>
												<asp:Label id=lblErrAccPeriod visible=false forecolor=red text="<br>Only current or future Account Period is acceptable." runat=server/>
											</td>
											<td height=25>For number of month(s) :*</td>
											<td><asp:Textbox id=txtMonth width=50% maxlength=2 runat=server/>
												<asp:Label id=lblErrMonth visible=false forecolor=red text="Please enter For Number of month(s)." runat=server/>
												<asp:RangeValidator id="rvMonths"
													ControlToValidate="txtMonth"
													MinimumValue="0"
													MaximumValue="99"
													Type="Double"
													EnableClientScript="True"
													Text="Invalid range of months."
													runat="server"/>
											</td>
										</tr>
										<tr>
											<td height=25 width=20%><asp:label id=lblAmountPercentage text="Amount" runat=server/> :*</td>
											<td width=30%>
												<!-- Modified BY ALIM maxlength = 22, Validation Exp and Text, and add CompareValidator -->
												<asp:Textbox id=txtAmount width=100% maxlength=22 runat=server/>
												<asp:Label id=lblErrAmount visible=false forecolor=red text="Please enter Amount." runat=server/>
												<asp:Label id=lblInvalidRange visible=false forecolor=red text="Invalid range of Amount." runat=server/>
												<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
													ControlToValidate="txtAmount" Text="The value must whole number or with decimal. " 
													Type="Double" Operator="DataTypeCheck"/>																								
												<asp:RegularExpressionValidator id=revAmount 
													ControlToValidate="txtAmount"
													ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 2 decimal points. "
													runat="server"/>
											</td>
											<td height=25 width=20%><asp:label id=lblBalAmount text="Balance Amount" visible=false runat=server/></td>
											<td width=30%>
												<!-- Modified BY ALIM maxlength = 22, Validation Exp and Text, and add CompareValidator -->
												<asp:Textbox id=txtBalAmount visible=false width=100% maxlength=22 runat=server/>
												<asp:Label id=lblErrBalAmount visible=false forecolor=red text="No Balance Amount." runat=server/>
												<asp:Textbox id=txtBalance visible=false width=100% maxlength=22 runat=server/>
												<asp:Textbox id=txtBalAmount1 visible=false width=100% maxlength=22 runat=server/>
											</td>
										</tr>
										<tr class="mb-c">											
											<TD colspan=6 height=25>
												<asp:Label id=lblErrOneTime visible=false forecolor=red text="You have choosen ONE TIME transaction type. Only 1 month can be added.<br>" runat=server/>
												<asp:Label id=lblErrPermanent visible=false forecolor=red text="You have choosen PERMANENT transaction type. Therefore, you can only add one effective period record for 1 month. The record will be increased automatically once month end payroll is generated.<br>" runat=server/>
												<asp:Label id=lblErrPercentage visible=false forecolor=red text="You have choosen PERCENTAGE transaction type and, should not less than 0% or exceed 100%.<br>" runat=server/>
												<asp:Label id=lblErrPeriodFound visible=false forecolor=red text="You can only add those available effective period but not period that has been added before.<br>" runat=server/>
												<asp:Label id=lblErrChecking visible=false forecolor=red text="This employee not qualified for having House Rent Allowance.<br>" runat=server/>
												<asp:Label id=lblErrHouseRent visible=false forecolor=red text="This employee has already got the house rent allowance for this effective year period.<br>" runat=server/>
												<asp:Label id=lblErrMedical visible=false forecolor=red text="This employee already set to have Medical Allowance.<br>" runat=server/>
												<asp:Label id=lblErrMedical1 visible=false forecolor=red text="No Medical Allowance Rate setup yet.<br>" runat=server/>
												<asp:Label id=lblErrMedical2 visible=false forecolor=red text="The Balance Amount for Medical Allowance is equal 0.<br>" runat=server/>
												<asp:Label id=lblErrMedical3 visible=false forecolor=red text="No Medical Rate setup for that employee and that AD Code. Please choose another AD Code<br>" runat=server/>
												<asp:Label id=lblErrTransport visible=false forecolor=red text="This employee not qualified for having Transport Allowance.<br>" runat=server/>
												<asp:Label id=lblErrMeal visible=false forecolor=red text="This employee not qualified for having Meal Allowance.<br>" runat=server/>
												<asp:Label id=lblErrMaternity visible=false forecolor=red text="This employee not qualified for having Maternity Allowance.<br>" runat=server/>
												<asp:Label id=lblErr3Maternity visible=false forecolor=red text="This employee has been given 3 times Maternity Allowance.<br>" runat=server/>
												<asp:Label id=lblErrNoMaternity visible=false forecolor=red text="No Maternity Allowance amount setup yet.<br>" runat=server/>
												<asp:Label id=lblErrRelocation visible=false forecolor=red text="This employee not qualified for having Relocation Allowance.<br>" runat=server/>
												<asp:Label id=lblErrNoRelocation visible=false forecolor=red text="No Relocation Allowance amount setup yet.<br>" runat=server/>
												<asp:Label id=lblErrTransGiven visible=false forecolor=red text="Transport Allowance for this employee has been given.<br>" runat=server/>
												<asp:Label id=lblErrMealGiven visible=false forecolor=red text="Meal Allowance for this employee has been given.<br>" runat=server/>
												<asp:Label id=lblErrCheckADCode visible=false forecolor=red text="AD Code selected above is not compatible with Career Progress screen for selected employee.<br>" runat=server/>
												<asp:Label id=lblFlgMedCheckEmp visible=false text="" runat=server/>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add visible=true onclick=btnAdd_Click runat=server />
											</TD>
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
							Pagerstyle-Visible=False
							AllowSorting="True" class="font9Tahoma">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
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
								<asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Allowance & Deduction Code" visible=false>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ADCode") %>  id=ADCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>		
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="" visible=false>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %>  id=AccCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="" visible=false>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCode") %>  id=BlkCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="" visible=false>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehCode") %>  id=VehCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="" visible=false>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ExpenseCode") %>  id=ExpenseCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>			
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Period">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("EffMonth") %> visible=false id=EffMonth runat="server" />
										<asp:Label Text=<%# Container.DataItem("EffYear") %> visible=false id=EffYear runat="server" />
										<asp:Label Text=<%# Container.DataItem("Period") %> visible=true id=lblPeriod runat="server" />
										<asp:Label Text=<%# Container.DataItem("Amount") %> visible=false id=Amount runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Status">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Status") %> visible=false id="Status" runat="server" />
										<asp:Label Text=<%# objPRTrx.mtdGetAdTrxLnStatus(Container.DataItem("Status")) %> id="lblStatus" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="" ItemStyle-HorizontalAlign=Right HeaderStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmount" runat="server" />  <!-- Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server CausesValidation=False />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
					<td colspan=2 height=25><hr style="width :100%" /> </td>
					<td width="5%">&nbsp;</td>					
				</tr>	
				<tr>
					<td colspan=3>&nbsp;</td>			
					<td height=25>Total Amount/Percent : </td>
					<td align=right><asp:label id="lblTotalAmount" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>									
				<tr>
					<td colspan="6">
						<asp:Label id=lblErrCheckBal visible=false forecolor=red text="Exceeding Amount.<br>" runat=server/>
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
				<input type=Hidden id=adid runat=server />
				<input type=hidden id=hidBlockCharge value="" runat=server/>
				<input type=hidden id=hidChargeLocCode value="" runat=server/>
				<input type=hidden id=hidTrxType value=0 runat=server/>		
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblADType visible=false text="0" runat=server/>
				<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
				<asp:Label id=lblPercentTag visible=false text="Percent" runat=server />
				<asp:Label id=lblAmountTag visible=false text="Amount" runat=server />
				<asp:label id=lblCloseExist visible=false text="no" runat=server/>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
