<%@ Page %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDData" src="menu_PDData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Menu</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<UserControl:MenuPDData id=MenuPDData runat="server" />
	</body>
</html>
