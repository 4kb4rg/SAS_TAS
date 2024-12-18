<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAdminData" src="menu_AdminData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuAdminData id=MenuAdminData runat="server" />
	</body>
</html>
