<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCT" src="menu_ctmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCT id=MenuCT runat="server" />
	</body>
</html>
