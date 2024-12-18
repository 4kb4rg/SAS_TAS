<%@ Page Language="vb" src="../../../include/PR_Setup_Aktivitikategori_Estate.aspx.vb" Inherits="PR_Setup_Aktivitikategori_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>kategori List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body >
		    <form id=FrmMain runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:label id="SortExpression" Visible="false" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="false" Runat="server"></asp:label>
		


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
							<td><strong>DAFTAR KATEGORI AKTIVITI</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom>
                                    Kode Kategori :<BR><asp:TextBox id=srchDeptCode width=100% maxlength="8" runat="server"/></td>
								<td width="40%" height="26" valign=bottom>
                                    Deskripsi:<BR><asp:TextBox id=srchName width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26" valign=bottom></td>
								<td width="20%" height="26" valign=bottom></td>
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
						                    AllowPaging="True" 
						                    Allowcustompaging="False" 
						                    OnPageIndexChanged="OnPageChanged"
						                    Pagerstyle-Visible="False"
						                    OnSortCommand="Sort_Grid" 
						                    AllowSorting="True"
                                                            class="font9Tahoma">
								
							                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					                    <Columns>					
					                    <asp:TemplateColumn HeaderText="Kode Kategori" SortExpression="CatId">
						                    <ItemTemplate>
							                    <%#Container.DataItem("CatId")%>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="BlkGrpCode" MaxLength="2" width=10%
								                    Text='<%# trim(Container.DataItem("CatId")) %>' runat="server"/><BR>
							                    <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								                    ControlToValidate=BlkGrpCode />
							                    <asp:RegularExpressionValidator id=revCode 
								                    ControlToValidate="BlkGrpCode"
								                    ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                    Display="Dynamic"
								                    text="Alphanumeric without any space in between only."
								                    runat="server"/>
							                    <asp:label id="lblDupMsg" Text="Code already exist" Visible = false forecolor=red Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>	
					
					                    <asp:TemplateColumn HeaderText="Deskripsi" SortExpression="CatName">
						                    <ItemTemplate>
							                    <%#Container.DataItem("CatName")%>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="Description" width=100% MaxLength="50"
								                    Text='<%# trim(Container.DataItem("CatName")) %>' runat="server"/>
							                    <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								                    ControlToValidate=Description />															
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>		
					
								
					                    <asp:TemplateColumn>					
						                    <ItemTemplate>
							                    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
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
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Kategory Code" runat="server"/>
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


                <input id="isNew" type="hidden" runat="server" />
			</Form>
		</body>
</html>
