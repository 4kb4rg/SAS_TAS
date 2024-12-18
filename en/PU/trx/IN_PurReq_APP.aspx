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
							<td><strong>PURCHASE REQUISITION APPROVAL</strong><hr style="width :100%" />   
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
								<td valign=middle width=20% style="height: 56px">Purchase Requisition ID :<BR><asp:TextBox id="srchPRID" width=100% maxlength="32" runat="server"/></td>
								<td valign=middle width=20% style="height: 56px">Item Name :<BR><asp:TextBox id="srchItem" width=100% maxlength="32" runat="server"/></td>
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
								    <td valign=middle width=15% style="height: 56px"><BR>
								        <asp:DropDownList id="lstAccYear" width=100% runat=server>
									    </asp:DropDownList></td>
								    <td valign=middle width=8% style="height: 56px">
                                        Item Status :<asp:DropDownList id="srchStatusLnList" width=100% runat=server />
								    <td valign="middle" width=15% style="height: 56px">
                                        <asp:DropDownList id="srchApprovedBy" width=100% visible=false runat=server >
											<asp:ListItem Value="0">All</asp:ListItem>
											<asp:ListItem Value="1">Supervisor</asp:ListItem>
											<asp:ListItem Value="2">Manager</asp:ListItem>
											<asp:ListItem Value="3">GM</asp:ListItem>
											<asp:ListItem Value="4">VP/CEO</asp:ListItem>
										</asp:DropDownList></td>
								    <td valign=middle width=10% style="height: 56px"><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
								    <td valign=middle width=10% align=right style="height: 56px"><asp:Button Text="Search" OnClick=srchBtn_Click runat="server" ID="BtnSearch" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid ID="dgPRListing" runat="server" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-Visible="False"
							                OnEditCommand="DEDR_Edit"
							                OnCancelCommand="DEDR_Cancel"
							                OnUpdateCommand="DEDR_Update"
							
                                            Width="100%" AllowPaging="True"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                            <Columns>
                                                <asp:BoundColumn DataField="PRLnId" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="ItemCode" HeaderText="Item Code" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Order From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderLoc" runat="server" Text='<%# Container.DataItem("PRLocCode") %>'></asp:Label>
                                                        <asp:Label ID="lblOrderFrom" runat="server" Text='<%# Container.DataItem("PrLocDescription") %>'></asp:Label>                                                                                    
                                                    </ItemTemplate>
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateColumn>
                                                                
                                                <asp:HyperLinkColumn   
							                HeaderText="PR ID" 
							                DataNavigateUrlField="PRID" 
							                DataNavigateUrlFormatString="IN_PurReq_Edit.aspx?PRID={0}"
							                DataTextField="PRID"
							                DataTextFormatString="{0:c}"							
							                SortExpression="PRID" />
                                                <asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ItemCode" runat="server" Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>
                                                        (<%# Container.DataItem("ItemDesc") %>)
                                                        <br />
                                                        <asp:Label ID="lblAddNote" runat="server" Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                                        <asp:Label ID="LnID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' Visible="false"></asp:Label><br />
                                                        <asp:Label ID="lblPRID" runat="server" Text='<%# Container.DataItem("PRID") %>' Visible="false"></asp:Label><br />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Stock UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOMCode" runat="server" Text='<%# Container.DataItem("UOMCode") %>'></asp:Label>
                                                        <asp:Label ID="hidStatus" runat="server" Text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("Status")) %>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Saldo Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaldoStock" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOnHand"),2) %>'></asp:Label>                                                                                    
                                                    </ItemTemplate>
                                                     <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantity Requested">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyReq" runat="server" Text='<%# Container.DataItem("QtyReq") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyReqDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %>'></asp:Label>
                                                        <asp:TextBox ID="lstQtyReq" runat="server" MaxLength="8" Text='<%# trim(Container.DataItem("QtyReq")) %>'
                                                            Visible="false" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rvQtyReq" runat="server" ControlToValidate="lstQtyReq"
                                                            Display="Dynamic" Text="<br>Maximum length 9 digits and 5 decimal points" ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="vQtyReq" runat="server" ControlToValidate="lstQtyReq"
                                                            Display="dynamic" ErrorMessage="<br>Please specify quantity to request"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rQtyReq" runat="server" ControlToValidate="lstQtyReq" Display="dynamic"
                                                            EnableClientScript="True" MaximumValue="999999999999999" MinimumValue="0" Text="<br>The value is out of acceptable range!"
                                                            Type="double"></asp:RangeValidator><br />
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
                                                            Visible="false" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rvQtyApp" runat="server" ControlToValidate="lstQtyApp"
                                                            Display="Dynamic" Text="<br>Maximum length 9 digits and 5 decimal points" ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="vQtyApp" runat="server" ControlToValidate="lstQtyApp"
                                                            Display="dynamic" ErrorMessage="<br>Please specify quantity to approved"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rQtyApp" runat="server" ControlToValidate="lstQtyApp" Display="dynamic"
                                                            EnableClientScript="True" MaximumValue="999999999999999" MinimumValue="0" Text="<br>The value is out of acceptable range!"
                                                            Type="double"></asp:RangeValidator><br />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Quantity/Price &lt;br&gt; on Last Order">
                                                    <ItemTemplate>
                                                        <!--
										                <asp:label text=<%# Container.DataItem("QtyRcv") %> id="lblQtyRcv" visible="false" runat="server" />
										                <asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyRcv"),5) %> id="lblQtyRcvDisplay" runat="server" />
										                -->
                                                        <asp:Label ID="lblQtyOrderLast" runat="server" Text='<%# Container.DataItem("QtyOrderLast") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyOrderLastDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrderLast"),2) %>'></asp:Label><br>
                                                        <asp:Label ID="lblCostOrderLastDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("CostLast"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Disposition To">
                                                    <ItemTemplate>                                        
                                                        <asp:Label ID="lblDispLocTo" runat="server" Text='<%# Container.DataItem("DispLocation") %>' BackColor="PaleGreen"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDispUserTo" runat="server" Text='<%# Container.DataItem("DispUser") %>' BackColor="PaleGreen"></asp:Label>                                            
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
                                                        <asp:Label ID="lblPOIDLast" runat="server" Text='<%# Container.DataItem("POIDLast") %>'></asp:Label>
                                                        <asp:Label ID="lblPODate" runat="server" Text='<%# Container.DataItem("PODate") %>'
                                                            Visible="false"></asp:Label><br />
                                                        <asp:Label ID="lblPODateDisplay" runat="server" Text='<%# ObjGlobal.GetLongDate(Container.DataItem("PODate")) %>' Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                </asp:TemplateColumn>                                
                                                <asp:TemplateColumn HeaderText="Item Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatusDescln" runat="server" Text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("StatusLn")) %>'></asp:Label>
                                                        <asp:Label ID="lblStatusln" runat="server" Text='<%# Container.DataItem("StatusLn") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="lstStatusLn" runat="server" size="1" Visible="false" Width="95%">
                                                        </asp:DropDownList>
                                                        <br>
                                                        <asp:RequiredFieldValidator ID="validateStatusln" runat="server" ControlToValidate="lstStatusLn"
                                                            Display="dynamic"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Last App. By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Container.DataItem("ApprovedBy") %>'></asp:Label><asp:Label
                                                            ID="lblAppBy_Level" runat="server" Text='<%# Container.DataItem("ApprovedBy_Level") %>'
                                                            Visible="False"></asp:Label><br />
                                        
                                                        <asp:Label ID="lblUpdDate" runat="server" Text='<%# Container.DataItem("UpdateDate") %>' Width="112px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Next App. By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNextAppBy" runat="server" Text='<%# Container.DataItem("NextApprovedBy") %>' Font-Bold="True" Font-Italic="True"></asp:Label><br />
                                                        <asp:Label ID="lblAppLevel" runat="server" Text='<%# Container.DataItem("AppLevel") %>'
                                                            Visible="False"></asp:Label>
                                                        <asp:Label ID="lblhidApprovedBy" runat="server" Text='<%# Container.DataItem("indApprovedBy") %>'
                                                            Visible="false"></asp:Label><br />
                                                        <asp:Label ID="lblNextApp_Level" runat="server" Text='<%# Container.DataItem("NextApprovedBy_Level") %>' Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                </asp:TemplateColumn>                                
                                                <asp:TemplateColumn>
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
                                                <asp:TemplateColumn HeaderText="Approved">
                                                    <ItemTemplate>
                                                        <asp:Button Text="Approved" OnClick=BtnApproved_Click runat="server" ID="BtnApproved" Font-Size="7pt" Width="56px" Height="26px" ToolTip="click approved PR"/>
                                                        <asp:Button Text="Retrieve" OnClick=BtnCancel_Click runat="server" ID="BtnCancel" Font-Size="7pt" Width="56px" Visible="False" Height="26px" ToolTip="click cancel PR"/>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="mr-h" />
                                        </asp:DataGrid>
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
					            <asp:ImageButton ID="ImgRefresh" OnClick="Btnrefresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />
						        <asp:ImageButton id=Stock UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						        <asp:ImageButton id=DC UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						        <asp:ImageButton id=WS UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" /> 
						        <asp:ImageButton id=FA UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						        <asp:ImageButton id=NU UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />						
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" visible=false/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            <asp:Button ID="ImgRefresh2" class="button-small" runat="server" Text="+ Refresh list"  />&nbsp;
                                <asp:Button ID="Stock2" class="button-small" runat="server" Text="New Stock/WorkShop PR"  />&nbsp;
                                <asp:Button ID="DC2" class="button-small" runat="server" Text="New Direct Charge PR"  />&nbsp;
                                <asp:Button ID="WS2" class="button-small" runat="server" Text="New WorkShop PR"  />&nbsp;
                                <asp:Button ID="FA2" class="button-small" runat="server" Text="New Fixed Asset PR" />&nbsp;
                                <asp:Button ID="NU2" class="button-small" runat="server" Text="New Nursery PR"  />&nbsp;
                                <asp:Button ID="ibPrint2" class="button-small" runat="server" Text="Print"  />                                
                            </td>
                        </tr>
					</table>
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
                            <td style="width: 59px; height: 21px;">
                                <span style="font-size: 8pt; font-family: Arial">KTU&nbsp;</span></td>
                            <td style="width: 15px; height: 21px;">
                                :</td>
                            <td style="width: 100px; height: 21px;">
                                <asp:Label ID="Label1" runat="server" BackColor="Yellow" Text="-" Width="40px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 59px">
                                <span style="font-size: 8pt; font-family: Arial">
                                Manager </span>
                            </td>
                            <td style="width: 15px">
                                :</td>
                            <td style="width: 100px">
                                <asp:Label ID="Label2" runat="server" BackColor="LightGreen" Text="-" Width="40px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 59px; height: 21px;">
                                <span style="font-size: 8pt; font-family: Arial">
                                GM/RC</span></td>
                            <td style="width: 15px; height: 21px;">
                                :</td>
                            <td style="width: 100px; height: 21px;">
                                <asp:Label ID="Label3" runat="server" BackColor="Blue" Text="-" Width="40px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 59px">
                                <span style="font-size: 8pt; font-family: Arial Narrow">
                                VP/CEO</span></td>
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

                        <asp:TextBox ID="txtCost" runat="server" BackColor="Transparent" BorderStyle="None" MaxLength="21" onkeyup="javascript:calTtlCost();" Width="25%"></asp:TextBox>
                        <asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />

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
