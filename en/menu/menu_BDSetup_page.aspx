<%@ Page trace="False" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBD" src="menu_BDsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuBD id=MenuGL runat="server" />
	</body>
</html>
