<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPM" src="menu_pmmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPM id=MenuPM runat="server" />
	</body>
</html>
