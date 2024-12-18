<%@ Page Language="vb" src="../../../include/PR_mthend_spkprocess_estate.aspx.vb" Inherits="PR_mthend_spkprocess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Hk Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet"
            type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess class="main-modul-bg-app-list-pu" runat=server>
			
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
                    <tr>
					<td colspan=3 class="font9Tahoma" width="100%" >
                        PROSES BAPP SPK</td>
				    </tr>
                                    <tr>
					<td colspan=3 class="font9Tahoma" width="100%" >
                        <hr style="width :100%" />   
                        </td>
				    </tr>
				<tr valign=top>
					<td height=25 width=20%>&nbsp;</td>
					<td width=50%>	&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonth" width="20%" runat=server>
										<asp:ListItem value="1">January</asp:ListItem>
										<asp:ListItem value="2">February</asp:ListItem>
										<asp:ListItem value="3">March</asp:ListItem>
										<asp:ListItem value="4">April</asp:ListItem>
										<asp:ListItem value="5">May</asp:ListItem>
										<asp:ListItem value="6">June</asp:ListItem>
										<asp:ListItem value="7">July</asp:ListItem>
										<asp:ListItem value="8">Augustus</asp:ListItem>
										<asp:ListItem value="9">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddlyear width="20%" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4 style="height: 19px">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_process.gif" alternatetext="Generate" runat=server /></td>
				</tr>
				<tr>
                    <td colspan="4">
			        <asp:Label id=lblErrMessage visible=false Text="Error while initiating component."  runat=server />
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
