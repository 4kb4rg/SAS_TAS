<%@ Page Language="vb" src="../include/menu_fitrx.aspx.vb" Inherits="menu_fitrx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
<body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server" >
         
               <table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 
			<tr>
				<td valign="top">
                    <div class="panel">
                        <table id="tblGLHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
    
				    <button class="accordion">Financial Management</button>					
					<div class="panel">
                        <table id="tblGL"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRpt1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
	                        <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRpt2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRpt3" runat="server" target="middleFrame"  text="Account Receivable" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRpt4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRpt5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>
                    <div class="panel">
                        <table id="tblSpc1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <div class="panel">
                        <table id="tblAPHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>

                    <button class="accordion">Account Payable</button>					
					<div class="panel">
                        <table id="tblAP"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkAP06" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_invrcvNotelist.aspx" target="middleFrame" text="Invoice Reception"></asp:hyperlink>
                                </div></a></td>
							</tr>
	                        <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkAP01" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_InvRcvList.aspx" target="middleFrame" text="Credited Invoice"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkAP02" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_DNList.aspx" target="middleFrame" text="Supplier Debit Note"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkAP03" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_CNList.aspx"  target="middleFrame" text="Supplier Credit Note"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkAP04" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_CJList.aspx" target="middleFrame" text="Creditor Journal"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkAP05" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_invrcv_wm_list.aspx" target="middleFrame" text="Weighing Credit Invoice"></asp:hyperlink>
                                </div></a></td>
							</tr>

						</table>
					</div>

                    <div class="panel">
                        <table id="tblSpc2"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <div class="panel">
                        <table id="tblBillHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <button class="accordion">Account 
							Receivable</button>		
                    <div class="panel">
                        <table id="tblBill"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkBill01" NavigateUrl="/en/BI/Trx/BI_Trx_InvoiceList.aspx" runat="server" target="middleFrame" text="Invoice"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkBill02" runat="server" NavigateUrl="/en/BI/Trx/BI_Trx_DNList.aspx" target="middleFrame" text="Debit Note"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkBill03" runat="server" NavigateUrl="/en/BI/Trx/BI_Trx_CNList.aspx" target="middleFrame" text="Credit Note"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkBill04" runat="server" NavigateUrl="/en/BI/Trx/BI_trx_JournalList.aspx" target="middleFrame" text="Debtor Journal"></asp:hyperlink>
                                </div></a></td>
							</tr>


						</table>
					</div>

                    <div class="panel">
                        <table id="tblSpc3"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <div class="panel">
                        <table id="tblCBHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>

                    <button class="accordion">Cash Bank Management</button>		
                    <div class="panel">
                        <table id="tblCB"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCB01" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_PayList.aspx" target="middleFrame" text="Payment"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCB02" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_ReceiptList.aspx" target="middleFrame" text="Received"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCB03" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_CashBankList.aspx" target="middleFrame" text="Cash Bank"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCB04" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_RekonsileList.aspx" target="middleFrame" text="Bank Rekonciliation"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCB05" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_InterestAdjList.aspx" target="middleFrame" text="Interest Adjustment"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCB06" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_WithdrawalList.aspx" target="middleFrame" text="Withdrawal"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>

                    <div class="panel">
                        <table id="tblSpc4"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <div class="panel">
                        <table id="tblFAHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <button class="accordion">Fixed Asset</button>		
                    <div class="panel">
                        <table id="Table1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkFA01" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetAddList.aspx" target="middleFrame" text="Asset Addition"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkFA02" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetDeprList.aspx" target="middleFrame" text="Allow Depreciation"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkFA03" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetDispList.aspx" target="middleFrame" text="Allow Disposal"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkFA04" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetWOList.aspx" target="middleFrame" text="Asset Write Off"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>


              
        <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 109px;"
				        scrolling="auto" src="black.aspx"  ></iframe>
             
               </div>
<%--
                    <button class="accordion">Testing</button>
					<div class="panel">
						<table cellpadding="0" cellspacing="1" style="width: 254px">
							<tr>
								<td><a href="#"><div class="fathermenu">Data Collection</div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="fathermenu">Processing</div></a></td>
							</tr>
							 
						</table>
					</div>--%>
					
					<script>
					    var acc = document.getElementsByClassName("accordion");
					    var i;


					    for (i = 0; i < acc.length; i++) {
					        acc[i].onclick = function () {
					            this.classList.toggle("active");
					            this.nextElementSibling.classList.toggle("hide");
					        }
					    }
					</script>				

				</td>
			</tr>
		</table>

		</td>
	</tr>
</table>
           
    </form>
</body>
</html>

