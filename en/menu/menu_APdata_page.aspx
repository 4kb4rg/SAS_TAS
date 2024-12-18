<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAPData" src="menu_APData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuAPData id=MenuAPData runat="server" />
	</body>
</html>
