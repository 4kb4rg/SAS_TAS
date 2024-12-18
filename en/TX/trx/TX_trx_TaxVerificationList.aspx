<%@ Page Language="vb" src="../../../include/TX_trx_TaxVerificationList.aspx.vb" Inherits="TX_trx_TaxVerificationList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>Tax Verification List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />
			
            		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuGLSetup id=MenuGLSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>TAX VERIFICATION LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Tax Object Group :<BR>
			                        <asp:DropDownList id="ddlTaxObjectGrp" width=100% runat=server>
			                        </asp:DropDownList>
		                        </td>
		                        <td width="15%" height="26">Document ID : <BR>
			                        <asp:TextBox id=srchDocID width=100% maxlength="128" runat="server" />
		                        </td>
		                        <td width="15%" height="26">Supplier : <BR>
			                        <asp:TextBox id=srchSupplier width=100% runat="server" />
		                        </td>
		                        <td width="15%" height="26">Tax Object : <BR>
			                        <asp:TextBox id=srchTaxObjectCode width=100% runat="server" />
		                        </td>
		                        <td valign=bottom width=8%>Period :<BR>
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
			                        </asp:DropDownList>
			                    </td>
		                        <td valign=bottom width=10%><BR>
		                            <asp:DropDownList id="lstAccYear" width=100% runat=server>
			                        </asp:DropDownList>
			                    </td>
			                    <td valign=bottom width=12%>Tax Status :<BR>
		                            <asp:DropDownList id="ddlTaxStatus" width=100% runat=server>
				                        <asp:ListItem value="1" Selected>Unverified</asp:ListItem>
				                        <asp:ListItem value="2">Verified</asp:ListItem>
			                        </asp:DropDownList>
			                    </td>
		                        <td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgLine runat=server 
	                        AutoGenerateColumns=false width=100% 
	                        GridLines=none 
	                        Cellpadding=2 
	                        AllowPaging=True 
	                        Allowcustompaging=False 
	                        Pagesize=15 
	                        OnPageIndexChanged=OnPageChanged 
	                        Pagerstyle-Visible=False 
	                        OnUpdateCommand=DEDR_Update 
	                        OnCancelCommand=DEDR_Cancel
	                        OnDeleteCommand=DEDR_Preview
	                        OnSortCommand=Sort_Grid 
	                        AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
	                        <Columns>
	                            <asp:TemplateColumn HeaderText="Tax Group" ItemStyle-Width="7%" SortExpression="TXDescr">
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("TXDescr") %> id="lblTXDescr" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Document ID" ItemStyle-Width="15%" SortExpression="DocID">
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" Visible=false runat="server" />
				                        <asp:LinkButton id="DocID" CommandName="Delete" Text=<%#Container.DataItem("DocID")%> CausesValidation=False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                         <asp:TemplateColumn HeaderText="Supplier Name" ItemStyle-Width="15%" SortExpression="FromTo">
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("FromTo")%> id="lblSupplierName" runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Tax Object" SortExpression="TaxObject" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("TaxObject") %> id="lblTaxObject" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="DPP Amount" SortExpression="DPP" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
				                        <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPPAmount"), 2), 2) %> id="lblIDDPP" runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("DPPAmount") %> id="lblDPPAmount" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                       	<asp:TemplateColumn HeaderText="Rate" SortExpression="Rate" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
				                          <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rate"), 2), 2) & "%"%> 
				                        <asp:Label Text=<%# FormatNumber(Container.DataItem("Rate"), 2) %> id="lblRate" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>	
	                            <asp:TemplateColumn HeaderText="Tax Amount" SortExpression="TaxAmount"  ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
		                            <ItemTemplate> 
			                            <ItemStyle />
				                            <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TaxAmount"), 2), 2) %> id="lblIDTaxAmount" runat="server" />
				                            <asp:Label Text=<%# Container.DataItem("TaxAmount") %> id="lblTaxAmount" visible = False runat="server" />
			                            </ItemStyle>
		                            </ItemTemplate>
	                            </asp:TemplateColumn>		
							  	<asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="7%" SortExpression="Usr.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tax Status" ItemStyle-Width="7%" SortExpression="Usr.UserName">
									<ItemTemplate>
										<%#Container.DataItem("TaxStatusDescr")%>
									</ItemTemplate>
								</asp:TemplateColumn>
		                        <asp:TemplateColumn >
			                        <ItemTemplate>
			                            <asp:LinkButton id=lbVerified CommandName=Update Text="set verified" Visible=false runat=server />
				                        <asp:LinkButton id=lbUnverified CommandName=Cancel Text="cancel" Visible=false runat=server />
				                        <asp:Label id=lblTaxStatus Text='<%# Trim(Container.DataItem("TaxStatus")) %>' Visible=False runat=server />
				                        <asp:Label id=lblOriDoc Text='<%# Trim(Container.DataItem("OriDoc")) %>' Visible=False runat=server />
				                        <asp:Label id=lblDocType Text='<%# Trim(Container.DataItem("DocType")) %>' Visible=False runat=server />
				                        <asp:Label id=lblTaxID Text='<%# Trim(Container.DataItem("TaxID")) %>' Visible=False runat=server />
				                        <asp:Label id=lblTaxInit Text='<%# Trim(Container.DataItem("TaxInit")) %>' Visible=False runat=server />
				                        <asp:Label id=lblDocDate Text='<%# objGlobal.GetLongDate(Container.DataItem("DocDate")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccMonth Text='<%# Trim(Container.DataItem("AccMonth")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccYear Text='<%# Trim(Container.DataItem("AccYear")) %>' Visible=False runat=server />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>	
	                        </Columns>
                        </asp:DataGrid><br />
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
						<asp:ImageButton ID="VerifiedBtn"  UseSubmitBehavior="false" AlternateText="Set Verified" onclick="VerifiedBtn_Click"  ImageUrl="../../images/butt_verified.gif"  CausesValidation=False Runat=server />
						<asp:ImageButton id="PrintDoc" ToolTip="Print document" UseSubmitBehavior="false" OnClick="PrintDoc_Click" runat="server" ImageUrl="../../images/butt_print_doc.gif"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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


			<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />		
		</FORM>
	</body>
</html>
