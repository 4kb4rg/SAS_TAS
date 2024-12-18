<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBIData" src="menu_BIData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuBIData id=MenuBIData runat="server" />
	</body>
</html>
