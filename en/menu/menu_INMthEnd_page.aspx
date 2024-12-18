<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuIN" src="menu_inmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuIN id=MenuIN runat="server" />
	</body>
</html>
