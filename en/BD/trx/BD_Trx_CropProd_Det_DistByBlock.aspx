<%@ Page Language="vb" trace="False" SmartNavigation="True" src="../../../include/BD_trx_CropProd_Det_DistByBlock.aspx.vb" Inherits="BD_CropProd_Det_DistByBlock" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>CROP PRODUCTION</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
		<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	
		<body onload="this.focus()" >
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblBlock visible=false runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />

<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPU id=menuPU runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:Label id="lblTitle" runat="server" /></strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%>Planting Year : <asp:label id="lblYear" Runat="server" /></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan=6><hr size="1" noshade></td>
                            </table></td>
				        </tr>
				        <!--<tr>
					        <td width=15%>&nbsp;</td>
					        <td width=15%>&nbsp;</td>
					        <td colspan="3" align="Center" width=55% > ------------------------------------- YIELD PER AREA ------------------------------------- </td>
					        <td width=15%>&nbsp;</td>
				        </tr>-->
						 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgCropProdByBlk"
						            AutoGenerateColumns="false" width="100%" runat="server"
						            GridLines = both
						            Cellpadding = "2"
						            OnEditCommand="DEDR_Edit"
						            OnUpdateCommand="DEDR_Update"
						            OnCancelCommand="DEDR_Cancel"
						            OnSortCommand="Sort_Grid" 
						            AllowSorting="True"
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					            <Columns>
					
					            <asp:TemplateColumn SortExpression="BlkCode">
						            <ItemStyle Width="10%" />
						            <ItemTemplate>
							            <%# Container.DataItem("BlkCode") %>
							            <asp:label id="lblBlkTotalArea" visible=false text=<%# Container.DataItem("TotalArea") %> Runat="server" />
						            </ItemTemplate>
						            <EditItemTemplate>
							            <asp:label id="lblBlkCode" Text =<%# Trim(Container.DataItem("BlkCode")) %> runat="server"/>
							            <asp:label id="lblBlkTotalArea" visible=false text=<%# Container.DataItem("TotalArea") %> Runat="server" />
						            </EditItemTemplate>
					            </asp:TemplateColumn>
				
					            <asp:TemplateColumn HeaderText="Forecasted (B)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="ForecastYield">
						            <ItemStyle Width="8%" />
						            <ItemTemplate>
							            <asp:label id="lblForecastYield" text=<%# formatNumber(Container.DataItem("ForecastYield"),2) %> Runat="server" />
						            </ItemTemplate>
						            <EditItemTemplate>							
							            <asp:TextBox id="txtCurr" width=100% MaxLength="25"
								            Text='<%# formatNumber(Container.DataItem("ForecastYield"),2) %>'
								            runat="server"/>
							            <asp:RequiredFieldValidator id=validateCurr display=dynamic runat=server 
									            text = "Please enter budgeted units"
									            ControlToValidate=txtCurr />															
							            <asp:RegularExpressionValidator id="RegExpValCurr" 
								            ControlToValidate="txtCurr"
								            ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								            Display="Dynamic"
								            text = "Maximum length 15 digits and 5 decimal points"
								            runat="server"/>
							            <asp:RangeValidator id="RangeCurr"
								            ControlToValidate="txtCurr"
								            MinimumValue="0"
								            MaximumValue="999999999999999"
								            Type="double"
								            EnableClientScript="True"
								            Text="The value is out of range !"
								            runat="server" display="dynamic"/>
					            </EditItemTemplate>
					            </asp:TemplateColumn>
								
					            <asp:TemplateColumn HeaderText="Budgeted Next Period" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right SortExpression="YieldPerArea">
						            <ItemStyle Width="8%" />
						            <ItemTemplate>
							            <asp:label id="lblBGTYield" text=<%# formatNumber(Container.DataItem("YieldPerArea"),2) %> Runat="server"></asp:label>
						            </ItemTemplate>
						            <EditItemTemplate>
							            <asp:TextBox id="txtNextYield" width=100% MaxLength="25"
								            Text='<%# trim(Container.DataItem("YieldPerArea")) %>'
								            runat="server"/>
							            <asp:RequiredFieldValidator id=validateNext display=dynamic runat=server 
									            text = "Please enter budgeted units"
									            ControlToValidate=txtNextYield />															
							            <asp:RegularExpressionValidator id="RegExpValNext" 
								            ControlToValidate="txtNextYield"
								            ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								            Display="Dynamic"
								            text = "Maximum length 15 digits and 5 decimal points"
								            runat="server"/>
							            <asp:RangeValidator id="RangeNext"
								            ControlToValidate="txtNextYield"
								            MinimumValue="0"
								            MaximumValue="999999999999999"
								            Type="double"
								            EnableClientScript="True"
								            Text="The value is out of range !"
								            runat="server" display="dynamic"/>
					            </EditItemTemplate>
					            </asp:TemplateColumn>
									
					            <asp:TemplateColumn>
						            <ItemStyle Width="9%" horizontalalign=right />					
						            <ItemTemplate>
							            <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						            </ItemTemplate>						
						            <EditItemTemplate>						
							            <asp:LinkButton id="Update" CommandName="Update" Text="Save & Next"	runat="server"/>
							            <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>							
						            </EditItemTemplate>
					            </asp:TemplateColumn>

					            </Columns>
					            </asp:DataGrid>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
					<td align="left" ColSpan=6>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="10%" height="26" valign=bottom>
									<B>TOTAL</B>
								</td>
								<td width="5%" height="26" valign=bottom>
									&nbsp;
								</td>
								<td width="5%" height="26" valign=bottom>
									&nbsp;
								</td>
								<!--<td width="5%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblAvrgstand width=100%  runat="server"/></B>
								</td>
								<td width="8%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblPlanted width=100% runat="server"/></B>
								</td>
								<td width="8%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblHist1 width=100% runat="server"/></B>
								</td>
								<td width="7%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblHist2 width=100% runat="server"/></B>
								</td>
								<td width="8%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblCurr width=100% runat="server"/></B>
								</td>-->
								<td width="8%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblForecast width=100% runat="server"/></B>
								</td>
								<!--<td width="10%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblYield width=100% runat="server"/></B>
								</td>-->
								<td width="8%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblBGT width=100% runat="server"/></B>
								</td>
								<!--<td width="10%" align=right height="26" valign=bottom>
									<B><asp:Label id=lblFFB width=100% runat="server"/></B>
								</td>-->
								<td width="10%" align=right height="26" valign=bottom>&nbsp;</td>
							</tr>
						</table>
						<asp:label id="lblOvrMsg" text='Number too big' Visible=False Forecolor=Red Runat="server" />
					</td>
				</tr>
				<tr>
					<td ColSpan=6>&nbsp;</td>
				</tr>
						 
						<tr>
							<td>
					            <asp:ImageButton id=BackBtn AlternateText="  Back  "  onclick=BackBtn_Click imageurl="../../images/butt_back.gif" CausesValidation=False runat=server />
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
