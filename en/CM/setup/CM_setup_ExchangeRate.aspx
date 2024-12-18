<%@ Page Language="vb" src="../../../include/CM_setup_ExchangeRate.aspx.vb" Inherits="CM_setup_ExchangeRate" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_cm_setup" src="../../menu/menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Exchange Rate</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" class="main-modul-bg-app-list-pu"/>
		<body>
		    <form id="frmMain" runat="server">
			<asp:label id="SortExpression" visible="False" runat="server"></asp:label>
			<asp:label id="blnUpdate" visible="false" runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" visible="False" runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />
			
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:menu_cm_setup id=menu_cm_setup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>EXCHANGE RATE LIST</strong><hr style="width :100%" />   
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
								<td width="18%">Date :<BR>
									<asp:TextBox id=srch1TransDate width=80% maxlength="10" runat="server"/>
									<a href="javascript:PopCal('srch1TransDate');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
									<asp:Label id=lblErrDateMsg visible=false text="<br>Date Format should be in " forecolor=red runat=server/>						
									<asp:Label id=lblErrDate forecolor=red visible=false runat=server/>
								</td>	
								<td width="20%" height="26">First Currency :<br><asp:TextBox id="srch1CurrencyCode" width="100%" maxlength="8" runat="server"/></td>
								<td width="20%" height="26">Second Currency :<br><asp:TextBox id="srch2CurrencyCode" width="100%" maxlength="8" runat="server"/></td>
								<td width="20%" height="26">Status :<br><asp:DropDownList id="srchStatus" width="100%" runat="server"/></td>
								<td width="15%" height="26">Last Updated By :<br><asp:TextBox id="srchUpdBy" width="100%" maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="EventData"
						            AutoGenerateColumns="false" 
						            width="100%" 
						            runat="server"
						            GridLines = none
						            Cellpadding = "2"
						            AllowPaging="True" 
						            Allowcustompaging="False"
						            Pagesize="15" 
						            OnPageIndexChanged="OnPageChanged"
						            Pagerstyle-Visible="False"
						            OnEditCommand=DEDR_Edit
						            OnDeleteCommand="DEDR_Delete"
						            OnSortCommand="Sort_Grid" 
						            AllowSorting="True"
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					            <Columns>	
					                <asp:TemplateColumn HeaderText="Date" SortExpression="TransDate">
							            <ItemTemplate>
								            <asp:LinkButton id="btnTransDate" CommandName=Edit Text=<%# objGlobal.GetLongDate(Container.DataItem("TransDate")) %>
									            runat="server"/>	
							            </ItemTemplate>
						            </asp:TemplateColumn>	
						            <asp:TemplateColumn HeaderText="First Currency" SortExpression="FirstCurrencyCode">
							            <ItemTemplate>
								            <asp:LinkButton id="btnFirstCurrency" CommandName=Edit Text=<%# Container.DataItem("FirstCurrencyCode") %>
									            runat="server"/>	
							            </ItemTemplate>
						            </asp:TemplateColumn>
						
					            <asp:TemplateColumn HeaderText="Second Currency" SortExpression="SecCurrencyCode">
							            <ItemTemplate>
								            <asp:LinkButton id="btnSecCurrency" CommandName=Edit Text=<%# Container.DataItem("SecCurrencyCode") %>
									            runat="server"/>	
							            </ItemTemplate>
						            </asp:TemplateColumn>
						
					            <asp:TemplateColumn HeaderText="Exchange Rate" ItemStyle-Width="15%" SortExpression="exc.ExchangeRate">
						            <ItemTemplate>
								            <asp:LinkButton id="btnExchangeRate" CommandName=Edit Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("ExchangeRate"),2) %>
									            runat="server"/>	
							            </ItemTemplate>		
					            </asp:TemplateColumn>					
					            <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="15%" SortExpression="exc.UpdateDate">
						            <ItemTemplate>
							            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						            </ItemTemplate>
					            </asp:TemplateColumn>
					            <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="15%" SortExpression="exc.Status">
						            <ItemTemplate>
							            <%# objCMSetup.mtdGetExchangeRateStatus(Container.DataItem("Status")) %>
						            </ItemTemplate>
					            </asp:TemplateColumn>
					            <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="15%" SortExpression="usr.UserName">
						            <ItemTemplate>
							            <%# Container.DataItem("UserName") %>
						            </ItemTemplate>
					            </asp:TemplateColumn>					
					            <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>					
						            <ItemTemplate>
							            <asp:LinkButton id=lblDelete CommandName=Delete Text=Delete runat=server/>
							            <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Exchange Rate" runat="server"/>
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
