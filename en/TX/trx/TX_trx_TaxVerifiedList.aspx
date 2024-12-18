<%@ Page Language="vb" src="../../../include/TX_trx_TaxVerifiedList.aspx.vb" Inherits="TX_trx_TaxVerifiedList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>Tax Verified List</title>
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
							<td><strong>TAX VERIFIED LIST</strong><hr style="width :100%" />   
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
		                        <td width="20%" height="26">Document ID : <BR>
			                        <asp:TextBox id=srchDocID width=100% maxlength="128" runat="server" />
		                        </td>
		                        <td width="15%" height="26">Supplier : <BR>
			                        <asp:TextBox id=srchSupplier width=100% runat="server" />
		                        </td>
		                        <td width="15%" height="26">KPP : <BR>
			                        <asp:DropDownList id="ddlKPP" width=100% runat=server>
			                        </asp:DropDownList>
		                        </td>
		                        <td valign=bottom width=8%>Period :<BR>
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
			                    </td>
		                        <td valign=bottom width=10%><BR>
		                            <asp:DropDownList id="lstAccYear" width=100% runat=server>
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
	                        OnDeleteCommand=DEDR_Preview 
	                        OnEditCommand=DEDR_Edit
	                        OnUpdateCommand=DEDR_Update
	                        OnCancelCommand=DEDR_Cancel
	                        OnItemCommand=onCommand
	                        OnSortCommand=Sort_Grid 
	                        AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
	                       <Columns>
	                            <asp:TemplateColumn HeaderText="Tax Object Group" ItemStyle-Width="10%">
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("TXDescr") %> id="lblTXDescr" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                         <asp:TemplateColumn HeaderText="Date" ItemStyle-Width="8%" >
			                        <ItemTemplate>
				                         <%# objGlobal.GetLongDate(Container.DataItem("DocDate")) %> 
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Document ID" ItemStyle-Width="15%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" Visible=false runat="server" />
				                        <asp:LinkButton id="DocID" CommandName="Delete" Text=<%#Container.DataItem("DocID")%> CausesValidation=False runat="server" /><br />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Supplier Name" ItemStyle-Width="20%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("FromTo")%> id="lblSupplierName" runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="DPP Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
				                        <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPPAmount"), 2), 2) %> id="lblIDDPPAmount" runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("DPPAmount") %> id="lblDPPAmount" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                       	<asp:TemplateColumn HeaderText="Rate" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right  ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
			                              <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rate"), 2), 2) & "%"%> 
			                              <asp:Label Text=<%# FormatNumber(Container.DataItem("Rate"), 2) %> id="lblRate" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>	
	                            <asp:TemplateColumn HeaderText="Tax Amount" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
		                            <ItemTemplate> 
			                            <ItemStyle />
			                                <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TaxAmount"), 2), 2) %> id="lblIDTaxAmount" runat="server" />
			                                <asp:Label Text=<%# Container.DataItem("TaxAmount") %> id="lblTaxAmount" visible = False runat="server" />
			                            </ItemStyle>
		                            </ItemTemplate>
	                            </asp:TemplateColumn>	
	                            <asp:TemplateColumn HeaderText="KPP" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center >
			                        <ItemTemplate>
			                            <asp:Label Text=<%# Container.DataItem("KPPInit") %> id="lblKPPInit" runat="server" /><br />
				                    </ItemTemplate>
		                        </asp:TemplateColumn>			
							  	<asp:TemplateColumn HeaderText="Printed ID" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center >
			                        <ItemTemplate>
			                            <asp:DropDownList id="lstKPPInit" visible=false size=1 width="95%" runat="server"/>
				                        <asp:Label Text=<%# Container.DataItem("TrxID") %> id="lblTrxID" visible = False runat="server" />
										<asp:TextBox Text=<%# Container.DataItem("TrxID") %> id="txtTrxID" runat="server" />
				                    </ItemTemplate>
		                        </asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="" ItemStyle-Width="30%" >
			                        <ItemTemplate>
			                            <asp:Button Text="Set" OnClick=SetNoBukti_Click runat="server" ID="btnSetBukti" Font-Size="8pt" Width="100%" Height="26px" ToolTip="Set Bo.Bukti" visible=True/>
										<asp:Button Text="Confirm" OnClick=ConfNoBukti_Click runat="server" ID="btnConfBukti" Font-Size="8pt" Width="100%" Height="26px" ToolTip="Confirm Bo.Bukti" visible=False/>
				                    </ItemTemplate>
		                        </asp:TemplateColumn>								
		                        				
		                        <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
			                        <ItemTemplate>
			                            <asp:LinkButton id=lbPrint CommandArgument="Print" Text="Print" runat=server />
			                            <asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" Visible=false runat=server />
			                            <asp:LinkButton id=lbUpdate CommandName="Update" Text="Update" Visible=false runat=server />
			                            <asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" Visible=false runat=server />
				                        <asp:Label id=lblTaxStatus Text='<%# Trim(Container.DataItem("TaxStatus")) %>' Visible=False runat=server />
				                        <asp:Label id=lblOriDoc Text='<%# Trim(Container.DataItem("OriDoc")) %>' Visible=False runat=server />
				                        <asp:Label id=lblDocType Text='<%# Trim(Container.DataItem("DocType")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccCode Text='<%# Trim(Container.DataItem("AccCode")) %>' Visible=False runat=server />
				                        <asp:Label id=lblTaxID Text='<%# Trim(Container.DataItem("TaxID")) %>' Visible=False runat=server />
				                        <asp:Label id=lblTaxInit Text='<%# Trim(Container.DataItem("TaxInit")) %>' Visible=False runat=server />
				                        <asp:Label id=lblDocDate Text='<%# objGlobal.GetLongDate(Container.DataItem("DocDate")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccMonth Text='<%# Trim(Container.DataItem("AccMonth")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccYear Text='<%# Trim(Container.DataItem("AccYear")) %>' Visible=False runat=server />
				                        <asp:Label id=lblDocLnID Text='<%# Trim(Container.DataItem("DocLnID")) %>' Visible=False runat=server />
				                        <asp:Label id=lblTaxLnID Text='<%# Trim(Container.DataItem("TaxLnID")) %>' Visible=False runat=server />
										<asp:Label id=lblLocCode Text='<%# Trim(Container.DataItem("LocCode")) %>' Visible=False runat=server />
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
							<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />	
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
					        <asp:ImageButton id="Generate" ToolTip="Generate autonumber" UseSubmitBehavior="false" OnClick="GenerateNumber_Click"  runat="server" ImageUrl="../../images/butt_generate.gif"/>
					    <asp:ImageButton id="PrintDoc" ToolTip="Print document" UseSubmitBehavior="false" OnClick="PrintDoc_Click" runat="server" ImageUrl="../../images/butt_print_doc.gif"/>
					    <asp:ImageButton ID="PostingBtn"  UseSubmitBehavior="false" AlternateText="Posting" onclick="PostingBtn_Click"  ImageUrl="../../images/butt_post.gif"  CausesValidation=False Runat=server />
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


				
			<Input Type=Hidden id=intRec value=0 runat=server />
			<Input Type=Hidden id=hidTaxStatus value=0 runat=server />
		</FORM>
	</body>
</html>
