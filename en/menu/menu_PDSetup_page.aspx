<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="menu_PDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPDSetup id=MenuPDSetup runat="server" />
	</body>
</html>
