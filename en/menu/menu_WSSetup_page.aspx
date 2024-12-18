<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWS id=menuWS runat="server" />
	</body>
</html>
