<%@ Page Language="vb" src="../../../include/PR_Setup_MedicalDet.aspx.vb" Inherits="PR_Setup_MedicalDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Medical Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblDefault" visible="false" text="Default" runat="server" />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=medicalcode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6"><asp:Label id=lblTitle runat=server /> DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="20%" height="25"><asp:Label id=lblTitle1 runat=server /> Code :* </td>
					<td width="30%">
						<asp:Textbox id="txtMedicalCode" width="50%" maxlength="8" runat="server"/>
						<asp:Label id=lblErrDupMedicalCode visible=false forecolor=red text="<br>Medical code in used, please try other Medical Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Medical Code"
							ControlToValidate=txtMedicalCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtMedicalCode"
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
					<td><asp:dropdownlist id=ddlMedicalType width=100% runat=server/>
						<asp:Label id=lblErrMedicalType visible=false forecolor=red text="<br>Please select one Medical Type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employee Category :*</td>
					<td><asp:DropDownList id=ddlEmpCategory width=85% runat=server/> 
						<asp:Label id=lblErrEmpCategory visible=false forecolor=red text="<br>Please select Employee Category." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Allowance & Deduction Code :*</td>
					<td colspan=2><asp:DropDownList id=ddlAD width=100% runat=server onselectedindexchanged=onChange_ADcode autopostback=true/></td>
					<td><input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlAD','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/></td>
						<asp:Label id=lblErrAD visible=false forecolor=red text="<br>Please select Allowance & Deduction code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Rate :*</td>
					<td><asp:Textbox id=txtMedicalRate maxlength=15 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfMedicalRate display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Medical rate."
							ControlToValidate=txtMedicalRate />
						<asp:CompareValidator id="cvMedicalRate" display=dynamic runat="server" 
							ControlToValidate="txtMedicalRate" Text="<br>Medical Rate must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revMedicalRate
							ControlToValidate=txtMedicalRate
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimals. "
							runat="server"/></td>
					<td colspan="4">(Amount = Rate x Basic Salary)</td>
					<td>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:Label id=lblErrCheckMedical1 visible=false forecolor=red Text="<BR>Selected Employee Category and Medical Type have been used. <BR>Please select other Employee Category or Medical Type." runat=server />
						<asp:Label id=lblErrCheckMedical2 visible=false forecolor=red Text="<BR>Selected ADCode has been used. <BR>Please select other AD Code." runat=server />
						<asp:Label id=lblErrCheckMedical3 visible=false forecolor=red Text="<BR>Selected Data has been used." runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
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
