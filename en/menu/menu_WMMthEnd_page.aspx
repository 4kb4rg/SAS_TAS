<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWM" src="menu_bimthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWM id=MenuWM runat="server" />
	</body>
</html>
