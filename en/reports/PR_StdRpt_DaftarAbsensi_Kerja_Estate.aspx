<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_DaftarAbsensi_Kerja_Estate.aspx.vb" Inherits="PR_StdRpt_DaftarAbsensi_Kerja_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl_Estate.ascx"%>
<HTML>
	<HEAD>
		<title>Absensi Kerja</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3">Absensi Kerja</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
			</table>
		
			<table cellspacing="0" cellpadding="0" border="0" width="100%" id="TABLEDET" class="font9Tahoma">
			<tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted;background-color: transparent;" colspan="7">
                                View Absensi Kerja karyawan
                              </td>
            </tr>
			 <tr>
					<td colspan="7" style="height:25px">&nbsp;</td>
			</tr>
			
			<tr >
							        <td width="10%" height="26" valign=bottom>Periode :<BR>
							        <asp:DropDownList id="ddlbulan" width="55%" runat=server>
										<asp:ListItem value="01">January</asp:ListItem>
										<asp:ListItem value="02">February</asp:ListItem>
										<asp:ListItem value="03">March</asp:ListItem>
										<asp:ListItem value="04">April</asp:ListItem>
										<asp:ListItem value="05">May</asp:ListItem>
										<asp:ListItem value="06">June</asp:ListItem>
										<asp:ListItem value="07">July</asp:ListItem>
										<asp:ListItem value="08">Augustus</asp:ListItem>
										<asp:ListItem value="09">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddltahun width="40%" runat="server" />
							        </td>
									<td width="10%" height="26" valign=bottom>Divisi   :<BR><asp:DropDownList id="ddlDiv" width=100% runat="server" /></td>							
									<td width="10%" height="26" valign=bottom>Mandor   :<BR><asp:DropDownList id="ddlMandor" width=100% runat="server" /></td>
									<td width="10%" height="26" valign=bottom>Karyawan :<BR><asp:DropDownList id="ddlEmp" width=100% runat="server" /></td>
							    	<td width="8%" height="26" valign=bottom>
								    </td>
							    </tr>
				<tr class="mb-t">
								    <td colspan=7  height="26" valign=bottom>
								    <asp:Button id=SearchBtn Text="View "  OnClick="ViewBtn_OnClick" runat="server"/>
								    <asp:Button id=SubmitBtn Text="Export Excel"  OnClick="ExportBtn_OnClick" runat="server"/>
								    </td>
				</tr>
				<tr>
					<td colspan=7 style="height: 5px">&nbsp;</td>
				</tr>
				
            <tr>
				<td colspan="7">
					 <div id="divgd" style="width:1050px;height:400px;overflow: auto;">
							  <asp:DataGrid ID="dgpay" runat="server" 
                               CellPadding="1" GridLines="None" width="100%" 
							   AutoGenerateColumns=true CssClass="font9Tahoma" >
                    <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                            </asp:DataGrid>
					</div>
						</td>
					</tr>								
			</table>
		        </div>
        </td>
        </tr>
        </table>	
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
