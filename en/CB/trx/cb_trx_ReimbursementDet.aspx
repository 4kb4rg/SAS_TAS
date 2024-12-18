<%@ Page Language="vb" src="../../../include/cb_trx_ReimbursementDet.aspx.vb" Inherits="cb_trx_ReimbursementDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
	<title>Cash  List</title>
	<Preference:PrefHdl id=PrefHdl runat="server" />
      <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
</head>
	<body>
	    <form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
        <table id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
        <tr> 
        <td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">

			        <asp:label id="SortExpression" Visible="False" Runat="server" />
			        <asp:Label id=SortCol Visible=False Runat="server" />
			        <Input type=hidden id=hidInit value="" runat=server />
			        <Input type=hidden id=hidStatus value="1" runat=server />
			        <table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
			            <tr>
				            <td colspan="6"><UserControl:MenuCB id=MenuCB runat="server" /></td>
			            </tr>
			            <tr>
				            <td class="mt-h" colspan="6">REIMBURSEMENT</td>
				            <td colspan="3" align=right>&nbsp;</td>
			            </tr>
			            <tr>
				            <td colspan=6><hr style="width :100%" />   
                                    </td>
			            </tr>
			            <tr>
				            <td height=25 width="20%"><asp:Label id=lblPaymentIDTag Text = "Payment ID :" runat=server /></td>
				            <td width="35%"><asp:Label id=lblPaymentID runat=server /></td>
				            <td width="5%">&nbsp;</td>
				            <td width="15%">Period :</td>
				            <td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
				            <td width="5%"><asp:label id="lblTracker" runat="server"/></td>
			            </tr>
			            <tr>
			                <td height=25>Transaction Date :</td>
			                <td><asp:TextBox id=txtDateCreated width=25% maxlength="10" CssClass="font9Tahoma" runat="server"/>
					            <a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					            <asp:RequiredFieldValidator	id="RequiredFieldValidator1" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
					            <asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					            <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				            </td>
				            <td>&nbsp;</td>
				            <td>Status :</td>
				            <td><asp:Label id=lblStatus runat=server /></td>
				            <td></td>
			            </tr>
			            <tr>
				            <td height=25><asp:Label id=lblPayTypeTag Text = "Payment Type :*" runat=server /> </td>
                            <td colspan="2">
                                <asp:DropDownList width="95%" id=ddlPayType autopostback=false CssClass="font9Tahoma" runat=server>
						            <asp:ListItem value="0">Cheque</asp:ListItem>
						            <asp:ListItem value="1">Cash</asp:ListItem>
						            <asp:ListItem value="3">Bilyet Giro</asp:ListItem>
							        <asp:ListItem value="4">Others</asp:ListItem>
							        <asp:ListItem value="5">Slip Journal</asp:ListItem>
					            </asp:DropDownList>
				                <asp:Label id=lblErrPayType forecolor=red visible=false text="Please select Payment Type"  runat=server/>&nbsp;</td>
                            <td>Date Created :</td>
				            <td><asp:Label id=lblDateCreated runat=server /></td>
			            </tr>
			            <tr>
				            <td height=25><asp:Label id=lblBankFrom Text = "Bank From :" runat=server /> </td>
                            <td colspan="2">
                                <asp:DropDownList width="95%" id=ddlBank autopostback=true CssClass="font9Tahoma" runat=server /><asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/>&nbsp;</td>				    				        
				            <td>Last Update :</td>
				            <td><asp:Label ID=lblLastUpdate runat=server /></td>
			            </tr>
			            <tr>
				            <td>Cheque/Bilyet Giro No. :</td>
                            <td colspan="2">
                                <asp:Textbox id=txtChequeNo width="95%" maxlength=32 CssClass="font9Tahoma" runat=server /><asp:Label id=lblErrCheque forecolor=red visible=false text="Please enter Cheque/Bilyet Giro No." runat=server/>&nbsp;</td>
				            <td>Updated By :</td>
				            <td><asp:Label ID=lblUpdatedBy runat=server /></td>
			            </tr>
			            <tr>
			                <td>Cheque/Bilyet Giro Date :</td>
				            <td><asp:TextBox id=txtGiroDate width=25% maxlength="10" CssClass="font9Tahoma" runat="server"/>
					            <a href="javascript:PopCal('txtGiroDate');"><asp:Image id="btnGiroDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                                <br />
					            <asp:label id=lblDateGiro Text ="Date Entered should be in the format" forecolor=Red Visible = False Runat="server"/> 
					            <asp:label id=lblFmtGiro  forecolor=red Visible = false Runat="server"/> 
                                <br />
				            </td>
				            <td>&nbsp;</td>
				            <td>Cheque/Bilyet Giro Print Date :</td>
				            <td><asp:Label ID=lblChequePrintDate runat=server /></td>
			            </tr>
			            <tr>
			                <td height=25>Remark :</td>
				            <td colspan="2"><asp:Textbox id="txtRemark" width="95%"  runat=server />	
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td height=25>Total Amount :</td>
				            <td><asp:Textbox id="txtAmount" width=50% maxlength=22 style="text-align:right" Enabled=false CssClass="font9Tahoma" runat=server />	
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
			            </tr>
			    
			            <tr>
				            <td colspan=6>&nbsp;</td>
			            </tr>
				        <tr>
					        <td colspan=6>					
						        <asp:DataGrid id=dgDataList
							        AutoGenerateColumns=false width=100% runat=server
							        GridLines=none 
							        Cellpadding=2 
							        AllowPaging=True 
							        Allowcustompaging=False 
							        Pagesize=15 
							        OnPageIndexChanged=OnPageChanged 
							        Pagerstyle-Visible=False 
							        OnDeleteCommand=DEDR_Delete 
							        OnSortCommand=Sort_Grid
                                    OnItemDataBound="dgLine_BindGrid"
							        AllowSorting=True>
							
							        <HeaderStyle CssClass="mr-h" />							
						            <ItemStyle CssClass="mr-l" />
						            <AlternatingItemStyle CssClass="mr-r" />	
							        <Columns>
								        <asp:BoundColumn Visible=False HeaderText="ID" DataField="DocID" />
                                        <asp:TemplateColumn HeaderText="Doc ID" >
									        <ItemTemplate>
										        <%#Container.DataItem("DocID")%>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn HeaderText="Date" >
									        <ItemTemplate>
										        <%# objGlobal.GetLongDate(Container.DataItem("DocDate")) %>
									        </ItemTemplate>
								        </asp:TemplateColumn>				
								        <asp:TemplateColumn HeaderText="Doc Type" >
									        <ItemTemplate>
										        <%#Container.DataItem("DocType")%>
									        </ItemTemplate>
								        </asp:TemplateColumn>			 
								        <asp:TemplateColumn HeaderText="Payment To">
									        <ItemTemplate>
										        <%#Container.DataItem("FromTo")%>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn HeaderText="Doc Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									        <ItemTemplate>
										        <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DocAmt"), 2), 2)%>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn HeaderText="Cheque/Giro No">
									        <ItemTemplate>
										        <%#Container.DataItem("ChequeNo")%>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn HeaderText="Update Date" >
									        <ItemTemplate>
										        <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn HeaderText="Status" >
									        <ItemTemplate>
										        <%# objCBTrx.mtdGetCashBankStatus(Container.DataItem("Status")) %>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn HeaderText="Updated By" >
									        <ItemTemplate>
										        <%# Container.DataItem("UserName") %>
									        </ItemTemplate>
								        </asp:TemplateColumn>
								        <asp:TemplateColumn>
									        <ItemTemplate>
										        <asp:label id=idCBId visible="false" text= <%# Container.DataItem("DocID")%> runat="server" />
										        <asp:label id="lblStatus" Text= <%# Trim(Container.DataItem("Status")) %> Visible="False" Runat="server" />
										        <asp:label id="lblBalance" Text= <%# Container.DataItem("BalanceAmount") %> Visible="False" Runat="server" />
										        <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									        </ItemTemplate>
								        </asp:TemplateColumn>	
							        </Columns>
						        </asp:DataGrid><BR>
					        </td>
				        </tr>
				        <tr>
					        <td align=right colspan="6">
						        <asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						        <asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	        <asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					        </td>
				        </tr>
				        <tr>
					        <td align="left" width="100%" ColSpan=6>
						        <a href="#" onclick="javascript:popwin(400, 700, 'CB_trx_PrintDocs.aspx?doctype=1')"><asp:Image id="ibPrintDoc" Visible=false runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
						        <asp:ImageButton id=NewBtn UseSubmitBehavior="false" onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>			
						        <asp:ImageButton ID=SaveBtn UseSubmitBehavior="false" onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
						        <asp:ImageButton ID=DeleteBtn UseSubmitBehavior="false" onclick=DeleteBtn_Click CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					            <asp:ImageButton ID=ConfirmBtn UseSubmitBehavior="false" onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					            <asp:ImageButton id=CancelBtn UseSubmitBehavior="false" onClick="CancelBtn_Click" ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel CausesValidation=False runat="server" />
					        </td>
				        </tr>
			        </table>
			        <asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />
                    </table>
            </div>
            </td>
            </tr>			
		</table>
        </FORM>

	</body>
</html>
