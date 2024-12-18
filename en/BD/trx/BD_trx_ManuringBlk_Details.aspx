<%@ Page Language="vb" trace="false" SmartNavigation="True" src="../../../include/BD_trx_ManuringBlk_Details.aspx.vb" Inherits="BD_ManuringBlk_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Manuring Schedule</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblBlock visible=false runat=server />
			<asp:label id=lblSubBlk visible=false runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblYear" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<table class="font9Tahoma"  border="0" cellspacing="0" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%></td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
                </table>

				<table style="width: 100%" class="font9Tahoma">
				<tr>
					<TD>					
						<asp:DataGrid id="dgManBlk"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines = none
							OnEditCommand="DEDR_Edit"
							Cellpadding = "2"
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
								<asp:TemplateColumn>
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<asp:label id="lblBlkCode" Text =<%# Trim(Container.DataItem("BlkCode")) %> runat="server"/>
										<asp:label id="lblManuringBlkID" Text =<%# Trim(Container.DataItem("ManuringBlkID")) %> visible=false runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
		
								<asp:TemplateColumn HeaderText="Planting Year">
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<%# Container.DataItem("YearPlanted") %>
										<asp:label id="lblPlantedArea" Text =<%# Trim(Container.DataItem("PlantedArea")) %> visible=false runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
										
								<asp:TemplateColumn HeaderText="SPH">
									<ItemStyle Width="10%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("SPH"), 0, True, False, False)) %>
										<asp:label id="lblSPH" Text='<%# FormatNumber(Container.DataItem("SPH"),0, True, False, False) %>' visible=false runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>					

								<asp:TemplateColumn HeaderText="Total">
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Total"), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Budgeting Month">
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<asp:dropdownlist id="lstPeriod" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn>
									<ItemStyle Width="10%" />
									<ItemTemplate>
										<asp:linkbutton id="lbNext" Text="Next" CommandName="Edit"  runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

							</Columns>
										
						</asp:DataGrid>
					</td>
					</tr>
				</table>


				</tr>
				<tr>
					<td ColSpan=6>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
			</table>	
            
            
        <br />
        </div>
        </td>
        </tr>
        </table>
         		
		</FORM>
	</body>
</html>
