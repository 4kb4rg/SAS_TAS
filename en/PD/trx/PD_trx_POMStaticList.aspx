<%@ Page Language="vb" src="../../../include/PD_trx_POMStaticList.aspx.vb" Inherits="PD_trx_POMStaticList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Palm Oil Mill Statistic List</title>
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
						<UserControl:MenuPDTrx id=menuPD runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>PALM OIL MILL STATISTIC LIST</strong><hr style="width :100%" />   
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
								<td width="10%" height="26">Date :<br><asp:TextBox id="srchDate" width="100%" maxlength="11" runat="server"/></td>
								<td width="10%" height="26">
									FFA :<br><asp:TextBox id="srchFFA" width="100%" maxlength="6" runat="server"/><br>
										<asp:CompareValidator id="cvsrchFFA" display=dynamic runat="server" 
											ControlToValidate="srchFFA" Text="<br>The value must whole number or with decimal. " 
											Type="Double" Operator="DataTypeCheck"/>
								</td>
								<td width="10%" height="26">
									M&I :<br><asp:TextBox id="srchMI" width="100%" maxlength="6" runat="server"/><br>
										<asp:CompareValidator id="cvsrchMI" display=dynamic runat="server" 
											ControlToValidate="srchMI" Text="<br>The value must whole number or with decimal. " 
											Type="Double" Operator="DataTypeCheck"/>
								</td>
								<td width="10%" height="26">
									DOBI :<br><asp:TextBox id="srchDOBI" width="100%" maxlength="6" runat="server"/>
										<asp:CompareValidator id="cvsrchDOBI" display=dynamic runat="server" 
											ControlToValidate="srchDOBI" Text="<br>The value must whole number or with decimal. " 
											Type="Double" Operator="DataTypeCheck"/>
								</td>
								<td width="20%" height="26">
									RAIN FALL MM :<br><asp:TextBox id="srchMM" width="100%" maxlength="6" runat="server"/>
										<asp:CompareValidator id="cvsrchMM" display=dynamic runat="server" 
											ControlToValidate="srchMM" Text="<br>The value must whole number or with decimal. " 
											Type="Double" Operator="DataTypeCheck"/>
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
					                <asp:TemplateColumn HeaderText="Date" ItemStyle-Width="15%" SortExpression="StaticDate">
						                <ItemTemplate>
							                <%# objGlobal.GetShortDate(strDateFmt, Container.DataItem("StaticDate")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="StaticDate" MaxLength="10" width="95%"
									                Text='<%# objGlobal.GetShortDate(strDateFmt, Container.DataItem("StaticDate")) %>'
									                runat="server"/>
							                <asp:label id="lblDupMsg" Text="<br>Date already exist." Visible = false forecolor=red Runat="server"/>
							                <asp:label id="lblErrStaticDate" Text="<br>Please use valid date format " visible=false forecolor=red runat=server />
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					                <asp:TemplateColumn HeaderText="FFA" ItemStyle-Width="10%" SortExpression="FFA">
						                <ItemTemplate>
							                <%# FormatNumber(Container.DataItem("FFA"),2) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="FFA" MaxLength="6" width="100%"
								                Text='<%# FormatNumber(Container.DataItem("FFA"),2) %>'
								                runat="server"/>
							                <asp:RangeValidator display=dynamic id=rvFFA
								                ControlToValidate="FFA"
								                MinimumValue="0"
								                MaximumValue="100"
								                Type="Double"
								                EnableClientScript="true"
								                Text="The value must be within percentage"
								                runat="server"/>
							                <asp:RegularExpressionValidator id=revFFA 
								                ControlToValidate="FFA"
								                ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
								                Display="Dynamic"
								                text = "Maximum length 3 digits and 2 decimal points. "
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="M&I" ItemStyle-Width="10%" SortExpression="MI">
						                <ItemTemplate>
							                <%# FormatNumber(Container.DataItem("MI"),2) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="MI" MaxLength="6" width="100%"
								                Text='<%# FormatNumber(Container.DataItem("MI"),2) %>'
								                runat="server"/>
							                <asp:RangeValidator display=dynamic id=rvMI
								                ControlToValidate="MI"
								                MinimumValue="0"
								                MaximumValue="100"
								                Type="Double"
								                EnableClientScript="true"
								                Text="The value must be within percentage"
								                runat="server"/>
							                <asp:RegularExpressionValidator id=revMI 
								                ControlToValidate="MI"
								                ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
								                Display="Dynamic"
								                text = "Maximum length 3 digits and 2 decimal points. "
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="DOBI" ItemStyle-Width="10%" SortExpression="DOBI">
						                <ItemTemplate>
							                <%# FormatNumber(Container.DataItem("DOBI"),2) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="DOBI" MaxLength="6" width="100%"
								                Text='<%# FormatNumber(Container.DataItem("DOBI"),2) %>'
								                runat="server"/>
							                <asp:RangeValidator display=dynamic id=rvDOBI
								                ControlToValidate="DOBI"
								                MinimumValue="0"
								                MaximumValue="100"
								                Type="Double"
								                EnableClientScript="true"
								                Text="The value must be within percentage"
								                runat="server"/>
							                <asp:RegularExpressionValidator id=revDOBI 
								                ControlToValidate="DOBI"
								                ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
								                Display="Dynamic"
								                text = "Maximum length 3 digits and 2 decimal points. "
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Rain Fall MM" ItemStyle-Width="20%" SortExpression="MM">
						                <ItemTemplate>
							                <%# FormatNumber(Container.DataItem("MM"),2) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="MM" MaxLength="21" width="100%"
								                Text='<%# FormatNumber(Container.DataItem("MM"),2) %>'
								                runat="server"/>
							                <asp:RangeValidator display=dynamic id=rvMM
								                ControlToValidate="MM"
								                MinimumValue="0"
								                MaximumValue="100"
								                Type="Double"
								                EnableClientScript="true"
								                Text="The value must be within percentage"
								                runat="server"/>
							                <asp:RegularExpressionValidator id=revMM 
								                ControlToValidate="MM"
								                ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
								                Display="Dynamic"
								                text = "Maximum length 3 digits and 2 decimal points. "
								                runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="S.UpdateDate">
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
					                <asp:TemplateColumn HeaderText="Status" ItemStyle-Width="5%" SortExpression="S.Status">
						                <ItemTemplate>
							                <%# objPD.mtdGetPOMStaticStatus(Container.DataItem("Status")) %>
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
					                <asp:TemplateColumn ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Right>					
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Statistic" runat="server"/>
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
