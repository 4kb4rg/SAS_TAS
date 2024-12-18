<%@ Page Language="vb" src="../include/menu_pdstp.aspx.vb" Inherits="menu_pdstp" %>

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

    
				    <button class="accordion"> SETUP</button>					
					<div class="panel">
                        <table id="tlbStpPD"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD1" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_Mill.aspx" target="middleFrame" text="Mill" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
	                        <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD2" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx" target="middleFrame" text="Ullage - Volume Table Master"
										cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD3" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageVolumeConversionMaster.aspx" target="middleFrame" text="Ullage - Volume Conversion Master"
										cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD4" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_UllageAverageCapacityConversionMaster.aspx" target="middleFrame" text="Ullage - Average Capacity Conversion Master"
										cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD5" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_CPOPropertiesMaster.aspx" target="middleFrame" text="CPO Properties Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD6" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageTypeMaster.aspx" target="middleFrame" text="Storage Type Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD7" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_StorageAreaMaster.aspx" target="middleFrame" text="Storage Area Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD8" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_ProcessingLineMaster.aspx" target="middleFrame" text="Processing Line Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD9" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineMaster.aspx" target="middleFrame" text="Machine Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD10" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableOilQuality.aspx"  target="middleFrame" text="Acceptable Oil Quality" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD11" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_AcceptableKernelQuality.aspx" target="middleFrame" text="Acceptable Kernel Quality" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD12" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_TestSample.aspx" target="middleFrame" text="Test Sample" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD13" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_HarvestingInterval.aspx" target="middleFrame" text="Harvesting Interval" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr hidden>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkPD14" runat="server" NavigateUrl="/en/PM/Setup/PM_Setup_MachineCriteria.aspx" target="middleFrame" text="Machine Criteria" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>

              

        <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
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

