<%@ Page Language="vb" trace="False" src="../../../include/BD_setup_TitleArea.aspx.vb" Inherits="BD_TitleArea" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDsetup" src="../../menu/menu_BDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Stock Analysis List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:label id="lbloper" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblBudgeting visible=false text="Budgeting " runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />


<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDsetup id=menuIN runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>Title Area</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
					        <td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					         
				        </tr>
				        <tr>
					        <td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period"  runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					         
				        </tr>
				        <tr>
					        <td colspan="4" width=60%>&nbsp;<hr style="width :100%" />   </td>
					         
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="18%" height="26" valign=bottom>Area :<BR>
									<asp:TextBox id=srchAreaCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="20%" height="26" valign=bottom>Description :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="64" runat="server"/>
								</td>
								<td width="20%" height="26" valign=bottom>Area Size :<BR>
									<asp:TextBox id=srchSize width=100% maxlength="64" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom>&nbsp;</td>
								<td width="10%" height="26" valign=bottom>Last Update By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="10" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="TitleData"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
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
					
					<asp:TemplateColumn HeaderText ="Area" Sortexpression="AreaCode" >
						<ItemTemplate>
							<%# Container.DataItem("AreaCode") %>
						</ItemTemplate>
						<EditItemTemplate>
						<asp:label id="lblAreaCode" Visible=False Text='<%# trim(Container.DataItem("AreaCode")) %>' runat="server"/>
							<asp:TextBox id="txtArea" MaxLength="8" width=95%
									Text='<%# trim(Container.DataItem("AreaCode")) %>'
									runat="server"/>
							<BR>
							<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
							<asp:label id="lblPeriodID" Text='<%# Container.DataItem("PeriodID") %>'  Visible = false Runat="server"/>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									ControlToValidate=txtArea />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="txtArea"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText ="Description" Sortexpression="Description">
						<ItemTemplate>
							<%# Container.DataItem("Description") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtDesc" MaxLength="32" width=95%
									Text='<%# trim(Container.DataItem("Description")) %>'
									runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText ="Area Size" Sortexpression="AreaSize">
						<ItemTemplate>
							<%# Container.DataItem("AreaSize") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtSize" MaxLength="12" width=50%
									Text='<%# trim(Container.DataItem("AreaSize")) %>'
									runat="server"/>
			                <asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtSize"
								ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								Display="Dynamic"
								text = "Maximum length 15 digits and 5 decimal points"
								runat="server"/>
							<asp:RequiredFieldValidator 
								id="validateQty" 
								runat="server" 
								ErrorMessage="Please Specify Quantity To Transfer" 
								ControlToValidate="txtSize" 
								display="dynamic"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtSize"
								MinimumValue="1"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value must be from 1 !"
								runat="server" display="dynamic"/>
							<asp:label id=lblerror text="Number generated is too big !" Visible=False forecolor=red Runat="server" />
									
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Status" >
						<ItemTemplate>
							<%# objBD.mtdGetPeriodStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Last Update"  Sortexpression="UpdateDate">
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By"  Sortexpression="UserName">
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
					
						<ItemTemplate>
						<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit"
							runat="server"/>
						</ItemTemplate>
						
						<EditItemTemplate>
						<asp:LinkButton id="Update" CommandName="Update" Text="Save"
							runat="server"/>
						<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete"
							runat="server"/>
						<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False
							runat="server"/>
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
                            <td><hr style="width :100%" />   
                                </td>
                        </tr>
                        <tr>
							<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td width="30%" align="right" colspan="1"><asp:label id="lblTotalArea" runat="server" /></td>
						            <td width="60%" align="Left"><asp:label id="lblTotAmtFig"   runat="server" /></td>						
						            <td width="10%">&nbsp;</td>
								</tr>
							</table>
							</td>
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
