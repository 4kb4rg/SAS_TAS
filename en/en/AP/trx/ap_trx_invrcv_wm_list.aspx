<%@ Page Language="vb" trace=false src="../../../include/ap_trx_invrcv_wm_list.aspx.vb" Inherits="ap_trx_invrcv_wm_list" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<html>
	<head>
		<title>Weighing Credit Invoice List</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmMain runat=server >
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<Input type=hidden id=hidSearch value="" runat=server />
			<table border=0 cellspacing=1 cellpadding=1 width=100%>
				<tr>
					<td colspan=6>
					
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3">WEIGHING CREDIT INVOICE LIST</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="14%" height="26">Weighing Inv ID. :<BR><asp:TextBox id=srchTrxID width=100% maxlength="32" runat="server" /></td>
								<td width="14%">Weighing Ref No. :<BR><asp:TextBox id=srchRefNo width=100% maxlength="20" runat="server"/></td>
								<td valign=top width="20%" height="26">
								    Date From :<BR>
									    <asp:TextBox id=srchDate width=50% maxlength="10" runat="server"/>
									    <a href="javascript:PopCal('srchDate');"><asp:Image id="btnSelDateFrom" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
									    <asp:Label id=lblErrDateMsg visible=false text="<br>Date Format should be in " forecolor=red runat=server/>						
									    <asp:Label id=lblErrDate forecolor=red visible=false runat=server/><BR>
									Date To:<br />
									    <asp:TextBox id=srchDateTo width=50% maxlength="10" runat="server"/>
									    <a href="javascript:PopCal('srchDateTo');"><asp:Image id="Image1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
									    <asp:Label id=lblErrDateToMsg visible=false text="<br>Date Format should be in " forecolor=red runat=server/>						
									    <asp:Label id=lblErrDateTo forecolor=red visible=false runat=server/>
								</td>
								<td width="20%">Supplier Code :<BR><asp:TextBox id=srchSupplier width=100% maxlength="20" runat="server"/></td>																								
								<td width="10%">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="15%">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" align=right style="height: 56px"><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				
			</table>
			
			<table border=0 cellspacing=1 cellpadding=1 width=100%>
                <tr>
                    <td style="height: 24px;" colspan="5">
                        <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                            SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
                            <DefaultTabStyle Height="22px">
                            </DefaultTabStyle>
                            <HoverTabStyle CssClass="ContentTabHover">
                            </HoverTabStyle>
                            <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                            <SelectedTabStyle CssClass="ContentTabSelected">
                            </SelectedTabStyle>
                            <Tabs>
                                <igtab:Tab Key="INVOICE" Text="INVOICE LISTING" Tooltip="INVOICE LISTING">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                            <tr>
					                            <td colspan=6>				
					                                <div id="div1" style="height:270px;width:1040;overflow:auto;">		
						                                <asp:DataGrid id=dgList
							                                AutoGenerateColumns=false width=100% runat=server
							                                GridLines=none 
							                                Cellpadding=2 
							                                Allowcustompaging=False 
							                                OnPageIndexChanged=OnPageChanged 
							                                Pagerstyle-Visible=False 
							                                OnEditCommand="DEDR_Edit"
						                                    OnUpdateCommand="DEDR_Update"
									                        OnDeleteCommand="DEDR_Delete"
									                        OnCancelCommand="DEDR_Cancel"
							                                OnSortCommand=Sort_Grid  
							                                AllowSorting=True>
                                							
							                                <HeaderStyle CssClass="mr-h" />
							                                <ItemStyle CssClass="mr-l" />
							                                <AlternatingItemStyle CssClass="mr-r" />
                                							
							                                <Columns>
								                                <asp:HyperLinkColumn HeaderText="Weighing Inv ID." 
									                                SortExpression="TrxID" 
									                                DataNavigateUrlField="TrxID" 
									                                DataNavigateUrlFormatString="ap_trx_invrcv_wm_det.aspx?trxID={0}" 
									                                DataTextField="TrxID" />
                                								
								                                <asp:HyperLinkColumn HeaderText="Ref No." 
									                                SortExpression="RefNo" 
									                                DataNavigateUrlField="TrxID" 
									                                DataNavigateUrlFormatString="ap_trx_invrcv_wm_det.aspx?trxID={0}" 
									                                DataTextField="RefNo" />
                                								
                                								<asp:TemplateColumn HeaderText="Supplier" SortExpression="SupplierCode">
									                                <ItemTemplate>
										                                <%#Container.DataItem("SupplierName")%>
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
								                                
								                                <asp:TemplateColumn HeaderText="Date" SortExpression="RefDate">
									                                <ItemTemplate>
										                                <%# objGlobal.GetLongDate(Container.DataItem("RefDate")) %>
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
								                                
								                                <asp:TemplateColumn HeaderText="Quantity" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								                                    <ItemTemplate>
										                                <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"), 0), 0)%>
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
								                                
								                                <asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								                                    <ItemTemplate>
										                            </ItemTemplate>
								                                </asp:TemplateColumn>
								                                
								                                <asp:TemplateColumn HeaderText="Total" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								                                    <ItemTemplate>
										                                <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmount"), 0), 0)%>
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
								                                
								                                <asp:TemplateColumn HeaderText="" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								                                    <ItemTemplate>
										                            </ItemTemplate>
								                                </asp:TemplateColumn>
								                                
								                                <asp:TemplateColumn HeaderText="Status" SortExpression="W.Status">
									                                <ItemTemplate>
										                                <asp:Label id=lblStatus text='<%# objAPTrx.mtdGetInvoiceRcvStatus(Container.DataItem("Status")) %>' runat=server/>	
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
                                								
								                                <asp:TemplateColumn HeaderText="Updated By" SortExpression="U.UserName">
									                                <ItemTemplate>
										                                <%# Container.DataItem("UserName") %>
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
								                                <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>
									                                <ItemTemplate>
										                                <asp:Label id=lblTrxID visible=false text='<%# Container.DataItem("TrxID") %>' runat=server/>																		
										                                <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
									                                    <asp:LinkButton id="Update" CommandName="Update" Text="Update" CausesValidation=False  runat="server"/>
										                                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False  runat="server"/>
										                                <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
									                                </ItemTemplate>
								                                </asp:TemplateColumn>
								                                <asp:TemplateColumn HeaderStyle-HorizontalAlign=Center HeaderText="Confirm">
                                                                    <ItemTemplate>
                                                                        <asp:Button Text="Confirm" OnClick=BtnConfirm_Click runat="server" ID="BtnConfirm" Font-Size="7pt" Width="56px" Height="26px" ToolTip="click confirm"/>
                                                                        <asp:Button Text="Cancel" OnClick=BtnCancel_Click runat="server" ID="BtnCancel" Font-Size="7pt" Width="56px" Visible="False" Height="26px" ToolTip="click cancel"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
							                                </Columns>
						                                </asp:DataGrid><BR>
						                            </div>
					                            </td>
				                            </tr>
				                            <tr>
					                            <td align=right colspan="6">
						                            <asp:ImageButton id="btnPrev" Visible=false runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						                            <asp:DropDownList id="lstDropList" Visible=false AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	                            <asp:Imagebutton id="btnNext" Visible=false runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					                            </td>
				                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                <asp:CheckBox id="cbExcelListRekap" text=" Preview Rekap" checked="false" runat="server" /></td>
                                            </tr>
				                            <tr>
                                                <td colspan="3">
                                                <asp:CheckBox id="cbExcelList" text=" Export To Excel" checked="false" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
				                            <tr>
					                            <td align="left" width="100%" ColSpan=6>
						                            <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Ticket" runat=server/>
						                            <asp:ImageButton id=GenInvBtn imageurl="../../images/butt_generate.gif" AlternateText="Generate invoice" OnClick="BtnGenInv_Click" runat="server" />
						                            <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print OnClick="btnPreview_Click" runat="server"/>
					                            </td>
				                            </tr>
                                        </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                            <igtab:Tab Key="INVOICERKP" Text="INVOICE SUMMARY" Tooltip="INVOICE SUMMARY">
                                <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div2" style="height:300px;width:1040;overflow:auto;">				
                                                        <asp:DataGrid ID="dgListSUM" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                            CellPadding="2" Width="100%">
                                                            <AlternatingItemStyle CssClass="mr-r" />
                                                            <ItemStyle CssClass="mr-l" />
                                                            <HeaderStyle CssClass="mr-h" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblNoUrut" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Name" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("NamaSupplier") %>' id="lblNamaSpl" runat="server" />
                                                                        <asp:Label Text='<%# Container.DataItem("KodeSupplier") %>' id="lblKodeSpl" Visible=false runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Category" HeaderStyle-HorizontalAlign=Center >
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("StatusSupplier") %>' id="lblStatusSpl" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Tonase" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetWeight"), 2), 0) %>' id="lblNetWeight" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Total Amount" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmount"), 2), 0) %>' id="lblTotalDiBayar" runat="server" />
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
                                            <tr>
                                                <td colspan="3">
                                                <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" Visible=false runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>
                                                    <asp:ImageButton id="PrintPrev" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />
                                                    <asp:ImageButton id="btnGenerate" Visible=false ToolTip="Generate journal" UseSubmitBehavior="false" OnClick="btnGenerate_Click"  runat="server" ImageUrl="../../images/butt_generate.gif"/>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                <asp:Label id=lblErrGenerate ForeColor=red Font-Italic=true visible=false runat=server />
                                                </td>
                                            </tr>
                                            
                                        </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                            <igtab:Tab Key="PAYMENT" Text="INVOICE PAYMENT" Tooltip="INVOICE PAYMENT">
                                <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div3" style="height:300px;width:1040;overflow:auto;">				
                                                        <asp:DataGrid ID="dgListPay" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                            CellPadding="2" Width="100%">
                                                            <AlternatingItemStyle CssClass="mr-r" />
                                                            <ItemStyle CssClass="mr-l" />
                                                            <HeaderStyle CssClass="mr-h" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblNoUrut" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("NamaSupplier") %>' id="lblNamaSpl" runat="server" />
                                                                        <asp:Label Text='<%# Container.DataItem("KodeSupplier") %>' id="lblKodeSpl" Visible=false runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign=Center >
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("NPWPNO") %>' id="lblNPWPNo" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Kategori" HeaderStyle-HorizontalAlign=Center >
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("StatusSupplier") %>' id="lblStatusSpl" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Nama Rekening" HeaderStyle-HorizontalAlign=Center >
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItem("BankAccName") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="No. Rekening" HeaderStyle-HorizontalAlign=Center >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" Text='<%# Container.DataItem("BankAccNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Bank" HeaderStyle-HorizontalAlign=Center >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" Text='<%# Container.DataItem("BankName") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="DPP" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 0) %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="PPN" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPN"), 2), 0) %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="PPH" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH"), 2), 0) %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Pembayaran TBS <br> excl. PPN" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalDiBayar"), 2), 0) %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Pembayaran TBS <br> incl. PPN" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalPembayaran"), 2), 0) %>' runat="server" />
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
                                            <tr>
                                                <td colspan="3">
                                                <asp:CheckBox id="cbExcelPay" text=" Export To Excel" checked="false" Visible=false runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>
                                                    <asp:ImageButton id="PrintPrevPay" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPrintPrevPay_Click" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                <asp:Label id=Label1 ForeColor=red Font-Italic=true visible=false runat=server />
                                                </td>
                                            </tr>
                                        
                                        </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                            <igtab:Tab Key="COA" Text="COA SETTING" Tooltip="COA SETTING">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="120%">
                                        <tr>
                                            <td>
                                                <div id="div4" >	
                                                    <tr>
                                                        <td height="25" style="width: 185px">Pembelian TBS (Pemilik Kebun) :</td>
                                                        <td><asp:DropDownList id="ddlTBSPemilik" width=90% runat=server></asp:DropDownList>
                                                            <input type=button value=" ... " id="FindAcc1" onclick="javascript:PopCOA('frmMain', '', 'ddlTBSOwner', 'False');" CausesValidation=False runat=server />  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" style="width: 185px">Pembelian TBS (Agen) :</td>
                                                        <td><asp:DropDownList id="ddlTBSAgen" width=90% runat=server></asp:DropDownList>
                                                            <input type=button value=" ... " id="FindAcc2" onclick="javascript:PopCOA('frmMain', '', 'ddlTBSAgen', 'False');" CausesValidation=False runat=server />  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" style="width: 185px">PPN :</td>
                                                        <td><asp:DropDownList id="ddlPPN" width=90% runat=server></asp:DropDownList>
                                                            <input type=button value=" ... " id="Button1" onclick="javascript:PopCOA('frmMain', '', 'ddlPPN', 'False');" CausesValidation=False runat=server />  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" style="width: 185px">PPh 22 :</td>
                                                        <td><asp:DropDownList id="ddlPPH" width=90% runat=server></asp:DropDownList>
                                                            <input type=button value=" ... " id="Button2" onclick="javascript:PopCOA('frmMain', '', 'ddlPPH', 'False');" CausesValidation=False runat=server />  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" style="width: 185px">Hutang SPTI Ongkos Bongkar TBS :</td>
                                                        <td><asp:DropDownList id="ddlOB" width=90% runat=server></asp:DropDownList>
                                                            <input type=button value=" ... " id="FindAcc3" onclick="javascript:PopCOA('frmMain', '', 'ddlOB', 'False');" CausesValidation=False runat=server />  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" style="width: 185px">Hutang SPTI Ongkos Lapangan TBS :</td>
                                                        <td><asp:DropDownList id="ddlOL" width=90% runat=server></asp:DropDownList>
                                                            <input type=button value=" ... " id="FindAcc4" onclick="javascript:PopCOA('frmMain', '', 'ddlOL', 'False');" CausesValidation=False runat=server />  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" style="width: 185px">
                                                            <asp:ImageButton id=btnSaveSetting imageurl="../../images/butt_save.gif" AlternateText="Save setting" OnClick="btnSaveSetting_Click" runat="server" /></td>
                                                    </tr>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                </td>
            </tr>	
          </table>                             
		</FORM>
	</body>
</html>
