<%@ Page Language="vb" src="../include/Data_UploadHR_Absensi.aspx.vb" Inherits="Data_UploadHR_Absensi" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Upload Reference File</title>
</head>

<body>

<form id=frmUpload enctype="multipart/form-data" runat=server>
	<table border="0" cellspacing="0" width="100%">
		<tr>
			<td class="mt-h" colspan="2">UPLOAD &nbsp;FILE ABSENSI</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan="2">Upload File *.mdb</td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblBefore border=0 cellpadding=2 cellspacing=0 width=100% runat=server>
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
						<td></td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Filename :</td>
					</tr>
					<tr>
						<td colspan="2">
							<input type=file id=flUpload runat=server /> 
							<asp:Label id=lblErrNoFile text="Please enter file name." visible=false forecolor=red runat=server/> 
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:ImageButton id=UploadBtn imageurl="images/butt_upload.gif" alternatetext=" Upload " onclick=UploadBtn_Click runat=server />
						</td>
					</tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
					<tr>
						<td colspan="2">
							<asp:Label id=lblSuccess text="" forecolor=blue visible=false runat=server />
							<asp:Label id=lblError text="" forecolor=red runat=server />
							
							<asp:Label id=lblErrSupplier text="Unknown supplier code found in the tickets : " forecolor=red visible=false runat=server /><br>
							<asp:Label id=lblErrBuyer text="Unknown buyer code found in the tickets : " forecolor=red visible=false runat=server /><br>
							<asp:Label id=lblErrTransporter text="Unknown transporter code in the tickets : " forecolor=red visible=false runat=server />
							<asp:Label id=lblSuccessRec text="Successfully uploaded " visible=false runat=server />
							<asp:Label id=lblSuccessPath text=" records for the file in " visible=false runat=server />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
</body>
</html>