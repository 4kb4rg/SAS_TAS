<%@ Page Language="vb" Trace="False" src="../../../include/IN_Trx_FuelIssue_List.aspx.vb" Inherits="IN_FuelIssue" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Fuel Issue List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>

            		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuINTrx id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>FUEL ISSUE LIST</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
						<tr>
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%" class="font9Tahoma">
								<tr>
								<td width="25%" valign=bottom>Fuel Issue ID :<BR><asp:TextBox id=srchStockTxID width=100% maxlength="20" runat="server"/></td>
								<td width="25%" valign=bottom>Issue Type :<BR><asp:DropDownList id="srchIssueList" width=100% runat=server/></td>
								<td width="20%" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server/></td>
								<td width="20%" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" valign=bottom align=right><asp:Button ID="Button1"  Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					<asp:DataGrid id="dgStockTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnDeleteCommand="DEDR_Delete"
						GridLines = none
						Cellpadding = "2"
						AllowPaging="True" 
						Allowcustompaging="False"
						Pagesize="15"  class="font9Tahoma"
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>					
					<Columns>
						<asp:HyperLinkColumn
								HeaderText="Fuel Issue ID"
								DataNavigateUrlField="FuelIssueID"
								DataNavigateUrlFormatString="IN_Trx_FuelIssue_Details.aspx?id={0}"
								DataTextField="FuelIssueID"
								DataTextFormatString="{0:c}"
								SortExpression="FuelIssueID"/>
								
						<asp:TemplateColumn HeaderText="Issue Type" SortExpression="IssueType">
							<ItemTemplate>
								<%# objINtx.mtdGetFuelIssueType(Container.DataItem("IssueType")) %>
							</ItemTemplate>
						</asp:TemplateColumn>

						<asp:TemplateColumn HeaderText="Last Update" SortExpression="Iss.UpdateDate">
							<ItemTemplate>
								<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status" SortExpression="Iss.Status">
							<ItemTemplate>
								<%# objINtx.mtdGetStocktransferStatus(Container.DataItem("Status")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
							<ItemTemplate>
								<%# Container.DataItem("UserName") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
								<asp:label id="lblTxID" Text=<%# Container.DataItem("FuelIssueID") %> Visible="False" Runat="server"></asp:label>
								<asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
								<asp:LinkButton id="Delete" CommandName="Delete" visible=false Text="Delete" Runat="server"/>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid>
					<BR><asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" />
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td style="width: 100%">&nbsp;</td>
									<td><img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" commandargument="prev" imageurl="../../../images/btprev.png" onClick="btnPrevNext_Click" /></td>
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
						        <asp:ImageButton id=Issue OnClick=btnNewItm_Click imageurl="../../images/butt_new_fuelissue.gif" AlternateText="New Fuel Issue" runat="server"/>&nbsp;
						        <asp:ImageButton id=Staff OnClick=btnNewItm_Click imageurl="../../images/butt_new_staffissue.gif" AlternateText="Staff Issue" runat="server"/>&nbsp;
						        <asp:ImageButton id=External OnClick=btnNewItm_Click imageurl="../../images/butt_new_externalissue.gif" AlternateText="External Party Issue" runat="server"/>&nbsp;
						        <asp:ImageButton id=ibPrint AlternateText=Print imageurl="../../images/butt_print.gif" visible=false/>
						        <a href="#" onclick="javascript:popwin(200, 400, 'IN_trx_PrintDocs.aspx?doctype=5')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
							</td>
						</tr>
                        <tr>
                            <td>
 					            <asp:Button ID="Issue2" class="button-small" runat="server" Text="New Fuel Issue"  />&nbsp;
                                <asp:Button ID="Staff2" class="button-small" runat="server" Text="Staff Issue"  />&nbsp;
                                <asp:Button ID="External2" class="button-small" runat="server" Text="External Party Issue"  />                         
                            </td>
                        </tr>
					</table>
				</div>
				</td>
				<td>
		<table cellpadding="0" cellspacing="0" style="width: 20px">
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>
				</td>
			</tr>
		</table>
			</FORM>
		</body>
</html>
