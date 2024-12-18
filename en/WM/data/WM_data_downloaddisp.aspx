<%@ Page Language="vb" src="../../../include/WM_data_downloaddisp.aspx.vb" Inherits="WM_data_downloaddisp" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMData" src="../../menu/menu_WMData.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Download Dispatch File</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%" align=center><UserControl:MenuWMData id=MenuWMData runat="server" /></td>
			</tr>
			<tr>
				<td width="100%">
					<form id=frmDownload runat=server>
					<table border="0" cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td class="mt-h" width="100%" colspan="5">DOWNLOAD DISPATCH FILE</td>
						</tr>
						<tr>
							<td colspan=5><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="5">Weighing Dispatch Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="5">&nbsp;</td>
						</tr>
						<tr>
							<td width="100%" colSpan="5">Steps:</td>
						</tr>
						<tr>
							<td width="100%" colSpan="5">1.&nbsp; 
								Leave the batch number blank if you wish to download dispatch data that have not been downloaded.
							</td>
						</tr>
						<tr>
							<td width="100%" colSpan="5">2.&nbsp; 
								To download specific range of data, enter the delivery date.
							</td>
						</tr>
						<tr>
							<td width="100%" colSpan="5">3.&nbsp; 
								Each downloaded data will be assigned Batch No. Enter Batch No to re-download the data.
							</td>
						</tr>
						<tr>
							<td width="100%" colSpan="5">4.&nbsp; 
								Click "Generate" button to download the file.
							</td>
						</tr>
						<tr>
							<td width="20%">&nbsp;</td>
							<td width="20%">&nbsp;</td>
							<td width="10%">&nbsp;</td>
							<td width="20%">&nbsp;</td>
							<td width="30%">&nbsp;</td>
						</tr>
						<tr>
							<td>Batch No: </td>
							<td><asp:textbox id=txtBatchNo with=100% maxlength=20 runat=server/></td>
							<td colspan=3>&nbsp;</td>
						</tr>
						<tr>
							<td>Delivery Date : </td>
							<td><asp:textbox id=txtFromDelDate with=100% maxlength=10 runat=server/></td>
							<td>&nbsp;</td>
							<td>To : </td>
							<td><asp:textbox id=txtToDelDate with=100% maxlength=10 runat=server/></td>
						</tr>
						<tr>
							<td colspan=5>
								<asp:Label id=lblErrBatchNo visible=false forecolor=red text="Please enter either Batch No or, Delivery Date." runat=server/> 
								<asp:Label id=lblErrDelivery visible=false forecolor=red text="Please enter both Delivery Date or, leave blank." runat=server/>
								<asp:Label id=lblErrDateFormat visible=false forecolor=red text="Delivery date format " runat=server/>
								&nbsp;
							</td>
						</tr>
						<tr>
							<td colspan="5"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="../../images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
