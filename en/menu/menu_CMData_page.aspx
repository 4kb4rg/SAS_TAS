<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCMData" src="menu_CMData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCMData id=MenuCMData runat="server" />
	</body>
</html>
