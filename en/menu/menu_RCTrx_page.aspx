<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuRCTrx" src="menu_RCTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuRCTrx id=MenuRCTrx runat="server" />
	</body>
</html>
