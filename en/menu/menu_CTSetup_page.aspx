<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTSetup" src="menu_CTsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCTSetup id=MenuCTSetup runat="server" />
	</body>
</html>
