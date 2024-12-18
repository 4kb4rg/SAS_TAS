<%@ Page Language="vb" src="../../../include/GL_trx_BudgetPupuk.aspx.vb" Inherits="GL_trx_BudgetPupuk" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Budget Setting List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="frmMain" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." forecolor="Red" runat=server />
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
							<td><strong><asp:label id="lblTitle" runat="server" />DAFTAR SETTING BUDGET PEMUPUKAN</strong><hr style="width :100%" />   
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
								<td width="15%" height="26">Kode Item :<br><asp:TextBox id="srchVehActCd" width="100%" maxlength="32" runat="server"/></td>
								<td width="20%" height="26">Deskripsi :<br><asp:TextBox id="srchDescription" width="100%" maxlength="100" runat="server"/></td>
								
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
							            AllowSorting=True
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>					
					<Columns>					
					
					<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Periode">
						<ItemTemplate>
							<%# Container.DataItem("AccYear") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:label id="PStart" size=4 Text='<%# trim(Container.DataItem("AccYear")) %>' runat="server"/>
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
					
					<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Rotasi">
						<ItemTemplate>
							<%#Container.DataItem("Rotasi")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:TextBox id="txtrotasi" MaxLength=2 Text='<%# trim(Container.DataItem("rotasi")) %>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Ha">
						<ItemTemplate>
							<%#Container.DataItem("ha")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:TextBox id="txtha" Text='<%# trim(Container.DataItem("ha")) %>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Pokok">
						<ItemTemplate>
							<%#Container.DataItem("pokok")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:TextBox id="txtpokok"  Text='<%# trim(Container.DataItem("pokok")) %>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Kg">
						<ItemTemplate>
							<%#Container.DataItem("kg")%>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:TextBox id="txtkg" Text='<%# trim(Container.DataItem("Kg")) %>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn ItemStyle-Width="7%" HeaderText="Kg/Ha">
						<ItemTemplate>
							<%#Container.DataItem("Kgha")%>
						</ItemTemplate>
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
                        <Input type=hidden id=hidStatusEdited value="" runat=server/>
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
