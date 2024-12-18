<%@ Page Language="vb" Src="../../../include/IN_Rpt_StockTransferDet.aspx.vb" Inherits="IN_Rpt_StockTransferDet"%>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<html>
	<head>
		<title>Stock Transfer</title>
	</head>
	<body>
		<form id=frmReport method=post runat=server>
			<cr:CrystalReportViewer id="crvView" width="350px" height="50px" runat="server" valign=center displaygrouptree="false" displaytoolbar="true" pagetotreeratio="4" />
			<asp:DataGrid id=dgResult Visible=false runat=server/>
		</form>
		<asp:Label id=lblErrMesage visible=false text="Error while initiating component." runat=server/>
	</body>
</html>
