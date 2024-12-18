<%@ Page Language="vb" trace="False" SmartNavigation="True" src="../../../include/BD_trx_CapExp.aspx.vb" Inherits="BD_CapExp" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Capital Expenditure </title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<asp:Label id=lblCode text=" Code" Visible="False" runat="server" />

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDtrx id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>CAPITAL EXPENDITURE</strong> 
                            </td>
                            
						</tr>
                        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="4" width=60%><asp:label id="lblLocTag" text="Location " runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
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
					        <td colspan=6><hr size="1" noshade></td>
                            </table></td>
				        </tr>
                        <tr>
                            <td><table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td class="mt-h" colspan="4" width=60%><asp:label id="lblTitle" runat="server" /></td>
					        <td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
                            </table></td>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="15%" height="26" valign=bottom><asp:label id="lblDetTag" Text="Expenditure" runat="server" /> :<BR>
								<asp:TextBox id=srchDetails width=100% maxlength="8" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom><asp:label id="lblAcc" Text="Account" runat="server" /> :<BR>
								<asp:TextBox id=srchAccount width=100% maxlength="8" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom> &nbsp</td>
								<td width="10%" height="26" valign=bottom> &nbsp</td>
								<td width="10%" height="26" valign=bottom> &nbsp</td>
								<td width="10%" height="26" valign=bottom> &nbsp</td>
								<td width="15%" height="26" valign=bottom><asp:label id="lblJust" Text="Justification" runat="server" /> :<BR>
									<asp:TextBox id=srchJust width=100% maxlength="64" runat="server"/>
								</td>
								<td width="12%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button id="SearchBtn" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
                                    <asp:DataGrid id="CapExp"
						            AutoGenerateColumns="false" width="100%" runat="server"
						            GridLines = none
						            Cellpadding = "2"
						            OnItemDataBound="DataGrid_ItemDataBound" 
						            OnEditCommand="DEDR_Edit"
						            OnUpdateCommand="DEDR_Update"
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
					
						                <asp:TemplateColumn HeaderText="Expenditure" SortExpression="ExpDetails">
							                <ItemStyle width=20% />
							                <ItemTemplate>
								                <%# Container.DataItem("ExpDetails") %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:Label id="lblExpID" Text='<%# Container.DataItem("ExpenditureID") %>' Visible=False runat="server" />
								                <asp:TextBox id="txtDet" MaxLength="64" width=95%
										                Text='<%# Container.DataItem("ExpDetails").trim %>'
										                runat="server"/>
								                <BR>
								                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									                text="Please enter expenditure details"
									                ControlToValidate=txtDet />
							                </EditItemTemplate>
							
							                </asp:TemplateColumn >
							                <asp:TemplateColumn HeaderText="Account Code" SortExpression="AccCode">
								                <ItemStyle Width="25%" />
								                <ItemTemplate>
									                <asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> runat="server" /><BR>
								                </ItemTemplate>
								                <EditItemTemplate>
									                <asp:DropDownList id="ddlAccCode" width=90% runat=server ></asp:DropDownList>
									                <asp:Label id="lblAccCode" Text=<%# Container.DataItem("AccCode") %> visible=false runat="server" /><BR>
									                <asp:RequiredFieldValidator id=validateAcc display=dynamic runat=server 
										                text="Please select Account Code"	ControlToValidate=ddlAccCode />
								                </EditItemTemplate>
							                </asp:TemplateColumn>

						                <asp:TemplateColumn HeaderText="Existing Unit" SortExpression="Existing">
							                <ItemStyle width=10% />
							                <ItemTemplate>
								                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Existing"), 0, True, False, False)) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="txtExist" width=100% MaxLength="19"
									                Text='<%# FormatNumber(Container.DataItem("Existing"), 0, True, False, False) %>'
									                runat="server"/>
								                <asp:RequiredFieldValidator id=validateExist display=dynamic runat=server 
										                text = "Please enter expenditure existing units"
										                ControlToValidate=txtExist />															
								                <asp:RegularExpressionValidator id="RegExpValExist" 
									                ControlToValidate="txtExist"
									                ValidationExpression="\d{1,19}"
									                Display="Dynamic"
									                text = "Maximum length 19 digits and 0 decimal points"
									                runat="server"/>
								                <asp:RangeValidator id="RangeExist"
									                ControlToValidate="txtExist"
									                MinimumValue="0"
									                MaximumValue="9999999999999999999"
									                Type="double"
									                EnableClientScript="True"
									                Text="The value is out of range !"
									                runat="server" display="dynamic"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Budgeted" SortExpression="Budgeted">
							                <ItemStyle width=10% />
							                <ItemTemplate>
								                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Budgeted"), 0, True, False, False)) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="txtBgt" width=100% MaxLength="19"
									                Text='<%# FormatNumber(Container.DataItem("Budgeted"), 0, True, False, False) %>'
									                runat="server"/>
								                <asp:RequiredFieldValidator id=validateBgt display=dynamic runat=server 
										                text = "Please enter budgeted units"
										                ControlToValidate=txtBgt />															
								                <asp:RegularExpressionValidator id="RegExpValBgt" 
									                ControlToValidate="txtBgt"
									                ValidationExpression="\d{1,19}"
									                Display="Dynamic"
									                text = "Maximum length 19 digits and 0 decimal points"
									                runat="server"/>
								                <asp:RangeValidator id="RangeBgt"
									                ControlToValidate="txtBgt"
									                MinimumValue="0"
									                MaximumValue="9999999999999999999"
									                Type="double"
									                EnableClientScript="True"
									                Text="The value is out of range !"
									                runat="server" display="dynamic"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Unit Cost" SortExpression="UnitCost">
							                <ItemStyle width=10% />
							                <ItemTemplate>
								                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("UnitCost"), 0, True, False, False)) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="txtCost" width=100% MaxLength="19"
									                Text='<%# FormatNumber(Container.DataItem("UnitCost"), 0, True, False, False) %>'
									                runat="server"/>
								                <asp:RequiredFieldValidator id=validatecost display=dynamic runat=server 
										                text = "Please enter unit cost"
										                ControlToValidate=txtCost />															
								                <asp:RegularExpressionValidator id="RegExpValCost" 
									                ControlToValidate="txtCost"
									                ValidationExpression="\d{1,19}"
									                Display="Dynamic"
									                text = "Maximum length 19 digits and 0 decimal points"
									                runat="server"/>
								                <asp:RangeValidator id="RangeCost"
									                ControlToValidate="txtCost"
									                MinimumValue="0"
									                MaximumValue="9999999999999999999"
									                Type="double"
									                EnableClientScript="True"
									                Text="The value is out of range !"
									                runat="server" display="dynamic"/>
						                </EditItemTemplate>
						                </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Total Amount" SortExpression="TotalCost">
							                <ItemStyle width=10% />
							                <ItemTemplate>
								                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("TotalCost"), 0, True, False, False)) %>
							                </ItemTemplate>
							                <EditItemTemplate>
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Justification" SortExpression="justification">
							                <ItemStyle width=10% />
							                <ItemTemplate>
								                <%# Container.DataItem("justification") %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="txtJust" width=100% MaxLength="64"
									                Text='<%# trim(Container.DataItem("justification")) %>'
									                runat="server"/>
								                <asp:RequiredFieldValidator id=validateJust display=dynamic runat=server 
										                text = "Please enter expenditure existing units"
										                ControlToValidate=txtExist />															
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						
						                <asp:TemplateColumn>
							                <ItemStyle width=5% />			
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
					        <td align="left" ColSpan=6>
						        <table width="100%" cellspacing="0" cellpadding="2" border="0" align="center">
							        <tr class="mb-t">
								        <td width="30%" height="26" valign=bottom>
									        <B>TOTAL</B>
								        </td>
								        <td width="45%" height="26" valign=bottom>&nbsp;</td>
								        <td width="10%" align=left height="26" valign=bottom>
									        <B><asp:Label text=0 id=lblTotalAmt width=100% runat="server"/></B>
								        </td>
								        <td width="15%" height="26" valign=bottom>&nbsp;</td>
							        </tr>					
						        </table>
					        </td>
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Expenditure" runat="server"/>
						<!--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>-->
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
		 
			</tr>
		</table>
        
        		
		</FORM>
	</body>
</html>
