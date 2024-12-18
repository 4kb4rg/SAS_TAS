<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="menu_wsmthend.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWS id=MenuWS runat="server" />
	</body>
</html>
