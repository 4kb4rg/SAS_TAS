<%@ Page Language="vb" trace="false" src="../../../include/PM_trx_ProducedKernelQuality_List.aspx.vb" Inherits="PM_ProducedKernelQualityList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>PRODUCED KERNEL QUALITY TRANSACTION</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id=MenuPDTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>PRODUCED KERNEL QUALITY LIST</strong><hr style="width :100%" />   
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
								<td width="25%"> Transaction Date : <BR>
									<asp:TextBox id="txtdate" runat="server" width=50% maxlength="10"/>                       
						 												
									<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
									<asp:label id=lblFmt forecolor=red Visible = false Runat="server"/> 
									<asp:label id="lblDupMsg" Text="This transaction already exist" Visible=false forecolor=red Runat="server"/>								
								</td>                        								
								<td width="45%">&nbsp;</td>						
								<!--<td width="10%" height="26"><BR><asp:DropDownList id="srchStatusList" visible="false" width=100% runat=server /></td>-->
								<td width="20%">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
						            <asp:DataGrid id=dgProducedKernelQualityList
							                AutoGenerateColumns=false width=100% runat=server
							                GridLines=none 
							                Cellpadding=2 
							                AllowPaging=True 
							                Allowcustompaging=False 
							                Pagesize=16 
							                OnPageIndexChanged=OnPageChanged 
							                Pagerstyle-Visible=False 
							                OnDeleteCommand=DEDR_Delete
							                OnEditCommand=DEDR_Edit 							
							                OnSortCommand=Sort_Grid  
							                AllowSorting=True
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							                <Columns>
								                <asp:TemplateColumn HeaderText="Transaction Date"   >
									                <ItemTemplate>				
                                                        <%# objGlobal.GetLongDate(Container.DataItem("DateProd")) %>						
										                <%--<asp:LinkButton id="Edit" CommandName="Edit" Text='<%# objGlobal.GetLongDate(Container.DataItem("DateProd")) %>' runat="server"/>--%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
																				 
								                <asp:TemplateColumn HeaderText="CPO"  >
									                <ItemTemplate>
										                <%# Container.DataItem("CPO")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>

								                <asp:TemplateColumn HeaderText="CPO <br> STANDARD (%)"  >
									                <ItemTemplate>
										                <%# Container.DataItem("StdCPO")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>

								                <asp:TemplateColumn HeaderText="CPO <br> QUALITI (%)"  >
									                <ItemTemplate>
										                <%# FormatNumber( Container.DataItem("NilaiCPO"),2)%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
                                                                                                                                                								 
								
								                <asp:TemplateColumn HeaderText="KERNEL" ItemStyle-BackColor="HighlightText"   >
									                <ItemTemplate>
										                <%# Container.DataItem("PK")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>

								                <asp:TemplateColumn HeaderText="KERNEL <br> STANDARD (%)" ItemStyle-BackColor="HighlightText"  >
									                <ItemTemplate>
										                <%# Container.DataItem("sTDPK")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>

								                <asp:TemplateColumn HeaderText="QUALITY <br> KERNEL (%)" ItemStyle-BackColor="HighlightText"  >
									                <ItemTemplate>
										                <%# FormatNumber(Container.DataItem("NilaiPK"), 2)%>
									                </ItemTemplate>
								                </asp:TemplateColumn>

								                <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>
									                <ItemTemplate>
										                <asp:Label id=lblLocCode visible=false text='<%# Container.DataItem("LocCode") %>' runat=server/>																		
										                <asp:Label id=lblTransDate visible=false text='<%# Container.DataItem("DateProd") %>' runat=server/>										
										                <asp:LinkButton id="Delete" CommandName="Delete" Text="" runat="server"/>
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
					            <asp:ImageButton id=NewBtn onClick=NewBtn_Click 
                                    imageurl="../../images/butt_new.gif" AlternateText="New" runat=server 
                                    Visible="False"/>
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
