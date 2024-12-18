<%@ Page Language="vb" src="../include/menu_sdtrx.aspx.vb" Inherits="menu_sdtrx" %>

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
                    <div class="panel">
                        <table id="tblCMHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

				    <button class="accordion">Contract Management</button>					
					<div class="panel">
                        <table id="tblCM"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCM1" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractRegList.aspx"  target="middleFrame" text="Contract Registration"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkCM2" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractMatchList.aspx" target="middleFrame" text="Contract Matching"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkCM3" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_DORegistrationList.aspx" target="middleFrame" text="DO Registration"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="Hyperlink5" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_SPKDOList.aspx" target="middleFrame" text="SPK-DO"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="Hyperlink6" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_Contract_Monitor.aspx" target="middleFrame" text="Contract Monitoring"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="Hyperlink4" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_FFBContractRegList.aspx" target="middleFrame" text="FFB Contract Registration"></asp:hyperlink></div></a></td>
							</tr>
						</table>
					</div>
 
					<div class="panel">
                        <table id="tblSpc1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

                    <div class="panel">
                        <table id="tblWMHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

                    <button class="accordion">Weighing Management</button>					
					<div class="panel">
                        <table id="tblWM"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkWM1" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_TicketList.aspx" target="middleFrame" text="WeighBridge Ticket"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="Hyperlink1" runat="server" NavigateUrl="/en/WM/trx/WM_WM_Trx_WeighBridgeTicketList.aspx" target="middleFrame" text="Calculate Ticket FFB"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="Hyperlink3" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeighBridgeTicketList.aspx" target="middleFrame" text="Edit Ticket Sales"></asp:hyperlink></div></a></td>
							</tr>
							<tr visible=false>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="Hyperlink2" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeightBridge_Edited.aspx" target="middleFrame" text="WeighBridge Edit"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkWM2" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_TicketPriceList.aspx" target="middleFrame" text="Ticket Price"></asp:hyperlink></div></a></td>
							</tr>
                            <tr visible=false>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkWM3" runat="server" NavigateUrl="/en/WM/data/WM_data_uploadweigh.aspx" target="middleFrame" text="Weighing Upload"></asp:hyperlink></a></td>
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

	    <table id="tblCMHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
			<tr height="20">
			<td width="20"></td>
			<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
			<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblCM);">Contract Management</A></td>
			</tr>
	    </table>

	    <table id="tblCM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0" runat="server">
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM1" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractRegList.aspx"  target="middleFrame" text="Contract Registration"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM2" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractMatchList.aspx" target="middleFrame" text="Contract Matching"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM3" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_DORegistrationList.aspx" target="middleFrame" text="DO Registration"></asp:hyperlink></td>
			</tr>
	  </table>

	  <table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
	    <tr>
		    <td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
	    </tr>
	  </table>


	   <table id="tblWMHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
		    <tr height="20">
		    <td width="20"></td>
		    <td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
		    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblWM);">Weighing Management</A></td>
		    </tr>
	    </table>

	    <table id="tblWM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0" runat="server" >
 		    <tr height="20">
			    <td width="20"></td>
			    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
			    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM1" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeighBridgeTicketList.aspx" target="middleFrame" text="WeighBridge Ticket"></asp:hyperlink></td>
		    </tr>	
	    <tr height="20">
			    <td width="20"></td>
			    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
			    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink1" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeightBridge_Edited.aspx" target="middleFrame" text="WeighBridge Edit"></asp:hyperlink></td>
		    </tr>		    
		    <tr height="20">
			    <td width="20"></td>
			    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
			    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM2" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_FFBAssessList.aspx" target="middleFrame" text="FFB Assessment"></asp:hyperlink></td>
		    </tr>
			<tr height="20">
			    <td width="20"></td>
			    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
			    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM3" runat="server" NavigateUrl="/en/WM/data/WM_data_uploadweigh.aspx" target="middleFrame" text="Weighing Upload"></asp:hyperlink></td>
		    </tr>
			
	    </table>

     
      </div>
         
     <div style="position:absolute; top:0px; width:85%; left:179px; height:1000px" >
      	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
		 scrolling="auto" src="black.aspx"></iframe>
      </div>
    
     <div class="BackgroundTopCorner"></div>--%>
    </form>
 
</body>
</html>

