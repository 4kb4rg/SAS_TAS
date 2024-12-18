<%@ Page Language="vb" src="../../../include/WS_Setup_CalendarMachineDet.aspx.vb" Inherits="WS_Setup_CalendarMachineDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Calendarized Machine Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<asp:label id=lblSelect visible=false text="Select " runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuWS id=MenuWS runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">CALENDARIZED MACHINE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id=lblBlkTag Runat="server"/> :*</td>
					<td width=30%><asp:DropDownList id="ddlBlock" Width=100% AutoPostBack=True OnSelectedIndexChanged=onSelect_Block runat=server />
								  <asp:label id=lblErrBlock Visible=False forecolor=red Runat="server" /></td>
					<td>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblSubBlkTag Runat="server"/> :*</td>
					<td><asp:DropDownList id="ddlSubBlock" Width=100% AutoPostBack=True runat=server />
						<asp:label id=lblErrSubBlock Visible=False forecolor=red Runat="server" /></td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>All Month : </td>
					<td><asp:CheckBox id="chkAllMonth" width=100% OnCheckedChanged="chkAllMonth_CheckedChanged" AutoPostBack="True" runat="server" /></td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Month : *</td>
					<td><asp:DropDownList id="ddlMonth" Width=100% runat=server>	
							<asp:ListItem value="1" selected>January</asp:ListItem>
							<asp:ListItem value="2">February</asp:ListItem>
							<asp:ListItem value="3">March</asp:ListItem>
							<asp:ListItem value="4">April</asp:ListItem>
							<asp:ListItem value="5">May</asp:ListItem>
							<asp:ListItem value="6">June</asp:ListItem>
							<asp:ListItem value="7">July</asp:ListItem>
							<asp:ListItem value="8">August</asp:ListItem>
							<asp:ListItem value="9">September</asp:ListItem>
							<asp:ListItem value="10">October</asp:ListItem>
							<asp:ListItem value="11">November</asp:ListItem>
							<asp:ListItem value="12">December</asp:ListItem>
						</asp:DropDownList>
						<asp:label id=lblErrMonth Visible=False forecolor=red Runat="server" /></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Week I</td>
					<td><asp:CheckBox id="chkWeek1" width=100% runat="server" /></td>
				</tr>			
				<tr>
					<td>Week II</td>
					<td><asp:CheckBox id="chkWeek2" width=100% runat="server" /></td>
				</tr>
				<tr>
					<td>Week III</td>
					<td><asp:CheckBox id="chkWeek3" width=100% runat="server" /></td>
				</tr>
				<tr>
					<td>Week IV</td>
					<td><asp:CheckBox id="chkWeek4" width=100% runat="server" /></td>
				</tr>
				<tr>
					<td>Week V</td>
					<td><asp:CheckBox id="chkWeek5" width=100% runat="server" /></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>									
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=SaveBtn_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText="  Delete  " imageurl="../../images/butt_delete.gif" onclick="DeleteBtn_Click" runat="server" />
						<asp:ImageButton id=UnDelBtn AlternateText="  Undelete  " imageurl="../../images/butt_undelete.gif" onclick=UnDeleteBtn_Click runat=server  visible=false/>
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=tbcode runat=server />
				<Input type=hidden id=hidBlockCharge value="" runat=server/>
				<Input type=hidden id=hidChargeLocCode value="" runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:label id=lblCloseExist visible=false text="no" runat=server/>
				<asp:label id=lblLifespan visible=false text=0 runat=server/>	
				<asp:label id=lblActHourMeter visible=false text=0 runat=server/>	
				<asp:Label id=lblErrDupl visible=false forecolor=red text="Data already exists." runat=server/>
				<tr>
					<td colspan="6">
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
