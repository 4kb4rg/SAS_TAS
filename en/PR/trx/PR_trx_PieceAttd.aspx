<%@ Page Language="vb" trace="false" src="../../../include/PR_trx_PieceAttd.aspx.vb" Inherits="PR_trx_PieceAttd" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Piece Rated Attendance</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                width: 394px;
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
			<asp:label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100%  class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong>  HARVESTER PRODUCTION ATTENDANCE</strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Period : <asp:Label id=lblPeriod runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
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
					<td width=20% height=25>Attendance Date :*</td>
					<td width=30%>
						<asp:Textbox id=txtAttdDate width=50% maxlength=10 runat=server/>
						<asp:Label id=lblRfvAttdDate text="<br>Please enter Attendance Date. " visible=false forecolor=red runat=server/>	
						<asp:Label id=lblErrAttdDate visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrAttdDateDesc visible=false text="<br>Date format " runat=server/>
						<a href="javascript:PopCal('txtAttdDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:ImageButton id=RefreshBtn AlternateText="Refresh" imageurl="../../images/butt_refresh.gif" onclick=onClick_Employee runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Attendance ID : </td>
					<td><asp:Label id=lblAttdId runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employee Code  :*</td>
					<td><asp:DropDownList id=ddlEmployee  width=89% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:RequiredFieldValidator id=rfvEmpCode
							display=dynamic 
							runat=server 
							ControlToValidate=ddlEmployee 
							text="Please select one Employee."/>	
						<asp:Label id=lblErrEmployee visible=false forecolor=red text="Please select one Employee" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<!--<tr>
					<td height=25>Pay Type :*</td>
					<td><asp:DropDownList id=ddlPayType width=100% runat=server/></td> -->
				<tr>
					<td height=25>Attendance Code :*</td>
					<td><asp:DropDownList id=ddlAttendance OnSelectedIndexChanged=onSelect_Attendance width=100% runat=server/>
						<asp:RequiredFieldValidator id=rfvAttendance 
							display=dynamic 
							runat=server 
							ControlToValidate=ddlAttendance 
							text="Please select one Attendance Code."/>	
						<asp:Label id=lblErrAttendance visible=false forecolor=red text="Please select Attendance Code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
                </table>

                <table  width="100%" class="font9Tahoma" cellspacing="0" cellpadding="4" border="0" align="center"  runat=server>
                <tr>
                <td>
         
				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%"  cellspacing="0" cellpadding="4" border="0" align="center" class="font9Tahoma" runat=server>
							<tr>
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% runat="server" class="font9Tahoma">
										<tr>											
											<td valign=top height=25 width=20%>Harvesting Incentive Scheme :*</td>
											<td valign=top width=80% colspan=5>
												<comment>Modified By BHL</comment>
												<asp:DropDownList id=ddlHarvInc width=99% AutoPostBack=True OnSelectedIndexChanged=ddlHarvInc_OnSelectedIndexChanged runat=server/> 
												<comment>
												<input type="button" id=btnFind4 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlHarvInc','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												</comment>
												<asp:Label id=lblErrHarvInc visible=false forecolor=red text="Please select one Harvesting Incentive Scheme." runat=server/>
												<comment>End Modified</comment>
											</td>											
										</tr>									
										<tr>
											<td height=25>Quota in Quantity :</td>
											<td>
												<asp:label id="lblQuotaInQtyVal" text="0" Width=25% runat=server /> 
											</td>
										</tr>
																						
										<tr>											
											<td valign=top height=25 width=20%>Denda Code :</td>
											<td colspan=5>
												<comment>Modified By BHL</comment>
												<asp:textbox id=txtDendaCode width=20% runat=server/> 
												<comment>
												<input type="button" id=btnFind5 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlDenda','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
												</comment>
												<input type="button" id=buttonSearch6 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','','','txtDendaCode','','','','','','','','','');" runat=server/>
												<asp:label id=lblDendaDesc width=50% runat=server/> 
												<asp:Label id=lblErrDenda visible=false forecolor=red text="Please select one Denda code." runat=server/>
												<comment>End Modified</comment>
											</td>
										</tr>					
										<tr>
											<td height=25><asp:label id="lblAccount" runat="server" /> (DR) :* </td>
											<td colspan=5>
												<asp:textbox id=txtAccount width=20% runat=server/> 
												<input type="button" id=buttonSearch1 value=" ? " onclick="javascript:findcodeNew('frmMain','','txtAccount','','','','','','','','','','','','','','','');" runat=server/>
												<!--<input type="button" id=btnFind3 value=" ... " onclick="javascript:findcode('frmMain','','ddlAccount','9',(hidBlockCharge.value==''? (document.frmMain.ddlBlock? 'ddlBlock': 'ddlSubBlock'): 'ddlPreBlock'),'','ddlVeh','ddlVehExp','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/> -->
												<asp:label id=lblAccountDesc width=40% runat=server/> 
												<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c">
											<td height=25>Charge Level :*</td>
											<td colspan=6>
												<asp:DropDownList id="ddlChargeLevel" Width=20% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> 
											</td>
										</tr>
										<tr id="RowPreBlock" class="mb-c">
											<td height=25><asp:label id=lblPreBlock Runat="server"/> :</td>
											<td colspan=5>
												<asp:textbox id="txtPreBlock" Width=20% runat=server />
												<input type="button" id=buttonSearch2 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','txtPreBlock','','','','','','','','','','','','0','');" runat=server/>
												<asp:label id=lblPreBlockDesc width=50% runat=server/> 
												<asp:label id=lblErrPreBlock Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr id="RowPreBlock1" class="mb-c">
											<td height=25><asp:label id=lblPreBlock1 Runat="server"/> :</td>
											<td colspan=5>
												<asp:textbox id="txtPreBlock1" Width=20% runat=server />
												<input type="button" id=buttonSearch5 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','txtPreBlock1','','','','','','','','','','','1','');" runat=server/>
												<asp:label id=lblPreBlockDesc1 width=50% runat=server/> 
												<asp:label id=lblErrPreBlock1 Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
											<td colspan=5>
												<asp:textbox id=txtVeh width=20% runat=server/>
												<input type="button" id=buttonSearch3 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','txtVeh','','','','','','','','','','','');" runat=server/>
												<asp:label id=lblVehDesc width=50% runat=server/> 
												<asp:Label id=lblErrVeh visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td colspan=5>
												<asp:textbox id=txtVehExp width=20% runat=server/>
												<input type="button" id=buttonSearch4 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','','txtVehExp','','','','','','','','','','');" runat=server/>
												<asp:label id=lblVehExpDesc width=50% runat=server/> 
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td width=15%>Janjang Bruto :</td>
											<td width=35%>
												<asp:textbox id=txtJanjangBruto width=50% value=0 runat=server/>
												<asp:RequiredFieldValidator id=rfvJanjangBruto display=Dynamic runat=server 
													ControlToValidate=txtJanjangBruto 
													text="Please enter value."/>
												<asp:Label id=lblErrJanjangBruto visible=false forecolor=red runat=server/>
											</td>
											<td>Lose Fruit :</td>
											<td colspan=2>
												<asp:Textbox id=txtLoseFruit text=0 width=50% maxlength=15 runat=server/>
												<asp:RequiredFieldValidator id=rfvLoseFruit display=Dynamic runat=server 
													ControlToValidate=txtLoseFruit 
													text="Please enter value."/>
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
										</tr>
										
										<tr>
											<td width=15%>Janjang Netto : </td>
											<td width=35%>
												<asp:textbox id=txtJanjangNetto width=50% value=0 runat=server/>
												<asp:RequiredFieldValidator id=rfvJanjangNetto display=Dynamic runat=server 
													ControlToValidate=txtJanjangNetto 
													text="Please enter value."/>
												<asp:Label id=lblErrJanjangNetto visible=false forecolor=red runat=server/>
											</td>
											<td>Premi Panen :</td>
											<td>
												<asp:Textbox id=txtHarvestInc width=50% value=0 maxlength=19 runat=server/>
												<asp:RequiredFieldValidator id=rfvHarvestInc display=Dynamic runat=server 
													ControlToValidate=txtHarvestInc 
													text="Please enter value."/>
												<asp:CompareValidator id="cvHarvestInc" display=dynamic runat="server" 
													ControlToValidate="txtHarvestInc" Text="<br>The value must be whole number or 5 decimals in maximum. " 
													Type="Double" Operator="DataTypeCheck" />
											</td>
										</tr>


										<tr>
											<td width=15%>Denda Quantity :</td>
											<td width=35%>
												<asp:Textbox id=txtDendaQty text=0 width=50% maxlength=19 runat=server/>
												<asp:RequiredFieldValidator id=rfvDendaQty display=Dynamic runat=server 
													ControlToValidate=txtDendaQty 
													text="Please enter value."/>
												<asp:CompareValidator id="cvPenaltyQty" display=dynamic runat="server" 
													ControlToValidate="txtDendaQty" Text="The value must whole number or with decimal. " 
													Type="Integer" Operator="DataTypeCheck"/>												
												<asp:RegularExpressionValidator id=revPenaltyQty
													ControlToValidate="txtDendaQty"
													ValidationExpression="\d{1,19}"
													Display="Dynamic"
													text = "Maximum length 19 digits and 0 decimal points. "
													runat="server"/>
												<asp:Label id=lblErrPenaltyQty visible=false forecolor=red text="Please enter Denda Quantity." runat=server/>
											</td>
											<td>OT Hours:</td>
											<td width=35%>
												<asp:Textbox id=txtOTHours width=50% value=0 maxlength=19 runat=server/>
											</td>
											<td colspan=2>&nbsp;</td>
										</tr>
										<tr class="mb-c">
											<td vAlign="top" colspan=4 height=25>
												<asp:Label id=lblErrTotal visible=false forecolor=red text="<br>The hours worked has exceeded remaining hours.<br>" runat=server/>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
											</td>
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
							AllowSorting="True" CssClass="font9Tahoma">
							
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
								<asp:TemplateColumn ItemStyle-Width="7%" HeaderText="Denda Code">
									<ItemTemplate>
										<comment>Added By BHL</comment>
										<asp:Label Text=<%# Container.DataItem("DendaCode") %> id="lblDendaCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccountCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("SubBlkCode") %> id="lblBlockCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehicleCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ExpenseCode") %> id="lblExpenseCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Janjang Bruto" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("UnitWorked"), 2, True, False, False),2) %> id="lblHours" runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Janjang Netto" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("UnitWorkNet"),2) %> id="lblUnitWorked" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Lose Fruit" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>									
									<ItemTemplate>
										<comment>Added By BHL</comment>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("LoseFruit"),2) %> id="lbldgLoseFruit" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn HeaderText="Denda Qty" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>									
									<ItemTemplate>
										<comment>Added By BHL</comment>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("DendaQty"),2) %> id="lbldgDendaQty" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Premi Panen" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("HarvestInc"),2) %> id="lblPremi" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="OT Hours" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("OTHours"),2) %> id="lblOTHours" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label id="lblChargeLocCode" Text=<%# Container.DataItem("ChargeLocCode") %> visible=false runat="server" />
										<asp:Label id=lblAttLnId Text='<%# Container.DataItem("AttLnId") %>' visible=false runat=server/>
										<asp:LinkButton id=lbDelete CommandName=Delete Text="Delete All" runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr> 

					<td colspan=3>&nbsp;</td>
					<td colspan=2 height=25><hr style="width :100%" />  </td>
					<td>&nbsp;</td>					
				</tr>	
				<tr>
					<td colspan=2>&nbsp;</td>			
					<td colspan=2 height=25>Total Janjang Bruto :</td>
					<td>		
						<asp:Label ID=lblTotalJanjangBruto visible=True Runat=server />
					</td>
				</tr>	
				<tr>	
					<td colspan=2>&nbsp;</td>			
					<td colspan=2 height=25>Total Janjang Netto : </td>
					<td>
						<asp:Label ID=lblTotalJanjangNetto visible=True Runat=server />
					</td>
				</tr>				
				<tr>
					<td colspan=2>&nbsp;</td>			
					<td colspan=2 height=25>Total Denda : </td>
					<td>
						<asp:Label ID=lblTotalDenda visible=True Runat=server />
					</td>
				</tr>	
				<tr>	
					<td colspan=2>&nbsp;</td>			
					<td colspan=2 height=25>Jumlah Premi Panen : </td>
					<td>
						<asp:Label ID=lblJmlPremi visible=True Runat=server />
					</td>
				</tr>				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;<comment>Minamas FS 2.2 - Loo 18/11/2005 - START</comment><asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
									<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
									<comment>Minamas FS 2.2 - Loo 18/11/2005 - END</comment>
									<asp:Label id=lblErrValidation visible=false forecolor=red text="" runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText=" Save " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=ConfirmBtn AlternateText="Confirm" imageurl="../../images/butt_confirm.gif" onclick=Button_Click CommandArgument=Confirm runat=server />
						<asp:ImageButton id=CancelBtn AlternateText=" Cancel " imageurl="../../images/butt_cancel.gif" onclick=Button_Click CommandArgument=Cancel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=attdid runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
				<asp:Label id=lblEmpPayType visible=false text="0" runat=server/>
				<asp:Label id=lblAttdPayType visible=false text="0" runat=server />
				<asp:Label id=lblHasShift visible=false text=false runat=server/>
				<asp:Label id=lblOTInd visible=false text=0 runat=server />
				<asp:Label id=lblPayType visible=false text=0 runat=server />
				<asp:Label id=lblCountDayType visible=false forecolor=red text="" runat=server/>
				<asp:Label id=lblErrMessage visible=false forecolor=red text="" runat=server/>
				<asp:Label id=lblDayType visible=false text=1 runat=server/>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
        </div>
        </td>
        </tr>
               </td>
                </tr>
        </table>
		</form>
	</body>
</html>
