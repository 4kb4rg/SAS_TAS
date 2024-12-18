<%@ Page Language="vb" src="../../../include/CT_trx_PurReq.aspx.vb" Inherits="CT_PurchaseRequest" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Purchase Requisition List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />

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
							<td><strong>PURCHASE REQUISITION LIST</strong><hr style="width :100%" />   
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
								<td valign=bottom width=25% align=left>Purchase Requisition ID :<BR><asp:TextBox id="srchPRID" width=100% maxlength="20" runat="server"/></td>
								<td width=35%>&nbsp;</td>
								<td valign=bottom width=10% align=left>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td valign=bottom width=20% align=left>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td valign=bottom width=10% align=right><asp:Button  Text="Search" OnClick=srchBtn_Click runat="server" ID="Button1" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgPRListing"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
						                AllowPaging="True" 
						                Allowcustompaging="False"
						                Pagesize="15" 
						                OnPageIndexChanged="OnPageChanged"
						                Pagerstyle-Visible="False"
						                OnDeleteCommand="DEDR_Delete"				
						                OnUpdateCommand="DEDR_UnDelete"								
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="True"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>			
						                <Columns>
							                <asp:HyperLinkColumn
								                HeaderText="Purchase Requisition ID"
								                DataNavigateUrlField="PRID"
								                DataNavigateUrlFormatString="CT_PurReq_Details.aspx?PRID={0}"
								                DataTextField="PRID"
								                DataTextFormatString="{0:c}"
								                SortExpression="PRID" />
							                <asp:TemplateColumn HeaderText="Last Update" SortExpression="PR.CreateDate">
								                <ItemTemplate>								
									                <asp:label id="PRID" visible="false" text=<%# Container.DataItem("PRID")%> runat="server" />
									                <asp:label id="Remark" visible="false" text=<%# Container.DataItem("Remark")%> runat="server" />							
									                <asp:label id="TotalAmount" visible="false" text=<%# Container.DataItem("TotalAmount")%> runat="server" />																			
									                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>							
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Status" SortExpression="PR.Status">
								                <ItemTemplate>
									                <asp:label id="Status" visible="true" text=<%# objCT.mtdGetPurReqStatus(Container.DataItem("Status")) %> runat="server" />
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
								                <ItemTemplate>
									                <%# Container.DataItem("UserName") %>
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn>		
								                <ItemTemplate>
									                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									                <asp:LinkButton id="Undelete" CommandName="Update" visible=False Text="Undelete" runat="server"/>
								                </ItemTemplate>
							                </asp:TemplateColumn>	
						                </Columns>
					                </asp:DataGrid><br>
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
					            <asp:ImageButton id=Stock imageurl="../../images/butt_new.gif" AlternateText="New Canteen PR" OnClick="btnNewCanteenPR_Click" runat="server" />
							</td>
						</tr>
                        <tr>
                            <td>
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


			</form>
		</body>
</html>
