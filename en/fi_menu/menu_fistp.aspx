<%@ Page Language="vb" src="../include/menu_mmstp.aspx.vb" Inherits="menu_mmstp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/MenuStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
    <body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server">
         
           <div id="Nav" style="position:absolute; width:177px; top:0px; left:0px; height:500px">
            	
            <table>
			    <tr height="20">
			    <td width="20"></td>
			   </tr>
			</table> 

                        <table id="tlbStp03" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpIN);">Inventory</A></td>
						</tr>
						</table>
						<table id="tlbStpIN" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN01" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdType.aspx" text="Product Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN02" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdBrand.aspx" text="Product Brand" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN03" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdModel.aspx" text="Product Model" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN04" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdCategory.aspx" text="Product Category" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN05" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdMaterial.aspx" text="Product Material" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN06" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockAnalysis.aspx" text="Analisis Stock" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN07" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockMaster.aspx" text="Stock Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN08" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockItem.aspx" text="Stock Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN09" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_DirectMaster.aspx" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpIN10" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_DirectCharge.aspx" text="Direct Charge Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						
						<table id="tblSpc18" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						
						
						<table id="tblStp1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblStpFI);">Purchasing</A></td>
							</tr>
						</table>
						<table id="tblStpFI" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI23" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" text="Supplier"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCM01" runat="server" NavigateUrl="/en/CM/setup/CM_Setup_CurrencyList.aspx" target="middleFrame" text="Currency" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCM02" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ExchangeRate.aspx" target="middleFrame" text="Exchange Rate" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>

						</table>
						



            </div>
         
             <div style="position:absolute; top:0px; width:835px; left:179px; height:500px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="yes" ></iframe>
             
               </div>
            
         
           <div class="BackgroundTopCorner"></div>
        </form>
           

</body>
</html>

