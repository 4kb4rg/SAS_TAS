<%@ Page Language="vb" src="../../../include/CT_trx_StockAdj_list.aspx.vb" Inherits="CT_StockAdjust" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Adjustment List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblOrderBy" Visible="False" Runat="server"></asp:label>
			<asp:label id="lblOrderDir" Visible="False" Runat="server"></asp:label>
			
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuCTTrx id=menuCT runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>CANTEEN ADJUSTMENT LIST</strong><hr style="width :100%" />   
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
								<td width="18%" height="26">Canteen Adjustment ID :<br><asp:TextBox id=txtSearchStockAdjID width=100% maxlength="20" runat="server"/></td>
								<td width="18%" height="26">Adjustment Type :<br><asp:DropDownList id="ddlSearchAdjType" width=100% runat=server /></td>
								<td width="18%" height="26">Transaction Type :<br><asp:DropDownList id="ddlSearchTransType" width=100% runat=server /></td>
								<td width="18%" height="26">Status :<br><asp:DropDownList id="ddlSearchStatus" width=100% runat=server /></td>
								<td width="18%" height="26">Last Updated By :<br><asp:TextBox id=txtSearchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id="btnSearch" Text="Search" OnClick="btnSearch_OnClick" runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						             <asp:DataGrid id="dgStockAdj"
							            AutoGenerateColumns="false" width="100%" runat="server"
							            OnDeleteCommand="dgStockAdj_OnDeleteCommand"
							            OnItemDataBound="dgStockAdj_OnItemDataBound"
							            OnPageIndexChanged="dgStockAdj_OnPageIndexChanged"
							            OnSortCommand="dgStockAdj_OnSortCommand" 
							            GridLines = none
							            Cellpadding = "2"
							            AllowPaging="True" 
							            Allowcustompaging="False"
							            Pagesize="15" 
							            Pagerstyle-Visible="False"
							            AllowSorting="True"
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:HyperLinkColumn
									            HeaderText="Canteen Adjustment ID"
									            DataNavigateUrlField="StockAdjID"
									            DataNavigateUrlFormatString="CT_trx_StockAdj_details.aspx?id={0}"
									            DataTextField="StockAdjID"
									            DataTextFormatString="{0:c}"
									            SortExpression="HD.StockAdjID" />
								            <asp:TemplateColumn HeaderText="Adjustment Type" SortExpression="HD.AdjType">
									            <ItemStyle Width="16%"/>
									            <ItemTemplate>
										            <%# objCT.mtdGetAdjustmentType(IIf(IsNumeric(Container.DataItem("AdjType")), Container.DataItem("AdjType"), 0)) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Transaction Type" SortExpression="HD.TransType">
									            <ItemStyle Width="16%"/>
									            <ItemTemplate>
										            <%# objCT.mtdGetTransactionType(IIf(IsNumeric(Container.DataItem("TransType")), Container.DataItem("TransType"), 0)) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="HD.UpdateDate">
									            <ItemStyle Width="16%"/>
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Status" SortExpression="HD.Status">
									            <ItemStyle Width="16%"/>
									            <ItemTemplate>
										            <%# objCT.mtdGetStockAdjustStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									            <ItemStyle Width="16%"/>
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="" >
									            <ItemStyle Width="5%" HorizontalAlign="right" />
									            <ItemTemplate>
										            <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" Runat="server"/>
										            <asp:label id="lblStockAdjId" Text=<%# Container.DataItem("StockAdjID") %> Visible="False" Runat="server"></asp:label>
										            <asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
									            </ItemTemplate>
								            </asp:TemplateColumn>
							            </Columns>
						            </asp:DataGrid>
						            <br><asp:label id=lblActionResult text="Insufficient Stock In Inventory to Perform Operation!" Visible=False forecolor=red Runat="server" />
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
					            <asp:ImageButton id="ibNew"   AlternateText="New Stock Adjustment" OnClick="ibNew_OnClick"  ImageURL="../../images/butt_new.gif"    CauseValidation=False               runat="server"/>
						        <asp:ImageButton id="ibPrint" AlternateText="Print"                                         ImageURL="../../images/butt_print.gif"  CauseValidation=False Visible=false runat="server" />
							</td>
						</tr>
                        <tr>
                            <td>
 					            <asp:Button ID="ibNew2" class="button-small" runat="server" Text="New Stock Adjustment"  />&nbsp;
                                <asp:Button ID="ibPrint2" class="button-small" runat="server" Text="Print"  />&nbsp;
                            
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


		</form>
	</body>
</html>
