<%@ Page Language="vb" src="../../../include/PU_trx_Pelimpahan.aspx.vb" Inherits="PU_PelimpahanList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Pelimpahan List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmPelimpahanList runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>	


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
							<td><strong>PELIMPAHAN LIST</strong><hr style="width :100%" />   
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
								    <td width="20%" height="26">Pelimpahan ID :<BR><asp:TextBox id=SrchPelimpahanID width=100% maxlength="20" runat="server" /></td>
								<td width="20%" height="26">PR ID :<BR><asp:TextBox id=SrchPRId width=100% maxlength="32" runat="server" /></td>	
								<td width="15%" height="26">Pelimpahan :<BR><asp:DropDownList id="ddlPelimpahanSrchType" width=100% runat=server /></td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected=true>Active</asp:ListItem>
										<asp:ListItem value="2">Confirmed</asp:ListItem>
										<asp:ListItem value="3">Cancelled</asp:ListItem>
										<asp:ListItem value="4">Closed</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26">Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
						            <asp:DataGrid id="dgPelimpahanList"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines = none
							Cellpadding = "2"
							OnEditCommand="DEDR_Save"
							OnUpdateCommand="DEDR_Confirm"
							OnCancelCommand="DEDR_Cancel"
							OnDeleteCommand="DEDR_Delete"
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
						<asp:TemplateColumn HeaderText="Pelimpahan ID" SortExpression="PelimpahanID" ItemStyle-Width="20%">
							<ItemTemplate>
								<%# Container.DataItem("PelimpahanID") %>
								<asp:label id=lblPelimpahanID visible=false text='<%# Container.DataItem("PelimpahanID") %>' runat=server/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:label id=lblPelimpahanID visible=false text='<%# Container.DataItem("PelimpahanID") %>' runat=server/>
							</EditItemTemplate>
							
						</asp:TemplateColumn>	
						<asp:TemplateColumn HeaderText="PR ID" SortExpression="PRID" ItemStyle-Width="20%">
							<ItemTemplate>
								<%# Container.DataItem("PRID") %>
								<asp:label id=lblPRID visible=false text='<%# Container.DataItem("PRID") %>' runat=server/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList id="ddlPRID" width=100% runat=server>
								</asp:DropDownList>
								
								<asp:Label id=lblErrPRID visible=false text ="Please select PR ID" forecolor=red runat=server/>	
							</EditItemTemplate>
						</asp:TemplateColumn>					
						<asp:TemplateColumn HeaderText="Pelimpahan" SortExpression="PelimpahanType" ItemStyle-Width="20%">
							<ItemTemplate>
								<%# Container.DataItem("PelimpahanType") %>
								<asp:label id=lblPelimpahanType visible=false text='<%# Container.DataItem("PelimpahanType") %>' runat=server/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList id="ddlPelimpahanType" width=100% runat=server/>
								
								<asp:RequiredFieldValidator id=rfvPelimpahanType colspan=4 display=dynamic runat=server 
										ErrorMessage="Please Select Pelimpahan Type"
										ControlToValidate="ddlPelimpahanType" />
							</EditItemTemplate>
						</asp:TemplateColumn>					
						<asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate" ItemStyle-Width="10%">
							<ItemTemplate>
								<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							</ItemTemplate>
							<EditItemTemplate >
								<asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
									Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>' runat="server"/>
								<asp:TextBox id="CreateDate" Visible=False
									Text='<%# Container.DataItem("CreateDate") %>' runat="server"/>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status" ItemStyle-Width="8%">
							<ItemTemplate>
								<%# objPU.mtdGetPelimpahanStatus(Container.DataItem("Status"))%>
								<asp:label id=lblStatus visible=false text='<%# Container.DataItem("Status") %>' runat=server/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
								<asp:TextBox id="Status" Readonly=TRUE Visible=False
									Text='<%# Container.DataItem("Status")%>' runat="server"/>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Updated By" SortExpression="A.UpdateID" ItemStyle-Width="12%">
							<ItemTemplate>
								<%# Container.DataItem("UpdateID") %>
							</ItemTemplate>
							<EditItemTemplate >
								<asp:TextBox id="UpdateID" Readonly=TRUE size=8 
									Text='<%# Session("SS_USERID") %>'	Visible=False runat="server"/>
							</EditItemTemplate>
						</asp:TemplateColumn>					
						<asp:TemplateColumn ItemStyle-Width="15%" ItemStyle-HorizontalAlign=center>					
							<ItemTemplate>
								<asp:LinkButton id="Confirm" CommandName="Update" Text="Confirm" runat="server"/>
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Cancel" runat="server"/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:LinkButton id="Save" CommandName="Edit" Text="Save" runat="server"/>
								<asp:LinkButton id="Confirm" CommandName="Update" Text="Confirm" runat="server"/>	
								<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
							</EditItemTemplate>
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
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Pelimpahan ID" runat="server"/>
			        			<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
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
