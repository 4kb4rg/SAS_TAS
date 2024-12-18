<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPMSetup" src="menu_PMSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPMSetup id=MenuPMSetup runat="server" />
	</body>
</html>
