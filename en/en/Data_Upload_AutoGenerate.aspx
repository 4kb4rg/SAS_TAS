<%@ Page Language="vb" src="../include/Data_Upload_AutoGenerate.aspx.vb" Inherits="Data_Upload_AutoGenerate" %>
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
			<td class="mt-h" colspan="2">UPLOAD REFERENCE FILE</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan="2">Upload Reference File</td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblBefore border=0 cellpadding=2 cellspacing=0 width=100% runat=server>
				    <tr>
				        <td width=40% height=25>Period To Be Uploaded :</td> 
				        <td width=30%>
					        <asp:DropDownList id="lstAccMonth" width=20% runat=server>
				                        <asp:ListItem value="1">1</asp:ListItem>
				                        <asp:ListItem value="2">2</asp:ListItem>										
				                        <asp:ListItem value="3">3</asp:ListItem>
				                        <asp:ListItem value="4">4</asp:ListItem>
				                        <asp:ListItem value="5">5</asp:ListItem>
				                        <asp:ListItem value="6">6</asp:ListItem>
				                        <asp:ListItem value="7">7</asp:ListItem>
				                        <asp:ListItem value="8">8</asp:ListItem>
				                        <asp:ListItem value="9">9</asp:ListItem>
				                        <asp:ListItem value="10">10</asp:ListItem>
				                        <asp:ListItem value="11">11</asp:ListItem>
				                        <asp:ListItem value="12">12</asp:ListItem>
			                        </asp:DropDownList>
					        <asp:DropDownList id=lstAccYear width=20% runat=server />
				        </td>
				        <td width=5%>&nbsp;</td>
				        <td width=15%>&nbsp;</td>
				        <td width=10%>&nbsp;</td>
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
						<td colspan="2">
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