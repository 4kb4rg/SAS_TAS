<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="menu_apmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuAP id=MenuAP runat="server" />
	</body>
</html>
