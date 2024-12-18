<%@ Page Language="vb" src="../../../include/IN_Trx_StockIssue_List.aspx.vb" Inherits="IN_StockIssue" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Issue List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	<script language="javascript">
	    function ChkTrxType() {
		    var doc = document.Form1;
		    var strDisplay = doc.Request.QueryString("type");
	    }
	</script>
	<body>
	    <form runat="server" id=Form1 class="main-modul-bg-app-list-pu">
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
							<td><strong><asp:label id="lblStkName" runat="server"/></strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								    <td width="25%" height="26"><asp:label id="lblStkID" runat="server"/> :<BR><asp:TextBox id=srchStockTxID width=100% maxlength="20" runat="server"/></td>
							        <td width="25%" height="26">Issue Type :<BR><asp:DropDownList id="srchIssueList" width=100% runat=server/></td>
							        <td width="20%" height="26">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
							        <td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
							        <td width="10%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
					                    Pagesize="15" 
					                    OnPageIndexChanged="OnPageChanged"
					                    Pagerstyle-Visible="False"				
					                    OnSortCommand="Sort_Grid" 
					                    AllowSorting="True"   
					
                                                                            class="font9Tahoma">
								
							                                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>					
				                    <Columns>
				
					                    <asp:HyperLinkColumn   
							                    HeaderText="Stock Issue ID" 
							                    DataNavigateUrlField="StockIssueID" 
							                    DataNavigateUrlFormatString="IN_Trx_StockIssue_Details.aspx?id={0}&type=SD"
							                    DataTextField="StockIssueID"
							                    DataTextFormatString="{0:c}"
							                    SortExpression="StockIssueID" />

					                    <asp:TemplateColumn HeaderText="Issue Type" SortExpression="iss.IssueType">
						                    <ItemTemplate>
							                    <%# objINtx.mtdGetStockIssueType(Container.DataItem("IssueType")) %>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Last Update" SortExpression="iss.UpdateDate">
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
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
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
		                          	<asp:Label id=lblPageCount visible=false text=1 runat=server/>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=External OnClick=btnNewItm_Click imageurl="../../images/butt_new_sales.gif" AlternateText="External Party Issue" runat="server"/>
					            <a href="#" onclick="javascript:popwin(200, 400, 'IN_trx_PrintDocs.aspx?doctype=4')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
                                &nbsp;
                          
                            </td>
                        </tr>
					</table>
				</div>
				</td>
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
