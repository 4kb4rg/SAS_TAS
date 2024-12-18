<%@ Page Language="vb" src="../../../include/PU_Trx_RFQ_List.aspx.vb" Inherits="PU_Trx_RFQ_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_pusetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Supplier List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmSuppList runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />



		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=MenuPU runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>RFQ LIST</strong><hr style="width :100%" />   
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
								    <td width="15%" height="26">
                                        RFQ ID &nbsp;:<BR><asp:TextBox id=txtRfqID width=100% maxlength="20" runat="server" /></td>
								    <td height="26" style="width: 35%">
                                        PR ID &nbsp;:<BR><asp:TextBox id=txtPRID width=100% maxlength="20" runat="server" /></td>
								    <td width="15%" height="26">
                                        User PO :<br />
                                        <asp:TextBox id=TxtUserPo width=100% maxlength="128" runat="server"/></td>
                                    <td height="26" width="10%">
                                        Periode :<br />
                                        <asp:DropDownList ID="lstAccMonth" runat="server" Width="100%">
                                            <asp:ListItem Selected="" Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td height="26" width="10%">
                                        <br />
                                        <asp:DropDownList ID="lstAccYear" runat="server" Width="100%">
                                        </asp:DropDownList></td>
								    <td width="10%" height="26">
                                        Status :&nbsp;<br />
									    <asp:DropDownList id="ddlStatus" width=100% runat=server>
										    <asp:ListItem value="0" >All</asp:ListItem>
										    <asp:ListItem value="1" Selected>Active</asp:ListItem>
										    <asp:ListItem value="2">Confirm</asp:ListItem>
										    <asp:ListItem value="3">Deleted</asp:ListItem>
										    <asp:ListItem value="4">Cancel</asp:ListItem>
									    </asp:DropDownList></td>
								    <td width="15%" height="26"></td>
								    <td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
                                </tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgSuppList
							                AutoGenerateColumns=False width=100% runat=server
							                GridLines=None 
							                Cellpadding=2 
							                AllowPaging=True 
							                Pagesize=15 
							                OnPageIndexChanged=OnPageChanged 
							                Pagerstyle-Visible=False 
							                OnDeleteCommand=DEDR_Delete 
							                OnSortCommand=Sort_Grid  
							                AllowSorting=True
                                                                        class="font9Tahoma">
								
							                                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							                <Columns>
								                <asp:BoundColumn Visible=False HeaderText="RFQ ID" DataField="RfqID" />

								                <asp:HyperLinkColumn HeaderText="RFQID" 
								
									                SortExpression="RFQID" 
									                DataNavigateUrlField="RFQID" 
									                DataNavigateUrlFormatString="PU_Trx_RFQ_Detail.aspx?RFQID={0}" 
									                DataTextField="RFQID" />

								                <asp:TemplateColumn HeaderText="PR ID" SortExpression="PRID" >
									                <ItemTemplate>
										                <%#Container.DataItem("PRID")%>
									                </ItemTemplate>
									                <ItemStyle Width="15%" />
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="PR Location" SortExpression="LocDescription">
									                <ItemTemplate>
										                <%#Container.DataItem("LocDescription")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
																							    
							                    <asp:TemplateColumn HeaderText="Transaction Date" SortExpression="RfqDate">
									                <ItemTemplate>
										                <%#objGlobal.GetLongDate(Container.DataItem("RfqDate"))%>
									                </ItemTemplate>
									                <ItemStyle Width="8%" />
								                </asp:TemplateColumn>
																	
							                    <asp:TemplateColumn HeaderText="Deadline" SortExpression="rfqDeadline">
									                <ItemTemplate>
									                <%#objGlobal.GetLongDate(Container.DataItem("rfqDeadline"))%>										
									                </ItemTemplate>
									                <ItemStyle Width="8%" />
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="Remark" SortExpression="Remark">
									                <ItemTemplate>
										                <%#Container.DataItem("Remark")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Create Date">
	                                                <ItemTemplate>
										                <%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%>
									                </ItemTemplate>                                
                                                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="Last Update">
									                <ItemTemplate>
										                <%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="User PO">
                                	                <ItemTemplate>
										                <%#Container.DataItem("UserName")%>
									                </ItemTemplate>
                                                </asp:TemplateColumn>								
								                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
									                <ItemTemplate>
										                <%#Container.DataItem("UpdateID")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="StatusDesc" SortExpression="StatusDesc">
									                <ItemTemplate>
										                <%#Container.DataItem("StatusDesc")%>
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
					            <asp:ImageButton id=NewSuppBtn onClick=NewSuppBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Supplier" runat=server/>&nbsp;
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



		</FORM>
	</body>
</html>
