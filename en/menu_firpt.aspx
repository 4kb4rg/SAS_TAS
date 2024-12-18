<%@ Page Language="vb" src="../include/menu_firpt.aspx.vb" Inherits="menu_firpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
     <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
    <body bgcolor="white" style="margin: 0">
    <form id="form1" runat="server">
                  
           <div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
                   <div class="panel">
                        <table id="tlbStpFIHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
                        </table>
                    </div>
				    <button class="accordion">Report</button>					
					<div class="panel">
                        <table id="tlbRpt"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr visible="false">
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRpt1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-mt"></asp:hyperlink></div>
                                </a></td>
							</tr>
							<tr visible="false">
								<td><a href="#"><div class="childmenu">
                               <asp:hyperlink class="lb-mt" id="lnkRpt2" runat="server" target="middleFrame" text="Account Payable"></asp:hyperlink></div>
                                </a></td>
							</tr>
							<tr visible="false">
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkRpt3" runat="server" target="middleFrame" text="Account Receivable"></asp:hyperlink></div>
                                </a></td>
							</tr>
							<tr visible="false" >
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkRpt4" runat="server"  target="middleFrame" text="Cash Bank Management"></asp:hyperlink></div>
                                </a></td>
							</tr>
							<tr visible="false">
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkRpt5" runat="server"  target="middleFrame" text="Fixed Asset"></asp:hyperlink></div>
                                </a></td>
							</tr>
                        
						</table>
					</div>

            </div>
           <%--<div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
            <table>
			    <tr height="20">
			    <td width="20"></td>
			   </tr>
			</table> 

                        <table id="tlbRpt" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/spacer.gif" border="0" align="left" width="1" height="1"></td>
							<td class="lb-hti"><asp:hyperlink id="lnkRpt1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-tti"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/spacer.gif" border="0" align="left" width="1" height="1"></td>
							<td class="lb-hti"><asp:hyperlink id="lnkRpt2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-tti"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/spacer.gif" border="0" align="left" width="1" height="1"></td>
							<td class="lb-hti"><asp:hyperlink id="lnkRpt3" runat="server" target="middleFrame"  text="Account Receivable" cssclass="lb-tti"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/spacer.gif" border="0" align="left" width="1" height="1"></td>
							<td class="lb-hti"><asp:hyperlink id="lnkRpt4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-tti"></asp:hyperlink></td>
						</tr>
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/spacer.gif" border="0" align="left" width="1" height="1"></td>
							<td class="lb-hti"><asp:hyperlink id="lnkRpt5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-tti"></asp:hyperlink></td>
						</tr>
						</table>



            </div>--%>
         
        <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 80px;"
				        scrolling="auto" src="black.aspx"  ></iframe>
             
               </div>
            
         
           <div class="BackgroundTopCorner"></div>
        </form>
           

</body>
</html>

