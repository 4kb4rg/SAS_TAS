<%@ Page Language="vb" trace="False"  SmartNavigation="True" src="../../../include/BD_setup_Overhead.aspx.vb" Inherits="BD_Overhead_Format" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Plantation Overheads</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lbloper" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />

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
							<td><strong><asp:label id=lblTitle text="PLANTATION OVERHEADS" runat=server /></strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
					        <td colspan="4" width=60%><asp:label id="lblLocTag" text="Location" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					         
				        </tr>
				        <tr>
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					        
				        </tr>
				        <tr>
					        <td colspan="4" width=60%>&nbsp;<hr style="width :100%" />   </td>
					        
				        </tr>
						 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="OHSetup"
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
						<asp:TemplateColumn HeaderText="No." ItemStyle-Width="4%">
							<ItemTemplate>
								<asp:Label id="lblIdx" runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblIdx" runat="server" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Seq." ItemStyle-Width="4%" >
							<ItemTemplate>
								<asp:Label id="lblSeq" Text='<%# trim(Container.DataItem("DispSeq")) %>' runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtDispSeq" MaxLength="4" width=90%
									Text='<%# trim(Container.DataItem("DispSeq")) %>'
									runat="server"/>
								<asp:Label id="lblSeq" Text="Sequence Exist !" forecolor="red" Visible=false  runat="server" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn ItemStyle-Width="15%">
							<ItemTemplate>
								<asp:Label id="lblAcc" Text=<%# Container.DataItem("AccCode") %> runat="server" /><BR>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> visible=false runat="server" />
								<asp:DropDownList id="ddlAccCode" width=90% runat=server />
								<asp:RequiredFieldValidator id=validateAcc display=dynamic runat=server 
									text="Please select Account Code" ControlToValidate=ddlAccCode />
							</EditItemTemplate>
						</asp:TemplateColumn>				

						<asp:TemplateColumn HeaderText="Item" ItemStyle-Width="29%">
							<ItemTemplate>
								<%# Container.DataItem("Description") %>
								<%# trim(Container.DataItem("ItemDescription")) %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblTxID" Text=<%# trim(Container.DataItem("OverheadSetID")) %> visible=false runat="server" />
								<asp:TextBox id="txtItemDesc" MaxLength="128" 
									Text='<%# trim(Container.DataItem("ItemDescription")) %>'
									runat="server"/>
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Display Type" ItemStyle-Width="10%" >
							<ItemTemplate>
								<%# objBD.mtdGetFormatItem(Container.DataItem("ItemDisplayType")) %>
								<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblDisp" Text=<%# Container.DataItem("ItemDisplayType") %> Visible=false  runat="server" />
								<asp:DropDownList id="ddlDispType" AutoPostback=True OnSelectedIndexChanged=ddlCheckType runat=server />
								<asp:RequiredFieldValidator id=validateDisp display=dynamic runat=server 
									text="Please select display type" ControlToValidate=ddlDispType />
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Reference" ItemStyle-Width="5%">
							<ItemTemplate>
								<%# Container.DataItem("FormulaRef").trim %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtFormulaRef" text=<%# Trim(Container.DataItem("FormulaRef")) %> MaxLength="8" width=95% runat="server" />
								<asp:Label id="lblRef" Text="reference duplicated !" forecolor="red" Visible=false  runat="server" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Formula" ItemStyle-Width="18%">
							<ItemTemplate>
								<asp:Label id="lblForm" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %>  visible=False runat="server" />
								<asp:Label id="lblForm1"  runat="server" />
								<asp:Label id="lblForm2"  runat="server" />
								<asp:Label id="lblForm3"  runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtFormula" text=<%# Trim(Container.DataItem("ItemCalcFormula")) %> MaxLength="1024" width=95% visible=false runat="server" /> 
								<asp:TextBox id="txtFormula1"  MaxLength="256" Tooltip="Others" width=95% visible=false runat="server" /> 
								<asp:TextBox id="txtFormula2"  MaxLength="256" Tooltip="Material" width=95% visible=false runat="server" />
								<asp:TextBox id="txtFormula3"  MaxLength="256" Tooltip="Labour" width=95% visible=false runat="server" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="Display Column" ItemStyle-Width="10%">
							<ItemTemplate>
								<asp:Label id="lblColName" text=<%# objBD.mtdGetItemColumn(Container.DataItem("ItemDisplayCol")) %> runat="server" />
								<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblCol" Text=<%# Container.DataItem("ItemDisplayCol") %> Visible=false  runat="server" />
								<asp:DropDownList id="ddlDispCol" runat=server ></asp:DropDownList>
								<asp:RequiredFieldValidator id=validateDispCol display=dynamic runat=server 
									text="Please select display type"	ControlToValidate=ddlDispType />
							</EditItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn ItemStyle-Width="5%" >
							<ItemTemplate>
								<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server" />
							</ItemTemplate>						
							<EditItemTemplate>
								<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Plantation Overhead" runat="server"/>
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
