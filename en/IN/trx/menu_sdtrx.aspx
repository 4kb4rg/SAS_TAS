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
         
           <div id="Nav" style="position:absolute; width:177px; top:0px; left:0px; height:1000px">
            	
            <table>
		<tr height="20">
		<td width="20"></td>
		</tr>
	    </table> 

	    <table id="tblCMHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
			<tr height="20">
			<td width="20"></td>
			<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
			<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblCM);">Contract Management</A></td>
			</tr>
	    </table>

	    <table id="tblCM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0" runat="server">
							
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM1" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractRegList.aspx"  target="middleFrame" text="Contract Registration"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkCM2" runat="server" NavigateUrl="/en/CM/trx/CM_Trx_ContractMatchList.aspx" target="middleFrame" text="Contract Matching"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
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
			<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
			<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblWM);">Weighing Management</A></td>
			</tr>
	    </table>

	    <table id="tblWM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0" runat="server" >
							
			
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM1" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_WeighBridgeTicketList.aspx" target="middleFrame" text="WeighBridge Ticket"></asp:hyperlink></td>
			</tr>
			<tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM2" runat="server" NavigateUrl="/en/WM/trx/WM_Trx_FFBAssessList.aspx" target="middleFrame" text="FFB Assessment"></asp:hyperlink></td>
			</tr>
                        <tr height="20">
				<td width="20"></td>
				<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
				<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkWM3" runat="server" NavigateUrl="/en/WM/data/WM_data_uploadweigh.aspx" target="middleFrame" text="Weighing Download"></asp:hyperlink></td>
			</tr>

	  </table>

						
					
            </div>
         
             <div style="position:absolute; top:0px; width:820px; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
           <div class="BackgroundTopCorner"></div>
          </form>
           
    </form>
</body>
</html>

