<%@ Page Language="vb" trace=false src="../../include/reports/WM_StdRpt_CPO_Sales_Daily_Report.aspx.vb" Inherits="WM_StdRpt_CPO_Sales_Daily_Report" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Weighing Management - FFB Received Summary by Supplier</title>
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
					<td class="font9Tahoma" colspan="3"><strong> WEIGHING MANAGEMENT - FFB RECEIVED SUMMARY BY SUPPLIER</strong></td>
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
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">		
                <tr>
                    <td width="15%">
                        Report Type :</td>
                    <td width="35%">
                        <asp:DropDownList id="lstRptType" width="75%" runat="server" /></td>
                    <td width="15%">
                    </td>
                    <td width="35%">
                    </td>
                </tr>
                <tr>
                    <td width="15%" style="height: 24px">
                        Customer Code :
                    </td>
                    <td width="35%" style="height: 24px">
                        <asp:DropDownList id="lstBillParty" width="75%" runat="server" />
                        (Blank For All)</td>
                    <td width="15%" style="height: 24px">
                    </td>
                    <td width="35%" style="height: 24px">
                    </td>
                </tr>
				<tr>
					<td width=15% style="height: 24px">
                        Product &nbsp;: </td>
					<td width=35% style="height: 24px"><asp:DropDownList id="lstProdCode" width="75%" runat="server" />
                        (Blank For All)</td>
					<td width=15% style="height: 24px">&nbsp;</td>										
					<td width=35% style="height: 24px">&nbsp;</td>										
				</tr>	
				<tr>
					<td width=15% style="height: 21px">
                        Date From :</td>
					<td width=35% style="height: 21px">
                        <asp:Textbox id="txtDate" width=25% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="../images/calendar.gif"/></a>
                        s.d Date To :
                        						<asp:Textbox id="txtDateTo" width=25% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="../images/calendar.gif"/></a></td>
					<td width=15% style="height: 21px">&nbsp;</td>										
					<td width=35% style="height: 21px">&nbsp;</td>										
				</tr>	
                <tr>
                    <td style="height: 21px" width="15%">
                        Contract No :</td>
                    <td style="height: 21px" width="35%">
                        <asp:Textbox id="txtContractNo" width="75%" maxlength=64 runat=server/>
                        (Blank For All)</td>
                    <td style="height: 21px" width="15%">
                    </td>
                    <td style="height: 21px" width="35%">
                    </td>
                </tr>
                <tr>
                    <td style="height: 21px" width="15%">
                        Transporter :</td>
                    <td style="height: 21px" width="35%">
                        <asp:DropDownList id="lstTransport" width="75%" runat="server" />&nbsp; (Blank For
                        All)</td>
                    <td style="height: 21px" width="15%">
                    </td>
                    <td style="height: 21px" width="35%">
                    </td>
                </tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>
				</tr>				
                <tr>
                    <td colspan="4" style="height: 21px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 21px">
                        <asp:TextBox ID="txtPPNInit" runat="server" BackColor="Transparent" BorderStyle="None"
                            Text="" Width="4%"></asp:TextBox><asp:TextBox ID="txtPPN" runat="server" BackColor="Transparent"
                                BorderColor="Transparent" BorderStyle="None" Width="3%"></asp:TextBox><asp:TextBox
                                    ID="TextBox1" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" Width="4%"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server"
                                        BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" Width="3%"></asp:TextBox></td>
                </tr>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
