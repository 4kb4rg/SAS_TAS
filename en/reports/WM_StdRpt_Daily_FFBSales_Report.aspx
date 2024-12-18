<%@ Page Language="vb" trace=false src="~/include/reports/WM_StdRpt_Daily_FFBSales_Report.aspx.vb" Inherits="WM_StdRpt_Daily_FFBSales" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Weighing Management - Daily FFB Sales Report</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />		
			<asp:Label id="lblLocation" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> WEIGHING MANAGEMENT - DAILY FFB RECEIVED BY SUPPLIER REPORT</strong></td>
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
					<td colspan="6"><UserControl:WM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6" style="height: 21px">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">		
				<tr>
					<td width=15% style="height: 24px"> Report Type :</td>
					<td width=35% style="height: 24px">   
                        <asp:DropDownList ID="lstRptType" runat="server" AutoPostBack="true" 
                            Width="76%">
                            <asp:ListItem Selected="True" Value="0">Rekepitulasi Pengiriman TBS</asp:ListItem>
                     
                            <asp:ListItem Value="1">Detail Pengiriman</asp:ListItem>
                          
                        </asp:DropDownList></td>
					<td width=15% style="height: 24px">&nbsp;</td>										
					<td width=35% style="height: 24px">&nbsp;</td>										
				</tr>	

				<tr>
					<td>Supplier Code : </td>
					<td><asp:textbox id="txtSupplier" width="20%" maxlength=16 runat="server" />&nbsp;<asp:TextBox
                            ID="txtSupName" runat="server" AutoPostBack="False" MaxLength="15" Width="50%"></asp:TextBox>
                        <asp:Button
                            ID="FindSupplierButton" runat="server" CausesValidation="false" OnClientClick="javascript:PopSupplier_New('frmMain','','txtSupplier','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                            Text="..." Width="24px" />
                        (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>			

                <tr>
                    <td valign="top" width="15%">
                        Date :</td>
                    <td width="35%">
                        <asp:TextBox ID="srchDateIn" runat="server" MaxLength="10" Width="25%"></asp:TextBox>
                        <a href="javascript:PopCal('srchDateIn');">
                            <asp:Image ID="btnSelDateFrom" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/en/images/calendar.gif" /></a>
                        <asp:Label ID="lblTo" runat="server">To</asp:Label>
                        <a href="javascript:PopCal('srchDateTo');">
                            <asp:TextBox ID="srchDateTo" runat="server" MaxLength="10" Width="25%"></asp:TextBox></a>
                        <asp:Image ID="btnSelDateTo" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/en/images/calendar.gif" /><br />
                        <asp:Label ID="lblErrDateInMsg" runat="server" ForeColor="red" Text="<br>Date Format should be in "
                            Visible="false"></asp:Label><asp:Label ID="lblErrDateIn" runat="server" ForeColor="red"
                                Visible="false"></asp:Label></td>
                    <td width="15%">
                    </td>
                    <td width="35%">
                    </td>
                </tr>
				<tr>
					<td width=15%>&nbsp;</td>
					<td width=35%><asp:DropDownList id="lstSuppType" width="40%" runat="server" Visible="False" /></td>
					<td width=15%>&nbsp;</td>										
					<td width=35%>&nbsp;</td>										
				</tr>	
                <tr>
                    <td width="15%">
                        <asp:CheckBox ID="cbExcel" runat="server" AutoPostBack="True" Checked="false" Text=" Export To Excel" /></td>
                    <td width="35%">
                    </td>
                    <td width="15%">
                    </td>
                    <td width="35%">
                    </td>
                </tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<br />
                        &nbsp; &nbsp;&nbsp;
                    </td>
				</tr>				
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtPPNInit" runat="server" BackColor="Transparent" ForeColor="Transparent" BorderStyle="None"
                            Text="" Width="4%"></asp:TextBox><asp:TextBox ID="txtPPN" ForeColor="Transparent" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" Width="3%"></asp:TextBox><asp:TextBox ID="TextBox1" ForeColor="Transparent" runat="server" Width="4%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server" Width="3%" ForeColor="Transparent" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"></asp:TextBox></td>
                </tr>
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
