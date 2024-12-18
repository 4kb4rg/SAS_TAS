<%@ Page Language="vb" src="../include/menu_sdstp.aspx.vb" Inherits="menu_sdstp" %>

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

                        
			<table id="tlbStpSD" style="position:absolute" cellspacing="0" cellpadding="0"
							width="100%" border="0" runat="server">
				
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:hyperlink id="lnkStpSD1" runat="server" NavigateUrl="/en/CM/setup/CM_Setup_CurrencyList.aspx" target="middleFrame" text="Currency" cssclass="lb-mt"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:hyperlink id="lnkStpSD2" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ExchangeRate.aspx" target="middleFrame" text="Exchange Rate" cssclass="lb-mt"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:hyperlink id="lnkStpSD3" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ContractQuality.aspx" target="middleFrame" text="Contract Quality" cssclass="lb-mt"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:hyperlink id="lnkStpSD4" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ClaimQuality.aspx" target="middleFrame" text="Claim Quality" cssclass="lb-mt"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpSD5" runat="server" target="middleFrame" NavigateUrl="/en/BI/setup/BI_setup_BillPartyList.aspx" text="Customer"></asp:hyperlink></td>
				</tr>
				<tr height="20">
					<td width="20"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpSD6" runat="server" target="middleFrame" NavigateUrl="/en/WM/setup/WM_setup_TransporterList.aspx" text="Transporter"></asp:hyperlink></td>
				</tr>
							
			</table>
						
						
					
            </div>
         
             <div style="position:absolute; top:0px; width:85%; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
         
           <div class="BackgroundTopCorner"></div>
        </form>
           

</body>
</html>

