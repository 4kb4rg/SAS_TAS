<%@ Page Language="vb" src="../include/sysadm_menu.aspx.vb" Inherits="sysadm_menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
    <body bgcolor="white" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server">
         <table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 
			<tr>
				<td valign="top">
				    <button class="accordion">System Setup</button>					
					<div class="panel">
                        <table id="tlbStpIN"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                               <asp:hyperlink  id="lnkSS01" runat="server" NavigateUrl="/en/system/config/syssetting.aspx" target="middleFrame" text="System Configuration" ></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkSS02" runat="server" NavigateUrl="/en/system/config/sys_param_setting.aspx" target="middleFrame" text="Parameter Setting" ></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkSS03" runat="server" NavigateUrl="/en/system/user/userlist.aspx" target="middleFrame" text="Application User"></asp:hyperlink></div></a></td>
							</tr>

							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkSS03_a" runat="server" NavigateUrl="/en/system/user/userDailyControl.aspx" target="middleFrame" text="User Daily Control"></asp:hyperlink>
                                    </div></a></td>
							</tr>		

							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkIN04" runat="server" NavigateUrl="/en/system/langcap/Language_Cap.aspx" target="middleFrame" text="Penamaan Istilah" ></asp:hyperlink>
                                    </div></a></td>
							</tr>                         

						</table>
					</div>
                  <button class="accordion">Administration</button>					
					<div class="panel">
                        <table id="tlbStpPU" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkAD01" runat="server" NavigateUrl="/en/menu/menu_admin_page.aspx" target="middleFrame" text="Setup"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkAD02" runat="server" NavigateUrl="/en/menu/menu_admindata_page.aspx" target="middleFrame" text="Data Transfer"></asp:hyperlink></div></a></td>
							</tr>

                        </table>
                    </div>

                <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 80px;"
				        scrolling="auto" src="black.aspx"  ></iframe>     
             
               </div>

                </td>
            </tr>
        </table>
    </tr>
</table>

           <%--<div id="Nav" style="position:absolute; width:177px; top:0px; left:0px; height:1000px">
            	
            <table>
			    <tr height="20">
			    <td width="20"></td>
			   </tr>
			</table> 

                        
			 <table id="tblSSHead" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblSS);">System Setup</A></td>
							</tr>
						</table>
						<table id="tblSS" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSS01x" runat="server" NavigateUrl="/en/system/config/syssetting.aspx" target="middleFrame" text="System Configuration"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSS02x" runat="server" NavigateUrl="/en/system/config/sys_param_setting.aspx" target="middleFrame" text="Parameter Setting"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkSS03x" runat="server" NavigateUrl="/en/system/user/userlist.aspx" target="middleFrame" text="Application User"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkIN04x" runat="server" NavigateUrl="/en/system/langcap/Language_Cap.aspx" target="middleFrame" text="Penamaan Istilah"></asp:hyperlink></td>
							</tr>
							
						</table>
						
						<table id="tblSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>			
						
                                               <table id="tblADHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblAD);">Administration</A></td>
							</tr>
						</table>
						<table id="tblAD" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAD01" runat="server" NavigateUrl="/en/menu/menu_admin_page.aspx" target="middleFrame" text="Setup"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkAD02" runat="server" NavigateUrl="/en/menu/menu_admindata_page.aspx" target="middleFrame" text="Data Transfer"></asp:hyperlink></td>
							</tr>
							
						</table>
		




            </div>--%><%--
         
             <div style="position:absolute; top:0px; width:100%; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>--%>
            
         
           <div class="BackgroundTopCorner"></div>
        </form>
           

</body>
</html>

