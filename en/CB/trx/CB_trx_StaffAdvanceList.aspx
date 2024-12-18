<%@ Page Language="vb" trace=false src="../../../include/CB_trx_StaffAdvanceList.aspx.vb" Inherits="CB_trx_StaffAdvanceList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>	
		<title>Staff Advance & Realization</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id="SortExpression" visible="false" runat="server" />
			<asp:Label id="SortCol" visible="false" runat="server" />
			<Input type=hidden id=hidInit value="" runat=server />
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
					 
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>STAFF ADVANCE & REALIZATION</strong><hr style="width :100%" />   
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
								<td height="26" style="width: 20%">
                                    Staff :<BR><asp:TextBox id=txtStaff width="100%" runat="server" /></td>
                                
								<td height="26" style="width: 20%">
                                    Doc ID :<BR><asp:TextBox id=txtDocID width="100%" runat="server" /></td>
                                
								<td width="20%">
                                    Doc Status :<asp:DropDownList ID="ddlDocStatus" runat="server" Width="100%">
                                        <asp:ListItem value="0" Selected>-All-</asp:ListItem>
										<asp:ListItem value="1">Open</asp:ListItem>
										<asp:ListItem value="2">Closed</asp:ListItem>
                                    </asp:DropDownList></td>	
								<td valign=bottom width=10%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% runat=server>
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
								<td valign=bottom width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList>
								<td width="20%" height="26">
                                    <br />
                                    <asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								<td width="15%"></td>
								<td width="15%"></td>
								<td width="10%"valign=bottom align=right></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
								
                                    <tr>
                                    <td>
									<div id="div1" style="height:370px;width:1140;overflow:auto;">
						            <asp:DataGrid id="dgList"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines="Both" 
							Cellpadding="2" 
							Allowcustompaging="False" 
							OnEditCommand=DEDR_Edit
							Pagerstyle-Visible="False" 
							OnSortCommand="Sort_Grid"  
							OnItemDataBound="dgList_BindGrid"
							AllowSorting="false"
                                        			class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							<Columns>
							    <asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=center>
								    <ItemTemplate>
									    <asp:LinkButton id=lbView CommandName="Edit" Text="preview" CausesValidation=False runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Staff" SortExpression="Name" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
									<ItemTemplate>
									    <asp:Label Width="200px" ID="lblName" runat="server" Text='<%#Container.DataItem("Name")%>'></asp:Label>										
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Doc ID" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
									<ItemTemplate>
									    <asp:Label Width="200px" ID="lblDocID" runat="server" Text='<%#Container.DataItem("StaffAdvDoc")%>'></asp:Label>	
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Date" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>								
								    <ItemTemplate>									    
										<asp:Label Width="70px" ID="lblDate" runat="server" Text='<%#objGlobal.GetLongDate(Container.DataItem("StaffAdvDate"))%>'></asp:Label>									
									</ItemTemplate>		
								</asp:TemplateColumn>																																			
								<asp:TemplateColumn HeaderText="Advance" ItemStyle-HorizontalAlign=Right HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
									<ItemTemplate>										
										<asp:Label ID="Label1"  Width="100px" runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 0)%>'></asp:Label>	
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Realization" ItemStyle-HorizontalAlign=Right HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
									<ItemTemplate>										
										<asp:Label ID="Label2"  Width="100px" runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("RealAmount"), 2), 0)%>'></asp:Label>	
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-HorizontalAlign=Right HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
									<ItemTemplate>										
										<asp:Label ID="Label3"  Width="100px" runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Outstanding"), 2), 0)%>'></asp:Label>	
									</ItemTemplate>
								</asp:TemplateColumn>
								
							</Columns>
						</asp:DataGrid>
						</div>
						<BR>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
                        				<tr>
					<td class="mt-h"><asp:Label id=lblRealization text="Advance Realization" Font-Bold=true Visible=false runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan=6>		
						<div id="div2" style="height:370px;width:1140;overflow:auto;">
						<asp:DataGrid id="dgTrx"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines="Both" 
							Cellpadding="2" 
							Allowcustompaging="False" 
							Pagerstyle-Visible="False" 
							OnSortCommand="Sort_Grid"  
							OnItemDataBound="dgTrx_BindGrid"
							AllowSorting="false"
                            class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							<Columns>
							    <asp:TemplateColumn HeaderText="Doc ID" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
								    <ItemTemplate>
									    <asp:Label ID="lblDocID" Width="120px" runat="server" Text='<%#Container.DataItem("DocID")%>'></asp:Label>										
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Date" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>
									<ItemTemplate>
									    <asp:Label ID="lblDate" Width="70px" runat="server" Text='<%#objGlobal.GetLongDate(Container.DataItem("DocDate"))%>'></asp:Label>										
									</ItemTemplate>
								</asp:TemplateColumn>	
							    <asp:TemplateColumn HeaderText="Description" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>								
								    <ItemTemplate>									    
										<asp:Label ID="lblDescr" Width="200px" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:Label>									
									</ItemTemplate>		
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="COA" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>								
								    <ItemTemplate>									    
										<asp:Label ID="lblCOA" Width="70px" runat="server" Text='<%#Container.DataItem("AccCode")%>'></asp:Label>									
									</ItemTemplate>		
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=center>								
								    <ItemTemplate>									    
										<asp:Label ID="lblCOADescr" Width="150px" runat="server" Text='<%#Container.DataItem("COADescr")%>'></asp:Label>									
									</ItemTemplate>		
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Amount" HeaderStyle-Font-Bold=true ItemStyle-HorizontalAlign=Right HeaderStyle-HorizontalAlign=Center  >
									<ItemTemplate>										
										<asp:Label ID="lblAmount" Width="100px" runat="server" Text='<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 0)%>'></asp:Label>	
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
						</div>
						<BR>
					</td>
				</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
		 
						<tr>
							<td>
					             &nbsp;<asp:ImageButton id="ibPrint" imageurl="../../images/butt_print.gif" AlternateText="Print" runat="server" OnClick="btnPreview_Click"/>
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

		</form>
	</body>
</html>
