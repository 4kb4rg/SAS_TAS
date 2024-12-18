<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPUTrx" src="menu_PUtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPUTrx id=MenuPUTrx runat="server" />
	</body>
</html>
