<%@ Page Language="vb" src="../../../include/HR_setup_TaxBranchDet.aspx.vb" Inherits="HR_setup_TaxBranchDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Tax Branch Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmCPDet runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">TAX BRANCH DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>Tax Branch Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtTaxBranchCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="Please Enter Tax Branch Code"
								ControlToValidate=txtTaxBranchCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtTaxBranchCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="This code has been used, please try another tax branch code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td>Description :*</td>
					<td>
						<asp:Textbox id=txtDescription maxlength=64 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="Please Enter Tax Branch Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left">Employer Tax No :*</td>
					<td align="left">
						<asp:Textbox id=txtEmpTaxNo maxlength=32 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateTaxNo display=Dynamic runat=server 
								ErrorMessage="Please Enter Employer Tax No"
								ControlToValidate=txtEmpTaxNo />					
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Address :</td>
					<td align="left" valign=top>
						<textarea rows="6" id=txtAddress cols="29" runat=server></textarea>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
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
				<Input Type=Hidden id=tbcode runat=server />
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
