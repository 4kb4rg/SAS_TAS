<%@ Page Language="vb" src="../../../include/HR_setup_ManPwrReqDet.aspx.vb" Inherits="HR_setup_ManPwrReqDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Man Power Requirement Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />   
        <Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		
		<Form id=frmCPDet class="main-modul-bg-app-list-pu"  runat="server">
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
					<td  colspan="5"><strong> MAN POWER REQUIREMENT DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20%>M.P.Requirement Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtManPwrReqCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Man Power Requirement Code"
								ControlToValidate=txtManPwrReqCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtManPwrReqCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another man power requirement code." runat=server/>
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
								ErrorMessage="<br>Please Enter Man Power Requirement Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left">Man Power Type :*</td>
					<td align="left">
						<asp:RadioButton id=rbLocal checked groupname=mprtype text=" Local Labour" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top></td>
					<td align="left" valign=top>
						<asp:RadioButton id=rbForeigner groupname=mprtype text=" Foreign Labour" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Total Man Power :*</td>
					<td align="left" valign=top>
						<asp:Textbox id=txtTotal maxlength=21 width=50% text=0 runat=server/>
						<asp:RequiredFieldValidator id=validateTotal display=Dynamic runat=server 
								ErrorMessage="Please Enter Total Man Power"
								ControlToValidate=txtTotal />															
						<asp:CompareValidator id="cvValidateTotal" display=dynamic runat="server" 
							ControlToValidate="txtTotal" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revTotal 
							ControlToValidate="txtTotal"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>&nbsp;</td>
					<td valign=top>
					</td>
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
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=mprcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
