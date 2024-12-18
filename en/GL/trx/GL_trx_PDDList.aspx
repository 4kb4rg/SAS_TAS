<%@ Page Language="vb" src="../../../include/GL_trx_PDDList.aspx.vb" Inherits="GL_trx_PDDList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Transfer List</title>
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SQLStatement" Visible="False" Runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			
			<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuGLTrx id=menuGL runat="server" />
					</td>
				</tr>
			    <tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
				<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>Permintaan Droping Dana  LIST</strong><hr style="width :100%" />   
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
								<td valign=top width=8% style="height: 26">Period :<BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList><td width="15%" valign=top style="height: 52px"></td>
								<td width="20%" valign=top style="height: 26"></td>
								<td valign=middle align=right style="height: 26; width: 14%;"><asp:Button  Text="Search" CssClass="button-small" OnClick=srchBtn_Click runat="server" ID="Button1"/></td>
							</tr>
							</table>
					</td>
				</tr>												
				<tr>
					<TD colspan=6 style="height: 317px" valign="top">					
					<asp:DataGrid id="dgPDD"
						AutoGenerateColumns="False" width="100%" runat="server"
						GridLines = None
						Cellpadding = "2"
						AllowPaging="True"
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />		
						 
						<Columns>
							<asp:HyperLinkColumn
									HeaderText="PDD ID"
									DataNavigateUrlField="PDDID"
									DataNavigateUrlFormatString="GL_trx_PDDDet.aspx?id={0}"
									DataTextField="PDDID"
									DataTextFormatString="{0:c}"
									SortExpression="PDDID"/>
							<asp:TemplateColumn HeaderText="Tgl.PDD">
								<ItemTemplate>
									<%# objGlobal.GetLongDate(Container.DataItem("PDDDate")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:BoundColumn DataField="PDOID" HeaderText="PDO ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AccMonth" HeaderText="Month"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AccYear" HeaderText="Year"></asp:BoundColumn>
							
							<asp:TemplateColumn HeaderText="Last Update" SortExpression="tx.UpdateDate">
								<ItemTemplate>
									<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Status">
								<ItemTemplate>
									<%# objGLtx.mtdGetJournalStatus(Container.DataItem("Status")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Approved By">
								<ItemTemplate>
									<%#Container.DataItem("UpdateID")%>
									<asp:label id="lblTxID" Text=<%# Container.DataItem("PDDID") %> Visible="False" Runat="server"></asp:label>
									<asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Next Approved">
								<ItemTemplate>
									<%#Container.DataItem("UpdateID")%>
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
                        <PagerStyle Visible="False" />
					</asp:DataGrid>&nbsp;<br />
                        <asp:label id=lblUnDel text="PDO ID Not Found!" Visible=False forecolor=red Runat="server" />
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
							<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
							<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         		<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" /></td>
				</tr>
                <tr>
                    <td align="right" colspan="6">
                    </td>
                </tr>
				<tr>
					<td align="left" width="80%" ColSpan=6>
                        &nbsp;<asp:ImageButton id=ibNew UseSubmitBehavior="false" imageurl="../../images/butt_new.gif" OnClick=btnNewItm_Click AlternateText="New PDO" runat=server/>
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