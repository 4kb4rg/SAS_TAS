<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_WPList.aspx.vb" Inherits="PR_StdRpt_WPList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Work Performance List Report</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - WORK PERFORMANCE LIST (LAPORAN HARIAN ASISTEN)</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table1">
				<tr>
					<td width=17%>Date From :</td>
					<td width=39%><asp:TextBox id="txtDateFrom" size="12" width=50% maxlength="10" runat="server"/>
  								<a href="javascript:PopCal('txtDateFrom');">
  								<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>						
					<td width=4%>To :</td>
					<td width=40%><asp:TextBox id="txtDateTo" size="12" width=50% maxlength="10" runat="server"/>
  								<a href="javascript:PopCal('txtDateTo');">
  								<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>										
				</tr>	

				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100%>
										<tr>
											<td>Account Class :*</td>
											<td><asp:DropDownList id="ddlAccClsCode" width="100%" size="1" OnSelectedIndexChanged=onSelect_AccClsCode AutoPostBack=true runat="server" />
											    <asp:Label id=lblErrAccClsCode visible=false forecolor=red text="Please select one Account Class." runat=server/></td>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
										</tr>		
										<tr>
											<td width=17%>Sub Activity From :*</td>
											<td width=39%><asp:DropDownList id="ddlSubActCodeFrom" width="100%" size="1" runat="server" />
														  <asp:Label id=lblErrSubActCode visible=false forecolor=red text="Please select one Sub Activity." runat=server/></td>
											<td width=4%>To :</td>
											<td width=40%><asp:DropDownList id="ddlSubActCodeTo" width="100%" size="1" runat="server" /></td>
										</tr>
										<tr class="mb-c">											
											<td colspan=6 height=25>
												<asp:ImageButton id=btnAdd imageurl="../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />												
												<asp:Label id=lblErrDupl visible=false forecolor=red text="Account Class already exists." runat=server/>
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
							AllowSorting="True">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>						
								<asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" HeaderText="Account Class" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccClsCode") %>  id=AccClsCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="35%" HeaderStyle-Width="20%" HeaderText="Description" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %>  id=AccDescr runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Sub Activity" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("SubActCodeFrom") %>  id=SubActCodeFrom runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="To" HeaderStyle-HorizontalAlign=Center>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Sub Activity" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("SubActCodeTo") %>  id=SubActCodeTo runat="server" />
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
					<td width=17%>Personil in Charge 1</td>
					<td width=39%><asp:textbox id="txtPIC1" maxlength=8 width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Personil in Charge 2</td>
					<td><asp:textbox id="txtPIC2" maxlength=8 width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>				
			</table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
