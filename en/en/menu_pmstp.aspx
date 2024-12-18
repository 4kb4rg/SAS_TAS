<%@ Page Language="vb" src="../include/menu_pmstp.aspx.vb" Inherits="menu_pmstp" %>

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

                        <table id="tlbStpUpHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpUp);">Upkeep</A></td>
						</tr>
						</table>
						<table id="tlbStpUp" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP01" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubGrpCode.aspx" text="Grup Biaya Kendaraan Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP02" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubCode.aspx" text="Biaya Kenderaan Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP03" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleType.aspx" text="Jenis Kendaraan"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP04" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Vehicle.aspx" text="Kendaraan"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP05" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_BlockGrp.aspx" text="Divisi"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP06" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Block.aspx" text="Tahun Tanam"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpUP07" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_SubBlock.aspx" text="Blok"></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpUP08" runat="server" NavigateUrl="/en/HR/setup/HR_setup_GangList.aspx" target="middleFrame" text="Gang" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpUP09" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ContractorSuperList.aspx" target="middleFrame" text="Contrator Supervision" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink cssclass="lb-mt" id="lnkStpUP10" runat="server" target="middleFrame" NavigateUrl="/en/GL/setup/GL_Setup_VehicleActivity.aspx" text="Aktifitas Kendaraan"></asp:hyperlink></td>
							</tr>
						</table>
						
						<table id="tlbSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>




                        <table id="tlbStpNUHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpNU);">Nursery</A></td>
						</tr>
						</table>
						<table id="tlbStpNU" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU01" runat="server" NavigateUrl="/en/NU/setup/NU_setup_ItemMaster.aspx" target="middleFrame" text="Nursery Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU02" runat="server" NavigateUrl="/en/NU/setup/NU_setup_Item.aspx" target="middleFrame" text="Nursery Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU03" runat="server" NavigateUrl="/en/NU/setup/NU_setup_NUBatch.aspx" target="middleFrame" text="MS Nursery Batch" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU04" runat="server" NavigateUrl="/en/NU/setup/NU_setup_CullType.aspx" target="middleFrame" text="MS Culling Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpNU05" runat="server" NavigateUrl="/en/NU/setup/NU_setup_AccDist.aspx" target="middleFrame" text="MS Account Classification" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
						</table>
						
						<table id="tlbSpc2" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>

                        <table id="tlbStpWSHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<tr height="20">
							<td width="20"></td>
							<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
							<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpWS);">Workshop</A></td>
						</tr>
						</table>
						<table id="tlbStpWS" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS01" runat="server" NavigateUrl="/en/WS/setup/WS_ProdType.aspx" target="middleFrame" text="Tipe Produk" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS02" runat="server" NavigateUrl="/en/WS/setup/WS_ProdBrand.aspx" target="middleFrame" text="Product Brand" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS03" runat="server" NavigateUrl="/en/WS/setup/WS_ProdModel.aspx" target="middleFrame" text="Product Model" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS04" runat="server" NavigateUrl="/en/WS/setup/WS_ProdCategory.aspx" target="middleFrame" text="Product Category" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS05" runat="server" NavigateUrl="/en/WS/setup/WS_ProdMaterial.aspx" target="middleFrame" text="Product Material" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS06" runat="server" NavigateUrl="/en/WS/setup/WS_StockAnalysis.aspx" target="middleFrame" text="Analisis Stok" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS07" runat="server" NavigateUrl="/en/WS/setup/WS_WorkCodeList.aspx" target="middleFrame" text="Pekerjaan Code" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS08" runat="server" NavigateUrl="/en/WS/setup/WS_ServTypeList.aspx" target="middleFrame" text="Service Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS09" runat="server" NavigateUrl="/en/WS/setup/WS_DirectCharge.aspx" target="middleFrame" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS10" runat="server" NavigateUrl="/en/WS/setup/WS_DirectMaster.aspx" target="middleFrame" text="Workshop Direct Charge Master"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS11" runat="server" NavigateUrl="/en/WS/setup/WS_ItemPart.aspx" target="middleFrame" text="Workshop Item Part" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS12" runat="server" NavigateUrl="/en/WS/setup/WS_MillProcDistr.aspx" target="middleFrame" text="Monthly Mill Process Distribution"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpWS13" runat="server" NavigateUrl="/en/WS/setup/WS_CalendarMachineList.aspx" target="middleFrame" text="Calendarized Machine" cssclass="lb-mt"></asp:hyperlink></td>
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

