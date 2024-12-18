<%@ Page Language="vb" src="../../../include/PU_setup_UserGroup.aspx.vb" Inherits="PU_setup_UserGroup" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>



<html>
	<head>
		<title>Setup COA Parameter</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form id="frmVehicleType" runat="server"  class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="sortcol" Visible="False" Runat="server"/>	
			           		
			                        <table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="3"><UserControl:MenuGLSetup id=menuGL runat="server" /></td>
                    <td colspan="1">
                    </td>
				</tr>
				<tr>
					<td class="mt-h" style="height: 21px"><asp:label id="lblTitle" runat="server" /></td>
                    <td align="right" colspan="1" style="height: 21px">
                    </td>
					<td align="right" style="height: 21px" >
                        &nbsp;</td>
                    <td align="right" colspan="1" style="height: 21px">
                    </td>
				</tr>
                                        <tr>
                                            <td class="mt-h" style="height: 21px">
                                            </td>
                                            <td align="right" colspan="1" style="height: 21px">
                                            </td>
                                            <td align="right" style="height: 21px">
                                            </td>
                                            <td align="right" colspan="1" style="height: 21px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="mt-h" style="height: 21px">
                                                <div style="text-align: left">
                                                    <table style="width: 50%; height: 50%" class="font9Tahoma">
                                                        <tr>
                                                            <td style="width: 100px">
                                                User :</td>
                                                            <td colspan="4">
                                                                <asp:DropDownList ID="DDLUser" runat="server" Width="100%">
                                                                </asp:DropDownList></td>
                                                            <td colspan="1" style="width: 45px">
                                                            </td>
                                                            <td colspan="1" style="width: 140px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px">
                                                            </td>
                                                            <td style="width: 100px">
                                                            </td>
                                                            <td style="width: 100px">
                                                            </td>
                                                            <td style="width: 100px">
                                                            </td>
                                                            <td style="width: 100px">
                                                            </td>
                                                            <td style="width: 45px">
                                                            </td>
                                                            <td style="width: 140px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                Item Group :</td>
                                                            <td colspan="4" valign="top">
                                                                <asp:DropDownList ID="DDLItemGroup" runat="server" Width="100%">
                                                                </asp:DropDownList>&nbsp;</td>
                                                            <td colspan="1" style="width: 45px" valign="top">
                                                                <asp:ImageButton ID="btnAdd" runat="server" AlternateText="Add"
                                                                  OnClick= "AddBtn_Click"  ImageUrl="../../images/butt_add.gif" />&nbsp;</td>
                                                            <td colspan="1" style="width: 140px" valign="top">
                                                                <asp:ImageButton ID="btnAddall" runat="server" AlternateText="Add"
                                                                  OnClick= "AddBtnALL_Click"  ImageUrl="../../images/butt_addall.gif" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                &nbsp;</td>
                                                            <td colspan="4" valign="top">
                                                                &nbsp;</td>
                                                            <td colspan="1" style="width: 45px" valign="top">
                                                                &nbsp;</td>
                                                            <td colspan="1" style="width: 140px" valign="top">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td align="right" colspan="1" style="height: 21px">
                                            </td>
                                            <td align="right" style="height: 21px">
                                            </td>
                                            <td align="right" colspan="1" style="height: 21px">
                                            </td>
                                        </tr>
				<tr>
					<td colspan=3 width=100% class="mb-c">
					</td>
                    <td class="mb-c" colspan="1" width="100%">
                    </td>
				</tr>
				<tr>
					<TD colspan = 3 >					
					<asp:DataGrid id="EventData"
						AutoGenerateColumns="False" width="100%" runat="server"
						GridLines = None
						Cellpadding = "2"
						OnDeleteCommand="DEDR_Delete"
						AllowPaging="True"
						Pagesize="20" 						
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"
                            class="font9Tahoma">	
							 
							<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>						
					<Columns>					
					<asp:TemplateColumn SortExpression="ProdTypeCode" HeaderText="Item Group">
						<ItemTemplate>
						   <asp:Label Text=<%# Container.DataItem("ProdTypeCode") %> id="lblProdTypeCode" runat="server" />						
						</ItemTemplate>
                        <ItemStyle Width="25%" />
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Description" HeaderText="Description">
						<ItemTemplate>
						   <asp:Label Text=<%# Container.DataItem("Description") %> id="lblProdTypeDesc" runat="server" />						
						</ItemTemplate>
                        <ItemStyle Width="35%" />
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
                        <ItemStyle Width="15%" />
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="UpdateID">
						<ItemTemplate>
							<%#Container.DataItem("UpdateID")%>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UserName" Readonly=TRUE size=8 
								Text='<%# Session("SS_USERID") %>'
								Visible=False runat="server"/>
						</EditItemTemplate>
                        <ItemStyle Width="15%" />
					</asp:TemplateColumn>	
					
					
					<asp:TemplateColumn>					
						<ItemTemplate>							
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>							
						</ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>
					</Columns>
                        <PagerStyle Visible="False" />
					</asp:DataGrid><BR>
                        <asp:ImageButton ID="NewSuppBtn" runat="server" AlternateText="New Supplier" ImageUrl="../../images/butt_new.gif"
                            OnClick="NewSuppBtn_Click" />&nbsp;<asp:ImageButton ID="btnDelete" runat="server"
                                AlternateText=" Delete " CausesValidation="false" ImageUrl="../../images/butt_delete.gif"
                                OnClick="btnDelete_Click" Visible="true" />&nbsp;<asp:ImageButton ID="btnUnDelete"
                                    runat="server" AlternateText="Undelete" ImageUrl="../../images/butt_undelete.gif"
                                    OnClick="btnUnDelete_Click" Visible="true" />&nbsp;<asp:ImageButton ID="btnBack"
                                        runat="server" AlternateText="  Back  " CausesValidation="False" ImageUrl="../../images/butt_back.gif"
                                        OnClick="btnBack_Click" /></td>
                    <td colspan="1">
                    </td>
				</tr>
				<tr>
					<TD colspan = 3 >					
                                            &nbsp;</td>
                    <td colspan="1">
                        &nbsp;</td>
				</tr>
				<tr>
					<td align=right colspan="3">
                        &nbsp;</td>
                    <td align="right" colspan="1">
                        &nbsp;</td>
				</tr>
				<tr>					
                    <td align="left" colspan="1">
                    </td>
				</tr>
			</table>


        <br />
        </div>
        </td>
        </tr>
        </table>


			</FORM>
		</body>
</html>
