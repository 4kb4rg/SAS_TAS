<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuSYS id=MenuSYS runat="server" />
	</body>
</html>
