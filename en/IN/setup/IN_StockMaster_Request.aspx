<%@ Page Language="vb" src="../../../include/IN_StockMaster_Request.aspx.vb" Inherits="IN_StockMaster_Request" %>
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
}
        </style>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
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
					<td colspan="2"><UserControl:MenuINSetup id=menuIN runat="server" /></td>
				</tr>
				<tr>
					<td width=60%>
                     <strong>  &nbsp;REQUEST STOCK MASTER</strong> </td>
					<td align="right" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td  colspan="2" width=60% style="width: 100%">
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=2 style="height: 36px"><asp:label id="lblTitle" runat="server" Visible="False" /></td>
				</tr>
				<tr>
					<td colspan=2 width=100% class="font9Tahoma" style="height: 48px">
						<table width="100%" class="font9Tahoma" cellspacing="0" cellpadding="3" border="0" align="center" style="height: 1px">
							<tr >
								<td valign=bottom style="height: 26px; width: 6%;">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td valign=bottom style="height: 26px; width: 7%;">
                                    Tahun :<BR>
                                    <asp:DropDownList id="lstAccYear" width=100% runat=server /></td>
                                <td style="width: 20%; height: 26px" valign="bottom">
                                    Bulan :<BR>
                                    <asp:DropDownList id="lstAccMonth" width="40%" runat=server /></td>
                                <td style="width: 20%; height: 26px" valign="bottom">
                                </td>
								<td width="8%" valign=bottom align=right style="height: 26px"><asp:Button  Text="Search" OnClick=srchBtn_Click runat="server" ID="Button1" class="button-small"/></td>
							</tr>
						</table>
					</td>
				</tr>												
				<tr>
					<TD colspan = 2 >					
                        <asp:DataGrid ID="EventData" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="None" 
                            OnPageIndexChanged="OnPageChanged"
                            OnSortCommand="Sort_Grid" 
                            OnItemDataBound="dgLine_BindGrid" 
                            PagerStyle-Visible="False" 
                            PageSize="15" Width="100%" CssClass="font9Tahoma">
                            <PagerStyle Visible="False" />
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
                                <asp:TemplateColumn HeaderText="StockItem Code" SortExpression="ItemCode">
                                     <ItemTemplate>                                       
                                         <asp:Label ID="LblItemCode" runat="server" Text=<%#Container.DataItem("ItemCode")%>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:HyperLinkColumn DataNavigateUrlField="ItemCode" DataNavigateUrlFormatString="IN_StockMaster_Detail.aspx?id={0}"
                                    DataTextField="Description" DataTextFormatString="{0:c}" SortExpression="ItemCode" Text="Deskripsi">
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataNavigateUrlField="NoDoc" DataNavigateUrlFormatString="IN_StockMaster_Request_Detail.aspx?id={0}"
                                    DataTextField="NoDoc" DataTextFormatString="{0:c}" SortExpression="NoDoc" Text="No Document" HeaderText="No Document">
                                </asp:HyperLinkColumn>
                                <asp:TemplateColumn HeaderText="No.Doc" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNodoc" runat="server" Text=<%#Container.DataItem("NoDoc")%> ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Category" SortExpression="p.Description" Visible="False">
                                    <ItemTemplate>
                                        <%#Container.DataItem("ProdCatDesc")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Inventory UOM" SortExpression="itm.UOMCode">
                                    <ItemTemplate>
								<%#Container.DataItem("UOMCode")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Create Date" SortExpression="itm.CreateDate">
                                     <ItemTemplate>
                                        <%#Format(Container.DataItem("CreateDate"), "dd-MMM-yyyy HH:mm:ss")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last Update" SortExpression="itm.UpdateDate">
                                    <ItemTemplate>
                                         <%#Format(Container.DataItem("UpdateDate"), "dd-MMM-yyyy HH:mm:ss")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status" SortExpression="itm.Status">
                                    <ItemTemplate>
                                                                
                                        <asp:Label ID="lblstatus" runat="server" Text= <%#objCBTrx.mtdGetCashBankStatus(Container.DataItem("Status"))%>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Create By" SortExpression="UserName">
                                    <ItemTemplate>
                                        <%# Container.DataItem("UserName") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Confirmed By" SortExpression="ApprovedBy">
                                    <ItemTemplate>
                                        <%#Container.DataItem("ApprovedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnCreateItem" runat="server" Font-Size="XX-Small" OnClick="BtnCreateItem_Click"
                                            Text="Create Item" Width="66px" class="button-small"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle CssClass="mr-h" />
                        </asp:DataGrid><BR>
                        <asp:Label ID="lblErrMesage" runat="server" ForeColor="red" Text="Error while initiating component."
                            Visible="false"></asp:Label></td>
				</tr>
				<tr>
					<td align=right colspan="2">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
						<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
						<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
						<asp:Label id=lblPageCount visible=false text=1 runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=2>
						<asp:ImageButton id=ibNew OnClick="btnNewItm_Click" imageurl="../../images/butt_new.gif" AlternateText="New Stock Master" runat="server"/>&nbsp;
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
