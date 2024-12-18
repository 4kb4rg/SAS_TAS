<%@ Page Language="vb" AutoEventWireup="false" src="../../include/reports/WS_StdRpt_Selection.aspx.vb" Inherits="WS_StdRpt_Selection" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WS_STDRPT_SELECTION_CTRL" src="../include/reports/WS_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Workshop - Standard Reports</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td colspan="3"><strong> WORKSHOP - STANDARD REPORTS</td>
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
					<td colspan="6"><UserControl:WS_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
