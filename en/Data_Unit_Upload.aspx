<%@ Page Language="vb" src="../include/Data_unit_download.aspx.vb" Inherits="Data_Unit_Download" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Download Data Unit</title>
	    <style type="text/css">

.font9 {
	font-size: 9pt;
}
a {
	text-decoration:none;
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
	<Preference:PrefHdl id=PrefHdl runat="server" />
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<body class="main-modul-bg-app-list-pu">
		<form id=frmMain runat=server>
        <div class="kontenlist">
		<table id="tblDownload" border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma" >
			
			<tr>
				<td class="mt-h" colspan=5>
                  <strong>  UPLOAD DATA UNIT</strong></td>
			</tr>
			<tr>
				<td colspan=5>
                         <hr style="width :100%" />
					</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red></font><p>
                        Process will Upload data unit on
                        selected periode:</td>
			</tr>
			<tr>
				<td colspan=5 style="height: 25px">&nbsp;</td>
			</tr>
			<tr>
				<td width=40% height=25>Accounting Period To Be Upoaded :</td> 
				<td width=30%>
					<asp:DropDownList id=ddlAccMonth runat=server/> / 
					<asp:DropDownList id=ddlAccYear OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod AutoPostBack=True runat=server />
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
			</tr>
			
			<tr>
				<td colspan=5 height=25>
                    <asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when Upload data." runat=server/>
				</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:Button id=btnProceed Text="Upload" AlternateText="Upload data unit" OnClick="btnProceed_Click" class="button-small" runat="server" />
				&nbsp;</td>
			</tr>
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			
		</table>
        </div>
		</form>
	</body>
</html>
