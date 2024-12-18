<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCM" src="menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCM id=MenuCM runat="server" />
	</body>
</html>
