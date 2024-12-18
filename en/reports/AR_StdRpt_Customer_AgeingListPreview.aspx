<%@ Page Language="vb" EnableViewState="false" Src="../../include/reports/AR_StdRpt_Customer_AgeingListPreview.aspx.vb" Inherits="Ar_StdRpt_Customer_AgeingListPreview" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<HTML>
	<HEAD>
		<title>Account Payable - Mutasi Account Payable</title>
        <link href="/aspnet_client/system_web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
            rel="stylesheet" type="text/css" />
	</HEAD>
	<body bottommargin="0" leftmargin="0" rightmargin="0"  topmargin="0" MS_POSITIONING="GridLayout" onload="this.focus()" >
		<form id="Form1" method="post" runat="server">
			<table border="0" cellspacing="1" cellpadding="1" width="100%" id="tblCrystal" runat="server" >
				<tr>
					<td>
						<cr:CrystalReportViewer id="crvView" runat="server" DisplayGroupTree="False" HasCrystalLogo="False" 
						HasToggleGroupTreeButton="False" HasZoomFactorList="False" />
					</td>
				</tr>
			</table>
		</form>
		<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat="server"/>			
        <br />
        <br />
        <asp:Label ID="lbltest" runat="server" Text="Label"></asp:Label>
	</body>
</HTML>
