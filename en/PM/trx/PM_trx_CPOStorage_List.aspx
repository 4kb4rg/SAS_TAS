<%@ Page Language="vb" trace="false" src="../../../include/PM_Trx_CPOStorage_List.aspx.vb" Inherits="PM_Trx_CPOStorage_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>CPO Storage Transaction List</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
	    <form id=frmMain  class="main-modul-bg-app-list-pu" runat=server >
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id=MenuPDTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">

			<table border=0 cellspacing=1 cellpadding=1 width=100% class="font9Tahoma">
 
				<tr>
					<td class="mt-h" colspan="3" NOWRAP>CPO STORAGE LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td style="background-color:#FFCC00" >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td style="width: 22%" style="visibility:False";>Storage Code :<BR>
									<asp:TextBox id=srchTransDate width=60% maxlength="10" runat="server"/>
									  <BR>
									<asp:label id=lblDate Text ="Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
									<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
								</td>
								<td width="15%">
                                    Period :<BR>
                                    <asp:DropDownList ID="lstAccMonth" runat="server" Width="30%">
                                     <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                        
                                    </asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="lstAccYear" runat="server" Width="50%">
                                    </asp:DropDownList></td>
								<td width="30%"><asp:TextBox id=srchStorageArea width="50%" maxlength="8" runat="server" Visible="False"/><br />
                                    <asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server" Visible="False"/></td>														
								<td width="20%"></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>
		 			
						<asp:DataGrid id=EventData
							AutoGenerateColumns=false width=100% runat=server
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=31
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnEditCommand="DEDR_Edit"
							OnSortCommand=Sort_Grid  
							AllowSorting=True class="font9Tahoma">
							
							<HeaderStyle CssClass="mr-h" />
							<ItemStyle CssClass="mr-l" />
							<AlternatingItemStyle CssClass="mr-r" />
							
							<Columns>
								<asp:TemplateColumn HeaderText="Tgl.Produksi">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("DateProd"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
													 
								
								<asp:TemplateColumn HeaderText="Storage">
									<ItemTemplate>
										<%#Container.DataItem("TankiCode")%>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Sounding (Cm)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("SoundingCm"), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Temp ('C)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("Temperatur"), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="M.Jenis">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("BeratJenis"), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty(Ltr)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("ProductLiter"), 2)%>
									</ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                      <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Koreksi">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("SelisihSuhu"), 8)%>
									</ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                      <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>


								<asp:TemplateColumn HeaderText="Total(Ltr)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("TotalProductLiter"), 2)%>
									</ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                      <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Stock Akhir(Kg)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("TotalProductKg"), 2)%>
									</ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>


								<asp:TemplateColumn HeaderText="FFA (%)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("FFA"), 2)%>
									</ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                      <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Moist (%)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("Moisture"), 2)%>
									</ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                      <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Dirt (%)">
									<ItemTemplate>
										<%#FormatNumber(Container.DataItem("Dirt"), 2)%>
									</ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                      <HeaderStyle HorizontalAlign="Right" />
								</asp:TemplateColumn>
																																																																									
							</Columns>
						</asp:DataGrid><BR>
			 
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server Visible="False"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=False runat="server"/>
					</td>
				</tr>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</FORM>
	</body>
</html>
