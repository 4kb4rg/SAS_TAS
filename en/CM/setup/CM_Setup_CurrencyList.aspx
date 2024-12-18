<%@ Page Language="vb" src="../../../include/CM_Setup_CurrencyList.aspx.vb" Inherits="CM_Setup_CurrencyList" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_cm_setup" src="../../menu/menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Currency List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="frmCurr" runat="server" class="main-modul-bg-app-list-pu">
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
							<td><strong>CURRENCY LIST</strong><hr style="width :100%" />   
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
								<td width="20%" height="26">Currency Code :<br><asp:TextBox id="srchCurrencyCode" width="100%" maxlength="8" runat="server"/></td>
								<td width="35%" height="26">Description :<br><asp:TextBox id="srchDescription" width="100%" maxlength="128" runat="server"/></td>
								<td width="15%" height="26">Status :<br><asp:DropDownList id="srchStatus" width="100%" runat="server"/></td>
								<td width="20%" height="26">Last Updated By :<br><asp:TextBox id="srchUpdBy" width="100%" maxlength="128" runat="server"/></td>
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
					            <asp:TemplateColumn HeaderText="Currency Code" ItemStyle-Width="21%" SortExpression="curr.CurrencyCode">
						            <ItemTemplate>
							            <%# Container.DataItem("CurrencyCode") %>
						            </ItemTemplate>
						            <EditItemTemplate>
							            <asp:TextBox id="CurrencyCode" MaxLength="8" width="100%"
								            Text='<%# trim(Container.DataItem("CurrencyCode")) %>'
								            runat="server"/>
							            <asp:RequiredFieldValidator id=validateCode 
								            display=dynamic 
								            runat=server 
								            ControlToValidate="CurrencyCode" 
								            text="Please enter Currency Code." />															
							            <asp:RegularExpressionValidator id=revCode 
								            ControlToValidate="CurrencyCode"
								            ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								            Display="Dynamic"
								            text="Alphanumeric without any space in between only."
								            runat="server"/>
							            <asp:label id="lblDupMsg" text="Code already exist." visible=false forecolor=red runat="server"/>

						            </EditItemTemplate>
					            </asp:TemplateColumn>	
					            <asp:TemplateColumn HeaderText="Description" SortExpression="curr.Description">
						            <ItemTemplate>
							            <%# Container.DataItem("Description") %>
						            </ItemTemplate>
						            <EditItemTemplate>
							            <asp:TextBox id="Description" width="100%" MaxLength="128"
								            Text='<%# trim(Container.DataItem("Description")) %>'
								            runat="server"/>
							            <asp:RequiredFieldValidator id=validateDesc 
								            display=Dynamic 
								            runat=server 
								            ControlToValidate="Description" 
								            text="Please enter Description."/>															
						            </EditItemTemplate>
					            </asp:TemplateColumn>					
					            <asp:TemplateColumn HeaderText="Last Update" SortExpression="curr.UpdateDate">
						            <ItemTemplate>
							            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						            </ItemTemplate>
						            <EditItemTemplate >
							            <asp:TextBox id="UpdateDate" Readonly=true size=8 
								            Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								            runat="server"/>
							            <asp:TextBox id="CreateDate" Visible=False
								            Text='<%# Container.DataItem("CreateDate") %>'
								            runat="server"/>
						            </EditItemTemplate>
					            </asp:TemplateColumn>
					            <asp:TemplateColumn HeaderText="Status" SortExpression="curr.Status">
						            <ItemTemplate>
							            <%# objCMSetup.mtdGetCurrencyStatus(Container.DataItem("Status")) %>
						            </ItemTemplate>
						            <EditItemTemplate>
							            <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							            <asp:TextBox id="Status" Readonly=true Visible=False
								            Text='<%# Container.DataItem("Status")%>'
								            runat="server"/>
						            </EditItemTemplate>
					            </asp:TemplateColumn>
					            <asp:TemplateColumn HeaderText="Updated By" SortExpression="usr.UserName">
						            <ItemTemplate>
							            <%# Container.DataItem("UserName") %>
						            </ItemTemplate>
						            <EditItemTemplate >
							            <asp:TextBox id="UserName" Readonly=true size=8 
								            Text='<%# Session("SS_USERID") %>'
								            Visible=False runat="server"/>
						            </EditItemTemplate>
					            </asp:TemplateColumn>					
					            <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>					
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Account Class Group" runat="server"/>
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
