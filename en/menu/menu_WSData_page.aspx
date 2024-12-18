<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSData" src="menu_WSData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWSData id=MenuWSData runat="server" />
	</body>
</html>
