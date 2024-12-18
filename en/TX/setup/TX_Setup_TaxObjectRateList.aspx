<%@ Page Language="vb" src="../../../include/TX_Setup_TaxObjectRateList.aspx.vb" Inherits="TX_Setup_TaxObjectRateList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Tax Object Rate List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu"> 
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />


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
							<td><strong>TAX OBJECT RATE LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Tax Object Group :<BR>
									<asp:DropDownList id="ddlTaxObjectGrp" width=100% runat=server>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26">Description : <BR>
									<asp:TextBox id=srchDescription width=100% maxlength="128" runat="server" />
								</td>
								<td width="25%" height="26">Tax Object : <BR>
									<asp:TextBox id=srchTaxObjectCode width=100% runat="server" />
								</td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="srchStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" align=left>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
							            OnDeleteCommand=DEDR_Delete 
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
    							            <asp:HyperLinkColumn ItemStyle-Width="10%" HeaderText="COA" 
    							                SortExpression="TOB.AccCode" 
									            DataNavigateUrlField="TaxID" 
									            DataNavigateUrlFormatString="TX_Setup_TaxObjectRateDet.aspx?taxid={0}" 
									            DataTextField="AccCode"  />
									
								            <asp:HyperLinkColumn ItemStyle-Width="15%" HeaderText="Description" 
									            SortExpression="TOB.Description" 
									            DataNavigateUrlField="TaxID" 
									            DataNavigateUrlFormatString="TX_Setup_TaxObjectRateDet.aspx?taxid={0}"
									            DataTextField="Description" />
									
								            <asp:HyperLinkColumn ItemStyle-Width="35%" HeaderText="Tax Object" 
									            SortExpression="_Description" 
									            DataNavigateUrlField="TaxID" 
									            DataNavigateUrlFormatString="TX_Setup_TaxObjectRateDet.aspx?taxid={0}" 
									            DataTextField="_Description"  />
									
								            <asp:TemplateColumn HeaderText=" Rate (%)<br>w/ NPWP" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								                <ItemTemplate> 
									                <ItemStyle />
										                <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WRate"), 2), 2) %> id="lblIDWRate" runat="server" />
										                <asp:Label Text=<%# FormatNumber(Container.DataItem("WRate"), 2) %> id="lblWRate" visible = False runat="server" />
									                </ItemStyle>
								                </ItemTemplate>
							                </asp:TemplateColumn>		
							                <asp:TemplateColumn HeaderText=" Rate (%)<br>w/o NPWP" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								                <ItemTemplate> 
									                <ItemStyle />
										                <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WORate"), 2), 2) %> id="lblIDWORate" runat="server" />
										                <asp:Label Text=<%# FormatNumber(Container.DataItem("WORate"), 2) %> id="lblWORate" visible = False runat="server" />
									                </ItemStyle>
								                </ItemTemplate>
							                </asp:TemplateColumn>		
							    							
								            <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="8%" SortExpression="TOB.UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="8%" SortExpression="TOB.Status">
									            <ItemTemplate>
										            <%#objCTSetup.mtdGetProductBrandStatus(Container.DataItem("Status"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
									
								            <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" SortExpression="Usr.UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=center>
									            <ItemTemplate>
									                <asp:label text= '<%# Container.DataItem("TaxID") %>' Visible=False id="lblTaxID" runat="server" />
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										            <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>	
							            </Columns>
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
					            <asp:ImageButton id=NewBtn OnClick="NewBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Tax Object" runat="server"/>
						        <asp:imagebutton id="btnSyncData" onclick="btnSyncData_Click" runat="server" imageurl="../../images/butt_synchronize_data.gif" AlternateText="Synchronize Data" />
						        <asp:Label id=SortCol Visible=False Runat="server" />
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


		</FORM>
	</body>
</html>
