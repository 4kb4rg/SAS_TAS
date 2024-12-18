<%@ Page Language="vb" src="../../../include/GL_Setup_VehicleSubCodeDet.aspx.vb" Inherits="GL_Setup_VehicleSubCodeDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Expense Code Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmVehExpenseCodeDet class="main-modul-bg-app-list-pu" runat="server">

                        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=tbcode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                    <asp:label id="lblTitle" runat="server" /> CODE DETAILS</td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                          <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblVehExpCode" runat="server" /> :* </td>
					<td width=30%>
						<asp:Textbox id=txtVehExpenseCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ControlToValidate=txtVehExpenseCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtVehExpenseCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehExpDesc" runat="server" /> :*</td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:Label id=lblVehExpGrp runat=server/> :*</td>
					<td><asp:DropDownList id=ddlVehExpGrp width=100% runat=server/>
						<asp:Label id=lblErrVehExpGrp visible=false forecolor=red text="Please select Vehicle Expense Group Code." display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>

				<tr>
					<td height=25>Unit of Measurement :*</td>
					<td><asp:DropDownList id=ddlUOM width=100% runat=server/>
						<asp:Label id=lblErrUOM visible=false forecolor=red text="Please select Unit of Measurement." display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td>Running Cost :</td>
					<td><asp:CheckBox id=cbRunCost text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
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
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
