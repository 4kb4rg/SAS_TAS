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
         
           <div id="Nav" style="position:absolute; width:177px; top:0px; left:0px; height:500px">
            	
            <table>
			    <tr height="20">
			    <td width="20"></td>
			</tr>
			</table> 

			     		<table id="tblESHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
						<tr height="20">
						<td width="20"></td>
						<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
						<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblES);">Estate</A></td>
						</tr>
			    		</table>

					<table id="tblES" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkES01" runat="server" NavigateUrl="/en/PD/Trx/PD_Trx_EstProdList.aspx" target="middleFrame" text="Oil Palm Yield"></asp:hyperlink></td>
						</tr>

					</table>
						
					<table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr>
							<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
						</tr>
					</table>			
						
                        		<table id="tblMLHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblML);">Mill</A></td>
						</tr>
					</table>
					<table id="tblML" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">

						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML01" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_DailyProd_List.aspx" target="middleFrame" text="Daily Production"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML02" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_oilLoss_list.aspx" target="middleFrame" text="Oil Loss"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML03" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_oilQuality_list.aspx" target="middleFrame" text="Oil Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML04" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_KernelQuality_list.aspx" target="middleFrame" text="Kernel Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML05" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_ProducedKernelQuality_list.aspx" target="middleFrame" text="Produced Kernel Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML06" runat="server" NavigateUrl="/en/PM/Trx/pm_trx_DispatchedKernelQuality_list.aspx" target="middleFrame" text="Dispatched Kernel Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML07" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_CPOStorage_List.aspx" target="middleFrame" text="CPO Storage"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML08" runat="server" NavigateUrl="/en/PM/Trx/PM_TRX_PKStorage_List.aspx" target="middleFrame" text="PK Storage"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML09" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_KernelLoss_List.aspx" target="middleFrame" text="Kernel Loss"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML10" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WaterQuality_List.aspx" target="middleFrame" text="Water Quality"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkML11" runat="server" NavigateUrl="/en/PM/Trx/PM_trx_WasteWaterQuality_List.aspx" target="middleFrame" text="Wasted Water Quality"></asp:hyperlink></td>
						</tr>
							
					</table>

						




            </div>
         
             <div style="position:absolute; top:0px; width:835px; left:179px; height:500px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="yes" ></iframe>
             
               </div>
            
           </td>
           <div class="BackgroundTopCorner"></div>
          </form>
           
    </form>
</body>
</html>

