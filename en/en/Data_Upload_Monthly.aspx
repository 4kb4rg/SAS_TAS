<%@ Page Language="vb" src="../include/Data_Upload_Monthly.aspx.vb" Inherits="Data_Upload_Monthly" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>

<html>
	<head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <title>Upload Reference File</title>
	</head>
	<body style="font-size: 12pt; font-family: Times New Roman">
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%">
					<form id="frmMain" runat=server>
					<table id="tblDownload" border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">
                                Upload Reference File</td>
						</tr>
						<tr>
							<td colspan=5><hr size="1" noshade>
                                <br />
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 445px; height: 20px;">
                                        <a href="http://10.120.1.3:1000" target="_blank">    <span>Upload .rar File Monthly Closing</span> </a> </td>
                                    </tr>
                                </table>
                            </td>
						</tr>
					</table>
					
					<asp:Label id="lblErrMesage" visible="false" Text="Error while initiating component." runat="server" />
					<asp:Label id="lblDownloadfile" visible="true" runat="server" />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
