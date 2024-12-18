<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCBTrx" src="menu_CBTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCBTrx id=MenuCBTrx runat="server" />
	</body>
</html>
