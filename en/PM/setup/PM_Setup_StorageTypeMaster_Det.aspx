<%@ Page Language="vb" Src="../../../include/PM_Setup_StorageTypeMaster_Det.aspx.vb" Inherits="PM_Setup_StorageTypeMaster_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Storage Type Master Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<asp:Label id="lblStorageTypeCode" runat="server" Visible="False"/>
			<asp:Label id="lblLineNo" runat="server" Visible="False"/>
			<asp:Label id="lblFormulaType" runat="server" Visible="False"/>
			<asp:Label id="lblTableCode" runat="server" Visible="False"/>
			<asp:Label id="lblFirstOperandType" runat="server" Visible="False"/>
			<asp:Label id="lblFirstOperandValue" runat="server" Visible="False"/>
			<asp:Label id="lblSecondOperandType" runat="server" Visible="False"/>
			<asp:Label id="lblSecondOperandValue" runat="server" Visible="False"/>
			<asp:Label id="lblFMLCreateDate" runat="server" Visible="False"/>
			<table cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuPDSetup id=MenuPD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="50%">STORAGE TYPE MASTER DETAILS</td>
					<td colspan="3" align=right width="50%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="20%" height=25>Storage Type Code :*</td>
					<td width="30%" valign=center>
						<asp:TextBox id="txtStorageTypeCode" runat="server" width=100% maxlength="8" />
						<asp:RequiredFieldValidator 
							id="rfvStorageTypeCode" 
							runat="server"  
							ControlToValidate="txtStorageTypeCode" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:label id="lblDupMsg"  Text="Record already exist" Visible = false forecolor=red Runat="server"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status :</td>
					<td width=25%><asp:Label id="lblStatus" runat="server"/></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description :*</td>
					<td><asp:TextBox id="txtDescription" runat="server" width=100% maxlength="128"/>
						<asp:RequiredFieldValidator 
							id="rfvDescription" 
							runat="server"  
							ControlToValidate="txtDescription" 
							text = "Field cannot be blank"
							display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/>
						<asp:Label id="txtCreateDate" runat="server" Visible="False"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Product Stored :*</td>
					<td><asp:DropDownList id="lstProductCode" width=100% runat="server" size="1" AutoPostBack=true OnSelectedIndexChanged="lstProductCode_OnSelectedIndexChanged" />
						<asp:RequiredFieldValidator id="validateProductCode" runat="server" 
								display="dynamic" 
								ControlToValidate="lstProductCode"
								Text="Please select product code." />
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblCPOStorageLocation" runat="server" visible = "False"  Text = " Storage Location : "/></td>
					<td><asp:RadioButton id=rbCPOStorageLocationMill text=" Mill" groupname=CPOStorageLocation checked=true runat=server visible = "False" /> 
						<asp:RadioButton id=rbCPOStorageLocationBulking text=" Bulking" groupname=CPOStorageLocation runat=server visible = "False" />
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td>
						<table id=tblFormula  class="mb-c" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server >
							<tr><td colspan=3><b><u>Formula</u></b></td></tr>
							<tr>
								<td width="20%" height=25>Line :*</td>
								<td width="30%">
									<asp:TextBox id="txtLineNo" runat="server" width=100% maxlength="3"/>
									<asp:RequiredFieldValidator 
										id="rfvLineNo" 
										runat="server"  
										ControlToValidate="txtLineNo" 
										text = "Field cannot be blank"
										display="dynamic" />
									<asp:RegularExpressionValidator id="revLineNo" 
										ControlToValidate="txtLineNo"
										ValidationExpression="\d{1,3}"
										Display="Dynamic"
										text = "Maximum length 3 digits and 0 decimal places"
										runat="server"/>
									<asp:label id="lblFMLDupMsg"  Text="Record already exist" Visible = false forecolor=red Runat="server"/>
								</td>
								<td width=50%>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Formula Type :*</td>
								<td>
									<asp:DropDownList id="lstFormulaType" width=100% runat="server" size="1" AutoPostBack=true OnSelectedIndexChanged="lstFormulaType_OnSelectedIndexChanged" />
									<asp:label id="lblFormulaTypeMsg"  Text="Please select formula type" Visible = false forecolor=red Runat="server"/>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Table Code :*</td>
								<td>
									<asp:DropDownList id="lstTableCode" width=100% runat="server" size="1"/>
									<asp:RequiredFieldValidator 
										id="rfvTableCode" 
										runat="server"  
										ControlToValidate="lstTableCode" 
										text = "Please select a code"
										display="dynamic" />
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>First Operand Type :*</td>
								<td><asp:DropDownList id="lstFirstOperandType" width=100% runat="server" size="1" AutoPostBack=true OnSelectedIndexChanged="lstDropDownList_OnSelectedIndexChanged" />
									<asp:RequiredFieldValidator 
										id="rfvFirstOperandType" 
										runat="server"  
										ControlToValidate="lstFirstOperandType" 
										text = "Please select first operand type"
										display="dynamic" />
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>First Operand Value :*</td>
								<td><asp:DropDownList id="lstFirstOperandValue" width=100% runat="server" size="1"/>
									<asp:RequiredFieldValidator 
										id="rfvFirstOperandValuelst" 
										runat="server"  
										ControlToValidate="lstFirstOperandValue" 
										text = "Please select a line"
										display="dynamic" />
									<asp:TextBox id="txtFirstOperandValue" runat="server" width=100% maxlength="21" />
									<asp:RequiredFieldValidator 
										id="rfvFirstOperandValue" 
										runat="server"  
										ControlToValidate="txtFirstOperandValue" 
										text = "Field cannot be blank"
										display="dynamic"/>
									<asp:RegularExpressionValidator id="revFirstOperandValue" 
										ControlToValidate="txtFirstOperandValue"
										ValidationExpression="\d{1,10}\.\d{1,10}|\d{1,10}"
										Display="Dynamic"
										text = "Maximum length 10 digits and 10 decimal places"
										runat="server"/>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Second Operand Type :*</td>
								<td><asp:DropDownList id="lstSecondOperandType" width=100% runat="server" size="1" AutoPostBack=true OnSelectedIndexChanged="lstDropDownList_OnSelectedIndexChanged" />
									<asp:RequiredFieldValidator 
										id="rfvSecondOperandType" 
										runat="server"  
										ControlToValidate="lstSecondOperandType" 
										text = "Please select second operand type"
										display="dynamic" />
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td height=25>Second Operand Value :*</td>
								<td><asp:DropDownList id="lstSecondOperandValue" width=100% runat="server" size="1"/>
									<asp:RequiredFieldValidator 
										id="rfvSecondOperandValuelst" 
										runat="server"  
										ControlToValidate="lstSecondOperandValue" 
										text = "Please select a line"
										display="dynamic" />
									<asp:DropDownList id="lstMatchType" width=100% runat="server" size="1"/>
									<asp:RequiredFieldValidator 
										id="rfvMatchType" 
										runat="server"  
										ControlToValidate="lstMatchType" 
										text = "Please select a match type"
										display="dynamic" />
									<asp:TextBox id="txtSecondOperandValue" runat="server" width=100% maxlength="21" />
									<asp:RequiredFieldValidator 
										id="rfvSecondOperandValue" 
										runat="server"  
										ControlToValidate="txtSecondOperandValue" 
										text = "Field cannot be blank"
										display="dynamic"/>
									<asp:RegularExpressionValidator id="revSecondOperandValue" 
										ControlToValidate="txtSecondOperandValue"
										ValidationExpression="\d{1,10}\.\d{1,10}|\d{1,10}"
										Display="Dynamic"
										text = "Maximum length 10 digits and 10 decimal places"
										runat="server"/>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td colspan="3">
									<asp:ImageButton id="FMLAdd" imageurl="../../images/butt_add.gif" onclick="btnFMLAdd_Click" runat="server" AlternateText="Add"/>
									<asp:ImageButton id="FMLSave" imageurl="../../images/butt_save.gif" onclick="btnFMLSave_Click" runat="server" AlternateText="Save"/>
								</td>
							</tr>
							<tr>
								<td colspan="3">
                                            &nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>



				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" runat=server >
							<tr>
								<td colspan=6>					
									<asp:DataGrid id=EventData
										AutoGenerateColumns=false width=100% runat=server
										GridLines=none 
										Cellpadding=2 
										OnEditCommand="DEDR_Edit"
										OnCancelCommand="DEDR_Cancel"
										OnDeleteCommand="DEDR_Delete"
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
											<asp:TemplateColumn HeaderText="Line">
												<ItemStyle Width="5%" />
												<ItemTemplate>
													<asp:Label id="FMLLineNo" Text='<%# Container.DataItem("LnNo") %>' runat="server" />
													<asp:TextBox id="txtFMLLineNo" MaxLength="5" visible=false
														Text='<%# trim(Container.DataItem("LnNo")) %>'
														runat="server"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Formula Type">
												<ItemStyle Width="21%" />
												<ItemTemplate>
													<asp:Label id="FMLFormulaType" Text='<%# Container.DataItem("FormulaType") %>' runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Table Code">
												<ItemStyle Width="12%" />
												<ItemTemplate>
													<asp:Label id="FMLTableCode" Text='<%# Container.DataItem("TableCode") %>' runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="First Operand Type">
												<ItemStyle Width="15%" />
												<ItemTemplate>
													<asp:Label id="FMLOperandType1" Text='<%# Container.DataItem("OperandType1") %>' runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="First Operand Value">
												<ItemStyle Width="12%" />
												<ItemTemplate>
													<asp:Label id="FMLOperandValue1" Text='<%# Container.DataItem("OperandValue1") %>' runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Second Operand Type">
												<ItemStyle Width="15%" />
												<ItemTemplate>
													<asp:Label id="FMLOperandType2" Text='<%# Container.DataItem("OperandType2") %>' runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Second Operand Value">
												<ItemStyle Width="10%" />
												<ItemTemplate>
													<asp:Label id="FMLOperandValue2" Text='<%# Container.DataItem("OperandValue2") %>' runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-HorizontalAlign=Right>					
												<ItemStyle Width="10%" />
												<ItemTemplate>
													<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"  CausesValidation="False"/>
													<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"  CausesValidation="False"/>
													<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" visible=false runat="server"  CausesValidation="False"/>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:DataGrid>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="3" width="100%" border="0">
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>
									<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
									<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" CausesValidation="False"/>
									<asp:ImageButton id="Undelete" imageurl="../../images/butt_undelete.gif" onclick="btnUndelete_Click" runat="server" AlternateText="Undelete" CausesValidation="False"/>
									<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
								</td>
							</tr>
							<tr>
								<td>
                                            &nbsp;</td>
							</tr>
						</table>
					</td>
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
