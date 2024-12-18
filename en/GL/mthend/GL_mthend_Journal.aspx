<%@ Page Language="vb" src="../../../include/GL_MthEnd_Journal.aspx.vb" Inherits="GL_mthend_Journal" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLMthEnd" src="../../menu/menu_GLMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Ledger Journal Month End Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain   runat=server>
  <div class="kontenlist">
		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma" >
			<tr>
				<td colspan=5 align=center><UserControl:MenuGLMthEnd id=MenuGLMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>GENERAL LEDGER JOURNAL ADJUSTMENT PROCESS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red>Important notes before journal adjustment process:</font><p>
					1. Please backup up the database before proceed.<br>
					2. Ensure no user is in the system.<br>
					3. All journal vouchers have been generated.
				</td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td width=40% height=25>Accounting Period To Be Processed :</td> 
				<td width=30%>
					<asp:DropDownList id=ddlAccMonth runat=server/> / 
					<asp:DropDownList id=ddlAccYear OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod AutoPostBack=True runat=server />
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrLic Forecolor=Red Visible=False Text="System failed to process journal adjustment for the reason of License/Configuration. Kindly check System Configuration and try again." Runat=Server />
					<asp:Label id=lblSuccess Forecolor=Blue Visible=False Text="Successfully processed journal adjustment for the given accounting period." Runat=Server />
					<asp:Label id=lblErrPosted Forecolor=Red Visible=False Text="System failed to process journal adjustment for the reason of found journal adjustment with active status. Kindly post all journal adjustment for the period given and try again." Runat=Server />
					<asp:Label id=lblErrOther Forecolor=Red Visible=False Text="System failed to process journal adjustment for the reason of some unknown errors are found. Kindly contact system administrator for assistance." Runat=Server />
					<asp:Label id=lblMessage Forecolor=Red Visible=False Text="System required period configuration to process your request. Please set period configuration for the year of." Runat=Server />
				</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with journal adjustment process" OnClick="btnProceed_Click" runat="server" />
				&nbsp;</td>
			</tr>
		</table>
 </div>
		</form>
	</body>
</html>
