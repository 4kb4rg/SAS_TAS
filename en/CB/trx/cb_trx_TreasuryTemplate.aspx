<%@ Page Language="vb" src="../../../include/cb_trx_TreasuryTemplate.aspx.vb" Inherits="cb_trx_TreasuryTemplate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Treasury Cash Flow Report Template Detail</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		<form id=frmTmpl class="main-modul-bg-app-list-pu"  runat="server">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			<asp:label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblCompId visible=false runat=server />
			<asp:label id=blnUpdate Visible=false Runat=server/>
			<asp:label id=lblOper visible=false runat=server />
			<asp:label id=lblAccount visible=false runat=server />
			
			<table border=0 cellspacing=1 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuCB id=menuCB runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"><strong>TREASURY CASH FLOW REPORT TEMPLATE DETAIL </strong> </td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				
				<TR>
					<TD height="25" width = 20% >Report Code :*</TD>
					<TD width = 75%  ><asp:textbox id="txtCode" width="30%" maxlength="32" runat="server" />
						<asp:RequiredFieldValidator id="rfvCode" runat="server" ErrorMessage="Required" ControlToValidate="txtCode"
							display="dynamic" />
					</TD>
					<TD width = 1%>&nbsp;</TD>
					<TD width = 1%>&nbsp;</TD>
					<TD width = 1%>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25">Name :*</TD>
					<TD><asp:textbox id="txtName" width="80%" maxlength="32" runat="server" />
						<asp:RequiredFieldValidator id="rfvName" runat="server" ErrorMessage="Required" ControlToValidate="txtName"
							display="dynamic" />
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25">Description :</TD>
					<TD><asp:textbox id="txtDescription" width="80%" maxlength="32" runat="server" />
						
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
			</table>
			
			<table border=0 cellspacing=1 cellpadding=2 width=100%>
				<tr>
					<td colspan=6 align=left>					
					<asp:DataGrid id="TmplList"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnItemDataBound="DataGrid_ItemCreated"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"  class="font9Tahoma">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />	
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
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="5%" />
						<ItemTemplate>
							<asp:label id="lblNo" runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Seq.">
						<ItemStyle Width="5%" />
						<ItemTemplate>
							<asp:Label id="lblDispSeq" text=<%# Container.DataItem("DispSeq") %> runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtDispSeq" text=<%# Trim(Container.DataItem("DispSeq")) %> MaxLength=4 width=95% runat="server" />
							<asp:RequiredFieldValidator id=rfvDispSeq 
								display=dynamic runat=server 
								text="Please enter Sequence."
								ControlToValidate=txtDispSeq />
							<asp:RangeValidator id="RgDispSeq"
								ControlToValidate="txtDispSeq"
								MinimumValue="0"
								MaximumValue="9999"
								Type="Integer"
								EnableClientScript="True"
								Text="Sequence must be in positive number."
								runat="server" 
								display="dynamic"/>	 
							<asp:Label id="lblDupSeq" text="Sequence is already in used." forecolor=red visible=false runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
						
					<asp:TemplateColumn HeaderText="Description">
						<ItemStyle Width="30%"/>
						<ItemTemplate>
							<asp:Label id=lblDescription Text=<%#Container.DataItem("Description")%> runat="server" />
							<asp:Label id="lblRowId" Text=<%#Container.DataItem("RowId")%> visible=false runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:textbox id="txtDescription" Text=<%#Container.DataItem("Description")%> width=100% runat=server />
							<asp:RequiredFieldValidator id=rfvDesc 
								display=dynamic 
								runat=server 
								text="Please enter Description."
								ControlToValidate=txtDescription
								visible=false />
							<asp:Label id=lblRowId Text=<%#Container.DataItem("RowId")%> visible=False runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText ="Display Type" >
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# objCBTrx.mtdGetRowType(Container.DataItem("RowType")) %>
							<asp:Label id="lblRowType" Text=<%#Container.DataItem("RowType")%> visible=false runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="ddlRowType" AutoPostback=True OnSelectedIndexChanged=ddlCheckType runat=server />
							<asp:Label id="prevRowType" Text=<%#Container.DataItem("RowType")%> visible=false runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText ="Reference" >
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# Container.DataItem("Refno") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtRefNo" text=<%#Trim(Container.DataItem("RefNo"))%> MaxLength="64" width=95% visible=false runat="server" />
							<asp:Label id="lblDupRef" text="Reference Number is already in used." forecolor=red visible=false runat="server" />
							<asp:RequiredFieldValidator id=rfvRefNo
								display=dynamic 
								runat=server 
								text="Please enter Reference Number."
								ControlToValidate=txtRefNo 
								visible=false />
						</EditItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Formula" >
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# Container.DataItem("Formula") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtFormula" text=<%#Trim(Container.DataItem("Formula"))%> MaxLength="2048" width=95% visible=false runat="server" />
							<asp:RequiredFieldValidator id=rfvFormula 
								display=dynamic 
								runat=server 
								text="Please enter Formula."
								ControlToValidate=txtFormula 
								visible=false />
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Beg Balance" >
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<asp:CheckBox id="chkBegBalance1" Checked= <%#Container.DataItem("BegBalance")%>  runat="server" Enabled ="false"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:CheckBox id="chkBegBalance" Checked= <%#Container.DataItem("BegBalance")%>  visible=false runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Account">
						<ItemStyle Width="10%"/>
						<ItemTemplate>
							<asp:Button id=btnAccount text="view" commandargument=<%#Container.DataItem("RowId")%> commandname=<%#Container.DataItem("Description")%> OnClick="onClick_Account" visible=false runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Button id=btnEditAccount text="view" OnClick="onClick_Account" runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>
						<ItemStyle Width="10%" />
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
				<tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_add.gif" AlternateText="Add Item" runat="server"/>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<b><asp:label id=lblErrCalc runat="server"/></b>
					</td>
				</tr>
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<TD colSpan="6">
						<asp:ImageButton ID="SaveBtn" CausesValidation="True" onclick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif"
							AlternateText="Save" Runat="server" />
						<asp:ImageButton ID="DeleteBtn" CausesValidation="False" Visible = "False" onclick="DeleteBtn_Click" ImageUrl="../../images/butt_delete.gif"
							AlternateText="Delete" Runat="server" />
						<asp:ImageButton ID="BackBtn" CausesValidation="False" onclick="BackBtn_Click" ImageUrl="../../images/butt_back.gif"
							AlternateText="Back" Runat="server" />
					</TD>
				</tr>
				
				
								
			</table>
</div>
</td>
</tr>
</table>
		</FORM>
		</body>
</html>
