<%@ Page Language="vb" src="../../../include/PR_Setup_DanaPensiunDet.aspx.vb" Inherits="PR_Setup_DanaPensiunDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Dana Pensiun Details</title>
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblDefault" visible="false" text="Default" runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=danapensiuncode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6"><strong> <asp:Label id=lblTitle runat=server /> DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=6> <hr style="width :100%" /></td>
				</tr>
				<tr>
					<td width="20%" height="25"><asp:Label id=lblTitle1 runat=server /> Code :* </td>
					<td width="30%">
						<asp:Textbox id="txtDanaPensiunCode" width="50%" maxlength="8" runat="server"/>
						<asp:Label id=lblErrDupDanaPensiunCode visible=false forecolor=red text="<br>Dana Pensiun code in used, please try other Dana Pensiun Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Dana Pensiun Code"
							ControlToValidate=txtDanaPensiunCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtDanaPensiunCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height="25"><asp:Label id=lblTitle2 runat=server /> Description :* </td>
					<td><asp:Textbox id=txtDesc maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Description"
							ControlToValidate=txtDesc />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Type :*</td>
					<td><asp:dropdownlist id=ddlDanaPensiunType width=100% autopostback=true onselectedindexchanged=onChange_DPType runat=server/>
						<asp:Label id=lblErrDanaPensiunType visible=false forecolor=red text="<br>Please select one Dana Pensiun Type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:Label id=lblEmpCategory Text="Employee Category :*" runat=server /></td>
					<td><asp:dropdownlist id=ddlEmpCategory width=100%  runat=server/>
						<asp:Label id=lblErrEmpCategory visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Allowance & Deduction Code :*</td>
					<td><asp:DropDownList id=ddlAD width=85% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlAD','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrAD visible=false forecolor=red text="<br>Please select Allowance & Deduction code." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25 valign=top><asp:Label id=lblRate Text="Rate :*" runat=server /></td>
					<td><asp:Textbox id=txtDanaPensiunRate maxlength=15 width=50% runat=server/>
						<asp:Label id=lblErrDanaPensiunRate visible=false forecolor=red text="<br>Please Enter Dana Pensiun Rate." runat=server/>
						<asp:CompareValidator id="cvDanaPensiunRate" display=dynamic runat="server" 
							ControlToValidate="txtDanaPensiunRate" Text="<br>Dana Pensiun Rate must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revDanaPensiunRate
							ControlToValidate=txtDanaPensiunRate
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimals. "
							runat="server"/></td>
					<td colspan="6">(Amount = Rate x Basic Salary)</td>
					<td colspan=4>&nbsp;</td>					
				</tr>
				
				
				<tr>
					<td height=25 valign=top><asp:Label id=lblTotalAmount Text="Total Amount :*" runat=server /></td>
					<td><asp:Textbox id=txtTotalAmount maxlength=15 width=50% runat=server/>
						<asp:Label id=lblErrTotalAmount visible=false forecolor=red text="<br>Please Enter Dana Pensiun Total Amount." runat=server/>
						<asp:CompareValidator id="cvTotalAmount" display=dynamic runat="server" 
							ControlToValidate="txtTotalAmount" Text="<br>Total Amount must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revTotalAmount
							ControlToValidate=txtTotalAmount
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimals. "
							runat="server"/></td>				
				</tr>
				<tr>
					<td height=25 valign=top><asp:Label id=lblBalance Text= "Balance :*" runat=server /></td>
					<td><asp:Textbox id=txtBalance maxlength=15 width=50% runat=server/>
						<asp:Label id=lblErrBalance visible=false forecolor=red text="<br>Please Enter Dana Pensiun Balance." runat=server/>
						<asp:CompareValidator id="cvBalance" display=dynamic runat="server" 
							ControlToValidate="txtBalance" Text="<br>Balance must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revBalance
							ControlToValidate=txtBalance
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimals. "
							runat="server"/></td>				
				</tr>
				<tr>
					<td height=25 valign=top><asp:Label id=lblAmount Text="Amount :*" runat=server /></td>
					<td><asp:Textbox id=txtAmount maxlength=15 width=50% runat=server/>
						<asp:Label id=lblErrAmount visible=false forecolor=red text="<br>Please Enter Dana Pensiun Amount." runat=server/>
						<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
							ControlToValidate="txtAmount" Text="<br>Amount must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revlAmount
							ControlToValidate=txtAmount
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimals. "
							runat="server"/></td>				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:Label id=lblErrCheckDP1 visible=false forecolor=red Text="<BR>Selected Employee Category have been used. <BR>Please select other Employee Category." runat=server />
						<asp:Label id=lblErrCheckDP2 visible=false forecolor=red Text="<BR>Data has been used." runat=server />
						<asp:Label id=lblErrCheckDP3 visible=false forecolor=red Text="<BR>Selected Employee Code have been used. <BR>Please select other Employee Code." runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
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
