<%@ Page Language="vb" src="../../../include/PR_YearEnd_Process.aspx.vb" Inherits="PR_yearend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRYearEnd" src="../../menu/menu_PRYearEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Payroll Year End Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma">
			<tr>
				<td colspan=5 align=center><UserControl:MenuPRYearEnd id=MenuPRYearEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>PAYROLL YEAR END PROCESS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td width=20%>Year End Accounting Year :</td>
				<td width=35%><asp:DropDownList id="lstYearEnd" AutoPostBack=true width="30%" runat="server" /></td>
				<td width=5%>&nbsp;</td>
				<td width=5%>&nbsp;</td>
				<td width=35%>&nbsp;</td>
			</tr>																				
			<tr>
				<td colspan=5 height=25><asp:Label id=lblYearEnd visible=false forecolor=red text="Year End Process Successful." runat=server/>&nbsp;</td>
				<td height=25><asp:Label id=lblErrYearEnd visible=false forecolor=red text="Year End Process Failed." runat=server/>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_yrend.gif" AlternateText="Proceed with year end process" OnClick="btnProceed_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan=5>
                                            <asp:Button ID="AddBtn2" runat="server" class="button-small" Text="Proceed with year end process" />							
				</td>
			</tr>
		</table>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />

        <br />
        </div>
        </td>
        </tr>
        </table>



		</form>
	</body>
</html>
