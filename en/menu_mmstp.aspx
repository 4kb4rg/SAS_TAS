<%@ Page Language="vb" src="../include/menu_mmstp.aspx.vb" Inherits="menu_mmstp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
     <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />

</head>
    <body bgcolor="white" style="margin: 0">
    <form id="form1" runat="server">

<table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 
			<tr>
				<td valign="top">
				    <button class="accordion">Inventory</button>					
					<div class="panel">
                        <table id="tlbStpIN"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                               <asp:hyperlink id="lnkStpIN01" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdType.aspx" text="Product Type" ></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpIN02" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdBrand.aspx" text="Product Brand" ></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN03" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdModel.aspx" text="Product Model"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN04" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdCategory.aspx" text="Product Category" ></asp:hyperlink>
                                    </div></a></td>
							</tr>

                            <tr>
								<td ><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN05" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdMaterial.aspx" text="Product Material" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td ><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN06" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockAnalysis.aspx" text="Analisis Stock" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr >
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN07" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockMaster.aspx" text="Stock Master" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN08" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockItem.aspx" text="Stock Item" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN09" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_DirectMaster.aspx" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN10" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_DirectCharge.aspx" text="Direct Charge Item" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN11" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_setup_UserApprovalDet.aspx" text="User Level Approval" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>

						</table>
					</div>
                  <button class="accordion">Purchasing</button>					
					<div class="panel">
                        <table id="tlbStpPU" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkStpPU01" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" text="Supplier"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU02" runat="server" NavigateUrl="/en/CM/setup/CM_Setup_CurrencyList.aspx" target="middleFrame" text="Currency"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU03" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ExchangeRate.aspx" target="middleFrame" text="Exchange Rate"></asp:hyperlink></div></a></td>
							</tr>
			  			    <tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU08" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_UserGroupList.aspx" text="User Group Item"></asp:hyperlink></div></a></td>
							    </tr>
				            <tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU09" runat="server" NavigateUrl="/en/PU/setup/PU_Setup_LocMGr.aspx" target="middleFrame" text="Purchasing Manager"></asp:hyperlink></div></a></td>
					        </tr>
                        </table>
                    </div>

                <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 80px;"
				        scrolling="auto" src="black.aspx"  ></iframe>
             
               </div>

                </td>
            </tr>
        </table>
    </tr>
</table>
      
         
        </form>
           

</body>
</html>

