<%@ Page Language="vb" src="../../../include/FA_trx_AssetAddList.aspx.vb" Inherits="FA_trx_AssetAddList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFATrx" src="../../menu/menu_FATrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>FIXED ASSET - Asset Addition List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id=frmTrx runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuFATrx id=MenuFATrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom>
									<asp:label id="lblTxID" runat="server" /> :<BR>
									<asp:TextBox id=srchTxID width=100% maxlength="20" runat="server"/>
								</td>
								<td width="25%" height="26" valign=bottom>
									<asp:label id="lblAssetCode" runat="server" /> :<BR>
									<asp:TextBox id=srchAssetCode width=100% maxlength="50" runat="server"/>
								</td>
								<td width="18%" height="26">Period :<BR>
									<asp:TextBox id="srchAccMonth" width=35% runat=server />/
									<asp:TextBox id="srchAccYear" width=55% runat=server />
								</td>
								<td width="12%" height="26">Status :<BR>
									<asp:DropDownList id="srchStatusList" width=100% runat=server />
								</td>
								<td width="15%" height="26">Last Update By :<BR>
									<asp:TextBox id=srchUpdateBy width=100% maxlength="10" runat="server"/>
								</td>
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
						            <asp:DataGrid id=dgTxList
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
							AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							<Columns>
								<asp:HyperLinkColumn
									SortExpression="AssetAddID" 
									DataNavigateUrlField="AssetAddID"
									DataNavigateUrlFormatString="FA_trx_AssetAddDetails.aspx?Id={0}"
									DataTextField="AssetAddID"
									DataTextFormatString="{0:c}"
									ItemStyle-Width= "20%" /> 
								<asp:TemplateColumn SortExpression="LN.Description">
								<ItemStyle Width="40%" />
									<ItemTemplate>
										<%#Container.DataItem("Description")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Period" SortExpression="AccYear, AccMonth">
								<ItemStyle Width="10%" />
									<ItemTemplate>
										<%# Container.DataItem("AccMonth").Trim & "/" & Container.DataItem("AccYear").Trim %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="FA.UpdateDate">
								<ItemStyle Width="10%" />
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" SortExpression="FA.Status">
								<ItemStyle Width="10%" />
									<ItemTemplate>
										<%# objFATrx.mtdGetAssetAddStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="FA.UpdateID">
								<ItemStyle Width="10%" />
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
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
						        <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</tr>
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
