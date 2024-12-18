<%@ Page Language="vb" src="../../../include/PU_trx_PO_MTR.aspx.vb" Inherits="PU_trx_PO_MTR" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%><html>
	<head>
		<title>Purchasing Monitor</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
		<table cellpadding="0" cellspacing="0" style="width: 100%">
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
							<td><strong>PURCHASING MONITOR</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
						<tr>
							<td style="background-color:#FFCC00">
							
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
									<td valign="bottom">PO ID<br /><asp:TextBox id=srchPOID width=80px maxlength="32" runat="server" /></td>
									<td valign="bottom">Item Name<br /><asp:TextBox id="srchItem" width=100px maxlength="32" runat="server"/></td>
									<td valign="bottom">Supplier<br /><asp:TextBox id=srchSupplier width=80px maxlength="20" runat="server"/></td>
                                    <td valign="bottom">Purchasing User<br /><asp:DropDownList id="ddlPOUser" width=100px runat="server"/></td>
                                    <td valign="top">Request From<br /><asp:DropDownList id="ddlLocation" width=100px runat=server/><br />Delivery To<br /><asp:DropDownList id="ddlDelLocation" width=100% runat=server/></td>
									<td valign="bottom">Period<br /><asp:DropDownList id="lstAccMonth" width=100px runat=server>
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
										<asp:ListItem value="12">12</asp:ListItem></asp:DropDownList></td>
									<td valign="bottom"><br /><asp:DropDownList id="lstAccYear" width=80px runat=server></asp:DropDownList></td>
									<td valign="top">PO Status<br /><asp:DropDownList id="srchReqSatus" width=120px runat=server >
										<asp:ListItem Value="0">All</asp:ListItem>
										<asp:ListItem Value="1">Outstanding</asp:ListItem>
										<asp:ListItem Value="2">Fully Received</asp:ListItem>									
									</asp:DropDownList><br />Item Status<br /><asp:DropDownList id="srchStatusLnList" width=120px runat=server /></td>
                                    <td valign=bottom width=0%><asp:TextBox id=srchUpdateBy width=100% maxlength="128" Visible=false runat="server"/></td>
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
                                        <asp:DataGrid ID="dgPRListing" runat="server" AutoGenerateColumns="False"  class="font9Tahoma"
                                            CellPadding="2" GridLines="Both" OnItemDataBound="dgPRListing_BindGrid" 
                                            PagerStyle-Visible="False" Width="100%">
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
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOID" runat="server" text='<%# Container.DataItem("POID") %>' 
                                                            visible="true" Width="170px"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="lblPOLNID" runat="server" 
                                                            Text='<%# Container.DataItem("POLNID") %>' visible="false"></asp:Label>
                                                        <asp:Label ID="lblPOLNStatus" runat="server" 
                                                            Text='<%# Container.DataItem("POLNStatus") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" 
                                                            text='<%# Container.DataItem("PODate") %>' visible="true" Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Items">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" 
                                                            Text='<%# Container.DataItem("ItemCode") %>' Width="370"></asp:Label>
                                                        (<%# Container.DataItem("ItemDesc") %>)
                                            <br  />
                                                        <asp:Label ID="lblAddNote" runat="server" 
                                                            Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                                        <asp:TextBox ID="lstAddNote" runat="server" 
                                                            Text='<%# trim(Container.DataItem("AdditionalNote")) %>' Visible="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOMCode" runat="server" 
                                                            Text='<%# Container.DataItem("UOMCode") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty Order">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyOrder" runat="server" 
                                                            Text='<%# Container.DataItem("POQty") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyOrderDisplay" runat="server" Font-Bold="true" 
                                                            Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("POQty"),2) %>'></asp:Label>
                                                        <br></br>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Outstanding">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQtyOutstandingDisplay" runat="server" Font-Bold="true" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),2) %>'></asp:Label>
                                                        <asp:Label ID="lblQtyOutstanding" runat="server" 
                                                            Text='<%# Container.DataItem("QtyOutstanding") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="PR">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItem("PRID") %>' 
                                                            Width="170px"></asp:Label>
                                                        <br  />
                                                        <asp:Label ID="Label5" runat="server" 
                                                            Text='<%# Container.DataItem("PRLocCode") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:Button ID="BtnCompleted" runat="server" Font-Size="7pt" Height="26px" 
                                                            OnClick="BtnCompleted_Click" Text="set completed" 
                                                            ToolTip="click delivery completed item" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Supplier">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label88" runat="server" text='<%# Container.DataItem("Name") %>' 
                                                            visible="true" Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Sent to">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" 
                                                            text='<%# Container.DataItem("LocPenyerahan") %>' visible="true" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" 
                                                            text='<%# Container.DataItem("POUserID") %>' visible="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRID" runat="server" text='<%# Container.DataItem("GRID") %>' 
                                                            visible="true" Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRDate" runat="server" 
                                                            text='<%# Container.DataItem("GRDate") %>' visible="true" Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRQty" runat="server" 
                                                            text='<%# Container.DataItem("GRQty") %>' visible="false"></asp:Label>
                                                        <asp:Label ID="lblQtyRcvDisplay" runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("GRQty"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("GRUserID") %>' 
                                                            visible="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDAID" runat="server" text='<%# Container.DataItem("DAID") %>' 
                                                            visible="true" Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDADate" runat="server" 
                                                            text='<%# Container.DataItem("DADate") %>' visible="true" Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDAQty" runat="server" 
                                                            text='<%# Container.DataItem("DAQty") %>' visible="false"></asp:Label>
                                                        <asp:Label runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("DAQty"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("DAUserID") %>' 
                                                            visible="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRIDPW" runat="server" 
                                                            text='<%# Container.DataItem("SRPWID") %>' visible="true" Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRDatePW" runat="server" 
                                                            text='<%# Container.DataItem("SRPWDate") %>' visible="true" Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRQtyPW" runat="server" 
                                                            text='<%# Container.DataItem("SRPWQty") %>' visible="false"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("SRPWQty"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" 
                                                            text='<%# Container.DataItem("SRPWUserID") %>' visible="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTRID" runat="server" 
                                                            text='<%# Container.DataItem("STID") %>' visible="true" Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTRDate" runat="server" 
                                                            text='<%# Container.DataItem("STDate") %>' visible="true" Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTRQty" runat="server" 
                                                            text='<%# Container.DataItem("STQTY") %>' visible="false"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("STQTY"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" 
                                                            text='<%# Container.DataItem("SRUserID") %>' visible="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRID" runat="server" text='<%# Container.DataItem("SRID") %>' 
                                                            visible="true" Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRDate" runat="server" 
                                                            text='<%# Container.DataItem("SRDate") %>' visible="true" Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRQty" runat="server" 
                                                            text='<%# Container.DataItem("SRQty") %>' visible="false"></asp:Label>
                                                        <asp:Label runat="server" 
                                                            text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("SRQty"),2) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderText="By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" text='<%# Container.DataItem("SRUserID") %>' 
                                                            visible="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
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
        <asp:Label ID="lblSearch" runat="server" Font-Bold="True" ForeColor="Yellow" Visible="False"></asp:Label>
							</td>
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
