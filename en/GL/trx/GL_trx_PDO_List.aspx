<%@ Page Language="vb" src="../../../include/GL_trx_PDO_List.aspx.vb" Inherits="GL_trx_PDO_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Transfer List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SQLStatement" Visible="False" Runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						 
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>PERMINTAAN DANA OPERASIONAL (PDO) LIST</strong><hr style="width :100%" />   
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
								<td valign=top width=8% style="height: 52px">Period :<BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList><td width="15%" valign=top style="height: 52px"></td>
								<td width="20%" valign=top style="height: 52px"></td>
								<td valign=middle align=right style="height: 52px; width: 14%;"><asp:Button  Text="Search" OnClick=srchBtn_Click runat="server" ID="Button1" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					<asp:DataGrid id="dgPDO"
						AutoGenerateColumns="False" width="100%" runat="server"
						OnDeleteCommand="DEDR_Delete"
						GridLines = None
						Cellpadding = "2"
						AllowPaging="True"
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
							            AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>	
						 
						<Columns>
							<asp:HyperLinkColumn
									HeaderText="PDO ID"
									DataNavigateUrlField="TransactionID"
									DataNavigateUrlFormatString="GL_trx_PDODet.aspx?id={0}"
									DataTextField="TransactionID"
									DataTextFormatString="{0:c}"
									SortExpression="TransactionID"/>
                            <asp:BoundColumn DataField="AccMonth" HeaderText="Month"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AccYear" HeaderText="Year"></asp:BoundColumn>
							<asp:HyperLinkColumn
									HeaderText="Description"
									DataNavigateUrlField="TransactionID"
									DataNavigateUrlFormatString="GL_trx_PDODet.aspx?id={0}"
									DataTextField="Description"
									DataTextFormatString="{0:c}"
									SortExpression="Description"/>						
							<asp:TemplateColumn HeaderText="Last Update" SortExpression="tx.UpdateDate">
							
								<ItemTemplate>
									<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Status" SortExpression="tx.Status">
								<ItemTemplate>
									<%# objGLtx.mtdGetJournalStatus(Container.DataItem("Status")) %>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
								<ItemTemplate>
									<%#Container.DataItem("UpdateID")%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" Runat="server" Visible="False"/>
									<asp:label id="lblTxID" Text=<%# Container.DataItem("TransactionID") %> Visible="False" Runat="server"></asp:label>
									<asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
                        <PagerStyle Visible="False" />
					</asp:DataGrid>&nbsp;<br />
                        <asp:label id=lblUnDel text="PDO ID Not Found!" Visible=False forecolor=red Runat="server" />
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
                            &nbsp;<asp:ImageButton id=ibNew UseSubmitBehavior="false" imageurl="../../images/butt_new.gif" OnClick=btnNewItm_Click AlternateText="New PDO" runat=server/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
                          
                            </td>
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

		</FORM>
		</body>
</html>
