<%@ Page Language="vb" src="../../include/reports/PD_StdRpt_ProdDet_Estate.aspx.vb" Inherits="PD_StdRpt_ProdDet_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Laporan Detail Pengiriman TBS</title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma"><strong>PRODUCTION - DETAIL PENGIRIMAN TBS REPORT</strong> </td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=2><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=2><UserControl:PD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>							
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>		
				<tr>
					<td colspan=2><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
			   <tr>
					<td>Tanggal Panen : </td>
					<td><asp:TextBox id="txtDateFrom" width="50%" maxlength="10" runat="server"/>
  					    <a href="javascript:PopCal('txtDateFrom');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
						<asp:Label id="lblDateFrom" visible="false" forecolor=red runat="server" />
					</td>						
					<td>s/d : </td>
					<td><asp:TextBox id="txtDateTo" width="50%" maxlength="10"  runat="server"/>
  					    <a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a> 
						<asp:Label id="lblDateTo" visible="false" forecolor=red runat="server" /></td>
				</tr>
				<tr>
					<td width=25%>Divisi : </td>
					<td colspan=3>
						<asp:DropDownList id="ddlBlkGrp" width="45%" runat="server" />						
					</td>			
				</tr>
                <tr>
					<td>Group By :</td>
					<td><asp:DropDownList width=50% id=ddlGroupBy runat=server>
                        <asp:ListItem value="1">Divisi</asp:ListItem>
                        <asp:ListItem value="2">Tahun Tanam</asp:ListItem>
					    <asp:ListItem value="3">Blok</asp:ListItem>
					    <asp:ListItem value="4">Transport</asp:ListItem>
					</asp:DropDownList></td>
				</tr>
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>					
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;
                    </td>					
				</tr>				
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
