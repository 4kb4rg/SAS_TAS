<%@ Page Language="vb" codefile="../../include/reports/PU_StdRpt_LastItemPrice.aspx.vb" Inherits="PU_StdRpt_LastItemPrice" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PU_STDRPT_SELECTION_CTRL" src="../include/reports/PU_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Purchasing - Last Purchase Price</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> PURCHASING - LAST PURCHASE PRICE</strong></td>
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
					<td colspan="6"><UserControl:PU_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
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
			
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				
				
				<tr>
					<td colspan="3"> </td>		
				</tr>
				<tr>
					<td>
                        PT </td>
					<td>
                        :&nbsp;<asp:DropDownList ID="cbPT" runat="server" Width="21%">
                            <asp:ListItem>All PT</asp:ListItem>
                            <asp:ListItem>PT.MAS</asp:ListItem>
                         
                        </asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Item Code </td>
					<td>
                        : <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>&nbsp;
                        <input id="Find2" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');"
                            type="button" value=" ... " />
                        ( blank for all )</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="3"><asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>						
				</tr>				
				
				<tr>
					<td colspan="3"><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>						
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
