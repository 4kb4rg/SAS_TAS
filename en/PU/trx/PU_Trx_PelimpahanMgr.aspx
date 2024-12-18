<%@ Page Language="vb" src="../../../include/PU_Trx_PelimpahanMgr.aspx.vb" Inherits="PU_Trx_PelimpahanMgr" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
  TagPrefix="igtab" %>

<html>
	<head>
		<title>DISPOSITION BY USER</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">


hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
    margin-bottom: 0px;
}
        </style>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma"  >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">     

		    <table border="0" cellspacing="1" class="font9Tahoma"  cellpadding="1" width="100%">
            			<tr>
					<td  colspan="3" style="height: 21px"> <strong> DISPOSITION TO USER&nbsp;</strong> </td>
				</tr>
								    <tr>
				    <td colspan=6 style="height: 11px">
                        <hr style="width :100%" />
                                        </td>
				    </tr>
            </table>
          
		     <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" Width="100%" ForeColor=black runat="server">
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
                            <igtab:Tab Key="PRINBOX" Text="INBOX" Tooltip="SETUP TYPE GROUP COA LIST">
                                <ContentPane>                                		
                 
			<table border="0" cellspacing="1" cellpadding="1" width="99%">
			
				<tr>
					<td colspan="6"><UserControl:MenuINTrx id=menuIN runat="server" /></td>
				</tr>
				
				<tr>					
					<td align="right" colspan="3" style="height: 21px" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>				
				<tr>
					<td colspan=6 width=99% style="background-color:#FFCC00" >
						<table width="99%" style="background-color:#FFCC00" cellspacing="0" cellpadding="3" border="0" align="center" style="height: 40px">
							<tr class="mb-c">
								<td valign=bottom width=20% style="height: 39px">Purchase Requisition ID :<BR><asp:TextBox id="srchPRID" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=25% style="height: 39px">Item Name :<BR><asp:TextBox id="srchItem" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=10% style="height: 39px">From Location :<BR>
								    <asp:DropDownList id="lstFrLocCode" width=100% runat=server>
									</asp:DropDownList></td>																	
								<td valign=bottom width=18% style="height: 39px">
                                    Group Item :<asp:DropDownList id="srchGroupItem" width=100% runat=server />
								<td valign="bottom" width=15% style="height: 39px">
                                        <asp:DropDownList id="srchApprovedBy" width=100% visible=false runat=server >
											<asp:ListItem Value="0">All</asp:ListItem>
											<asp:ListItem Value="1">Supervisor</asp:ListItem>
											<asp:ListItem Value="2">Manager</asp:ListItem>
											<asp:ListItem Value="3">GM</asp:ListItem>
											<asp:ListItem Value="4">VP/CEO</asp:ListItem>
										</asp:DropDownList></td>
								<td valign=bottom width=10% style="height: 39px"><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=12% style="height: 39px"><BR></td>
								<td valign=bottom width=10% align=right style="height: 39px"><asp:Button Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server" ID="BtnSearch"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD colspan=6>
					<div id="divgd" style="width:100%;height:400px;overflow: auto;">   
	                        <asp:DataGrid ID="dgPRListing" runat="server" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-Visible="False"   PageSize="20" OnItemDataBound="dgPRListing_BindGrid"
							OnEditCommand="DEDR_Edit"
							OnCancelCommand="DEDR_Cancel"
							OnUpdateCommand="DEDR_Update"							
                            Width="100%" AllowPaging="True" class="font9Tahoma">
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
                                 <asp:TemplateColumn HeaderText="No">
                                        <ItemTemplate>
                                              <asp:Label ID="lblNoUrut" runat="server" Visible="True"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                </asp:TemplateColumn>
                            
                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="False" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="PrIDSwow" HeaderText="PR ID" Visible="True" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ItemCode" HeaderText="Item Code" Visible="False"></asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupItem" runat="server" Text='<%# Container.DataItem("ProdTypeCode") %>'></asp:Label>
                                        <asp:Label ID="ItemCode" runat="server" Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>
                                        (<%# Container.DataItem("ItemDesc") %>)
                                        <br />
                                        <asp:Label ID="lblAddNote" runat="server" Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                        <asp:TextBox ID="lstAddNote" runat="server" Text='<%# trim(Container.DataItem("AdditionalNote")) %>'
                                            Visible="false"></asp:TextBox>
                                        <asp:Label ID="LnID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Quantity Requested">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyReq" runat="server" Text='<%# Container.DataItem("QtyReq") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblQtyReqDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %>'></asp:Label>
                                        <asp:TextBox ID="lstQtyReq" runat="server" MaxLength="8" Text='<%# trim(Container.DataItem("QtyReq")) %>'
                                            Visible="false" Width="90%"></asp:TextBox>
                                        &nbsp;
                                        <br />
                                  <asp:Label ID="lblPurchUomReq" runat="server" Text='<%# Container.DataItem("UomCode") %>'
                                            Visible="true"></asp:Label>     
                                        <br />
                                        <asp:Label ID="lblUnitCost" runat="server" Text='<%# Container.DataItem("Cost") %>'
                                            Visible="false"></asp:Label>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Quantity Approved">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyApp" runat="server" Text='<%# Container.DataItem("QtyApp") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblQtyAppDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyApp"),2) %>'></asp:Label>
                                        <asp:TextBox ID="lstQtyApp" runat="server" MaxLength="8" Text='<%# trim(Container.DataItem("QtyApp")) %>'
                                            Visible="false" Width="90%"></asp:TextBox>&nbsp;
                                        <br />
                                  <asp:Label ID="lblPurchUomAPP" runat="server" Text='<%# Container.DataItem("UomCode") %>'
                                            Visible="true"></asp:Label>                                                 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="From Location">
                                    <ItemTemplate>                                        
                                       PR From : <asp:Label ID="lblOrderLoc" runat="server" Text='<%# Container.DataItem("PrLocDesc") %>'></asp:Label>                                                                                   
                                        <br />
                                        <asp:Label ID="lblPrCreated" runat="server" Text='<%# Container.DataItem("PRCreated") %>' Font-Italic="true"></asp:Label>                                       
                                        <br />
                                       Disp From : <asp:Label ID="lblDispLoc" runat="server" Text='<%# Container.DataItem("PL_LocDesc") %>'></asp:Label>                                                                                   
                                        <br />
                                        <asp:Label ID="lblStatusln" runat="server" Text='<%# Container.DataItem("StatusLn") %>'
                                            Visible="false"></asp:Label>                                        
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="14%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last App. By">
                                    <ItemTemplate>
                                        Pr App By : <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Container.DataItem("ApprovedBy") %>'></asp:Label> <br />
                                        Send By : <asp:Label ID="lblSenBy" runat="server" Text='<%# Container.DataItem("SendID") %>'></asp:Label>
                                        <asp:Label ID="lblAppBy_Level" runat="server" Text='<%# Container.DataItem("ApprovedBy_Level") %>'
                                            Visible="False"></asp:Label><br />
                                        
                                        <asp:Label ID="lblUpdDate" runat="server" Text='<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>' Width="112px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Disposition <br> To User PO">
                                    <ItemTemplate>                                        
                                    <GG:AutoCompleteDropDownList id=ddlToUser width="100%" CssClass="fontObject" runat=server />                                                                           
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="LEFT" Width="15%" />
                                </asp:TemplateColumn>
                                
                                 <asp:TemplateColumn  HeaderText="Send By Item">
                                    <ItemTemplate>                                    
                                        <asp:Button Text="Send" OnClick=BtnSend_Click runat="server" ID="BtnSend" Font-Size="7pt" Width="35px" Visible="True" Height="26px" ToolTip="click Send for submit PR"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateColumn>                        

                                 <asp:TemplateColumn  HeaderText="Send All By PR">
                                    <ItemTemplate>                                    
                                        <asp:Button Text="Send By PR" OnClick=BtnSendPR_Click runat="server" ID="BtnSendPR" Font-Size="7pt" Width="63px" Visible="True" Height="26px" BackColor="blue" ToolTip="click Send for submit PR"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateColumn>   
                                                                
                                <asp:TemplateColumn HeaderText="Disposition To <br> User Other Location">
                                    <ItemTemplate>
                                    <GG:AutoCompleteDropDownList id=ddlToLocCode AutoPostBack=true onSelectedIndexChanged="BindToMgrUser" width="75%" CssClass="fontObject"  runat=server />                                       
                                    <asp:Button Text="Yes" OnClick=BtnYes_Click runat="server" ID="BtnYes" Font-Size="7pt" Width="20%" Visible="True" Height="20px" ToolTip="click Yes for Disposition"/>
                                     <asp:Label ID="lblToLocCode" Visible=true runat="server"></asp:Label><Br />
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="LEFT" Width="15%" />
                                </asp:TemplateColumn>
            
                                <asp:TemplateColumn HeaderText="PL. Status" Visible=false>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPLstatus" runat="server" Text='<%# Container.DataItem("Pl_Status") %>' Width="112px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Approved" runat="server" CausesValidation="False" CommandName="Approved"
                                            Text="Approved" Visible="False"></asp:LinkButton>
                                        <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="Update" runat="server" CausesValidation="False" CommandName="Update"
                                            Text="Update" Visible=false></asp:LinkButton>
                        
                                        <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Cancel" Visible=false></asp:LinkButton>&nbsp;                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle CssClass="mr-h" />
                        </asp:DataGrid>
                    </div>
                    </td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" Visible="False" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" Visible="False" />
					</td>
				</tr>
				
				<tr>
					<td align="left" width="80%" ColSpan=6>
					    <asp:ImageButton ID="ImgRefresh" OnClick="Btnrefresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />
					    <asp:ImageButton ID="imgProcess" OnClick="BtnProcess_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_process.gif" Visible=false />
						<asp:ImageButton id=Stock UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=DC UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=WS UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" /> 
						<asp:ImageButton id=FA UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=NU UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />						
						<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" visible=false/>
                        <table>
                            <tr>
                                <td style="width: 59px">
                                </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>                            
                            <tr>
                                <td style="width: 59px; height: 21px;">
                                    <span style="font-size: 8pt; font-family: Arial">Cancel&nbsp;</span></td>
                                <td style="width: 15px; height: 21px;">
                                    :</td>
                                <td style="width: 100px; height: 21px;">
                                    <asp:Label ID="Label1" runat="server" BackColor="Yellow" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    <span style="font-size: 8pt; font-family: Arial">
                                    Disposition From Other Location </span>
                                </td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label2" runat="server" BackColor="LightGreen" Text="-" Width="40px"></asp:Label></td>
                            </tr>                            
                            <tr>
                                <td style="width: 59px">
                                    <asp:Label ID="lblSearch" runat="server" Font-Bold="True" ForeColor="Yellow" Visible="False"></asp:Label></td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="srchPRTypeList" width=100% runat=server Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="srchPRLevelList" width=100% runat=server Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="srchStatusList" width=100% runat=server Visible="False" /></td>
                            </tr>
                        </table>
					</td>									
				</tr>
				<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage Font-Size="Large" ForeColor="Red" visible=false Text="Error while initiating component." runat=server /></table>
				                </ContentPane>
			           </igtab:Tab> 
			  
			  			  
			                <igtab:Tab Key="PRSENT" Text="SENT ITEM" Tooltip="SENT ITEM">
                                <ContentPane>                                		                 
			                        <table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>					
					<td align="right" colspan="3" style="height: 21px" ><asp:label id="lblTracker_sent" runat="server"/></td>
				</tr>				
				<tr>
					<td colspan=6 width=100% style="background-color:#FFCC00">
						<table width="100%" style="background-color:#FFCC00"cellspacing="0" cellpadding="3" border="0" align="center" style="height: 40px">
							<tr class="mb-c">
							<td valign=bottom width=10% style="height: 39px">PR Status :<BR>
								    <asp:DropDownList id="lstOutstanding" width=100% runat=server>
										<asp:ListItem value="0" Selected>All</asp:ListItem>
										<asp:ListItem value="1">Outstanding</asp:ListItem>
										<asp:ListItem value="2">Realisation</asp:ListItem>										
									</asp:DropDownList></td>	
								<td valign=bottom width=20% style="height: 39px">Purchase Requisition ID :<BR><asp:TextBox id="srchPRID_Sent" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=20% style="height: 39px">Item Name :<BR><asp:TextBox id="srchItem_Sent" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=10% style="height: 39px">Period :<BR>
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
									</asp:DropDownList></td>
									
								<td valign=bottom width=15% style="height: 39px"><BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList></td>
			                    <td valign="bottom" width=15% style="height: 39px">User PO To :
                                        <asp:DropDownList id="lstDisposTo" width=100% visible=true runat=server >
										</asp:DropDownList></td>									
								<td valign=bottom width=8% style="height: 39px">
                                 </td>   
					
								<td valign=bottom width=10% style="height: 39px"><asp:TextBox id=TextBox3 width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=12% style="height: 39px"><BR></td>
								<td valign=bottom width=10% align=right style="height: 39px"><asp:Button Text="Search" OnClick=srchBtnSent_Click CssClass="button-small" runat="server" ID="srchBtnSent"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD colspan=6>
                        <asp:DataGrid ID="dgPRSend" runat="server" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-Visible="False"
							OnEditCommand="DEDR_Edit"
							OnCancelCommand="DEDR_Cancel"
							OnUpdateCommand="DEDR_Update"
							OnItemDataBound="dgPRListing_BindGrid"
                            Width="100%" AllowPaging="True" class="font9Tahoma">
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
                                <asp:TemplateColumn HeaderText="No">
                                        <ItemTemplate>
                                              <asp:Label ID="lblNoUrutSend" runat="server" Visible="True"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                </asp:TemplateColumn>
                                
                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PrIDSwow" HeaderText="PR ID" Visible="True" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ItemCode" HeaderText="Item Code" Visible="False"></asp:BoundColumn>
                          			<asp:HyperLinkColumn   Visible=false 
							HeaderText="PR ID" 
							DataNavigateUrlField="PRID" 							
							DataTextField="PRID"
							DataTextFormatString="{0:c}"							
							SortExpression="PRID" />
                                <asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
                                    <ItemTemplate>
                                        <asp:Label ID="ItemCode" runat="server" Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>
                                        (<%# Container.DataItem("ItemDesc") %>)
                                        <br />
                                        <asp:Label ID="lblAddNote" runat="server" Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                        <asp:TextBox ID="lstAddNote" runat="server" Text='<%# trim(Container.DataItem("AdditionalNote")) %>'
                                            Visible="false"></asp:TextBox>
                                        <asp:Label ID="LnID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Quantity Requested">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyReq" runat="server" Text='<%# Container.DataItem("QtyReq") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblQtyReqDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %>'></asp:Label>
                                        
                                  <br />
                                  <asp:Label ID="lblPurchUomReqSent" runat="server" Text='<%# Container.DataItem("UomCode") %>'
                                            Visible="true"></asp:Label>     
                                            
                                        <asp:TextBox ID="lstQtyReq" runat="server" MaxLength="8" Text='<%# trim(Container.DataItem("QtyReq")) %>'
                                            Visible="false" Width="90%"></asp:TextBox>
                                        &nbsp;
                                        <br />
                                        <asp:Label ID="lblUnitCost" runat="server" Text='<%# Container.DataItem("Cost") %>'
                                            Visible="false"></asp:Label>
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Quantity Approved">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyApp" runat="server" Text='<%# Container.DataItem("QtyApp") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblQtyAppDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyApp"),2) %>'></asp:Label>
                                        <asp:TextBox ID="lstQtyApp" runat="server" MaxLength="8" Text='<%# trim(Container.DataItem("QtyApp")) %>'
                                            Visible="false" Width="90%"></asp:TextBox>&nbsp;
                                    <asp:Label ID="lblPurchUomAPPSent" runat="server" Text='<%# Container.DataItem("UomCode") %>'
                                            Visible="true"></asp:Label>     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="DTH ID">
                                    <ItemTemplate>
			                        <asp:LinkButton id=lnkDTHID CommandName=Item text='<%# Container.DataItem("DTHID") %>'  runat=server />
                                        <asp:Label ID="lblDthID" runat="server" Text='<%# Container.DataItem("DTHID") %>'                                        
                                            Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="LEFT" Width="8%" />
                                </asp:TemplateColumn>     
                                                                
                                <asp:TemplateColumn HeaderText="Purchase Order ID &lt;br&gt; PO Date">
                                    <ItemTemplate>
                                        <!--
										<asp:label text=<%# Container.DataItem("QtyOutstanding") %> id="lblQtyOutstanding" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),5) %> id="lblQtyOutstandingDisplay" runat="server" />
										-->
                                    <asp:LinkButton id=lnkPOID CommandName=Item text='<%# Container.DataItem("POIDLast") %>'  runat=server />
			                        <asp:Label id=lblPOID text='<%# Container.DataItem("POIDLast") %>'  visible=False runat=server />			                        										
                                        <asp:Label ID="lblPOIDLast" Visible=false runat="server" Text='<%# Container.DataItem("POIDLast") %>'></asp:Label>
                                        <asp:Label ID="lblPODate" runat="server" Text='<%# Container.DataItem("PODate") %>'
                                            Visible="false"></asp:Label><br />
                                        <asp:Label ID="lblPODateDisplay" Visible=False runat="server" Text='<%# ObjGlobal.GetLongDate(Container.DataItem("PODate")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:TemplateColumn>
                                  
                                                                                
                                <asp:TemplateColumn HeaderText="Order From Location">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblOrderLoc" runat="server" Text='<%# Container.DataItem("PrLocCode") %>' Font-Bold="True"></asp:Label>                                                                                   
                                        <br />
                                        <asp:Label ID="lblPrCreatedSend" runat="server" Text='<%# Container.DataItem("PRCreated") %>' Font-Italic="true"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblStatusln" runat="server" Text='<%# Container.DataItem("StatusLn") %>'
                                            Visible="false"></asp:Label>                                        
                                            
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Disposition <br> User PO">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblUserNamePO" runat="server" Text='<%# Container.DataItem("UserName") %>'
                                            Visible="True"></asp:Label>                                        
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last App. By">
                                    <ItemTemplate>
                                    Send By : <asp:Label ID="lblSendBy" runat="server" Text='<%# Container.DataItem("UpdateID") %>' Width="112px"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblUpdDate" runat="server" Text='<%# ObjGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>' Width="112px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Status" Visible=false>
                                    <ItemTemplate>
                                        <asp:Label ID="lblplStatus_Sent" runat="server" Text='<%# Container.DataItem("PL_Status") %>'
                                            Visible="False"></asp:Label>
                                          <asp:Label ID="lblPLStatusDesc" runat="server" 
                                            Visible="True"></asp:Label>
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="LEFT" />
                                    <ItemStyle HorizontalAlign="LEFT" Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Approved" runat="server" CausesValidation="False" CommandName="Approved"
                                            Text="Approved" Visible="False"></asp:LinkButton>
                                        <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="Update" runat="server" CausesValidation="False" CommandName="Update"
                                            Text="Update" Visible=false></asp:LinkButton>
                        
                                        <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Cancel" Visible=false></asp:LinkButton>&nbsp;
                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>                                                                            
                                        <asp:Button Text="Retrieve" OnClick=BtnCancel_Click runat="server" ID="BtnCancel" Font-Size="7pt" Width="50px" Visible="True" Height="26px" ToolTip="click cancel PR"/>&nbsp;                                        
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn>
                                    <ItemTemplate>                                                                            
                                        <asp:Button Text="Retrieve By PR" OnClick=BtnCancelPR_Click runat="server" ID="BtnCancelPR" Font-Size="7pt" Width="85px" Visible="True" Height="26px" ToolTip="click cancel PR"/>&nbsp;                                        
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                            </Columns>
                            <HeaderStyle CssClass="mr-h" />
                        </asp:DataGrid></td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:DropDownList id="lstDropList_Sent" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged_Sent" runat="server" />			         	
					</td>
				</tr>				
				<tr>
					<td align="left" width="80%" ColSpan=6>					    
						<asp:ImageButton id=ImageButton4 UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=ImageButton5 UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=ImageButton6 UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" /> 
						<asp:ImageButton id=ImageButton7 UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=ImageButton8 UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />						
						<asp:ImageButton id=ImageButton9 UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" visible=false/>
                        <table>
                            <tr>
                                <td style="width: 59px">
                                </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>                            
                             <tr>
                                <td style="width: 59px">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Yellow" Visible="False"></asp:Label></td>
                                <td style="width: 15px">
                                </td>
 
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
   
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
 
                            </tr>
                        </table>
					</td>									
				</tr>
				<asp:label id="Label11" Visible="False" Runat="server"></asp:label><asp:label id="Label12" Visible="False" Runat="server"></asp:label><asp:Label id=Label13 visible=false Text="Error while initiating component." runat=server /></table>
				                </ContentPane>
			           </igtab:Tab>          
			           </Tabs>
			            </igtab:UltraWebTab>
             </div>
             </td>
             </tr>
             </table>
			</form>
		</body>
</html>
