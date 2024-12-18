<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGL" src="menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuGL id=MenuGL runat="server" />
	</body>
</html>
