<%@ Page Language="vb" src="../../../include/HR_setup_MPOBbonusDet.aspx.vb" Inherits="HR_setup_MPOBbonusDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>MPOB PRICE ZONE</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">MPOB PRICE ZONE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% valign=top>Bonus Code :* </td>
					<td width=30% valign=top>
						<asp:Textbox id=txtBonusCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator 
								id=validateBonusCode 
								display=Dynamic 
								runat=server 
								ErrorMessage="<br>Please Enter Bonus Code"
								ControlToValidate=txtBonusCode />
						<asp:RegularExpressionValidator 
								id=revBonusCode 
								ControlToValidate="txtBonusCode"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="<br>Alphanumeric without any space in between only."
								runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another Bonus Code." runat=server/>					
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20% valign=top>Status : </td>
					<td width=25% valign=top><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td width=20% valign=top>Description :*</td>
					<td width=30% valign=top>
						<asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator 
								id=validateDesc 
								display=Dynamic 
								runat=server 
								ErrorMessage="<br>Please Enter Bonus Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td valign=top>Date Created : </td>
					<td valign=top><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td width=20% valign=top>Allowance & Deduction Code :*</td>
					<td width=30% valign=top><asp:DropDownList id=ddlAD width=85% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlAD','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrAD visible=false forecolor=red text="<br>Please select Allowance & Deduction code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Last Updated : </td>
					<td valign=top><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td valign=top align="left">Bonus Type :*</td>
					<td valign=top align="left">
						<asp:RadioButton id=rbPriceBonus Text=" Price Bonus" GroupName="BonusType" onCheckedChanged="onCheck_PriceRate" autopostback="true" runat="server" />
					</td>
					<td>&nbsp;</td>
					<td valign=top valign=top>Updated By : </td>
					<td valign=top valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td valign=top align="left" valign=top>&nbsp;</td>
					<td valign=top align="left" valign=top>
						<asp:RadioButton id=rbAddPay Text=" Additional Pay" GroupName="BonusType" onCheckedChanged="onCheck_PriceRate" autopostback="true" runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign=top align="left" valign=top>&nbsp;</td>
					<td valign=top align="left" valign=top>
						<asp:RadioButton id=rbLoadBasicPay Text=" Loader Basic Pay" GroupName="BonusType" onCheckedChanged="onCheck_PriceRate" autopostback="true" runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign=top align="left" valign=top>&nbsp;</td>
					<td valign=top align="left" valign=top>
						<asp:RadioButton id=rbHarvBasicPay Text=" Harvester Basic Pay" GroupName="BonusType" onCheckedChanged="onCheck_PriceRate" autopostback="true" runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
					<td colspan="5" valign=top>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD vAlign="top" width=22% height=25>From Palm Oil Price :*</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtFromPrice maxlength=8 text=0 width=100% runat=server/>
													<asp:RequiredFieldValidator 
															id=rfvFromPrice 
															display=Dynamic 
															runat=server 
															ErrorMessage="Please enter 'From Palm Oil Price'.<br>"
															ControlToValidate=txtFromPrice />															
													<asp:CompareValidator 
															id="cvFromPrice" 
															display=dynamic 
															runat="server" 
															ControlToValidate="txtFromPrice" 
															Text="<br>The value must be number or with decimal. " 
															Type="Double" 
															Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator 
															id=revFromPrice 
															ControlToValidate="txtFromPrice"
															ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
															Display="Dynamic"
															text = "Maximum length 5 digits and 2 decimal points. "
															runat="server"/>
													<asp:Label id=lblErrRange visible=false forecolor=red text="Price range is invalid or overlapping." runat=server/>
											</TD>
											<TD vAlign="top" width=6%>&nbsp;</TD>
											<TD vAlign="top" width=22%>To Palm Oil Price :*</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtToPrice maxlength=8 text=0 width=100% runat=server/>
													<asp:RequiredFieldValidator id=rfvToPrice display=Dynamic runat=server 
															ErrorMessage="Please enter 'To Palm Oil Price'."
															ControlToValidate=txtToPrice />															
													<asp:CompareValidator id="cvToPrice" 
															display=dynamic 
															runat="server" 
															ControlToValidate="txtToPrice" 
															Text="The value must be number or with decimal. " 
															Type="Double" 
															Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator 
															id=revToPrice 
															ControlToValidate="txtToPrice"
															ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
															Display="Dynamic"
															text = "Maximum length 5 digits and 2 decimal points. "
															runat="server"/>
 											</TD>
										</TR>	
										<TR id=TrYield class="mb-c" visible=false runat=server>
											<TD vAlign="top" width=22% height=25>From Yield Bracket :*</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtFromYieldBracket maxlength=11 text=0 width=100% runat=server/>
													<asp:RequiredFieldValidator 
															id=rfvFromYieldBracket 
															display=Dynamic 
															runat=server 
															ErrorMessage="Please enter 'From Yield Bracket'.<br>"
															ControlToValidate=txtFromYieldBracket />															
													<asp:CompareValidator 
															id="cvFromYieldBracket" 
															display=dynamic 
															runat="server" 
															ControlToValidate="txtFromYieldBracket" 
															Text="The value must be in numeric. " 
															Type="Double" 
															Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator 
															id=revFromYieldBracket 
															ControlToValidate="txtFromYieldBracket"
															ValidationExpression="\d{1,5}\.\d{1,5}|\d{1,5}"
															Display="Dynamic"
															text = "Maximum length 5 digits and 5 decimal points. "
															runat="server"/>
													<asp:Label id=lblErrYieldRange visible=false forecolor=red text="Yield Bracket range is invalid or overlapping." runat=server/>
											</TD>
											<TD vAlign="top" width=6%>&nbsp;</TD>
											<TD vAlign="top" width=22%>To Yield Bracket :*</TD>
											<TD vAlign="top" width=25%>
													<asp:Textbox id=txtToYieldBracket maxlength=11 text=0 width=100% runat=server/>
													<asp:RequiredFieldValidator 
															id=rfvToYieldBracket 
															display=Dynamic 
															runat=server 
															ErrorMessage="Please Enter 'To Yield Bracket'."
															ControlToValidate=txtToYieldBracket />															
													<asp:CompareValidator 
															id="cvToYieldBracket" 
															display=dynamic 
															runat="server" 
															ControlToValidate="txtToYieldBracket" 
															Text="The value must be in numeric." 
															Type="Double" 
															Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator 
															id=revToYieldBracket 
															ControlToValidate="txtToYieldBracket"
															ValidationExpression="\d{1,5}\.\d{1,5}|\d{1,5}"
															Display="Dynamic"
															text = "Maximum length 5 digits and 5 decimal points. "
															runat="server"/>
 											</TD>
										</TR>							
										<TR class="mb-c">
											<TD vAlign="top" height=25><asp:label id=lblPriceRate runat=server /> :*</TD>
											<TD vAlign="top">
													<asp:Textbox id=txtBonusPrice maxlength=8 text=0 width=100% runat=server/>
													<asp:CompareValidator id="cvBonusPrice" display=dynamic runat="server" 
														ControlToValidate="txtBonusPrice" Text="The value must be number or with decimal. " 
														Type="Double" Operator="DataTypeCheck"/>
													<asp:RegularExpressionValidator id=revBonusPrice 
														ControlToValidate="txtBonusPrice"
														ValidationExpression="\d{1,5}\.\d{1,2}|\d{1,5}"
														Display="Dynamic"
														text = "Maximum length 5 digits and 2 decimal points. "
														runat="server"/>
											</TD>
											<TD vAlign="top">&nbsp;</TD>
											<TD vAlign="top">&nbsp;</TD>
											<TD vAlign="top">&nbsp;</TD>
										</TR>					
										<TR class="mb-c">
											<TD vAlign="top" height=25 colspan=5>
												<asp:label id=lblInvalidRange visible=false forecolor=red text="The new Palm Oil Price range must not less than existing Palm Oil Price list.<br>" runat=server/>
												<asp:label id=lblInvalidYieldRange visible=false forecolor=red text="The new Yield Bracket range must not less than existing Yield Bracket list.<br>" runat=server/>
												<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click runat=server /> 
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
				<tr>
					<td valign=top>
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
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="From Palm Oil Price per MT" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("FromPrice"), 2) %> id="lblFromPrice" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="To Palm Oil Price per MT" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("ToPrice"), 2) %> id="lblToPrice" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="From Yield Bracket" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right Visible=False>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("FromYieldBracket"), 5) %> id="lblFromYieldBracket" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="To Yield Bracket" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right Visible=False>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("ToYieldBracket"), 5) %> id="lblToYieldBracket" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="15%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:Label Text=<%# FormatNumber(Container.DataItem("BonusPrice"), 2) %> id="lblPriceBonus" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:label id=BonusLnId visible="false" text=<%# Container.DataItem("BonusLnId")%> runat="server" />
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
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=bonuscode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblHidIncomeRange visible=false runat=server/>
				<asp:Label id=lblHidYieldRange visible=False runat=server />
				<asp:Label id=lblHidPriceBonus text="Rate (per day)" visible=false runat=server/>
				<asp:Label id=lblHidAddPay text="Rate (per day)" visible=false runat=server/>
				<asp:Label id=lblHidLoadBasicPay text="Rate (per MT)" visible=false runat=server/>
				<asp:Label id=lblHidHarvBasicPay text="Rate (per MT)" visible=false runat=server/>
				
				<tr>
					<td>
                                            &nbsp;</td>
				</tr>
				</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
