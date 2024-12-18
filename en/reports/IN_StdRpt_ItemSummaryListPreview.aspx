<%@ Page Language="vb"Src="../../include/reports/IN_StdRpt_ItemSummaryListPreview.aspx.vb" Inherits="IN_StdRpt_ItemSummaryList_Preview" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory Item Summary Listing Report</title>
	</HEAD>
	<body onload="this.focus()" >
		<form id="Form1" method="post" runat="server">
			<table align=center id="tblCrystal" runat="server">
				<tr>
					<td>
						<cr:CrystalReportViewer id="crvView" width="350px" height="50px" runat="server" valign=center displaygrouptree="false" displaytoolbar="true" pagetotreeratio="4" />
					</td>
				</tr>
			</table>
		</form>
		<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server/>			
	</body>
</HTML>
