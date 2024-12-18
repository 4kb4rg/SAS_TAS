<%@ Page Language="vb" src="../../../include/GL_data_trx_upload.aspx.vb" Inherits="GL_data_trx_upload" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLData" src="../../menu/menu_GLData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Upload Transaction File</title>
</head>

<body>

<form id=frmUpload enctype="multipart/form-data" runat=server>
	<table border="0" cellspacing="0" width="100%">
		<tr>
			<td width=100% colspan=3 align=center><UserControl:MenuGLData id=MenuGLData runat="server" /></td>
		</tr>
		<tr>
			<td class="mt-h" colspan="2">UPLOAD TRANSACTION FILE</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan=2>
				<font color=red>Important notes before upload transaction file:</font><p>
				1. Please backup up the database before proceed.<br>
				2. Ensure no user is in the system.<br>
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
						<td colspan="2">1.&nbsp; Select transaction file type before upload the file.</td>
					</tr>
					<tr>
						<td colspan="2">2.&nbsp; Click &quot;Browse&quot; button to select your file location.</td>
					</tr>
					<tr>
						<td colspan="2">3.&nbsp; Click &quot;Upload&quot; button to save your file, data into database.</td>
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
						<td>Transaction Type: </td>
						<td>
							<asp:RadioButton id=rbIN groupname=trxtype text=" Inventory Data" runat=server/> 
							<asp:Label id=lblOKIN visible=false text=" (Inventory transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>
							<asp:RadioButton id=rbCT groupname=trxtype text=" Canteen Data" runat=server/> 
							<asp:Label id=lblOKCT visible=false text=" (Canteen transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td><asp:RadioButton id=rbWS groupname=trxtype text=" Workshop Data" runat=server/> 
							<asp:Label id=lblOKWS visible=false text=" (Workshop transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td><asp:RadioButton id=rbPU groupname=trxtype text=" Purchasing Data" runat=server/> 
							<asp:Label id=lblOKPU visible=false text=" (Purchasing transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td><asp:RadioButton id=rbAP groupname=trxtype text=" Account Payable Data" runat=server/> 
							<asp:Label id=lblOKAP visible=false text=" (Account Payable transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td><asp:RadioButton id=rbPR groupname=trxtype text=" Payroll Data" runat=server/> 
							<asp:Label id=lblOKPR visible=false text=" (Payroll transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td><asp:RadioButton id=rbBI groupname=trxtype text=" Billing Data" runat=server/> 
							<asp:Label id=lblOKBI visible=false text=" (Billing transaction file has been posted.)" runat=server/>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td><asp:RadioButton id=rbGL groupname=trxtype text=" General Ledger Data" runat=server/> 
							<asp:Label id=lblOKGL visible=false text=" (General Ledger transaction file has been posted.)" runat=server/> 
							<asp:Label id=lblErrTrxType forecolor=red visible=false text="<br>Please tick the transaction type to which you are uploading." runat=server/>
						</td>
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
	<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
	</table>
</form>
</body>

</html>
