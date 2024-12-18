<%@ Page Language="vb" src="../include/menu_mmtrx.aspx.vb" Inherits="menu_mmtrx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
<table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 
			<tr>
				<td valign="top">
				    <button class="accordion">Data Collection</button>					
					<div class="panel">
                        <table id="tblDATAHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="Hyperlink2" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq_MTR.aspx" target="middleFrame" text="Requisition Monitor"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="Hyperlink3" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_PO_MTR.aspx" 
                                    target="middleFrame" text="Purchasing Monitor"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:HyperLink ID="lnkStpINRequest" runat="server" 
                                    CssClass="lb-mt" NavigateUrl="/en/IN/setup/IN_StockMaster_Request.aspx"
                                        Target="middleFrame" Text="Request Stock Master"></asp:HyperLink></div></a></td>
							</tr>

							<tr>
								<td><a href="#"><div class="childmenu"><asp:HyperLink ID="HyperLink4" runat="server" CssClass="lb-mt" NavigateUrl="/en/IN/setup/IN_StockMaster_View.aspx"
                                        Target="middleFrame" Text="Find Stock Master"></asp:HyperLink>
                                    </div></a></td>
							</tr>

                            <tr>
								<td><a href="#"><div class="childmenu"><asp:HyperLink ID="HyperLink5" runat="server" CssClass="lb-mt" NavigateUrl="/en/IN/setup/IN_StockItem_View.aspx"
                                        Target="middleFrame" Text="Find Stock Item"></asp:HyperLink> 
                                    </div></a></td>
							</tr>

							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU07" runat="server" 
                                    NavigateUrl="/en/PU/Trx/PU_trx_HistoricalPrices.aspx" target="middleFrame" text="Historical Prices"></asp:hyperlink>
                                    </div></a></td>
							</tr>

							<tr>
								<td><a href="#"><div class="childmenu"><asp:HyperLink ID="HyperLink8" runat="server" CssClass="lb-mt" NavigateUrl="/en/IN/Trx/IN_trx_StockPosition_list.aspx?type=IN" 
                                        Target="middleFrame" Text="Posisi Stock"></asp:HyperLink>
                                    </div></a></td>
							</tr>

						</table>
					</div>
					<button class="accordion">Inventory</button>					
					<div class="panel">
                        <table id="tblIN" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN01" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq.aspx" target="middleFrame" text="Purchase Requisition"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN01_APP" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq_APP.aspx" target="middleFrame" text="Requisition Approval"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN01_MTR" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq_MTR.aspx" target="middleFrame" text="Requisition Monitor"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN02" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockReceive_List.aspx" target="middleFrame" text="Stock Receive"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN03" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockRetAdv_List.aspx" target="middleFrame" text="Stock Return Advice"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN04" runat="server" NavigateUrl="/en/IN/Trx/in_trx_StockAdj_list.aspx" target="middleFrame" text="Stock Adjustment"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN05" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockTransfer_List.aspx" target="middleFrame" text="Stock Transfer"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN05A" runat="server" NavigateUrl="/en/IN/Trx/IN_trx_StockTransferDIV_list.aspx" target="middleFrame" text="Stock Transfer Divisi"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN06" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockIssue_List.aspx?type=IN" target="middleFrame" text="Stock Issue"></asp:hyperlink></div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN07" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockReturn_List.aspx" target="middleFrame" text="Stock Return"></asp:hyperlink></div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN08" runat="server" NavigateUrl="/en/IN/Trx/in_trx_FuelIssue_list.aspx" target="middleFrame" text="Fuel Issue"></asp:hyperlink></div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN09" runat="server" NavigateUrl="/en/IN/Trx/in_trx_ItemToMachine_list.aspx" target="middleFrame" text="Assign Item To Machine"></asp:hyperlink></div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkIN10" runat="server" NavigateUrl="/en/IN/Trx/in_trx_StockTransferInternal_list.aspx" target="middleFrame" text="Internal Stock Transfer"></asp:hyperlink></div></a></td>
							</tr>
						</table>
					</div>
					<button class="accordion">Purchasing</button>
					<div class="panel">
                        <table id="tblPU" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU21" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_PelimpahanGM.aspx" target="middleFrame" text="Disposition By Location"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="Hyperlink1" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_PelimpahanMgr.aspx" target="middleFrame" text="Disposition By User"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU06" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_PelimpahanUser.aspx" target="middleFrame" text="PR Assign To User"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU12" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_RFQ_List.aspx" target="middleFrame" text="RFQ"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU01" runat="server" NavigateUrl="/en/PU/Trx/pu_trx_RPHList.aspx" target="middleFrame" text="Quotation (DTH)"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU02" runat="server" NavigateUrl="/en/PU/Trx/pu_trx_POlist.aspx" target="middleFrame" text="Purchase Order"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU03" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_GRList.aspx" target="middleFrame" text="Goods Receive"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU04" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_GRNList.aspx" target="middleFrame" text="Goods Return"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkPU05" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_DAList.aspx" target="middleFrame" text="Dispatch Advice"></asp:hyperlink></div></a></td>
							</tr>
						</table>
					</div>

                <div style="position:absolute; top:0px; width:86%; left:125px; height:1500px" >          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 80px;"
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

