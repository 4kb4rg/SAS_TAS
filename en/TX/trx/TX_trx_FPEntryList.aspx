<%@ Page Language="vb" src="../../../include/TX_trx_FPEntryList.aspx.vb" Inherits="TX_trx_FPEntryList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>VAT List</title>
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
							<td><strong>VAT LISTING</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Document ID : <BR>
			                        <asp:TextBox id=srchDocID width=100% runat="server" />
		                        </td>
		                        <td width="20%" height="26">Supplier : <BR>
			                        <asp:TextBox id=srchSupplier width=100% runat="server" />
		                        </td>
					<td width="20%" height="26">Tax Invoice No. : <BR>
			                        <asp:TextBox id=srchFPNo width=100% maxlength="128" runat="server" />
		                        </td>
		                        <td valign=bottom width=8%>VAT Type :<BR>
		                            <asp:DropDownList id="ddlVATType" width=100% runat=server>
				                        <asp:ListItem value="" Selected>All</asp:ListItem>
				                        <asp:ListItem value="1">VAT IN</asp:ListItem>
				                        <asp:ListItem value="2">VAT OUT</asp:ListItem>										
			                        </asp:DropDownList>
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
	                        Pagesize=10
	                        OnPageIndexChanged=OnPageChanged 
	                        Pagerstyle-Visible=False
	                        OnEditCommand=DEDR_FPDetail
	                        OnUpdateCommand=DEDR_FPUpdate
	                        OnSortCommand=Sort_Grid 
	                        AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
	                       <Columns>
	                            <asp:TemplateColumn HeaderText="Document ID/<br> Date" ItemStyle-Width="15%" >
			                        <ItemTemplate>
			                            <asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" Visible=false runat="server" />
				                        <asp:LinkButton id="lbDocID" CommandName="Edit" Text=<%#Container.DataItem("DocID")%> CausesValidation=False runat="server" /><br />
				                        <asp:Label Text=<%#objGlobal.GetLongDate(Container.DataItem("DocDate"))%> id="lblIDDocDate" runat="server" /><br />
				                        <asp:Label Text=<%#Container.DataItem("DocDate")%> id="lblDocDate" Visible=false runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Supplier Name" ItemStyle-Width="20%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("SupplierName")%> id="lblSupplierName" runat="server" />
				                        <asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Description" ItemStyle-Width="27%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("Description")%> id="lblDescription" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Document Amount/<br>Account Code" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
			                            <asp:LinkButton id="lblIDDocAmount" CommandName="Edit" Text=<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DocAmount"), 2), 2)%> CausesValidation=False runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("DocAmount") %> id="lblDocAmount" visible = False runat="server" />
				                        <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblIDAccCode" False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Tax Invoice No/<br> Date" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right ItemStyle-Width="12%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("FPNo") %> id="lblFPNo" Visible=false runat="server" />  
				                        <asp:LinkButton id="lbFPNo" CommandName="Edit" Text=<%#Container.DataItem("FPNo")%> CausesValidation=False runat="server" /><br />
				                        <asp:Label Text=<%#objGlobal.GetLongDate(Container.DataItem("FPDate"))%> id="lblFPDate" False runat="server" /> 
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Tax Invoice <br>Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
				                        <asp:LinkButton id="lblIDFPAmount" CommandName="Edit"  Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FPAmount"), 2), 2) %>  runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("FPAmount") %> id="lblFPAmount" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        
							    <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
			                        <ItemTemplate>
			                            <asp:LinkButton id=lbUpdate CommandName="Update" Text="set verified" Visible=False runat=server />
			                            <asp:Label id=lblTrxID Text='<%# Trim(Container.DataItem("TrxID")) %>' Visible=False runat=server />
			                            <asp:Label id=lblOriDoc Text='<%# Trim(Container.DataItem("OriDoc")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccCode Text='<%# Trim(Container.DataItem("AccCode")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccMonth Text='<%# Trim(Container.DataItem("AccMonth")) %>' Visible=False runat=server />
				                        <asp:Label id=lblAccYear Text='<%# Trim(Container.DataItem("AccYear")) %>' Visible=False runat=server />
				                        <asp:Label id=lblDocLnID Text='<%# Trim(Container.DataItem("DocLnID")) %>' Visible=False runat=server />
				                        <asp:Label id=lblVATType Text='<%# Trim(Container.DataItem("VATType")) %>' Visible=False runat=server />
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
					            <asp:ImageButton id="GenerateBtn" ToolTip="Generate tax number" UseSubmitBehavior="false" OnClick="GenerateTaxNumber_Click"  runat="server" ImageUrl="../../images/butt_generate.gif"/>
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

			<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />		
			<Input Type=Hidden id=intRec value=0 runat=server />
			<Input Type=Hidden id=hidTaxStatus value=0 runat=server />
		</FORM>
	</body>
</html>
