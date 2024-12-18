<%@ Page Language="vb" src="../../../include/TX_trx_PPNNoList.aspx.vb" Inherits="TX_trx_PPNNoList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>VAT No. Assignment</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server"  class="main-modul-bg-app-list-pu">
     <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 	
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" /> 

		<table border="0" cellspacing="1" cellpadding=1 width="100%" class="font9Tahoma">
				<tr>
					<td colspan="2"></td>
				</tr>
				<tr>
					<td class="mt-h">&nbsp;</td>
					<td align=right><asp:label id="lblTracker" Visible=false runat="server"/></td>
				</tr>
				<tr>
					<td colspan=2><hr size="1" noshade></td>
				</tr>
                
                <tr class="font9Tahoma"
                    <td colspan=6 width=100%>
                        <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                            SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" runat="server">
                            <DefaultTabStyle ForeColor="black" Height="22px">
                            </DefaultTabStyle>
                            <HoverTabStyle CssClass="ContentTabHover">
                            </HoverTabStyle>
                            <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                                NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                                FillStyle="LeftMergedWithCenter"></RoundedImage>
                            <SelectedTabStyle CssClass="ContentTabSelected">
                            </SelectedTabStyle>
                            <Tabs>
				                <%--Assigning Number--%>
                                <igtab:Tab Key="TAB1" Text="Assigning Number" Tooltip="Assigning Number">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%" class="font9Tahoma">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div1" style="height: 200px;width:1000;">
										                <table id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0"  class="font9Tahoma" runat=server>
                                                            <tr>
				                                                <td colspan=6 height=25>
					                                                <font color=red>Important notes :</font><p>
					                                                - Click REFRESH for review all invoices.<br>
					                                                - Click Checkbox per item and GENERATE to process assigning tax number into invoices on temporary.<br>
					                                                - Click POST to post invoices that had assigned on temporary.<br>
					                                            </td>
			                                                </tr>
			                                                <tr>
			                                                    <td colspan=5 height=25>&nbsp;</td>
		                                                    </tr>										                
                                                            <tr>
                                                                <td width="15%" height=25>Invoice Period : </td>
                                                                <td colSpan="5">
													                <asp:DropDownList id="lstAccMonth" width=7% runat=server>
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
													                &nbsp;
													                <asp:DropDownList id=lstAccYear width="10%" maxlength="4" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colSpan="5">&nbsp;</td>
                                                            </tr>	
                                                            <tr>
                                                                <td height=25 colspan=6>
                                                                    <asp:ImageButton id=RefreshBtn ToolTip="Refresh" imageurl="../../images/butt_refresh.gif" AlternateText="Refresh" OnClick="RefreshBtn_Click" runat="server" />
                                                                    <asp:ImageButton id=GenerateBtn ToolTip="Generate/assigning tax number" imageurl="../../images/butt_generate.gif" alternatetext="Generate/Assigning tax number" CausesValidation=True onclick=GenerateBtn_Click UseSubmitBehavior="false" runat=server /> 									
                                                                    <asp:ImageButton ID=PostingBtn ToolTip="Posting assignment" UseSubmitBehavior="false" AlternateText="Posting" onclick="PostingBtn_Click"  ImageUrl="../../images/butt_post.gif"  CausesValidation=False Runat=server />
																	<asp:ImageButton id=RollBackBtn ToolTip="Rollback" onclick=RollBackBtn_Click imageurl="../../images/butt_rollback.gif" alternatetext="Rollback" runat=server />
																	<asp:ImageButton id=DownloadBtn ToolTip="Download CSV" onclick=DownloadBtn_Click imageurl="../../images/butt_download.gif" alternatetext="Download" runat=server />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
				                                                <td colspan=2>
				                                                    <asp:HyperLink ID="LinkDownload" ForeColor=red Visible=false runat="server" NavigateUrl="~/en/TX/trx/TX_trx_PPNNoList.aspx">HyperLink</asp:HyperLink>
				                                                </td>
				                                            </tr>
				                                            <tr><td colspan=2>&nbsp;</td></tr>
                                                            <tr>
                                                                <td height=25 colspan=2><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor=red runat=server /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="div4" style="height: 700px;width:1020;overflow: auto;">                                                        
										                <asp:DataGrid id=dgLine runat=server 
											                AutoGenerateColumns=false width=100% 
											                GridLines=none 
											                Cellpadding=2 
											                Allowcustompaging=False 
											                OnPageIndexChanged=OnPageChanged 
											                Pagerstyle-Visible=False
											                OnEditCommand=DEDR_FPDetail
											                OnUpdateCommand=DEDR_FPDetail
											                OnSortCommand=Sort_Grid 
											                AllowSorting=True CssClass="font9Tahoma">
                												
											                <HeaderStyle CssClass="mr-h" />
											                <ItemStyle CssClass="mr-l" />
											                <AlternatingItemStyle CssClass="mr-r" />
                											
										                   <Columns>
										                        <asp:TemplateColumn HeaderText="Document ID/<br> Date" ItemStyle-Width="10%" >
													                <ItemTemplate>
														                <asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" Visible=false runat="server" />
														                <asp:Label id="lbDocID" Text=<%#Container.DataItem("DocID")%> CausesValidation=False runat="server" /><br />
														                <asp:Label Text=<%#objGlobal.GetLongDate(Container.DataItem("DocDate"))%> id="lblIDDocDate" runat="server" />
														                <asp:Label Text=<%#Container.DataItem("DocDate")%> id="lblDocDate" Visible=false runat="server" />
														                <asp:Label Text=<%# trim(Container.DataItem("AccMonth"))+"/"+trim(Container.DataItem("AccYear")) %> id="lblPeriod" Visible=false runat="server" />
													                </ItemTemplate>
												                </asp:TemplateColumn>
												                <asp:TemplateColumn HeaderText="Customer Name" ItemStyle-Width="15%" >
													                <ItemTemplate>
														                <asp:Label Text=<%#Container.DataItem("SupplierName")%> id="lblSupplierName" runat="server" /><br />
														                <asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" visible = False runat="server" />
													                </ItemTemplate>
												                </asp:TemplateColumn>
												                <asp:TemplateColumn HeaderText="Description" ItemStyle-Width="20%" >
													                <ItemTemplate>
														                <asp:Label Text=<%#Container.DataItem("DocDescr")%> id="lblDocDescr" runat="server" /><br />
													                </ItemTemplate>
												                </asp:TemplateColumn>
												                <asp:TemplateColumn HeaderText="DPP Amount/<br> Tax Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
													                <ItemTemplate>
														                <asp:Label id="lblIDDPPAmount" Text=<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPPAmount"), 0), 0)%> CausesValidation=False runat="server" /><br />
														                <asp:Label Text=<%# Container.DataItem("DPPAmount") %> id="lblDPPAmount" visible = False runat="server" /><br />
														                <asp:Label id="lblIDTaxAmount" Text=<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TaxAmount"), 0), 0)%> CausesValidation=False runat="server" /><br />
														                <asp:Label Text=<%# Container.DataItem("DPPAmount") %> id="lblTaxAmount" visible = False runat="server" />
													                </ItemTemplate>
												                </asp:TemplateColumn>
												                <asp:TemplateColumn HeaderText="Receipt No/<br>Date" ItemStyle-Width="12%" >
													                <ItemTemplate>
													                    <asp:Label Text=<%# Container.DataItem("ReceiptID") %> id="lblRcpID" runat="server" /><br />
														                <asp:Label Text=<%#objGlobal.GetLongDate(Container.DataItem("ReceiptDate"))%> id="lblRcpDate" runat="server" />
													                </ItemTemplate>
												                </asp:TemplateColumn>
																
																<asp:TemplateColumn HeaderText="Type" ItemStyle-Width="7%">
																	<ItemTemplate>
																			<asp:DropDownList id="ddlTaxType" width="75%" OnSelectedIndexChanged="Onchanged_TaxType" AutoPostBack=true runat="server" />
																			<asp:Label Text=<%# Container.DataItem("TaxType") %> id="lblTaxType" Visible=false runat="server" />
																	</ItemTemplate>
																</asp:TemplateColumn>

																
																<asp:TemplateColumn>
						                                            <ItemTemplate> 
                                                                            <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelected_Changed" AutoPostBack=true
                                                                            Checked = <%#Container.DataItem("IsCheck")%> />
						                                            </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
					                                            </asp:TemplateColumn>
																
												                <asp:TemplateColumn HeaderText="Tax Invoice No/<br>Date" ItemStyle-Width="12%" >
													                <ItemTemplate>
														                <asp:Label Text=<%# Container.DataItem("FPNo") %> id="lblFPNo" runat="server" /><br />
														                <asp:Label Text=<%#objGlobal.GetLongDate(Container.DataItem("FPDate"))%> id="lblFPDate" runat="server" />
													                </ItemTemplate>
												                </asp:TemplateColumn>
											                </Columns>
										                </asp:DataGrid><BR>
										                <tr>
                                                            <td align=right colspan="6">
	                                                            <asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" Visible=false commandargument="prev" onClick="btnPrevNext_Click" />
	                                                            <asp:DropDownList id="lstDropList" runat="server" Visible=false
		                                                            AutoPostBack="True" 
		                                                            onSelectedIndexChanged="PagingIndexChanged" />
 	                                                            <asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" Visible=false commandargument="next" onClick="btnPrevNext_Click" />
                                                            </td>
                                                        </tr>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                </igtab:Tab>
                                
                                <%--Generate Sequence--%>
                                <igtab:Tab Key="TAB2" Text="Generate Sequence" Tooltip="Generate Sequence Number">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%" class="font9Tahoma">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div2" style="height: 350px;width:1000;">		
                                                        <table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0"  class="font9Tahoma" runat=server>
                                                            <tr>
				                                                <td colspan=7 height=25>
					                                                <font color=red>Important notes :</font><p>
					                                                - Click REFRESH for review all tax number that had created.<br>
					                                                - Click GENERATE to process sequence tax number on temporary.<br>
					                                                - Click POST to post sequence tax number had created on temporary.<br>
					                                            </td>
			                                                </tr>
			                                                <tr>
			                                                    <td colspan=6 height=25>&nbsp;</td>
		                                                    </tr>
                                                            <tr id="TrxPeriod">
                                                                <td width="15%" height=25>Tax Period : </td>
                                                                <td colSpan="6">
                                                                    <asp:DropDownList id="ddlTaxMonth" width=10% runat=server>
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
                                                                    &nbsp;
                                                                    <asp:DropDownList id=ddlTaxYear width="10%" maxlength="4" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Effective Date : </td>
                                                                <td colSpan="6"><asp:TextBox id=txtFPDate width=10% maxlength="10" runat="server"/>
					                                                <asp:RequiredFieldValidator	id="rfvtFPDate" runat="server"  ControlToValidate="txtFPDate" text = "Please enter effective date" display="dynamic"/>
					                                                <asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					                                                <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				                                                </td>
                                                            </tr>
                                                            <tr id="TrxCode">
                                                                <td width="15%" height=25>Transaction Code : </td>
                                                                <td colSpan="6"><asp:DropDownList id="ddlTaxTrx" width=10% runat=server>
																	                <asp:ListItem value="01" Selected>01</asp:ListItem>
																	                <asp:ListItem value="02">02</asp:ListItem>										
																	                <asp:ListItem value="03">03</asp:ListItem>
																	                <asp:ListItem value="04">04</asp:ListItem>
																	                <asp:ListItem value="05">05</asp:ListItem>
																	                <asp:ListItem value="06">06</asp:ListItem>
																	                <asp:ListItem value="07">07</asp:ListItem>
																	                <asp:ListItem value="08">08</asp:ListItem>
																	                <asp:ListItem value="09">09</asp:ListItem>
																                </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr id="TrxStatus">
                                                                <td width="15%" height=25>Status : </td>
                                                                <td colSpan="6"><asp:DropDownList id="ddlTaxStatus" width=10% runat=server>
																	                <asp:ListItem value="0" Selected>0</asp:ListItem>
																	                <asp:ListItem value="1">1</asp:ListItem>
																                </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Branch : </td>
                                                                <td colSpan="6"><asp:TextBox id=txtTaxBranch width=10% maxlength=3 runat=server />
																        		
                                                                <asp:DropDownList id="ddlTaxBranch" Visible=false width=10% runat=server>
																	                <asp:ListItem value="000" Selected>000</asp:ListItem>
																	                <asp:ListItem value="001">001</asp:ListItem>
																	                <asp:ListItem value="002">002</asp:ListItem>
																	                <asp:ListItem value="003">003</asp:ListItem>
																	                <asp:ListItem value="004">004</asp:ListItem>
																	                <asp:ListItem value="032">032</asp:ListItem>
																                </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Year : </td>
                                                                <td colSpan="6"><asp:TextBox id=txtTaxYear width=10% maxlength=2 runat=server />
                                                                                <asp:Label id=lblErrTaxYear visible=false forecolor=red text="Tax year cannot be empty." runat=server />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Sequence From : </td>
                                                                <td colSpan="6"><asp:TextBox id=txtTaxNoFrom width=10% maxlength=8 runat=server />
																        		-
																                <asp:TextBox id=txtTaxNoTo width=10% maxlength=8 runat=server />
                                                                                <asp:Label id=lblErrSequence visible=false forecolor=red runat=server />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colSpan="6">&nbsp;</td>
                                                            </tr>	
                                                            <tr>
                                                                <td height=25 colspan=7>
                                                                    <asp:ImageButton id=RefreshNoBtn ToolTip="Refresh" imageurl="../../images/butt_refresh.gif" AlternateText="Refresh" OnClick="RefreshNoBtn_Click" runat="server" />
                                                                    <asp:ImageButton id=GenerateNoBtn ToolTip="Generate Number" imageurl="../../images/butt_generate.gif" alternatetext=Add CausesValidation=True onclick=GenerateNoBtn_Click UseSubmitBehavior="false" runat=server /> 					
                                                                    <asp:ImageButton ID=PostingNoBtn ToolTip="Posting Number" UseSubmitBehavior="false" AlternateText="Posting" onclick="PostingNoBtn_Click"  ImageUrl="../../images/butt_post.gif"  CausesValidation=False Runat=server />
																	<asp:ImageButton id=RollBackNoBtn ToolTip="Rollback Number" onclick=RollBackNoBtn_Click imageurl="../../images/butt_rollback.gif" alternatetext="Rollback" runat=server />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td height=25 colspan=2><asp:Label id=lblErrMessageNo visible=false Text="Error while initiating component." ForeColor=red runat=server /></td>
                                                            </tr>
                                                        </table>
                                                    </div>                                                    
                                                    <div id="div3" style="height: 300px;width:1000;overflow: auto;">
                                                        <asp:DataGrid id=dgLineGen
	                                                        AutoGenerateColumns="false" width="100%" runat="server"
	                                                        GridLines=none
	                                                        Cellpadding="2"
	                                                        Pagerstyle-Visible="False"
	                                                        AllowSorting="True"  CssClass="font9Tahoma">
	                                                        <HeaderStyle CssClass="mr-h"/>
	                                                        <ItemStyle CssClass="mr-l"/>
	                                                        <AlternatingItemStyle CssClass="mr-r"/>
	                                                        <Columns>	
	                                                            <asp:TemplateColumn HeaderText="Period" ItemStyle-Width="3%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# trim(Container.DataItem("AccMonth"))+"/"+trim(Container.DataItem("AccYear")) %> id="lblFPPeriod" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Eff. Date" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("EffDate")) %> id="lblEffDate" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Tax Inv No" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("FPNo") %> id="lblFPNo" runat="server" /><br />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Tax Inv Date" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("FakturPajakDate")) %> id="lblFPDate" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Rsvd">
						                                            <ItemTemplate>
                                                                            <asp:CheckBox ID="chkReserved" runat="server" OnCheckedChanged="chkReserved_Changed" AutoPostBack=true
                                                                            Checked = <%#Container.DataItem("IsReserved")%> />
						                                            </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
					                                            </asp:TemplateColumn>	
												                	
		                                                        <asp:TemplateColumn HeaderText="Document ID<br>Date" ItemStyle-Width="10%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("DocID") %> id="lblDocID" runat="server" /><br />
				                                                        <asp:Label visible=true Text=<%#objGlobal.GetLongDate(Container.DataItem("DocDate"))%> id="lblDocDate" runat="server" />
			                                                        </ItemTemplate>
		                                                        </asp:TemplateColumn>	
		                                                        <asp:TemplateColumn HeaderText="Customer Name" ItemStyle-Width="15%" >
													                <ItemTemplate>
														                <asp:Label Text=<%#Container.DataItem("SupplierName")%> id="lblSupplierName" runat="server" /><br />
														                <asp:Label Text=<%# Container.DataItem("SupplierCode") %> id="lblSupplierCode" visible = False runat="server" />
													                </ItemTemplate>
												                </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Description" ItemStyle-Width="20%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("Description") %> id="lblDescr" runat="server" />
			                                                        </ItemTemplate>
		                                                        </asp:TemplateColumn>		                                                        
                                                        </Columns>										
                                                        </asp:DataGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                </igtab:Tab>                    
                            </Tabs>
                        </igtab:UltraWebTab>
                    </td>
                </tr>
                
                
			</table>


			<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />		
			<Input Type=Hidden id=intRec value=0 runat=server />
			<Input Type=Hidden id=hidStatus value=0 runat=server />
			<Input Type=Hidden id=hidTaxType value="" runat=server />
			<Input Type=Hidden id=hidTaxTypeLn value=0 runat=server />
			
        </div>
        </td>
        </tr>
        </table>
		</FORM>
	</body>
</html>
