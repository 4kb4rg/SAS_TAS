<%@ Page Language="vb" src="../../../include/System_user_DailyControl_Det.aspx.vb" Inherits="System_user_DailyControl_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Vehicle Details</title>
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">

   		 <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">  

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=tbcode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100%  class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr class="font12Tahoma" >
					<td   colspan="6"> <asp:label id="lblTitle" runat="server" /> DETAILS</td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25>
                        User Level :</td>
					<td width=30%>
						<asp:DropDownList id=ddlUser width="50%" runat=server/>
                        <asp:Label id=lblUserID runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
                <tr>
                    <td height="25">
                        Location :</td>
                    <td>
                        <asp:DropDownList id=DDLLocation width="50%" runat=server/>
                        <asp:Label id=LblLocation runat=server />
                        <asp:Label id=LblLocName runat=server /></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
				<tr>
					<td height=25>
                        Maximum Day :</td>
					<td><asp:Textbox id=TxtDay maxlength=128 width="30%" runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ControlToValidate=txtDay />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25></td>
					<td>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25> </td>
					<td></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25> </td>
					<td></td>
					<td></td>
					<td> </td>
					<td>
					</td>
					<td>&nbsp;</td>						
				</tr>
				
				<tr>
					<td height=25></td>
					<td>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText=Delete onClick=Delete_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
			</table>
            &nbsp;
                </div>
            </td>
        </tr>
        </table>    
		</form>
	</body>
</html>
