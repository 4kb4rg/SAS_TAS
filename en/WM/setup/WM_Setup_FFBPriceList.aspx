<%@ Page Language="vb" trace=false src="~/include/WM_Setup_FFBPriceList.vb" Inherits="WM_Setup_FFBPriceList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWM" src="../../menu/menu_wmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FFB Price List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuWM id=MenuWM runat=server /></td>
				</tr>
			    <tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
				<table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">

				<tr>
					<td colspan="4" width="60%"><STRONG>FFB PRICE LIST</STRONG></td>
					<td colspan="2" align=right width="40%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					 
				</tr>
				<tr>
					<td style="background-color:#FFCC00" >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								
								<td height="26" valign=bottom style="width: 20%" class="font9Tahoma">
									<asp:DropDownList id="srchlocation" width=100% runat=server class="font9Tahoma" />
								</td> 
								<td height="26" valign=bottom style="width: 20%" class="font9Tahoma">
                                    Periode :<br />
                                    <asp:DropDownList ID="srcpmonth" runat="server" Width="50%" CssClass="font9Tahoma">
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="srcpyear" runat="server" Width="40%" CssClass="font9Tahoma">
                                    </asp:DropDownList>
								</td>
 
						 
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" CssClass="button-small" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgLine runat=server
							AutoGenerateColumns=False width=100% 
							GridLines=None 
							Cellpadding=2 
							AllowPaging=True 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Edit
							OnSortCommand=Sort_Grid  
							AllowSorting=True>								
							<HeaderStyle CssClass="mr-h" />						
							<ItemStyle CssClass="mr-l" />							
							<AlternatingItemStyle CssClass="mr-r" />
							<Columns>	 													 
								<asp:TemplateColumn HeaderText="Tanggal" SortExpression="Indate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("Indate"))%>
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Keterangan" >
									<ItemTemplate>
										<%# Container.DataItem("SubDescription")%>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Harga <br>Buah Besar" >
									<ItemTemplate>
										<%# Container.DataItem("BuahBesarPrice")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Harga <br>Buah Sedang" >
									<ItemTemplate>
										<%# Container.DataItem("BuahSedangPrice")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Harga <br>Buah Kecil" >
									<ItemTemplate>
										<%#Container.DataItem("BuahKecilPrice")%>
									</ItemTemplate>
								</asp:TemplateColumn>
		
								 						
								<asp:TemplateColumn HeaderText="Update By">
									<ItemTemplate>
										<%# Container.DataItem("UpdateID")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id="lbDelete" CommandName="Edit" Text="Edit" runat=server />
									</ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>	
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server"
							AutoPostBack="True" 
							onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="80%" ColSpan=6>
						<asp:ImageButton id=NewSalBtn onClick=NewSalBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat="server"/>
						<asp:ImageButton id=btnGen OnClick="DEDR_Gen" imageurl="../../images/butt_generate.gif" AlternateText="Generate Gol SKUB" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						<asp:Label id=SortCol Visible=False Runat="server" />			
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
