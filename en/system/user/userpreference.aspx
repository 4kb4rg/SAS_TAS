<%@ Page Language="vb" src="../../../include/system_user_preference.aspx.vb" Inherits="system_user_preference" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuUserPref" src="../../menu/menu_user_preference.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Colour Preference</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPreference runat="server">
			<table border=0 cellspacing=0 cellpadding=3 width="100%">
				<tr>
					<td colspan=4">
						<UserControl:MenuUserPref id=MenuUserPref runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan=4>COLOUR PREFERENCE</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
				<tr class="mr-h">
					<td colspan=4>Select your colour theme.</td>
				</tr>
				<tr>
					<td width=20%><asp:RadioButton id=rb01 groupname=Colourscheme autopostback=true text=" Sea Blue:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#0052a4><font color=white> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
					<td width=20%><asp:RadioButton id=rb02 groupname=Colourscheme autopostback=true text=" Sky Blue:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#A6C2FF><font color=black> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width=20%><asp:RadioButton id=rb03 groupname=Colourscheme autopostback=true text=" Rainforest Green:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#018712><font color=white> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
					<td width=20%><asp:RadioButton id=rb04 groupname=Colourscheme autopostback=true text=" Hawaiian Light:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#ffb722><font color=black> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width=20%><asp:RadioButton id=rb05 groupname=Colourscheme autopostback=true text=" Violet Purple:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#9300d9><font color=white> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
					<td width=20%><asp:RadioButton id=rb06 groupname=Colourscheme autopostback=true text=" Chilli Red:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#ff1717><font color=white> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width=20%><asp:RadioButton id=rb07 groupname=Colourscheme autopostback=true text=" Corn Yellow:" runat=server/></td>
					<td width=30% align=left>
						<table height=30 width=80 cellspacing=2 cellpadding=2>
							<tr>
								<td align=center bgColor=#ffff00><font color=black> Text Colour </Colour></td>
							</tr>
						</table>
					</td>
					<td width=20%></td>
					<td width=30% align=left>
					</td>
				</tr>
			</table>
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		</FORM>
	</body>
</html>
