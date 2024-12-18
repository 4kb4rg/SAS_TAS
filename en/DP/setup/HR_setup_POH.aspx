<%@ Page Language="vb" src="../../../include/HR_Setup_POH.aspx.vb" Inherits="HR_Setup_POH" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Point Of Hired List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:label id="lblValidate" visible="false" text="Please enter " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> LIST</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom><asp:label id="lblPOHCode" runat="server" /> Code :<BR><asp:TextBox id=srchPOHCode width=100% maxlength="8" runat="server"/></td>
								<td width="40%" height="26" valign=bottom><asp:label id="lblPOHDesc" runat="server" /> :<BR><asp:TextBox id=srchPOHDesc width=100% maxlength="128" runat="server"/></td>
								<td width="20%" height="26" valign=bottom>Last Update :<BR><asp:TextBox id=srchLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button ID="SearchBtn" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
					                <asp:TemplateColumn SortExpression="POHCode">
						                <ItemTemplate>
							                <%# Container.DataItem("POHCode") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtPOHCode" MaxLength="8" width=95%
								                Text='<%# trim(Container.DataItem("POHCode")) %>' runat="server"/><BR>
							                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								                ControlToValidate=txtPOHCode />
							                <asp:RegularExpressionValidator id=revCode 
								                ControlToValidate="txtPOHCode"
								                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                Display="Dynamic"
								                text="Alphanumeric without any space in between only."
								                runat="server"/>

							                <asp:label id="lblDupMsg" Text="Code already exist" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="Description" SortExpression="POHDesc">
						                <ItemTemplate>
							                <%# Container.DataItem("POHDesc") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtPOHDesc" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("POHDesc")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								                ControlToValidate=txtPOHDesc />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="Create Date" SortExpression="CreateDate">
						                <ItemTemplate>
							                <%# objGlobal.GetLongDate(Container.DataItem("CreateDate")) %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="txtCreateDate" Readonly=TRUE size=8 
								                Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
				
					                <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
						                <ItemTemplate>
							                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="txtUpdateDate" Readonly=TRUE size=8 
								                Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
						                <ItemTemplate>
							                <%# objHR.mtdGetPOHStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="ddlStatusList" size=1 runat=server />
							                <asp:TextBox id="txtStatus" Readonly=TRUE Visible = False
								                Text='<%# Container.DataItem("Status")%>' runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
						                <ItemTemplate>
							                <%# Container.DataItem("username") %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="txtUpdatedBy" Readonly=TRUE size=8 
								                Text='<%# Session("SS_USERID") %>' Visible=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn>					
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
					            <asp:ImageButton id="btnNew" OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Point Of Hired Code" runat="server"/>
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



			</Form>
		</body>
</html>
