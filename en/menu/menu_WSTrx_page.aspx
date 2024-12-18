<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSTrx" src="menu_WStrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuWSTrx id=MenuWSTrx runat="server" />
	</body>
</html>
