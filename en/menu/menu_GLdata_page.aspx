<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLData" src="menu_GLData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuGLData id=MenuGLData runat="server" />
	</body>
</html>
