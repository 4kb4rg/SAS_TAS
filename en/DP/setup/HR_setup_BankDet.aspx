<%@ Page Language="vb" src="../../../include/HR_setup_BankDet.aspx.vb" Inherits="HR_setup_BankDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Bank Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmCPDet runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />


        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">




			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma" >
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">BANK DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>Bank Code :* </td>
					<td width=35%>
						<asp:Textbox id=txtBankCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="Please Enter Bank Code"
								ControlToValidate=txtBankCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtBankCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="This code has been used, please try another Bank Code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=15%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td>Description :*</td>
					<td>
						<asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="Please Enter Bank Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left">Bank Account Number :*</td>
					<td align="left">
						<asp:Textbox id=txtBankAcc maxlength=32 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateBankAcc display=Dynamic runat=server 
								ErrorMessage="Please Enter Bank Account Number."
								ControlToValidate=txtBankAcc />					
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Bank Account Owner :*</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtBankAccOwner width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateBankAccOwner display=Dynamic runat=server 
								ErrorMessage="Please Enter Bank Account Owner."
								ControlToValidate=txtBankAccOwner />		
					</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Account Code :*</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlAccCode width=100% runat=server/>
						<asp:Label id=lblErrAccCode visible=false forecolor=red text="Please select Account Code" runat=server />
					</td>
				</tr>

				<tr>
					<td>Transfer Charges :*</td>
					<td>
						<asp:Textbox id=txtTransCharges width=100% maxlength=9 runat=server/>
						<asp:RequiredFieldValidator id="rfvTransCharges" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Transfer Charges" 
							ControlToValidate="txtTransCharges" />																
						<asp:RegularExpressionValidator id="revTransCharges" 
							ControlToValidate="txtTransCharges"
							ValidationExpression="^(\-|)\d{1,7}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 7 digits and 2 decimal points. "
							runat="server"/>
					</td>
				</tr>		
				


				<tr>
					<td align="left" valign=top>Address :</td>
					<td align="left" valign=top>
						<textarea rows="6" id=txtAddress cols="29" runat=server></textarea>
					</td>
					
				</tr>
				<tr>
					<td align="left" valign=top>Autocredit Format :</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlAutoCredit width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Autocredit Bank Bar Code :</td>
					<td valign=top>
						<asp:Textbox id=txtBarCode width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Cheque Format :</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlChequeFormat width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Autocredit Bank Customer ID :</td>
					<td valign=top>
						<asp:Textbox id=txtCustID width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Report Format :</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlReportFormat width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Autocredit Batch No :</td>
					<td valign=top>
						<asp:Textbox id=txtBatchNo width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Bilyet Giro Format :</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlGiroFormat width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Slip Setoran Format :</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlSetoranFormat width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Slip Transfer Format :</td>
					<td align="left" valign=top>
						<asp:Dropdownlist id=ddlTransferFormat width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>SWIFT :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtSwift width=100% runat=server/>
					</td>
				</tr>
				
				<tr>
				    <td height="25" style="font-weight: bolder; font-size: 11px;">Correspondent Bank:
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Description :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtCorBankDescription width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Account No :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtCorBankAccNo width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>SWIFT :</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtCorBankSwift width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Last Integrated Date :</td>
					<td align="left" valign=top>
						<asp:Label id=lblLastDate runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top></td>
					<td valign=top></td>
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
					</td>
				</tr>
				<Input Type=Hidden id=bankcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<tr>
					<td colspan="5">
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
