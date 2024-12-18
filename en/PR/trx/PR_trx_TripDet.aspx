<%@ Page Language="vb" trace="false" src="../../../include/PR_trx_TripDet.aspx.vb" Inherits="PR_trx_TripDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Trip Entry Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
         <Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">   
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblErrSelect visible=false text="Please select one " runat="server" />
			<asp:Label id=lblSelect visible=false text="Select one " runat="server" />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=tripid runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100%  class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6"><strong>TRIP ENTRY DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td height=25>Trip ID : </td>
					<td><asp:Label id=lblTripId runat=server/></td>
					<td>&nbsp;</td>
					<td>Period : </td>
					<td><asp:Label id=lblPeriod runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description : </td>
					<td><asp:Textbox id=txtDesc width=100% maxlength=128 runat=server/></td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25>Employee Code :* </td>
					<td width=30%>
						<asp:Dropdownlist id=ddlEmployee width=80% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmp visible=false forecolor=red text="<br>Please select Employee ID." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Date Created : </td>
					<td width=25%><asp:Label id=lblDateCreated runat=server/></td>
					<td width=5%>&nbsp;</td>
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
				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% runat=server  class="font9Tahoma">
										<tr>
											<td valign=top height=25 width=20%>Date of Trip :*</td>
											<td valign=top width=80% colspan=5>
												<asp:Textbox id=txtDate width=25% maxlength=10 runat=server/>
												<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
												<asp:Label id=lblErrDate text="Please enter Date of Trip." visible=false forecolor=red runat=server />
												<asp:Label id=lblErrDateFmt forecolor=red runat=server/>
												<asp:Label id=lblErrDateFmtMsg visible=false text="<br>Date Format should be in " runat=server/>
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id="lblRoute" runat="server" /> :*</td>
											<td valign=top width=80% colspan=5>
												<asp:Dropdownlist id=ddlRoute width=100% runat=server/>
												<asp:Label id=lblErrRoute visible=false forecolor=red text="<br>Please select one Route." runat=server/>
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id="lblLoad" runat="server" /> :*</td>
											<td valign=top width=80% colspan=5>
												<asp:Dropdownlist id=ddlLoad width=100% runat=server/>
												<asp:Label id=lblErrLoad visible=false forecolor=red text="<br>Please select one Load." runat=server/>
											</td>
										</tr>	
										<tr>											
											<td valign=top height=25 width=20%>Denda Code :</td>
											<td valign=top width=80% colspan=5>
												<comment>Added By BHL</comment>
												<asp:Dropdownlist id=ddlDenda width=100% runat=server/>
												<asp:Label id=lblErrDenda visible=false forecolor=red text="<br>Please select one Denda Code." runat=server/>
												<comment>End Added</comment>
											</td>											
										</tr>	
										<tr>
											<td valign=top height=25 width=20%>Total Load :*</td>
											<td valign=top width=30%>
												<asp:Textbox id=txtTotal width=50% maxlength=15 runat=server/> Kg
												<asp:CompareValidator id="cvTotal" display=dynamic runat="server" 
													ControlToValidate="txtTotal" Text="<br>The value must whole number." 
													Type="Double" Operator="DataTypeCheck"/>
												<asp:RangeValidator id="Range1"
													ControlToValidate="txtTotal"
													MinimumValue="0"
													MaximumValue="999999999999999"
													Type="double"
													EnableClientScript="True"
													Text="<br>Minimum Value From 0 - 999999999999999."
													runat="server" display="dynamic"/>
												<asp:label id=lblErrTotalTrip text="Please enter Total of Trip." visible=false forecolor=red runat=server/>
											</td>
											<td width=5%>&nbsp;</td>											
											<td valign=top height=25 width=15%>Denda Quantity :</td>
											<td valign=top width=30%>
												<comment>Added By BHL</comment>
												<asp:Textbox id=txtDendaQty width=50% maxlength=19 runat=server text="0"/> 
												<asp:CompareValidator id="cvDendaQty" display=dynamic runat="server" 
													ControlToValidate="txtDendaQty" Text="<br>The value must whole number." 
													Type="Double" Operator="DataTypeCheck"/>
												<asp:RangeValidator id="Range2"
													ControlToValidate="txtDendaQty"
													MinimumValue="0"
													MaximumValue="999999999999999"
													Type="Double"
													EnableClientScript="True"
													Text="<br>The value must be from 0."
													runat="server" display="dynamic"/>
												<asp:label id=lblErrDendaQty text="<br>Please enter Denda Quantity of Trip." visible=false forecolor=red runat=server/>
												<comment>End Added</comment>
											</td>										
											<td width=5%>&nbsp;</td>										
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id=lblAccount runat=server /> (DR) :* </td>
											<td valign=top width=80% colspan=5>
												<asp:dropdownlist id=ddlAccount Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server/>
												<input type=button value=" ... " id="Find" onclick="javascript:findcode('frmMain','','ddlAccount','9','ddlBlock','','ddlVehicle','ddlVehExpense','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
												<asp:label id=lblAccountErr Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id=lblBlock runat=server /> : </td>
											<td valign=top width=80% colspan=5>
												<asp:dropdownlist id=ddlBlock width=100% runat=server />
												<asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id=lblVehicle runat=server /> : </td>
											<td valign=top width=80% colspan=5>
												<asp:dropdownlist id=ddlVehicle width=100% runat=server />
												<asp:label id=lblVehicleErr visible=False forecolor=red runat="server" />
											</td>
										</tr>
										<tr>
											<td valign=top height=25 width=20%><asp:label id=lblVehExpense runat=server /> : </td>
											<td valign=top width=80% colspan=5>
												<asp:dropdownlist id=ddlVehExpense width=100% runat=server />
												<asp:label id=lblVehExpenseErr visible=False forecolor=red runat="server" />
											</td>
										</tr>
										<tr class="mb-c">
											<TD vAlign="top" colspan=6 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" onclick=btnAdd_Click alternatetext=Add runat=server /></TD>
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
							AllowSorting="true" CssClass="font9Tahoma" >
							
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
								<asp:TemplateColumn HeaderText="Date of Trip">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetShortDate(Session("SS_DATEFMT"), Container.DataItem("TripDate")) %> id="lblTripDate" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>																
								<asp:TemplateColumn HeaderText="Route">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("RouteCode") %> id="lblRouteCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Load">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("LoadCode") %> id="lblLoadCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Denda">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("DendaCode") %> id="lblDendaCode" runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Load">
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("TotalTrip"),5) %> id="lblTotalTrip" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Denda Qty">
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("DendaQty"),5) %> id="lblDendaQty" runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Account">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Block">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
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
								<asp:TemplateColumn HeaderText="Rate">
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Rate")) %> id="lblRate" runat="server" /> <!--Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label id=lblTripLnId visible=false text=<%# Container.DataItem("TripLnId") %> runat=server/>
										<asp:LinkButton id=lbDelete CommandName=Delete CausesValidation=False Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>					
					
					
					</td>
				</tr>	
				<tr>
					<td colspan=6>&nbsp;</td>
				<td>			
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=CancelBtn AlternateText=" Cancel " imageurl="../../images/butt_cancel.gif" onclick=Button_Click CommandArgument=Cancel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidPayrollInd value=2 runat=server />
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
