<%@ Page Language="vb" src="../../include/reports/PU_StdRpt_PurchaseOrderPF.aspx.vb" Inherits="PU_StdRpt_PurchaseOrderPF" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PU_STDRPT_SELECTION_CTRL" src="../include/reports/PU_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Purchasing - Purchase Order</title>
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
					<td class="font9Tahoma" colspan="3"><strong> PURCHASING - PURCHASE ORDER</strong></td>
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
					<td colspan=6>
						<asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
						<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />
					</td>
				</tr>		
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td width=17%>PO No : </td>
					<td width=39%><asp:DropDownList id=ddlPONoFrom maxlength=20 width=70% runat=server/> (blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:DropDownList id=ddlPONoTo maxlength=20 width=70% runat=server/> (blank for all)</td>
				</tr>
				<tr>
					<td width=17%>PIC I for Sign Of: </td>
					<td width=39%><asp:textbox id=txtPIC1 maxlength=128 width=50% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Jabatan : </td>
					<td width=39%>
						<asp:textbox id=txtJabatan1 maxlength=128 width=50% runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
					<tr>
					<td width=17%>PIC II for Sign Of: </td>
					<td width=39%><asp:textbox id=txtPIC2 maxlength=128 width=50% runat=server /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Jabatan : </td>
					<td width=39%>
						<asp:textbox id=txtJabatan2 maxlength=128 width=50% runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=17%>Catatan : </td>
					<td width=39%>
						<asp:TextBox id=txtCatatan size=1 width=100% textmode="Multiline" runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>	
				<tr>
					<td width=17%>Tempat Penyerahan : </td>
					<td width=39%>
						<asp:TextBox id=txtLokasi size=1 width=100% textmode="Multiline" runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>	
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
