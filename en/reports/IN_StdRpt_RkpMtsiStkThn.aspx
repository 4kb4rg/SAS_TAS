<%@ Page Language="vb" src="../../include/reports/IN_StdRpt_RkpMtsiStkThn.aspx.vb" Inherits="IN_StdRpt_RkpMtsiStkThn" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory - REKAP MUTASI STOCK TAHUNAN</title>
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
					<td class="font9Tahoma" colspan="3"><strong>INVENTORY - REKAP MUTASI STOCK TAHUNAN</strong> </td>
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
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></tr>
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
					<td width=17%> Item Code / Nama Item
                        
                        </td>
                     <td width=50%> 
                         :&nbsp;
                         <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                         &nbsp;
                         <input id="Find2" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');"
                             type="button" value=" ... " />
                         ( blank for all )</td>
				</tr>
				<tr>
					<td>
					<br />
                        Rekap Type</td>
					<td>
					<br />
                        :&nbsp;&nbsp;<asp:RadioButton ID="rbIN" runat="server" GroupName="rbType" Text="IN" />&nbsp;
                        &nbsp;<asp:RadioButton
                            ID="rbOUT" runat="server" Checked="true" GroupName="rbType" Text="OUT" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td height=25>
                        <asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>
    				<td>
                        &nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4></td>			
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
