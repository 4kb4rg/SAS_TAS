<%@ Page Language="vb" src="../../../include/WS_Setup_ServTypeDet.aspx.vb" Inherits="WS_ServTypeDet"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Service Type Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
			<Form id=frmServTypeDet runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<Input Type=Hidden id=ServType runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6">
						<UserControl:MenuWS id=menuWS runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">SERVICE TYPE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>Service Type :* </td>
					<td width=30%>
						<asp:TextBox id=txtServType width=50% maxlength=8 runat=server />
						<asp:RequiredFieldValidator id=rfvCode display=dynamic runat=server 
							ErrorMessage="<br>Please enter service type. " 
							ControlToValidate=txtServType />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtServType"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
							<asp:label id=lblErrBlankST text="Please enter Service Type" visible=false forecolor=red runat=server />
							<asp:label id=lblErrDupST text="Duplicate Service Type" visible=false forecolor=red runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /> </td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td>Description : </td>
					<td><asp:TextBox id=txtDescription width=100% maxlength=128 runat=server /> </td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Label id=lblAccCode runat=server /> Code :*</td>
					<td><asp:dropdownlist id=ddlAccCode width=100% runat=server></asp:dropdownlist></td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25></td>
					<td></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:DataGrid id=dgServTypeDet runat="server"
							AutoGenerateColumns="false" width="100%"
							GridLines = none
							Cellpadding = "2"
							OnUpdateCommand="DEDR_Update"
							OnCancelCommand="DEDR_Cancel"
							OnDeleteCommand="DEDR_Delete"
							AllowPaging="True" 
							AllowCustomPaging="False"
							PageSize="15" 
							PagerStyle-Visible="False"
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
								<asp:TemplateColumn HeaderText="Work Code" SortExpression="WorkCode">
									<ItemStyle width=25% />														
									<ItemTemplate>
										<asp:label id=WorkCode text='<%# Container.DataItem("WorkCode")%>' runat=server />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList id=ddlWorkCode width=100% runat=server /><BR>
										<asp:Label id="lblErrDupWC"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
									</EditItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Description" SortExpression="Description">
									<ItemStyle width=45% />														
									<ItemTemplate>
										<%# Container.DataItem("Description")%>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label id=lblDescription runat=server />
									</EditItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Charge Rate" SortExpression="ChrgRate">
									<ItemStyle width=15% />														
									<ItemTemplate>
										<%# Container.DataItem("ChrgRate")%>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label id=lblChrgRate runat=server />
									</EditItemTemplate>
								</asp:TemplateColumn>
					
								<asp:TemplateColumn>
									<ItemStyle width=15% />														
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton id=Update CommandName=Update Text=Save runat=server />
										<asp:LinkButton id=Cancel CommandName=Cancel Text=Cancel CausesValidation=False runat=server />
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="left" colspan=6>
						<asp:Button id=AddBtn OnClick="DEDR_Add" Text="Add Work Code" runat="server"/>
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn imageurl="../../images/butt_save.gif" AlternateText="  Save  " onClick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn imageurl="../../images/butt_delete.gif" AlternateText=" Delete " CausesValidation=false onClick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn imageurl="../../images/butt_undelete.gif" AlternateText="Undelete" onClick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn imageurl="../../images/butt_back.gif" AlternateText="  Back  " onClick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						&nbsp;</td>
				</tr>
			</table>

        <br />
        </div>
        </td>
        </tr>
        </table>

		</form>
	</body>
</html>
