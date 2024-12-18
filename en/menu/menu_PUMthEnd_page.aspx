<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="menu_pumthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPU id=MenuPU runat="server" />
	</body>
</html>
