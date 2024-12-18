<%@ Page Language="vb" src="../include/Data_Upload_Monthly.aspx.vb" Inherits="Data_Upload_Monthly" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>

<html>
	<head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <title>Upload Reference File</title>
	    <style type="text/css">



hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
}
        </style>
	</head>
	<body class="main-modul-bg-app-list-pu" style="font-size: 12pt; font-family: Times New Roman">
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma">
			<tr>
				<td width="100%">
					<form id="frmMain" runat=server>
					<table id="tblDownload" border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">
                                Upload Reference File</td>
						</tr>
						<tr>
							<td colspan=5>
                    <hr style="width :100%" />
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
        </div>
    </td>
    </tr>
    </table>
	</body>
</html>
