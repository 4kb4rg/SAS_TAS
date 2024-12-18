<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuRCRead" src="menu_RCRead.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuRCRead id=MenuRCRead runat="server" />
	</body>
</html>
