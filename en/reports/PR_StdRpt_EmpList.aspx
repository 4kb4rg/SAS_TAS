<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_EmpList.aspx.vb" Inherits="PR_StdRpt_EmpList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Employee Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblLocation" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - EMPLOYEE LISTING</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
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
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table1">
				<tr>
					<td>Employee Code From :</td>
					<td><asp:textbox id="txtEmpCodeFrom" maxlength=20 width="50%" runat="server" /> (leave blank for all)</td>
					<td>To :</td>
					<td><asp:textbox id="txtEmpCodeTo" maxlength=20 width="50%" runat="server" /> (leave blank for all)</td>
				</tr>		
				<tr>
					<td>Employee Status :</td>
					<td><asp:DropDownList id="lstStatus" width="50%" size="1" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Gang Code :</td>
					<td><asp:textbox id=txtGangCode maxlength=8 width=50% runat=server /> (leave blank for all)</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=16%>Sort By :</td>
					<td width=40%><asp:DropDownList id="lstSortBy" width="50%" size="1" runat="server" /></td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td>Order By :</td>
					<td><asp:dropdownlist id="lstOrderBy" width="50%" runat="server" >
						<asp:listitem value=ASC text="Ascending" />
						<asp:listitem value=DESC text="Descending" />
						</asp:dropdownlist>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>				
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>				
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblJamsostek" visible="false" text="" runat="server"/>
	</body>
</HTML>
