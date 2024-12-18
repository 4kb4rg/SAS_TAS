<%@ Page Language="vb" src="../../../include/PR_trx_ContractCheckrollList.aspx.vb" Inherits="PR_trx_ContractCheckrollList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contractor Checkroll List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id=frmMain runat=server  class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />



<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>CONTRACTOR CHECKROLL LIST</strong><hr style="width :100%" />   
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
								<td width="12%" valign=top>Supplier Code :<BR><asp:TextBox id=txtSuppCode width=100% maxlength="8" runat="server" /></td>
								<td width="20%" valign=top>Name :<BR><asp:TextBox id=txtName width=100% maxlength="128" runat="server"/></td>
								<td width="20%" valign=top align=right>Attendance Date <BR>
									From :<asp:TextBox id=txtFromAttdDate width=50% maxlength="10" runat="server"/>
										<a href="javascript:PopCal('txtFromAttdDate');">
											<asp:Image id="btnAttdDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/>
										</a>
										<BR>To :<asp:TextBox id=txtToAttdDate width=50% maxlength="10" runat="server"/>
										<a href="javascript:PopCal('txtToAttdDate');">
											<asp:Image id="btnAttdDateTo" runat="server" ImageUrl="../../Images/calendar.gif"/>
										</a>																			
								</td>
								<td width="15%" valign=top>Attendance Code :<BR><asp:TextBox id=txtAttdCode width=100% maxlength="8" runat="server"/></td>
								<td width="10%" valign=top>Status :<BR><asp:DropDownList id="ddlStatus" width=100% runat=server /></td>
								<td width="15%" valign=top>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>


                        <tr>
							<td  >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
                                    <asp:label id=lblErrAttdDate Text ="Date entered should be in the format " forecolor=red visible=false runat="server"/>
									<asp:label id=lblDateFormat forecolor=red visible=false runat=server />
									<asp:Label id=lblErrFromAttdDate visible=false text="From Attendance Date cannot be blank." forecolor=red runat=server />
									<asp:Label id=lblErrToAttdDate visible=false text="To Attendance Date cannot be blank." forecolor=red runat=server />									
                                 </tr>
							</table>
							</td>
						</tr>


						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgContCheckList
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            OnDeleteCommand=DEDR_Delete 							
							            Pagerstyle-Visible=False 
							            OnSortCommand=Sort_Grid  
							            AllowSorting=True
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="Supplier Code" DataField="SupplierCode" />

								            <asp:HyperLinkColumn HeaderText="Supplier Code" 
									            SortExpression="CA.SupplierCode" 
									            DataNavigateUrlField="AttID" 
									            HeaderStyle-Width="15%"									
									            ItemStyle-Width="15%"									
									            DataNavigateUrlFormatString="PR_trx_ContractCheckrollDet.aspx?AttdID={0}" 
									            DataTextField="SupplierCode" />
									
								            <asp:HyperLinkColumn HeaderText="Name" 
									            SortExpression="SUP.Name" 
									            HeaderStyle-Width="25%"									
									            ItemStyle-Width="25%"									
									            DataNavigateUrlField="AttID" 
									            DataNavigateUrlFormatString="PR_trx_ContractCheckrollDet.aspx?AttdID={0}" 
									            DataTextField="Name" />
									
								            <asp:TemplateColumn HeaderText="Attendance Date" HeaderStyle-Width="15%" ItemStyle-Width="15%" SortExpression="CA.AttDate">
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("AttDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
																	
								            <asp:TemplateColumn HeaderText="Attendance Code" HeaderStyle-Width="15%" ItemStyle-Width="15%" SortExpression="CA.AttCode">
									            <ItemTemplate>
										            <%# Container.DataItem("AttCode") %>
										            <asp:Label id="lblAttdID" visible="False" Text='<%# trim(Container.DataItem("AttID")) %>' runat="server"/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Status" HeaderStyle-Width="15%" ItemStyle-Width="15%" SortExpression="CA.Status">
									            <ItemTemplate>
										            <%# objPRTrx.mtdGetContractCheckrollStatus(Container.DataItem("Status")) %>
										            <asp:Label id="lblStatus" visible="False" Text='<%# trim(Container.DataItem("Status")) %>' runat="server"/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Last Updated By" HeaderStyle-Width="15%" ItemStyle-Width="15%" SortExpression="USR.UserName">
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn ItemStyle-Width="10%" >
									            <ItemTemplate>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
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
					            <asp:ImageButton id=NewSuppBtn onClick=NewSuppBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Contractor Checkroll" runat=server/>
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
