<%@ Page Language="vb" Debug="true"  src="../../../include/PR_Setup_Hari_Pengganti_Estate.aspx.vb" Inherits="PR_Setup_Hari_Pengganti_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Holiday List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="false" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:label id="lblValidate" visible="false" text="Please enter " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="false" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR TANGGAL &nbsp;PENGGANTI JADWAL KERJA</strong><hr style="width :100%" />   
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
								<td height="26" valign=bottom style="width: 14%">
                                    &nbsp;Periode :<BR><asp:TextBox id=srcperiode width=100% maxlength="4" runat="server"/></td>
								<td height="26" valign=bottom style="width: 14%">
                                    </td>
								<td height="26" valign=bottom colspan="3">
                                    &nbsp;<BR></td>
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
						                AutoGenerateColumns="False" width="100%" runat="server"
						                GridLines = None
						                Cellpadding = "2"
						                OnEditCommand="DEDR_Edit"
						                OnUpdateCommand="DEDR_Update"
						                OnCancelCommand="DEDR_Cancel"
						                OnDeleteCommand="DEDR_Delete"
						                AllowPaging="True" 
						                OnPageIndexChanged="OnPageChanged"
						                Pagerstyle-Visible="False"
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="True"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					                <Columns>					
					                <asp:TemplateColumn HeaderText="Tanggal Kerja" SortExpression="WorkDate">
						                <ItemTemplate>
							                <%#objGlobal.GetLongDate(Container.DataItem("WorkDate"))%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="WorkDate" width=100% MaxLength="64"
								                Text='<%#  objGlobal.GetShortDate(strDateFormat,Container.DataItem("WorkDate")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validatehol display=Dynamic runat=server 
								                ControlToValidate=WorkDate/>
			   			                    <asp:label id="lblDupMsg" Text="Date already exist" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
                   	                <asp:TemplateColumn HeaderText="Tanggal Pengganti" SortExpression="Chg_WorkDate">
						                <ItemTemplate>
							                <%#objGlobal.GetLongDate(Container.DataItem("Chg_WorkDate"))%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="Chg_WorkDate" width=100% MaxLength="64"
								                Text='<%#  objGlobal.GetShortDate(strDateFormat,Container.DataItem("Chg_WorkDate")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validatehol_Chg display=Dynamic runat=server 
								                ControlToValidate=Chg_WorkDate/>
			   			                    <asp:label id="lblDupMsg_Chg" Text="Date already exist" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Deskripsi" SortExpression="Description">
						                <ItemTemplate>
							                <%# Container.DataItem("Description") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="Description" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("Description")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								                ControlToValidate=Description />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
										
					                <asp:TemplateColumn HeaderText="Tgl update" SortExpression="A.UpdateDate">
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
					                <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
						                <ItemTemplate>
							                <%# objHR.mtdGetDeptCodeStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                <asp:TextBox id="Status" Readonly=TRUE Visible = False
								                Text='<%# Container.DataItem("Status")%>' runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="UserName" Readonly=TRUE size=8 
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
                                        <PagerStyle Visible="False" />
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
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Holiday Code" runat="server"/>&nbsp;
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


			</Form>
		</body>
</html>
