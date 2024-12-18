<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPUData" src="menu_PUData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPUData id=MenuPUData runat="server" />
	</body>
</html>
