<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuRCData" src="menu_RCData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuRCData id=MenuRCData runat="server" />
	</body>
</html>
