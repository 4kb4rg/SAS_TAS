<%@ Page Language="vb" src="../../../include/PM_Setup_UllageAverageCapacityConversionMaster.aspx.vb" Inherits="PM_Setup_UllageAverageCapacityConversionMaster" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Ullage - Average Capacity Conversion Master</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server" />



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
								<td width="10%">
									<asp:label id="lblUVTableCode" runat="server" /> :<BR>
									<asp:TextBox id=srchUVTableCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="10%">
									<asp:label id="lblUllageFrom" runat="server" /> :<BR>
									<asp:TextBox id=srchUllageFrom width=100% maxlength="21" runat="server"/>
									<asp:RegularExpressionValidator id="revsrchUllageFrom" 
										ControlToValidate="srchUllageFrom"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal places"
										runat="server"/>
								</td>
								<td width="10%">
									<asp:label id="lblUllageTo" runat="server" /> :<BR>
									<asp:TextBox id=srchUllageTo width=100% maxlength="21" runat="server"/>
									<asp:RegularExpressionValidator id="revsrchUllageTo" 
										ControlToValidate="srchUllageTo"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal places"
										runat="server"/>
								</td>
								<td width="10%">
									<asp:label id="lblUllageDiff" runat="server" /> :<BR>
									<asp:TextBox id=srchUllageDiff width=100% maxlength="21" runat="server"/>
									<asp:RegularExpressionValidator id="revsrchUllageDiff" 
										ControlToValidate="srchUllageDiff"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal places"
										runat="server"/>
								</td>
								<td width="18%">
									<asp:label id="lblVolume" runat="server" /> :<BR>
									<asp:TextBox id=srchVolume width=100% maxlength="21" runat="server"/>
									<asp:RegularExpressionValidator id="revsrchVolume" 
										ControlToValidate="srchVolume"
										ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
										Display="Dynamic"
										text = "Maximum length 15 digits and 5 decimal places"
										runat="server"/>
								</td>
								<td width="15%">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="15%">Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height=26 valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
					                <asp:TemplateColumn SortExpression="UVTableCode">
						                <ItemStyle Width="12%" />
						                <ItemTemplate>
							                <%# Container.DataItem("UVTableCode") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtUVTableCode" width=100% MaxLength="8" visible="False"
								                Text='<%# trim(Container.DataItem("UVTableCode")) %>'
								                runat="server"/>
							                <asp:DropDownList id="lstUVTableCode" width=100% runat="server" size="1"/>
							                <asp:RequiredFieldValidator id="validateUVTableCode" runat="server" 
								                display="dynamic" 
								                ControlToValidate="lstUVTableCode"
								                Text="Please select table code."/>
							                <asp:label id="lblDupMsg"  Text="Record already exist" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn SortExpression="UllageFrom">
						                <ItemStyle Width="12%" />
						                <ItemTemplate>
							                <%# objGlobal.DisplayQuantityFormat(Container.DataItem("UllageFrom")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtUllageFrom" width=100% MaxLength="21"
								                Text='<%# trim(Container.DataItem("UllageFrom")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateUllageFrom display=Dynamic runat=server 
									                ControlToValidate=txtUllageFrom />
							                <asp:RegularExpressionValidator id="revUllageFrom" 
								                ControlToValidate="txtUllageFrom"
								                ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								                Display="Dynamic"
								                text = "Maximum length 15 digits and 5 decimal places"
								                runat="server"/>
							                <asp:label id="lblUllageFromErr"  Text="Ullage From must be smaller than Ullage To" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn SortExpression="UllageTo">
						                <ItemStyle Width="12%" />
						                <ItemTemplate>
							                <%# objGlobal.DisplayQuantityFormat(Container.DataItem("UllageTo")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtUllageTo" width=100% MaxLength="21"
								                Text='<%# trim(Container.DataItem("UllageTo")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateUllageTo display=Dynamic runat=server 
									                ControlToValidate=txtUllageTo />
							                <asp:RegularExpressionValidator id="revUllageTo" 
								                ControlToValidate="txtUllageTo"
								                ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								                Display="Dynamic"
								                text = "Maximum length 15 digits and 5 decimal places"
								                runat="server"/>
							                <asp:label id="lblUllageToErr"  Text="Ullage To must be larger than Ullage From" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn SortExpression="UllageDiff">
						                <ItemStyle Width="12%" />
						                <ItemTemplate>
							                <%# objGlobal.DisplayQuantityFormat(Container.DataItem("UllageDiff")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtUllageDiff" width=100% MaxLength="21"
								                Text='<%# trim(Container.DataItem("UllageDiff")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateUllageDiff display=Dynamic runat=server 
									                ControlToValidate=txtUllageDiff />
							                <asp:RegularExpressionValidator id="revUllageDiff" 
								                ControlToValidate="txtUllageDiff"
								                ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								                Display="Dynamic"
								                text = "Maximum length 15 digits and 5 decimal places"
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn SortExpression="Volume">
						                <ItemStyle Width="16%" />
						                <ItemTemplate>
							                <%# objGlobal.DisplayQuantityFormat(Container.DataItem("Volume")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtVolume" width=100% MaxLength="21"
								                Text='<%# trim(Container.DataItem("Volume")) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validateVolume display=Dynamic runat=server 
									                ControlToValidate=txtVolume />
							                <asp:RegularExpressionValidator id="revVolume" 
								                ControlToValidate="txtVolume"
								                ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
								                Display="Dynamic"
								                text = "Maximum length 15 digits and 5 decimal places"
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="Last Update" SortExpression="UACC.UpdateDate">
						                <ItemStyle Width="10%" />
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
					                <asp:TemplateColumn HeaderText="Status" SortExpression="UACC.Status">
						                <ItemStyle Width="6%" />
						                <ItemTemplate>
							                <%# objPM.mtdGetUllageAverageCapacityConversionStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                <asp:TextBox id="txtStatus" Readonly=TRUE Visible = False
								                Text='<%# Container.DataItem("Status")%>'
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						                <ItemStyle Width="15%" />
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="txtUserName" Readonly=TRUE size=8 
								                Text='<%# Session("SS_USERID") %>'
								                Visible=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn ItemStyle-HorizontalAlign=Center>					
						                <ItemStyle Width="12%" />					
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
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
				            		<asp:Label id=lblPageCount visible=false text=1 runat=server/>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=ibNew imageurl="../../images/butt_new.gif" OnClick="DEDR_Add" AlternateText="New Ullage-Average Capacity Conversion Entry" runat="server"/>
					        	<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
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
