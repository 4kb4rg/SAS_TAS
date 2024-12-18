<%@ Page Language="vb" src="../../../include/IN_trx_PurReq_MTR.aspx.vb" Inherits="IN_PurReq_MTR" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>



<html>
	<head>
		<title>Purchase Requisition List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuINTrx id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma"  >
						<tr>
							<td><strong>REQUISITION MONITOR</strong><hr style="width :100%" />   
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
									<td valign="top">Purchase Requisition ID<br /><asp:TextBox id="srchPRID" width=160px maxlength="32" CssClass="fontObject" runat="server"/></td>
									<td valign="top">Item Name<br /><asp:TextBox id="srchItem" width=130px maxlength="32" CssClass="fontObject" runat="server"/></td>
									<td valign="top">From Location<br /><asp:DropDownList id="ddlLocation" width=120px CssClass="fontObject" runat="server"/></td>
									<td valign="top">Purchasing User<br /><asp:DropDownList id="ddlPOUser" width=120px CssClass="fontObject" runat="server"/></td>
									<td valign="top">Period Month<br /><asp:DropDownList id="lstAccMonth" width=90px CssClass="fontObject" runat=server>
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
									<td valign="top">Period Year<br /><asp:DropDownList id="lstAccYear" width=90px CssClass="fontObject" runat=server>
									</asp:DropDownList></td>
									<td valign="top">Req. Status<br /><asp:DropDownList id="srchReqSatus" width=100px runat=server >
											<asp:ListItem Value="0">All</asp:ListItem>
											<asp:ListItem Value="1">Outstanding</asp:ListItem>
											<asp:ListItem Value="2">Ordered</asp:ListItem>
											<asp:ListItem Value="3">Received</asp:ListItem>											
										</asp:DropDownList></td>
                                    <td valign="top">Item Status<br /><asp:DropDownList id="srchStatusLnList" width=100px CssClass="fontObject" runat=server /></td>
                      
                                    <td><asp:DropDownList id="srchApprovedBy" width=100% runat=server Visible="False" >
											<asp:ListItem Value="9">-</asp:ListItem>
											<asp:ListItem Value="0">User/Krani</asp:ListItem>
                                            <asp:ListItem Value="1">Asisten</asp:ListItem>
                                            <asp:ListItem Value="2">KTU</asp:ListItem>
                                            <asp:ListItem Value="3">Askep/Manager</asp:ListItem>
                                            <asp:ListItem Value="4">RC/CorEM</asp:ListItem>
                                            <asp:ListItem Value="5">GM</asp:ListItem>
                                            <asp:ListItem Value="6">DirOps</asp:ListItem>
										</asp:DropDownList></td>
									<td valign="bottom" class="cell-right" style="width:100%"><asp:Button Text="Search" OnClick=srchBtn_Click runat="server" ID="BtnSearch" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
                                        <div id="div2" style="height:400px;width:1100;overflow:auto;">
                                            <asp:DataGrid ID="dgPRListing" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="2" OnItemDataBound="dgPRListing_BindGrid" 
                                            PagerStyle-Visible="False" PageSize="15" Width="100%" class="font9Tahoma">
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
                                                <asp:TemplateColumn HeaderText="Request From">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItem("PRLocCode") %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRID" runat="server" Text='<%# Container.DataItem("PRID") %>' 
                                                            Width="150px"></asp:Label><br />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Req. Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItem("PRDate") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Items">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" 
                                                            Text='<%# Container.DataItem("ItemCode") %>' Width="370"></asp:Label>
                                                        (<%# Container.DataItem("ItemDesc") %>)
                                            <br  />
                                                        <asp:Label ID="lblAddNote" runat="server" 
                                                            Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                                        <asp:TextBox ID="lstAddNote" runat="server" 
                                                            Text='<%# trim(Container.DataItem("AdditionalNote")) %>' Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblPRLnID" runat="server" 
                                                            Text='<%# Container.DataItem("PRLnID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOMCode" runat="server" 
                                                            Text='<%# Container.DataItem("UOMCode") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Req. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyReqDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="App. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyApp" runat="server" 
                                                            Text='<%# Container.DataItem("QtyApp") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyAppDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyApp"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Btnclosed" runat="server" Font-Size="7pt" Height="26px" 
                                                            OnClick="BtnClosed_Click" Text="set closed" 
                                                            ToolTip="click delivery completed item" />
                                            <br  />
                                                        <asp:TextBox ID="txtsetclosed" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Req. Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReqStatus" runat="server" Text='<%# "" %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Item Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItem("PRStatusLn") %>'></asp:Label>
                                                        <asp:Label ID="lblStatusln" runat="server" 
                                                            Text='<%# Container.DataItem("StatusLn") %>' Visible="false" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Approved by">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("ApprovedBy") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("PRUpdateDate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Next Approval">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("NextApprovedBy") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="To">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DispLocTo") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DispLocDate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DispLocBy") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="To">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DispUserID") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DispUserDate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DispUserBy") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("RPHID") %>' 
                                                            visible="true" Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("RPHDate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("RPHUserID") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOID" runat="server" text='<%# Container.DataItem("POID") %>' 
                                                            visible="true" Width="155px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("PODate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyOrder" runat="server" 
                                                            Text='<%# Container.DataItem("POQty") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyOrderDisplay" runat="server" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("POQty"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                        <br></br>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Outstanding">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyOutstandingDisplay" runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Sent to">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("LocPenyerahan") %>' 
                                                            visible="true" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("POUserID") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                        <asp:Label ID="lblUserPO" runat="server" 
                                                            text='<%# Container.DataItem("UserPO") %>' visible="false" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("GRID") %>' 
                                                            visible="true" Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("GRDate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyRcv" runat="server" 
                                                            text='<%# Container.DataItem("GRQty") %>' visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyRcvDisplay" runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("GRQty"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("GRUserID") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DAID") %>' 
                                                            visible="true" Width="175px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DADate") %>' 
                                                            visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DAQty") %>' 
                                                            visible="false"></asp:Label>
                                                        <asp:Label runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("DAQty"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DAUserID") %>' 
                                                            visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" text='<%# Container.DataItem("SRID") %>' 
                                                            visible="true" Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" 
                                                            text='<%# Container.DataItem("SRDate") %>' visible="true" Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" text='<%# Container.DataItem("SRQty") %>' 
                                                            visible="false"></asp:Label>
                                                        <asp:Label ID="Label8" runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("SRQty"),2) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" 
                                                            text='<%# Container.DataItem("SRUserID") %>' visible="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="mr-h" />
                                            <PagerStyle Visible="False" />
                                        </asp:DataGrid>
                                        </div>
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
									<td><img height="18px" src="images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" Visible="False" /></td>
									<td><asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" /></td>
									<td><asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" Visible="False" /></td>
									<td><img height="18px" src="images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
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
						        <%--<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print.gif" AlternateText="Print" onClick="btnPreview_Click" visible="false" runat="server"/>--%>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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
                                        Requisition Status </strong></span></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 21px;">
                                    <span style="font-size: 8pt; font-family: Arial">Outstanding&nbsp;</span></td>
                                <td style="width: 15px; height: 21px;">
                                    :</td>
                                <td style="width: 100px; height: 21px;">
                                    <asp:Label ID="Label1" runat="server" BackColor="Yellow" Text="-" Width="40px"></asp:Label></td>
                            </tr>
							<tr>
                                <td style="width: 100px">
                                    <span style="font-size: 8pt; font-family: Arial">
                                    Full Ordered </span>
                                </td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label2" runat="server" BackColor="Green" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    <span style="font-size: 8pt; font-family: Arial">
                                    Full Received </span>
                                </td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label4" runat="server" BackColor="Blue" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 21px;">
                                    <span style="font-size: 8pt; font-family: Arial">
                                    Cancelled/Closed</span></td>
                                <td style="width: 15px; height: 21px;">
                                    :</td>
                                <td style="width: 100px; height: 21px;">
                                    <asp:Label ID="Label3" runat="server" BackColor="Red" Text="-" Width="40px"></asp:Label></td>
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
