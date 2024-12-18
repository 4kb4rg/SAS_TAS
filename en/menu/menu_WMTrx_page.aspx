<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="menu_WMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWMTrx id=MenuWMTrx runat="server" />
	</body>
</html>
