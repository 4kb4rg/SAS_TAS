<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRData" src="menu_HRData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuHRData id=MenuHRData runat="server" />
	</body>
</html>
