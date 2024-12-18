<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRData" src="menu_PRData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPRData id=MenuPRData runat="server" />
	</body>
</html>
