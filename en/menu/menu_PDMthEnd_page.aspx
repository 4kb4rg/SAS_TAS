<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDMthEnd" src="menu_PDMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPDMthEnd id=MenuPDMthEnd runat="server" />
	</body>
</html>
