<%@ Page Language="vb" trace=false src="../../../include/GL_trx_BudgetProd_Karet_Estate_list.aspx.vb" Inherits="GL_trx_BudgetProd_Karet_Estate_list" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Budget List</title>
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id="SortExpression" visible="false" runat="server" />
			<asp:Label id="SortCol" visible="false" runat="server" />
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
				 
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>BUDGET KARET LIST</strong><hr style="width :100%" />   
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
								    <td width="14%" height="26">Acc Year :<BR><asp:TextBox id=srchAccYear width=100% maxlength="20" runat="server" /></td>
								    <td width="24%">
                                        Blok:<BR><asp:TextBox id="srchVehCode" 
                                            width="83%" maxlength="8" runat="server" /></td>							
								     
								    <td width="20%" height="26">
                                       <asp:Button id=Button1 Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								    <td width="15%"></td>
                                    <td width="15%"></td>
								    <td width="10%"valign=bottom align=right></td>
                                </tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgList"
							            AutoGenerateColumns="false" width="100%" runat="server"
							            GridLines="none" 
							            Cellpadding="2" 
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
								            <asp:HyperLinkColumn HeaderText="Acc Year" 
									            DataNavigateUrlField="Idx" 
									            DataNavigateUrlFormatString="GL_trx_BudgetProd_Karet_Estate_Det.aspx?Idx={0}" 
									            DataTextField="AccYear" />
								            <asp:TemplateColumn HeaderText="Blok" >
									            <ItemTemplate>
										            <%#Container.DataItem("CodeSubBlok")%>
									            </ItemTemplate>
								            </asp:TemplateColumn>																													
								            <asp:TemplateColumn HeaderText="Create Date" >
									            <ItemTemplate>
										            <%#objGlobal.GetLongDate(Container.DataItem("CreateDate"))%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Create By" >
									            <ItemTemplate>
										            <%#Container.DataItem("UpdateID")%>
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
					            <asp:ImageButton id="NewBtn" onClick="NewBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Budget" runat="server"/>
				        		<asp:ImageButton id="ibPrint" imageurl="../../images/butt_print.gif" AlternateText="Print" visible="false" runat="server"/>
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
</html>
