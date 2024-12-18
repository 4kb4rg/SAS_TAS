<%@ Page Language="vb" src="../../../include/PU_trx_DAList.aspx.vb" Inherits="PU_DAList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>


<html>
	<head>
		<title>Dispatch Advice List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmDAList class="main-modul-bg-app-list-pu" runat="server">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrOnHand visible=false text="Insufficient quantity on hand" runat=server />		
			<asp:label id=lblErrOnHold visible=false text="Insufficient quantity on hold" runat=server />		
			<asp:label id=lblTo visible=false text="To " runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=menuPU runat="server" />
					</td>
				</tr>

			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
			 

				<tr>
					<td class="font9Tahoma" colspan="3"><strong> DISPATCH ADVICE LIST</strong></td>
					<td align="right" colspan="3" ><asp:label id="lblTracker" class="font9Tahoma"  runat="server"/></td>
				</tr>
                  <tr>
					<td colspan=6><hr style="width :100%" />   
                            </td>
				</tr>
				<tr>
					<td colspan=6>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=7 width=100% class="font9Tahoma">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" class="font9Tahoma" align="center">
							<tr style="background-color:#FFCC00">
								<td width="15%" height="26">DA ID :<BR><asp:TextBox id=txtDispAdvId width="100%" CssClass="fontObject" maxlength="32" runat="server" /></td>
								<td width="15%" height="26">DA Type :<BR><asp:DropDownList id="ddlDAType" CssClass="fontObject"   width=100% runat=server /></td>
								<td width="12%" height="26">Supplier :<BR><asp:TextBox id=txtSuppCode CssClass="fontObject"  width=100% maxlength="20" runat="server"/></td>
								<td width="15%" height="26">GR ID :<BR><asp:TextBox id=txtGoodsRcvID CssClass="fontObject"  width=100% maxlength="128" runat="server"/></td>
								<td width="11%" height="26">Dispatch To :<BR><asp:TextBox id=txtToLocCode width="100%" CssClass="fontObject" maxlength="8" runat="server"/></td>
								<td width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% CssClass="fontObject" runat=server>
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
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								<td width="10%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1">Active</asp:ListItem>
										<asp:ListItem value="4">Cancelled</asp:ListItem>
										<asp:ListItem value="2">Confirmed</asp:ListItem>
										<asp:ListItem value="3">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="16%" height="26"><BR><asp:TextBox id=txtLastUpdate width="100%" maxlength="128" Visible=false CssClass="fontObject" runat="server"/></td>
								<td width="19%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>
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
                                <igtab:Tab Key="DAList" Text="DISPATCH ADVICE LIST" Tooltip="DISPATCH ADVICE LIST">
                                <ContentPane>
                                <table border="0" cellspacing="1" cellpadding="1" width="99%">                              				
				                    <tr>
					                    <td colspan=6 >
					                        <div id="div1" style="height:300px;width:1100;overflow:auto;">								                        								
						                            <asp:DataGrid id=dgDAList runat=server
							                        AutoGenerateColumns=False width=100% 
							                        GridLines=None 
							                        Cellpadding=2 
							                        AllowPaging=True 
							                        Pagesize=15 
							                        OnPageIndexChanged=OnPageChanged 
							                        Pagerstyle-Visible=False 
							                        OnDeleteCommand=DEDR_Delete 
    						                        OnEditCommand=DEDR_Edit
							                        OnSortCommand=Sort_Grid  
                                                     OnItemDataBound="dgLine_BindGrid" 
							                        AllowSorting=True  >	
                                                    <HeaderStyle CssClass="mr-h"/>
                                                    <ItemStyle CssClass="mr-l"/>
                                                    <AlternatingItemStyle CssClass="mr-r"/>
                        								
				 
                        							
							                        <Columns>
								                        <asp:BoundColumn Visible=False DataField="DispAdvId" />
								                        <asp:BoundColumn Visible=False DataField="Status" />

								                        <asp:HyperLinkColumn HeaderText="Dispatch Advice ID" 
									                        SortExpression="A.DispAdvId" 
									                        DataNavigateUrlField="DispAdvId" 
									                        DataNavigateUrlFormatString="PU_trx_DADet.aspx?DispAdvId={0}" 
									                        DataTextField="DispAdvId" />
                        								
								                        <asp:HyperLinkColumn HeaderText="Transporter" 
									                        SortExpression="A.Transporter" 
									                        DataNavigateUrlField="DispAdvId" 
									                        DataNavigateUrlFormatString="PU_trx_DADet.aspx?DispAdvId={0}" 
									                        DataTextField="Transporter" />
                        								
								                        <asp:TemplateColumn HeaderText="Dispatch To" SortExpression="A.ToLocCode">
									                        <ItemTemplate>
										                        <%# Container.DataItem("ToLocCode") %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
								                        <asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
									                        <ItemTemplate>
										                        <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
								                        <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									                        <ItemTemplate>
										                        <%# objPU.mtdGetDAStatus(Container.DataItem("Status")) %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="PIC" SortExpression="B.PIC">
									                        <ItemTemplate>
										                        <%# Container.DataItem("PIC") %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>   
                                                        <asp:TemplateColumn HeaderText="Document Receive ID" SortExpression="SRDID">
									                        <ItemTemplate>
										                        <%#Container.DataItem("SRDID")%>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>   
                        																                             
								                        <asp:TemplateColumn HeaderText="Updated By" SortExpression="B.UserName">
									                        <ItemTemplate>
										                        <%# Container.DataItem("UserName") %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
								                        <asp:TemplateColumn>
									                        <ItemTemplate>
										                        <asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										                        <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										                        <asp:LinkButton id=lbUnDelete CommandName=Edit Text=Undelete runat=server />
									                        </ItemTemplate>
								                        </asp:TemplateColumn>	
							                        </Columns>
                                                    <PagerStyle Visible="False" />
						                        </asp:DataGrid><BR>						
						                    </div>
						               </td>
						           </tr>
						           				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" CssClass="fontObject" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
						       </table>
						      </ContentPane>
						      </igtab:Tab>
						      
                                <igtab:Tab Key="DAListAuto" Text="DISPATCH AUTO POST" Tooltip="DISPATCH AUTO POST">
                                <ContentPane>
                                <table border="0" cellspacing="1" cellpadding="1" width="99%">                              				
				                    <tr>
					                    <td colspan=6 >
					                        <div id="div2" style="height:300px;width:1100;overflow:auto;">
                                                <asp:DataGrid id=dgAdvAPost runat=server
							                        AutoGenerateColumns=False width=100% 
							                        GridLines=None 
							                        Cellpadding=2 
                                                     OnItemDataBound="dgLine_BindGrid" 
 							                        AllowSorting=True  >	
                                                <HeaderStyle CssClass="mr-h"/>
                                                <ItemStyle CssClass="mr-l"/>
                                                <AlternatingItemStyle CssClass="mr-r"/>
                        								
							                        <HeaderStyle CssClass="mr-h"/>
							                        <ItemStyle CssClass="mr-l"/>
							                        <AlternatingItemStyle CssClass="mr-r"/>
                        							
							                        <Columns>
								                        <asp:BoundColumn Visible=False DataField="DispAdvId" />
								                        <asp:BoundColumn Visible=False DataField="Status" />

								                        <asp:HyperLinkColumn HeaderText="Dispatch Advice ID" 
									                        SortExpression="A.DispAdvId" 
									                        DataNavigateUrlField="DispAdvId" 
									                        DataNavigateUrlFormatString="PU_trx_DADet.aspx?DispAdvId={0}" 
									                        DataTextField="DispAdvId" />
                        								
								                        <asp:HyperLinkColumn HeaderText="Transporter" 
									                        SortExpression="A.Transporter" 
									                        DataNavigateUrlField="DispAdvId" 
									                        DataNavigateUrlFormatString="PU_trx_DADet.aspx?DispAdvId={0}" 
									                        DataTextField="Transporter" />
                        								
								                        <asp:TemplateColumn HeaderText="Dispatch To" SortExpression="A.ToLocCode">
									                        <ItemTemplate>
										                        <%# Container.DataItem("ToLocCode") %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
								                        <asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
									                        <ItemTemplate>
										                        <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
								                        <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									                        <ItemTemplate>
										                        <%# objPU.mtdGetDAStatus(Container.DataItem("Status")) %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="PIC" SortExpression="B.PIC">
									                        <ItemTemplate>
										                        <%# Container.DataItem("PIC") %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>   
                                                        <asp:TemplateColumn HeaderText="Document Receive ID" SortExpression="SRDID">
									                        <ItemTemplate>
										                        <%#Container.DataItem("SRDID")%>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>   
                        																                             
								                        <asp:TemplateColumn HeaderText="Updated By" SortExpression="B.UserName">
									                        <ItemTemplate>
										                        <%# Container.DataItem("UserName") %>
									                        </ItemTemplate>
								                        </asp:TemplateColumn>
								 
							                        </Columns>
                                                    <PagerStyle Visible="False" />
						                        </asp:DataGrid>					                        
						                    </div>
						               </td>
						           </tr>
						       </table>
						      </ContentPane>
						      </igtab:Tab>					                        						      
						    </Tabs>
				        </igtab:UltraWebTab> 
						
					</td>
				</tr>

				<tr>
					<td align="left" width="100%" colspan=6>
						<asp:ImageButton id=NewINDABtn UseSubmitBehavior="false" onClick=NewINDABtn_Click imageurl="../../images/butt_new_stockda.gif" AlternateText="New Stock/Workshop Dispatch Advice" runat=server/>
						<asp:ImageButton id=NewDCDABtn UseSubmitBehavior="false" onClick=NewDCDABtn_Click imageurl="../../images/butt_new_directchargeda.gif" AlternateText="New Direct Charge Dispatch Advice" runat=server/>
						<asp:ImageButton id=NewFADABtn UseSubmitBehavior="false" onClick=NewFADABtn_Click imageurl="../../images/butt_new_fixedassetda.gif" AlternateText="New Fixed Asset Dispatch Advice" runat=server/>
						<asp:ImageButton id=NewNUDABtn UseSubmitBehavior="false" onClick=NewNUDABtn_Click imageurl="../../images/butt_new_nurseryda.gif" AlternateText="New Nursery Dispatch Advice" runat=server/>	
						<a href="#" onclick="javascript:popwin(200, 400, 'PU_trx_PrintDocs.aspx?doctype=3')"> <asp:Image id=ibPrintDoc runat="server" ImageUrl="../../images/butt_print_doc.gif" /> </a>
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
