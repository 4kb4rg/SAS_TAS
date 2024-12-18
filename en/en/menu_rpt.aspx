<%@ Page Language="vb" src="../include/menu_fitrx.aspx.vb" Inherits="menu_fitrx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/MenuStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
<body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server" >
         
           <div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
            <table>
			    <tr height="20">
			        <td width="20"></td>
			    </tr>
			</table> 

		    <table id="tblGLHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblGL);">Financial Management</A></td>
			    </tr>
			</table>
			<table id="tblGL" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRpt1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRpt2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRpt3" runat="server" target="middleFrame"  text="Account Receivable" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRpt4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRpt5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			

			<table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>

			<table id="tblAPHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblAP);">Account Payable</A></td>
				</tr>
			</table>
			<table id="tblAP" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP06" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_invrcvNotelist.aspx" target="middleFrame" text="Invoice Reception"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP01" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_InvRcvList.aspx" target="middleFrame" text="Credited Invoice"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP02" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_DNList.aspx" target="middleFrame" text="Supplier Debit Note"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP03" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_CNList.aspx"  target="middleFrame" text="Supplier Credit Note"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP04" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_CJList.aspx" target="middleFrame" text="Creditor Journal"></asp:hyperlink></td>
				</tr>
                <tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAP05" runat="server" NavigateUrl="/en/AP/Trx/ap_trx_invrcv_wm_list.aspx" target="middleFrame" text="Weighing Credit Invoice"></asp:hyperlink></td>
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
					<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblBill);">Account 
							Receivable</A></td>
				</tr>
			</table>
			<table id="tblBill" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill01" NavigateUrl="/en/BI/Trx/BI_Trx_InvoiceList.aspx" runat="server" target="middleFrame" text="Invoice"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill02" runat="server" NavigateUrl="/en/BI/Trx/BI_Trx_DNList.aspx" target="middleFrame" text="Debit Note"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill03" runat="server" NavigateUrl="/en/BI/Trx/BI_Trx_CNList.aspx" target="middleFrame" text="Credit Note"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkBill04" runat="server" NavigateUrl="/en/BI/Trx/BI_trx_JournalList.aspx" target="middleFrame" text="Debtor Journal"></asp:hyperlink></td>
				</tr>
			</table>

			<table id="tblSpc3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>

			<table id="tblCBHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblCB);">Cash Bank Management</A></td>
				</tr>
			</table>
			<table id="tblCB" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB01" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_PayList.aspx" target="middleFrame" text="Payment"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB02" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_ReceiptList.aspx" target="middleFrame" text="Received"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB03" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_CashBankList.aspx" target="middleFrame" text="Cash Bank"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB04" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_RekonsileList.aspx" target="middleFrame" text="Bank Rekonciliation"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB05" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_InterestAdjList.aspx" target="middleFrame" text="Interest Adjustment"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCB06" runat="server" NavigateUrl="/en/CB/Trx/cb_trx_WithdrawalList.aspx" target="middleFrame" text="Withdrawal"></asp:hyperlink></td>
				</tr>
			</table>

			<table id="tblSpc4" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>

			<table id="tblFAHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr height="20" >
					<td width="20"></td>
					<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblFA);">Fixed Asset</A></td>
				</tr>
			</table>
            <table id="tblFA" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
                width="100%" border="0" runat="server">
                <tr height="20">
                    <td width="20"></td>
                    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
                    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA01" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetAddList.aspx" target="middleFrame" text="Asset Addition"></asp:hyperlink></td>
                </tr>
                <tr height="20">
                    <td width="20"></td>
                    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
                    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA02" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetDeprList.aspx" target="middleFrame" text="Allow Depreciation"></asp:hyperlink></td>
                </tr>
                <tr height="20">
                    <td width="20"></td>
                    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
                    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA03" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetDispList.aspx" target="middleFrame" text="Allow Disposal"></asp:hyperlink></td>
                </tr>
                <tr height="20">
                    <td width="20"></td>
                    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
                    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkFA04" runat="server" NavigateUrl="/en/FA/trx/FA_trx_AssetWOList.aspx" target="middleFrame" text="Asset Write Off"></asp:hyperlink></td>
                </tr>
            </table>



            </div>
         
             <div style="position:absolute; top:0px; width:85%; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
           </td>
           <div class="BackgroundTopCorner"></div>
          </form>
           
    </form>
</body>
</html>

