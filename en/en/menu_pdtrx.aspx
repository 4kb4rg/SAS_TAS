<%@ Page Language="vb" src="../include/menu_pdtrx.aspx.vb" Inherits="menu_pdtrx" %>

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
								
						
                        		


            </div>
         
             <div style="position:absolute; top:0px; width:85%; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
           <div class="BackgroundTopCorner"></div>
          </form>

</body>
</html>

