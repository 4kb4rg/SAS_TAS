<%@ Page Language="vb" src="../../../include/IN_trx_PurReq_APP.aspx.vb" Inherits="IN_PurReq_APP" %>
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
							<td><strong>PURCHASE REQUISITION APPROVAL</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
						<tr>
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td valign="middle" width=10% style="height: 56px">
                                        Type :
                                        <asp:DropDownList id="SrchddlType" width=100% visible="true" runat=server >
											<asp:ListItem Value="0">All</asp:ListItem>
											<asp:ListItem Value="1">PR.Inbox</asp:ListItem>
											<asp:ListItem Value="2">PR. History</asp:ListItem>											
										</asp:DropDownList>

                                </td>
								<td valign=middle width=20% style="height: 56px">Purchase Requisition ID :<BR><asp:TextBox id="srchPRID" width=100% maxlength="32" runat="server"/></td>
								<td valign=middle width=15% style="height: 56px">Item Name :<BR><asp:TextBox id="srchItem" width=100% maxlength="32" runat="server"/></td>
								<td width="20%" height="26">From Location :<BR><asp:DropDownList id="ddlLocation" width=100% runat="server"/></td>
								<td valign=middle width=10% style="height: 56px">Period :<BR>
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
								<td valign=middle width=10% style="height: 56px"><BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList></td>
								<td valign=middle width=8% style="height: 56px">
                                    Item Status :<asp:DropDownList id="srchStatusLnList" width=100% runat=server /></td>

								<td valign=middle width=10% style="height: 56px"><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
								<td valign=middle width=10% align=right style="height: 56px"><asp:Button Text="Search" OnClick=srchBtn_Click runat="server" ID="BtnSearch" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
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
                                <igtab:Tab Key="PRList" Text="PURCHASE REQUISITION INBOX" Tooltip="PURCHASE REQUISITION INBOX">
                                    <ContentPane>
							            <table cellpadding="4" cellspacing="2" style="width: 100%">
                                    <tr>
                                    <td>
                                        <asp:DataGrid ID="dgPRListing" runat="server" 
                                            AllowPaging="True" class="font9Tahoma"
                                            AllowSorting="True" 
                                            AutoGenerateColumns="False" 
                                            OnCancelCommand="DEDR_Cancel" 
                                            OnEditCommand="DEDR_Edit" 
                                            OnUpdateCommand="DEDR_Update" 
                                             OnItemDataBound="dgLine_BindGrid" 
                                            PagerStyle-Visible="False" Width="100%">
                                            <PagerStyle Visible="False" />
						<HeaderStyle CssClass="mr-h"  Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l"   Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r"  Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>		
                                            <Columns>
                                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ItemCode" HeaderText="Item Code" Visible="False">
                                                </asp:BoundColumn>

                                                <asp:TemplateColumn HeaderText="PR. From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderLoc" runat="server" 
                                                            Text='<%# Container.DataItem("PRLocCode") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOrderFrom" runat="server" Font-Bold="true" 
                                                            Text='<%# Container.DataItem("PrLocDescription") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                        Dept : <asp:Label ID="lblDeptCode" runat="server" Font-Italic="true"   Font-Bold="true" Visible="true" Text='<%# Container.DataItem("DeptCode") %>'></asp:Label>
                                                        <asp:Label ID="lblLocLevel" runat="server" Font-Italic="true"   Font-Bold="true" Visible="false" Text='<%# Container.DataItem("LocLevelDesc") %>'></asp:Label>
                                                                                                           
 
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="13%" />
                                                </asp:TemplateColumn>

                                                <asp:HyperLinkColumn DataNavigateUrlField="PRID" 
                                                    DataNavigateUrlFormatString="IN_PurReq_Edit.aspx?PRID={0}" DataTextField="PRID" 
                                                    DataTextFormatString="{0:c}" HeaderText="PR ID" SortExpression="PRID">
                                                </asp:HyperLinkColumn>


                                                <asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ItemCode" runat="server" 
                                                            Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>
                                                        (<%# Container.DataItem("ItemDesc") %>)
                                        <br  />
                                                        <asp:Label ID="lblAddNote" runat="server" 
                                                            Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                                        <asp:Label ID="LnID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' 
                                                            Visible="false"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblPRID" runat="server" Text='<%# Container.DataItem("PRID") %>' 
                                                            Visible="false"></asp:Label>
                                                        <br  />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Stock UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOMCode" runat="server" 
                                                            Text='<%# Container.DataItem("UOMCode") %>'></asp:Label>
                                                        <asp:Label ID="hidStatus" runat="server" 
                                                            Text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("Status")) %>' 
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Saldo Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaldoStock" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOnHand"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantity Requested">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyReq" runat="server" 
                                                            Text='<%# Container.DataItem("QtyReq") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyReqDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %>'></asp:Label>
                                                        <asp:TextBox ID="lstQtyReq" runat="server" MaxLength="8" 
                                                            Text='<%# trim(Container.DataItem("QtyReq")) %>' Visible="false" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rvQtyReq" runat="server" 
                                                            ControlToValidate="lstQtyReq" Display="Dynamic" 
                                                            Text="&lt;br&gt;Maximum length 9 digits and 5 decimal points" 
                                                            ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="vQtyReq" runat="server" 
                                                            ControlToValidate="lstQtyReq" Display="dynamic" 
                                                            ErrorMessage="&lt;br&gt;Please specify quantity to request"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rQtyReq" runat="server" ControlToValidate="lstQtyReq" 
                                                            Display="dynamic" EnableClientScript="True" MaximumValue="999999999999999" 
                                                            MinimumValue="0" Text="&lt;br&gt;The value is out of acceptable range!" 
                                                            Type="double"></asp:RangeValidator>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantity Approved">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyApp" runat="server" 
                                                            Text='<%# Container.DataItem("QtyApp") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyAppDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyApp"),2) %>'></asp:Label>
                                                        <asp:TextBox ID="lstQtyApp" runat="server" MaxLength="8" 
                                                            Text='<%# trim(Container.DataItem("QtyApp")) %>' Visible="false" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rvQtyApp" runat="server" 
                                                            ControlToValidate="lstQtyApp" Display="Dynamic" 
                                                            Text="&lt;br&gt;Maximum length 9 digits and 5 decimal points" 
                                                            ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="vQtyApp" runat="server" 
                                                            ControlToValidate="lstQtyApp" Display="dynamic" 
                                                            ErrorMessage="&lt;br&gt;Please specify quantity to approved"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rQtyApp" runat="server" ControlToValidate="lstQtyApp" 
                                                            Display="dynamic" EnableClientScript="True" MaximumValue="999999999999999" 
                                                            MinimumValue="0" Text="&lt;br&gt;The value is out of acceptable range!" 
                                                            Type="double"></asp:RangeValidator>
                                                     </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Cost ">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" 
                                                            Text='<%# Container.DataItem("Cost") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblAmount" runat="server" 
                                                            Text='<%# Container.DataItem("Amount") %>'></asp:Label>
 
                                                    </ItemTemplate>

                                                   
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>

                                               
                                                <asp:TemplateColumn HeaderText="Quantity/Price &lt;br&gt; on Last Order" 
                                                    Visible="False">
                                                    <ItemTemplate>
                                        <!--
										<asp:label text=<%# Container.DataItem("QtyRcv") %> id="lblQtyRcv" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyRcv"),5) %> id="lblQtyRcvDisplay" runat="server" />
										-->
                                                        <asp:Label ID="lblQtyOrderLast" runat="server" 
                                                            Text='<%# Container.DataItem("QtyOrderLast") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyOrderLastDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrderLast"),2) %>'></asp:Label>
                                                        <br>
                                                        <asp:Label ID="lblCostOrderLastDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("CostLast"),2) %>'></asp:Label>
                                                        </br>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Disposition To" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDispLocTo" runat="server" BackColor="PaleGreen" 
                                                            Text='<%# Container.DataItem("DispLocation") %>'></asp:Label>
                                        <br  />
                                                        <asp:Label ID="lblDispUserTo" runat="server" BackColor="PaleGreen" 
                                                            Text='<%# Container.DataItem("DispUser") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Purchase Order By &lt;br&gt;" Visible="false">
                                                    <ItemTemplate>
                                        <!--
										<asp:label text=<%# Container.DataItem("QtyOutstanding") %> id="lblQtyOutstanding" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),5) %> id="lblQtyOutstandingDisplay" runat="server" />
										-->
                                                        <asp:Label ID="lblPOIDLast" runat="server" 
                                                            Text='<%# Container.DataItem("POIDLast") %>'></asp:Label>
                                                        <asp:Label ID="lblPODate" runat="server" 
                                                            Text='<%# Container.DataItem("PODate") %>' Visible="false"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblPODateDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetLongDate(Container.DataItem("PODate")) %>' 
                                                            Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Item Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatusDescln" runat="server" 
                                                            Text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("StatusLn")) %>'></asp:Label>
                                                        <asp:Label ID="lblStatusln" runat="server" 
                                                            Text='<%# Container.DataItem("StatusLn") %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="lstStatusLn" runat="server" size="1" Visible="false" 
                                                            Width="95%">
                                                        </asp:DropDownList>
                                                        <br>
                                                        <asp:RequiredFieldValidator ID="validateStatusln" runat="server" 
                                                            ControlToValidate="lstStatusLn" Display="dynamic"></asp:RequiredFieldValidator>
                                                        </br>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Last App. By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApprovedBy" runat="server"  
                                                            Text='<%# Container.DataItem("ApprovedBy")  %>'></asp:Label>
                                                        <asp:Label ID="lblAppBy_Level" runat="server" 
                                                            Text='<%# Container.DataItem("ApprovedBy_Level") %>' Visible="False"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblUpdateid" runat="server" Visible="false"
                                                            Text='<%# Container.DataItem("UserName") %>' Width="112px"></asp:Label>
                                                        
                                                        <asp:Label ID="lblUpdDate" runat="server" 
                                                            Text='<%#  Container.DataItem("UpdateDate") %>' Width="112px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Next App. By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNextAppBy" runat="server" Font-Bold="True" Font-Italic="True" 
                                                            Text='<%# Container.DataItem("NextApprovedBy") %>'></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblAppLevel" runat="server" 
                                                            Text='<%# Container.DataItem("AppLevel") %>' Visible="False"></asp:Label>
                                                        <asp:Label ID="lblhidApprovedBy" runat="server" 
                                                            Text='<%# Container.DataItem("indApprovedBy") %>' Visible="false"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblNextApp_Level" runat="server" 
                                                            Text='<%# Container.DataItem("NextApprovedBy_Level") %>' Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Created By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserPR" runat="server" 
                                                            Text='<%# Container.DataItem("UserPR") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>

                                                 

                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Approved" runat="server" CausesValidation="False" 
                                                            CommandName="Approved" Text="Approved" Visible="False"></asp:LinkButton>
                                                        <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" 
                                                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                        <asp:LinkButton ID="Update" runat="server" CausesValidation="False" 
                                                            CommandName="Update" Text="Update" Visible="false"></asp:LinkButton>
                                                        <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" 
                                                            CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                                        &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Approved">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BtnApproved" runat="server" Font-Size="7pt" Height="26px" 
                                                            OnClick="BtnApproved_Click" Text="Approved" ToolTip="click approved PR" 
                                                            Width="56px" />
                                                        <asp:Button ID="BtnCancel" runat="server" Font-Size="7pt" Height="26px" 
                                                            OnClick="BtnCancel_Click" Text="Retrieve" ToolTip="click cancel PR" 
                                                            Visible="False" Width="56px" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="mr-h" />
                                        </asp:DataGrid>
                                    </td>
                                    </tr>
                                    </table>
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
								
                                    </ContentPane>
                                </igtab:Tab>
                                <igtab:Tab Key="PRList" Text="PURCHASE REQUISITION HISTORY" Tooltip="PURCHASE REQUISITION LIST">
                                    <ContentPane>
                                        <table cellpadding="4" cellspacing="2" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="dgPrSent" runat="server" 
                                            AllowPaging="false" class="font9Tahoma"
                                            AllowSorting="True" 
                                            AutoGenerateColumns="False" 
                                             OnItemDataBound="dgLine_BindGrid" 
                                            PagerStyle-Visible="False" Width="100%">
                                            <PagerStyle Visible="False" />
						<HeaderStyle CssClass="mr-h"   Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l"  Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>		
                                            <Columns>
                                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ItemCode" HeaderText="Item Code" Visible="False">
                                                </asp:BoundColumn>

                                                <asp:TemplateColumn HeaderText="PR. From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderLoc" runat="server" 
                                                            Text='<%# Container.DataItem("PRLocCode") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOrderFrom" runat="server" Font-Bold="true" 
                                                            Text='<%# Container.DataItem("PrLocDescription") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                        PR Level : <asp:Label ID="lblLocLevel" runat="server" Font-Italic="true"   Font-Bold="true" 
                                                            Text='<%# Container.DataItem("LocLevelDesc") %>'></asp:Label>

                                                
 
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="13%" />
                                                </asp:TemplateColumn>

                                                <asp:HyperLinkColumn DataNavigateUrlField="PRID" 
                                                    DataNavigateUrlFormatString="IN_PurReq_Edit.aspx?PRID={0}" DataTextField="PRID" 
                                                    DataTextFormatString="{0:c}" HeaderText="PR ID" SortExpression="PRID">
                                                </asp:HyperLinkColumn>


                                                <asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ItemCode" runat="server" 
                                                            Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>
                                                        (<%# Container.DataItem("ItemDesc") %>)
                                        <br  />
                                                        <asp:Label ID="lblAddNote" runat="server" 
                                                            Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                                        <asp:Label ID="LnID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' 
                                                            Visible="false"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblPRID" runat="server" Text='<%# Container.DataItem("PRID") %>' 
                                                            Visible="false"></asp:Label>
                                                        <br  />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Stock UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOMCode" runat="server" 
                                                            Text='<%# Container.DataItem("UOMCode") %>'></asp:Label>
                                                        <asp:Label ID="hidStatus" runat="server" 
                                                            Text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("Status")) %>' 
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Saldo Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaldoStock" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOnHand"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantity Requested">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyReq" runat="server" 
                                                            Text='<%# Container.DataItem("QtyReq") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyReqDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %>'></asp:Label>
                                                        <asp:TextBox ID="lstQtyReq" runat="server" MaxLength="8" 
                                                            Text='<%# trim(Container.DataItem("QtyReq")) %>' Visible="false" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rvQtyReq" runat="server" 
                                                            ControlToValidate="lstQtyReq" Display="Dynamic" 
                                                            Text="&lt;br&gt;Maximum length 9 digits and 5 decimal points" 
                                                            ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="vQtyReq" runat="server" 
                                                            ControlToValidate="lstQtyReq" Display="dynamic" 
                                                            ErrorMessage="&lt;br&gt;Please specify quantity to request"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rQtyReq" runat="server" ControlToValidate="lstQtyReq" 
                                                            Display="dynamic" EnableClientScript="True" MaximumValue="999999999999999" 
                                                            MinimumValue="0" Text="&lt;br&gt;The value is out of acceptable range!" 
                                                            Type="double"></asp:RangeValidator>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantity Approved">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyApp" runat="server" 
                                                            Text='<%# Container.DataItem("QtyApp") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyAppDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyApp"),2) %>'></asp:Label>
                                                        <asp:TextBox ID="lstQtyApp" runat="server" MaxLength="8" 
                                                            Text='<%# trim(Container.DataItem("QtyApp")) %>' Visible="false" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rvQtyApp" runat="server" 
                                                            ControlToValidate="lstQtyApp" Display="Dynamic" 
                                                            Text="&lt;br&gt;Maximum length 9 digits and 5 decimal points" 
                                                            ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="vQtyApp" runat="server" 
                                                            ControlToValidate="lstQtyApp" Display="dynamic" 
                                                            ErrorMessage="&lt;br&gt;Please specify quantity to approved"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rQtyApp" runat="server" ControlToValidate="lstQtyApp" 
                                                            Display="dynamic" EnableClientScript="True" MaximumValue="999999999999999" 
                                                            MinimumValue="0" Text="&lt;br&gt;The value is out of acceptable range!" 
                                                            Type="double"></asp:RangeValidator>
                                        

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Cost ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitCost" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Cost"),2) %>' Visible="TRUE"></asp:Label> <br />
                                                        <asp:Label ID="lblAmount" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Amount"),2) %>' Visible="TRUE"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Quantity/Price &lt;br&gt; on Last Order" 
                                                    Visible="False">
                                                    <ItemTemplate>
                                        <!--
										<asp:label text=<%# Container.DataItem("QtyRcv") %> id="lblQtyRcv" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyRcv"),5) %> id="lblQtyRcvDisplay" runat="server" />
										-->
                                                        <asp:Label ID="lblQtyOrderLast" runat="server" 
                                                            Text='<%# Container.DataItem("QtyOrderLast") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyOrderLastDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrderLast"),2) %>'></asp:Label>
                                                        <br>
                                                        <asp:Label ID="lblCostOrderLastDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("CostLast"),2) %>'></asp:Label>
                                                        </br>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Disposition To" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDispLocTo" runat="server" BackColor="PaleGreen" 
                                                            Text='<%# Container.DataItem("DispLocation") %>'></asp:Label>
                                        <br  />
                                                        <asp:Label ID="lblDispUserTo" runat="server" BackColor="PaleGreen" 
                                                            Text='<%# Container.DataItem("DispUser") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Purchase Order By &lt;br&gt;">
                                                    <ItemTemplate>
                                        <!--
										<asp:label text=<%# Container.DataItem("QtyOutstanding") %> id="lblQtyOutstanding" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),5) %> id="lblQtyOutstandingDisplay" runat="server" />
										-->
                                                        <asp:Label ID="lblPOIDLast" runat="server" 
                                                            Text='<%# Container.DataItem("POIDLast") %>'></asp:Label>
                                                        <asp:Label ID="lblPODate" runat="server" 
                                                            Text='<%# Container.DataItem("PODate") %>' Visible="false"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblPODateDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetLongDate(Container.DataItem("PODate")) %>' 
                                                            Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Item Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatusDescln" runat="server"   
                                                            Text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("StatusLn")) %>'></asp:Label>
                                                        <asp:Label ID="lblStatusln" runat="server" 
                                                            Text='<%# Container.DataItem("StatusLn") %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="lstStatusLn" runat="server" size="1" Visible="false" 
                                                            Width="95%">
                                                        </asp:DropDownList>
                                                        <br>
                                                        <asp:RequiredFieldValidator ID="validateStatusln" runat="server" 
                                                            ControlToValidate="lstStatusLn" Display="dynamic"></asp:RequiredFieldValidator>
                                                        </br>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Last App. By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApprovedBy" runat="server" 
                                                            Text='<%# Container.DataItem("ApprovedBy") %>'></asp:Label>
                                                        <asp:Label ID="lblAppBy_Level" runat="server" 
                                                            Text='<%# Container.DataItem("ApprovedBy_Level") %>' Visible="False"></asp:Label>
                                                        <br  />
                                        
                                                        <asp:Label ID="lblUpdDate" runat="server" 
                                                            Text='<%# Container.DataItem("UpdateDate") %>' Width="112px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Next App. By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNextAppBy" runat="server" Font-Bold="True" Font-Italic="True" 
                                                            Text='<%# Container.DataItem("NextApprovedBy") %>'></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblAppLevel" runat="server" 
                                                            Text='<%# Container.DataItem("AppLevel") %>' Visible="False"></asp:Label>
                                                        <asp:Label ID="lblhidApprovedBy" runat="server" 
                                                            Text='<%# Container.DataItem("indApprovedBy") %>' Visible="false"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblNextApp_Level" runat="server" 
                                                            Text='<%# Container.DataItem("NextApprovedBy_Level") %>' Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Created By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserPR" runat="server" 
                                                            Text='<%# Container.DataItem("UserPR") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>

                                                 

                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Approved" runat="server" CausesValidation="False" 
                                                            CommandName="Approved" Text="Approved" Visible="False"></asp:LinkButton>
                                                        <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" 
                                                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                        <asp:LinkButton ID="Update" runat="server" CausesValidation="False" 
                                                            CommandName="Update" Text="Update" Visible="false"></asp:LinkButton>
                                                        <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" 
                                                            CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                                        &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BtnRetrieved" runat="server" Font-Size="7pt" Height="26px" 
                                                            OnClick="BtnRetrieved_Click" Text="Approved" ToolTip="click Retrieve" 
                                                            Width="56px" />
                                                        <asp:Button ID="BtnCancel" runat="server" Font-Size="7pt" Height="26px" 
                                                            OnClick="BtnCancel_Click" Text="Retrieve" ToolTip="click cancel PR" 
                                                            Visible="False" Width="56px" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="mr-h" />
                                        </asp:DataGrid>
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
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>

							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton ID="ImgRefresh" OnClick="Btnrefresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />&nbsp;
						        <asp:ImageButton id=Stock UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />&nbsp;
						        <asp:ImageButton id=DC UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />&nbsp;
						        <asp:ImageButton id=WS UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" /> &nbsp;
						        <asp:ImageButton id=FA UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />&nbsp;
						        <asp:ImageButton id=NU UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />	&nbsp;					
						        
                                        <asp:DropDownList id="srchApprovedBy" width=100% visible="false" runat=server >
											<asp:ListItem Value="0">All</asp:ListItem>
											<asp:ListItem Value="1">Supervisor</asp:ListItem>
											<asp:ListItem Value="2">Manager</asp:ListItem>
											<asp:ListItem Value="3">GM</asp:ListItem>
											<asp:ListItem Value="4">VP/CEO</asp:ListItem>
										</asp:DropDownList>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;                         
                            </td>
                        </tr>
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
                                <td colspan="3">
                                    <span style="font-size: 9pt; font-family: Arial; text-decoration: underline"><strong>
                                        Next
                                    Approved By</strong></span></td>
                            </tr>
 
                            <tr>
                                <td style="width: 59px">
                                    <span style="font-size: 8pt; font-family: Arial Narrow">
                                    Supervisor</span></td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label3" runat="server" BackColor="GreenYellow" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    <span style="font-size: 8pt; font-family: Arial Narrow">
                                    Manager</span></td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label2" runat="server" BackColor="Blue" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    <span style="font-size: 8pt; font-family: Arial Narrow">
                                    Direktur</span></td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label4" runat="server" BackColor="Red" Text="-" Width="40px"></asp:Label></td>
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
                        <asp:TextBox ID="txtCost" runat="server" BackColor="Transparent" BorderStyle="None"
                            MaxLength="21" onkeyup="javascript:calTtlCost();" Width="25%"></asp:TextBox></td>									
				</tr>
				<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
					</table>
				</div>
				</td>
				<td>
		<table cellpadding="0" cellspacing="0" style="width: 20px">
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>                                
	
			</form>
		</body>
</html>
