<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAdmin" src="menu_admin.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuAdmin id=MenuAdmin runat="server" />
	</body>
</html>
