<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTData" src="menu_CTData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuCTData id=MenuCTData runat="server" />
	</body>
</html>
