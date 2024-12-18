<%@ Page Language="vb" src="../include/menu_pmstp.aspx.vb" Inherits="menu_pmstp" %>

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
                
                    <div class="panel">
                        <table id="tlbStpUpHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
        
				    <button class="accordion">Upkeep</button>					
					<div class="panel">
                        <table id="tlbStpUp"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP01" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubGrpCode.aspx" text="Grup Biaya Kendaraan Code"></asp:hyperlink>
                                </div></a></td>
							</tr>
	                        <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP02" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubCode.aspx" text="Biaya Kenderaan Code"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP03" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleType.aspx" text="Jenis Kendaraan"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP04" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Vehicle.aspx" text="Kendaraan"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP05" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_BlockGrp.aspx" text="Divisi"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP06" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Block.aspx" text="Tahun Tanam"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink class="lb-mt" id="lnkStpUP07" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_SubBlock.aspx" text="Blok"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpUP08" runat="server" NavigateUrl="/en/HR/setup/HR_setup_GangList.aspx" target="middleFrame" text="Gang" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpUP09" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ContractorSuperList.aspx" target="middleFrame" text="Contrator Supervision" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink cssclass="lb-mt" id="lnkStpUP10" runat="server" target="middleFrame" NavigateUrl="/en/GL/setup/GL_Setup_VehicleActivity.aspx" text="Aktifitas Kendaraan"></asp:hyperlink>
                                </div></a></td>
							</tr>
						</table>
					</div>
                    <div class="panel">
                        <table id="tlbSpc1"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <div class="panel">
                        <table id="tlbStpNUHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>

                    <button class="accordion">Nursery</button>	
                    <div class="panel">
                        <table id="tlbStpNU"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpNU01" runat="server" NavigateUrl="/en/NU/setup/NU_setup_ItemMaster.aspx" target="middleFrame" text="Nursery Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpNU02" runat="server" NavigateUrl="/en/NU/setup/NU_setup_Item.aspx" target="middleFrame" text="Nursery Item" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpNU03" runat="server" NavigateUrl="/en/NU/setup/NU_setup_NUBatch.aspx" target="middleFrame" text="MS Nursery Batch" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpNU04" runat="server" NavigateUrl="/en/NU/setup/NU_setup_CullType.aspx" target="middleFrame" text="MS Culling Type" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpNU05" runat="server" NavigateUrl="/en/NU/setup/NU_setup_AccDist.aspx" target="middleFrame" text="MS Account Classification" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>

						</table>
					</div>
              
               <div class="panel">
                        <table id="tlbSpc2"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>
                    <div class="panel">
                        <table id="tlbStpWSHead"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 
						</table>
					</div>

                    <button class="accordion">Workshop</button>	
                    <div class="panel">
                        <table id="tlbStpWS"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							 <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS01" runat="server" NavigateUrl="/en/WS/setup/WS_ProdType.aspx" target="middleFrame" text="Tipe Produk" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS02" runat="server" NavigateUrl="/en/WS/setup/WS_ProdBrand.aspx" target="middleFrame" text="Product Brand" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS03" runat="server" NavigateUrl="/en/WS/setup/WS_ProdModel.aspx" target="middleFrame" text="Product Model" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS04" runat="server" NavigateUrl="/en/WS/setup/WS_ProdCategory.aspx" target="middleFrame" text="Product Category" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS05" runat="server" NavigateUrl="/en/WS/setup/WS_ProdMaterial.aspx" target="middleFrame" text="Product Material" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS06" runat="server" NavigateUrl="/en/WS/setup/WS_StockAnalysis.aspx" target="middleFrame" text="Analisis Stok" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS07" runat="server" NavigateUrl="/en/WS/setup/WS_WorkCodeList.aspx" target="middleFrame" text="Pekerjaan Code" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS08" runat="server" NavigateUrl="/en/WS/setup/WS_ServTypeList.aspx" target="middleFrame" text="Service Type" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS09" runat="server" NavigateUrl="/en/WS/setup/WS_DirectCharge.aspx" target="middleFrame" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS10" runat="server" NavigateUrl="/en/WS/setup/WS_DirectMaster.aspx" target="middleFrame" text="Workshop Direct Charge Master"
										cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS11" runat="server" NavigateUrl="/en/WS/setup/WS_ItemPart.aspx" target="middleFrame" text="Workshop Item Part" cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS12" runat="server" NavigateUrl="/en/WS/setup/WS_MillProcDistr.aspx" target="middleFrame" text="Monthly Mill Process Distribution"
										cssclass="lb-mt"></asp:hyperlink>
                                </div></a></td>
							</tr>
                            <tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpWS13" runat="server" NavigateUrl="/en/WS/setup/WS_CalendarMachineList.aspx" target="middleFrame" text="Calendarized Machine" cssclass="lb-mt"></asp:hyperlink>
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

