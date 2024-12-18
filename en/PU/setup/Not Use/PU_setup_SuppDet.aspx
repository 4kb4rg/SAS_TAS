<%@ Page Language="vb" src="../../../include/PU_setup_SuppDet.aspx.vb" Inherits="PU_SuppDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_pusetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Supplier Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmSuppDet runat=server>
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<asp:label id=lblSelectList visible=false text="Select " runat="server" />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<input type=hidden id=SuppCode runat=server />
			<input Type=Hidden id=hidBankAccNo text="" runat=server />
			<input Type=Hidden id=hidStatus text=1 runat=server />
			<input Type=Hidden id=hidSuppGrp text="" runat=server />
			<table border=0 cellspacing=0 cellpadding=1 width=100%>
				<tr>
					<td colspan=5>
						<UserControl:MenuPU id=MenuPU runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">SUPPLIER DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>Supplier Code :*</td>
					<td width=35%>
						<asp:TextBox id=txtSuppCode width=100% maxlength=20 ReadOnly=true runat=server />
						<asp:Label id=lblErrDup visible=false forecolor=red text="Duplicate Supplier Code." runat=server />
						
						<!--asp:RegularExpressionValidator id=revCode 
						    ControlToValidate="txtSuppCode"
						    ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						    Display="Dynamic"
						    text="<br>Alphanumeric without any space in between only."
						    runat="server"/-->
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
			        <td>Supplier Group :</td>
			        <td>
                        <asp:DropDownList id="ddlSuppGroup" width=100% runat=server>
                            <asp:ListItem value="0" Selected>Select Supplier Group</asp:ListItem>
                            <asp:ListItem value="1">Supplier PO</asp:ListItem>
                            <asp:ListItem value="2">Umum</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label id=lblErrSuppGrp visible=true forecolor=red text="Please select supplier group" runat=server/>
                    </td>
                    <td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
			    </tr>
				<tr>
					<td>Name :*</td>
					<td><asp:TextBox id=txtName width=100% maxlength=128 runat=server />
						<asp:RequiredFieldValidator id=validateName display=dynamic runat=server 
							ErrorMessage="Please enter Supplier Name." 
							ControlToValidate=txtName />			
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td>Contact Person :*</td>
					<td><asp:TextBox id=txtContactPerson width=100% maxlength=64 runat=server />
						<asp:RequiredFieldValidator id=validateContactPerson display=dynamic runat=server 
							ErrorMessage="Please enter Contact Person." 
							ControlToValidate=txtContactPerson />			
					</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
				</tr>
				<tr>
					<td>Address :*</td>
					<td rowspan="5">
						<textarea rows="6" id=txtAddress cols="50" maxlength="512" runat=server></textarea>
						<asp:Label id=lblErrAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
						<asp:RequiredFieldValidator id=validateAddress display=dynamic runat=server 
							ErrorMessage="Please enter Address." 
							ControlToValidate=txtAddress />
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td>Town :*</td>
					<td><asp:TextBox id=txtTown width=100% maxlength=64 runat=server />
						<asp:RequiredFieldValidator id=validateTown display=dynamic runat=server 
							ErrorMessage="Please enter Town." 
							ControlToValidate=txtTown />
						<asp:CompareValidator id="validateStrTown" display=dynamic runat="server" 
							ControlToValidate="txtTown" Text="The value must whole characters." 
							Type="string" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>Telephone No. :*</td>
					<td><asp:TextBox id=txtTelNo width=100% maxlength=16 runat=server />
						<asp:RequiredFieldValidator id=validateTelNo display=dynamic runat=server 
							ErrorMessage="Please enter Telephone Number." 
							ControlToValidate=txtTelNo />
						<asp:RegularExpressionValidator id="revTelNo" 
							ControlToValidate="txtTelNo"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td>State :*</td>
					<td><asp:TextBox id=txtState width=100% maxlength=64 runat=server />
						<asp:RequiredFieldValidator id=validateState display=dynamic runat=server 
							ErrorMessage="Please enter State." 
							ControlToValidate=txtState />
						<asp:CompareValidator id="validateStrState" display=dynamic runat="server" 
							ControlToValidate="txtState" Text="The value must whole characters." 
							Type="String" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>Fax No. :*</td>
					<td><asp:TextBox id=txtFaxNo width=100% maxlength=16 runat=server />
						<asp:RequiredFieldValidator id=validateFaxNo display=dynamic runat=server 
							ErrorMessage="Please enter Fax Number." 
							ControlToValidate=txtFaxNo />
						<asp:RegularExpressionValidator id="revFaxNo" 
							ControlToValidate="txtFaxNo"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="Fax number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td>Post Code :*</td>
					<td><asp:TextBox id=txtPostCode width=50% maxlength=16 runat=server />
						<asp:RequiredFieldValidator id=validatePostCode display=dynamic runat=server 
							ErrorMessage="Please enter Post Code." 
							ControlToValidate=txtPostCode />
						<asp:CompareValidator id="validateIntPostCode" display=dynamic runat="server" 
							ControlToValidate="txtPostCode" Text="The value must whole number." 
							Type="integer" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>Mobile Phone No :</td>
					<td><asp:TextBox id=txtMobileTel width=100% maxlength=14 runat=server />
						 <asp:RegularExpressionValidator id="revMobileNo" 
							ControlToValidate="txtMobileTel"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td>Country :</td>
					<td>
						<asp:DropDownList id=ddlCountry width=100% runat=server />
		  			</td>
					<td>&nbsp;</td>
					<td>Email Address :</td>
					<td><asp:TextBox id=txtEmail width=100% maxlength=64 runat=server />
						<asp:RegularExpressionValidator id="validateEmail2" display=dynamic runat="server" 
							ControlToValidate="txtEmail" ErrorMessage="Please enter a valid Email Address."
							ValidationExpression=".*@.*\..*" EnableClientScript="True"/>
					</td>
				</tr>
				<!--tr>
					<td>Finance Account Code :*</td>
					<td><asp:TextBox id=txtFinAccCode width=100% maxlength=32 runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr-->
				<tr>
					<td>Credit Term :*</td>
					<td><asp:TextBox id=txtCreditTerm width=50% maxlength=3 runat=server />
						Days
						<asp:RequiredFieldValidator id=validateCreditTerm display=dynamic runat=server 
							ErrorMessage="Please enter Credit Term." 
							ControlToValidate=txtCreditTerm />
						<asp:CompareValidator id="validateIntCreditTerm" display=dynamic runat="server" 
							ControlToValidate="txtCreditTerm" Text="The value must whole number." 
							Type="integer" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Credit Term Type :*</td>
							<!--<asp:ListItem value="0">Days after Acceptance Date</asp:ListItem>
							<asp:ListItem value="1">Days after Bill of Lading</asp:ListItem>
							<asp:ListItem value="2">Days after Date of Draft</asp:ListItem>
							<asp:ListItem value="3">Days after Delivery Order Date</asp:ListItem>
							<asp:ListItem value="4">Days after Date of Invoice</asp:ListItem>
							<asp:ListItem value="5">Days after Shipment Date</asp:ListItem>
							<asp:ListItem value="6">Days Sight</asp:ListItem>
							<asp:ListItem value="7">Days from Acceptance Date</asp:ListItem>
							<asp:ListItem value="8">Days from Bill of Lading</asp:ListItem>
							<asp:ListItem value="9">Days from Date of Draft</asp:ListItem>
							<asp:ListItem value="10">Days from Delivery Order Date</asp:ListItem>-->
							<!--<asp:ListItem value="12">Sight</asp:ListItem>-->
					<td><asp:DropDownList id="ddlTermType" width=100% runat=server>
							<asp:ListItem value="11" selected>Days from Date of Invoice</asp:ListItem>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Credit Limit : </td>
					<td><asp:TextBox id=txtCreditLimit width=50% maxlength=19 runat=server />
						<asp:CompareValidator id="validateCreditLimit" display=dynamic runat="server" 
							ControlToValidate="txtCreditLimit" Text="The value must be numeric." 
							Type="double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revCreditLimit 
							ControlToValidate="txtCreditLimit"
							ValidationExpression="^(\-|)\d{1,19}(\.\d{0,0}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 19 digits. "
							runat="server"/> </td>
				</tr>
				<tr>
					<td><asp:Label id=lblIssueAccCode runat=server /> :*</td>
					<td>
						<asp:dropdownlist id=ddlAccCode width=100% runat=server></asp:dropdownlist>
						<asp:Label id=lblErrAccCode visible=true forecolor=red text="Please select one account code" runat=server/>
					</td>
					
				</tr>
				<tr>
					<td><asp:Label Text="Supplier Type"  runat=server /> :*</td>
					<td>
						<asp:dropdownlist id=ddlSuppType width=100% AutoPostBack="True" onSelectedIndexChanged="SupTypeChanged" runat=server></asp:dropdownlist>
						<asp:RequiredFieldValidator id=validateType display=dynamic runat=server 
							ErrorMessage="Please Choose Supplier Type" 
							ControlToValidate=ddlSuppType />
					</td>
				</tr>
				<tr id=SuppCat Visible = false runat="server">
					<td><asp:Label Text="Supplier Category"  runat=server /> :*</td>
					<td>
						<asp:dropdownlist id=ddlSuppCat width=100% runat=server>
						    <asp:ListItem value="0">Other</asp:ListItem>
							<asp:ListItem value="1">Agent</asp:ListItem>
							<asp:ListItem value="2">Owner</asp:ListItem>
						</asp:dropdownlist>
						<asp:Label id=lblErrSuppCat visible=true forecolor=red text="Please select one supplier category" runat=server/>
					</td>
				</tr>
				<tr>
    				<td>PPN Charged :	</td>
    				<td><asp:CheckBox id="chkPPN" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=chkPPN_Change runat=server /></td>
				</tr>
				<tr id=PotSortasi Visible = false runat="server">
					<td height="25">Potongan Sortasi :</td>
					<td ><asp:TextBox id=txtPotSortasi width=25% maxlength=15 runat=server />
						<asp:RegularExpressionValidator id="RegularExpressionValidatorPotSortasi" 
							ControlToValidate="txtPotSortasi"
							ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
							Display="Dynamic"
							text = "Numerics of length (5.2) only"
							runat="server"/>
						<asp:RequiredFieldValidator 
							id="validatePotSortasi" 
							runat="server" 
							ErrorMessage="Please Specify potongan Sortasi" 
							ControlToValidate="txtPotSortasi" 
							display="dynamic"/>
						<asp:label id=lblErrPotSortasi text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
					</td>
				</tr>
				<tr id=MaxSortasi Visible = false runat="server">
					<td height="25">Maximum Sortasi (%) :</td>
					<td ><asp:TextBox id=txtMaxSortasi width=25% maxlength=15 runat=server />
						<asp:RegularExpressionValidator id="RegularExpressionValidatorMaxSortasi" 
							ControlToValidate="txtMaxSortasi"
							ValidationExpression="\d{1,5}\.\d{0,2}|\d{1,5}"
							Display="Dynamic"
							text = "Numerics of length (5.2) only"
							runat="server"/>
						<asp:RequiredFieldValidator 
							id="validateMaxSortasi" 
							runat="server" 
							ErrorMessage="Please Specify Maximum Sortasi" 
							ControlToValidate="txtMaxSortasi" 
							display="dynamic"/>
						<asp:label id=lblErrMaxSortasi text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
					</td>
				</tr>
				<tr>
					<td valign=top>PO Notes : </td>
					<td>
					<textarea rows="6" id=txtPOTerms cols="50" maxlength="512" runat=server></textarea>
					<asp:Label id=lblErrPOTerms visible=false forecolor=red text="Maximum length for PO Terms & Conditions is up to 512 characters only." runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td style="font-weight: bolder; font-size: 11px;">Tax Informations:</td>
				</tr>
				<tr>
    				<td>NPWP Activation Date :	</td>
    				<td><asp:TextBox id=txtNPWPDate width=50% maxlength="10" runat="server"/>
				        <a href="javascript:PopCal('txtNPWPDate');"><asp:Image id="Image1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				        <asp:RequiredFieldValidator	id="RequiredFieldValidator1" runat="server"  ControlToValidate="txtNPWPDate" text = "Please enter Date Created" display="dynamic"/>
				        <asp:label id=lblDate2 Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
				        <asp:label id=lblFmt2  forecolor=red Visible = false Runat="server"/> 
				    </td>
				</tr>
				<tr>
    				<td>NPWP Name :	</td>
    				<td><asp:TextBox id=txtNPWPName width=100% maxlength=64 runat=server /></td>
				</tr>
				<tr>
    				<td>NPWP No. :	</td>
    				<td><asp:TextBox id=txtNPWPNo width=100% maxlength=15 runat=server /></td>
				</tr>
				<tr>
    				<td>NPWP Address :	</td>
    				<td rowspan="5"><textarea rows="6" id=txtNPWPAddress cols="50" maxlength="512" runat=server></textarea></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				    </tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
    				<td>PKP Activation :	</td>
    				<td><asp:CheckBox id="chkPKP" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=chkPKP_Change runat=server /></td>
				</tr>
				<tr>
    				<td>PKP No. :	</td>
    				<td><asp:TextBox id=txtPKPNo width=100% maxlength=64 runat=server /></td>
				</tr>
				<tr>
    				<td>PKP Date :	</td>
    				<td><asp:TextBox id=txtPKPDate width=50% maxlength="10" runat="server"/>
				        <a href="javascript:PopCal('txtPKPDate');"><asp:Image id="btnDateCreated" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				        <asp:RequiredFieldValidator	id="rfvDateCreated" runat="server"  ControlToValidate="txtPKPDate" text = "Please enter Date Created" display="dynamic"/>
				        <asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
				        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				    </td>
				</tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        SKB Activation :</td>
                    <td>
                        <asp:CheckBox id="ChkSKB" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=chkSKB_Change runat=server /></td>
                </tr>
                <tr>
                    <td>
                        SKB No :</td>
                    <td>
                        <asp:TextBox id=TxtSKBNo width=100% maxlength=64 runat=server /></td>
                </tr>
                <tr>
                    <td>
                        SKB Date :</td>
                    <td>
                        <asp:TextBox id=TxtSKBDate width=50% maxlength="10" runat="server"/>
                            <a href="javascript:PopCal('TxtSKBDate');"><asp:Image id="BtnSKBDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
                            <asp:RequiredFieldValidator	id="RequiredFieldValidator2" runat="server"  ControlToValidate="TxtSKBDate" text = "Please enter Date Created" display="dynamic"/>
                        <br />
                        <asp:label id=lblDateSKB Text ="<br>Date Entered should be in the format " forecolor=Red Visible = False Runat="server"/><asp:label id=lblfmtSKB  forecolor=Red Visible = False Runat="server"/></td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td>
                        </td>
                </tr>
				<tr>
				    <td colspan=5>&nbsp;</td>
			    </tr>
			    <tr>
				    <td style="font-weight: bolder; font-size: 11px;">Correspondent Bank:
					</td>
				</tr>
			    <tr>
				    <td colSpan="5">
				        <table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank : </td>
                                <td colSpan="5"><asp:DropDownList id=ddlBankCode width=90% runat=server />
                                                <asp:label id=lblerrBankCode Visible=False Text="Tax object cannot be empty" forecolor=red Runat="server" />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank Account Name : </td>
                                <td colSpan="5"><asp:TextBox id=txtBankAccName width=90% maxlength=64 runat=server />
                                                <asp:Label id=lblErrBankAccName visible=false forecolor=red text=" Please enter row number on reports." runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank Account Number : </td>
                                <td colSpan="5"><asp:TextBox id=txtBankAccNo width=90% maxlength=32 runat=server />
                                                <asp:Label id=lblerrBankAccNo visible=false forecolor=red text=" Please enter row number on reports." runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank Address : </td>
                                <td colSpan="5"><asp:TextBox ID=txtBankAddress maxlength=512 width=90% Enabled=true Runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank Town : </td>
                                <td colSpan="5"><asp:TextBox id=txtBankTown width=90% maxlength=64 runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank State : </td>
                                <td colSpan="5"><asp:TextBox id=txtBankState width=90% maxlength=64 runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Bank Country : </td>
                                <td colSpan="5"><asp:DropDownList id=ddlBankCountry width=90% runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td width="20%" height=25>Remark : </td>
                                <td colSpan="5"><asp:TextBox id=txtBankRemark width=90% maxlength=64 runat=server />
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td colSpan="6">&nbsp;</td>
                            </tr>	
                            <tr class="mb-c">
                                <td height=25 colspan=2>
                                    <asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add CausesValidation=True onclick=AddBtn_Click UseSubmitBehavior="false" runat=server /> 									
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
			    </tr>
			    <tr>
				    <td colSpan="5">
					    <asp:DataGrid id=dgLineDet
						    AutoGenerateColumns="false" width="100%" runat="server"
						    GridLines=none
						    Cellpadding="2"
						    Pagerstyle-Visible="False"
						    OnEditCommand="DEDR_Edit"
						    OnCancelCommand="DEDR_Cancel"
						    OnDeleteCommand="DEDR_Delete"
						    AllowSorting="True">
						    <HeaderStyle CssClass="mr-h"/>
						    <ItemStyle CssClass="mr-l"/>
						    <AlternatingItemStyle CssClass="mr-r"/>
						    <Columns>	
							    <asp:TemplateColumn HeaderText="Bank">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# Container.DataItem("BankDescr") %> id="lblBankDescr" runat="server" />
									    <asp:Label visible=false Text=<%# Container.DataItem("BankCode") %> id="lblBankCode" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Acc. Name">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# Container.DataItem("BankAccName") %> id="lblBankAccName" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Acc. No">
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# Container.DataItem("BankAccNo") %> id="lblBankAccNo" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		
							     <asp:TemplateColumn HeaderText="Bank Address">
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# Container.DataItem("BankAddress") %> id="lblBankAddress" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>	
							    <asp:TemplateColumn HeaderText="Bank Town">
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# Container.DataItem("BankTown") %> id="lblBankTown" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>	
							    <asp:TemplateColumn HeaderText="Remark">
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# Container.DataItem("BankRemark") %> id="lblBankRemark" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>	
							    <asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate>
								        <asp:label text= '<%# Container.DataItem("BankState") %>' Visible=False id="lblBankState" runat="server" />
								        <asp:label text= '<%# Container.DataItem("BankCountry") %>' Visible=False id="lblBankCountry" runat="server" />
								        <asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
									    <asp:LinkButton id=lbDelete CommandName="Delete" Text="Delete" CausesValidation=False runat=server />
								        <asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
								    </ItemTemplate>
							    </asp:TemplateColumn>	
					    </Columns>										
					    </asp:DataGrid>
				    </td>
			    </tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">
					    <asp:ImageButton id=btnNew imageurl="../../images/butt_new.gif" UseSubmitBehavior="false" onClick=btnNew_Click  AlternateText="New" runat=server/>
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" visible=true CausesValidation=false AlternateText=" Delete " onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" visible=true AlternateText="Undelete" onclick=btnUnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="  Back  " onclick=btnBack_Click runat=server />
					</td>
				</tr>
			</table>
		</form>    
	</body>
</html>
