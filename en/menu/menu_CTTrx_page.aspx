<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTTrx" src="menu_CTtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCTTrx id=MenuCTTrx runat="server" />
	</body>
</html>
