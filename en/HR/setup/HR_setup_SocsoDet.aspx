<%@ Page Language="vb" src="../../../include/HR_setup_SocsoDet.aspx.vb" Inherits="HR_setup_SocsoDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Socso Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />   
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
               <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5"><strong> SOCSO DETAILS </strong></td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% valign=top>Socso Code :* </td>
					<td width=30% valign=top>
						<asp:Textbox id=txtSocsoCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Socso Code"
								ControlToValidate=txtSocsoCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtSocsoCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another Socso code." runat=server/>					
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20% valign=top>Status : </td>
					<td width=25% valign=top><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td valign=top>Description :*</td>
					<td valign=top>
						<asp:Textbox id=txtDescription maxlength=64 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Socso Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td valign=top>Date Created : </td>
					<td valign=top><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td valign=top align="left">Employer Socso Deduction Code :*</td>
					<td valign=top align="left">
						<asp:DropDownList id=ddlEmprDeductCode width=80% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlEmprDeductCode','4',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmprDeductCode visible=false forecolor=red text="<br>Please select a code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Last Updated : </td>
					<td valign=top><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td valign=top align="left" valign=top>Employee Socso Deduction Code :*</td>
					<td valign=top align="left" valign=top>
						<asp:DropDownList id=ddlEmpeDeductCode width=80% runat=server/> 
						<input type="button" id=btnFind2 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlEmpeDeductCode','2',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmpeDeductCode visible=false forecolor=red text="<br>Please select a code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top valign=top>Updated By : </td>
					<td valign=top valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td colspan="5" valign=top>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD vAlign="top" width=22% height=25>Income Range From :*</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtIncomeFrom maxlength=21 text=0 width=100% runat=server/>
													<asp:RequiredFieldValidator id=validateIncFrom display=Dynamic runat=server 
															ErrorMessage="<br>Please Enter Income Range From."
															ControlToValidate=txtIncomeFrom />															
													<asp:CompareValidator id="cvValidateIncFrom" display=dynamic runat="server" 
														ControlToValidate="txtIncomeFrom" Text="<br>The value must whole number. " 
														Type="Double" Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator id=revIncomeFrom 
														ControlToValidate="txtIncomeFrom"
														ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
														Display="Dynamic"
														text = "Maximum length 15 digits and 5 decimal points. "
														runat="server"/>
													<asp:Label id=lblErrRange visible=false forecolor=red text="Invalid Palm Oil Price range, please key in valid Palm Oil Price range." runat=server/>
											</TD>
											<TD vAlign="top" width=6%>&nbsp;</TD>
											<TD vAlign="top" width=22%>Income Range To :*</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtIncomeTo maxlength=21 width=100% text=0 runat=server/>
													<asp:RequiredFieldValidator id=validateIncTo display=Dynamic runat=server 
															ErrorMessage="<br>Please Enter Income Range To."
															ControlToValidate=txtIncomeTo />															
													<asp:CompareValidator id="cvValidateIncTo" display=dynamic runat="server" 
														ControlToValidate="txtIncomeTo" Text="<br>The value must whole number. " 
														Type="Double" Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator id=revIncomeTo 
														ControlToValidate="txtIncomeTo"
														ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
														Display="Dynamic"
														text = "Maximum length 15 digits and 5 decimal points. "
														runat="server"/>
											</TD>
										</TR>					
										<TR class="mb-c">
											<TD vAlign="top" width=22% height=25>Employer Contribution % :</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtEmprContributePercent maxlength=3 text=0 width=50% runat=server/>
													<asp:CompareValidator id="cvValidatePercent" display=dynamic runat="server" 
														ControlToValidate="txtEmprContributePercent" Text="<br>The value must whole number. " 
														Type="Integer" Operator="DataTypeCheck"/>
													<asp:Label id=lblErrEmployer visible=false forecolor=red text="<br>Key in either Employer Contribution in percentage or, in amount." runat=server/>
													<asp:Label id=lblErrEmployerPercent visible=false forecolor=red text="<br>The contribution should not more than 100 percent." runat=server/>
											</TD>
											<TD vAlign="top" width=6%>&nbsp;</TD>
											<TD vAlign="top" width=22%>Employer Contribution Amount :</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtEmprContributeAmount width=100% text=0 runat=server/>
													<asp:CompareValidator id="cvValidateAmount" display=dynamic runat="server" 
														ControlToValidate="txtEmprContributeAmount" Text="<br>The value must whole number. " 
														Type="Double" Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator id=revEmprContributeAmount 
														ControlToValidate="txtEmprContributeAmount"
														ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
														Display="Dynamic"
														text = "Maximum length 15 digits and 5 decimal points. "
														runat="server"/>
											</TD>
										</TR>					
										<TR class="mb-c">
											<TD vAlign="top" width=22% height=25>Employee Contribution % :</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtEmpeContributePercent maxlength=3 width=50% text=0 runat=server/>
													<asp:CompareValidator id="cvValidatePercent2" display=dynamic runat="server" 
														ControlToValidate="txtEmpeContributePercent" Text="<br>The value must whole number. " 
														Type="Integer" Operator="DataTypeCheck"/>
													<asp:Label id=lblErrEmployee visible=false forecolor=red text="<br>Key in either Employee Contribution in percentage or, in amount." runat=server/>
													<asp:Label id=lblErrEmployeePercent visible=false forecolor=red text="<br>The contribution should not more than 100 percent." runat=server/>
											</TD>
											<TD vAlign="top" width=6%>&nbsp;</TD>
											<TD vAlign="top" width=22%>Employee Contribution Amount :</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtEmpeContributeAmount maxlength=21 width=100% text=0 runat=server/>
													<asp:CompareValidator id="cvValidateAmount2" display=dynamic runat="server" 
														ControlToValidate="txtEmpeContributeAmount" Text="<br>The value must whole number. " 
														Type="Double" Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator id=revEmpeContributeAmount 
														ControlToValidate="txtEmpeContributeAmount"
														ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
														Display="Dynamic"
														text = "Maximum length 15 digits and 5 decimal points. "
														runat="server"/>
											</TD>
										</TR>					
										<TR class="mb-c">
											<TD vAlign="top" height=25 colspan=5>
												<asp:label id=lblErrConsistent visible=false forecolor=red text="Please use both (employee and employer) in percentage or both in amount.<br>" runat=server/> 
												<asp:label id=lblInvalidRange visible=false forecolor=red text="The new Income Range must not less than existing Income Range.<br>" runat=server/>
												<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click runat=server /> 
											&nbsp;</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="5" valign=top>
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma">
						                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>		
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>						
								<asp:TemplateColumn ItemStyle-Width="30%" HeaderText="Income Range">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("IncomeRange") %> id="lblIncomeRange" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Employer %" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("EmprContribute") %> id="lblEmprPercent" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Employer $" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate> 
										<asp:Label Text=<%# FormatNumber(Container.DataItem("EmprCAmt"), 2) %> id="lblEmprAmount" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Employee %" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("EmpeContribute") %> id="lblEmpePercent" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Employee $" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate> 
										<asp:Label Text=<%# FormatNumber(Container.DataItem("EmpeCAmt"), 2) %> id="lblEmpeAmount" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:label id=SocsoLnId visible="false" text=<%# Container.DataItem("SocsoLnId")%> runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
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
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=socsocode runat=server />
				<asp:Label id=lblHidIncomeRange visible=False runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
         	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
