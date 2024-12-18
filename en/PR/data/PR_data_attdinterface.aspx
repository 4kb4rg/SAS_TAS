<%@ Page Language="vb" src="../../../include/PR_data_attdinterface.aspx.vb" Inherits="PR_data_attdinterface" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRData" src="../../menu/menu_PRData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Attendance System Interface</title>
</head>

<body>

<form id=frmUpload enctype="multipart/form-data" runat=server>
	<table border="0" cellspacing="0" width="100%">
		<tr>
			<td width=100% colspan=3 align=center><UserControl:MenuPRData id=MenuPRData runat="server" /></td>
		</tr>
		<tr>
			<td class="mt-h" colspan="2">ATTENDANCE SYSTEM INTERFACE</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan="2">Attendance Interface Reference Data</td>
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
							<input type=file id=flUpload runat=server /> 
							<asp:Label id=lblErrNoFile text="Please enter file name." visible=false forecolor=red runat=server/> 
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
						<td colspan="2">Your Attendance Interface reference file has been successfully uploaded. Please kindly check on Payroll Attendance Checkroll Transaction Screen</td>
					</tr>
				</table>
			</td>
		</tr>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
	</table>
</form>
</body>

</html>
