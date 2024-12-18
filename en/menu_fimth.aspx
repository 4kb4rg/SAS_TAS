<%@ Page Language="vb" src="../include/menu_fimth.aspx.vb" Inherits="menu_fimth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
    <body style="margin: 0" >
    <form id="form1" runat="server">
         
 <table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 			<tr>
				<td valign="top">
                   
				    <button class="accordion">Month End</button>					
					<div class="panel">
                        <table id="tlbMth"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkMth1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-tti"></asp:hyperlink>
                                </div></a></td>
							</tr>
		                    <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkMth2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-tti"></asp:hyperlink>
                                </div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkMth3" runat="server" target="middleFrame"  text="Account Receiveable" cssclass="lb-tti"></asp:hyperlink>
                                </div></a></td>
							</tr> 
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkMth4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-tti"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkMth5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-tti"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkMth6" runat="server" target="middleFrame"  text="Financial Statement" cssclass="lb-tti"></asp:hyperlink>
                                </div></a></td>
							</tr>							
						</table>
					</div>

              

        <div style="position:absolute; top:0px; width:86%; left:125px; height:1500px" >
          
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


  
        </form>
           

</body>
</html>

