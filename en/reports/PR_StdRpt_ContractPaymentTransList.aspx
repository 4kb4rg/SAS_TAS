<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_ContractPaymentTransList.aspx.vb" Inherits="PR_StdRpt_ContractPaymentTransList"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Contract Payment Transaction Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<!--<input type="Hidden" id="hidUserLocPX" runat="server" NAME="hidUserLocPX" /> 
			<input type="Hidden" id="hidAccMonthPX" runat="server" NAME="hidAccMonthPX" />
			<input type="Hidden" id="hidAccYearPX" runat="server" NAME="hidAccYearPX" /> -->		
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma">PAYROLL - CONTRACT PAYMENT TRANSACTION LISTING</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade>
					</td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade>
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" runat="server">
				<tr>
					<td width=15%>Contract Payment Id From : </td>
					<td width=40%><asp:Textbox id=txtFromCtrtPay maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=5%>To : </td>	
					<td width=40%><asp:Textbox id=txtToCtrtPay maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td>Contractor Code : </td>
					<td><asp:Textbox id=txtContractorCode maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
				</tr>	
				<tr>
					<td><asp:label id="lblAccCode" runat=server /> : </td>  
					<td>
						<asp:TextBox id="txtSrchAccCode" maxlength=32 width=50% runat="server"/> (leave blank for all)
					</td>	
					<td colspan=2>&nbsp;</td>	
				</tr>
				<tr>
					<td><asp:label id="lblBlock" runat="server" /> Type : </td>
					<td>
						<asp:DropDownList id="ddlBlkType" width=50% AutoPostBack=true runat="server" />
					</td>
					<td colspan=2>&nbsp;</td>	
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id="lblSubBlkCode" runat="server" /> : </td>
					<td>
						<asp:textbox id="txtSrchSubBlkCode" maxlength=8 width=50% runat="server" /> (leave blank for all)
					</td>
					<td colspan=2>&nbsp;</td>	
				</tr>	
				<tr id=TrBlk>
					<td><asp:label id="lblBlkCode" runat="server" /> : </td>
					<td>
						<asp:textbox id="txtSrchBlkCode" maxlength=8 width=50% runat="server"/> (leave blank for all)
					</td>
					<td colspan=2>&nbsp;</td>	
				</tr>
				<tr>
					<td><asp:label id="lblVehCode" runat="server" /> Code : </td>
					<td>
						<asp:TextBox id="txtSrchVehCode" maxlength=8 width=50% runat="server"/> (leave blank for all)
					</td>	
					<td colspan=2>&nbsp;</td>	
				</tr>	
				<tr>
					<td><asp:label id="lblVehExpCode" runat="server" /> Code : </td>
					<td>
						<asp:TextBox id="txtSrchVehExpCode" maxlength=8 width=50% runat="server"/> (leave blank for all)
					</td>
					<td colspan=2>&nbsp;</td>	
				</tr>					
				<tr>
					<td>Transaction Status : </td>
					<td><asp:dropdownlist id=lstTransStat width=50% runat=server/></td>			
					<td colspan=2>&nbsp;</td>	
				</tr>											
				<tr>
					<td>Sort By :</td>
					<td><asp:dropdownlist id="lstSortBy" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>		
				<tr>
					<td>Order By :</td>
					<td><asp:dropdownlist id="lstOrderBy" width="50%" runat="server" >
						<asp:listitem value=ASC text="Ascending" />
						<asp:listitem value=DESC text="Descending" />
						</asp:dropdownlist>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onclick=btnPrintPrev_Click runat="server" /></td>
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

