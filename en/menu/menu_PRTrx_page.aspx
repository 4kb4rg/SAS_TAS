<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="menu_PRTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
	</body>
</html>
