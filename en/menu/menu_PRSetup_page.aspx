<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="menu_PRSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
	</body>
</html>
