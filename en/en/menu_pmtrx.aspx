<%@ Page Language="vb" src="../include/menu_pmtrx.aspx.vb" Inherits="menu_pmtrx" %>

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

