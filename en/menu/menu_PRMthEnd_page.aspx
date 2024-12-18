<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" />
	</body>
</html>
