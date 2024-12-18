<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPMTrx" src="menu_PMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPMTrx id=MenuPMTrx runat="server" />
	</body>
</html>
