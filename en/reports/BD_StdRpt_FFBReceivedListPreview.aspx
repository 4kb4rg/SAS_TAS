<%@ Page Language="vb" Src="../../include/reports/BD_StdRpt_FFBReceivedListPreview.aspx.vb" Inherits="BD_StdRpt_FFBReceivedListPreview" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Budgeting - FFB Received Budget Transaction Listing</title>
	</HEAD>
	<body onload="this.focus()" >
		<form id="frmMain" method="post" runat="server">
			<table align=center runat="server">
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
		<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server/>			
	</body>
</HTML>
