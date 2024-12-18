<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUTrx" src="menu_NUtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuNUTrx id=MenuNUTrx runat="server" />
	</body>
</html>
