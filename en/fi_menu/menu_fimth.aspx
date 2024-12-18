<%@ Page Language="vb" src="../include/menu_fimth.aspx.vb" Inherits="menu_fimth" %>

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

                        <table id="tlbmth01" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			  <tr height="20">
				<td width="20"></td>
				<td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				<td class="lb-hti"><asp:hyperlink id="lnkMth1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-tti"></asp:hyperlink></td>
			  </tr>

			  <tr height="20">
				<td width="20"></td>
				<td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				<td class="lb-hti"><asp:hyperlink id="lnkMth2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-tti"></asp:hyperlink></td>
			  </tr>

			  <tr height="20">
				<td width="20"></td>
				<td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				<td class="lb-hti"><asp:hyperlink id="lnkMth3" runat="server" target="middleFrame"  text="Account Receiveable" cssclass="lb-tti"></asp:hyperlink></td>
			  </tr>

			  <tr height="20">
				<td width="20"></td>
				<td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				<td class="lb-hti"><asp:hyperlink id="lnkMth4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-tti"></asp:hyperlink></td>
			  </tr>

			   <tr height="20">
				<td width="20"></td>
				<td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				<td class="lb-hti"><asp:hyperlink id="lnkMth5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-tti"></asp:hyperlink></td>
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

