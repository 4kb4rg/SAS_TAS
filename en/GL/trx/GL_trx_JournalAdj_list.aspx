<%@ Page Language="vb" Trace="False" src="../../../include/GL_Trx_JournalAdj_List.aspx.vb" Inherits="GL_JournalAdj_List" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Journal Adjustment List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
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
							<td><strong>JOURNAL ADJUSTMENT LIST</strong><hr style="width :100%" />   
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
					 
								<td width="20%" height="26">Journal Adjustment ID :<BR><asp:TextBox id=srchJournalAdjId width=100% maxlength="32" runat="server"/></td>
								<td width="20%" height="26">Description :<BR><asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/></td>
								<td width="20%" height="26">Transaction Type :<BR><asp:DropDownList id=srchJournalAdjType width=100% runat="server"/></td>
								<td width="20%" height="26">Accounting Period :<BR><asp:TextBox id=srchAccPeriod width=100% maxlength="7" runat="server"/><asp:Label id=lblErrAccPeriod Text="Accounting period format is MM/YYYY" Forecolor=Red Visible=False Runat=Server /></td>
								<td width="10%" height="26">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id="SearchBtn" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
							 
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					<asp:DataGrid id="dgResult"
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
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>					
					<Columns>
						<asp:HyperLinkColumn
								HeaderText="Journal Adjustment ID"
								DataNavigateUrlField="JournalAdjID"
								DataNavigateUrlFormatString="GL_Trx_JournalAdj_Details.aspx?id={0}"
								DataTextField="JournalAdjID"
								DataTextFormatString="{0:c}"
								SortExpression="JournalAdjID"/>
						<asp:TemplateColumn HeaderText="Description" SortExpression="Description">
							<ItemTemplate>
								<%# Container.DataItem("Description")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Journal Type" SortExpression="TransactType">
							<ItemTemplate>
								<%# objGLTrx.mtdGetJournalTransactType(Container.DataItem("TransactType"))%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Accounting Period" SortExpression="AccPeriod">
							<ItemTemplate>
								<%# Container.DataItem("AccPeriod") %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
							<ItemTemplate>
								<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
							<ItemTemplate>
								<%# objGLTrx.mtdGetJournalAdjStatus(Container.DataItem("Status")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Document Amount" SortExpression="DocAmt" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							<ItemTemplate>
								<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("DocAmt")) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
								<asp:label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid><BR>
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
						        <asp:ImageButton id=Usage UseSubmitBehavior="false" OnClick=btnNewItm_Click imageurl="../../images/butt_new.gif" AlternateText="New Journal Adjustment Entry" runat="server"/>
						        <asp:ImageButton id=ibPrint UseSubmitBehavior="false" AlternateText=Print imageurl="../../images/butt_print.gif" visible=false runat="server" />
						        <BR><asp:Label id=lblErrMaxPeriod Forecolor=Red Visible=False Runat=Server />
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
