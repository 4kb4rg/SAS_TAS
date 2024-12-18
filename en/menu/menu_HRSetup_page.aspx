<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuHR id=MenuHR runat="server" />
	</body>
</html>
