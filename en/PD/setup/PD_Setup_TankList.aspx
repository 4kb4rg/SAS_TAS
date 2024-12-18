<%@ Page Language="vb" src="../../../include/PD_Setup_TankList.aspx.vb" Inherits="PD_Setup_TankList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Palm Oil Mill Storage List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>


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
							<td><strong>PALM OIL MILL STORAGE LIST</strong><hr style="width :100%" />   
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
								<td width="10%" height="26">Tank Code :<br><asp:TextBox id="srchTankCd" width="100%" maxlength="8" runat="server"/></td>
								<td width="20%" height="26">Name :<br><asp:TextBox id="srchName" width="100%" maxlength="64" runat="server"/></td>
								<td width="20%" height="26">Type:<br>
									<asp:DropDownList id="srchType" width="100%" runat="server"/>									
								</td>
								<td width="10%" height="26">Status:<br>
									<asp:DropDownList id="srchStatus" width="100%" runat="server"/>									
								</td>
								<td width="20%" height="26">Last Update By :<br><asp:TextBox id="srchUpdBy" width="100%" maxlength="10" runat="server"/></td>
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
						                GridLines=none
						                Cellpadding="2"
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
					                <asp:TemplateColumn HeaderText="Tank Code" ItemStyle-Width="10%" SortExpression="T.TankCode">
						                <ItemTemplate>
							                <%# Container.DataItem("TankCode") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="TankCode" MaxLength="8" width="95%"
									                Text='<%# Trim(Container.DataItem("TankCode")) %>'
									                runat="server"/>
							                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									                ErrorMessage="<br>Please enter Tank Code."
									                ControlToValidate="TankCode" />
							                <asp:RegularExpressionValidator id=revCode 
								                ControlToValidate="TankCode"
								                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                Display="Dynamic"
								                text="Alphanumeric without any space in between only."
								                runat="server"/>
							                <asp:label id="lblDupMsg"  Text="Code already exist." Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="Name" ItemStyle-Width="20%" SortExpression="T.Name">
						                <ItemTemplate>
							                <%# Container.DataItem("Name") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="Name" MaxLength="64" width="100%"
								                Text='<%# Trim(Container.DataItem("Name")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
									                ErrorMessage="Please enter Name."
									                ControlToValidate="Name" />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Type" ItemStyle-Width="20%" SortExpression="T.Type">
						                <ItemTemplate>
							                <%# Container.DataItem("Type") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList id="TypeList" size=1 runat=server />
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Storage (MT)" ItemStyle-Width="15%" SortExpression="T.Storage">
						                <ItemTemplate>
							                <%# Container.DataItem("Storage")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="Storage" MaxLength="64" width="100%"
								                Text='<%# Trim(Container.DataItem("Storage")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateStorage display=Dynamic runat=server 
								                ErrorMessage="Please enter storage in metric ton."
								                ControlToValidate="Storage" />															
							                <asp:RegularExpressionValidator id=revStorage display="dynamic" runat="server"
								                text = "<br>Maximum length 15 digits and 5 decimal points. "
								                ControlToValidate="Storage"
								                ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}" />
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="T.UpdateDate">
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
					                <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="5%" SortExpression="T.Status">
						                <ItemTemplate>
							                <%# objPD.mtdGetTankStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                <asp:TextBox id="Status" Readonly=TRUE Visible = False
								                Text='<%# Container.DataItem("Status")%>'
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="10%" SortExpression="Usr.UserName">
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="UserName" Readonly=TRUE size=8 
								                Text='<%# Session("SS_USERID") %>'
								                Visible=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Right>					
						                <ItemTemplate>
						                <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit"
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
							<td>
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Storage" runat="server"/>
						<!--
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						-->
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
