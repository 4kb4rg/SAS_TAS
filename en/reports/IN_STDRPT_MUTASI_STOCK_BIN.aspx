<%@ Page Language="vb" src="../../include/reports/IN_STDRPT_MUTASI_STOCK_BIN.aspx.vb" Inherits="IN_STDRPT_MUTASI_STOCK_BIN" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>

    
<HTML>
	<HEAD>
		<title>Inventory - Rekap Stok Bulanan</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
        <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">

            <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%" class="font9Tahoma">
                <tr>
                    <td class="font9Tahoma" colspan="3">
                       <strong>INVENTORY - Rekap Stok Bulanan</strong> </td>
                    <td align="right" colspan="3">
                        <asp:Label ID="lblTracker" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <usercontrol:in_stdrpt_selection_ctrl id="RptSelect" runat="server"></usercontrol:in_stdrpt_selection_ctrl>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Label ID="lblDate" runat="server" ForeColor="red" Text="Incorrect Date Format. Date Format is "
                            Visible="false"></asp:Label></td>
                    <asp:Label ID="lblDateFormat" runat="server" ForeColor="red" Visible="false"></asp:Label></tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
                </tr>
            </table>
            <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%" class="font9Tahoma">
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
                    <td>
                        Item Code / Nama Item
                    </td>
                    <td>
                        :
                        <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>&nbsp;
                        <input id="Find2" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');"
                            type="button" value=" ... " />
                        ( blank for all )</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
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
                    <td>
                        Item Code / Nama Item
                    </td>
                    <td>
                        :
                        <asp:TextBox ID="txtItemCodeTo" runat="server"></asp:TextBox>&nbsp;
                        <input id="Button1" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');"
                            type="button" value=" ... " />
                        ( blank for all )</td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="cbExcel" runat="server" Checked="false" Text=" Export To Excel" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="17%">
                        <asp:ImageButton ID="PrintPrev" runat="server" AlternateText="Print Preview" ImageUrl="../images/butt_print_preview.gif"
                            OnClick="btnPrintPrev_Click" />&nbsp;</td>
                    <td width="39%">
                        &nbsp;<a href="javascript:PopCal('txtFromTrxDate');"></a><br />
                        &nbsp;</td>
                    <td width="4%">
                    </td>
                    <td width="40%">
                        &nbsp;<a href="javascript:PopCal('txtToTrxDate');"></a><br />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td height="25">
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblLocation" runat="server" Visible="false"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
            </table>
            </div>
        </td>
        </tr>
        </table>
        </form>
        <asp:Label ID="lblErrMessage" runat="server" Text="Error while initiating component."
            Visible="false"></asp:Label>
	</body>
</HTML>
