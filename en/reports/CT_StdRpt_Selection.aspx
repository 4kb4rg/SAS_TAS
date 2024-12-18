<%@ Page Language="vb" AutoEventWireup="false" src="../../include/reports/CT_StdRpt_Selection.aspx.vb" Inherits="CT_StdRpt_Selection" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CT_STDRPT_SELECTION_CTRL" src="../include/reports/CT_StdRpt_Selection_Ctrl.ascx"%>

<HTML>
	<HEAD>
		<title>Canteen - Standard Reports</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>

	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
           		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>

			<Preference:PrefHdl id="PrefHdl" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3">CANTEEN - STANDARD REPORTS</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CT_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" /> 
    			</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</HTML>
