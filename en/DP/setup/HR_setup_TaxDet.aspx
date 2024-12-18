<%@ Page Language="vb" src="../../../include/HR_setup_TaxDet.aspx.vb" Inherits="HR_setup_TaxDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_HRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Tax Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmTaxDet runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<Input Type=Hidden id=tbcode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<asp:Label id=lblHidRange visible=false runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuHRSetup id=MenuHRSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">TAX DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Tax Code :* </td>
					<td width=30%>
						<asp:TextBox id=txtTaxCode width=50% runat=server/>
						<asp:RequiredFieldValidator id=rfvTaxCode 
							text="<br>Please enter Tax Code."
							display=Dynamic 
							runat=server
							ControlToValidate=txtTaxCode />	
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtTaxCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>														
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description :*</td>
					<td><asp:TextBox id=txtDesc maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=rfvDesc
							text="Please enter Description."
							display=Dynamic 
							runat=server
							ControlToValidate=txtDesc />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employer Deduction Code : </td>
					<td><asp:DropDownList id=ddlEmprDeCode width=100% onselectedindexchanged=onchange_DeCode autopostback=true runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employee Deduction Code : </td>
					<td>
						<asp:DropDownList id=ddlEmpeDeCode width=100% onselectedindexchanged=onchange_DeCode autopostback=true runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top >Minimum Taxable Income :*</td>
					<td>
						<!-- Modified BY ALIM max Length = 22 and width = 75%-->
						<asp:TextBox id=txtMinTaxIncome text="0" maxlength=22 width=75% runat=server />
						<asp:RequiredFieldValidator id=rfvMinTaxIncome
							text="<br>Please enter Minimum Taxable Income."
							display=Dynamic 
							runat=server
							ControlToValidate=txtMinTaxIncome />
						<!-- Modified BY ALIM -->
						<asp:RegularExpressionValidator id=revMinTaxIncome 
							ControlToValidate="txtMinTaxIncome"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text="<br>Maximum length 19 digits with 2 decimal points. "
							runat="server"/>
						<!-- End of Modified BY ALIM -->
						<asp:label id=lblErrMinIncome text="Minimum Taxable Income must be 0 or more." forecolor=red visible=false runat=server />
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Functional Percentage :*</td>
					<td>
						<asp:TextBox id=txtFuncPercentage text="0" maxlength=22 width=75% runat=server />
						<asp:RequiredFieldValidator id=rfvFuncPercentage
							text="<br>Please enter Minimum Taxable Income."
							display=Dynamic 
							runat=server
							ControlToValidate=txtFuncPercentage />
						<asp:RegularExpressionValidator id=revFuncPercentage 
							ControlToValidate="txtFuncPercentage"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text="<br>Maximum length 19 digits with 2 decimal points. "
						runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Max. Allowance :*</td>
					<td>
						<asp:TextBox id=txtMaxAllowance text="0" maxlength=22 width=75% runat=server />
						<asp:RequiredFieldValidator id=rfvMaxAllowance
							text="<br>Please enter Maximum Allowance Deduction."
							display=Dynamic 
							runat=server
							ControlToValidate=txtMaxAllowance />
						<!-- Modified BY ALIM -->
						<asp:RegularExpressionValidator id=revMaxAllowance 
							ControlToValidate="txtMaxAllowance"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text="<br>Maximum length 19 digits with 2 decimal points. "
						runat="server"/>
					</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>Personal Allowance :*</td>
					<td>
						<asp:TextBox id=txtPeronalAllowance text="0" maxlength=22 width=75% runat=server />
						<asp:RequiredFieldValidator id=rfvPeronalAllowance
							text="<br>Please enter Personal Allowance Deduction."
							display=Dynamic 
							runat=server
							ControlToValidate=txtPeronalAllowance />
						<asp:RegularExpressionValidator id=revPeronalAllowance
							ControlToValidate="txtPeronalAllowance"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text="<br>Maximum length 19 digits with 2 decimal points. "
						runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Dependent Allowance :*</td>
					<td>
						<asp:TextBox id=txtDependentAllowance text="0" maxlength=22 width=75% runat=server />
						<asp:RequiredFieldValidator id=rfvDependentAllowance
							text="<br>Please enter Dependent Allowance Deduction."
							display=Dynamic 
							runat=server
							ControlToValidate=txtDependentAllowance />
						<!-- Modified BY ALIM -->
						<asp:RegularExpressionValidator id=revDependentAllowance
							ControlToValidate="txtDependentAllowance"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text="<br>Maximum length 19 digits with 2 decimal points. "
						runat="server"/>
					</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25 valign=top >Maximum Dependents :*</td>
					<td>
					<asp:TextBox id=txtMaxDependents text="0" maxlength=22 width=75% runat=server />
						<asp:RequiredFieldValidator id=rfvMaxDependents
							text="<br>Please enter Maximun Dependents."
							display=Dynamic 
							runat=server
							ControlToValidate=txtMaxDependents />
						<asp:RegularExpressionValidator id=revMaxDependents 
							ControlToValidate="txtMaxDependents"
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text="<br>Maximum length 19 digits with 0 decimal points. "
							runat="server"/>						
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:label id=lblErrSelectOne text="Please select one Employer or Employee Deduction Code.<br>" forecolor=red visible=false runat=server />
					</td>
				</tr>
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
					<td colspan="6">
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table cellspacing="0" cellpadding="2" width="100%" border="0">
										<tr class="mb-c">
											<td width=20% valign=top>Income From :*</td>
											<td width=27% valign=top>
												<!-- Modified BY ALIM max Length = 22 and width = 75% -->
												<asp:TextBox id=txtFromIncome text=0 maxlength=22 width=75% runat=server/>
												<asp:RequiredFieldValidator id=rfvFromIncome 
														display=Dynamic 
														runat=server 
														ErrorMessage="<br>Please enter a minimum value for range of income."
														ControlToValidate=txtFromIncome />
												<!-- Modified BY ALIM -->
												<asp:RegularExpressionValidator id=revFromIncome 
													ControlToValidate="txtFromIncome"
													ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
													Display="Dynamic"
													text="<br>Maximum length 19 digits with 2 decimal points. "
													runat="server"/>
												<!-- End of Modified BY ALIM -->		
												
												<asp:label id=lblErrFromIncome text="<br>Income value must be 0 or more." forecolor=red visible=false runat=server />
												<asp:label id=lblErrRange text="<br>Income Range is invalid or overlapping." forecolor=red visible=false runat=server/>
											</td>
											<td width=6%>&nbsp;</td>
											<td width=20% valign=top>To :*</td>
											<td width=27% valign=top>
												<!-- Modified BY ALIM max Length = 22 and width = 75% -->
												<asp:TextBox id=txtToIncome text=0 maxlength=22 width=75% runat=server/>
												<asp:RequiredFieldValidator id=rfvToIncome 
														display=Dynamic 
														runat=server 
														ErrorMessage="<br>Please enter a maximum value for range of income."
														ControlToValidate=txtToIncome />
														<!-- Modified BY ALIM -->
														<asp:RegularExpressionValidator id=revToIncome 
															ControlToValidate="txtToIncome"
															ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
															Display="Dynamic"
															text="<br>Maximum length 19 digits with 2 decimal points. "
															runat="server"/>
														<!-- End of Modified BY ALIM -->			
														
												<asp:label id=lblErrToIncome text="<br>Income value must be 0 or more." forecolor=red visible=false runat=server />
											</td>
										</tr>
										<tr class="mb-c">
											<td width=20% valign=top>Employer Contribution(%): </td>
											<td width=27% valign=top>
												<asp:TextBox id=txtEmprRate text=0 maxlength=5 width=50% runat=server/>
												<asp:RegularExpressionValidator id=revEmprRate 
														ControlToValidate="txtEmprRate"
														ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,3}"
														Display="Dynamic"
														text="<br>Maximum length 2 digits with 2 decimal points. "
														runat="server"/>
												<asp:label id=lblErrEmprRate text="<br>Employer Contribution must be from 0 to 100." forecolor=red visible=false runat=server />
											</td>
											<td width=6%>&nbsp;</td>
											<td width=20% valign=top>Employee Contribution(%) : </td>
											<td width=27% valign=top>
												<asp:TextBox id=txtEmpeRate text=0 maxlength=5 width=50% runat=server/>
												<asp:RegularExpressionValidator id=revEmpeRate 
														ControlToValidate="txtEmpeRate"
														ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,3}"
														Display="Dynamic"
														text="<br>Maximum length 2 digits with 2 decimal points. "
														runat="server"/>
												<asp:label id=lblErrEmpeRate text="<br>Employee Contribution must be from 0 to 100." forecolor=red visible=false runat=server/>
											</td>
										</tr>
										<tr class="mb-c">
											<td valign="top" colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
                                            </td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
                </table>


                <table style="width: 100%" class="font9Tahoma">
				<tr>
					<td>
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
							Pagerstyle-Visible=False
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
								<asp:TemplateColumn HeaderText="Income From" ItemStyle-Width="20%">
									<ItemTemplate>
										<!-- Remarked BY ALIM 
										<%# FormatNumber(Container.DataItem("FromIncome"), 2) %>  -->
										<!-- Modified BY ALIM -->
										<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("FromIncome")) %>  
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="To" ItemStyle-Width="20%">
									<ItemTemplate>
										<!-- Remarked BY ALIM 
										<%# FormatNumber(Container.DataItem("ToIncome"), 2) %> -->
										<!-- Modified BY ALIM -->
										<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("ToIncome")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Employer Contribution(%)" ItemStyle-Width="25%">
									<ItemTemplate>
										<!-- Remarked BY ALIM 
										<%# FormatNumber(Container.DataItem("EmprRate"), 2) %> -->
										<!-- Modified BY ALIM -->
										<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("EmprRate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Employee Contribution(%)" ItemStyle-Width="25%">
									<ItemTemplate>
										<!-- Remarked BY ALIM 
										<%# FormatNumber(Container.DataItem("EmpeRate"), 2) %> -->
										<!-- Modified BY ALIM -->
										<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("EmpeRate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:label id=lblCode visible="false" text=<%# Container.DataItem("TaxLnId")%> runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
					<td>
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
