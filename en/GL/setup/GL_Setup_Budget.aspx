<%@ Page Language="vb" src="../../../include/GL_Setup_Budget.aspx.vb" Inherits="GL_Setup_Budget" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Budget Setting List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			
		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuGLSetup id=menuGL runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> DAFTAR SETTING BUDGET</strong><hr style="width :100%" />   
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
								<td width="8%">Periode :<br><asp:DropDownList id="lstAccYear" width="100%" runat="server"/></td> 
								<td width="8%" height="26">Divisi :<br><asp:DropDownList id="srchDiv" width="100%" runat="server"  OnSelectedIndexChanged="srchDiv_OnSelectedIndexChanged" AutoPostBack=true /></td>
								<td width="8%" height="26">T.Tanam:<br><asp:DropDownList id="srchTTnm" width="100%" runat="server" OnSelectedIndexChanged="srchTTnm_OnSelectedIndexChanged" AutoPostBack=true/></td>
								<td width="15%" height="26">Kode Account :<br><asp:TextBox id="srchVehActCd" width="100%" maxlength="32" runat="server"/></td>
								<td width="20%" height="26">Deskripsi :<br><asp:TextBox id="srchDescription" width="100%" maxlength="100" runat="server"/></td>
								<td width="25%" height="26">Sub Kategori :<br><asp:DropDownList id="srchSubCat" width="100%" runat="server" OnSelectedIndexChanged="srchTTnm_OnSelectedIndexChanged" AutoPostBack=true /></td>
								<td width="10%" height="26">Status :<br><asp:DropDownList id="srchStatus" width="100%" runat="server"/></td>
								<td width="20%" height="26">Update Oleh :<br><asp:TextBox id="srchUpdBy" width="100%" maxlength="50" runat="server"/></td>
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
					
					<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Periode">
						<ItemTemplate>
							<%# Container.DataItem("PeriodeStart") + " - " + Container.DataItem("PeriodeEnd") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="PStart" size=6 Text='<%# trim(Container.DataItem("PeriodeStart")) %>' runat="server"/>
							<asp:TextBox id="PEnd" size=6 Text='<%# trim(Container.DataItem("PeriodeEnd")) %>' runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>		
					
					<asp:TemplateColumn ItemStyle-Width="5%" SortExpression="subcategory" HeaderText="Sub">
						<ItemTemplate>
							<%#Container.DataItem("subcategory")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:label id=lblType text='<%# trim(Container.DataItem("subcategory")) %>' runat=server/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="5%"  HeaderText="Divisi">
						<ItemTemplate>
							<%#Container.DataItem("CodeBlkGrp")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:label id=lbldiv  text='<%# trim(Container.DataItem("CodeBlkGrp")) %>' runat=server/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn ItemStyle-Width="8%"  HeaderText="T.Tanam">
						<ItemTemplate>
							<%#Container.DataItem("CodeBlk")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:label id=lblttnm text='<%# trim(Container.DataItem("CodeBlk")) %>' runat=server/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					
					<asp:TemplateColumn ItemStyle-Width="25%" SortExpression="a.acccode" HeaderText="Account">
						<ItemTemplate>
							<%# Container.DataItem("accdesc")  %>
						</ItemTemplate>
						<EditItemTemplate>													
							<GG:AutoCompleteDropDownList id="ddlAccount" width=100% runat=server>
							</GG:AutoCompleteDropDownList>
							<asp:label id=lblAccCode visible=false text='<%# trim(Container.DataItem("acccode")) %>' runat=server/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="25%" SortExpression="a.itemcode" HeaderText="Item">
						<ItemTemplate>
							<%# Container.DataItem("itemdesc") %>
						</ItemTemplate>
						<EditItemTemplate>													
							<GG:AutoCompleteDropDownList id="ddlItem" width=100% runat=server>
							</GG:AutoCompleteDropDownList>
							<asp:label id=lblItem visible=false text='<%# trim(Container.DataItem("itemcode")) %>' runat=server/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="5%" SortExpression="a.coalevel" HeaderText="Level">
						<ItemTemplate>
							<%#Container.DataItem("coalevel")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:DropDownList id="ddllevel" width=90% runat=server>
							</asp:DropDownList>
							<asp:label id=lbllevel visible=false text='<%# trim(Container.DataItem("coalevel")) %>' runat=server/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="a.UpdateDate">
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
					
					<asp:TemplateColumn HeaderText="Status" SortExpression="a.Status">
						<ItemTemplate>
							<%# objGLSetup.mtdGetVehicleExpenseGrpStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							<asp:TextBox id="Status" Readonly=TRUE Visible = False
								Text='<%# Container.DataItem("Status")%>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="Usr.UserName">
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UserName" Readonly=TRUE size=8 
								Text='<%# Session("SS_USERID") %>'
								Visible=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>			
					
					<asp:TemplateColumn ItemStyle-HorizontalAlign=center>
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
					            <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Setup Budget" runat="server"/>
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
        
        		
			<Input type=hidden id=hidStatusEdited value="" runat=server/>
			</FORM>
		</body>
</html>
