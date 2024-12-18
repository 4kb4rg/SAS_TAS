<%@ Page Language="vb" src="../../../include/HR_setup_JamsostekDet.aspx.vb" Inherits="HR_setup_JamsostekDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_HRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Jamsostek Details</title>
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />    
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmTaxDet class="main-modul-bg-app-list-pu"  runat="server">
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<Input Type=Hidden id=tbcode runat=server />
			<asp:label id=lblHiddenSts text="0" visible=false runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100%  class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuHRSetup id=MenuHRSetup runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6"><strong><asp:label id=lblTitle runat=server/> DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                          <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id=lblJamCode runat=server /> :* </td>
					<td width=25%>
						<asp:TextBox id=txtJamCode width=50% runat=server/>
						<asp:RequiredFieldValidator id=rfvJamCode 
							display=Dynamic 
							runat=server
							ControlToValidate=txtJamCode />	
						<asp:RegularExpressionValidator id=revJamCode 
							ControlToValidate="txtJamCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>														
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblDesc runat=server /> :*</td>
					<td><asp:TextBox id=txtDesc maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=rfvDesc
							display=Dynamic 
							runat=server
							ControlToValidate=txtDesc />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employer Deduction Code : </td>
					<td><asp:DropDownList id=ddlEmprDeCode width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Employee Deduction Code : </td>
					<td>
						<asp:DropDownList id=ddlEmpeDeCode width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Employer Contribution (%) : </td>
					<td>
						<asp:TextBox id=txtEmprRate text="0" width=50% runat=server />
						<asp:RegularExpressionValidator id=revEmprRate 
							ControlToValidate="txtEmprRate"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text="<br>Maximum length 2 digits with 2 decimal points. "
							runat="server"/>
						<asp:label id=lblErrEmprRate text="Employer Contribution must be from 1 to 100." forecolor=red visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td valign=top>Employee Contribution (%) : </td>
					<td>
						<asp:TextBox id=txtEmpeRate text="0" width=50% runat=server />
						<asp:RegularExpressionValidator id=revEmpeRate 
							ControlToValidate="txtEmpeRate"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text="<br>Maximum length 2 digits with 2 decimal points. "
							runat="server"/>
						<asp:label id=lblErrEmpeRate text="Employee Contribution must be from 1 to 100." forecolor=red visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign=top>Category : </td>
					<td>
						<asp:radiobutton id=rbJKK GroupName=JamCategory checked=true runat=server/><br>
						<asp:radiobutton id=rbJK GroupName=JamCategory runat=server/><br>
						<asp:radiobutton id=rbJHT GroupName=JamCategory runat=server/><br>
						<asp:radiobutton id=rbJPK GroupName=JamCategory runat=server/>
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><asp:label id=lblErrSelectOne text="Please select one Employer or Employee Deduction Code." forecolor=red visible=false runat=server />
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
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
