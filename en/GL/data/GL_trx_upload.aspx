<%@ Page Language="vb" src="../../../include/GL_trx_upload.aspx.vb" Inherits="GL_trx_upload" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLData" src="../../menu/menu_GLData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Upload GL Transaction</title>
</head>

<body>

<form id=frmUpload enctype="multipart/form-data" runat=server>
	<table border="0" cellspacing="0" width="100%">
		<tr>
			<td width=100% colspan=3 align=center><UserControl:MenuGLData id=MenuGLData runat="server" /></td>
		</tr>
		<tr>
			<td class="mt-h" colspan="2">UPLOAD GL TRANSACTION</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan=2>
				<font color=red>Important notes before upload transaction file:</font><p>
				1. Please backup up the database before proceed.<br>
				2. Ensure no user is in the system.<br>
				3. Ensure GL has not closed the month end process.<br>
				4. Each transaction is allowed to upload ONE time.<br>
				5. Upload file is carrying with block, sub-block, vehicle, vehicle expense, vehicle usage and oil palm yield data.
			</td>
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
						<td colspan="2">2.&nbsp; Click &quot;Upload&quot; button to save your GL transaction into database.</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Note : The process may take a couple of minutes.</td>
					</tr>
					<tr>
						<td width=15%>&nbsp;</td>
						<td width=85%>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Filename :</td>
					</tr>
					<tr>
						<td colspan="2">
							<input type=file id=flUpload runat=server /><br>
							<asp:Label id=lblErrNoFile text="Please enter file name." visible=false forecolor=red runat=server/> 
							<asp:Label id=lblErrUpload text="There was an error when uploading the file. Please contact administrator for assistance." forecolor=red visible=false runat=server />
							<asp:Label id=lblErrNoXmlRecord text="There was no data in your GL transaction file." forecolor=red visible=false runat=server />
							<asp:Label id=lblErrHasDBRecord text="You are not allowed to upload the same set of GL transaction twice or, you have run the month end closed before GL transaction is uploaded." forecolor=red visible=false runat=server />
							<asp:Label id=lblErrLoc text="You are attempted to upload the data not belongs to this location." forecolor=red visible=false runat=server />
							<asp:Label id=lblSuccess text="File is successfully uploaded." forecolor=blue visible=false runat=server />
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
	</table>
</form>
</body>

</html>
