<%@ Page Language="vb" src="../include/menu_pdtrx.aspx.vb" Inherits="menu_pdtrx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
				    <button class="accordion">Production</button>					
					<div class="panel">
                        <table id="tblProd" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
	 
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD2" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_DailyProd_List.aspx" target="middleFrame" text="Daily Production"></asp:hyperlink>
                                <asp:hyperlink class="lb-mt" id="lnkPD1" runat="server" NavigateUrl="/en/PD/Trx/PD_Trx_EstProdList.aspx" target="middleFrame" Visible="false" text="Oil Palm Yield"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD3" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_oilLoss_list.aspx" target="middleFrame" text="Oil & Kernel Loss"></asp:hyperlink>
                                </div></a></td>
							</tr>
							 
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD7" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_DispatchedKernelQuality_list.aspx" target="middleFrame" text="Dispatched Product Quality"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD8" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_CPOStorage_List.aspx" target="middleFrame" text="CPO Storage & Quality"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD9" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_PKStorage_List.aspx" target="middleFrame" text="Kernel Storage & Quality"></asp:hyperlink>
                                <asp:hyperlink class="lb-mt" id="lnkPD10" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_KernelLoss_List.aspx" target="middleFrame" Visible="false"  text="Kernel Loss"></asp:hyperlink>
                                <asp:hyperlink class="lb-mt" id="lnkPD11" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WaterQuality_List_KLR.aspx" target="middleFrame" Visible="false" text="Water Quality"></asp:hyperlink>
                                <asp:hyperlink class="lb-mt" id="lnkPD12" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WasteWaterQuality_List.aspx" target="middleFrame" Visible="false" text="Wasted Water Quality"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD13" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_ProdSummary.aspx" target="middleFrame" text="Production Summary"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD14" runat="server" NavigateUrl="/en/PD/Trx/PD_trx_CurahHujan_Estate.aspx" target="middleFrame" text="Curah Hujan"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkPD15" runat="server" NavigateUrl="/en/PD/Trx/PD_trx_ProdList_Estate.aspx" target="middleFrame" text="Pengiriman TBS"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>
		
        <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
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



<%-- 
           <div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
            <table>
			    <tr height="20">
			    <td width="20"></td>
			</tr>
			</table> 


					<table id="tblProd" style= "position:absolute" cellspacing="0" cellpadding="0" runat="server" >
							
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD1" runat="server" NavigateUrl="/en/PD/Trx/PD_Trx_EstProdList.aspx" target="middleFrame" text="Oil Palm Yield"></asp:hyperlink></td>
						</tr>

						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD2" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_DailyProd_List.aspx" target="middleFrame" text="Daily Production"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD3" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_oilLoss_list.aspx" target="middleFrame" text="Oil Loss"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD4" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_OilQuality_List_KLR.aspx" target="middleFrame" text="Oil Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD5" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_KernelQuality_list.aspx" target="middleFrame" text="Kernel Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD6" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_ProducedKernelQuality_list.aspx" target="middleFrame" text="Produced Kernel Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD7" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_DispatchedKernelQuality_list.aspx" target="middleFrame" text="Dispatched Kernel Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD8" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_CPOStorage_List.aspx" target="middleFrame" text="CPO Storage"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD9" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_PKStorage_List.aspx" target="middleFrame" text="PK Storage"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD10" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_KernelLoss_List.aspx" target="middleFrame" text="Kernel Loss"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD11" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WaterQuality_List_KLR.aspx" target="middleFrame" text="Water Quality"></asp:hyperlink></td>
						</tr>
						
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD12" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WasteWaterQuality_List.aspx" target="middleFrame" text="Wasted Water Quality"></asp:hyperlink></td>
						</tr>

						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD13" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_ProdSummary.aspx" target="middleFrame" text="Production Summary"></asp:hyperlink></td>
						</tr>

						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD14" runat="server" NavigateUrl="/en/PD/Trx/PD_trx_CurahHujan_Estate.aspx" target="middleFrame" text="Curah Hujan"></asp:hyperlink></td>
						</tr>

						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkPD15" runat="server" NavigateUrl="/en/PD/Trx/PD_trx_ProdList_Estate.aspx" target="middleFrame" text="Pengiriman TBS"></asp:hyperlink></td>
						</tr>

					</table>
								
						
                        		


            </div>--%>
         
   
            
 
          </form>

</body>
</html>

