<%@ Page Language="vb" src="../../../include/CM_Setup_SellerDet.aspx.vb" Inherits="CM_Setup_SellerDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_cm_setup" src="../../menu/menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Seller Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmSeller runat="server"class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:menu_cm_setup id=menu_cm_setup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">SELLER DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>Seller Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtSellerCode width=50% runat=server/>
						<asp:RequiredFieldValidator 
							id=validateCode 
							display=Dynamic 
							runat=server
							ControlToValidate=txtSellerCode />	
						<asp:RegularExpressionValidator 
							id=revCode 
							ControlToValidate="txtSellerCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>														
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>Code already exist." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td>Description :*</td>
					<td>
						<asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator 
							id=validateDesc 
							display=Dynamic 
							runat=server
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td>Address :*</td>
					<td rowspan=5>
						<textarea rows="6" id=txtAddress cols="29" runat=server></textarea>
						<asp:Label id=lblErrAddress visible=false forecolor=red text="<br>Maximum length for address is up to 512 characters only." runat=server />
						<asp:RequiredFieldValidator id=validateAddress display=dynamic runat=server 
							ErrorMessage="<br>Please enter address. " 
							ControlToValidate=txtAddress />
					</td>
					<td>&nbsp;</td>
					<td valign=top>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>			
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td>Telephone No. :*</td>
					<td><asp:TextBox id=txtTelNo width=100% maxlength=16 runat=server />
						<asp:RequiredFieldValidator id=validateTelNo display=dynamic runat=server 
							ErrorMessage="Please enter Telephone Number." 
							ControlToValidate=txtTelNo />
						<asp:RegularExpressionValidator id="revTelNo" 
							ControlToValidate="txtTelNo"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
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
