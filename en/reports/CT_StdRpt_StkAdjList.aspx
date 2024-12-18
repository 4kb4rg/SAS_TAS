<%@ Page Language="vb" src="../../include/reports/CT_StdRpt_StkAdjList.aspx.vb" Inherits="CT_StdRpt_StkAdjList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/CT_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Canteen - Canteen Adjustment Listing</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3">CANTEEN - CANTEEN ADJUSTMENT LISTING</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width=17%>Canteen Adjustment ID From :</td>
					<td width=39%><asp:textbox id="txtStkAdjIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td width=4%>To :</td>
					<td width=40%><asp:textbox ID="txtStkAdjIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>
				<tr>
					<td>Adjustment Type :</td>
					<td><asp:DropDownList id="ddlAdjType" size="1" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Transaction Type :</td>
					<td><asp:DropDownList id="ddlTransType" size="1" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Reference No. :</td>
					<td><asp:textbox id="txtAdjRefNo" maxlength=32 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td><asp:label id=lblProdTypeCode runat=server /> :</td>
					<td><asp:textbox id="txtProdType" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdBrandCode runat=server /> :</td>
					<td><asp:textbox id="txtProdBrand" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblProdModelCode runat=server /> :</td>
					<td><asp:textbox id="txtProdModel" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:label id=lblProdCatCode runat=server /> :</td>
					<td><asp:textbox id="txtProdCat" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdMatCode runat=server /> :</td>
					<td><asp:textbox id="txtProdMaterial" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblStkAnaCode runat=server /> :</td>
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
					<td><asp:label id=lblAccCode runat=server /> :</td>
					<td><asp:textbox id="txtAccCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehType runat=server /> :</td>
					<td><asp:textbox id="txtVehType" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:label id=lblVehCode runat=server /> :</td>
					<td><asp:textbox id="txtVehCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehExpCode runat=server /> :</td>
					<td><asp:textbox id="txtVehExpCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblBlkType runat=server /> :</td>
					<td><asp:DropDownList id="ddlBlkType" AutoPostBack=true width="50%" OnSelectedIndexChanged="ddlBlkType_OnSelectedIndexChanged" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr id=trBlkGrp>
					<td><asp:label id=lblBlkGrp runat=server /> :</td>
					<td><asp:textbox id="txtBlkGrp" maxlength="8" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>										
				</tr>										
				<tr id=trBlk>
					<td><asp:label id=lblBlkCode runat=server /> :</td>
					<td><asp:textbox id="txtBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr id=trSubBlk>
					<td><asp:label id=lblSubBlkCode runat=server /> :</td>
					<td><asp:textbox id="txtSubBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="ddlStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4><asp:ImageButton id="ibPrintPreview" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" OnClick="ibPrintPreview_OnClick" runat="server" /></td>					
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
