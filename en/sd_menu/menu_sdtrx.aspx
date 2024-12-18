<%@ Page Language="vb" src="../include/menu_sdtrx.aspx.vb" Inherits="menu_sdtrx" %>

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

	    <table id="tblSDHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
			<tr height="20">
			<td width="20"></td>
			<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
			<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblSD);">Sales and Distribution</A></td>
			</tr>
	    </table>

	    <table id="tblSD" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSD01" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractRegList.aspx"  target="middleFrame" text="Contract Registration"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSD02" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractMatchList.aspx" target="middleFrame" text="Contract Matching"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSD03" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_DORegistrationList.aspx" target="middleFrame" text="DO Registration"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSD04" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeighBridgeTicketList.aspx" target="middleFrame" text="WeighBridge Ticket"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSD05" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_FFBAssessList.aspx" target="middleFrame" text="FFB Assessment"></asp:hyperlink></td>
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

