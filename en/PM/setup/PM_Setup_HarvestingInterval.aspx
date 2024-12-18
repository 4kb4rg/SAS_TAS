<%@ Page Language="vb" Src="../../../include/PM_Setup_HarvestingInterval.aspx.vb" Inherits="PM_Setup_HarvestingInterval" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>HARVESTING INTERVAL SETUP</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server"/>
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server"/>


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDSetup id=menuPD runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>HARVESTING INTERVAL SETUP</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="EventData"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    GridLines = none
						                    Cellpadding = "2"
						                    OnEditCommand="DEDR_Edit"
						                    OnUpdateCommand="DEDR_Update"
						                    OnCancelCommand="DEDR_Cancel"
						                    AllowPaging="True" 
						                    Allowcustompaging="False"
						                    Pagesize="15" 
						                    Pagerstyle-Visible="False"						
						                    OnSortCommand="Sort_Grid"						
						                    AllowSorting="True"
                                                            class="font9Tahoma">
								
							                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					                    <Columns>
					                    <asp:TemplateColumn HeaderText="Range Type" SortExpression="PM_HAINTERVAL.RangeType">
						                    <ItemStyle Width="25%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("RangeType") %>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="From" SortExpression="PM_HAINTERVAL.DayFrom">
						                    <ItemStyle Width="15%" />
						                    <ItemTemplate>
								                    <%# Container.DataItem("DayFrom") & iif (Container.DataItem("DayFrom")>1, " days" , " day") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtDayFrom" MaxLength="8" width=95%
									                    Text='<%# trim(Container.DataItem("DayFrom")) %>'
									                    runat="server"/>
							                    <BR>
							                    <asp:RequiredFieldValidator id=validateDayFrom display=dynamic runat=server 
									                    ErrorMessage="Field cannot be blank." ControlToValidate=txtDayFrom />
							                    <asp:RegularExpressionValidator id=revDayFrom 
								                    ControlToValidate="txtDayFrom"
								                    ValidationExpression="[0-9]{1,8}"
								                    Display="Dynamic"
								                    text="Please enter integer only."
								                    runat="server"/>
							                    <asp:label id="lblDayFromMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="To" SortExpression="PM_HAINTERVAL.DayTo">
						                    <ItemStyle Width="15%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("DayTo") & iif (Container.DataItem("DayTo")>1, " days" , " day") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="txtDayTo" MaxLength="8" width=95%
									                    Text='<%# trim(Container.DataItem("DayTo")) %>'
									                    runat="server"/>
							                    <BR>
							                    <asp:RequiredFieldValidator id=validateDayTo display=dynamic runat=server 
									                    ErrorMessage="Field cannot be blank." ControlToValidate=txtDayTo />
							                    <asp:RegularExpressionValidator id=revDayTo 
								                    ControlToValidate="txtDayTo"
								                    ValidationExpression="[0-9]{1,8}"
								                    Display="Dynamic"
								                    text="Please enter integer only."
								                    runat="server"/>
							                    <asp:label id="lblDayToMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>					
					                    <asp:TemplateColumn HeaderText="Last Update" SortExpression="PM_HAINTERVAL.UpdateDate">
						                    <ItemStyle Width="15%" />
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
							                    <asp:TextBox id="txtUpdateDate" Readonly=TRUE size=8 
								                    Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								                    runat="server"/>
							                    <asp:TextBox id="txtCreateDate" Visible=False
								                    Text='<%# Container.DataItem("UpdateDate") %>'
								                    runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					                    <asp:TemplateColumn HeaderText="Updated By" SortExpression="PM_HAINTERVAL.UpdateID">
						                    <ItemStyle Width="15%" />
						                    <ItemTemplate>
							                    <%# Container.DataItem("UpdateID") %>
						                    </ItemTemplate>
						                    <EditItemTemplate >
							                    <asp:TextBox id="txtUserName" Readonly=TRUE size=8 
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
							                    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>										
					                    <asp:TemplateColumn>
						                    <ItemStyle Width="0" />
						                    <ItemTemplate>
							                    <asp:TextBox id="txtIntervalID" Width="0"
									                    Text='<%# Container.DataItem("IntervalID")%>'
									                    runat="server"/>
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
					            <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
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
