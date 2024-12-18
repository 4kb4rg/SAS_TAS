<%@ Page Language="vb" src="../../../include/IN_setup_prodCategory.aspx.vb" Inherits="IN_ProdCategory" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINSetup" src="../../menu/menu_INsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Product Category List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
         <form runat="server" ID="Form1" class="main-modul-bg-app-list-pu">
        <table cellpadding="0" cellspacing="0" style="width: 100%" class=font9Tahoma" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
		   
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:label id=lblList visible=false text=" LIST" runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />

			<table border="0" cellspacing="1" cellpadding="1" width="100%" class=font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuINSetup id=menuIN runat="server" /></td>
				</tr>
				<tr class="font9Tahoma">
					<td  colspan="4" width=60%><strong> <asp:label id="lblTitle" runat="server" /> LIST</strong></td>
					<td align="right" colspan="2" width=40%> <asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6 width=100% >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr style="background-color:#FFCC00">
								<td width="20%" height="26" valign=bottom >
									<asp:label id="lblProdCatCode" runat="server" /> :<BR>
									<asp:TextBox id=srchProdCatCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="42%" height="26" valign=bottom>
									<asp:label id="lblDescription" runat="server" /> :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button  Text="Search" OnClick=srchBtn_Click class="button-small"  runat="server"/></td>
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
						Pagesize="15" 
						OnPageIndexChanged="OnPageChanged"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"  CssClass="font9Tahoma">
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
					
					<asp:TemplateColumn SortExpression="ProdCatCode">
						<ItemTemplate>
							<%# Container.DataItem("ProdCatCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="ProdCatCode" MaxLength="8" width=95%
									Text='<%# Trim(Container.DataItem("ProdCatCode")) %>'
									runat="server"/>
							<BR>
							<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
									ControlToValidate=ProdCatCode />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="ProdCatCode"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn SortExpression="Description">
						<ItemTemplate>
							<%# Container.DataItem("Description") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="Description" width=100% MaxLength="64"
								Text='<%# trim(Container.DataItem("Description")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateDesc display=dynamic runat=server 
									ControlToValidate=Description />															
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Last Update" SortExpression="PCat.UpdateDate">
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UpdateDate"  Visible=False size=8 
								Text='<%# objGlobal.GetLongDate(Now()) %>'
								runat="server"/>
							<asp:TextBox id="CreateDate" Visible=False
								Text='<%# Container.DataItem("CreateDate") %>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status" SortExpression="PCat.Status">
						<ItemTemplate>
							<%# objIN.mtdGetProductCategoryStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="StatusList" size=1  Visible=False runat=server />
							<asp:TextBox id="Status" Readonly=TRUE Visible = False
								Text='<%# Container.DataItem("Status")%>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UserName"  Visible=False size=8 
								Text='<%# Session("SS_USERID") %>'
								runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
					
						<ItemTemplate>
						<asp:LinkButton id="Edit" CommandName="Edit"   Text="Edit"
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
					</asp:DataGrid><BR>
					</td>
					</tr>
				<tr>
				<td align=right colspan="6">
					<img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /><asp:ImageButton 
                        id="btnPrev" runat="server" imageurl="../../../images/btprev.png" 
                        alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
					<asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			        <asp:Imagebutton id="btnNext" runat="server"  
                        imageurl="../../../images/btnext.png" alternatetext="Next" 
                        commandargument="next" onClick="btnPrevNext_Click" />
				    <img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Product Category" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onclick="btnPreview_Click" runat="server"/>
					&nbsp;</td>
				</tr>
			</table>
                    </div>
            </td>
        </tr>
        </table>
				</FORM>

		</body>
</html>
