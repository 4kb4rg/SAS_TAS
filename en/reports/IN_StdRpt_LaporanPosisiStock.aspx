<%@ Page Language="vb" src="../../include/reports/IN_StdRpt_LaporanPosisiStock.aspx.vb" Inherits="IN_StdRpt_LaporanPosisiStock" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory - Stock Movement Detail Listing</title>
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
					<td class="font9Tahoma" colspan="3"><strong> INVENTORY - LAPORAN POSISI STOCK</strong></td>
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
					<td>Item Code :</td>
					<td><asp:textbox id="txtItemCode" maxlength=20 width="50%" runat="server" /> 					    
					     <input type=button value=" ... " id="Find2" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');" CausesValidation=False runat=server />
						 <asp:Label id=lblErrItemCode forecolor=red visible=false text="Item code cannot be blank for detail report type"  runat=server/>
					     </td>					
					<td>&nbsp;</td>
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
					<td width=17%>Transaction Date From : </td>
					<td width=39%>
						<asp:Textbox id=txtFromTrxDate maxlength=30 width=50% runat=server/>
						<a href="javascript:PopCal('txtFromTrxDate');">
						<asp:Image id="btnFromInvoiceDate" runat="server" ImageUrl="../Images/calendar.gif"/></a><br>
						<asp:RequiredFieldValidator id="rfvTrxDate" runat="server" ErrorMessage="Field cannot be blank." EnableViewState="False"
							Display="Dynamic" ControlToValidate="txtFromTrxDate"></asp:RequiredFieldValidator>
					</td>
					<td width=4%>To : </td>	
					<td width=40%>
						<asp:Textbox id=txtToTrxDate maxlength=30 width=50% runat=server/>
						<a href="javascript:PopCal('txtToTrxDate');">
						<asp:Image id="btnToInvoiceDate" runat="server" ImageUrl="../Images/calendar.gif"/></a><br>
						<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Field cannot be blank." EnableViewState="False"
							Display="Dynamic" ControlToValidate="txtToTrxDate"></asp:RequiredFieldValidator>
					</td>
				</tr>						
				<tr style="visibility:hidden">
					<td height=25>Inventory Bin :</td>
    				<td><asp:DropDownList id="ddlInventoryBin" Width=50% runat=server/>
    				</td>
				</tr>
				<tr>
					<td width=16%>Report Type :</td>
					<td><asp:DropDownList width=50% id=ddlRptType autopostback=false runat=server>
						<asp:ListItem value="0" selected>Detail</asp:ListItem>
						<asp:ListItem value="1">Rekap</asp:ListItem>
					</asp:DropDownList>
					</td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>
				</tr>
                <tr>
                    <td style="width: 59px; height: 24px;">
                       Warehouse :</td>
                    <td style="width: 504px; height: 24px">
                        
                        <asp:DropDownList ID="lstStorage" runat="server" AutoPostBack="False"
                            Width="50%">
                        </asp:DropDownList></td>
                    <td style="width: 3px; height: 24px">
                    </td>
                    <td style="height: 24px">
                    </td>
                </tr>

				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
				<tr>
					<td colspan=4>&nbsp;<asp:CheckBox ID="cbExcel" runat="server" Checked="false" Text=" Export To Excel" /></td>
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
