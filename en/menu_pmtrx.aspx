<%@ Page Language="vb" src="../include/menu_pmtrx.aspx.vb" Inherits="menu_pmtrx" %>

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
				    <button class="accordion">Vehicle & Workshop</button>					
					<div class="panel">
                        <table id="tblUPHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>
                    <div class="panel">
                        <table id="tblUP"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkUP01" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WPList.aspx"  target="middleFrame" text="Work Performance"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkUP02" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WPContractorList.aspx"  target="middleFrame" text="Work Performance Contractor">
                                </asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink  id="lnkUP03" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_VehicleUsage_list.aspx" target="middleFrame" text="Vehicle Usage">
                                </asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink  id="lnkUP04" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_Trx_Workshop_List.aspx" target="middleFrame" text="Workshop">
                                </asp:hyperlink>
                                    </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink  id="lnkUP05" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_RunningHour_list.aspx" target="middleFrame" text="Actual Station Running Hour">
                                </asp:hyperlink>
                                    </div></a></td>
							</tr>
                        </table>
					</div>

                    <div class="panel">
                        <table id="tblSpc1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

                    <div class="panel">
                        <table id="tblNUHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

                    <button class="accordion">Nursery</button>					
					<div class="panel">
                        <table id="tblNU"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkNU01" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_PurReq.aspx" target="middleFrame" text="Purchase Requisition"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkNU02" runat="server" NavigateUrl="/en/NU/Trx/NU_trx_SeedReceiveList.aspx"  target="middleFrame" text="Seeds Receive"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkNU03" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedPlantList.aspx"  target="middleFrame" text="Seeds Planting"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkNU04" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_DoubleTurnList.aspx" target="middleFrame" text="Double Tone"></asp:hyperlink>
                                    </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkNU06" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_CullList.aspx" target="middleFrame" text="Culling Transaction"></asp:hyperlink>
                                    </div></a></td>
							</tr>

                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkNU05" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedTransPlantList.aspx"  target="middleFrame" text="Seedings Transplanting"></asp:hyperlink>
                                    </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkNU07" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedDispatchList.aspx" target="middleFrame" text="Seedlings Dispatch"></asp:hyperlink>
                                    </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
								<%--<asp:hyperlink class="lb-mt" id="lnkNU08" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedlingsIssue_list.aspx" target="middleFrame" text="Seedlings Issue"></asp:hyperlink>--%>
								<asp:hyperlink class="lb-mt" id="lnkNU08" runat="server" NavigateUrl="/en/IN/Trx/IN_Trx_StockIssue_List.aspx?type=NU" target="middleFrame" text="Seedlings Issue"></asp:hyperlink>
                                    </div></a></td>
							</tr>
						</table>
					</div>

                    <div class="panel">
                        <table id="tblSpc2"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

                    <div class="panel">
                        <table id="tblWSHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>

                    <button class="accordion">Workshop</button>					
					<div class="panel">
                        <table id="tblWS"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkWS01" runat="server" NavigateUrl="/en/WS/trx/ws_trx_job_list.aspx" target="middleFrame" text="Workshop Job"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                               <asp:hyperlink class="lb-mt" id="lnkWS02" runat="server" NavigateUrl="/en/WS/trx/ws_trx_mechanichour_list.aspx" target="middleFrame" text="Mechanic Hour"></asp:hyperlink>
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

			     		<table id="tblUPHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
						<tr height="20">
						<td width="20"></td>
						<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
						<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblUP);">Vehicle & Workshop </A></td>
						</tr>
			    		</table>
						<table id="tblUP" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkUP01" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WPList.aspx"  target="middleFrame" text="Work Performance"></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkUP02" runat="server" NavigateUrl="/en/PR/Trx/PR_Trx_WPContractorList.aspx"  target="middleFrame" text="Work Performance Contractor"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink  id="lnkUP03" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_VehicleUsage_list.aspx" target="middleFrame" text="Vehicle Usage"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink  id="lnkUP04" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_Trx_Workshop_List.aspx" target="middleFrame" text="Workshop"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink  id="lnkUP05" runat="server"  cssclass="lb-mt" NavigateUrl="/en/GL/Trx/GL_trx_RunningHour_list.aspx" target="middleFrame" text="Actual Station Running Hour"></asp:hyperlink></td>
							</tr>


						</table>
						
						<table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>			
						
                        			<table id="tblNUHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblNU);">Nursery</A></td>
							</tr>
						</table>
						<table id="tblNU" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU01" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_PurReq.aspx" target="middleFrame" text="Purchase Requisition"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU02" runat="server" NavigateUrl="/en/NU/Trx/NU_trx_SeedReceiveList.aspx"  target="middleFrame" text="Seeds Receive"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU03" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedPlantList.aspx"  target="middleFrame" text="Seeds Planting"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU04" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_DoubleTurnList.aspx" target="middleFrame" text="Double Turns"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU06" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_CullList.aspx" target="middleFrame" text="Culling Transaction"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU05" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedTransPlantList.aspx"  target="middleFrame" text="Seedings Transplanting"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU07" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedDispatchList.aspx" target="middleFrame" text="Seedlings Dispatch"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkNU08" runat="server" NavigateUrl="/en/NU/Trx/NU_Trx_SeedlingsIssue_list.aspx" target="middleFrame" text="Seedlings Issue"></asp:hyperlink></td> 
							    
							</tr>
						</table>

						<table id="tblSpc2" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>

						<table id="tblWSHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblWS);">Workshop</A></td>
							</tr>
						</table>
						<table id="tblWS" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWS01" runat="server" NavigateUrl="/en/WS/trx/ws_trx_job_list.aspx" target="middleFrame" text="Workshop Job"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWS02" runat="server" NavigateUrl="/en/WS/trx/ws_trx_mechanichour_list.aspx" target="middleFrame" text="Mechanic Hour"></asp:hyperlink></td>
							</tr>
						</table>




            </div>--%>
         

            
          </form>
           
    </form>
</body>
</html>

