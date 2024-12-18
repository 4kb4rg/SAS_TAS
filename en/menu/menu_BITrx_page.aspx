<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBITrx" src="menu_BITrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuBITrx id=MenuBITrx runat="server" />
	</body>
</html>
