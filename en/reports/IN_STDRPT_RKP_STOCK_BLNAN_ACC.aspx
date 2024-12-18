<%@ Page Language="vb" src="../../include/reports/IN_STDRPT_RKP_STOCK_BLNAN_ACC.aspx.vb" Inherits="IN_STDRPT_RKP_STOCK_BLNAN_ACC" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory - Rekap Stok Bulanan</title>
           <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> INVENTORY - Rekap Stok Bulanan</strong></td>
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
					<td colspan="6" style="height: 21px">&nbsp;</td>
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
			<!--	Removed: not used
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
				</tr>	\
				-->								
				<tr>
					<td style="width: 59px">
                        Item Code / Nama Item </td>
					<td style="width: 504px">
                        : <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>&nbsp;
                        <input id="Find2" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');"
                            type="button" value=" ... " />
                        ( blank for all )</td>
					<td style="width: 3px">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<!--
				<tr>
					<td>Document ID From :</td>
					<td><asp:textbox id="txtDocIDFrom" width="50%" maxlength="20" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox id="txtDocIDTo" width="50%" maxlength="20" runat="server" /> (blank for all)</td>
				</tr>
				
				<tr>
					<td width=16%>Transaction Type :</td>
					<td width=40%><asp:DropDownList id="lstTransType" width="50%" size="1" runat="server" /></td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>
				</tr>
				-->		
                <tr>
                    <td style="width: 59px">
                        Item Code / Nama Item</td>
                    <td style="width: 504px">
                        :
                        <asp:TextBox ID="txtItemCodeTo" runat="server"></asp:TextBox>&nbsp;
                        <input id="Find3" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'txtItemCodeTo', 'False');"
                            type="button" value=" ... " />
                        ( blank for all )</td>
                    <td style="width: 3px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 59px; height: 24px;">
                        Report
                        Type</td>
                    <td style="width: 504px; height: 24px">
                        :
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="False"
                            Width="50%">
                        </asp:DropDownList></td>
                    <td style="width: 3px; height: 24px">
                    </td>
                    <td style="height: 24px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 59px">
                        Saldo
                        Category</td>
                    <td style="width: 504px">
                        :
                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="False"
                            Width="50%">
                        </asp:DropDownList></td>
                    <td style="width: 3px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 22px">
                    </td>
                </tr>
				<tr>
					<td colspan="4" style="height: 22px">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
			    <tr>
					<td style="width: 59px"> <asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
					<td style="width: 504px">
                        &nbsp;<a href="javascript:PopCal('txtFromTrxDate');"></a><br>
                        &nbsp;</td>
					<td style="width: 3px"> </td>	
					<td width=40%>
                        &nbsp;<a href="javascript:PopCal('txtToTrxDate');"></a><br>
                        &nbsp;</td>
				</tr>						
				<tr>
					<td height=25 style="width: 59px"></td>
    				<td style="width: 504px">
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
