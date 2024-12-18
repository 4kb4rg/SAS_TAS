<%@ Page Language="vb" src="../../../include/NU_setup_CullType.aspx.vb" Inherits="NU_setup_CullType" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUSetup" src="../../menu/menu_NUSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Culling Type </title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" ID="Form1"  class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:label id="lblOper" Visible="False" Runat="server"></asp:label>



		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuNUSetup id=menuNU runat="server" />
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
								<td width="15%" height="26" valign=bottom><asp:label id="lblCullTypeCode" runat="server" /> :<BR><asp:TextBox id=srchCullTypeCode width=100% maxlength="8" runat="server"/></td>
								<td width="40%" height="26" valign=bottom><asp:label id="lblDescription" runat="server" /> :<BR><asp:TextBox id=srchDescription width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
						                <asp:TemplateColumn SortExpression="CullTypeCode">
							                <ItemStyle Width="15%" />
							                <ItemTemplate>
								                <%# Container.DataItem("CullTypeCode") %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="CullTypeCode" MaxLength="8" width=95%
										                Text='<%# trim(Container.DataItem("CullTypeCode")) %>'
										                runat="server"/>
								                <BR>
								                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
										                ControlToValidate=CullTypeCode />
								                <asp:RegularExpressionValidator id=revCode 
									                ControlToValidate="CullTypeCode"
									                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
									                Display="Dynamic"
									                text="Alphanumeric without any space in between only."
									                runat="server"/>
								                <asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>	
						                <asp:TemplateColumn SortExpression="Description">
							                <ItemStyle Width="30%" />
							                <ItemTemplate>
								                <%# Container.DataItem("Description") %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:TextBox id="Description" width=100% MaxLength="128"
									                Text='<%# trim(Container.DataItem("Description")) %>'
									                runat="server"/>
								                <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
										                ControlToValidate=Description />															
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn SortExpression="NurseryStage">
							                <ItemStyle Width="12%" />
							                <ItemTemplate>
								                <%# objNUSetup.mtdGetNurseryStage(Container.DataItem("NurseryStage")) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:DropDownList id="NurseryStage" width=98% runat=server>
								                </asp:DropDownList>							
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Last Update" SortExpression="CT.UpdateDate">
							                <ItemStyle Width="10%" />
							                <ItemTemplate>
								                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
							                </ItemTemplate>
							                <EditItemTemplate >
								                <asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
									                Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
									                runat="server"/>
								                <asp:TextBox id="CreateDate" Visible=False
									                Text='<%# Container.DataItem("CreateDate") %>'
									                runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Status" SortExpression="CT.Status">
							                <ItemStyle Width="8%" />
							                <ItemTemplate>
								                <%# objNUSetup.mtdGetNurseryBatchStatus(Container.DataItem("Status")) %>
							                </ItemTemplate>
							                <EditItemTemplate>
								                <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
								                <asp:TextBox id="Status" Readonly=TRUE Visible = False
									                Text='<%# Container.DataItem("Status")%>'
									                runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>
						                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
							                <ItemStyle Width="11%" />
							                <ItemTemplate>
								                <%# Container.DataItem("UserName") %>
							                </ItemTemplate>
							                <EditItemTemplate >
								                <asp:TextBox id="UserName" Readonly=TRUE size=8 
									                Text='<%# Session("SS_USERID") %>'
									                Visible=False runat="server"/>
							                </EditItemTemplate>
						                </asp:TemplateColumn>											
						                <asp:TemplateColumn ItemStyle-HorizontalAlign=center >					
							                <ItemStyle Width="14%" />
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
					            <asp:ImageButton id=ibNew imageurl="../../images/butt_new.gif" OnClick="DEDR_Add" AlternateText="New" runat="server"/>
				        		<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
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
