<%@ Page Language="vb" trace=false Src="../../include/reports/AP_StdRpt_InvRcvNoteFakturPajakListingPreview.aspx.vb" Inherits="AP_StdRpt_InvRcvNoteFakturPajakListingPreview" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Serah Terima Faktur Pajak Listing</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="this.focus()" >
		<form id="Form1" method="post" runat="server">
			<table align=center id="tblCrystal" runat="server">
				<tr>
					<td>
						<cr:CrystalReportViewer id="crvView" width="350px" height="50px" runat="server" valign=center displaygrouptree="false" displaytoolbar="true" pagetotreeratio="4" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:datagrid id="EventData" AutoGenerateColumns="true" width="100%" runat="server" />		
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
