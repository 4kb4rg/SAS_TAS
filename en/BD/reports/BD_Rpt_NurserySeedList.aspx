<%@ Page Language="vb" Src="../../../include/BD_Rpt_NurserySeedList.aspx.vb" Inherits="BD_Rpt_NurserySeedList"%>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<html>
	<head>
		<title>NURSERY SEED LIST REPORT</title>
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id=frmReport method=post runat=server>
		<cr:CrystalReportViewer id="crvView" width="350px" height="50px" runat="server" valign=center displaygrouptree="false" displaytoolbar="true" pagetotreeratio="4" />
		</form>
		<asp:Label id=lblErrMesage visible=false text="Error while initiating component." runat=server/>
	</body>
</html>
