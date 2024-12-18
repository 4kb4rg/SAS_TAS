<%@ Page Language="vb" src="../../include/reports/WS_StdRpt_InventoryValList.aspx.vb" Inherits="WS_StdRpt_InventoryValList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WS_STDRPT_SELECTION_CTRL" src="../include/reports/WS_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Workshop - Workshop Stock Valuation Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<!--<input type=hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">WORKSHOP - WORKSHOP STOCK VALUATION LISTING</td>
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
			<table width=100% border="0" cellspacing="0" cellpadding="1" ID="Table2" class="font9Tahoma">
				<tr>
					<td>Group By :</td>
					<td colspan=2><asp:dropdownlist id="lstGrpBy" width="70%" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>															
				<tr>
					<td><asp:label id=lblProdTypeCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtProdType" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdBrandCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtProdBrand" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblProdModelCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtProdModel" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:label id=lblProdCatCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtProdCat" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdMatCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtProdMaterial" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblStkAnaCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtStkAna" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Item Code :</td>
					<td colspan=2 ><asp:textbox id="txtItemCode" maxlength=8 width="30%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=20%>Suppress zero balance : </td>
					<td width=5%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />&nbsp;			
								 <asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=30%>&nbsp;</td>								
				</tr>	
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
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
	</body>
</HTML>
