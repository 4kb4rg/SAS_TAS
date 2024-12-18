<%@ Page Language="vb" src="../include/appmenu.aspx.vb" Inherits="appmenu" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Application Menu</title>
		<PREFERENCE:PREFHDL id="PrefHdl" runat="server"></PREFERENCE:PREFHDL>
	</HEAD>
	<body class="menu" leftMargin="0" topMargin="0" onload="javascript:togglebox(tblOther);">
		<form id="frmAppMenu" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><img src="images/spacer.gif" border="0" width="5" height="5"></td>
				</tr>
				<tr>
					<td>
						<table id="tblGrpTran" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="25">
								<td width="20" class="lb-ht"></td>
								<td width="3"></td>
								<td class="lb-ht"><a class="lb-ti">TRANSACTION</a></td>
							</tr>
						</table>
						<table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblGLHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblGL);">General Ledger</A></td>
							</tr>
						</table>
						<table id="tblGL" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkGL01" runat="server" cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_Journal_list.aspx" target="right"
										text="Journal"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkGL02" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_PostJournal_list.aspx" target="right" text="Post Journal"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkGL03" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_JournalAdj_list.aspx" target="right" text="Journal Adjustment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink  id="lnkGL04" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_VehicleUsage_list.aspx" target="right" text="Vehicle Usage"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink  id="lnkGL05" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_RunningHour_list.aspx" target="right" text="Actual Station Running Hour"></asp:hyperlink></td>
							</tr>
							
						</table>
						<table id="tblSpc2" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblBillHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblBill);">Account 
										Receivables</A></td>
							</tr>
						</table>
						<table id="tblBill" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill01" NavigateUrl="/en/BI/Trx/BI_Trx_InvoiceList.aspx" runat="server" target="right" text="Invoice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill02" runat="server" NavigateUrl="/en/BI/Trx/BI_Trx_DNList.aspx" target="right" text="Debit Note"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill03" runat="server" NavigateUrl="/en/BI/Trx/BI_Trx_CNList.aspx" target="right" text="Credit Note"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill04" runat="server" NavigateUrl="/en/BI/Trx/BI_trx_JournalList.aspx" target="right" text="Debtor Journal"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblAPHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblAP);">Account Payables</A></td>
							</tr>
						</table>
						<table id="tblAP" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP01" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_InvRcvList.aspx" target="right" text="Credited Invoice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP02" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_DNList.aspx" target="right" text="Supplier Debit Note"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP03" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_CNList.aspx"  target="right" text="Supplier Credit Note"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP04" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_CJList.aspx" target="right" text="Creditor Journal"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc4" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblCBHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblCB);">Cash And Bank</A></td>
							</tr>
						</table>
						<table id="tblCB" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB01" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_PayList.aspx" target="right" text="Payment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB02" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_ReceiptList.aspx" target="right" text="Received"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB03" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_CashBankList.aspx" target="right" text="Cash Bank"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB04" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_DepositList.aspx" target="right" text="Deposit"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB05" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_InterestAdjList.aspx" target="right" text="Interest Adjustment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB06" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_WithdrawalList.aspx" target="right" text="Withdrawal"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc5" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblPUHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblPU);">Purchasing</A></td>
							</tr>
						</table>
						<table id="tblPU" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU01" runat="server" NavigateUrl="/en/PU/Trx/pu_trx_RPHList.aspx" target="right" text="Quotation"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU02" runat="server" NavigateUrl="/en/PU/Trx/pu_trx_POlist.aspx" target="right" text="Purchase Order"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU03" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_GRList.aspx" target="right" text="Goods Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU04" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_GRNList.aspx" target="right" text="Goods Return"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU05" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_DAList.aspx" target="right" text="Dispatch Advice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU06" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_Pelimpahan.aspx" target="right" text="Pelimpahan"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc6" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblINHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblIN);">Inventory</A></td>
							</tr>
						</table>
						<table id="tblIN" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN01" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq.aspx" target="right" text="Purchase Requisition"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN02" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockReceive_List.aspx" target="right" text="Stock Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN03" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockRetAdv_List.aspx" target="right" text="Stock Return Advice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN04" runat="server" NavigateUrl="/en/IN/Trx/in_trx_StockAdj_list.aspx" target="right" text="Stock Adjustment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN05" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockTransfer_List.aspx" target="right" text="Stock Transfer"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN06" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockIssue_List.aspx" target="right" text="Stock Issue"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN07" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockReturn_List.aspx" target="right" text="Stock Return"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN08" runat="server" NavigateUrl="/en/IN/Trx/in_trx_FuelIssue_list.aspx" target="right" text="Fuel Issue"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN09" runat="server" NavigateUrl="/en/IN/Trx/in_trx_ItemToMachine_list.aspx" target="right" text="Assign Item To Machine"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc7" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblHRHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblHR);">Human Resource</A></td>
							</tr>
						</table>
						<table id="tblHR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR01" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empdet" target="right" text="Employee Details"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR02" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=emppay" target="right" text="Employee Payroll"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR03" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empemp" target="right" text="Employee Employment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR04" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empstat" target="right" text="Employee Statutory"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR05" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empfam" target="right" text="Employee Family"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR06" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empqlf" target="right" text="Employee Qualification"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR07" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empskill" target="right" text="Employee Skill"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkHR08" runat="server" NavigateUrl="/en/HR/trx/HR_Trx_EmployeeList.aspx?redirect=empcp" target="right" text="Career Progress"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc8" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblPRHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblPR);">Payroll</A></td>
							</tr>
						</table>
						<table id="tblPR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR01" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_PieceRatePayList.aspx" target="right" text="Harvester Production Payment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR02" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_ContractPayList.aspx" target="right" text="Contract Payment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR03" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_TripList.aspx" target="right" text="Trip"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR04" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WagesList.aspx"  target="right" text="Wages Payment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR05" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_ADList.aspx"  target="right" text="Allowance and Deduction"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR06" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WPList.aspx"  target="right" text="Work Performance"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR07" runat="server" NavigateUrl="/en/PR/Trx/PR_trx_DailyAttd.aspx"  target="right" text="Daily Attendance"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR08" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_PieceAttd.aspx"  target="right" text="Harvester Production Attendance"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR09" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WeeklyAttd.aspx"  target="right" text="Weekly Attendance"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR10" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_AttdList.aspx"  target="right" text="Attendance Checkroll"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR11" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_ContractCheckrollList.aspx"  target="right" text="Contractor Checkroll"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPR12" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WPContractorList.aspx"  target="right" text="Work Performance Contractor"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc9" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblProdHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblProd);">Production</A></td>
							</tr>
						</table>
						<table id="tblProd" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd01" runat="server" NavigateUrl="/en/PD/Trx/PD_Trx_EstProdList.aspx" target="right" text="Oil Palm"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd02" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_DailyProd_List.aspx" target="right" text="Daily Production"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd03" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_oilLoss_list.aspx" target="right" text="Oil Loss"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd04" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_oilQuality_list.aspx" target="right" text="Oil Quality"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd05" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_KernelQuality_list.aspx" target="right" text="Kernel Quality"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd06" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_ProducedKernelQuality_list.aspx" target="right" text="Produced Kernel Quality"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd07" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_DispatchedKernelQuality_list.aspx" target="right" text="Dispatched Kernel Quality"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd08" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_CPOStorage_List.aspx" target="right" text="CPO Storage"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd09" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_PKStorage_List.aspx" target="right" text="PK Storage"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd10" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_KernelLoss_List.aspx" target="right" text="Kernel Loss"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd11" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WaterQuality_List.aspx" target="right" text="Water Quality"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkProd12" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WasteWaterQuality_List.aspx" target="right" text="Wasted Water Quality"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc10" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblWMHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblWM);">Weighing</A></td>
							</tr>
						</table>
						<table id="tblWM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM01" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeighBridgeTicketList.aspx" target="right" text="WeighBridge Ticket"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM02" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_FFBAssessList.aspx" target="right" text="FFB Assessment"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc11" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblCMHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblCM);">Contract</A></td>
							</tr>
						</table>
						<table id="tblCM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM01" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractRegList.aspx"  target="right" text="Contract Registration"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM02" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractMatchList.aspx" target="right" text="Contract Matching"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM03" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_DORegistrationList.aspx" target="right" text="DO Registration"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc12" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblCTHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblCT);">Canteen</A></td>
							</tr>
						</table>
						<table id="tblCT" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT01" runat="server" NavigateUrl="/en/CT/trx/CT_PurReq.aspx" target="right" text="Purchase Requisition"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT02" runat="server" NavigateUrl="/en/CT/trx/CT_trx_CanteenReceiveList.aspx" target="right" text="Canteen Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT03" runat="server" NavigateUrl="/en/CT/trx/CT_trx_CRAList.aspx" target="right" text="Canteen Return Advice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT04" runat="server" NavigateUrl="/en/CT/trx/CT_trx_StockAdj_list.aspx" target="right" text="Canteen Adjustment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT05" runat="server" NavigateUrl="/en/CT/trx/CT_Trx_StockTransfer_List.aspx" target="right" text="Canteen Transfer"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT06" runat="server" NavigateUrl="/en/CT/trx/CT_Trx_StockIssue_List.aspx" target="right" text="Canteen Issue"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCT07" runat="server" NavigateUrl="/en/CT/trx/CT_Trx_StockReturn_List.aspx" target="right" text="Canteen Return"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc13" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblWSHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblWS);">Workshop</A></td>
							</tr>
						</table>
						<table id="tblWS" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWS01" runat="server" NavigateUrl="/en/WS/trx/ws_trx_job_list.aspx" target="right" text="Workshop Job"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWS02" runat="server" NavigateUrl="/en/WS/trx/ws_trx_mechanichour_list.aspx" target="right" text="Mechanic Hour"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc14" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblNUHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblNU);">Nursery</A></td>
							</tr>
						</table>
						<table id="tblNU" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU01" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_PurReq.aspx" target="right" text="Purchase Requisition"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU02" runat="server" NavigateUrl="/en/NU/Trx/NU_trx_SeedReceiveList.aspx"  target="right" text="Seeds Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU03" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedPlantList.aspx"  target="right" text="Seeds Planting"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU04" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_DoubleTurnList.aspx" target="right" text="Double Turns"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU05" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedTransPlantList.aspx"  target="right" text="Seedings Transplanting"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU06" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_CullList.aspx" target="right" text="Culling Transaction"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU07" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedDispatchList.aspx" target="right" text="Seedlings Dispatch"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU08" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedlingsIssue_list.aspx" target="right" text="Seedlings Issue"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc15" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblFAHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblFA);">Fixed Asset</A></td>
							</tr>
						</table>
						<table id="tblFA" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA01" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetAddList.aspx" target="right" text="Asset Addition"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA02" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetDeprList.aspx" target="right" text="Allow Depreciation"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA03" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetDispList.aspx" target="right" text="Allow Disposal"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA04" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetWOList.aspx" target="right" text="Asset Write Off"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc16" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="5">
								<td colSpan="2"><IMG src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblGrpStp" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="25">
								<td width="20" class="lb-ht"></td>
								<td width="3"></td>
								<td class="lb-ht"><a class="lb-ti">SETUP</a></td>
							</tr>
						</table>
						<table id="tblSpc17" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblStp1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblStpFI);">General Ledger</A></td>
							</tr>
						</table>
						<table id="tblStpFI" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFI01" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_AccCls.aspx" text="Kumpulan Kelas Akaun" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFI02" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_AccClsGrp.aspx" text="Kelas Akaun" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI03" runat="server" NavigateUrl="/en/GL/Setup/GL_Setup_ActivityGrp.aspx" target="right" text="Kumpulan Aktiviti"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI04" runat="server" NavigateUrl="/en/GL/Setup/GL_Setup_Activity.aspx" target="right" text="Aktiviti"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI05" runat="server" NavigateUrl="/en/GL/Setup/GL_Setup_SubActivity.aspx" target="right" text="Sub Activiti"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI06" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_ExpenseCode.aspx" text="Expense Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI07" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubGrpCode.aspx" text="Grup Biaya Kendaraan Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI08" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubCode.aspx" text="Biaya Kenderaan Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI09" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_Vehicle.aspx" text="Jenis Kenderaan"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI10" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleType.aspx" text="Kenderaan"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI11" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_BlockGrp.aspx" text="Divisi"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI12" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_Block.aspx" text="Tahun Tanam"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI13" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_SubBlock.aspx" text="Blok"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI14" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_ChartOfAccGrp.aspx" text="Group COA"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI15" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_ChartOfAcc.aspx" text="COA"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI16" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_DoubleEntry.aspx" text="Double Entry Setup"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI17" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_BalanceSheet.aspx" text="Balance Sheet"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI18" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_ProfitLoss.aspx" text="Profit and Loss"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI19" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_COGS.aspx" text="COGS Account Setup Page 1"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI20" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_COGS2.aspx" text="COGS Account Setup Page 2"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI21" runat="server" target="right" NavigateUrl="/en/GL/Setup/GL_Setup_FSList.aspx" text="Financial Statement Report Setup"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI22" runat="server" target="right" NavigateUrl="/en/BI/setup/BI_setup_BillPartyList.aspx" text="Bill Party"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI23" runat="server" target="right" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" text="Supplier"></asp:hyperlink></td>
							</tr>

						</table>
						<table id="tblSpc18" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp03" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpIN);">Inventory</A></td>
							</tr>
						</table>
						<table id="tlbStpIN" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN01" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_ProdType.aspx" text="Product Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN02" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_ProdBrand.aspx" text="Product Brand" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN03" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_ProdModel.aspx" text="Product Model" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN04" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_ProdCategory.aspx" text="Product Category" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN05" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_ProdMaterial.aspx" text="Product Material" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN06" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_StockAnalysis.aspx" text="Analisis Stock" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN07" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_StockMaster.aspx" text="Stock Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN08" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_StockItem.aspx" text="Stock Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN09" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_DirectMaster.aspx" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN10" runat="server" target="right" NavigateUrl="/en/IN/setup/IN_DirectCharge.aspx" text="Direct Charge Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc19" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp04" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpHR);">Human Resources</A></td>
							</tr>
						</table>
						<table id="tlbStpHR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR01" runat="server" NavigateUrl="/en/HR/setup/HR_setup_POH.aspx" target="right" text="Point Of Hired" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR02" runat="server" NavigateUrl="/en/HR/setup/HR_setup_DeptCode.aspx" target="right" text="Departement Code" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR03" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Deptlist.aspx" target="right" text="Departement" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR04" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Nationality.aspx" target="right" text="Nationality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR05" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Function.aspx" target="right" text="Function" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR06" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Position.aspx" target="right" text="Position" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR07" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Level.aspx" target="right" text="Level" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR08" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Religion.aspx" target="right" text="Religion" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR09" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ICType.aspx" target="right" text="IC Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR10" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Race.aspx" target="right" text="Race" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR11" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Skill.aspx" target="right" text="Skill" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR12" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ShiftList.aspx" target="right" text="Shift" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR13" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Qualification.aspx" target="right" text="Qualification" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR14" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Subject.aspx" target="right" text="Subject" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR15" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Evaluation.aspx" target="right" text="Evaluation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR16" runat="server" NavigateUrl="/en/HR/setup/HR_setup_CPlist.aspx" target="right" text="Riwayat Pekerjaan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR17" runat="server" NavigateUrl="/en/HR/setup/HR_setup_SalScheme.aspx" target="right" text="Employee Category" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR18" runat="server" NavigateUrl="/en/HR/setup/HR_setup_SalGradeList.aspx" target="right" text="Salary Grade" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR19" runat="server" NavigateUrl="/en/HR/setup/HR_setup_BankFormat.aspx" target="right" text="Bank Format" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR20" runat="server" NavigateUrl="/en/HR/setup/HR_setup_BankList.aspx" target="right" text="Bank" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR21" runat="server" NavigateUrl="/en/HR/setup/HR_setup_TaxBranchlist.aspx" target="right" text="Tax Branch" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR22" runat="server" NavigateUrl="/en/HR/setup/HR_setup_TaxList.aspx" target="right" text="Tax" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR23" runat="server" NavigateUrl="/en/HR/setup/HR_setup_JamsostekList.aspx" target="right" text="Jamsostek" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR24" runat="server" NavigateUrl="/en/HR/setup/HR_setup_GPH.aspx" target="right" text="Public Holiday" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR25" runat="server" NavigateUrl="/en/HR/setup/HR_setup_HSList.aspx" target="right" text="Holiday Schedule" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR26" runat="server" NavigateUrl="/en/HR/setup/HR_setup_GangList.aspx" target="right" text="Gang" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR27" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ContractorSuperList.aspx" target="right" text="Contrator Supervision" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc20" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp05" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpPR);">Payroll</A></td>
							</tr>
						</table>
						<table id="tlbStpPR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR01" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_ADGroup.aspx" target="right" text="Allowance And Deduction Group"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR02" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_ADList.aspx" target="right" text="Allowance And Deduction Code"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR03" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Load.aspx" target="right" text="Load" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR04" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Route.aspx" target="right" text="Route" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR05" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AttdList.aspx" target="right" text="Attendance Code" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR06" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AirBusList.aspx" target="right" text="Air Fare/Bus Ticket" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR07" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_DendaList.aspx" target="right" text="Denda" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR08" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_HarvIncList.aspx" target="right" text="Harvesting Incentive Scheme"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR09" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Contractor.aspx" target="right" text="Contrator" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR10" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_RiceRationList.aspx" target="right" text="Catu Beras" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR11" runat="server"  NavigateUrl="/en/PR/Setup/PR_Setup_IncentiveList.aspx" target="right" text="Premi Kerajinan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR12" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_MedicalList.aspx" target="right" text="Kesehatan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR13" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_MaternityList.aspx" target="right" text="Kehamilan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR14" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Payroll.aspx" target="right" text="Payroll Process" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR15" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_DanaPensiunList.aspx" target="right" text="Pensiunan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR16" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_RelocationList.aspx" target="right" text="Relocation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR17" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_EmpEvalList.aspx" target="right" text="Employee Evaluation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR18" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_StdEvalList.aspx" target="right" text="Standard Evaluation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR19" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_SalIncList.aspx" target="right" text="Salary Increase" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc21" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp06" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpFA);">Fixed Asset</A></td>
							</tr>
						</table>
						<table id="tlbStpFA" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA01" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetClass.aspx" target="right" text="Asset Clasification" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA02" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetGrp.aspx" target="right" text="Asset Group" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA03" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetReg.aspx" target="right" text="Asset Registration Header" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA04" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetReglnList.aspx" target="right" text="Asset Registration Line" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA05" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetPermitList.aspx" target="right" text="Asset Permission" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA06" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetMaster.aspx" target="right" text="Asset Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA07" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetItem.aspx" target="right" text="Asset Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc22" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp07" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpPD);">Production</A></td>
							</tr>
						</table>
						<table id="tlbStpPD" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD01" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx" target="right" text="Ullage - Volume Table Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD02" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeConversionMaster.aspx" target="right" text="Ullage - Volume Conversion Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD03" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageAverageCapacityConversionMaster.aspx" target="right" text="Ullage - Average Capacity Conversion Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD04" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_CPOPropertiesMaster.aspx" target="right" text="CPO Properties Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD05" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageTypeMaster.aspx" target="right" text="Storage Type Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD06" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageAreaMaster.aspx" target="right" text="Storage Area Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD07" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_ProcessingLineMaster.aspx" target="right" text="Processing Line Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD08" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineMaster.aspx" target="right" text="Machine Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD09" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableOilQuality.aspx"  target="right" text="Acceptable Oil Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD10" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableKernelQuality.aspx" target="right" text="Acceptable Kernel Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD11" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_TestSample.aspx" target="right" text="Test Sample" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD12" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_HarvestingInterval.aspx" target="right" text="Harvesting Interval" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD13" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineCriteria.aspx" target="right" text="Machine Criteria" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPD14" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_Mill.aspx" target="right" text="Mill" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc23" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp08" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpNU);">Nursery</A></td>
							</tr>
						</table>
						<table id="tlbStpNU" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU01" runat="server" NavigateUrl="/en/NU/setup/NU_setup_ItemMaster.aspx" target="right" text="Nursery Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU02" runat="server" NavigateUrl="/en/NU/setup/NU_setup_Item.aspx" target="right" text="Nursery Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU03" runat="server" NavigateUrl="/en/NU/setup/NU_setup_NUBatch.aspx" target="right" text="MS Nursery Batch" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU04" runat="server" NavigateUrl="/en/NU/setup/NU_setup_CullType.aspx" target="right" text="MS Culling Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU05" runat="server" NavigateUrl="/en/NU/setup/NU_setup_AccDist.aspx" target="right" text="MS Account Classification" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc24" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp09" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpWS);">Workshop</A></td>
							</tr>
						</table>
						
						<table id="tlbStpWS" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS01" runat="server" NavigateUrl="/en/WS/setup/WS_ProdType.aspx" target="right" text="Tipe Produk" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS02" runat="server" NavigateUrl="/en/WS/setup/WS_ProdBrand.aspx" target="right" text="Product Brand" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS03" runat="server" NavigateUrl="/en/WS/setup/WS_ProdModel.aspx" target="right" text="Product Model" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS04" runat="server" NavigateUrl="/en/WS/setup/WS_ProdCategory.aspx" target="right" text="Product Category" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS05" runat="server" NavigateUrl="/en/WS/setup/WS_ProdMaterial.aspx" target="right" text="Product Material" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS06" runat="server" NavigateUrl="/en/WS/setup/WS_StockAnalysis.aspx" target="right" text="Analisis Stok" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS07" runat="server" NavigateUrl="/en/WS/setup/WS_WorkCodeList.aspx" target="right" text="Pekerjaan Code" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS08" runat="server" NavigateUrl="/en/WS/setup/WS_ServTypeList.aspx" target="right" text="Service Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS09" runat="server" NavigateUrl="/en/WS/setup/WS_DirectCharge.aspx" target="right" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS10" runat="server" NavigateUrl="/en/WS/setup/WS_DirectMaster.aspx" target="right" text="Workshop Direct Charge Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS11" runat="server" NavigateUrl="/en/WS/setup/WS_ItemPart.aspx" target="right" text="Workshop Item Part" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS12" runat="server" NavigateUrl="/en/WS/setup/WS_MillProcDistr.aspx" target="right" text="Monthly Mill Process Distribution"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS13" runat="server" NavigateUrl="/en/WS/setup/WS_CalendarMachineList.aspx" target="right" text="Calendarized Machine" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>


						
						
						<table id="tblSpc50" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp15" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpCM);">Contract</A></td>
							</tr>
						</table>
						<table id="tlbStpCM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCM01" runat="server" NavigateUrl="/en/CM/setup/CM_Setup_CurrencyList.aspx" target="right" text="Currency" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCM02" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ExchangeRate.aspx" target="right" text="Exchange Rate" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCM03" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ContractQuality.aspx" target="right" text="Contract Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCM04" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ClaimQuality.aspx" target="right" text="Claim Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>


						<table id="tblSpc51" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tlbStp16" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpWM);">Weighing</A></td>
							</tr>
						</table>
						<table id="tlbStpWM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWM01" runat="server" NavigateUrl="/en/WM/setup/WM_setup_TransporterList.aspx" target="right" text="Transporter" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>

						
						<table id="tblSpc25" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="5">
								<td colSpan="2"><IMG src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblGrpRpt" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="25">
								<td width="20" class="lb-ht"></td>
								<td width="3"></td>
								<td class="lb-ht"><A class="lb-ti" href="javascript:togglebox(tlbRpt);">
										REPORT</A></td>
							</tr>
						</table>
						<table id="tlbRpt" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt1" runat="server" target="right" text="General Ledger" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt2" runat="server" target="right" text="Account Receivables" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt3" runat="server" target="right" text="Account Payables" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt4" runat="server" target="right" text="Cash And Bank" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt5" runat="server" target="right" text="Purchasing" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt6" runat="server" target="right" text="Inventory" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt7" runat="server" target="right" text="Human Resource" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt8" runat="server" target="right" text="Payroll" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt9" runat="server" target="right" text="Production" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt10" runat="server" target="right" text="Weighing" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt11" runat="server" target="right" text="Contract" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt12" runat="server" target="right" text="Canteen" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt13" runat="server" target="right" text="Workshop" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt14" runat="server" target="right" text="Nursery" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkRpt15" runat="server" target="right" text="Fixed Asset" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc26" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="5">
								<td colSpan="2"><IMG height="0" src="images/spacer.gif"  width="5" border="0"></td>
							</tr>
						</table>
						<table id="tblGrpMthEnd" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="25">
								<td width="20" class="lb-ht"></td>
								<td width="3"></td>
								<td class="lb-ht"><A class="lb-ti" href="javascript:togglebox(tblMth);">
										MONTH END</A></td>
							</tr>
						</table>
						<table id="tblMth" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth1" runat="server" target="right" text="Inventory"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth2" runat="server" target="right" text="Workshop"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth3" runat="server" target="right" text="Canteen"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth4" runat="server" target="right" text="Production"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth5" runat="server" target="right" text="Nursery"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth6" runat="server" target="right" text="Purchasing"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth7" runat="server" target="right" text="Fixed Asset"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth8" runat="server" target="right" text="Account Payables"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth9" runat="server" target="right" text="Account Receivables"  cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth10" runat="server" target="right" text="Cash And Bank" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth11" runat="server" target="right" text="Payroll" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkMth12" runat="server" target="right" text="General Ledger" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc27" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="5">
								<td colSpan="2"><IMG height="0" src="images/spacer.gif"  width="5" border="0"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr height="25">
								<td width="20" class="lb-ht"></td>
								<td width="3"></td>
								<td class="lb-ht"><A class="lb-ti" href="javascript:togglebox(tblAdmin);">
										CONFIGURATION</A></td>
							</tr>
						</table>
						<table id="tblAdmin" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAdminSetup" runat="server" target="right" text="Setup"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAdminDT" runat="server" target="right" text="Data Transfer"></asp:hyperlink></td>
							</tr>
						</table>
						<table id="tblSpc28" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="5">
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5"  border="0"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr height="25">
								<td width="20" class="lb-ht"></td>
								<td width="3"></td>
								<td class="lb-ht"><A class="lb-ti" href="javascript:togglebox(tblOther);">
										OTHERS</A></td>
							
							</tr>
						</table>
						<table id="tblOther" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkChgPwd" runat="server" target="left" text="Change Password"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkLocation" navigateurl = "/en/system/user/setlocation.aspx" runat="server" target="right" text="Location Setting"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="9"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkLogOut" navigateurl="/logout.aspx" runat="server" target="right"
									text="Log Out"></asp:hyperlink></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
