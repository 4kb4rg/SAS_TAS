<%@ Page Language="vb" src="../../include/reports/PM_StdRpt_StorageTypeMasterList.aspx.vb" Inherits="PM_StdRpt_StorageTypeMasterList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PD_STDRPT_SELECTION_CTRL" src="../include/reports/PD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>PRODUCTION MANAGEMENT - STORAGE TYPE MASTER LISTING</title>
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
					<td class="font9Tahoma" colspan="3">PRODUCTION MANAGEMENT - STORAGE TYPE MASTER LISTING</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" />
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade>
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=17%>Storage Type Code :</td>
					<td width=39%><asp:TextBox id="txtStorageTypeCode" maxlength="10" width=50% runat="server"/> (blank for all) </td>
					<td width=4% align=center> &nbsp; </td>
					<td width=40%> &nbsp; </td>
				</tr>
				<tr>
					<td width=17%>Product :</td>
					<td width=39%><asp:DropDownList id="ddlProduct" width=50% runat="server"/></td>
					<td width=4% align=center> &nbsp; </td>
					<td width=40%> &nbsp; </td>
				</tr>
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="ddlStatus" size="1" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
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
