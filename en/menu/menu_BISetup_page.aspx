<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="menu_bisetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuBI id=MenuBI runat="server" />
	</body>
</html>
