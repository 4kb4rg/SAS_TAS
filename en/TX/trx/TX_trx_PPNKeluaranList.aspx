<%@ Page Language="vb" src="../../../include/TX_trx_PPNKeluaranList.aspx.vb" Inherits="TX_trx_PPNKeluaranList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>VAT OUT List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
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
							<td><strong>VAT OUT LIST</strong><hr style="width :100%; margin-right: 0px;" />   
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
								 <td width="20%" height="26">Tax Invoice No. : <BR>
			                        <asp:TextBox id=srchFPNo width=100% maxlength="128" runat="server" />
		                        </td>
		                        <td width="20%" height="26">Document ID : <BR>
			                        <asp:TextBox id=srchDocID width=100% runat="server" />
		                        </td>
		                        <td width="20%" height="26">Supplier : <BR>
			                        <asp:TextBox id=srchSupplier width=100% runat="server" />
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
	                        OnEditCommand=DEDR_FPDetail
	                        OnUpdateCommand=DEDR_FPDetail
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
				                        <asp:Label Text=<%#objGlobal.GetLongDate(Container.DataItem("DocDate"))%> id="lblIDDocDate" runat="server" />
				                         <asp:Label Text=<%#Container.DataItem("DocDate")%> id="lblDocDate" Visible=false runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		            
		                        <asp:TemplateColumn HeaderText="Supplier Name" ItemStyle-Width="15%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("SupplierName")%> id="lblSupplierName" runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" visible = False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Description" ItemStyle-Width="27%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("Description")%> id="lblDescription" runat="server" /><br />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                         <asp:TemplateColumn HeaderText="Document Amount/<br>Account Code" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
			                        <ItemTemplate>
			                            <asp:Label Text=<%# Container.DataItem("FPDPPAmount") %> id="lblDocAmount" visible = False runat="server" />
			                            <asp:LinkButton id="lblIDDocAmount" CommandName="Edit" Text=<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FPDPPAmount"), 2), 2)%> CausesValidation=False runat="server" /><br />
				                        <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblIDAccCode" False runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="Tax Invoice No/<br> Date" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right ItemStyle-Width="12%" >
			                        <ItemTemplate>
				                        <asp:Label Text=<%# Container.DataItem("FPNo") %> id="lblFPNo" Visible=false runat="server" />
				                        <asp:LinkButton id="lbFPNo" CommandName="Edit" Text=<%#Container.DataItem("FPNo")%> CausesValidation=False runat="server" /><br />
				                        <%#objGlobal.GetLongDate(Container.DataItem("FPDate"))%> 
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        <asp:TemplateColumn HeaderText="PPN<br>Dikreditkan" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
		                            <ItemTemplate> 
			                            <ItemStyle />
			                                <asp:Label Text=<%# Container.DataItem("FPAmountCredited") %> id="lblFPAmountCredited" visible = False runat="server" />
			                                <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FPAmountCredited"), 2), 2) %> id="lblIDFPAmountCredited" runat="server" /><br />
			                            </ItemStyle>
		                            </ItemTemplate>
	                            </asp:TemplateColumn>		
	                            <asp:TemplateColumn HeaderText="PPN<br>Tdk Dikreditkan" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
		                            <ItemTemplate> 
			                            <ItemStyle />
			                                <asp:Label Text=<%# Container.DataItem("FPAmountCreditedNot") %> id="lblFPAmountCreditedNot" visible = False runat="server" />
			                                <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FPAmountCreditedNot"), 2), 2) %> id="lblIDFPAmountCreditedNot" runat="server" />
			                            </ItemStyle>
		                            </ItemTemplate>
	                            </asp:TemplateColumn>
	                            <asp:TemplateColumn HeaderText="Status<br>Period">
			                        <ItemTemplate>
				                        <asp:Label Text=<%#Container.DataItem("Status")%> id="lblStatus" runat="server" /><br />
				                        <asp:Label Text=<%#Container.DataItem("Period")%> id="lblPeriod" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
	                            <asp:TemplateColumn>
		                                <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelected_Changed" AutoPostBack=true  Checked = <%#Container.DataItem("IsCheck")%> />
							            </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
					            </asp:TemplateColumn>
							    <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
			                        <ItemTemplate>
			                            <asp:Label id=lblTrxID Text='<%# Trim(Container.DataItem("TrxID")) %>' Visible=False runat=server />
			                            <asp:Label id=lblAccCode Text='<%# Trim(Container.DataItem("AccCode")) %>' Visible=False runat=server />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>	
		                        <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
			                        <ItemTemplate>
			                            <asp:Label id=lblTrxLnID Text='<%# Trim(Container.DataItem("TrxLnID")) %>' Visible=False runat=server />
			                            <asp:Label id=lblAccMonth Text='<%# Trim(Container.DataItem("FPAccMonth")) %>' Visible=False runat=server />
				                    </ItemTemplate>
		                        </asp:TemplateColumn>	
		                        <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
			                        <ItemTemplate>
				                        <asp:Label id=lblAccYear Text='<%# Trim(Container.DataItem("FPAccYear")) %>' Visible=False runat=server />
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
                    <td align=right colspan="6">
	                    <asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
	                    <asp:DropDownList id="lstDropList" runat="server"
		                    AutoPostBack="True" 
		                    onSelectedIndexChanged="PagingIndexChanged" />
 	                    <asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
                    </td>
                    </tr>
					<tr>
						<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td width=20%>Total DPP Amount : </td>
									<td width=5% align=right><asp:Label id=lblTtlDPPAmt runat=server /></td>
								</tr>
								<tr>
									<td width=20%>Total PPN Dikreditkan : </td>
									<td width=5% align=right><asp:Label id=lblTtlPPNCreditedAmt runat=server /></td>
								</tr>
								<tr>
									<td width=20%>Total PPN Tdk Dikreditkan : </td>
									<td width=5% align=right><asp:Label id=lblTtlPPNCreditedNotAmt runat=server /></td>
								</tr>
							</table>
						</td>
					</tr>
                    
                    <tr>
				        <td colspan=5>&nbsp;</td>
			        </tr>
                    <tr>
					    <td colspan="4">					
                        <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				    </tr>		
				    <tr>
					    <td colspan="4">					
                        <asp:CheckBox id="cbCSV" text=" Export To CSV" checked="false" runat="server" /></td>
				    </tr>	
				    <tr>
				        <td colspan=5>&nbsp;</td>
			        </tr>
			        <tr>
				        <td>
				            <asp:ImageButton id="PrintDoc" ToolTip="Print document" UseSubmitBehavior="false" OnClick="PrintDoc_Click" runat="server" ImageUrl="../../images/butt_print_doc.gif"/>
					        <asp:ImageButton ID="PostingBtn"  UseSubmitBehavior="false" AlternateText="Posting" onclick="PostingBtn_Click"  ImageUrl="../../images/butt_post.gif"  CausesValidation=False Runat=server />
					        <asp:ImageButton ID="CancelBtn"  UseSubmitBehavior="false" AlternateText="Cancel" onclick="CancelBtn_Click"  ImageUrl="../../images/butt_cancel.gif"  CausesValidation=False Runat=server />
				        </td>
			        </tr>
			        <tr>
                        <td colspan=2>
                            <asp:HyperLink ID="LinkDownload" ForeColor=red Visible=false runat="server" NavigateUrl="~/en/TX/trx/TX_trx_PPNKeluaranList.aspx">HyperLink</asp:HyperLink>
                        </td>
                    </tr>
                    <tr><td colspan=2>&nbsp;</td></tr>
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
			<Input Type=Hidden id=hidTrxStatus value=0 runat=server />
		</FORM>
	</body>
</html>
