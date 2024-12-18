<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMData" src="menu_WMData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWMData id=MenuWMData runat="server" />
	</body>
</html>
