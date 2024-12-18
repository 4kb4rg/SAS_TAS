<%@ Page Language="vb" trace="false" src="../../../include/PR_setup_RiceRationDet.aspx.vb" Inherits="PR_setup_RiceRationDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_PRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Rice Ration Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
<tr>
	<td style="width: 100%; height: 1500px" valign="top">
	<div class="kontenlist">
			<Input Type=Hidden id=tbcode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server />		
			<asp:Label id=lblCode text=" Code" visible=false runat=server />		
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6"><strong> <asp:label id="lblTitle" runat="server" /> DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=22% height=25><asp:label id="lblRiceCode" runat="server" /> :* </td>
					<td width=28%>
						<asp:Textbox id=txtRiceCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=rfvRiceCode display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Rice Ration Code."
							ControlToValidate=txtRiceCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtRiceCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblRiceDesc" runat="server" /> : </td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server />
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Applicable for Employee Category :* </td>
					<td><asp:dropdownlist id=ddlPayType width=100% runat=server/>
						<asp:RequiredFieldValidator id=rfvPayType display=Dynamic runat=server 
							ErrorMessage="<br>Please select Pay Type."
							ControlToValidate=ddlPayType /></td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Ration for Individual :* </td>
					<!-- Modified BY ALIM maxLength = 15 -->
					<td><asp:Textbox id=txtIndRice maxlength=15 width=50% runat=server/> (kg)
						<asp:RequiredFieldValidator id=rvfIndRice display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Ration for Individual."
							ControlToValidate=txtIndRice />
						<!-- Modified BY ALIM -->	
						<asp:RegularExpressionValidator 
								id=revIndRice
								ControlToValidate=txtIndRice
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<br>Maximum length 9 digits and 5 decimals. "
								runat="server"/></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Ration for Spouse :* </td>
					<!-- Modified BY ALIM maxLength = 15 -->
					<td><asp:Textbox id=txtSpouseRice maxlength=15 width=50% runat=server/> (kg)
						<asp:RequiredFieldValidator id=rvfSpouseRice display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Ration for Spouse."
							ControlToValidate=txtSpouseRice />
						<!-- Modified BY ALIM -->	
						<asp:RegularExpressionValidator 
								id=revSpouseRice
								ControlToValidate=txtSpouseRice
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<br>Maximum length 9 digits and 5 decimals. "
								runat="server"/></td>
					<!-- Modified By BHL -->
					<td>&nbsp;</td>
					<td>Price per Kg (Rp) :* </td>
					<td><asp:Textbox id=txtPricePerKg Text=0  maxlength=19 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfPriceKg display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Price per Kg (Rp)."
							ControlToValidate=txtPricePerKg />
							
						<!-- Modified BY ALIM -->	
						<asp:RegularExpressionValidator 
								id=revPriceKg
								ControlToValidate=txtPricePerKg
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 0 decimals. "
								runat="server"/></td>
					<!-- End Modified -->	
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Ration for Each Child :* </td>
					<!-- Modified BY ALIM maxLength = 15 -->
					<td><asp:Textbox id=txtChildRice maxlength=15 width=50% runat=server/> (kg)
						<asp:RequiredFieldValidator id=rvfChildRice display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Ration for Each Child."
							ControlToValidate=txtChildRice />
						<!-- Modified BY ALIM -->	
						<asp:RegularExpressionValidator 
								id=revChildRice
								ControlToValidate=txtChildRice
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<br>Maximum length 9 digits and 5 decimals. "
								runat="server"/></td>
					<td>&nbsp;</td>
					<!-- Modified By BHL -->
					<td>Bonus Price per Kg (Rp) :* </td>
					<td><asp:Textbox id=txtBonusPricePerKg Text=0 maxlength=19 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfBonusPriceKg display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Bonus Price per Kg (Rp)."
							ControlToValidate=txtBonusPricePerKg />
						<asp:RegularExpressionValidator 
								id=revBonusPriceKg
								ControlToValidate=txtBonusPricePerKg
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 0 decimals. "
								runat="server"/></td>
					<!-- End Modified -->	
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>No. of Children Entitled :*</td>
					<!-- Modified BY ALIM maxLength = 2 -->
					<td><asp:Textbox id=txtTotalChild maxlength=2 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfTotalChild display=Dynamic runat=server 
							ErrorMessage="<br>Please enter No. of Children Entitled."
							ControlToValidate=txtTotalChild />
						<asp:CompareValidator id="cvTotalChild" display=dynamic runat="server" 
							ControlToValidate="txtTotalChild" Text="<br>No. of Children Entitled must be an integer. " 
							Type="integer" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revTotalChild
							ControlToValidate=txtTotalChild
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "<br>Maximum length 2 digits. "
							runat="server"/></td>
					<td>&nbsp;</td>
					<!-- Modified By BHL -->
					<td>THR Price per Kg (Rp) :* </td>
					<td><asp:Textbox id=txtTHRPricePerKg Text=0 maxlength=19 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfTHRPriceKg display=Dynamic runat=server 
							ErrorMessage="<br>Please enter THR Price per Kg (Rp)."
							ControlToValidate=txtTHRPricePerKg />
						<asp:RegularExpressionValidator 
								id=revTHRPriceKg
								ControlToValidate=txtTHRPricePerKg
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 0 decimals. "
								runat="server"/></td>
					<!-- End Modified -->	
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
					<!-- Modified By BHL -->
					<td>JAMSOSTEK Price per Kg (Rp) :* </td>
					<td><asp:Textbox id=txtJAMPricePerKg Text=0 maxlength=19 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfJAMPriceKg display=Dynamic runat=server 
							ErrorMessage="<br>Please enter JAMSOSTEK Price per Kg (Rp)."
							ControlToValidate=txtJAMPricePerKg />
						<asp:RegularExpressionValidator 
								id=revJAMPriceKg
								ControlToValidate=txtJAMPricePerKg
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "<br>Maximum length 19 digits and 0 decimals. "
								runat="server"/></td>
					<!-- End Modified -->	
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="3">&nbsp;</td>
					<td>Bonus :</td>
					<td><asp:CheckBox id=cbBonus text=" Yes" runat=server/></td>
					<td>&nbsp;</td>					
				</tr>				
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del CausesValidation=False runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " imageurl="../../images/butt_back.gif" onclick=BackBtn_Click CausesValidation=False runat=server />
					    <br />
					</td>
				</tr>
			</table>
         	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
