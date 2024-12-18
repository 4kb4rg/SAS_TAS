<%@ Page Language="vb" trace="false" src="../../../include/PR_trx_WeeklyAttd.aspx.vb" Inherits="PR_trx_WeeklyAttd" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Weekly Attendance</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<preference:prefHdl id=PrefHdl runat="server" />
		<form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                     <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><usercontrol:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6"><strong>WEEKLY ATTENDANCE</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25>Gang Code :*</td>
					<td width=30%><asp:dropdownlist id=ddlGang width=100% autopostback=true onselectedindexchanged=onChg_Gang runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Period : </td>
					<td width=25%><asp:label id=lblPeriod runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>(select either Gang or Employee)</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employee Code  :*</td>
					<td><asp:dropdownlist id=ddlEmployee onselectedindexchanged=onChg_Employee  width=89% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:label id=lblErrGangOrEmp visible=false forecolor=red text="Please select Gang or Employee." runat=server/>
						<asp:label id=lblErrEither visible=false forecolor=red text="Please select either Gang or Employee only." runat=server />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Attendance Code : </td>
					<td>
						<asp:dropdownlist id=ddlAttd OnSelectedIndexChanged=onSelect_Attendance width=89% runat=server/>
						<asp:RequiredFieldValidator id=rfvAttdCode 
							display=Dynamic runat=server 
							ErrorMessage="Please select Attendance Code."
							ControlToValidate=ddlAttd 
							runat=server/>
						<asp:label id=lblCountDayType text="" visible=false runat=server />
						<asp:label id=lblErrAttCode text="Please select Attendance Code." visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Attendance Date From :*</td>
					<td>
						<asp:textbox id=txtAttdDateFrom ontextchanged=onChg_Date autopostback=true width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtAttdDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:label id=lblErrAttdDate visible=false text="<br>Invalide range of Attendance Date." forecolor=red runat=server/>
						<asp:label id=lblErrMaxDate visible=false text="<br>Maximum range of date is 7 days." forecolor=red runat=server/>
						<asp:label id=lblErrAttdDateDesc visible=false text="<br>Date format is " runat=server/>												
					</td>
					<td>To : </td>
					<td colspan=3>
						<asp:textbox id=txtAttdDateTo ontextchanged=onChg_Date autopostback=true width=40% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtAttdDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:ImageButton id=RefreshBtn AlternateText="Refresh" imageurl="../../images/butt_refresh.gif" onclick=onClick_DateRange CausesValidation=false runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% runat="server">
										<tr id=TrOnePayType valign=top visible=false runat=server>
											<td height=25>&nbsp;</td>
											<td colspan=3><asp:checkbox id=cbOnePayType value=1 text="Applies to gang member with the selected pay type as above ." visible=false runat=server/></td>
										</tr>
										<tr valign=top>
											<td height=25><asp:label id="lblAccount" runat="server" /> (DR) :* </td>
											<td colspan=3>
												<asp:textbox id=txtAccount width=25% runat=server/> 
												<input type="button" id=buttonSearch1 value=" ? " onclick="javascript:findcodeNew('frmMain','','txtAccount','','','','','','','','','','','','','','','');" runat=server/>
												<asp:label id=lblAccountDesc width=50% runat=server/> 
												<asp:label id=lblErrAccount visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c" valign=top>
											<td height=25>Charge Level :*</td>
											<td colspan=3>
												<asp:DropDownList id="ddlChargeLevel" autopostback=true OnSelectedIndexChanged=onSelect_ChangeLevel Width=25% runat=server /> 
											</td>
										</tr>
										<tr id="RowPreBlock" class="mb-c" valign=top>
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
										<tr valign=top>
											<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
											<td colspan=3>
												<asp:textbox id=txtVeh width=25% runat=server/>
												<input type="button" id=buttonSearch3 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','txtVeh','','','','','','','','','','','');" runat=server/>
												<asp:label id=lblVehDesc width=50% runat=server/> 
												<asp:label id=lblErrVeh visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr valign=top>
											<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
											<td colspan=3>
												<asp:textbox id=txtVehExp width=25% runat=server/>
												<input type="button" id=buttonSearch4 value=" ? " onclick="javascript:findcodeNew('frmMain','','','','','','','txtVehExp','','','','','','','','','','');" runat=server/>
												<asp:label id=lblVehExpDesc width=50% runat=server/> 
												<asp:label id=lblErrVehExp visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td height=25 width=20%>Premi :</td>
											<td width=35%>
												<asp:Textbox id=txtHarvestInc width=60% value=0 maxlength=19 runat=server/>
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
											<td colspan=2>&nbsp;</td>
										</tr>		
										<tr id=TrErrorData visible=false runat=server>
											<td valign=top colspan=4 height=25><asp:label id=lblErrTotal visible=false forecolor=red runat=server /></td>
										</tr>
										<tr class="mb-c">
											<td valign="top" colspan=4 height=25>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
											</td>
										</tr>
										<tr>
											<td>
												<asp:label id=lblvaldoubledata visible=false text="Data already input !" forecolor=red runat=server/>
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
							AllowSorting="True" CssClass="font9Tahoma" >
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
								<asp:TemplateColumn HeaderText="Gang" HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top>
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("GangCode") %> id="lblGangCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Employee" HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top ItemStyle-Width="7%">
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("_EmpDesc") %> id="lblEmpDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Date" HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top ItemStyle-Width="7%">
									<ItemTemplate>
										<asp:label text=<%# objGlobal.GetLongDate(Container.DataItem("AttDate")) %> id="lblattdate" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top>
									<ItemTemplate>
										<asp:label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top ItemStyle-Width="7%">
									<ItemTemplate>
										<asp:label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top ItemStyle-Width="7%">
									<ItemTemplate>
										<asp:label Text=<%# Container.DataItem("ExpenseCode") %> id="lblVehExpCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Premi" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("HarvestInc"),2) %> id="lblPremi" runat="server" />  <!-- Create by Dian -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign=Right HeaderStyle-VerticalAlign=Top ItemStyle-VerticalAlign=Top>
									<ItemTemplate>
										<asp:Label id="lblChargeLocCode" Text=<%# Container.DataItem("ChargeLocCode") %> visible=false runat="server" />
										<asp:label id=lblAttId text=<%# Container.DataItem("AttId") %> visible=false runat=server />
										<asp:label id=lblAttLnId Text=<%# Container.DataItem("AttLnId") %> visible=false runat=server/>
										<asp:label id=lblStatus text=<%# Container.DataItem("Status") %>  visible=false runat=server />
										<asp:label id=lblTotalHour text=<%# Container.DataItem("TotalHours") %>  visible=false runat=server />
										<asp:label id=lblTotalVolume text=<%# Container.DataItem("TotalVolume") %>  visible=false runat=server />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
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
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:label id=lblVehOption visible=false text=false runat=server/>
						<asp:label id=lblHasShift visible=false text=false runat=server/>				
						<asp:label id="lblErrSelect" visible="false" text="Please select " runat="server" />
						<asp:label id="lblSelect" visible="false" text="Select " runat="server" />
						<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
						<asp:label id=lblEmpName visible=false text="" runat=server/>
						<asp:label id=lblEmpPayType visible=false text="" runat=server/>
						<asp:label id=lblEmpOTInd visible=false text="1" runat=server/>
						<asp:Label id=lblErrMessage visible=false text=1 runat=server/>
						<asp:Label id=lblFlagBindAttCode visible=false text=1 runat=server/>
						<asp:Label id=lblErrValidation visible=false forecolor=red text="" runat=server/>
						<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
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
