<%@ Page Language="vb" src="../../../include/PR_setup_HarvIncDet.aspx.vb" Inherits="PR_setup_HarvIncDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Harvesting Incentive Details</title>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=hicode runat=server />

			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">HARVESTING INCENTIVE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Harvesting Incentive Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtHarvIncCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Harvesting Incentive Code"
							ControlToValidate=txtHarvIncCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtHarvIncCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description :*</td>
					<td><asp:Textbox id=txtDescription maxlength=64 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter HarvInc Description"
							ControlToValidate=txtDescription />
						<asp:Label id=lblErrField visible=false forecolor=red text="Please key in Harvesting Incentive Code and Description." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Allowance & Deduction Code :*</td>
					<td><asp:DropDownList id=ddlAD width=85% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlAD','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrAD visible=false forecolor=red text="<br>Please select Allowance & Deduction code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Divison of Labour :</td>
					<td valign=top>
						<asp:radiobuttonlist id=rdDivLabour OnSelectedIndexChanged=ChangeUOM AutoPostBack=true runat=server />
					</td>
					<td>&nbsp;</td>					
					<td height=25 valign=top Align=Left>Updated By :</td>
					<td height=25 valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>Progressive Status :</td>
					<td >
						<table width=100% cellpadding=1 cellspacing=0>
							<tr>
								<td width=20% height=25>
									<asp:RadioButton id=rbIncTypeFixed text=" Progressive" groupname="inctype" OnCheckedChanged=HidePaymentBasis autopostback=true runat=server/>														
								</td>
								<td width=20% height=25>
									<asp:RadioButton id=rbIncTypeMeasure text=" Non Progressive" groupname="inctype" OnCheckedChanged=HidePaymentBasis autopostback=true runat=server/>
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>							
				<tr id=TrPayBasis runat=server>
					<td height=25>Type :</td>
					<td><asp:DropDownList id=ddlType width=85% runat=server/>		
					</td>
					<td colspan=4>&nbsp;</td>									
				</tr>
				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
						<tr>						
							<td>
								<table border=0 cellpadding=2 cellspacing=0 width=100%>				
									<tr>
										<td valign=top height=25 width=20%>Quota in Quantity:*</td>										
										<td valign=top width=35%>
											<asp:Textbox id=txtQuota width=55%  maxlength=15 runat=server/>
											<asp:CompareValidator id="cvQuota" display=dynamic runat="server" 
												ControlToValidate="txtQuota" Text="<br>The value must be a whole number with decimal. " 
												Type="double" Operator="DataTypeCheck"/>											
											<asp:RegularExpressionValidator id=revQuotaQty 
												ControlToValidate="txtQuota"
												ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
												Display="Dynamic"
												text = "Maximum length 9 digits and 5 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrQuota visible=false forecolor=red text="<br>Please enter quota in quantity." runat=server/>							
											<asp:DropDownList id=ddlUOM width=40% visible=False runat=server/>
											<asp:Label id=lblUOM width=40% runat=server />
										</td>										
										<td>&nbsp;</td>
										<td height=25 valign=top>
											<asp:Label id=lblErrDet visible=false forecolor=red 
											text="<br>This Quota in Quantity Level have been used, please try another Quota Quantity Level." runat=server/>
										</td>										
										<td>&nbsp;</td>
										<td>&nbsp;</td>
									</tr>				
									<tr>
										<td valign=top height=25 width=20%>Quota Incentive :*</td>
										<td valign=top width=20%>
											<asp:Textbox id=txtQuotaInc width=55% maxlength=20 runat=server/>
											<asp:CompareValidator id="cvQuotaInc" display=dynamic runat="server" 
												ControlToValidate="txtQuotaInc" Text="<br>The value must be a whole number with integer. " 
												Type="integer" Operator="DataTypeCheck"/>											
											<asp:RegularExpressionValidator id=revQuotaInc 
												ControlToValidate="txtQuotaInc"
												ValidationExpression="\d{1,19}"
												Display="Dynamic"
												text = "Maximum length 19 digits and 0 decimal points."
												runat="server"/>						
											<asp:Label id=lblErrQuotaInc visible=false forecolor=red text="<br>Please enter Quota Incentive." runat=server/>																			
										</td>
										<td>&nbsp;</td>
										<td width=18%>Above Quota Incentive :*</td>
										<td width=55%>
											<asp:Textbox id=txtAboveQuotaInc width=60% maxlength=20 runat=server/>
											<asp:CompareValidator id="cvAboveQuotaInc" display=dynamic runat="server" 
												ControlToValidate="txtAboveQuotaInc" Text="<br>The value must be a whole number with integer. " 
												Type="integer" Operator="DataTypeCheck"/>											
											<asp:RegularExpressionValidator id=revAboveQuotaInc 
												ControlToValidate="txtAboveQuotaInc"
												ValidationExpression="\d{1,19}"
												Display="Dynamic"
												text = "Maximum length 19 digits and 0 decimal points."
												runat="server"/>						
											<asp:Label id=lblErrAboveQuotaInc visible=false forecolor=red text="<br>Please enter Above Quota Incentive." runat=server/>												
										</td>
										<td>&nbsp;</td>																				
									</tr>		
									<tr>
										<td valign=top height=25 width=20%>Lose Fruit Rate :*</td>
										<td valign=top width=20%>
											<asp:Textbox id=txtRateFruit width=60% maxlength=20 runat=server/>											
											<asp:CompareValidator id="cvRateFruit" display=dynamic runat="server" 
												ControlToValidate="txtRateFruit" Text="<br>The value must whole number." 
												Type="Integer" Operator="DataTypeCheck"/>
											<asp:RangeValidator id="Range3"
												ControlToValidate="txtRateFruit"
												MinimumValue="0"
												MaximumValue="999999999999999"
												Type="double"
												EnableClientScript="True"
												Text="<br>The value must be from 1."
												runat="server" display="dynamic"/>
											<asp:label id=lblErrRateFruit text="Please enter Lose Fruit Rate." visible=false forecolor=red runat=server/>
										</td>
										<td colspan=4 >&nbsp;</td>																				
									</tr>															
									<tr class="mb-c">
										<TD vAlign="top" colspan=6 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" onclick=btnAdd_Click alternatetext=Add runat=server /></TD>
									</tr>
								</table>
							</td>
						</tr>									
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="6">		    						
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete
							Pagerstyle-Visible=False
							AllowSorting="True">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>						
								<asp:TemplateColumn HeaderText="Quota in Quantity">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QuotaQty"),5) %> id="lblLoadCode" runat="server" />
										<asp:Label Text=<%# Container.DataItem("QuotaQty") %> id="lblHdQuotaQty" visible=False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quota Incentive">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QuotaInc"),0) %> id="lblTotalTrip" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Above Quota Incentive">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("AbvQuotaInc"),0) %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Loss Fruit Rate">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("LFruitRate"),0) %> id="lblVehExp" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("HarvIncCode") %> id="lblHarvIncCode" visible=False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>										
							</Columns>
						</asp:DataGrid>		
					</td>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
		</form>
	</body>
</html>
