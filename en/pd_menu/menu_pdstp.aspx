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
         
           <div id="Nav" style="position:absolute; width:177px; top:0px; left:0px; height:500px">
            	
            		<table>
			    <tr height="20">
			    <td width="20"></td>
			   </tr>
			</table> 

                        <table id="tlbStp01" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpES);">Estate</A></td>
						</tr>
						</table>
						<table id="tlbStpES" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpES01" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_Mill.aspx" target="middleFrame" text="Mill" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
						</table>
						
						<table id="tblSpc01" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>




                        <table id="tlbStp02" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpML);">Mill</A></td>
						</tr>
						</table>
						<table id="tlbStpML" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML01" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx" target="middleFrame" text="Ullage - Volume Table Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML02" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeConversionMaster.aspx" target="middleFrame" text="Ullage - Volume Conversion Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML03" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageAverageCapacityConversionMaster.aspx" target="middleFrame" text="Ullage - Average Capacity Conversion Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML04" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_CPOPropertiesMaster.aspx" target="middleFrame" text="CPO Properties Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML05" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageTypeMaster.aspx" target="middleFrame" text="Storage Type Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML06" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageAreaMaster.aspx" target="middleFrame" text="Storage Area Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML07" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_ProcessingLineMaster.aspx" target="middleFrame" text="Processing Line Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML08" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineMaster.aspx" target="middleFrame" text="Machine Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML09" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableOilQuality.aspx"  target="middleFrameright" text="Acceptable Oil Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML10" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableKernelQuality.aspx" target="middleFrame" text="Acceptable Kernel Quality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML11" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_TestSample.aspx" target="middleFrame" text="Test Sample" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML12" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_HarvestingInterval.aspx" target="middleFrame" text="Harvesting Interval" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpML13" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineCriteria.aspx" target="middleFrame" text="Machine Criteria" cssclass="lb-mt"></asp:hyperlink></td>
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

