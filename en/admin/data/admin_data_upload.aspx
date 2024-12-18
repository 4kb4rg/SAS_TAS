<%@ Page Language="vb" src="../../../include/Admin_data_upload.aspx.vb" Inherits="Admin_data_upload" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAdminData" src="../../menu/menu_AdminData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<title>Upload References File</title>
</head>

<body>

<form id=frmUpload enctype="multipart/form-data" class="main-modul-bg-app-list-pu" runat=server>
<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 2000px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

	<table border="0" cellspacing="0" width="100%" class="font9Tahoma" >
		<tr>
			<td width=100% colspan=3 align=center><UserControl:MenuAdminData id=MenuAdminData runat="server" /></td>
		</tr>
		<tr>
			<td class="font9Tahoma" colspan="2"><strong> UPLOAD REFERENCES FILE</strong></td>
		</tr>
		<tr>
			<td colspan=2><hr style="width:100%">
                    </td>
		</tr>
		<tr>
			<td colspan="2">Administration Reference Data</td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblBefore border=0 cellpadding=2 cellspacing=0 width=100% class="font9Tahoma"  runat=server>
					<tr>
						<td colspan="2">Steps</td>
					</tr>
					<tr>
						<td colspan="2">1.&nbsp; Click &quot;Browse&quot; button to select your file location.</td>
					</tr>
					<tr>
						<td colspan="2">2.&nbsp; Click &quot;Upload&quot; button to save your file, data into database.</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Note : The process may take a couple of minutes.</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Filename :</td>
					</tr>
					<tr>
						<td colspan="2">
							<input type=file id=flUpload runat=server />
							<asp:Label id=lblErrNoFile text="No file selected" forecolor=red visible=false runat=server /> 
							<asp:Label id=lblErrUpload text="There was an error when uploading the file" visible=false runat=server />
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:ImageButton id=UploadBtn imageurl="../../images/butt_upload.gif" alternatetext=" Upload " onclick=UploadBtn_Click runat=server />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblAfter border=0 width=100% runat=server>
					<tr>
						<td colspan="2">Your administration reference file has been successfully uploaded.</td>
					</tr>
				</table>
			</td>
		</tr>
        <tr>
			<td colspan=2 height="25px;">&nbsp;</td>
		</tr>
		<tr>
			<td colspan=2><hr style="width:100%">
                    </td>
		</tr>
		<tr>
			<td colspan="2">Global Reference Data</td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblBeforeGlobal border=0 cellpadding=2 cellspacing=0 width=100% class="font9Tahoma"  runat=server>
					<tr>
						<td colspan="2">Steps</td>
					</tr>
					<tr>
						<td colspan="2">1.&nbsp; Click &quot;Browse&quot; button to select your file location.</td>
					</tr>
					<tr>
						<td colspan="2">2.&nbsp; Click &quot;Upload&quot; button to save your file, data into database.</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Note : The process may take a couple of minutes.</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Filename :</td>
					</tr>
					<tr>
						<td colspan="2">
							<input type=file id=flUploadGlobal runat=server /> 
							<asp:Label id=lblErrNoFileGlobal text="No file selected" forecolor=red visible=false runat=server /> 
							<asp:Label id=lblErrUploadGlobal text="There was an error when uploading the file" forecolor=red visible=false runat=server />
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:ImageButton id=UploadGlobalBtn imageurl="../../images/butt_upload.gif" alternatetext=" Upload " onclick=UploadGlobalBtn_Click runat=server />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblAfterGlobal border=0 width=100% runat=server>
					<tr>
						<td colspan="2">Your global reference file has been successfully uploaded.</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
                          </div>
            </td>
            </tr>
            </table>
</form>
</body>

</html>
