<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuRCSend" src="menu_RCSend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuRCSend id=MenuRCSend runat="server" />
	</body>
</html>
