<%@ Page Language="vb" trace=false Src="../../include/reports/AP_StdRpt_PaymentListingPreview.aspx.vb" Inherits="AP_StdRpt_PaymentListing_Preview" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Accounts Payable - Payment Listing</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        
        
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="this.focus()" >
		<form id="Form1" method="post" runat="server">
			<table align=center id="tblCrystal" runat="server">
				<tr>
					<td>
                        <CR:CrystalReportViewer id="crvView" runat="server" AutoDataBind="true">
                        </CR:CrystalReportViewer>
					</td>
				</tr>
				<tr>
					<td style="height: 154px">
						<asp:datagrid id="EventData" AutoGenerateColumns="true" width="100%" runat="server" />&nbsp;
					</td>
				</tr>
			</table>
		</form>
		<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server/>			
	</body>
</HTML>
