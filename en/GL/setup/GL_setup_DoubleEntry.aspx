<%@ Page Language="vb"  CodeFile="~/include/GL_setup_DoubleEntry.aspx.vb" Inherits="GL_setup_DoubleEntry" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>    

<html>
	<head>
		<title>Double Entry Setup</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body onload="loadContent('Detail')">
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain  method="post" runat="server"  class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblCode text=" Code" visible=false runat=server/>
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			        <table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan=5>
						<UserControl:MenuGLSetup id=MenuGLSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td  colspan=5><strong> DOUBLE ENTRY SETUP </strong></td>
				</tr>
				<tr>
					<td colspan=5></td>
				</tr>
				<tr>
					<td width=20% height=25>&nbsp;</td>
					<td width=30%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Last Updated : </td>
					<td width=30%><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td colspan=3 height=25>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td colspan=5>
						<ul class="tab">
							<li><table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE"><tr><td><a href="#" class="tablinks" onclick="openContent(event, 'Detail')">GENERAL SETUP</a></td></tr></table></li>
							<li><table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE"><tr><td><a href="#" class="tablinks" onclick="openContent(event, 'PembelianTBS')">SETUP JURNAL PEMBELIAN TBS</a></td></tr></table></li> 							
							<li><table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #EEEEEE"><tr><td><a href="#" class="tablinks" onclick="openContent(event, 'Piutang')">SETUP JURNAL PENJUALAN PALM OIL</a></td></tr></table></li> 
						</ul>
						<div id="Detail" class="tabcontent">							
							<table cellpadding="4" cellspacing="0" style="width: 100%; background-color: #EEEEEE" class="font9Tahoma">
								<tr>	
									<td>
										<table id=tblIN border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
											<tr>
												<td width=37% height=25><u>Inventory Transaction</u></td>
												<td width=30%><b>DEBIT Account</b></td>
												<td width=3%>&nbsp;</td>
												<td width=30% align=right><b>CREDIT Account</b></td>
											</tr>
											<tr>
												<td valign=top align="left">Stock Receive from Dispatch Advice for Direct Charge Item PR :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False class="fontObject"  id=ddlDRINStkRcvDADirectPR width=100% runat=server/>
													<asp:Label id=lblErrDRINStkRcvDADirectPR visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStkRcvDADirectPR   class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrCRINStkRcvDADirectPR visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Stock Receive from Dispatch Advice for Stock Item PR :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINStkRcvDAStockPR width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRINStkRcvDAStockPR visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStkRcvDAStockPR width=100%   class="fontObject" runat=server/>
													<asp:Label id=lblErrCRINStkRcvDAStockPR visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Stock Receive from Stock Transfer :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINStkRcvStkTransfer width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRINStkRcvStkTransfer visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStkRcvStkTransfer width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRINStkRcvStkTransfer visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Stock Receive from Stock Return Advice :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINStkRcvStkRtnAdvice width=100% runat=server/>
													<asp:Label id=lblErrDRINStkRcvStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStkRcvStkRtnAdvice width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRINStkRcvStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Stock Return Advice :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINStkRtnAdvice width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRINStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStkRtnAdvice width=100% class="fontObject"  runat=server/>
													<asp:Label id=lblErrCRINStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Stock Issue for Employee :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINStkIssueEmp width=100% runat=server/>
													<asp:Label id=lblErrDRINStkIssueEmp visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStkIssueEmp width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRINStkIssueEmp visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Fuel Issue for Employee :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINFuelIssueEmp width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRINFuelIssueEmp visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINFuelIssueEmp width=100% runat=server/>
													<asp:Label id=lblErrCRINFuelIssueEmp visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Balance Amount from Stock Return Advice :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINBalanceFromStkRtnAdvice  class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrDRINBalanceFromStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINBalanceFromStkRtnAdvice  class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrCRINBalanceFromStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Penyesuaian Stock Gudang :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRINStockAdj width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRINStockAdj visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False  class="fontObject" id=ddlCRINStockAdj width=100% AutoPostBack=True OnSelectedIndexChanged="ddlCRINStockAdj_OnSelectedIndexChanged" runat=server/>
													<asp:Label id=lblErrCRINStockAdj visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left"></td>
												<td valign=top align="left">									
													<asp:Dropdownlist visible=false id=ddlDRINStockAdjBlkCode width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRINStockAdjBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRINStockAdjBlkCode width=100% runat=server/>
													<asp:Label id=lblErrCRINStockAdjBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
											</tr>
											<tr>
												<td colspan=4>&nbsp;</td>
											</tr>
										</table>
										
										
										<table id=tblPU border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
											<tr>
												<td width=37% height=25><u>Purchasing Transaction</u></td>
												<td width=30%><b>DEBIT Account</b></td>
												<td width=3%>&nbsp;</td>
												<td width=30% align=right><b>CREDIT Account</b></td>
											</tr>
											<tr>
												<td valign=top align="left">Goods Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRPUGoodsRcv class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrDRPUGoodsRcv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRPUGoodsRcv  class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrCRPUGoodsRcv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>		
											<tr>
												<td valign=top align="left">Dispatch Advice :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRPUDispAdv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRPUDispAdv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRPUDispAdv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRPUDispAdv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>							
											<tr>
												<td colspan=4>&nbsp;</td>
											</tr>
										</table>
										<table id=tblAP border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
											<tr>
												<td width=37% height=25><u>Account Payable Transaction</u></td>
												<td width=30%><b>DEBIT Account</b></td>
												<td width=3%>&nbsp;</td>
												<td width=30% align=right><b>CREDIT Account</b></td>
											</tr>
											<tr visible=False>
												<td valign=top align="left">Invoice Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPInvRcv  class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrDRAPInvRcv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRAPInvRcv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">PPN Invoice Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPNInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAPPPNInvRcv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPNInvRcv width=100% runat=server/>
													<asp:Label id=lblErrCRAPPPNInvRcv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>																
											<tr>
												<td valign=top align="left">PPN Invoice Not Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPNInvRcv2 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAPPPNInvRcv2 visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPNInvRcv2 width=100% runat=server/>
													<asp:Label id=lblErrCRAPPPNInvRcv2 visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											
											<tr>
												<td valign=top align="left">PPN Invoice Non Credit :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPNInvRcv4 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAPPPNInvRcv4 visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPNInvRcv4 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRAPPPNInvRcv4 visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											
											<tr>
												<td valign=top align="left">Selisih Pembulatan :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPNInvRcv3 width=100%  class="fontObject" runat=server OnSelectedIndexChanged="ddlDRAPPPNInvRcv3_OnSelectedIndexChanged" />
													<asp:Label id=lblErrDRAPPPNInvRcv3 visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
											
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPNInvRcv3 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRAPPPNInvRcv3 visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
												
											</tr>			
											<tr>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPNInvRcv3Blk width=100% AutoPostBack=false  class="fontObject"  runat=server/>
													<asp:Label id=lblErrDRAPPPNInvRcv3Blk visible=false forecolor=red text="" runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPNInvRcv3Blk width=100% AutoPostBack=false   class="fontObject" runat=server/>
													<asp:Label id=lblErrCRAPPPNInvRcv3Blk visible=false forecolor=red text="" runat=server/>
												</td>
											</tr>																								
											<tr>
												<td valign=top align="left">PPh 23/26 Invoice Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPHInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAPPPHInvRcv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPHInvRcv width=100% runat=server/>
													<asp:Label id=lblErrCRAPPPHInvRcv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">PPN Creditor Journal :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPNCrdJrn width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAPPPNCrdJrn visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPNCrdJrn width=100% runat=server/>
													<asp:Label id=lblErrCRAPPPNCrdJrn visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">PPh 23/26 Creditor Journal :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAPPPHCrdJrn width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAPPPHCrdJrn visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAPPPHCrdJrn width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRAPPPHCrdJrn visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Advance Payment :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRAdvPayment width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRAdvPayment visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRAdvPayment width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRAdvPayment visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td colspan=4>&nbsp;</td>
											</tr>
										</table>
										<table id=tblBI border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
											<tr>
												<td width=37% height=25><u>Account Receivable Transaction</u></td>
												<td width=30%><b>DEBIT Account</b></td>
												<td width=3%>&nbsp;</td>
												<td width=30% align=right><b>CREDIT Account</b></td>
											</tr>
											<tr>
												<td valign=top align="left">PPN Invoice Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRBIPPNInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRBIPPNInvRcv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRBIPPNInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRBIPPNInvRcv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">PPh 23/26 Invoice Receive :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRBIPPHInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRBIPPHInvRcv visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRBIPPHInvRcv width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRBIPPHInvRcv visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td colspan=4>&nbsp;</td>
											</tr>
										</table>
										
										<table id=tblCB border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
											<tr>
												<td width=37% height=25><u>Cash And Bank</u></td>
												<td width=30%><b>DEBIT Account</b></td>
												<td width=3%>&nbsp;</td>
												<td width=30% align=right><b>CREDIT Account</b></td>
											</tr>
											<tr>
												<td valign=top align="left">PPh 23/26 Payment :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRCBPPHInvPay width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRCBPPHInvPay visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRCBPPHInvPay width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRCBPPHInvPay visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">PPN Receipt :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRCBPPNRcpt width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRCBPPNRcpt visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRCBPPNRcpt width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRCBPPNRcpt visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">PPh 23/26 Receipt :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRCBPPHRcpt width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRCBPPHRcpt visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRCBPPHRcpt width=100%   class="fontObject" runat=server/>
													<asp:Label id=lblErrCRCBPPHRcpt visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Selisih Kurs :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRIntIncome width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRIntIncome visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRIntIncome width=100%  class="fontObject" AutoPostBack=True OnSelectedIndexChanged="ddlCRIntIncome_OnSelectedIndexChanged" runat=server/>
													<asp:Label id=lblErrCRIntIncome visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>															
											<tr>
												<td valign=top align="left"></td>
												<td valign=top align="left">									
													<asp:Dropdownlist visible=false id=ddlDRIntIncomeBlkCode width=100% runat=server/>
													<asp:Label id=lblErrDRIntIncomeBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRIntIncomeBlkCode  class="fontObject" width=100% runat=server/>
													<asp:Label id=lblErrCRIntIncomeBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
											</tr>
										
											<tr>
												<td valign=top align="left">Selisih Kurs Blm Terealisasi:</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRIntIncome2 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRIntIncome2 visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRIntIncome2  class="fontObject" width=100% AutoPostBack=True OnSelectedIndexChanged="ddlCRIntIncome2_OnSelectedIndexChanged" runat=server/>
													<asp:Label id=lblErrCRIntIncome2 visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>																								
											</tr>
											
											<tr>
												<td valign=top align="left"></td>
												<td valign=top align="left">									
													<asp:Dropdownlist visible=false id=ddlDRIntIncomeBlkCode2 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrDRIntIncomeBlkCode2 visible=false forecolor=red text="" runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRIntIncomeBlkCode2 width=100%  class="fontObject" runat=server/>
													<asp:Label id=lblErrCRIntIncomeBlkCode2 visible=false forecolor=red text="" runat=server/>
												</td>
											</tr>																					
											<tr>
												<td colspan=4>&nbsp;</td>
											</tr>
										</table>
										
										<table id=tblGL border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
											<tr>
												<td width=37% height=25><u>General Ledger</u></td>
												<td width=30%><b>DEBIT Account</b></td>
												<td width=3%>&nbsp;</td>
												<td width=30% align=right><b>CREDIT Account</b></td>
											</tr>
											<tr>
												<td valign=top align="left">Sundry Income :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRSunIncome width=100% runat=server/>
													<asp:Label id=lblErrDRSunIncome visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRSunIncome width=100% AutoPostBack=True OnSelectedIndexChanged="ddlCRSunIncome_OnSelectedIndexChanged" runat=server/>
													<asp:Label id=lblErrCRSunIncome visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											
											<tr>
												<td valign=top align="left"></td>
												<td valign=top align="left">									
													<asp:Dropdownlist visible=false id=ddlDRSunIncomeBlkCode width=100% runat=server/>
													<asp:Label id=lblErrDRSunIncomeBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRSunIncomeBlkCode width=100% runat=server/>
													<asp:Label id=lblErrCRSunIncomeBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
											</tr>
											
											<tr>
												<td valign=top align="left">Vehicle Suspend :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRVehSuspende width=100% runat=server/>
													<asp:Label id=lblErrDRVehSuspende visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRVehSuspende width=100% runat=server/>
													<asp:Label id=lblErrCRVehSuspende visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>

											<tr>
												<td valign=top align="left">Retained Earning :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRRetainEarn width=100% runat=server/>
													<asp:Label id=lblErrDRRetainEarn visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRRetainEarn width=100% runat=server/>
													<asp:Label id=lblErrCRRetainEarn visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Balance from Workshop :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRBalFrmWSAccCode width=100% AutoPostBack=True OnSelectedIndexChanged="ddlDRBalFrmWSAccCode_OnSelectedIndexChanged"  runat=server/>
													<asp:Label id=lblErrDRBalFrmWSAccCode visible=false forecolor=red text="Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">									
													<asp:Dropdownlist Enabled=False id=ddlCRBalFrmWSAccCode width=100% AutoPostBack=True OnSelectedIndexChanged="ddlCRBalFrmWSAccCode_OnSelectedIndexChanged" runat=server/>
													<asp:Label id=lblErrCRBalFrmWSAccCode visible=false forecolor=red text="Please select one account to Credit." runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left"></td>
												<td valign=top align="left">									
													<asp:Dropdownlist Enabled=False id=ddlDRBalFrmWSBlkCode width=100% runat=server/>
													<asp:Label id=lblErrDRBalFrmWSBlkCode visible=false forecolor=red text="" runat=server/>
													<asp:Label id=lblABC visible=true forecolor=red text="" runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlCRBalFrmWSBlkCode width=100% runat=server/>
													<asp:Label id=lblErrCRBalFrmWSBlkCode visible=false forecolor=red text="" runat=server/>
												</td>
											</tr>
											<tr>
												<td valign=top align="left">Product Ending Balance movement :</td>
												<td valign=top align="left">
													<asp:Dropdownlist Enabled=False id=ddlDRPEBMovementAccCode width=100% runat=server/>
													<asp:Label id=lblErrDRPEBMovementAccCode visible=false forecolor=red text="Please select one account to Debit." runat=server/>
												</td>
												<td>&nbsp;</td>
												<td valign=top align="left">									
													<asp:Dropdownlist Enabled=False id=ddlCRPEBMovementAccCode width=100% runat=server/>
													<asp:Label id=lblErrCRPEBMovementAccCode visible=false forecolor=red text="Please select one account to Credit." runat=server/>
												</td>
											</tr>							
											<tr>
												<td colspan=4>&nbsp;</td>
											</tr>
										</table>
									</td>
									</tr>
									<tr>
										<td colspan=5>
											<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" CommandArgument=Save onclick=Button_Click runat=server />
											<asp:Label id=lblHasRecord visible=false text=false runat=server/>
										</td>
									</tr>									
								</table>
						</div>
					
						<div id="PembelianTBS" class="tabcontent">
							<table cellpadding="4" cellspacing="0" style="width: 100%; background-color: #EEEEEE" class="font9Tahoma">
								<tr>
									<td>
										<div id="font9Tahoma" >	
											<tr>
												<td height="25" width="15%" >Pembelian TBS (Pemilik Kebun) :</td>
												<td width="50%">
														<telerik:RadComboBox   CssClass="fontObject" ID="radTbsPemilik" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox>                                                            
												</td>
											</tr>
											<tr>
												<td height="25">Pembelian TBS (Agen) :</td>
												<td>
													<telerik:RadComboBox   CssClass="fontObject" ID="radTbsAgen"
														Runat="server" AllowCustomText="True" 
														EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
														ExpandDelay="50" Filter="Contains" Sort="Ascending" 
														EnableVirtualScrolling="True">
														<CollapseAnimation Type="InQuart" />
													</telerik:RadComboBox>                                                             
												</td>
											</tr>
											<tr>
												<td height="25">PPN :</td>
												<td>
													<telerik:RadComboBox   CssClass="fontObject" ID="radTbsPPN"
													Runat="server" AllowCustomText="True" 
													EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
													ExpandDelay="50" Filter="Contains" Sort="Ascending" 
													EnableVirtualScrolling="True">
													<CollapseAnimation Type="InQuart" />
												</telerik:RadComboBox>                                                             
												</td>
											</tr>
											<tr>
												<td height="25">PPH :</td>
												<td>
													<telerik:RadComboBox   CssClass="fontObject" ID="radTbsPPH"
													Runat="server" AllowCustomText="True" 
													EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
													ExpandDelay="50" Filter="Contains" Sort="Ascending" 
													EnableVirtualScrolling="True">
													<CollapseAnimation Type="InQuart" />
												</telerik:RadComboBox>                                                             
												</td>
											</tr>                                                    
											<tr>
												<td height="25" >Hutang SPTI Ongkos Bongkar TBS :</td>
												<td>
													<telerik:RadComboBox   CssClass="fontObject" ID="radTBSOBongkar"
														Runat="server" AllowCustomText="True" 
														EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
														ExpandDelay="50" Filter="Contains" Sort="Ascending" 
														EnableVirtualScrolling="True">
														<CollapseAnimation Type="InQuart" />
												</telerik:RadComboBox>                                                             
												</td>
											</tr>
											<tr>
												<td height="25">Hutang SPTI Ongkos Lapangan TBS :</td>
												<td>
													<telerik:RadComboBox   CssClass="fontObject" ID="radTbsOLapangan"
														Runat="server" AllowCustomText="True" 
														EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
														ExpandDelay="50" Filter="Contains" Sort="Ascending" 
														EnableVirtualScrolling="True">
														<CollapseAnimation Type="InQuart" />
													</telerik:RadComboBox>                                                             
												</td>
											</tr>
			
											<tr>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="25" style="width: 185px">
													<asp:ImageButton id=btnSaveSetting imageurl="../../images/butt_save.gif" AlternateText="Save setting" onclick="btnSaveTBS_Click"   runat="server" /></td>
											</tr>
										</div>
									</td>
								</tr>
							</table>
						</div>

						<div id="Piutang" class="tabcontent">
							<table cellpadding="4" cellspacing="0" style="width: 100%; background-color: #EEEEEE" class="font9Tahoma">
								<tr>
									<td>	
										<div id="div2" style="width:100%">		
											<tr>
												<td width=20%>CPO (Crude Palm Oil) :</td>
												<td width=30%>														
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesCPO" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">                                                        
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxCPO" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td width=20%>PK (Palm Kernell) :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesKNL" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxPK" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">EFB (Janjang Kosong) :</td>
												<td width=30%>
													     <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesEFB" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxEFB" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">Shell (Cangkang) :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesCKG" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                         <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxCKG" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">Abu Janjang :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesABJ" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxABJ" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">Fiber :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesFBR" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxFBR" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">Solid :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesSLD" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxSLD" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">Minyak Limbah :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesLMB" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxLMB" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">Lain-lain :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesOTH" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
												<td>&nbsp</td>
												<td width=10% style="visibility:hidden">Transport :</td>
												<td width=30% style="visibility:hidden">
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesTrxOTH" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td style="width:20%">PPN :</td>
												<td width=30%>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="ddlSalesPPN" 
															Runat="server" AllowCustomText="True" 
															EmptyMessage="Please Select Chart of Account" Height="200" Width="100%" 
															ExpandDelay="50" Filter="Contains" Sort="Ascending" 
															EnableVirtualScrolling="True">
															<CollapseAnimation Type="InQuart" />
														</telerik:RadComboBox> 
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="25" style="width:20%">
													<asp:ImageButton id=btnSaveSales imageurl="../../images/butt_save.gif" AlternateText="Save setting" onclick="btnSaveSales_Click"  runat="server" /></td>
											</tr>
										</div>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>

			</table>
                </div>
            </td>
            </tr>

            </table>

			<table id=tblPD style="visibility:hidden" border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
				<tr>
					<td width=37% height=25><u>Production Transaction</u></td>
					<td width=30%><b>DEBIT Account</b></td>
					<td width=3%>&nbsp;</td>
					<td width=30% align=right><b>CREDIT Account</b></td>
				</tr>
				<tr>
					<td valign=top align="left">Estate Yield :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDREstYield width=100% runat=server/>
						<asp:Label id=lblErrDREstYield visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCREstYield width=100% runat=server/>
						<asp:Label id=lblErrCREstYield visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left">Harga Pokok Produksi (COGS) :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRPDHPPCost width=100%  AutoPostBack=True OnSelectedIndexChanged="ddlDRPDHPPCost_OnSelectedIndexChanged" runat=server/>
						<asp:Label id=lblErrDRPDHPPCost visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPDHPPCost width=100%  AutoPostBack=True OnSelectedIndexChanged="ddlCRPDHPPCost_OnSelectedIndexChanged" runat=server/>
						<asp:Label id=lblErrCRPDHPPCost visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left"></td>
					<td valign=top align="left">									
						<asp:Dropdownlist visible=false id=ddlDRPDHPPBlkCode width=100% runat=server/>
						<asp:Label id=lblErrDRPDHPPBlkCode visible=false forecolor=red text="" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPDHPPBlkCode width=100% runat=server/>
						<asp:Label id=lblErrCRPDHPPBlkCode visible=false forecolor=red text="" runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left">Pembelian TBS Kebun Sendiri :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRPDTBSKebun width=100%  AutoPostBack=True OnSelectedIndexChanged="ddlDRPDTBSKebun_OnSelectedIndexChanged" runat=server/>
						<asp:Label id=lblErrDRPDTBSKebun visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPDTBSKebun width=100%  AutoPostBack=True OnSelectedIndexChanged="ddlCRPDTBSKebun_OnSelectedIndexChanged" runat=server/>
						<asp:Label id=lblErrCRPDTBSKebun visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left"></td>
					<td valign=top align="left">									
						<asp:Dropdownlist visible=false id=ddlDRPDTBSKebunBlkCode width=100% runat=server/>
						<asp:Label id=lblErrDRPDTBSKebunBlkCode visible=false forecolor=red text="" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPDTBSKebunBlkCode width=100% runat=server/>
						<asp:Label id=lblErrCRPDTBSKebunBlkCode visible=false forecolor=red text="" runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left">Persediaan CPO :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRPDStockCPO width=100% runat=server/>
						<asp:Label id=lblErrDRPDStockCPO visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPDStockCPO width=100% runat=server/>
						<asp:Label id=lblErrCRPDStockCPO visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left">Persediaan Kernell :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRPDStockPK width=100% runat=server/>
						<asp:Label id=lblErrDRPDStockPK visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPDStockPK width=100% runat=server/>
						<asp:Label id=lblErrCRPDStockPK visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
			</table>
			<table id=tblCT style="visibility:hidden" border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
				<tr>
					<td width=37% height=25><u>Canteen Transaction</u></td>
					<td width=30%><b>DEBIT Account</b></td>
					<td width=3%>&nbsp;</td>
					<td width=30% align=right><b>CREDIT Account</b></td>
				</tr>
				<tr>
					<td valign=top align="left">Canteen Receive from Dispatch Advice for Direct Charge Item PR (optional) :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTStkRcvDADirectPR width=100% runat=server/>
						<asp:Label id=lblErrDRCTStkRcvDADirectPR visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTStkRcvDADirectPR width=100% runat=server/>
						<asp:Label id=lblErrCRCTStkRcvDADirectPR visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left">Canteen Receive from Dispatch Advice for Stock Item PR (optional) :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTStkRcvDAStockPR width=100% runat=server/>
						<asp:Label id=lblErrDRCTStkRcvDAStockPR visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTStkRcvDAStockPR width=100% runat=server/>
						<asp:Label id=lblErrCRCTStkRcvDAStockPR visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr class="font9Tahoma">
					<td valign=top align="left">Canteen Receive from Stock Transfer :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTStkRcvStkTransfer width=100% runat=server/>
						<asp:Label id=lblErrDRCTStkRcvStkTransfer visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTStkRcvStkTransfer width=100% runat=server/>
						<asp:Label id=lblErrCRCTStkRcvStkTransfer visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr class="font9Tahoma">
					<td valign=top align="left">Canteen Receive from Stock Return Advice :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTStkRcvStkRtnAdvice width=100% runat=server/>
						<asp:Label id=lblErrDRCTStkRcvStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTStkRcvStkRtnAdvice width=100% runat=server/>
						<asp:Label id=lblErrCRCTStkRcvStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr class="font9Tahoma">
					<td valign=top align="left">Canteen Return Advice :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTStkRtnAdvice width=100% runat=server/>
						<asp:Label id=lblErrDRCTStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTStkRtnAdvice width=100% runat=server/>
						<asp:Label id=lblErrCRCTStkRtnAdvice visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr class="font9Tahoma">
					<td valign=top align="left">Canteen Issue for Employee :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTStkIssueEmp width=100% runat=server/>
						<asp:Label id=lblErrDRCTStkIssueEmp visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTStkIssueEmp width=100% runat=server/>
						<asp:Label id=lblErrCRCTStkIssueEmp visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>							
				<tr class="font9Tahoma">
					<td valign=top align="left">Balance Amount from Canteen Return Advice:</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRCTBalanceFromCTRtnAdvice width=100% runat=server/>
						<asp:Label id=lblErrDRCTBalanceFromCTRtnAdvice visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRCTBalanceFromCTRtnAdvice width=100% runat=server/>
						<asp:Label id=lblErrCRCTBalanceFromCTRtnAdvice visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
			</table>
			<table id=tblWS style="visibility:hidden" border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
				<tr>
					<td width=37% height=25><u>Workshop Transaction</u></td>
					<td width=30%><b>DEBIT Account</b></td>
					<td width=3%>&nbsp;</td>
					<td width=30% align=right><b>CREDIT Account</b></td>
				</tr>
				<tr>
					<td valign=top align="left">Job for Employee :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRWSJobEmp width=100% runat=server/>
						<asp:Label id=lblErrDRWSJobEmp visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRWSJobEmp width=100% runat=server/>
						<asp:Label id=lblErrCRWSJobEmp visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td valign=top align="left">Control Account :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRWSCtrlAcc width=100% runat=server/>
						<asp:Label id=lblErrDRWSCtrlAcc visible=false forecolor=red text="Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRWSCtrlAcc width=100% runat=server/>
						<asp:Label id=lblErrCRWSCtrlAcc visible=false forecolor=red text="Please select one account to Credit." runat=server/>
					</td>
				</tr>							
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
			</table>			
			<table id=tblNU style="visibility:hidden" border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
				<tr>
					<td width=37% height=25><u>Nursery Transaction</u></td>
					<td width=30%><b>DEBIT Account</b></td>
					<td width=3%>&nbsp;</td>
					<td width=30% align=right><b>CREDIT Account</b></td>
				</tr>
				<tr>
					<td valign=top align="left">Seedlings Issue :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRNUSeedlingsIssue width=100% runat=server/>
						<asp:Label id=lblErrDRNUSeedlingsIssue visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRNUSeedlingsIssue width=100% runat=server/>
						<asp:Label id=lblErrCRNUSeedlingsIssue visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>							
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
			</table>
			<table id=tblPR style="visibility:hidden" border=0 cellspacing=0 cellpadding=2 width=100% runat=server class="font9Tahoma">
				<tr>
					<td width=37% height=25><u>Payroll Transaction</u></td>
					<td width=30%><b>DEBIT Account</b></td>
					<td width=3%>&nbsp;</td>
					<td width=30% align=right><b>CREDIT Account</b></td>
				</tr>
				<tr>
					<td valign=top align="left">Payroll Clearence Account :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRPRClr width=100% runat=server/>
						<asp:Label id=lblErrDRPRClr visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPRClr width=100% runat=server/>
						<asp:Label id=lblErrCRPRClr visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>								
					<td valign=top align="left">Hutang Karyawan :</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlDRPRHK width=100% runat=server/>
						<asp:Label id=lblErrDRPRHK visible=false forecolor=red text="<br>Please select one account to Debit." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top align="left">
						<asp:Dropdownlist Enabled=False id=ddlCRPRHK width=100% runat=server/>
						<asp:Label id=lblErrCRPRHK visible=false forecolor=red text="<br>Please select one account to Credit." runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
			</table>
						
			<script>
				function loadContent(contentName) {
					var i, tabcontent, tablinks;
					tabcontent = document.getElementsByClassName("tabcontent");
					tabcontent[0].style.display = "block";
					tablinks = document.getElementsByClassName("tablinks");
					tablinks[0].className = tablinks[0].className.replace(" active", "");
					document.getElementById(contentName).style.display = "block";
				}
	
				function openContent(evt, contentName) {
					var i, tabcontent, tablinks;
					tabcontent = document.getElementsByClassName("tabcontent");
	
					for (i = 0; i < tabcontent.length; i++) {
						tabcontent[i].style.display = "none";
					}
	
					tablinks = document.getElementsByClassName("tablinks");
	
					for (i = 0; i < tablinks.length; i++) {
						tablinks[i].className = tablinks[i].className.replace(" active", "");
					}
	
					document.getElementById(contentName).style.display = "block";
					evt.currentTarget.className += " active";
				}
			</script>		
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        	
		</form>
	</body>
</html>
