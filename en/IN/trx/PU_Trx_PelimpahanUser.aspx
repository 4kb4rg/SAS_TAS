<%@ Page Language="vb" src="../../../include/PU_Trx_PelimpahanUser.aspx.vb" Inherits="PU_Trx_PelimpahanUser" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
  TagPrefix="igtab" %>

<html>
	<head>
		<title>DISPOSITION BY USER</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
 <style type="text/css">
                /* The Modal (background) */
                .modal {
                    display:none; /* Hidden by default */
                    position: fixed; /* Stay in place */
                    z-index: 1; /* Sit on top */
                    padding-top: 30px; /* Location of the box */
                    left: 0;
                    top: 0;
                    width: 80%; /* Full width */
                    height: 100%; /* Full height */
                    overflow: auto; /* Enable scroll if needed */
                    background-color: rgb(0,0,0); /* Fallback color */
                    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
                }

                /* Modal Content */
                .modal-content {
                    position: relative;
                    background-color: white;
                    margin: auto;
                    padding: 0;
                    border: 1px solid #888;
                    width: 80%;
                    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
                    -webkit-animation-name: animatetop;
                    -webkit-animation-duration: 0.4s;
                    animation-name: animatetop;
                    animation-duration: 0.4s
                }

                /* Add Animation */
                @-webkit-keyframes animatetop {
                    from {top:-300px; opacity:0}
                    to {top:0; opacity:1}
                }

                @keyframes animatetop {
                    from {top:-300px; opacity:0}
                    to {top:0; opacity:1}
                }

                /* The Close Button */
                .close {
                    color: white;
                    float: right;
                    font-size: 28px;
                    font-weight: bold;
                }

                .close:hover,
                .close:focus {
                    color: #000;
                    text-decoration: none;
                    cursor: pointer;
                }

                .modal-header {
                    padding: 2px 1px;
                    Height:50px;
                    background-color:lightgreen;
                    color: white;
                }

                .modal-body {padding: 2px 2px;}

                .modal-footer {
                    padding: 2px 5px;
                    background-color:lightgreen;
                    color: white;
                }
                </style>		
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">	
            
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">
                
                
                	    
		     <table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
            			<tr>
					<td class="mt-h" colspan="3" style="height: 21px"> PURCHASE REQUISITION ASSIGN TO USER</td>
				</tr>
				    <tr>
				    <td colspan=6 style="height: 11px"><hr size="1" noshade>
                        &nbsp;</td>
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
                 
			<table border="0" cellspacing="1" cellpadding="1" width="100%">			
				<tr>
					<td colspan="6"><UserControl:MenuINTrx id=menuIN runat="server" /></td>
				</tr>				
				<tr>					
					<td align="right" colspan="3" style="height: 21px" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>				
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" bgcolor=white cellspacing="0" cellpadding="3" border="0" align="center" style="height: 40px">
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
								<td valign=bottom width=10% align=right style="height: 39px"><asp:Button Text="Search" OnClick=srchBtn_Click runat="server" ID="BtnSearch"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD colspan=6>
	                        <asp:DataGrid ID="dgPRListing" runat="server" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-Visible="False"
							OnEditCommand="DEDR_Edit"
							OnCancelCommand="DEDR_Cancel"
							OnUpdateCommand="DEDR_Update"							
                            Width="100%" AllowPaging="True" 
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
                            <PagerStyle Visible="False" />
                            
                            <Columns>
                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="True" ></asp:BoundColumn>
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
                                        <br />
                                        <asp:Label ID="lblAddNote" runat="server" Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                        <asp:TextBox ID="lstAddNote" runat="server" Text='<%# trim(Container.DataItem("AdditionalNote")) %>'
                                            Visible="false"></asp:TextBox>
                                        <asp:Label ID="LnID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>                             
                                <asp:TemplateColumn HeaderText="From Location">
                                    <ItemTemplate>                                        
                                       PR From : <asp:Label ID="lblOrderLoc" runat="server" Text='<%# Container.DataItem("PrLocDesc") %>'></asp:Label>                                                                                   
                                        <br />
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="14%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Disposition By">
                                    <ItemTemplate>                                        
                                        Send By : <asp:Label ID="lblSenBy" runat="server" Text='<%# Container.DataItem("Updateid") %>'></asp:Label>
                                        <br />
                                        
                                        <asp:Label ID="lblUpdDate" runat="server" Text='<%# Container.DataItem("UpdateDate") %>' Width="112px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                          
                                <asp:TemplateColumn HeaderText="Disposition To Location" Visible=false>
                                    <ItemTemplate>                                    
                                    <asp:Label ID="lblToLocCode" runat="server" Text='<%# Container.DataItem("ToLocCode") %>' Width="112px"></asp:Label>                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="LEFT" Width="15%" />
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
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:TemplateColumn> 
                                                               
                                <asp:TemplateColumn Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Approved" runat="server" CausesValidation="False" CommandName="Approved"
                                            Text="Approved" Visible="False"></asp:LinkButton>
                                        <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Edit" Visible=false></asp:LinkButton>
                                        <asp:LinkButton ID="Update" runat="server" CausesValidation="False" CommandName="Update"
                                            Text="Update" Visible=false></asp:LinkButton>                        
                                        <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Cancel" Visible=false></asp:LinkButton>&nbsp;                                 
                                        <asp:LinkButton ID="WriteNote" runat="server" CausesValidation="False" CommandName="WriteNote"
                                            Text="Write Note" Visible=true></asp:LinkButton>&nbsp;                                                                                                 
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle CssClass="mr-h" />
                        </asp:DataGrid></td>
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
				<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /></table>
				                </ContentPane>
			           </igtab:Tab> 
			  
			  			  
			                <igtab:Tab Key="PRSENT" Text="PR REALISATION" Tooltip="PR REALISATION">
                                <ContentPane>                                		                 
			                        <table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>					
					<td align="right" colspan="3" style="height: 21px" ><asp:label id="lblTracker_sent" runat="server"/></td>
				</tr>				
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" bgcolor=white cellspacing="0" cellpadding="3" border="0" align="center" style="height: 40px">
							<tr class="mb-c">
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
								<td valign=bottom width=8% style="height: 39px">
                                    
								<td valign="bottom" width=15% style="height: 39px">
                                        <asp:DropDownList id="DropDownList4" width=100% visible=false runat=server >
											<asp:ListItem Value="0">All</asp:ListItem>
											<asp:ListItem Value="1">Supervisor</asp:ListItem>
											<asp:ListItem Value="2">Manager</asp:ListItem>
											<asp:ListItem Value="3">GM</asp:ListItem>
											<asp:ListItem Value="4">VP/CEO</asp:ListItem>
										</asp:DropDownList></td>
								<td valign=bottom width=10% style="height: 39px"><asp:TextBox id=TextBox3 width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=bottom width=12% style="height: 39px"><BR></td>
								<td valign=bottom width=10% align=right style="height: 39px"><asp:Button Text="Search" OnClick=srchBtnSent_Click runat="server" ID="srchBtnSent"/></td>
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
							
                            Width="100%" AllowPaging="True" 
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
                            <PagerStyle Visible="False" />
                             
                            <Columns>
                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="True"></asp:BoundColumn>
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
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Order From Location">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblOrderLoc" runat="server" Text='<%# Container.DataItem("PrLocCode") %>' Font-Bold="True"></asp:Label>                      
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Disposition To Location">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblSendToLoc" runat="server" Text='<%# Container.DataItem("ToLocCode") %>'
                                            Visible="True"></asp:Label>                                        
                                        </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Disposition By">
                                    <ItemTemplate>
                                    Send By : <asp:Label ID="lblSendBy" runat="server" Text='<%# Container.DataItem("UpdateID") %>' Width="112px"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblUpdDate" runat="server" Text='<%# Container.DataItem("UpdateDate") %>' Width="112px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="RFQ ID">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblRfqId" runat="server" Text='<%# Container.DataItem("RfqID") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RPHID">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblRhpID" runat="server" Text='<%# Container.DataItem("RphID") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateColumn>                                                                
                                <asp:TemplateColumn HeaderText="POID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOIDLast" runat="server" Text='<%# Container.DataItem("POIDLast") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
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
                                <td style="width: 100px">
                                    <asp:DropDownList id="DropDownList6" width=100% runat=server Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="DropDownList7" width=100% runat=server Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="DropDownList8" width=100% runat=server Visible="False" /></td>
                            </tr>
                        </table>
					</td>									
				</tr>
				<asp:label id="Label11" Visible="False" Runat="server"></asp:label><asp:label id="Label12" Visible="False" Runat="server"></asp:label><asp:Label id=Label13 visible=false Text="Error while initiating component." runat=server /></table>
				                </ContentPane>
			           </igtab:Tab>          
			           </Tabs>
			            </igtab:UltraWebTab>
                <br />
                <br />
		    <!-- Trigger/Open The Modal -->

<div id="myModal" class="modal" runat=server>
 <div class="modal-content" runat=server style="left: 0px; top: 0px">
    <div class="modal-header" runat=server>
      <span class="close">×</span>
         <table style="width: 95%">
              <tr>
                  <td style="width: 36px">
                  </td>
                  <td style="width: 235px">
                  </td>
                  <td style="width: 100px">
                  </td>
              </tr>
              <tr>
                  <td style="width: 36px">
                      Supplier Code/Name</td>
                  <td style="width: 235px">
                      <asp:TextBox ID="txtItemCode" runat="server" MaxLength="128" Width="100%"></asp:TextBox></td>
                  <td style="width: 100px">
                      &nbsp;</td>
              </tr>
          </table>
    </div>
    <div class="modal-body" runat=server>
      &nbsp;</div>
    <div class="modal-footer">
        &nbsp;</div>
  </div>
</div>
<script>
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the button that opens the modal
    var btn = document.getElementById('TABBK__ctl0_dgPRListing_ctl03_WriteNote');

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal
    btn.onclick = function () {
        modal.style.display = "block";
        return false;
    }
                              
    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            return false;             
        }       
    }
</script>


        <br />
        </div>
        </td>
        </tr>
        </table>


			</form>
		</body>
</html>
