<%@ Page Language="vb" src="../../../include/HR_setup_CPdet.aspx.vb" Inherits="HR_setup_CPdet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Career Progress Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmCPDet runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="lblEnter" visible="false" text="Please enter " runat="server" />
			<asp:label id="lblSelect" visible="false" text="Please select " runat="server" />
			<asp:label id="lblList" visible="false" text="Select " runat="server" />

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">

			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5"><asp:label id="lblTitle" runat="server" /> DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%><asp:label id="lblCP" runat="server" /> Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtCPCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ControlToValidate=txtCPCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtCPCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDupCP visible=false forecolor=red text="This code has been used, please try another code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td><asp:label id="lblDesc" runat="server" /> :*</td>
					<td>
						<asp:Textbox id=txtDescription maxlength=64 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ControlToValidate=txtDescription />															
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left"><asp:label id="lblCPType" runat="server" /> :*</td>
					<td align="left">
						<asp:DropDownList id="ddlCPType" width=100% onSelectedIndexChanged="ChangeBlackList" AutoPostBack=true runat=server>
							<asp:ListItem value="" Selected>(please select one)</asp:ListItem>
							<asp:ListItem value="1">Appointment</asp:ListItem>
							<asp:ListItem value="2">Confirmation</asp:ListItem>										
							<asp:ListItem value="3">Cease</asp:ListItem>
							<asp:ListItem value="4">Inter-movement In</asp:ListItem>
							<asp:ListItem value="5">Inter-movement Out</asp:ListItem>
							<asp:ListItem value="6">External Movement</asp:ListItem>
							<asp:ListItem value="7">Fired</asp:ListItem>
							<asp:ListItem value="8">Pass Away</asp:ListItem>
							<asp:ListItem value="9">Retirement</asp:ListItem>
						</asp:DropDownList>
						<asp:Label id=lblErrCPType visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr id=trBlackList runat="server">
					<td align="left">Allow to be black listed?</td>
					<td align="left">
						<asp:Checkbox id=cbBlackList width=100% text=" Yes" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left">Allow to define range of calendar date? </td>
					<td align="left">
						<asp:Checkbox id=cbPeriod width=100% text=" Yes" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" onclick=Button_Click CausesValidation=false CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=cpcode runat=server />
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
