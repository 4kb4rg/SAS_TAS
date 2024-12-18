<%@ Page Language="vb" src="../../include/reports/AR_StdRpt_FakturPajakStandar.aspx.vb" Inherits="AR_StdRpt_FakturPajakStandar" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Receivables - Faktur Pajak Standar</title>
          <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> ACCOUNT RECEIVABLES - FAKTUR PAJAK STANDAR</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr id=TrSupp visible=false runat=server>
					<td>Suppress zero balance : </td>
					<td>
						<asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />			
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=25%>Kode dan Nomor Seri Faktur Pajak : </td>
					<td width=50%><asp:Textbox id=txtFaktur maxlength=20 width=75% runat=server/></td>			
				</tr>
				<tr>
					<td width=25%>Tanggal Pembayaran : </td>
					<td width=50%>
						<asp:Textbox id=txtTglBayar maxlength=30 width=50% runat=server/>
						<a href="javascript:PopCal('txtTglBayar');">
						<asp:Image id="btnTglBayar" runat="server" ImageUrl="../Images/calendar.gif"/></a><br>
						<asp:RequiredFieldValidator id="rfvTglBayar" runat="server" ErrorMessage="Field cannot be blank." EnableViewState="False"
							Display="Dynamic" ControlToValidate="txtTglBayar"></asp:RequiredFieldValidator>
					</td>								
				</tr>	
				<tr>
					<td height=25>Product :</td>
					<td><asp:dropdownlist id=ddlProduct width=50% runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvProduct
							display=dynamic 
							runat=server
							ControlToValidate=ddlProduct
							text="<br>Please select Product." />
					</td>										
				</tr>
				<tr>
					<td height=25>UOM :</td>
					<td><asp:DropDownList id=ddlUOMCode Width=50% runat=server/>
					    <asp:Label id=lblErrUOMCode visible=false forecolor=red runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Contract :</td>
					<td><asp:DropDownList id=ddlContract Width=75% runat=server/>
					    <asp:Label id=lblErrContract visible=false forecolor=red runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Invoice :</td>
					<td><asp:DropDownList id=ddlInvoice Width=75% runat=server/>
					    <asp:Label id=lblErrInvoice visible=false forecolor=red runat=server/>
					</td>
				</tr>	
				<tr>
					<td width=25%>Undersigned Name : </td>
					<td width=50%>
						<asp:textbox id=txtUndName maxlength=128 width=50% runat=server />
						<asp:label id=lblUndName visible=false runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td width=25%>Undersigned Post : </td>
					<td width=50%>
						<asp:textbox id=txtUndPost maxlength=128 width=50% runat=server />
						<asp:label id=lblUndPost visible=false runat=server />
					</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
				    <td>PPN Included :</td>
				    <td><asp:CheckBox id="ChkPPN" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=PPN_Type runat=server /></td>
				    <td>&nbsp;</td>				    
				</tr>
				<tr>
				    <td> </td>
				    <td><asp:RadioButton id="Opt1" runat="server" TextAlign="Right" Text=" Lembar ke-1   :   Untuk pembeli BKP/penerima JKP sebagai bukti pajak masukan" GroupName="Option" Checked="True"/><br>
                        <asp:RadioButton id="Opt2" runat="server" TextAlign="Right" Text=" Lembar ke-2   :   Untuk PKP yang menerbitkan Faktur Pajak Standar sebagai bukti pajak keluaran" GroupName="Option"/><br>
                        <asp:RadioButton id="Opt3" runat="server" TextAlign="Right" Text=" Lembar ke-3   :   Arsip" GroupName="Option"/></td>
				    <td>&nbsp;</td>				    
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
