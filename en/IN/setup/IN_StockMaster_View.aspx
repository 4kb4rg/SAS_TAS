<%@ Page Language="vb" src="../../../include/IN_Setup_StockMaster_View.aspx.vb" Inherits="IN_StockMaster_View" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINSetup" src="../../menu/menu_INsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Master List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
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
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form class="main-modul-bg-app-list-pu" runat="server">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">  

			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuINSetup id=menuIN runat="server" /></td>
				</tr>
				<tr>
					<td class="font12Tahoma" colspan="4" width=60%> <asp:label id="lblTitle" runat="server" /> LIST</td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   
                            </td>
				</tr>
				<tr class="font9Tahoma">
					<td colspan=6 width=100%  style="background-color:#FFCC00">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="font9Tahoma">
								<td width="20%" height="26" valign=bottom  >
									<asp:label id="lblStockItemCode" CssClass="font9Tahoma" runat="server" /> :<BR>
									<asp:TextBox id=srchStockCode width=100% maxlength="20" CssClass="fontObject" runat="server"/>
								</td>
								<td width="35%" height="26" valign=bottom>
									<asp:label id="lblDescription" CssClass="font9Tahoma" runat="server" /> :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="128" CssClass="fontObject" runat="server"/>
								</td>
								<td class= width="20%" height="26" valign=bottom>Category :<BR><asp:DropDownList id="lstProdCat" width=100% CssClass="fontObject" runat=server /></td>
								<td class= width="10%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% CssClass="fontObject" runat=server /></td>
								<td class= width="10%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% CssClass="fontObject" maxlength="128" runat="server"/></td>
								<td class= width="8%" height="26" valign=bottom align=right><asp:Button  Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>												
				<tr>
					<TD colspan = 6 >					
					<asp:DataGrid id="EventData"
						AutoGenerateColumns="False" width="100%" runat="server"
						GridLines = None
						Cellpadding = "2"
						AllowPaging="True"
						Pagesize="20" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
                        OnItemDataBound="dgLine_BindGrid"
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
						<asp:HyperLinkColumn
								DataNavigateUrlField="ItemCode"
								DataNavigateUrlFormatString="IN_StockMaster_Detail.aspx?id={0}"
								DataTextField="ItemCode"
								DataTextFormatString="{0:c}"
								SortExpression="right(rtrim(ItemCode),5)" Visible=false/>

						<asp:HyperLinkColumn
								DataNavigateUrlField="ItemCode"
								DataNavigateUrlFormatString="IN_StockMaster_Detail.aspx?id={0}"
								DataTextField="Description"
								DataTextFormatString="{0:c}"
								SortExpression="Description" Visible=false/>

						<asp:TemplateColumn HeaderText="ItemCode" SortExpression="itm.ItemCode">
							<ItemTemplate>
								<%#Container.DataItem("ItemCode")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Description">
							<ItemTemplate>
								<%#Container.DataItem("Description")%>
							</ItemTemplate>
						</asp:TemplateColumn>	
												
						<asp:TemplateColumn HeaderText="COA" SortExpression="p.ActCode">
							<ItemTemplate>
								<%#Container.DataItem("ActCode")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Category" SortExpression="p.Description">
							<ItemTemplate>
								<%#Container.DataItem("ProdCatDesc")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Purchase UOM" SortExpression="itm.PurchaseUOM">
							<ItemTemplate>
								<%#Container.DataItem("PurchaseUOM")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Inventory UOM" SortExpression="itm.UOMCode">
							<ItemTemplate>
								<%#Container.DataItem("UOMCode")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Last Update" SortExpression="itm.UpdateDate">
							<ItemTemplate>
								<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status" SortExpression="itm.Status">
							<ItemTemplate>
								<%# objIN.mtdGetStockItemStatus(Container.DataItem("Status")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
							<ItemTemplate>
								<%# Container.DataItem("UserName") %>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
                        <PagerStyle Visible="False" />
					</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
						<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
						<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
						<asp:Label id=lblPageCount visible=false text=1 runat=server/>
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
