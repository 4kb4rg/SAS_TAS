<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAPTrx" src="menu_APtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuAPTrx id=MenuAPTrx runat="server" />
	</body>
</html>
