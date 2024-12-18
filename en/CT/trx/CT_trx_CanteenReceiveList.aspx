<%@ Page Language="vb" src="../../../include/CT_Trx_CanteenReceiveList.aspx.vb" Inherits="CT_Receive" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="../../menu/menu_CTTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Canteen Receive List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id="lblErrMessage" Visible="False" Text="Error while initiating component." Runat="server"/>
			<asp:Label id="SQLStatement" Visible="False" Runat="server"/>
			<asp:Label id="SortExpression" Visible="False" Runat="server"/>
			<asp:Label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:Label id="curStatus" Visible="False" Runat="server"/>
			<asp:Label id="SortCol" Visible="False" Runat="server"/>

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuCTTrx id=menuCT runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>CANTEEN RECEIVE LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Canteen Receive ID :<BR><asp:TextBox id=srchCanteenTxID width=100% maxlength="20" runat="server"/></td>
								<td width="20%" height="26">Canteen Receive Ref. No. :<BR><asp:TextBox id=srchRef width=100% maxlength="32" runat="server"/></td>
								<td width="20%" height="26">Canteen Receive Type :<BR><asp:DropDownList id=srchCanRcvType width=100% runat="server"/></td>
								<td width="10%" height="26">Status :<BR><asp:DropDownList id=srchStatusList width=100% runat=server/></td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgCanteenReceive"
							            AutoGenerateColumns="false" width="100%" runat="server"
							            OnDeleteCommand="DEDR_Delete"
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
									            HeaderText="Canteen Receive ID"
									            DataNavigateUrlField="CanteenRcvID"
									            DataNavigateUrlFormatString="CT_Trx_CanteenReceiveDet.aspx?id={0}"
									            DataTextField="CanteenRcvID"
									            DataTextFormatString="{0:c}"
									            SortExpression="CanteenRcvID"/>
	
								            <asp:HyperLinkColumn
									            HeaderText="Canteen Receive Ref. No."
									            DataNavigateUrlField="CanteenRcvID"
									            DataNavigateUrlFormatString="CT_Trx_CanteenReceiveDet.aspx?id={0}"
									            DataTextField="CanteenRefNo"
									            DataTextFormatString="{0:c}"
									            SortExpression="CanteenRefNo"/>
								            <asp:TemplateColumn HeaderText="Canteen Receive Type" SortExpression="tx.CanteenDocType">
									            <ItemTemplate>
										            <%# objCTtx.mtdGetCanteenReceiveDocType(Container.DataItem("CanteenDocType")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
						
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="tx.UpdateDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>							
								            <asp:TemplateColumn HeaderText="Status" SortExpression="tx.Status">
									            <ItemTemplate>
										            <%# objCTtx.mtdGetCanteenReceiveStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>							
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>						
								            <asp:TemplateColumn>
									            <ItemTemplate>
										            <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" Runat="server"/>
										            <asp:Label id="lblTxID" Text=<%# Container.DataItem("CanteenRcvID") %> Visible="False" Runat="server"></asp:label>
										            <asp:Label id="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" Runat="server"></asp:label>
									            </ItemTemplate>
								            </asp:TemplateColumn>
							            </Columns>
						            </asp:DataGrid>
						            <br><asp:label id=lblUnDel text="Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" />
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
					            <asp:ImageButton id=ibNew imageurl="../../images/butt_new.gif" AlternateText="New Canteen Receive" OnClick = btnNewItm_Click runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            <asp:Button ID="ibNew2" class="button-small" runat="server" Text="New Canteen Receive"  />&nbsp;
                            
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
