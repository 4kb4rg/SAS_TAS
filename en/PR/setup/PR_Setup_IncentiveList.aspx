<%@ Page Language="vb" src="../../../include/PR_Setup_IncentiveList.aspx.vb" Inherits="PR_setup_IncentiveList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Incentive List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form i="frmIncentive" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			<asp:label id="curStatus" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRSetup id=menuPR runat="server" />
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
								<td width="20%" height="26" valign=bottom>
									<asp:label id="lblIncentiveCode" runat="server" /> :<BR>
									<asp:TextBox id=srchIncentiveCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="42%" height="26" valign=bottom>
									<asp:label id="lblDescription" runat="server" /> :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
						            <asp:DataGrid id="dgLine"
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
					                <asp:TemplateColumn SortExpression="IncentiveCode">
						                <ItemStyle Width="15%" />
						                <ItemTemplate>
							                <%# Container.DataItem("IncentiveCode") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtIncentiveCode" MaxLength="8" width=95%
								                Text='<%# trim(Container.DataItem("IncentiveCode")) %>'
								                runat="server"/><BR>
							                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								                ControlToValidate=txtIncentiveCode />
							                <asp:RegularExpressionValidator id=revCode 
								                ControlToValidate="txtIncentiveCode"
								                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                Display="Dynamic"
								                text="Alphanumeric without any space in between only."
								                runat="server"/>
							                <asp:label id="lblDupMsg" Text="Code already exist" Visible=false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn SortExpression="Description">
						                <ItemStyle Width="20%" />
						                <ItemTemplate>
							                <%# Container.DataItem("Description") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtDescription" width=100% MaxLength="128"
								                Text='<%# trim(Container.DataItem("Description")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateDesc display=Dynamic 
								                ControlToValidate=txtDescription 
								                runat=server />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="Premi Type" SortExpression="PremiType">						
						                <ItemStyle Width="22%" />
						                <ItemTemplate>
							                <%# objPR.mtdGetPremiType(IIF(ISNUMERIC(Container.DataItem("PremiType")),Container.DataItem("PremiType"),VAL(Container.DataItem("PremiType")))) %>
						                </ItemTemplate>
						                <EditItemTemplate>							
							                <asp:DropDownList id="ddlPremiType" width=100% onSelectedIndexChanged="PremiType_Changed" AutoPostBack=true runat=server>
							                </asp:DropDownList>						
							                <asp:Label id=lblErrPremiType Text="Select Premi Type" visible=false forecolor=red runat=server/>	
							                <asp:label id=lblPremiType visible=false text='<%# trim(Container.DataItem("PremiType")) %>' runat=server/>						
						                </EditItemTemplate>
					                </asp:TemplateColumn>				
					                <asp:TemplateColumn HeaderText="Rate" SortExpression="Rate">
						                <ItemStyle Width="12%" />
						                <ItemTemplate>
							                <%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Rate")) %> <!-- Modified BY ALIM -->
						                </ItemTemplate>
						                <EditItemTemplate>
							                <!-- Modified BY ALIM maxLength = 22 -->
							                <asp:TextBox id="txtRate" width=100% MaxLength="22"
								                Text='<%# trim(Container.DataItem("Rate")) %>'
								                runat="server"/>
							                <asp:CompareValidator id="cvRate" display=dynamic runat="server" 
							                ControlToValidate="txtRate" Text="<br>The value must be a whole number with decimal. " 
							                Type="double" Operator="DataTypeCheck"/>	
							                <!-- Modified BY ALIM -->								
							                <asp:RegularExpressionValidator id=revRate
								                ControlToValidate="txtRate"
								                ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
								                Display="Dynamic"
								                text = "Maximum length 19 digits and 2 decimal points. "
								                runat="server"/>																
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Percentage" SortExpression="Percentage">
						                <ItemStyle Width="12%" />
						                <ItemTemplate>							
							                <%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Percentage")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <!-- Added By BHL -->
							                <asp:TextBox id="txtPercentage" width=100% MaxLength="12"
								                Text='<%# Container.DataItem("Percentage") %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validatePercentage display=Dynamic 
								                ControlToValidate=txtPercentage 
								                Text="Please Enter Percentage."
								                runat=server />							
							                <asp:CompareValidator id="cvPercentage" display=dynamic runat="server" 
							                ControlToValidate="txtPercentage" Text="<br>The value must be a whole number with decimal. " 
							                Type="double" Operator="DataTypeCheck"/>																
							                <asp:RegularExpressionValidator id=revPercentage
								                ControlToValidate="txtRate"
								                ValidationExpression="\d{1,9}\.\d{1,2}|\d{1,9}"
								                Display="Dynamic"
								                text = "Maximum length 9 digits and 2 decimal points. "
								                runat="server"/>																
						                </EditItemTemplate>
					                </asp:TemplateColumn>			
					                <asp:TemplateColumn HeaderText="Last Update" SortExpression="Inc.UpdateDate">
						                <ItemStyle Width="10%" />
						                <ItemTemplate>
							                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="txtUpdateDate" Readonly=TRUE size=8 
								                Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
								                runat="server"/>
							                <asp:TextBox id="txtCreateDate" Visible=False
								                Text='<%# Container.DataItem("CreateDate") %>'
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Status" SortExpression="Inc.Status">
						                <ItemStyle Width="8%" />
						                <ItemTemplate>
							                <%# objPR.mtdGetIncentiveStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="ddlStatus" runat=server />
							                <asp:TextBox id="txtStatus" Readonly=TRUE Visible=False
								                Text='<%# Container.DataItem("Status")%>'
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						                <ItemStyle Width="30%" />
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="txtUserName" Readonly=TRUE size=8 
								                Text='<%# Session("SS_USERID") %>'
								                Visible=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn>					
						                <ItemStyle Width="5%" />
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
					            <asp:ImageButton id=ibNew imageurl="../../images/butt_new.gif" OnClick="DEDR_Add" AlternateText="New Incentive" runat="server"/>
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
