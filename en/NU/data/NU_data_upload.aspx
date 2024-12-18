<%@ Page Language="vb" src="../../../include/NU_data_upload.aspx.vb" Inherits="NU_data_upload" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUData" src="../../menu/menu_NUData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Upload References File</title>
</head>

<body>

<form id=frmUpload enctype="multipart/form-data" runat=server>
	<table border="0" cellspacing="0" width="100%">
		<tr>
			<td width=100% colspan=3 align=center><UserControl:MenuNUData id=MenuNUData runat="server" /></td>
		</tr>
		<tr>
			<td class="mt-h" colspan="2">UPLOAD REFERENCES FILE</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan="2">Nursery Reference Data</td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblBefore cellpadding=2 cellspacing=0 border=0 width=100% runat=server>
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
							<input type=file id=flUpload runat=server /> <asp:Label id=lblErrUpload text="There was an error when uploading the file" visible=false runat=server />
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
						<td colspan="2">Your nursery reference file has been successfully uploaded.</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
</body>

</html>
