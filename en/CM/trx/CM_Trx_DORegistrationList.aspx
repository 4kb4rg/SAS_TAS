<%@ Page Language="vb" src="../../../include/CM_Trx_DORegistrationList.aspx.vb" Inherits="CM_Trx_DORegistrationList" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>DO Registration List</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding=1 width="100%">
				<tr>
					<td colspan="6"><UserControl:menu_CM_Trx id=menu_CM_Trx runat="server" /></td>
				</tr>

			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
				<table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">

				<tr>
					<td class="font9Tahoma" colspan="3"><strong> DO REGISTRATION LIST</strong></td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="font9Tahoma">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr style="background-color:#FFCC00">
								<td valign=top width="15%" height="26">DO No :<BR>
									<asp:TextBox id=txtDONo width=100% maxlength="50" CssClass="fontObject" runat="server" /></td>
								<td valign=top width="25%" height="26">Contract No :<BR>
									<asp:TextBox id="txtContractNo" width=100% CssClass="fontObject" runat=server/></td>
								<td valign=top width=8%>Period :<BR>
								    <asp:DropDownList id="lstAccMonth" width=100% CssClass="fontObject" runat=server>
										<asp:ListItem value="0" Selected>All</asp:ListItem>
										<asp:ListItem value="1">1</asp:ListItem>
										<asp:ListItem value="2">2</asp:ListItem>										
										<asp:ListItem value="3">3</asp:ListItem>
										<asp:ListItem value="4">4</asp:ListItem>
										<asp:ListItem value="5">5</asp:ListItem>
										<asp:ListItem value="6">6</asp:ListItem>
										<asp:ListItem value="7">7</asp:ListItem>
										<asp:ListItem value="8">8</asp:ListItem>
										<asp:ListItem value="9">9</asp:ListItem>
										<asp:ListItem value="10">10</asp:ListItem>
										<asp:ListItem value="11">11</asp:ListItem>
										<asp:ListItem value="12">12</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td valign=top width=10%><BR>
								    <asp:DropDownList id="lstAccYear" width=100% CssClass="fontObject" runat=server>
									</asp:DropDownList>
								</td>
								<td valign=top width="10%" height="26">Status :<br>
									<asp:DropDownList id="ddlStatus" width=100% CssClass="fontObject" runat=server/>
								</td>
								<td valign=top width="10%" height="26">Last Updated By :<br>
									<asp:TextBox id="txtLastUpdate" width=100% CssClass="fontObject" runat=server/>
								</td>
								<td width="5%" valign=bottom height="26" align=right>
									<asp:Button id="SearchBtn" Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/>
								</td>
							</tr>
							<tr>
								<td valign=top colspan=8>
									<asp:label id=lblErrCtrDate Text ="Date entered should be in the format " 
										forecolor=red visible=false runat="server"/>
									<asp:label id=lblDateFormat forecolor=red visible=false runat=server />
									<asp:label id=lblErrDelvPeriod Text ="Invalid format of Delivery Period. " 
										forecolor=red visible=false runat="server"/>  
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgLine 
							runat=server
							AutoGenerateColumns=false 
							width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid  
                            OnItemDataBound="dgLine_BindGrid" 
							AllowSorting=True>
								
							<HeaderStyle CssClass="mr-h" />
							<ItemStyle CssClass="mr-l" />
							<AlternatingItemStyle CssClass="mr-r" />
							
							<Columns>
								
								<asp:TemplateColumn HeaderText="DO No.">
								  <ItemTemplate>
									 <asp:Hyperlink runat= "server" Text='<%# DataBinder.Eval(Container.DataItem,"DONo").tostring%>' 
									  NavigateUrl='<%# "CM_Trx_DORegistrationDet.aspx?tbcode=" & DataBinder.Eval (Container.DataItem,"DONo").tostring & _   
									  "&tbCtrNo=" & DataBinder.Eval(Container.DataItem,"ContractNo").tostring %>' ID="DONo"/>   
									 </ItemTemplate>
								 </asp:TemplateColumn> 
								 
								<asp:TemplateColumn HeaderText="Contract No.">
								  <ItemTemplate>
									 <asp:Hyperlink runat= "server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractNo").tostring%>' 
									  NavigateUrl='<%# "CM_Trx_DORegistrationDet.aspx?tbcode=" & DataBinder.Eval (Container.DataItem,"DONo").tostring & _   
									  "&tbCtrNo=" & DataBinder.Eval(Container.DataItem,"ContractNo").tostring %>' ID="ContractNo"/>   
									 </ItemTemplate>
								 </asp:TemplateColumn>    
								
								<asp:TemplateColumn HeaderText="DO Date" ItemStyle-Width="10%" SortExpression="cdo.DODate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("DODate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
                                <asp:TemplateColumn HeaderText="Customer" ItemStyle-Width="15%" SortExpression="BillPartyCode">
	                                <ItemTemplate>
		                                <%#Container.DataItem("BillPartyName")%>
	                                </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Transporter" ItemStyle-Width="15%" SortExpression="TransporterCode">
	                                <ItemTemplate>
		                                <%#Container.DataItem("TransporterName")%>
	                                </ItemTemplate>
                                </asp:TemplateColumn>
                                									
								<asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="cdo.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" SortExpression="cdo.Status">
									<ItemTemplate>
										<%# objCMTrx.mtdGetContractStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" SortExpression="usr.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										<asp:Label id=lblDONo Visible=False text=<%# Container.DataItem("DONo") %> Runat="server" />
										<asp:label id=lblContractNo visible=false text=<%# Container.DataItem("ContractNo") %> runat=server />
										<asp:label id=lblUpdateDate visible=false text=<%# Container.DataItem("UpdateDate") %> runat=server />
										<asp:label id=lblStatus visible=false text=<%# Container.DataItem("Status") %> runat=server />
										<asp:label id=lblUpdateID visible=false text=<%# Container.DataItem("UpdateID") %> runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" CssClass="fontObject" runat="server"
							AutoPostBack="True" 
							onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=NewTBBtn OnClick="NewTBBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Contract Registration" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible="false" runat="server"/>
						<asp:Label id=SortCol Visible=False Runat="server" />
					</td>
				</tr>
                </table>
                </div>
            </td>
            </tr>
			</table>
		</FORM>
	</body>
</html>
