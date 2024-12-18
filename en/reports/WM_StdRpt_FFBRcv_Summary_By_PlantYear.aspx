<%@ Page Language="vb" trace=false src="../../include/reports/WM_StdRpt_FFBRcv_Summary_By_PlantYear.aspx.vb" Inherits="WM_StdRpt_FFBRcv_SumByPlantYr" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Weighing Management - FFB Received Summary by Planting Year</title>
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
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> WEIGHING MANAGEMENT - FFB RECEIVED SUMMARY BY PLANTING YEAR</strong></td>
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
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>			
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">		
				<tr>
					<td>Supplier Code : </td>
					<td><asp:textbox id="txtSupplier" width="50%" maxlength=8 runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>			
				<tr>
					<td width=15%>Supplier Type : </td>
					<td width=35%><asp:DropDownList id="lstSuppType" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>										
					<td width=35%>&nbsp;</td>										
				</tr>	
				<tr>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
					<td width=15%>&nbsp;</td>										
					<td width=35%>&nbsp;</td>										
				</tr>	
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>
				</tr>				
			</table>

        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
