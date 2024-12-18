<%@ Page Language="vb" src="../../../include/IN_trx_PurReq.aspx.vb" Inherits="IN_PurchaseRequest" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Purchase Requisition List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">

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
								<td valign=bottom width=15% height=26>Purchase Requisition ID :<BR><asp:TextBox id="srchPRID" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=15% height=26>Item :<BR><asp:TextBox id="srchItem" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=10% height=26>PR Level :<BR><asp:DropDownList id="srchPRLevelList" width=100% runat=server /></td>
								<td valign=bottom width=10% height=26>PR Type :<BR><asp:DropDownList id="srchPRTypeList" width=100% runat=server /></td>
								<td valign=bottom width=8%>Period :<BR>
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
								<td valign=bottom width=10% height=26>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td valign=bottom width=10% height=26>Status Item :<BR><asp:DropDownList id="srchStatusLnList" width=100% runat=server /></td>
								<td valign=bottom width=15% height=26>Next Approved By :<BR><asp:DropDownList id="srchApprovedBy" width=100% runat=server >
								                                <asp:ListItem Value="0">-</asp:ListItem>
								                                <asp:ListItem Value="1">Supervisor</asp:ListItem>
                                                                <asp:ListItem Value="2">Manager</asp:ListItem>
                                                                <asp:ListItem Value="3">GM</asp:ListItem>
                                                                <asp:ListItem Value="4">VP/CEO</asp:ListItem>
                                                                </asp:DropDownList></td>
								<td valign=bottom width=12% height=26><BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=10% height=26 align=right><asp:Button Text="Search" OnClick=srchBtn_Click runat="server" ID="Button1" class="button-small"/></td>
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
						                AutoGenerateColumns="False" width="100%" runat="server"
						                GridLines=None
						                Cellpadding="2"
						                AllowPaging="True"
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
								                DataNavigateUrlFormatString="IN_PurReq_Details.aspx?PRID={0}"
								                DataTextField="PRID"
								                DataTextFormatString="{0:c}"
								                SortExpression="PR.PRID" />
								
							                <asp:TemplateColumn HeaderText="PR Level" SortExpression="PR.LocLevel">
								                <ItemTemplate>
									                <asp:label id="PRLevel" visible="true" text=<%# objAdminLoc.mtdGetLocLevel(Container.DataItem("LocLevel")) %> runat="server" />
								                </ItemTemplate>
							                </asp:TemplateColumn>	
							                <asp:TemplateColumn HeaderText="PR Type" SortExpression="PR.PRType">
								                <ItemTemplate>
									                <asp:label id="PRType" visible="true" text=<%# objIN.mtdGetPRtype(Container.DataItem("Prtype")) %> runat="server" />
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Last Updated" SortExpression="PR.UpdateDate">
								                <ItemTemplate>								
									                <asp:label id="PRID" visible="false" text=<%# Container.DataItem("PRID")%> runat="server" />
									                <asp:label id="Remark" visible="false" text=<%# Container.DataItem("Remark")%> runat="server" />							
									                <asp:label id="TotalAmount" visible="false" text=<%# Container.DataItem("TotalAmount")%> runat="server" />			
									                <asp:label id="PRTypeCode" visible="false" text=<%# Container.DataItem("Prtype") %> runat="server" />												
									                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>							
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Status" SortExpression="PR.Status">
								                <ItemTemplate>
									                <asp:label id="Status" visible="true" text=<%# objIN.mtdGetPurReqStatus(Container.DataItem("Status")) %> runat="server" />
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Created By" SortExpression="UserPR">
								                <ItemTemplate>
									                <%#Container.DataItem("UserPR")%>
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
								                <ItemTemplate>
									                <%# Container.DataItem("UserName") %>
								                </ItemTemplate>
							                </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="User History" Visible=false>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblHistID" TextMode="MultiLine" ReadOnly=true  BorderStyle="None"  BackColor="transparent" runat="server" Text='<%# Container.DataItem("UserHist") %>'></asp:TextBox>                                                                                    
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            </asp:TemplateColumn>						
							                <asp:TemplateColumn Visible="False">
								                <ItemStyle HorizontalAlign="Center" /> 															
								                <ItemTemplate>
									                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									                <asp:LinkButton id="Undelete" CommandName="Update" visible=False Text="Undelete" runat="server"/>
								                </ItemTemplate>
							                </asp:TemplateColumn>	
						                </Columns>
                                        <PagerStyle Visible="False" />
					                </asp:DataGrid><BR>
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
					            <asp:ImageButton id=Stock UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" />
						        <asp:ImageButton id=DC UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewDCPR_Click" runat="server" />
						        <asp:ImageButton id=WS UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" /> 
						        <asp:ImageButton id=FA UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" />
						        <asp:ImageButton id=NU UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" />						
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" visible=false/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
                        <asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /></table>
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
