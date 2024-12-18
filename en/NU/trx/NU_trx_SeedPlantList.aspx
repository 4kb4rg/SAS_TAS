<%@ Page Language="vb" src="../../../include/NU_trx_SeedPlantList.aspx.vb" Inherits="NU_trx_SeedPlantList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUTrx" src="../../menu/menu_NUtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>NURSERY - Seeds Planting List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmTrx runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuNUTrx id=MenuNUTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>SEEDS PLANTING LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26" valign=bottom>Planting ID :<BR><asp:TextBox id=srchTxID width=100% maxlength="20" runat="server"/></td>
								<td width="20%" height="26" valign=bottom><asp:label id="lblNurseryBlock" runat="server"/> :<BR><asp:TextBox id=srchBlkCode width=100% maxlength="8" runat="server"/></td>
								<td width="15%" height="26" valign=bottom><asp:label id="lblBatchNo" runat="server"/> :<BR><asp:TextBox id=srchBatchNo width=100% maxlength="8" runat="server"/></td>
								<td width="15%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
									            HeaderText="Planting ID"
									            SortExpression="PlantID" 
									            DataNavigateUrlField="PlantID"
									            DataNavigateUrlFormatString="NU_Trx_SeedPlantDetails.aspx?id={0}"
									            DataTextField="PlantID"
									            DataTextFormatString="{0:c}"
									            ItemStyle-Width= "15%" /> 
								            <asp:TemplateColumn HeaderText="Nursery Block" SortExpression="BlkCode">
								            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# Container.DataItem("BlkCode") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Batch No" SortExpression="BatchNo">
								            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# Container.DataItem("BatchNo") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Period" SortExpression="AccYear, AccMonth">
								            <ItemStyle Width="5%" />
									            <ItemTemplate>
										            <%# Container.DataItem("AccMonth").Trim & "/" & Container.DataItem("AccYear").Trim %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Date" >
								            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("PlantDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            
											<asp:TemplateColumn HeaderText="Qty" >
								            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# Container.DataItem("Qty")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="SP.UpdateDate">
								            <ItemStyle Width="12%" />
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Status" SortExpression="SP.Status">
								            <ItemStyle Width="10%" />
									            <ItemTemplate>
										            <%# objNUTrx.mtdGetSeedPlantStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="SP.UpdateID">
								            <ItemStyle Width="15%" />
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
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
					            <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
				        		<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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
