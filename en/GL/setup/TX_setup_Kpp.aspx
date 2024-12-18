<%@ Page Language="vb" src="../../../include/Tx_setup_Kpp.aspx.vb" Inherits="Tx_setup_Kpp" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Kantor Pelayanan Pajak List</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
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

			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">

				<tr>
					<td  colspan="4" width="60%"><strong>DAFTAR KANTOR PELAYANAN PAJAK</strong> </td>
					<td align="right" colspan="2" width="40%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   </td>
				</tr>
				<tr>
					<td colspan=6 width=100% style="background-color:#FFCC00" >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td width="10%" height="26" valign=bottom>Kode Kantor :<BR><asp:TextBox id=srchDeptCode width=100% maxlength="10" runat="server"/></td>
								<td width="45%" height="26" valign=bottom>Unit Kerja :<BR><asp:TextBox id=srchName width=100% maxlength="128" runat="server"/></td>
								<td width="20%" height="26" valign=bottom>Diupdate :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button Text="Search" OnClick=srchBtn_Click class="button-small" runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<TD colspan = 6 >					
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
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					<Columns>					
					<asp:TemplateColumn HeaderText="Kode KPP" SortExpression="KPPCode">
						<ItemTemplate>
							<%#Container.DataItem("KPPCode")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="KPPCode" MaxLength="10" width=95%
								Text='<%# trim(Container.DataItem("KPPCode")) %>' runat="server"/><BR>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								ControlToValidate=KPPCode />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="KPPCode"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
							<asp:label id="lblDupMsg" Text="Code already exist" Visible = false forecolor=red Runat="server"/>		
						</EditItemTemplate>
					</asp:TemplateColumn>						
					<asp:TemplateColumn HeaderText="Kode Alias" SortExpression="KPPInit">
						<ItemTemplate>
							<%#Container.DataItem("KPPInit")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="KPPInit" width=100% MaxLength="10"
								Text='<%# trim(Container.DataItem("KPPInit")) %>' runat="server"/>
							<asp:RequiredFieldValidator id=validateBs display=Dynamic runat=server 
								ControlToValidate=KPPInit />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Kerja" SortExpression="KPPDescr">
						<ItemTemplate>
							<%#Container.DataItem("KPPDescr")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="KPPDescr" width=100% MaxLength="120"
								Text='<%# trim(Container.DataItem("KPPDescr")) %>' runat="server"/>
							<asp:RequiredFieldValidator id=validateDes display=Dynamic runat=server 
								ControlToValidate=KPPDescr />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Alamat" SortExpression="KPPAddr">
						<ItemTemplate>
							<%#Container.DataItem("KPPAddr")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="KPPAddr" width=100% MaxLength="100"
								Text='<%# trim(Container.DataItem("KPPAddr")) %>' runat="server"/>
																	
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Telp" SortExpression="KppPhone">
						<ItemTemplate>
							<%#Container.DataItem("KppPhone")%>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="KppPhone" width=100% MaxLength="100"
								Text='<%# trim(Container.DataItem("KppPhone")) %>' runat="server"/>
																			
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
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
						<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="80%" ColSpan=6>
						<asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Beras Rate Code" runat="server"/>
					</td>
				</tr>
                </table>
                </div>
                </td>
                </tr>
			</table>
			<input id="isNew" type="hidden" runat="server" />
			</Form>
		</body>
</html>
