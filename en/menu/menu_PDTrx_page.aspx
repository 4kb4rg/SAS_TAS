<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="menu_PDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPDTrx id=MenuPDTrx runat="server" />
	</body>
</html>
