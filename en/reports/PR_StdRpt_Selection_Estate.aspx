<%@ Page Language="vb" AutoEventWireup="false" src="../../include/reports/PR_StdRpt_Selection_Estate.aspx.vb" Inherits="PR_StdRpt_Selection" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl_Estate.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll Estate Standard Reports</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />

        <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
           		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<input type=Hidden id=hidUserLocPX runat="server" name="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" name="hidAccMonthPX"/>			
			<input type=Hidden id=hidAccYearPX runat="server" name="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>PAYROLL-ESTATE STANDARD REPORTS</strong> </td>
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
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" /> 
                </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
