<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINData" src="menu_INData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuINData id=MenuINData runat="server" />
	</body>
</html>
