<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNU" src="menu_numthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuNU id=MenuNU runat="server" />
	</body>
</html>
