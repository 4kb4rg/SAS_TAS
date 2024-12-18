<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="menu_cbmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCB id=MenuCB runat="server" />
	</body>
</html>
