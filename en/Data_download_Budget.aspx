<%@ Page Language="vb" src="../include/Data_download_Budget.aspx.vb" Inherits="Data_download_Budget" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>

<html>
	<head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <title>Upload Reference File</title>
          <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">




hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
}
        

hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
}
        </style>
	</head>
	<body class="main-modul-bg-app-list-pu" >
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%">
					<form id="frmMain" runat=server>
					<table id="tblDownload" border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma" runat=server>
						<tr>
							<td  width="100%" colspan="4"><strong>DOWNLOAD BUDGET TEMPLATE</strong> </td>
						</tr>
						<tr>
							<td colspan=5>
                    <hr style="width :100%" />
                                <br />
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 445px; height: 29px; text-align: center;">
                                            <span style="font-size: 10pt; font-family: Tahoma"><strong>
                                            Description</strong></span></td>
                                        <td style="width: 95px; height: 29px; text-align: center;">
                                            <span style="font-size: 10pt; font-family: Tahoma"><strong>
                                            File</strong></span></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 445px">
                                            <span style="font-size: 10pt; font-family: Tahoma">Template Saldo Awal C.O.A</span></td>
                                        <td style="width: 95px; text-align: center" title="Download">
                                            <a href="File_Download/Temp_SALDOAWALCOA.xls">Download</a></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 445px;">
                                            <span style="font-size: 10pt; font-family: Tahoma">Template Budget Item Pabrik</span></td>
                                        <td style="width: 95px; text-align: center;" title="Download">
                                           <a href="File_Download/Temp_BudgetItem_Mill.xls">Download</a> </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 445px;">
                                            <span style="font-size: 10pt; font-family: Tahoma">Template Budget Item Estate</span></td>
                                        <td style="width: 95px; text-align: center;">
                                        <a href="File_Download/Temp_BudgetItem_Estate.xls">Download</a> 
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 445px">
                                            <span style="font-size: 10pt; font-family: Tahoma">Template Budget Produksi Estate</span></td>
                                        <td style="width: 95px; text-align: center">
                                             <a href="File_Download/TempBudget_Estate_Produksi.xls">Download</a> </td>
                                    </tr>
                                </table>
                            </td>
						</tr>
					</table>
					
					<asp:Label id="lblErrMesage" visible="false" Text="Error while initiating component." class="font9Tahoma" runat="server" />
					<asp:Label id="lblDownloadfile" visible="true" class="font9Tahoma" runat="server" />
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
