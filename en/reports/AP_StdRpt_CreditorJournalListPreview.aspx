<%@ Page Language="vb" trace=false Src="../../include/reports/AP_StdRpt_CreditorJournalListPreview.aspx.vb" Inherits="AP_StdRpt_CreditorJournalList_Preview" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Account Payables - Creditor Journal Listing</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</HEAD>
	<body POSITIONING="GridLayout" onload="this.focus()" >
		<form id="Form1" method="post" runat="server">
			<table align=center id="tblCrystal" runat="server">
				<tr>
					<td>
                        <CR:CrystalReportViewer ID="crvView" runat="server" AutoDataBind="true" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:datagrid id="EventData" AutoGenerateColumns="true" width="100%" runat="server" />		
					</td>
				</tr>
			</table>
		</form>
		<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server/>			
	</body>
</HTML>
