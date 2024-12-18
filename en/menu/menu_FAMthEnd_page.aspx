<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFA" src="menu_FAMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuFA id=MenuFA runat="server" />
	</body>
</html>
