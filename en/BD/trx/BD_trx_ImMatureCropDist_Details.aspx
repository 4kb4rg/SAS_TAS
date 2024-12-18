<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_trx_ImMatureCropDist_Details.aspx.vb" Inherits="BD_ImMatureCropDist_Details" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Immature Crop Activity Calenderisation</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="frmMain"  class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblBudgetingErr visible=false text="No active budgeting period." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id=lblPeriod visible=false runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<Input type=hidden id=hidBlkCode value="" runat=server />

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<table border="0" cellspacing="1" cellpadding="1" width="100%" runat=server class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuBDTrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>IMMATURE CROP ACTIVITY CALENDERISATION</td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBlockTag" runat="server"/> : <asp:label id="lblBlkCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%></td>
				</tr>	
				<tr id="RowSubBlk">
					<td colspan="4" width=60%><asp:label id="lblSubBlkTag" runat="server"/> : <asp:label id="lblSubBlkCode" runat="server" /></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>														
				<tr id="RowPlantingYr">
					<td colspan="4" width=60%><asp:label id="lblPlantYrTag" text="Planting Year" runat="server"/> : <asp:label id="lblYearPlanted" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>																															
				<tr>
					<td colspan="4" width=60%>&nbsp;</td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<TD colspan = 6 ></td>
				</tr>
			</table>
			<table cellpadding="2" cellspacing="0" runat="server" class="font9Tahoma">
				<tr id="trPeriodTitle" runat="server">
					<td style="width:100px;" nowrap="true">&nbsp; <!-- Account Code --></td>
					<td style="width:200px;" nowrap="true">&nbsp; <!-- Description --></td>
					<td style="width:100px;" nowrap="true">&nbsp; <!-- Year Budget --></td>
					<td style="width:100px;" nowrap="true">&nbsp; <!-- Total Add Vote --></td>
					<td style="width:50px;" nowrap="true">&nbsp; <!-- Edit --></td>
				</tr>
			</table>

            <table style="width: 100%" class="font9Tahoma">
			<asp:DataGrid id="dgImMatureCropDist"
				AutoGenerateColumns="false" runat="server"
				OnItemDataBound="DataGrid_ItemDataBound" 
				GridLines = both
				OnEditCommand="DEDR_Edit"	
				Cellpadding = "2"
				Pagerstyle-Visible="False"
				AllowSorting="True"
                        class="font9Tahoma">	
							 
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
					<asp:TemplateColumn >
						<ItemStyle Width="300px" />						
						<ItemTemplate>
							<asp:label id="lblAccCode" width=100px Text='<%# trim(Container.DataItem("AccCode")) %>' runat="server"/>
							<asp:label id="lblUnMatureCropDistID" width=300px visible=false Text='<%# trim(Container.DataItem("UnMatureCropDistID")) %>' runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>																																		
					<asp:TemplateColumn HeaderText="Description">
						<ItemStyle Width="800px" />						
						<ItemTemplate>
							<asp:label id="lblDesc" width=200px text='<%# Container.DataItem("AccDesc") %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>																																		
					<asp:TemplateColumn HeaderText="Year Budget" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
						<ItemStyle Width=80px  />						
						<ItemTemplate>
							<asp:label id="lblCost" width=100px text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("YearBudget"), 0, True, False, False)) %>' runat=server />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total Add Vote" HeaderStyle-HorizontalAlign="Right">
						<ItemStyle Width="80px" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblAddVote" width="100px" text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("TotalAddVote"), 0, True, False, False)) %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn ItemStyle-HorizontalAlign=Center >
						<ItemStyle Width=100px />						
						<ItemTemplate>
							<asp:LinkButton width=25px id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>						
					</asp:TemplateColumn>					
				</Columns>
			</asp:DataGrid>	
            </table>
            
        <br />
        </div>
        </td>
        </tr>
        </table>
        	
		</FORM>
	</body>
</html>
