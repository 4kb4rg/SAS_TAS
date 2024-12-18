<%@ Page Language="vb" trace="False" src="../../../include/BD_trx_CropDist_Details.aspx.vb" Inherits="BD_CropDist_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>CROP DISTRIBUTION</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblCode" Visible="False" text=" Code" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<Input type=hidden id=hidBlkCode runat=server />
			<Input type=hidden id=hidDistByBlk runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDtrx id=menuDB runat="server" />
					</td>
				</tr>
			    <tr>
                    <td>
                    <table cellpadding="0" cellspacing="0" style="width: 20px">
				    <td style="width: 100%; height: 800px" valign="top">
				    <div class="kontenlist">
					    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
				    <tr>
					    <td class="mt-h" colspan="4" width=60%><asp:Label id="lblTitle" runat="server" /></td>
					    <td align="right" colspan="2" width=40%>&nbsp;</td>
				    </tr>
				    <tr>
					    <td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					    <td align="right" colspan="2" width=40%>&nbsp;</td>
				    </tr>
				    <tr>
					    <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					    <td align="right" colspan="2" width=40%>&nbsp;</td>
				    </tr>
				    <tr>
					    <td colspan="4" width=60%><asp:label id="lblBlockTag" runat="server"/> : <asp:label id="lblBlkCode" runat="server"/></td>
					    <td align="right" colspan="2" width=40%></td>
				    </tr>	
				    <tr id="RowSubBlk">
					    <td colspan="4" width=60%><asp:label id="lblSubBlkTag" runat="server"/> : <asp:label id="lblSubBlkCode" runat="server" /></td>
					    <td align="right" colspan="2" width=40%>&nbsp;</td>
				    </tr>				
				    <tr id="RowPlantingYr">
					    <td colspan="4" width=60%><asp:label id="lblyrTag" text="Planting Year" runat="server"/> : <asp:label id="lblYear" runat="server"/></td>
					    <td align="right" colspan="2" width=40%>&nbsp;</td>
				    </tr>
				    <tr>
					    <td colspan=6><hr size="1" noshade></td>
				    </tr>
				    <tr>
					    <td ColSpan=6>
						    <asp:label id="lblOvrMsgTop" text='Number too big' Visible=False Forecolor=Red Runat="server" />
						    <asp:Label id=lblProdErrTop Text="Production Budgeting Not Complete!" Forecolor=Red Visible=False runat=server />
					    </td>
				    </tr>
						<tr>

						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
					                <asp:DataGrid id="dgCropDist"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
                                        	                class="font9Tahoma">
								
						                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
						                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
						                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					                <Columns>
					
					                <asp:TemplateColumn HeaderText="Month" >
						                <ItemStyle Width="20%" />
						                <ItemTemplate>
							                <%# Container.DataItem("GLAccMonth") %> / <%# Container.DataItem("GLAccYear") %> 
						                </ItemTemplate>
					                </asp:TemplateColumn>
	
					                <asp:TemplateColumn HeaderText="Area Harvested" >
						                <ItemStyle Width="20%" />
						                <ItemTemplate>
							                <%# objGlobal.GetIDDecimalSeparator(formatNumber(Container.DataItem("Area"), 0)) %>
						                </ItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Percentage" >
						                <ItemStyle Width="20%" />
						                <ItemTemplate>
							                <%# objGlobal.GetIDDecimalSeparator(Formatnumber(Container.DataItem("Distribution"),0)) %>
						                </ItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Weight" >
						                <ItemStyle Width="20%" />
						                <ItemTemplate>
							                <%# objGlobal.GetIDDecimalSeparator(Formatnumber(Container.DataItem("Yield"),0)) %>
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
						        <table id="Table1" width="100%" cellspacing="0" cellpadding="3" border="0" align="center" runat=server>
							        <tr class="mb-t">
								        <td width="20%" height="26" valign=bottom>
									        <B>TOTAL</B>
								        </td>
								        <td width="18%" height="26" valign=bottom>
									        <B><asp:Label id=lblAreaHarvested width=100% runat="server"/></B>
								        </td>
								        <td width="10%" height="26" valign=bottom>
									        &nbsp;								
								        </td>
								        <td id=CellPercent width="26%" height="26" valign=bottom>
									        <B><asp:Label id=lblPercent width=100% runat="server"/></B>
								        </td>
								        <td width="26%" height="26" valign=bottom>
									        <B><asp:Label id=lblTtlWeight width=100%  runat="server"/></B>
								        </td>
							         </tr>
						        </table>
                            </td>
				        </tr>
				        <tr>
					        <td align="left" ColSpan=5>&nbsp;</td>
					        <td height="26" align=right>
						        <asp:Button id=ibDist Text="Distribute" OnClick=DistBtn_Click runat="server"/>
					        </td>
				        </tr>
				        <tr>
					        <td align="left" ColSpan=6>
						        <asp:label id="lblOvrMsg" text='Number too big' Visible=False Forecolor=Red Runat="server" />
						        <asp:Label id=lblProdErr Text="Production Budgeting Not Complete !" Forecolor=Red Visible=False runat=server />
						        <!--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>-->
					        </td>
				        </tr>
					</table>
				</div>
				</td>
                    </table>
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
