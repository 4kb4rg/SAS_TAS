<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCMTrx" src="menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCMTrx id=MenuCMTrx runat="server" />
	</body>
</html>
