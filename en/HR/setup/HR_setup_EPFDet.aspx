<%@ Page Language="vb" src="../../../include/HR_setup_EPFDet.aspx.vb" Inherits="HR_setup_EPFDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Provident Fund Details</title>
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
					<td  colspan="5"><strong>EMPLOYEE PROVIDENT FUND DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20%>E.P.F Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtEPFCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Employee Provident Fund Code"
								ControlToValidate=txtEPFCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtEPFCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another EPF code." runat=server/>					
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td>Description :*</td>
					<td>
						<asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter EPF Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left">Employer EPF Deduction Code :*</td>
					<td align="left">
						<asp:DropDownList id=ddlEmprDeductCode width=80% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlEmprDeductCode','4',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmprDeductCode visible=false forecolor=red text="<br>Please select a code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Employee EPF Deduction Code :*</td>
					<td align="left" valign=top>
						<asp:DropDownList id=ddlEmpeDeductCode width=80% runat=server/>
						<input type="button" id=btnFind2 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlEmpeDeductCode','2',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrEmpeDeductCode visible=false forecolor=red text="<br>Please select a code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Minimum Employee Income :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtMinIncome maxlength=21 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateMinIncome" display=dynamic runat="server" 
							ControlToValidate="txtMinIncome" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revMinIncome 
							ControlToValidate="txtMinIncome"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td valign=top></td>
					<td valign=top>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Maximum EPF not deductable from Tax :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtMaxAmount maxlength=21 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateMaxAmount" display=dynamic runat="server" 
							ControlToValidate="txtMaxAmount" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revMaxAmount 
							ControlToValidate="txtMaxAmount"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td valign=top></td>
					<td valign=top>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Employer Contribution % :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtEmprContributePercent maxlength=3 width=50% text=0 runat=server/>
						<asp:CompareValidator id="cvValidatePercent" display=dynamic runat="server" 
							ControlToValidate="txtEmprContributePercent" Text="<br>The value must whole number. " 
							Type="Integer" Operator="DataTypeCheck"/>
						<asp:Label id=lblErrEmployer visible=false forecolor=red text="<br>Key in either Employer Contribution in percentage or, in amount." runat=server/>
						<asp:Label id=lblErrEmployerPercent visible=false forecolor=red text="<br>The contribution should not more than 100 percent." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Employer Contribution $ :</td>
					<td valign=top>
						<asp:Textbox id=txtEmprContributeAmount maxlength=21 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateAmount" display=dynamic runat="server" 
							ControlToValidate="txtEmprContributeAmount" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revEmprContributeAmount
							ControlToValidate="txtEmprContributeAmount"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Employee Contribution % :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtEmpeContributePercent maxlength=3 width=50% text=0 runat=server/>
						<asp:CompareValidator id="cvValidatePercent2" display=dynamic runat="server" 
							ControlToValidate="txtEmpeContributePercent" Text="<br>The value must whole number. " 
							Type="Integer" Operator="DataTypeCheck"/>
						<asp:Label id=lblErrEmployee visible=false forecolor=red text="<br>Key in either Employee Contribution in percentage or, in amount." runat=server/>
						<asp:Label id=lblErrEmployeePercent visible=false forecolor=red text="<br>The contribution should not more than 100 percent." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Employee Contribution $ :</td>
					<td valign=top>
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
					</td>
				</tr>

				<tr>
					<td colspan="5"><asp:label id=lblErrConsistent visible=false forecolor=red text="Please use both (employee and employer) in percentage or both in amount." runat=server/> &nbsp;</td>
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
				<Input Type=Hidden id=epfcode runat=server />
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
