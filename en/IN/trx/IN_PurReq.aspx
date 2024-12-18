<%@ Page Language="vb" src="../../../include/IN_trx_PurReq.aspx.vb" Inherits="IN_PurchaseRequest" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<html>
	<head>
		<title>Purchase Requisition List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form ID="Form1" class="main-modul-bg-app-list-pu"  runat="server" >
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
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
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
									<td valign="top">Purchase Requisition ID<br /><asp:TextBox id="srchPRID" width=180px maxlength="36" CssClass="fontObject" runat="server"/></td>
									<td valign="top">Item<br /><asp:TextBox id="srchItem" width=180px maxlength="128" CssClass="fontObject" runat="server"/></td>
									<td valign="top">PR Level<br /><asp:DropDownList id="srchPRLevelList" width=80px CssClass="fontObject" runat=server /></td>
									<td valign="top">PR Type<br /><asp:DropDownList id="srchPRTypeList" width=80px CssClass="fontObject" runat=server /></td>
                                    <td valign="top">Department<br /><asp:DropDownList id="lstDept" width=80px CssClass="fontObject" runat=server /></td>
									<td valign="top">Period<br /><asp:DropDownList id="lstAccMonth" width=100% CssClass="fontObject" runat=server>
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
									</asp:DropDownList></td>
									<td valign="top"><br /><asp:DropDownList id="lstAccYear" width=50px CssClass="fontObject" runat=server></asp:DropDownList></td>
									<td valign="top">Status<br /><asp:DropDownList id="srchStatusList" width=60px CssClass="fontObject" runat=server /></td>
                                    <td valign="top">Status Item<br /><asp:DropDownList id="srchStatusLnList" width=100px CssClass="fontObject" runat=server /></td>
                                    <td valign="top" style="visibility:hidden"><br /><asp:DropDownList id="srchApprovedBy" width=200px CssClass="fontObject" runat=server >
								                                <asp:ListItem Value="0">-</asp:ListItem>
								                                <asp:ListItem Value="1">Supervisor</asp:ListItem>
                                                                <asp:ListItem Value="2">Manager</asp:ListItem>
                                                                <asp:ListItem Value="3">GM</asp:ListItem>
                                                                <asp:ListItem Value="4">VP/CEO</asp:ListItem>
                                                                </asp:DropDownList></td>
                                    <td valign="top"><br /><asp:TextBox id=srchUpdateBy width=60px maxlength="128" Visible=false CssClass="fontObject" runat="server"/></td>
									<td valign="bottom" class="cell-right" style="width:100%"><asp:Button Text="Search" OnClick=srchBtn_Click runat="server" ID="Button1" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table class="font9Tahoma" cellpadding="4" cellspacing="0" style="width: 100%">
                                    <tr>
                                    <td>
					 <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
                        <DefaultTabStyle Height="22px">
                        </DefaultTabStyle>
                        <HoverTabStyle CssClass="ContentTabHover">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                        NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                        FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>
                            <Tabs>
                                <igtab:Tab Key="PRList" Text="PURCHASE REQUISITION LIST" Tooltip="PURCHASE REQUISITION LIST">
                                <ContentPane>
                                <table border="0" cellspacing="1" cellpadding="1" width="99%">                              				
				                    <tr>
					                    <td colspan=6 >
					                        <div id="div1" style="height:320px;width:1100;overflow:auto;">		
					                        
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
                         OnItemDataBound="dgLine_BindGrid" 
                        AllowSorting="True" class="font9Tahoma">
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
					</asp:DataGrid>
					                        </div>
					                     </TD>
					               </tr>
 				               
					           </table>
						           </ContentPane>
				        </igtab:Tab>

                                <igtab:Tab Key="PRListOst" Text="PURCHASE REQUISITION OUTSTANDING LIST" Tooltip="PURCHASE REQUISITION OUTSTANDING LIST">
                                <ContentPane>
                                <table border="0" cellspacing="1" cellpadding="1" width="99%">                              				
				                    <tr>
					                    <td colspan=6 >
					                        <div id="div2" style="height:360px;width:1100;overflow:auto;">
			                        	
					                            <asp:DataGrid id="dgPROst"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
                         OnItemDataBound="dgLine_BindGrid" 
AllowSorting="True" class="font9Tahoma">
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
							<asp:HyperLinkColumn
								HeaderText="Purchase Requisition ID"
								DataNavigateUrlField="PRID"
								DataNavigateUrlFormatString="IN_PurReq_Details.aspx?PRID={0}"
								DataTextField="PRID"
								DataTextFormatString="{0:c}"
								SortExpression="h.PRID" />
								
							<asp:TemplateColumn HeaderText="Trans Date">
								<ItemTemplate>
									<%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%>
								</ItemTemplate>
								<ItemStyle Width="7%" />
							</asp:TemplateColumn>
																				
							<asp:TemplateColumn HeaderText="Item Code">
								<ItemTemplate>
									<%#Container.DataItem("ItemCode")%>
								</ItemTemplate>
                            <ItemStyle Width="8%" />								
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Description" HeaderStyle-HorizontalAlign="center" >
								<ItemTemplate>
									<%#Container.DataItem("Description")%>
								</ItemTemplate>
                            <ItemStyle Width="15%" />								
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Qty PR" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
								<ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyApp"), 2), 0)%>
								</ItemTemplate>
                            <ItemStyle Width="6%" />								
							</asp:TemplateColumn>

							<asp:TemplateColumn HeaderText="Qty PO" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
								<ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyPO"), 2), 0)%>
								</ItemTemplate>
                            <ItemStyle Width="6%" />								
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Outstanding" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
								<ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyOutStanding"), 2), 0)%>
								</ItemTemplate>
                            <ItemStyle Width="6%" />								
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="UOM" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
								<ItemTemplate>
									<%#Container.DataItem("UOMCode")%>
								</ItemTemplate>
                            <ItemStyle Width="6%" />								
							</asp:TemplateColumn>

							<asp:TemplateColumn HeaderText="Cost" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
								<ItemTemplate>
									<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 0)%>
								</ItemTemplate>
                            <ItemStyle Width="6%" />								
							</asp:TemplateColumn>
												
 							<asp:TemplateColumn HeaderText="Other Name" HeaderStyle-HorizontalAlign="center" >
								<ItemTemplate>
									<%#Container.DataItem("OtherName")%>
										<asp:label id="Status" visible="true" text=<%#  Container.DataItem("OtherName")  %> runat="server" />
								</ItemTemplate>
                            <ItemStyle Width="10%" />								
							</asp:TemplateColumn>


							<asp:TemplateColumn HeaderText="Additional Note" HeaderStyle-HorizontalAlign="center" > 
								<ItemTemplate>
									<%#Container.DataItem("AdditionalNote")%>
								</ItemTemplate>
                            <ItemStyle Width="17%" />								
							</asp:TemplateColumn>
																																			
						</Columns>
					</asp:DataGrid>
 
					                        </div>
					                    </td>
					               </tr>
					           </table>
					           </ContentPane>
					          </igtab:Tab>                        
					   </Tabs>
					   </igtab:UltraWebTab>
					                    <BR>
                                    </td>
                                    </tr>
								</table>
							</td>
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
						        <asp:ImageButton id=Stock UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" />&nbsp;
						        <asp:ImageButton id=DC UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewDCPR_Click" runat="server" />&nbsp;
						        <asp:ImageButton id=WS UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" /> &nbsp;
						        <asp:ImageButton id=FA UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" />&nbsp;
						        <asp:ImageButton id=NU UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" />	&nbsp;				
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText="Print" onClick="btnPreview_Click" visible="false" runat="server"/>

							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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

 

			</form>
		</body>
</html>
