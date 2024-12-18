<%@ Page Language="vb" src="../include/menu_pdstp.aspx.vb" Inherits="menu_pdstp" %>

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

                      
						<table id="tlbStpPD" style="position:absolute" cellspacing="0" cellpadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD1" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_Mill.aspx" target="middleFrame" text="Mill" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>



							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD2" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx" target="middleFrame" text="Ullage - Volume Table Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD3" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeConversionMaster.aspx" target="middleFrame" text="Ullage - Volume Conversion Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD4" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageAverageCapacityConversionMaster.aspx" target="middleFrame" text="Ullage - Average Capacity Conversion Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD5" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_CPOPropertiesMaster.aspx" target="middleFrame" text="CPO Properties Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD6" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageTypeMaster.aspx" target="middleFrame" text="Storage Type Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD7" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageAreaMaster.aspx" target="middleFrame" text="Storage Area Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD8" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_ProcessingLineMaster.aspx" target="middleFrame" text="Processing Line Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD9" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineMaster.aspx" target="middleFrame" text="Machine Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD10" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableOilQuality.aspx"  target="middleFrame" text="Acceptable Oil Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD11" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableKernelQuality.aspx" target="middleFrame" text="Acceptable Kernel Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD12" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_TestSample.aspx" target="middleFrame" text="Test Sample" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD13" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_HarvestingInterval.aspx" target="middleFrame" text="Harvesting Interval" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkPD14" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineCriteria.aspx" target="middleFrame" text="Machine Criteria" cssclass="lb-mt"></asp:hyperlink></td>
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

