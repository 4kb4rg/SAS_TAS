<%@ Page Language="vb" src="../../../include/PU_trx_RPHList.aspx.vb" Inherits="PU_trx_RPHList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Quotation (RPH) List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">


hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
    margin-bottom: 0px;
}
        </style>
	</head>
	<body>
		<form id=frmRPHList class="main-modul-bg-app-list-pu" runat="server">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
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
					<td class="font9Tahoma" colspan="3"><strong>QUOTATION (DTH) LIST </strong> </td>
					<td align="right" colspan="3" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   
                        </td>
				</tr>

				<tr>
                
				<td colspan=6 width=100% class="font9Tahoma">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="font9Tahoma" style="background-color:#FFCC00">
								<td width="17%" height="26" >
                                    DTH ID :<BR><asp:TextBox id=txtRPHID width=100% maxlength="32" CssClass="font9Tahoma" runat="server" /></td>
								<td width="17%" height="26">PR ID :<BR><asp:TextBox id="txtPRID" width=100% maxlength="32" runat=server /></td>
								<td width="13%" height="26">
                                    DTH Type :<BR><asp:DropDownList id=ddlRPHType width=100% maxlength="8" CssClass="font9Tahoma"  runat="server"/></td>
								<td width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% CssClass="font9Tahoma"  runat=server>
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
								<td width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="font9Tahoma"  runat=server>
									</asp:DropDownList>
								<td width="10%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="font9Tahoma"  runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected=true>Active</asp:ListItem>
										<asp:ListItem value="3">Cancelled</asp:ListItem>
										<asp:ListItem value="2">Approved</asp:ListItem>
										<asp:ListItem value="5">Deleted</asp:ListItem>
							 
									</asp:DropDownList>
								</td>
								<td width="14%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" CssClass="font9Tahoma"  runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
							</tr>
						</table>
				</td>

				</tr>
				<tr>
					<td colspan=6>
						<asp:DataGrid id=dgRPHList runat=server
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
							AllowSorting=True class="font9Tahoma">
						<HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
							
							<Columns>
								<asp:BoundColumn Visible=False DataField="RphID" />
								<asp:BoundColumn Visible=False DataField="Status" />
								
								<asp:HyperLinkColumn HeaderText="DTH ID" 
									SortExpression="A.RPHID" 
									DataNavigateUrlField="RPHID" 
									DataNavigateUrlFormatString="PU_trx_RPHDet.aspx?RPHID={0}" 
									DataTextField="RPHID" Visible=False />
								
								<asp:HyperLinkColumn HeaderText="DTH ID" 
									SortExpression="A.RPHID_Show" 
									DataNavigateUrlField="RPHID_Show" 
									DataNavigateUrlFormatString="PU_trx_RPHDet.aspx?RPHID={0}" 
									DataTextField="RPHID_Show" />
									
								<asp:TemplateColumn HeaderText="DTH Date" SortExpression="A.CreateDate">
                                <ItemStyle Width="7%" HorizontalAlign="left" />
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%> 
									</ItemTemplate>
								</asp:TemplateColumn>
																											
								<asp:HyperLinkColumn HeaderText="PO ID" 
									SortExpression=".POID" 
									DataNavigateUrlField="POID" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POID={0}" 
									DataTextField="POID" />	
									
								<asp:TemplateColumn HeaderText="Supplier" SortExpression="sup.Name">
                                 <ItemStyle Width="20%" HorizontalAlign="left" />
									<ItemTemplate>
										<%#Container.DataItem("Name")%>
										<asp:label id="lblsupName" Text='<%# Container.DataItem("Name") %>' Visible="True" Runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
                                															
								<asp:TemplateColumn HeaderText="Lokasi Penyerahan" SortExpression="A.LocPenyerahan">
									<ItemTemplate>
										<%# Container.DataItem("LocPenyerahan") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="User PO" SortExpression="A.UserPOName">
									<ItemTemplate>
										<%#Container.DataItem("UserPOName")%>
									</ItemTemplate>
								</asp:TemplateColumn>
																
								<asp:TemplateColumn HeaderText="User PO" SortExpression="A.UserPOName">
									<ItemTemplate>
										<%#Container.DataItem("UserPOName")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
																
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %> 
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									<ItemTemplate>
										<%# objPU.mtdGetRPHStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="C.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
				 
								
								<asp:TemplateColumn HeaderText="Rec.Item" Visible="False">
									<ItemTemplate><asp:label id="lblRecItem" Text='<%# Container.DataItem("JlhRecord") %>' Runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
																
								<asp:TemplateColumn Visible=False>
									<ItemTemplate>
										<asp:label id="lblStatus" Text='<%# Trim(Container.DataItem("Status")) %>' Visible="False" Runat="server" />
										<asp:LinkButton id=lbDelete Visible=false CommandName=Delete Text=Delete runat=server/>
										<asp:LinkButton id=lbUndelete Visible=false CommandName=Edit Text=Undelete runat=server/>
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
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="100%" ColSpan=6>
						<asp:ImageButton id=NewINPOBtn UseSubmitBehavior="false" onClick=NewINPOBtn_Click imageurl="../../images/butt_new_stockRPH.gif" AlternateText="New Stock/Workshop Quatation" runat=server/>
						<asp:ImageButton id=NewDCPOBtn UseSubmitBehavior="false" onClick=NewDCPOBtn_Click imageurl="../../images/butt_new_directchargeRPH.gif" AlternateText="New Direct Charge Quatation" runat=server/>
						<asp:ImageButton id=NewFAPOBtn UseSubmitBehavior="false" onClick=NewFAPOBtn_Click imageurl="../../images/butt_new_faRPH.gif" AlternateText="New Fixed Asset Quatation" runat=server Visible="False"/>
						<asp:ImageButton id=NewNUPOBtn UseSubmitBehavior="false" onClick=NewNUPOBtn_Click imageurl="../../images/butt_new_nurseryRPH.gif" AlternateText="New Nursery Quatation" runat=server Visible="False"/>
						<a href="#" onclick="javascript:popwin(200, 400, 'PU_trx_PrintDocs.aspx?doctype=4')"><asp:Image id="ibPrintDoc" runat="server" ImageUrl="../../images/butt_print_doc.gif"/></a>
					</td>
				</tr>
 
                </table>
				</div>
				</td> 
            </tr>

			</table>

		</form>
	</body>
</html>
