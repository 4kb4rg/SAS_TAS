<%@ Page Language="vb" trace="false" src="../../../include/PR_trx_DailyAttd.aspx.vb" Inherits="PR_trx_DailyAttd" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Daily Attendance</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6"><strong>DAILY ATTENDANCE</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=12% height=25>Attendance Date :*</td>
					<td width=30%>
						<asp:Textbox id=txtAttdDate AutoPostBack=True width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtAttdDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:ImageButton id=RefreshBtn AlternateText="Refresh" imageurl="../../images/butt_refresh.gif" onclick=onClick_Refresh runat=server />
						<asp:Label id=lblRfvAttdDate text="<br>Please enter Attendance Date. " visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrAttdDate visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrAttdDateDesc visible=false text="<br>Date format should be in " runat=server/>
					</td>
					
					<td width=5%>&nbsp;</td> 
					<td width=10%>Period : </td>
					<td width=25%><asp:Label id=lblPeriod runat=server /></td>
					<td width=5%>&nbsp;</td> 
				</tr>
				<tr>
					<td height=25>Attendance ID : </td>
					<td><asp:Label id=lblAttdId runat=server/></td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Gang Code  :*</td>
					<td width=60%>
						<asp:DropDownList id=ddlGang OnSelectedIndexChanged=onChg_GangCode width=90% runat=server/> 
						<asp:Label id=lblErrGang visible=false forecolor=red text="<br>Please select Gang Code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employee Code  :*</td>
					<td>
						<asp:DropDownList id=ddlEmployee width=84% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmployee visible=false forecolor=red text="<br>Please select Employee Code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Attendance Code :*</td>
					<td>
						<asp:DropDownList id=ddlAttdCode  OnSelectedIndexChanged=onSelect_Attendance width=90% runat=server/> 
						<asp:Label id=lblErrAttdCode visible=false forecolor=red text="<br>Please select Attendance Code." runat=server/>
						<asp:Label id=lblCountDayType visible=false runat=server/>
					</td>					
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr id=TrRemainingHours runat=server visible=false>
					<td height=25>Remaining Hours:</td>
					<td><asp:Label id=lblRemainHour runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
            </table>
            <table  width="100%"  cellspacing="0" cellpadding="4" border="0" align="center" runat="server" class="font9Tahoma">
            <tr>
            <td>

				<tr>
					<td colspan=6>
						<table id="tblSelection" width="100%" class="sub-add" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% runat="server" class="font9Tahoma">
										<tr>
											<td height=25><asp:label id="lblAccount" runat="server" /> (DR) :* </td>
											<td colspan=3>
												<asp:textbox id=txtAccount width=25% runat=server/> 
												<input type="button" id=buttonSearch1 value=" ? " onclick="javascript:findcodeNew('frmMain','','txtAccount','','','','','','','','','','','','','','','');" runat=server/>
												<asp:label id=lblAccountDesc width=50% runat=server/> 
												<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c">
											<td height=25>Charge Level :*</td>
											<td colspan=3>
												<asp:DropDownList id="ddlChargeLevel" autopostback=true OnSelectedIndexChanged=onSelect_ChangeLevel  Width=25%  runat=server /> 
											</td>
										</tr>
										<tr id="RowPreBlock" class="mb-c">
											<td height=25><asp:label id=lblPreBlock Runat="server"/> :</td>
											<td colspan=3>
												<asp:textbox id="txtPreBlock" Width=25% runat=server />
												<input type="button" id=buttonSearch2 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','txtPreBlock','','','','','','','','','','','','0','');" runat=server/>
												<asp:label id=lblPreBlockDesc width=50% runat=server/> 
												<asp:label id=lblErrPreBlock Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr id="RowPreBlock1" class="mb-c">
											<td height=25><asp:label id=lblPreBlock1 Runat="server"/> :</td>
											<td colspan=3>
												<asp:textbox id="txtPreBlock1" Width=25% runat=server />
												<input type="button" id=buttonSearch5 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','txtPreBlock1','','','','','','','','','','','1','');" runat=server/>
												<asp:label id=lblPreBlockDesc1 width=50% runat=server/> 
												<asp:label id=lblErrPreBlock1 Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
											<td colspan=3>
												<asp:textbox id=txtVeh width=25% runat=server/>
												<input type="button" id=buttonSearch3 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','txtVeh','','','','','','','','','','','');" runat=server/>
												<asp:label id=lblVehDesc width=50% runat=server/> 
												<asp:Label id=lblErrVeh visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td colspan=3>
												<asp:textbox id=txtVehExp width=25% runat=server/>
												<input type="button" id=buttonSearch4 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','','txtVehExp','','','','','','','','','','');" runat=server/>
												<asp:label id=lblVehExpDesc width=50% runat=server/> 
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td width=15%>Premi :</td>
											<td width=35%>
												<asp:Textbox id=txtHarvestInc width=50% value=0 maxlength=21 runat=server/>
												<asp:CompareValidator id="cvHarvestInc" display=dynamic runat="server" 
													ControlToValidate="txtHarvestInc" Text="<br>The value must be whole number or 5 decimals in maximum. " 
													Type="Double" Operator="DataTypeCheck"/>
												<asp:RegularExpressionValidator id=revHarvestInc 
													ControlToValidate="txtHarvestInc"
													ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
													Display="Dynamic"
													text = "<br>Maximum length 15 digits and 5 decimal points. "
													runat="server"/>
											</td>											
											<td width=15%>OT Hours :</td>
											<td width=35%>
												<asp:Textbox id=txtOT width=50% value=0 maxlength=21 runat=server/>
												<asp:CompareValidator id="cvOT" display=dynamic runat="server" 
													ControlToValidate="txtOT" Text="<br>The value must be whole number or 5 decimals in maximum. " 
													Type="Double" Operator="DataTypeCheck"/>
												<asp:RegularExpressionValidator id=revOT 
													ControlToValidate="txtOT"
													ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
													Display="Dynamic"
													text = "<br>Maximum length 15 digits and 5 decimal points. "
													runat="server"/>
											</td>
											
										</tr>
										<tr class="mb-c">
											<TD valign="top" colspan=4 height=25>
												<asp:Label id=lblErrTotal visible=false forecolor=red text="The hours worked has exceeded remaining hours.<br>" runat=server/>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
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
								<asp:TemplateColumn ItemStyle-Width="15%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccountCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%">
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
								<asp:TemplateColumn ItemStyle-Width="7%" HeaderText="Premi" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("HarvestInc"),2) %> id="lblPremi" runat="server" />  <!-- Create by Dian -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="OT Hours" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("OTHours"), 2, True, False, False),2) %> id="lblOTHours" runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label id=lblAttLnId Text='<%# Container.DataItem("AttLnId") %>' visible=false runat=server/>
										<asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
					<td colspan=3 height=25><hr style="width :100%" /> </td>
					<td>&nbsp;</td>					
				</tr>	
				
				<tr>
					<td colspan=2>&nbsp;</td>	
					<td height=25 colspan=3>Jumlah Premi : </td>
					<td>
						<asp:label id=lblTotalPremi text=0 runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>	
					<td height=25 colspan=3>Jumlah OT Hours : </td>
					<td>
						<asp:label id=lblTotalOTHours text=0 runat=server/>
					</td>
					<td>&nbsp;</td>	
				</tr>
				
				<tr align=right visible=false runat=server>
					<td colspan=2>&nbsp;</td>	
					<td height=25 colspan=3>
						<asp:label id=lblTotalHour text=0 runat=server/>
						<asp:label id=lblTotalVolume text=0 runat=server/>
					</td>
					<td>&nbsp;</td>	
				</tr>
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment><asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
								<asp:Label id=lblErrEmpValidation visible=False forecolor=red text="This employee not qualified to have annual and sick leave entitlement" runat=server />
								<asp:Label id=lblErrAnnualLeaveBalance visible=false forecolor=red text="No more annual leave for this employee this year. Please check Employee Employment page" runat=server/>
								<asp:Label id=lblErrSickLeaveBalance visible=false forecolor=red text="No more sick leave for this employee this year. Please check Employee Employment page" runat=server/>
								<asp:Label id=lblErrNoAnnualLeave visible=false forecolor=red text="Annual leave entitlement has not been set for this employee. Please check Employee Employment page" runat=server/>
								<asp:Label id=lblErrNoSickLeave visible=false forecolor=red text="Sick leave entitlement has not been set for this employee. Please check Employee Employment page" runat=server/>
								<asp:Label id=lblErrValidation visible=false forecolor=red text="" runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText=" Save " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=ConfirmBtn AlternateText="Confirm" imageurl="../../images/butt_confirm.gif" onclick=Button_Click CommandArgument=Confirm runat=server />
						<asp:ImageButton id=CancelBtn AlternateText=" Cancel " imageurl="../../images/butt_cancel.gif" onclick=Button_Click CommandArgument=Cancel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<Input Type=hidden id=attdid runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
				<asp:Label id=lblPayType visible=false text="0" runat=server/>
				<asp:Label id=lblHasShift visible=false text=false runat=server/>
				<asp:Label id=lblOTInd visible=false text=0 runat=server />
				<asp:Label id=lblOTAllowed visible=false text=1 runat=server/>
				<asp:Label id=lblQuotaLevel visible=false text=1 runat=server/>
				<asp:Label id=lblErrMessage visible=false text=1 runat=server/>
				<asp:Label id=lblFlagBindAttCode visible=false text=1 runat=server/>
				<asp:Label id=lblDayType visible=false text=1 runat=server/>
                        </td>
            </tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
