<%@ Page Language="vb" src="../../include/reports/IN_StdRpt_ItemSummaryList.aspx.vb" Inherits="IN_StdRpt_ItemSummaryList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory Standard Reports</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu" ID="RptSelection">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">

			<input type=hidden id=x runat="server" NAME="x"/>
			<input type=hidden id=y  runat="server" NAME="y"/>

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>INVENTORY STANDARD REPORTS</strong> </td>
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
					<td colspan="6"><UserControl:IN_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma">
				<tr>
					<td width=20%>Item Code :</td>
					<td width=40%><asp:DropDownList id="lstItemCode" width="50%" runat="server" /></td>
				</tr>
				<tr>
					<td width=20%>Item Type :</td>
					<td width=40%><asp:DropDownList id="lstItemType" width="50%" size="1" runat="server" /></td>
				</tr>
				<tr>
					<td width=20%>Item Status :</td>
					<td width=40%><asp:DropDownList id="lstItemStatus" width="50%" size="1" runat="server" /></td>
				</tr>
				<tr>
					<td width=20%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
				</tr>
				<tr>
					<td width=20%><asp:Button id="PrintPrev" Text="Print Preview" onClick="btnPrintPrev_Click" CssClass="font9Tahoma" runat="server" /></td>			
					<td width=20%>&nbsp;</td>
				</tr>				
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
