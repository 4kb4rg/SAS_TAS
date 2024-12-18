<%@ Page Language="vb" Trace="False" src="../../../include/GL_Trx_PostJournal_List.aspx.vb" Inherits="GL_PostJournal_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Post Journal List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	<body>
		<form runat="server">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>

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
							<td><strong>POST JOURNAL LIST</strong><hr style="width :100%" />   
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
						<td width="18%" height="26">Journal ID :<BR><asp:TextBox id=srchStockTxID width=100% maxlength="8" runat="server"/></td>
								<td width="20%" height="26">Description :<BR><asp:TextBox id=srchDesc width=100% maxlength="8" runat="server"/></td>
								<td width="20%" height="26">Trans. Type :<BR><asp:DropDownList id="srchTxTypeList" width=100% runat="server"/></td>
								<td width="20%" height="26">Ref No :<BR><asp:TextBox id=srchRefNo width=100% maxlength="8" runat="server"/></td>
								<td valign=bottom width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% runat=server>
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
								<td valign=bottom width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% runat=server>
									</asp:DropDownList>
								<td width="10%" height="26" align=right>&nbsp;</td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button Text="Search" id="SearchBtn" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					<asp:DataGrid id="dgTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						AllowPaging="True" 
						Allowcustompaging="False"
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"
						class="font9Tahoma">
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
						<asp:TemplateColumn HeaderText="Journal ID" SortExpression="JournalID">
							<ItemTemplate>
								<%# Container.DataItem("JournalID")%>
							</ItemTemplate>
						</asp:TemplateColumn>
  						<asp:TemplateColumn HeaderText="Journal Description" SortExpression="Description">
							<ItemTemplate>
								<%# Container.DataItem("Description")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Trans Type" SortExpression="TransactType">
							<ItemTemplate>
								<%# objGLtx.mtdGetJournalTransactType(Container.DataItem("TransactType"))%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Doc Ref No." SortExpression="RefNo">
							<ItemTemplate>
								<%# Container.DataItem("RefNo")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Amount" SortExpression="DocAmt" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							<ItemTemplate>
								<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DocAmt"), 2), 2)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							<ItemTemplate>
								<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid>
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
						        <asp:ImageButton id=postJr UseSubmitBehavior="false" OnClick=btnPost_Click imageurl="../../images/butt_post_journal.gif" AlternateText="Post Journal" runat="server"/>&nbsp;
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" AlternateText=Print imageurl="../../images/butt_print.gif" visible=false runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
				        <tr>
					        <td align="left" colspan="6"> <asp:label id="lblErrMsg" forecolor="red" Visible="False" Runat="server"></asp:label> </td>
				        </tr>
				        <tr>
					        <td colspan=6 width="100%">
						        <asp:DataGrid id="dgErr"
							        AutoGenerateColumns="false" width="100%" runat="server"
							        OnItemDataBound="dgErr_ItemCreated"
							        GridLines = none
							        Cellpadding = "2"
							        AllowPaging="False" 
							        Allowcustompaging="False"
                                    class="font9Tahoma"
							        Pagerstyle-Visible="False" >
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
								        <asp:TemplateColumn HeaderText="Journal ID">
									        <HeaderStyle HorizontalAlign="Left" />
									        <ItemStyle width="10%" HorizontalAlign="Left" />
									        <ItemTemplate>
										        <%# Container.DataItem("JournalID")%>
									        </ItemTemplate>
								        </asp:TemplateColumn>
  								        <asp:TemplateColumn HeaderText="Reason">
  									        <HeaderStyle HorizontalAlign="Left" />
									        <ItemStyle width="90%" HorizontalAlign="Left" />
									        <ItemTemplate>
										        <asp:label text=<%# Container.DataItem("Reason") %> id="lblReason" runat="server" />
									        </ItemTemplate>
								        </asp:TemplateColumn>
							        </Columns>
						        </asp:DataGrid>
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

		</form>
	</body>
</html>
