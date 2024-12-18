<%@ Page Language="vb" src="../include/Data_download.aspx.vb" Inherits="Data_download" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>


<html>
	<head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <title>Upload Reference File</title>
	</head>
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%">
					<form id="frmMain" runat=server>
					<table id="tblDownload" border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD TRANSACTION FILE</td>
						</tr>
						<tr>
							<td colspan=5><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="5">Transaction Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="5">&nbsp;</td>
						</tr>
						<TR>
							<TD width="100%" colSpan="5">Steps:</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="5">1.&nbsp; select data to export.</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="5">2.&nbsp; Click "Generate" button to generate the file.</TD>
						</TR>
						<tr>
							<td colspan="5">&nbsp;</td>
						</tr>
		
						<tr>
						    <td>&nbsp;Data:</td>
						    <td colspan="2">
                                &nbsp;<asp:DropDownList width="75%" id=ddlTable runat=server>
								        <asp:ListItem value="0" Selected>Month End Trial</asp:ListItem>
								         <asp:ListItem value="4" >Jurnal Header</asp:ListItem>
								         <asp:ListItem value="5" >Jurnal Detail</asp:ListItem>
								         <asp:ListItem value="6" >Cash Bank Header</asp:ListItem>
								         <asp:ListItem value="7" >Cash Bank Detail</asp:ListItem>
								         <asp:ListItem value="8" >Payment Header</asp:ListItem>
								         <asp:ListItem value="9" >Payment Detail</asp:ListItem>
								        <asp:ListItem value="1" >Akun Master</asp:ListItem>
								        <asp:ListItem value="2" >Supplier Master</asp:ListItem>
								        <asp:ListItem value="3" >Bill Party Master</asp:ListItem>
					                </asp:DropDownList></td>
					         <td>&nbsp;</td>
					         <td>&nbsp;</td>
						</tr>
						<tr>
							<td colspan="5">&nbsp;</td>
						</tr>
						
				        <tr>
				            <td style="width: 15%">&nbsp;Date</td>
					        <td colspan="4">
                                &nbsp;<asp:Textbox id="txtDate" width="100px" maxlength=10 runat=server/>
						        <a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="images/calendar.gif"/></a>&nbsp;
                                To:&nbsp; &nbsp;<asp:Textbox id="txtDateTo" width="100px" maxlength=10 runat=server/>
						        <a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="images/calendar.gif"/></a>
					        </td>		
				        </tr>	
				        <tr>
							<td colspan="5">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan="5"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					
					<asp:Label id="lblErrMesage" visible="false" Text="Error while initiating component." runat="server" />
					<asp:Label id="lblDownloadfile" visible="true" runat="server" />
					</form>
				</td>
			</tr>
		</table>
	</body>
</html>
