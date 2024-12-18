<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINSetup" src="menu_INsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuINSetup id=MenuINSetup runat="server" />
	</body>
</html>
