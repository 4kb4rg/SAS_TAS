<%@ Page Language="vb" AutoEventWireup="false" src="../../include/reports/CB_StdRpt_Selection.aspx.vb" Inherits="CB_StdRpt_Selection" %>
<%@ Register TagPrefix="uc1" TagName="CB_stdrpt_selection_ctrl" Src="../include/reports/CB_stdrpt_selection_ctrl.ascx" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx" %>
<HTML>
	<HEAD>
		<title>Cash And Bank - Standard Reports</title>
        <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
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
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>CASH AND BANK - STANDARD REPORTS</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;<uc1:CB_stdrpt_selection_ctrl ID="CB_stdrpt_selection_ctrl1" runat="server" /> </td>
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
