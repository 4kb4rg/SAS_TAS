<%@ Page Language="vb" src="../../../include/GL_Setup_BalanceSheet.aspx.vb" Inherits="GL_Setup_BalanceSheet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Balance Sheet Statement Template</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		<form id=frmTmpl runat="server"  class="main-modul-bg-app-list-pu">
			<asp:label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblCompId visible=false runat=server />
			<asp:label id=blnUpdate Visible=false Runat=server/>
			<asp:label id=lblOper visible=false runat=server />
			<asp:label id=lblAccount visible=false runat=server />
			
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
							<td><strong>BALANCE SHEET TEMPLATE</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="TmplList"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnItemDataBound="DataGrid_ItemCreated"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>				
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
							<asp:Label id="lblAccYear" Text=<%#Container.DataItem("AccYear")%> visible=false runat="server" />
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
							<asp:Label id="lblStmtType" Text=<%#Container.DataItem("StmtType")%> visible=false runat="server" />
							<asp:Label id="lblAccYear" Text=<%#Container.DataItem("AccYear")%> visible=false runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText ="Display Type" >
						<ItemStyle Width="10%" />
						<ItemTemplate>
							<%# objGLSetup.mtdGetRowType(Container.DataItem("RowType")) %>
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
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
					    <td align="left" ColSpan=6>
						    <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_add.gif" AlternateText="Add Item" runat="server"/>
						    <asp:ImageButton id=ibSave OnClick="onclick_GenStmt" imageurl="../../images/butt_savetemplate.gif" AlternateText="Save template" runat="server"/>
					    </td>
				    </tr>
				    <tr>
					    <td align="left" ColSpan=6>
						    <b><asp:label id=lblErrCalc runat="server"/></b>
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


		</FORM>
		</body>
</html>
