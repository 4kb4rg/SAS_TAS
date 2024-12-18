<%@ Page Language="vb" src="../../include/reports/IN_StdRpt_StkReceiveList.aspx.vb" Inherits="IN_StdRpt_StkReceiveList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory - Stock Receive Listing</title>
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
					<td class="font9Tahoma" colspan="3"><strong> INVENTORY - STOCK RECEIVE LISTING</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
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
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td>Stock Receive ID From :</td>
					<td><asp:textbox id="txtStkRcvIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtStkRcvIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>
				<tr>
					<td>Receive Ref No :</td>
					<td><asp:textbox id="txtStkRefNo" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>PR ID From :</td>
					<td><asp:textbox id="txtPRIDFrom" width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtPRIDTo" width="50%" Runat=server /> (blank for all)</td>
				</tr>				
				<tr id=RefDateFrom>
					<td>Stock Ref Date From :</td>
					<td><asp:TextBox id="txtStkRefDateFrom" size="12" width=50% maxlength="10" runat="server"/>
  						<a href="javascript:PopCal('txtStkRefDateFrom');">
  						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>						
					<td>To :</td>
					<td><asp:TextBox id="txtStkRefDateTo" size="12" width=50% maxlength="10" runat="server"/>
  						<a href="javascript:PopCal('txtStkRefDateTo');">
  						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>										
				</tr>							
				<tr>
					<td>Receive Type :</td>
					<td><asp:DropDownList id="lstStkRcvType" size="1" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td><asp:label id=lblProdTypeCode runat=server /></td>
					<td><asp:textbox id="txtProdType" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdBrandCode runat=server /></td>
					<td><asp:textbox id="txtProdBrand" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblProdModelCode runat=server /></td>
					<td><asp:textbox id="txtProdModel" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:label id=lblProdCatCode runat=server /></td>
					<td><asp:textbox id="txtProdCat" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdMatCode runat=server /></td>
					<td><asp:textbox id="txtProdMaterial" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblStkAnaCode runat=server /></td>
					<td><asp:textbox id="txtStkAna" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>							
				<tr>
					<td>Item Code :</td>
					<td><asp:textbox id="txtItemCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>											
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" 
                            runat="server" AutoPostBack="True" /></td>
				</tr>							
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>					
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
