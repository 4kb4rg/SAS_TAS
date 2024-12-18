<%@ Page Language="vb" src="../../../include/PU_trx_POList.aspx.vb" Inherits="PU_POList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">


</script>

<html>
	<head>
		<title>Purchase Order List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPOList class="main-modul-bg-app-list-pu" runat="server">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" class="font9Tahoma" width="100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=menuPU runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
				<tr>
					<td   colspan="3"><strong> PURCHASE ORDER LIST</strong></td>
					<td align="right" colspan="3" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   
                            </td>
				</tr>
        

				<tr>
					<td colspan=6 width=100% class="font9Tahoma">
						<table width="100%" class="font9Tahoma" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr style="background-color:#FFCC00">
								<td width="15%" height="26">RPH ID :<BR><asp:TextBox id=txtRPHId width=100% maxlength="32" CssClass="fontObject" runat="server"/></td>
								<td width="15%" height="26">PO ID :<BR><asp:TextBox id=txtPOId width=100% maxlength="32" CssClass="fontObject"  runat="server" /></td>
								<td width="10%" height="26">PR ID :<BR><asp:TextBox id=txtPRId width=100% maxlength="32" CssClass="fontObject"  runat="server"/></td>
                                <td height="26" width="10%">
                                    Departement :<br /><asp:DropDownList id="lstDept" width=80px CssClass="fontObject" runat=server /></td>
								<td height="26" style="width: 8%">PO Type :<BR><asp:DropDownList id="ddlPOType" width="100%" CssClass="fontObject"  runat=server /></td>
								<td width="13%" height="26">Supplier :<BR><asp:TextBox id=txtSuppCode width=100% maxlength="20" CssClass="fontObject"  runat="server"/></td>
								<td width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% CssClass="fontObject"  runat=server>
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
								<td width=8%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject"  runat=server>
									</asp:DropDownList>
								<td height="26" style="width: 13%">Status :<BR>
									<asp:DropDownList id="ddlStatus" width="100%" CssClass="fontObject"  runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1">Active</asp:ListItem>
										<asp:ListItem value="2">Confirmed</asp:ListItem>
										<asp:ListItem value="3">Deleted</asp:ListItem>
										<asp:ListItem value="4">Cancelled</asp:ListItem>
										<asp:ListItem value="5">Invoiced</asp:ListItem>
										<asp:ListItem value="6">Closed</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td height="26"><BR></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click class="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:DataGrid id=dgPOList runat=server
							AutoGenerateColumns=False width=100% 
							GridLines=None 
							Cellpadding=2 
							AllowPaging=True 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnEditCommand=DEDR_Edit
							OnSortCommand=Sort_Grid 
                             OnItemDataBound="dgLine_BindGrid" 
							AllowSorting=True >
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>
							
							<Columns>
								<asp:BoundColumn Visible=False DataField="POId" />
								<asp:BoundColumn Visible=False DataField="Status" />
								
								
								<asp:HyperLinkColumn HeaderText="PO ID" 
									SortExpression="A.POId" 
									DataNavigateUrlField="POId" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POId={0}"
									DataTextFormatString="{0:c}"
									DataTextField="POId" />
								
								<asp:HyperLinkColumn HeaderText="PR ID" 
									SortExpression="E.PRID" 
									DataNavigateUrlField="POId" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POId={0}"
									DataTextFormatString="{0:c}"
									DataTextField="PRID" />
									
								<asp:HyperLinkColumn HeaderText="Supplier Code" 
									SortExpression="A.SupplierCode" 
									DataNavigateUrlField="POId" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POId={0}" 
									DataTextField="SupplierCode" Visible=False />
								
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression="Name">
									<ItemTemplate>
										<%#Container.DataItem("SupplierName")%>
									</ItemTemplate>
								</asp:TemplateColumn>
				                
				                <asp:TemplateColumn HeaderText="Tempat Penyerahan" SortExpression="LocDescPenyerahan">
									<ItemTemplate>
										<%#Container.DataItem("LocDescPenyerahan")%>
									</ItemTemplate>
								</asp:TemplateColumn>																
								<asp:TemplateColumn HeaderText="Amount" SortExpression="A.TotalAmountCurrency">
									<ItemTemplate>
										<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmountCurrency"), 2), 2)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PO Type/ Departement" SortExpression="A.POType">
									<ItemTemplate>
										<%# Container.DataItem("POType") %>
										<asp:label id="lblPOType" Text='<%# Container.DataItem("POType") %>' Visible="False" Runat="server" />
									<br>
										<%# Container.DataItem("DeptCode") %>
										<asp:label id="lstDept" Text='<%# Container.DataItem("DeptCode") %>' Visible="False" Runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="PO Date" SortExpression="A.CreateDate">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%> 
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="PO USER"  SortExpression="UserNamePO">
									<ItemTemplate>
										<%#Container.DataItem("UserNamePO")%> 
									</ItemTemplate>
								</asp:TemplateColumn>
														
								<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									<ItemTemplate>
										<%# objPU.mtdGetPOStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
                               									
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="C.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Document Received ID">
									<ItemTemplate>
										<asp:label id="lblDocID" Text='<%# Trim(Container.DataItem("DOcID")) %>' Visible="True" Runat="server" /><br />
										<asp:label id="lblGRStatus" Text='<%# Trim(Container.DataItem("GrStatus")) %>' Visible="False" Runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>									
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server/>
										<asp:LinkButton id=lbUndelete CommandName=Edit Text=Undelete runat=server/>
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
						<BR><asp:Label id=lblErrQtyReceive visible=false forecolor=red Text="Quantity received for this PO." runat=server />
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" CssClass="fontObject"  runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
					    <asp:ImageButton id=NewINPOBtn UseSubmitBehavior="false" onClick=NewINPOBtn_Click imageurl="../../images/butt_new_stockpo.gif" AlternateText="New Stock/Workshop Purchase Order" runat=server/>
						<asp:ImageButton id=NewDCPOBtn UseSubmitBehavior="false" onClick=NewDCPOBtn_Click imageurl="../../images/butt_new_directchargepo.gif" AlternateText="New Direct Charge Purchase Order" runat=server/>
						<asp:ImageButton id=NewFAPOBtn UseSubmitBehavior="false" onClick=NewFAPOBtn_Click imageurl="../../images/butt_new_fapo.gif" AlternateText="New Fixed Asset Purchase Order" runat=server/>
						<asp:ImageButton id=NewNUPOBtn UseSubmitBehavior="false" onClick=NewNUPOBtn_Click imageurl="../../images/butt_new_nurserypo.gif" AlternateText="New Nursery Purchase Order" runat=server/>
						
						<!--a href="#" onclick="javascript:popwin(400, 600, 'PU_trx_PrintDocs.aspx?doctype=1&TrxID=')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a-->
					</td>
				</tr>
                <tr>
                    <td align="left" colspan="6" width="100%">
                        <table style="text-align: left">
                            <tr>
                                <td style="width: 59px; height: 21px">
                                </td>
                                <td style="width: 15px; height: 21px">
                                </td>
                                <td style="width: 100px; height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <span style="font-size: 9pt; font-family: Arial; text-decoration: underline"><strong>
                                        Goods Received &nbsp;Status </strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    <span style="font-size: 8pt; font-family: Arial">Need Confirmation</span></td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label1" runat="server" BackColor="Yellow" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                            <tr style="font-size: 8pt; font-family: Arial">
                                <td style="width: 100px">
                                    <span>Full Received </span>
                                </td>
                                <td style="width: 15px">
                                    :</td>
                                <td style="width: 100px">
                                    <asp:Label ID="Label4" runat="server" BackColor="Blue" Text="-" Width="40px"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
                </div>
                </td>
                
                </tr>
			</table>
		</FORM>
	</body>
</html>
