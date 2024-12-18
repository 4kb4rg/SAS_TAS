<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUData" src="menu_NUData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuNUData id=MenuNUData runat="server" />
	</body>
</html>
