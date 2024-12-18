<%@ Page Language="vb" src="../../include/reports/PU_StdRpt_HistoricalServicePrice.aspx.vb" Inherits="PU_StdRpt_HistoricalServicePrice" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PU_STDRPT_SELECTION_CTRL" src="../include/reports/PU_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Purchasing - Purchases Item Listing</title>
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
					<td class="font9Tahoma" colspan="3"><strong> PURCHASING - HISTORICAL SERVICE PRICE</strong></td>
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
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />				
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td>Supplier :</td>
					<td><asp:textbox id="txtSupplier" width="50%" maxlength=20 runat="server" /> 
					    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSupplier', 'False');" CausesValidation=False runat=server /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>
				<tr>
					<td>Service Item :</td>
					<td><asp:textbox id="txtItemCode" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Item Notes :</td>
					<td><asp:textbox id="txtAddNote" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblPOType text="PO Type : " runat=server /> </td>
					<td><asp:DropDownList id="lstPOType" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr>
					<td width=15%>PO Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>			
				<tr>
					<td width=25%>Historical By : </td>
					<td colspan=2><asp:RadioButton id="rbService" text="Service" checked="true" GroupName="rbSupp" runat="server" />
						<asp:RadioButton id="rbItem" text="Item" GroupName="rbSupp" runat="server" />
						<asp:RadioButton id="rbSupp" text="Supplier" GroupName="rbSupp" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="cbExcel" runat="server" Checked="false" Text=" Export To Excel" /></td>
                </tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>					
				</tr>				
			</table>

        </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrCompList" ForeColor=red visible="false" text="Please select company first." runat="server" />
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
