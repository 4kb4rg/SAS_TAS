<%@ Page Language="vb" Src="../../include/reports/WS_StdRpt_WorkshopIssueReturnSlipPreview.aspx.vb" Inherits="WS_StdRpt_WorkshopIssueReturnSlipPreview" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Inventory - Stock Adjustment Listing</title>
		<Preference:PrefHdl ID="PrefHdl" RunAt="server" />
	</head>
	<body MS_POSITIONING="GridLayout" OnLoad="this.focus()" >
		<form ID="frmMain" Method="post" RunAt="server">
			<table ID="tblCrystal" Align="center" RunAt="server">
				<tr>
					<td>
						<cr:CrystalReportViewer ID="crvView" Width="350px" Height="50px" RunAt="server" VAlign="center" DisplayGroupTree="false" DisplayToolbar="true" PageToTreeRatio="4" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid ID="dgReport" AutoGenerateColumns="true" Width="100%" RunAt="server" />
					</td>
				</tr>
			</table>
		</form>
		<asp:Label id="lblErrMessage" Visible="false" Text="Error while initiating component." RunAt=server/>
	</body>
</html>
