<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuINTrx id=MenuINTrx runat="server" />
	</body>
</html>
