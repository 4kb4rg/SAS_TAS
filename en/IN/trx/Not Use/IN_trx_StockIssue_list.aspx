<%@ Page Language="vb" src="../../../include/IN_Trx_StockIssue_List.aspx.vb" Inherits="IN_StockIssue" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Issue List</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<script language="javascript">
	    function ChkTrxType() {
		    var doc = document.frmMain;
		    var strDisplay = doc.Request.QueryString("type");
	    }
	</script>
	<body>
	    <form runat="server" id=frmMain>
		<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
		<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
		<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
		<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
		<table border="0" cellspacing="1" cellpadding="1" width="100%">
			<tr>
				<td colspan="6"><UserControl:MenuINTrx id=menuIN runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="3"><asp:label id="lblStkName" runat="server"/></td>
				<td align="right" colspan="3" ><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=6 width=100% class="mb-c">
					<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
						<tr class="mb-t">
							<td width="25%" height="26"><asp:label id="lblStkID" runat="server"/> :<BR><asp:TextBox id=srchStockTxID width=100% maxlength="32" runat="server"/></td>
							<td width="25%" height="26">Issue Type :<BR><asp:DropDownList id="srchIssueList" width=100% runat=server/></td>
							<td width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% runat=server>
										<asp:ListItem value="0" Selected>All</asp:ListItem>
										<asp:ListItem value="1">1</asp:ListItem>
										<asp:ListItem value="2">2</asp:ListItem>										
										<asp:ListItem value="3">3</asp:ListItem>
										<asp:ListItem value="4">4</asp:ListItem>
										<asp:ListItem value="5">5</asp:ListItem>
										<asp:ListItem value="6">6</asp:ListItem>
										<asp:ListItem value="7">7</asp:ListItem>
										<asp:ListItem value="8">8</asp:ListItem>
										<asp:ListItem value="9">9</asp:ListItem>
										<asp:ListItem value="10">10</asp:ListItem>
										<asp:ListItem value="11">11</asp:ListItem>
										<asp:ListItem value="12">12</asp:ListItem>
									</asp:DropDownList>
							<td width=10%><BR>
							    <asp:DropDownList id="lstAccYear" width=100% runat=server>
								</asp:DropDownList>
							<td width="20%" height="26">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
							<td width="20%" height="26"><BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
							<td width="10%" height="26" valign=bottom align=right><asp:Button Text="Search" OnClick=srchBtn_Click runat="server"/></td>
						</tr>
					</table>
				</td>
			</tr>												
			<tr>
				<TD colspan = 6 >					
				<asp:DataGrid id="dgStockTx"
					AutoGenerateColumns="false" width="100%" runat="server"
					OnDeleteCommand="DEDR_Delete"
					GridLines = none
					Cellpadding = "2"
					AllowPaging="True" 
					Allowcustompaging="False"
					Pagesize="15" 
					OnPageIndexChanged="OnPageChanged"
					Pagerstyle-Visible="False"				
					OnSortCommand="Sort_Grid" 
					AllowSorting="True"   
					>
					<HeaderStyle CssClass="mr-h" />							
					<ItemStyle CssClass="mr-l" />
					<AlternatingItemStyle CssClass="mr-r" />						
				<Columns>
				
					<asp:HyperLinkColumn   
							HeaderText="Stock Issue ID" 
							DataNavigateUrlField="StockIssueID" 
							DataNavigateUrlFormatString="IN_Trx_StockIssue_Details.aspx?id={0}&type=IN"
							DataTextField="StockIssueID"
							DataTextFormatString="{0:c}"
							SortExpression="StockIssueID" />
							
                    <asp:TemplateColumn HeaderText="Issue Date" SortExpression="iss.StockIssueDate">
						<ItemTemplate>
							<%#objGlobal.GetLongDate(Container.DataItem("StockIssueDate"))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Remark" SortExpression="iss.Remark">
						<ItemTemplate>
							<%#Container.DataItem("Remark")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Issue Type" SortExpression="iss.IssueType">
						<ItemTemplate>
							<%# objINtx.mtdGetStockIssueType(Container.DataItem("IssueType")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="iss.UpdateDate">
						<ItemTemplate>
							<%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status" SortExpression="iss.Status">
						<ItemTemplate>
							<%# objINtx.mtdGetStocktransferStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="" >
						<ItemTemplate>
							<asp:label id="lblTxID" Text=<%# Container.DataItem("StockIssueID") %> Visible="False" Runat="server"></asp:label>
							<asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" Runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>

				</Columns>
				</asp:DataGrid>
				<BR><asp:label id=lblUnDel text="Insufficient Stock In Inventory to Perform Operation !" Visible=False forecolor=red Runat="server" />
				</td>
				</tr>
			<tr>
			<td align=right colspan="6">
					<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
					<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
		         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
		         	<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
		         	<asp:Label id=lblPageCount visible=false text=1 runat=server/>
			</td>
			</tr>
			<tr>
				<td align="left" width="80%" ColSpan=6>
					<asp:ImageButton id=Issue UseSubmitBehavior="false" OnClick=btnNewItm_Click imageurl="../../images/butt_new_stockissue.gif" AlternateText="New Stock Issue" runat="server"/>
					<asp:ImageButton id=Staff UseSubmitBehavior="false" OnClick=btnNewItm_Click imageurl="../../images/butt_new_staffissue.gif" AlternateText="Staff Issue" runat="server"/>
					<asp:ImageButton id=External UseSubmitBehavior="false" OnClick=btnNewItm_Click imageurl="../../images/butt_new_externalissue.gif" AlternateText="External Party Issue" runat="server"/>
					<asp:ImageButton id=Nursery UseSubmitBehavior="false" OnClick=btnNewItm_Click imageurl="../../images/butt_new_NUrseryissue.gif" AlternateText="Nursery Issue" runat="server"/>
					<asp:ImageButton id=ibPrint UseSubmitBehavior="false"  AlternateText=Print imageurl="../../images/butt_print.gif" visible=false/>
					<a href="#" onclick="javascript:popwin(200, 400, 'IN_trx_PrintDocs.aspx?doctype=4')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
				</td>
			</tr>
		</table>
			</FORM>

	</body>
</html>
