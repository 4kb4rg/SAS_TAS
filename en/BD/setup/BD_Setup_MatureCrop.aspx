<%@ Page Language="vb" trace="false"  SmartNavigation="True" src="../../../include/BD_setup_MatureCrop.aspx.vb" Inherits="BD_MatureCrop_Format" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mature Crop Activities</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id="lblCode" text=" Code" Visible = false Runat="server"/>

<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDsetup id=menuBD runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id=lblTitle runat=server /></strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
					        <td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					        
				        </tr>
				        <tr>
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					         
				        </tr>
				        <tr>
					        <td colspan="4" width=60%>&nbsp;<hr style="width :100%" /></td>
					        
				        </tr>


				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						 
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgMatureCropSetup"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
						<Columns>
							<asp:TemplateColumn HeaderText ="No." HeaderStyle-Width="4%" ItemStyle-Width="4%">
								<ItemTemplate>
									<asp:Label id="lblIdx" runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblIdx" width=100% runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Seq." HeaderStyle-Width="4%" ItemStyle-Width="4%">
								<ItemTemplate>
									<asp:Label id="lblSeq" Text='<%# trim(Container.DataItem("DispSeq")) %>' runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtDispSeq" MaxLength="4" width=100%
										Text='<%# trim(Container.DataItem("DispSeq")) %>'
										runat="server"/>
									<asp:Label id="lblSeq" Text="Sequence Exist !" forecolor="red" Visible=false  runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn HeaderStyle-Width="15%" ItemStyle-Width="15%">
								<ItemTemplate>
									<asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> runat="server" /><BR>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> visible=false runat="server" /><BR>
									<asp:DropDownList id="ddlAccCode" width=100% runat=server />
									<asp:RequiredFieldValidator id=validateAcc display=dynamic runat=server 
										text="Please select Account Code"	
										ControlToValidate=ddlAccCode />
								</EditItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn HeaderText="Item" HeaderStyle-Width="29%" ItemStyle-Width="29%">
								<ItemTemplate>
									<%# Container.DataItem("Description") %>
									<%# trim(Container.DataItem("ItemDescription")) %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblTxID" Text=<%# trim(Container.DataItem("MatureCropSetID")) %> visible=false runat="server" />
									<asp:TextBox id="txtItemDesc" MaxLength="128" 
										Text='<%# trim(Container.DataItem("ItemDescription")) %>'
										runat="server"/>
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Display Type" HeaderStyle-Width="10%" ItemStyle-Width="10%">
								<ItemTemplate>
									<%# objBD.mtdGetFormatItem(Container.DataItem("ItemDisplayType")) %>
									<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
									<asp:DropDownList id="ddlDispType" AutoPostback=True OnSelectedIndexChanged=ddlCheckType width=100% runat=server />
									<asp:RequiredFieldValidator id=validateDisp display=dynamic runat=server 
										text="Please select display type"	
										ControlToValidate=ddlDispType />
								</EditItemTemplate>
							</asp:TemplateColumn>
			
							<asp:TemplateColumn HeaderText ="Reference" HeaderStyle-Width="5%" ItemStyle-Width="5%">
								<ItemTemplate>
									<%# Container.DataItem("FormulaRef").trim %>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtFormulaRef" text=<%# Trim(Container.DataItem("FormulaRef")) %> MaxLength="8" width=100% runat="server" />
									<asp:Label id="lblRef" Text="reference duplicated !" forecolor="red" Visible=false  runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Formula" HeaderStyle-Width="18%" ItemStyle-Width="18%">
								<ItemTemplate>
									<asp:Label id="lblForm" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %>  visible=False runat="server" />
									<asp:Label id="lblForm1" runat="server" />
									<asp:Label id="lblForm2" runat="server" />
									<asp:Label id="lblForm3" runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:TextBox id="txtFormula" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %> MaxLength="1024" width=100% visible=false runat="server" /> 
									<asp:TextBox id="txtFormula1" MaxLength="256" Tooltip="Others" width=95% visible=false runat="server" /> 
									<asp:TextBox id="txtFormula2" MaxLength="256" Tooltip="Material" width=95% visible=false runat="server" />
									<asp:TextBox id="txtFormula3" MaxLength="256" Tooltip="Labour" width=95% visible=false runat="server" />
								</EditItemTemplate>
							</asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText ="Display Column" HeaderStyle-Width="10%" ItemStyle-Width="10%">
								<ItemTemplate>
									<asp:Label id="lblColName" text=<%# objBD.mtdGetItemColumn(Container.DataItem("ItemDisplayCol")) %> runat="server" />
									<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false  runat="server" />
								</ItemTemplate>
								<EditItemTemplate>
									<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false runat="server" />
									<asp:DropDownList id="ddlDispCol" width=100% runat=server />
									<asp:RequiredFieldValidator id=validateDispCol display=dynamic runat=server 
										text="Please select display type"	ControlToValidate=ddlDispType />
								</EditItemTemplate>
							</asp:TemplateColumn>
										
							<asp:TemplateColumn HeaderStyle-Width="5%" ItemStyle-Width="5%" >
								<ItemTemplate>
									<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
								</ItemTemplate>						
								<EditItemTemplate>
									<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
									<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
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
							<td>
							    &nbsp;</td>
						</tr>
						 
						<tr>
							<td>
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Mature Crop Activity" runat="server"/>
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
