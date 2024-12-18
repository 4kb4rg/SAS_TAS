<%@ Page Language="vb" Inherits="PM_WastedWaterQualityList" src="../../../include/PM_trx_WasteWaterQuality_List.aspx.vb"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>WASTED WATER QUALITY TRANSACTION</title> 
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id="PrefHdl" runat="server" />--%>
	</HEAD>
	<body>
		<form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id="SortExpression" visible="false" runat="server" />
			<asp:Label id="SortCol" visible="false" runat="server" />
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id="MenuPDTrx" runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>WASTED WATER QUALITY LIST</strong><hr style="width :100%" />   
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
								<td width="25%">
									Transaction Date :
									<BR>
									<asp:TextBox id="txtdate" runat="server" width="50%" maxlength="10" />
									<a href="javascript:PopCal('txtdate');">
										<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
									<asp:label id="lblDate" Text="<br>Date Entered should be in the format " forecolor="red" Visible="false"
										Runat="server" />
									<asp:label id="lblFmt" forecolor="red" Visible="false" Runat="server" />
									<asp:label id="lblDupMsg" Text="This transaction already exist" Visible="false" forecolor="red"
										Runat="server" />
								</td>
								<td width="20%">Test Sample :<BR>
									<asp:TextBox id="txtTestSample" width="100%" runat="server" maxlength="8" /></td>
								<td width="25%">Pond No&nbsp;:<BR>
									<asp:DropDownList id="srchPondNo" width="100%" runat="server" /></td>
								<!--<td width="15%">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>-->
								<td width="20%">Last Updated By :<BR>
									<asp:TextBox id="srchUpdateBy" width="100%" maxlength="128" runat="server" /></td>
								<td width="10%" height="26" valign="bottom" align="right"><asp:Button id="SearchBtn" Text="Search" OnClick="srchBtn_Click" runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgWasteWQList" AutoGenerateColumns="False" width="100%" runat="server" GridLines="None"
							            Cellpadding="2" AllowPaging="True" Pagesize="15" OnPageIndexChanged="OnPageChanged" Pagerstyle-Visible="False"
							            OnEditCommand="DEDR_Edit" OnSortCommand="Sort_Grid" AllowSorting="True"
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:TemplateColumn SortExpression="TransDate" HeaderText="Transaction Date">
									            <ItemStyle Width="10%"></ItemStyle>
									            <ItemTemplate>
										            <asp:LinkButton id=Edit runat="server" Text='<%# objGlobal.GetLongDate(Container.DataItem("TransDate")) %>' CommandName="Edit">
										            </asp:LinkButton>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn SortExpression="TestSampleCode" HeaderText="Test Sample">
									            <ItemStyle Width="23%"></ItemStyle>
									            <ItemTemplate>
										            <%#Container.DataItem("TestSample") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn SortExpression="PondNo" HeaderText="Pond No">
									            <ItemStyle Width="35%"></ItemStyle>
									            <ItemTemplate>
										            <%#Container.DataItem("PondNoDesc") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn SortExpression="UpdateDate" HeaderText="Last Update">
									            <ItemStyle Width="10%"></ItemStyle>
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									            <ItemStyle Width="10%"></ItemStyle>
									            <ItemTemplate>
										            <%# objPMTrx.mtdGetDailyProdStatus(Container.DataItem("Status")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn SortExpression="UpdateID" HeaderText="Updated By">
									            <ItemStyle Width="20%"></ItemStyle>
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn>
									            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									            <ItemTemplate>
										            <asp:Label id=lblLocCode visible=false text='<%# Container.DataItem("LocCode") %>' runat=server/>
										            <asp:Label id=lblTransDate visible=false text='<%# Container.DataItem("TransDate") %>' runat=server/>
										            <asp:Label id=lblTestSample visible=false text='<%# Container.DataItem("TestSampleCode") %>' runat=server/>
										            <asp:Label id="lblPondNo" visible=false text='<%# Container.DataItem("PondNo") %>' runat=server/>
										            <asp:LinkButton id="Delete" CommandName="Delete" Text="" runat="server" />
									            </ItemTemplate>
								            </asp:TemplateColumn>
							            </Columns>
							            <PagerStyle Visible="False"></PagerStyle>
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
					              <asp:ImageButton id="NewBtn" onClick="NewBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New"
							        runat="server" />
						        <asp:ImageButton id="ibPrint" imageurl="../../images/butt_print.gif" AlternateText="Print" visible="false"
							        runat="server" />
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


		</form>
	</body>
</HTML>
