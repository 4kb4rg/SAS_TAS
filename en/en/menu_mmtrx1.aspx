<%@ Page Language="vb" src="../include/menu_mmtrx.aspx.vb" Inherits="menu_mmtrx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/MenuStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
<body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server">
         
           <div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
            <table>
			    <tr height="20">
			    <td width="20"></td>
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
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN01" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq.aspx" target="middleFrame" text="Purchase Requisition"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN01_APP" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq_APP.aspx" target="middleFrame" text="Requisition Approve"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN01_REV" runat="server" NavigateUrl="/en/IN/Trx/IN_PurReq_REV.aspx" target="middleFrame" text="Requisition Review"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN02" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockReceive_List.aspx" target="middleFrame" text="Stock Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN03" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockRetAdv_List.aspx" target="middleFrame" text="Stock Return Advice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN04" runat="server" NavigateUrl="/en/IN/Trx/in_trx_StockAdj_list.aspx" target="middleFrame" text="Stock Adjustment"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN05" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockTransfer_List.aspx" target="middleFrame" text="Stock Transfer"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN05A" runat="server" NavigateUrl="/en/IN/Trx/IN_trx_StockTransferDIV_list.aspx" target="middleFrame" text="Stock Transfer Divisi"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN06" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockIssue_List.aspx?type=IN" target="middleFrame" text="Stock Issue"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN07" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockReturn_List.aspx" target="middleFrame" text="Stock Return"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN08" runat="server" NavigateUrl="/en/IN/Trx/in_trx_FuelIssue_list.aspx" target="middleFrame" text="Fuel Issue"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN09" runat="server" NavigateUrl="/en/IN/Trx/in_trx_ItemToMachine_list.aspx" target="middleFrame" text="Assign Item To Machine"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN10" runat="server" NavigateUrl="/en/IN/Trx/in_trx_StockTransferInternal_list.aspx" target="middleFrame" text="Internal Stock Transfer"></asp:hyperlink></td>
							</tr>
						</table>
						
						<table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
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
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU21" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_PelimpahanGM.aspx" target="middleFrame" text="Disposition By Location"></asp:hyperlink></td>
							</tr>
						    <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink1" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_PelimpahanMgr.aspx" target="middleFrame" text="Disposition By User"></asp:hyperlink></td>
							</tr>


							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU12" runat="server" NavigateUrl="/en/PU/Trx/PU_Trx_RFQ_List.aspx" target="middleFrame" text="RFQ"></asp:hyperlink></td>
							</tr>


							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU01" runat="server" NavigateUrl="/en/PU/Trx/pu_trx_RPHList.aspx" target="middleFrame" text="Quotation (RPH)"></asp:hyperlink></td>
							</tr>


							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU02" runat="server" NavigateUrl="/en/PU/Trx/pu_trx_POlist.aspx" target="middleFrame" text="Purchase Order"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU03" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_GRList.aspx" target="middleFrame" text="Goods Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU04" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_GRNList.aspx" target="middleFrame" text="Goods Return"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU05" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_DAList.aspx" target="middleFrame" text="Dispatch Advice"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU06" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_Pelimpahan.aspx" target="middleFrame" text="Pelimpahan"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPU07" runat="server" NavigateUrl="/en/PU/Trx/PU_trx_HistoricalPrices.aspx" target="middleFrame" text="Historical Prices"></asp:hyperlink></td>
							</tr>
						</table>




            </div>
         
             <div style="position:absolute; top:0px; width:89%; left:150px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
           </td>
           <div class="BackgroundTopCorner"></div>
          </form>
           
    </form>
</body>
</html>

