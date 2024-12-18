<%@ Page Language="vb" src="../include/menu_rptd.aspx.vb" Inherits="menu_rptd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
<body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server" >
         
                 <table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 
			<tr>
				<td valign="top">
                    <div class="panel">
                            <table id="tblMMHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						    </table>
					    </div>

       
				    <button class="accordion">Material Management</button>					
					<div class="panel">
                        <table id="tblMM"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptMM1" runat="server" target="middleFrame"  text="Inventory" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
	                        <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptMM2" runat="server" target="middleFrame"  text="Purchasing" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>
                    <div class="panel">
                            <table id="tblspc1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						    </table>
					</div>
                    <div class="panel">
                        <table id="tblPMHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						</table>
					</div>

                    <button class="accordion">Plant Maintenance</button>					
					<div class="panel">
                        <table id="Table1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptPM1" runat="server" target="middleFrame"  text="Nursery" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
	                        <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptPM2" runat="server" target="middleFrame"  text="Workshop" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>

                    <div class="panel">
                            <table id="tblspc2"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						    </table>
					</div>
                    <div class="panel">
                        <table id="tblPDHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						</table>
					</div>

                    <button class="accordion">Production</button>					
					<div class="panel">
                        <table id="tblPD"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptPD1" runat="server" target="middleFrame"  text="Production" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>

                    <div class="panel">
                            <table id="tblspc3"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						    </table>
					</div>
                    <div class="panel">
                        <table id="tblSDHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						</table>
					</div>
                    <button class="accordion">Sales & Distribution</button>					
					<div class="panel">
                        <table id="tblSD"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptSD1" runat="server" target="middleFrame"  text="Contract Management" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptSD2" runat="server" target="middleFrame"  text="Weighing Management" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>

						</table>
					</div>

                    <div class="panel">
                            <table id="tblspc4"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						    </table>
					</div>
                    <div class="panel">
                        <table id="tblFIHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						</table>
					</div>
                    <button class="accordion">Financial Management</button>					
					<div class="panel">
                        <table id="Table2"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptFI1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptFI2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptFI3" runat="server" target="middleFrame"  text="Account Receivable" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptFI4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptFI5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>

                    <div class="panel">
                            <table id="tblspc5"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						    </table>
					</div>
                    <div class="panel">
                        <table id="tblHRHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							     
						</table>
					</div>
                    <button class="accordion">HR & Payroll</button>					
					<div class="panel">
                        <table id="Table5"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptHR1" runat="server" target="middleFrame"  text="Human Resource" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkRptHR2" runat="server" target="middleFrame"  text="Payroll" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                           
						</table>
					</div>


              

        <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 109px;"
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
           
    </form>
</body>
</html>

