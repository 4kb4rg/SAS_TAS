<%@ Page Language="vb" src="../include/change_password.aspx.vb" Inherits="change_password" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<html>
<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Change Password</title>
</head>

<body class="menu" topmargin="0" leftmargin="0">

<form id=frmLogin runat=server>

	<table width=100% border=0 cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><span class="lb-ti">Change Password</span></td>
				</tr>
				
	</table>

	<table border=0 width=100% cellspacing=1>
		
		<tr>
			<td></td>
			<td>User ID:<br>
				<asp:TextBox id=txtUserId width=100% maxlength=8 runat=server />
				<asp:RegularExpressionValidator id=revUserId 
					ControlToValidate="txtUserId"
					ValidationExpression="[a-zA-Z0-9\-]{1,8}"
					Display="Dynamic"
					text="Alphanumeric without any space in between only."
					runat="server"/>
			</td>
		</tr>
		<tr>
			<td></td>
			<td>Password:<br><asp:TextBox textmode=password id=txtPwd width=100% maxlength=8 runat=server /></td>
		</tr>
		<tr>
			<td></td>
			<td>New Password:<br><asp:TextBox textmode=password id=txtNewPwd width=100% maxlength=8 runat=server /></td>
		</tr>
		<tr>
			<td></td>
			<td>Reenter New Password:<br><asp:TextBox textmode=password id=txtConfirmPwd width=100% maxlength=8 runat=server /></td>
		</tr>
		<tr>
			<td></td>
			<td>
				<asp:ImageButton imageurl="images/butt_change.gif" AlternateText="Change" onclick=changeBtn_Click id=changeBtn runat=server /> 
				<asp:ImageButton imageurl="images/butt_back.gif" AlternateText=" Back " CausesValidation=False onclick=BackBtn_Click id=BackBtn runat=server />
			</td>
		</tr>
		<tr class="lb-m">
			<td></td>
			<td>
				<asp:Label id=lblChangeResult forecolor=red runat=server />
				<asp:RequiredFieldValidator id=validateUserID display=dynamic runat=server 
					ErrorMessage="Please enter your User ID." 
					ControlToValidate=txtUserId />					
				<asp:RequiredFieldValidator id=validatePwd display=dynamic runat=server 
					ErrorMessage="Please enter your Password." 
					ControlToValidate=txtPwd />					
				<asp:RequiredFieldValidator id=validateConfirmPwd display=dynamic runat=server 
					ErrorMessage="Please re-enter your Password." 
					ControlToValidate=txtConfirmPwd />					
				<asp:RequiredFieldValidator id=validateNewPwd display=dynamic runat=server 
					ErrorMessage="Please your new Password." 
					ControlToValidate=txtNewPwd />					
			</td>
		</tr>		
	</table>
	<asp:Label id=lblErrReEnter visible=false Text="Password and re-enter password is not matched." runat=server />
	<asp:Label id=lblErrNotChg visible=false Text="Password is not changed, please try again." runat=server />
	<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
</form>

</body>

</html>
