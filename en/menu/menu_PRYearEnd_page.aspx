<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRYearEnd" src="menu_PRYearEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPRYearEnd id=MenuPRYearEnd runat="server" />
	</body>
</html>
