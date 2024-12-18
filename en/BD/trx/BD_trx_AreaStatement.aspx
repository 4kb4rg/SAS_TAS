<%@ Page Language="vb" trace="False" SmartNavigation="True" src="../../../include/BD_trx_areaStatement.aspx.vb" Inherits="BD_AreaStatement" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDTrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Analysis List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id=lblBudgeting visible=false Text="Budgeting " runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=LocationTag visible=false runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server" />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:Label id=lblCode text=" Code" Visible="False" runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDTrx id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><B>AREA STATEMENT</B></strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="BudgLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%>&nbsp;</td>
					        <td align="right" colspan="2" width=40%>&nbsp;<hr style="width :100%" /></td>
                            </table></td>
				        </tr>
                        <tr>
							<td><B>MATURE AREA</B>   
                            </td>
                            
						</tr>
 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="DGMature"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    OnItemDataBound="DataGrid_ItemDataBound" 
						                    GridLines = 2
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
					
					                    <asp:TemplateColumn HeaderText="Area" >
						                    <ItemStyle Width="13%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("AreaCode") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:DropDownList id="ddlLoccode" runat=server >
							                    </asp:DropDownList>
							                    <asp:label id="lblAreaID" Text='<%# Container.DataItem("AreaID") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaCode" Text='<%# Container.DataItem("AreaCode") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaType" Text='<%# Container.DataItem("AreaType") %>'  Visible = false Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
	
					                    <asp:TemplateColumn HeaderText="Description" >
						                    <ItemStyle Width="18%" />
						                    <ItemTemplate>
							                    <asp:label id="lblDesc" Text='<%# Container.DataItem("Description") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtdesc" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("Description")) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
								                    ControlToValidate=txtdesc />
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Material" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblMat" Text='<%# Container.DataItem("PlantMaterial") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtMat" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("PlantMaterial")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Age Grp.">
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblAge" Text='<%# Container.DataItem("AgeGroup") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtAge" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("AgeGroup")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Height" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblHeight" Text='<%# Container.DataItem("PlantHeight") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtHeight" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("PlantHeight")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Terrain" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblTerrain" Text='<%# Container.DataItem("AreaTerrain") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtTerrain" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("AreaTerrain")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Area Size" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="10%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblSize" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False)) %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtSize" width=100% MaxLength="15"
								                    Text='<%# FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									                    ControlToValidate=txtSize />
							                    <asp:RegularExpressionValidator id="revSize" 
								                    ControlToValidate="txtSize"
								                    ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								                    Display="Dynamic"
								                    text = "Maximum length 9 digits and 5 decimal points"
								                    runat="server"/>
							                    <asp:RangeValidator id="rvSize"
								                    ControlToValidate="txtSize"
								                    MinimumValue="0"
								                    MaximumValue="999999999"
								                    Type="double"
								                    EnableClientScript="True"
								                    Text="The value is out of range !"
								                    runat="server" display="dynamic"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Usage %" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="8%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblpercent" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaPercentage"), 0, True, False, False)) %>' Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
	
					                    <asp:TemplateColumn HeaderText="Last Update">
						                    <ItemStyle Width="12%" horizontalalign=right />
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
						                    <ItemStyle Width="10%" />
						                    <ItemTemplate>
							                    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							                    <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							                    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
							                    <asp:label id="lblAreaERR" Text='<BR>Titled Area size exceeded !' forecolor=red Visible=false Runat="server"/>
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
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td width="68%" align="right">&nbsp;</td>
					        <td width="8%" align="right"><B><asp:label id="lblMatTotal"   runat="server" /></B> </td>						
					        <td width="8%" align="right"><B><asp:label id="lblMatprcnt"   runat="server" /></B></td>
					        <td width="7%">&nbsp;</td>
					        <td width="7%">&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>    
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3>&nbsp;</td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><B>NEW PLANTING & IMMATURE AREA</B></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
                            <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="DGNew"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    ShowHeader = "False"
						                    OnItemDataBound="DataGrid_ItemDataBound" 
						                    GridLines = 2
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
					
					                    <asp:TemplateColumn HeaderText="Area" >
						                    <ItemStyle Width="13%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("AreaCode") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:DropDownList id="ddlLoccode" runat=server >
							                    </asp:DropDownList>
							                    <asp:label id="lblAreaID" Text='<%# Container.DataItem("AreaID") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaCode" Text='<%# Container.DataItem("AreaCode") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaType" Text='<%# Container.DataItem("AreaType") %>'  Visible = false Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
	
					                    <asp:TemplateColumn HeaderText="Description" >
						                    <ItemStyle Width="18%" />
						                    <ItemTemplate>
							                    <asp:label id="lblDesc" Text='<%# Container.DataItem("Description") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtdesc" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("Description")) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
								                    ControlToValidate=txtdesc />
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Material" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblMat" Text='<%# Container.DataItem("PlantMaterial") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtMat" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("PlantMaterial")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Age Grp.">
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblAge" Text='<%# Container.DataItem("AgeGroup") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtAge" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("AgeGroup")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Height" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblHeight" Text='<%# Container.DataItem("PlantHeight") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtHeight" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("PlantHeight")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Terrain" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblTerrain" Text='<%# Container.DataItem("AreaTerrain") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtTerrain" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("AreaTerrain")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Area Size" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="10%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblSize" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False)) %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtSize" width=100% MaxLength="25"
								                    Text='<%# FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									                    ControlToValidate=txtSize />
							                    <asp:RegularExpressionValidator id="revSize" 
								                    ControlToValidate="txtSize"
								                    ValidationExpression="\d{1,19}"
								                    Display="Dynamic"
								                    text = "Maximum length 19 digits and 0 decimal points"
								                    runat="server"/>
							                    <asp:RangeValidator id="rvSize"
								                    ControlToValidate="txtSize"
								                    MinimumValue="0"
								                    MaximumValue="9999999999999999999"
								                    Type="double"
								                    EnableClientScript="True"
								                    Text="The value is out of range !"
								                    runat="server" display="dynamic"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Usage %" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="8%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblpercent" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaPercentage"), 0, True, False, False)) %>' Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Last Update">
						                    <ItemStyle Width="12%"  horizontalalign=right/>
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
						                    <ItemStyle Width="10%" />
						                    <ItemTemplate>
							                    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							                    <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							                    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
							                    <asp:label id="lblAreaERR" Text='<BR>Titled Area size exceeded !' forecolor=red Visible=false Runat="server"/>
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
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right" colspan="1">&nbsp;</td>
					        <td align="right"><B><asp:label id="lblNewTotal" runat="server" /></B>  </td>						
					        <td align="right"><B><asp:label id="lblNewprcnt" runat="server" /></B></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                             </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3><hr size="1" noshade></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                             </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right"><asp:label id="TAmt" text="Planted Area :" runat="server" /></td>
					        <td align="right"><B><asp:label id="lblTtlPlanted"   runat="server" /></B>  </td>						
					        <td align="right"><B><asp:label id="lblPlantedPrcnt"   runat="server" /></B></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3>&nbsp;</td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><B>UNPLANTED AREA</B></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>
                            <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="DGOther"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    ShowHeader = "False"
						                    OnItemDataBound="DataGrid_ItemDataBound" 
						                    GridLines = 2
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
					                    <asp:TemplateColumn HeaderText="Area" >
						                    <ItemStyle Width="13%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("AreaCode") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:DropDownList id="ddlLoccode" runat=server >
							                    </asp:DropDownList>
							                    <asp:label id="lblAreaID" Text='<%# Container.DataItem("AreaID") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaCode" Text='<%# Container.DataItem("AreaCode") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaType" Text='<%# Container.DataItem("AreaType") %>'  Visible = false Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
	
					                    <asp:TemplateColumn HeaderText="Description" >
						                    <ItemStyle Width="18%" />
						                    <ItemTemplate>
							                    <asp:label id="lblDesc" Text='<%# Container.DataItem("Description") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtdesc" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("Description")) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
								                    ControlToValidate=txtdesc />
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Material" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblMat" Text='<%# Container.DataItem("PlantMaterial") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtMat" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("PlantMaterial")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Age Grp.">
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblAge" Text='<%# Container.DataItem("AgeGroup") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtAge" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("AgeGroup")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Height" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblHeight" Text='<%# Container.DataItem("PlantHeight") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtHeight" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("PlantHeight")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Terrain" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblTerrain" Text='<%# Container.DataItem("AreaTerrain") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtTerrain" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("AreaTerrain")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Area Size" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="10%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblSize" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False)) %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtSize" width=100% MaxLength="25"
								                    Text='<%# FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									                    ControlToValidate=txtSize />
							                    <asp:RegularExpressionValidator id="revSize" 
								                    ControlToValidate="txtSize"
								                    ValidationExpression="\d{1,19}"
								                    Display="Dynamic"
								                    text = "Maximum length 19 digits and 0 decimal points"
								                    runat="server"/>
							                    <asp:RangeValidator id="rvSize"
								                    ControlToValidate="txtSize"
								                    MinimumValue="0"
								                    MaximumValue="9999999999999999999"
								                    Type="double"
								                    EnableClientScript="True"
								                    Text="The value is out of range !"
								                    runat="server" display="dynamic"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Usage %" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="8%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblpercent" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaPercentage"), 0, True, False, False)) %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Last Update">
						                    <ItemStyle Width="12%"  horizontalalign=right/>
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
							                    <asp:DropDownList id="ddlUsage" Visible=false runat=server width = 85 AutoPostBack=True OnSelectedIndexChanged="ddlUsage_OnSelectedIndexChanged" ></asp:DropDownList>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
						                    <ItemStyle Width="10%" />
						                    <ItemTemplate>
							                    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							                    <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							                    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
							                    <asp:label id="lblAreaERR" Text='<BR>Titled Area size exceeded !' forecolor=red Visible=false Runat="server"/>
							                    <asp:label id="lblAdjErrMsg" Text='<br>Adjustment area size cannot be zero !' forecolor=red Visible=false Runat="server"/>
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
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right" colspan="1">&nbsp;</td>
					        <td align="right"><B><asp:label id="lblOtherTotal"   runat="server" /></B>  </td>						
					        <td align="right"><B><asp:label id="lblOtherprcnt"   runat="server" /></B></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3><hr size="1" noshade></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right" ><asp:label id="TotalAreaTag" runat="server" /></td>
					        <td align="right"><B><asp:label id="lblTotalArea"   runat="server" /></B>  </td>						
					        <td align="right"><B><asp:label id="lblprcntTotal"   runat="server" /></B></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <!-- #2 Start -->
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3>&nbsp;</td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><B>ADJUSTMENT</B></td>
					        <td align="right" colspan="2" width=40%>&nbsp;</td>
                            </table></td>
				        </tr>        
                         <tr>
							<td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
                                    <asp:DataGrid id="DGAdjustment"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    ShowHeader = "False"
						                    OnItemDataBound="DataGrid_ItemDataBound" 
						                    GridLines = 2
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
					
					                    <asp:TemplateColumn HeaderText="Area" >
						                    <ItemStyle Width="13%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("AreaCode") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:DropDownList id="ddlLoccode" runat=server >
							                    </asp:DropDownList>
							                    <asp:label id="lblAreaID" Text='<%# Container.DataItem("AreaID") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaCode" Text='<%# Container.DataItem("AreaCode") %>'  Visible = false Runat="server"/>
							                    <asp:label id="lblAreaType" Text='<%# Container.DataItem("AreaType") %>'  Visible = false Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
	
					                    <asp:TemplateColumn HeaderText="Description" >
						                    <ItemStyle Width="18%" />
						                    <ItemTemplate>
							                    <asp:label id="lblDesc" Text='<%# Container.DataItem("Description") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtdesc" width=100% MaxLength="32"
								                    Text='<%# trim(Container.DataItem("Description")) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
								                    ControlToValidate=txtdesc />
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Material" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblMat" Text='<%# Container.DataItem("PlantMaterial") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtMat" width=100% MaxLength="32" visible=false
								                    Text='<%# trim(Container.DataItem("PlantMaterial")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Age Grp.">
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblAge" Text='<%# Container.DataItem("AgeGroup") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtAge" width=100% MaxLength="32" visible=false
								                    Text='<%# trim(Container.DataItem("AgeGroup")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Height" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblHeight" Text='<%# Container.DataItem("PlantHeight") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtHeight" width=100% MaxLength="32" visible=false
								                    Text='<%# trim(Container.DataItem("PlantHeight")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Terrain" >
						                    <ItemStyle Width="8%" />
						                    <ItemTemplate>
							                    <asp:label id="lblTerrain" Text='<%# Container.DataItem("AreaTerrain") %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtTerrain" width=100% MaxLength="32" visible=false
								                    Text='<%# trim(Container.DataItem("AreaTerrain")) %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Area Size" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="10%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblSize" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False)) %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtSize" width=100% MaxLength="25"
								                    Text='<%# FormatNumber(Container.DataItem("AreaSize"), 0, True, False, False) %>'
								                    runat="server"/>
							                    <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									                    ControlToValidate=txtSize />
							                    <asp:RegularExpressionValidator id="revSize" 
								                    ControlToValidate="txtSize"
								                    ValidationExpression="\d{1,19}"
								                    Display="Dynamic"
								                    text = "Maximum length 19 digits and 0 decimal points"
								                    runat="server"/>
							                    <asp:RangeValidator id="rvSize"
								                    ControlToValidate="txtSize"
								                    MinimumValue="-9999999999999999999"
								                    MaximumValue="9999999999999999999"
								                    Type="double"
								                    EnableClientScript="True"
								                    Text="The value is out of range !"
								                    runat="server" display="dynamic"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Usage %" HeaderStyle-HorizontalAlign=Right>
						                    <ItemStyle Width="8%" horizontalalign=right />
						                    <ItemTemplate>
							                    <asp:label id="lblpercent" Text='<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("AreaPercentage"), 0, True, False, False)) %>'  Runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Last Update">
						                    <ItemStyle Width="12%"  horizontalalign=right/>
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
						                    <ItemStyle Width="10%" />
						                    <ItemTemplate>
							                    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							                    <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							                    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
							                    <asp:label id="lblAreaERR" Text='<BR>Titled Area size exceeded !' forecolor=red Visible=false Runat="server"/>
							                    <asp:label id="lblAdjErrMsg" Text='<br>Adjustment area size cannot be zero !' forecolor=red Visible=false Runat="server"/>
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
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right" colspan="1">&nbsp;</td>
					        <td align="right"><B><asp:label id="lblAdjustmentTotal" runat="server" /></B>  </td>						
					        <td align="right"><B><asp:label id="lblAdjustmentprcnt" runat="server" /></B></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3><hr size="1" noshade></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right"><asp:label id="TAdjAmt" text="Total Area after Adjustment :" runat="server" /></td>
					        <td align="right"><B><asp:label id="lblTtlTitleTotal"   runat="server" /></B>  </td>						
					        <td align="right"><B><asp:label id="lblTtlTitlePrcnt"   runat="server" /></B></td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <!-- #2 End -->
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td align="right" ><asp:label id="TitleAmt" text="Title Area :" runat="server" /></td>
					        <td align="right"><B><asp:label id="lblTitleArea"   runat="server" /></B>  </td>						
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>
				        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25 colspan =3>&nbsp;</td>
					        <td >&nbsp;</td>
					        <td >&nbsp;</td>
                            </table></td>
				        </tr>

        
						 
						<tr>
							<td>
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Area" runat="server"/>
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
