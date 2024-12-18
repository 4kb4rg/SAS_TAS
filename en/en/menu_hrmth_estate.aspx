<%@ Page Language="vb" src="../include/menu_hrmth_estate.aspx.vb" Inherits="menu_hrmth_estate" %>

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
                        
                        <%--<table id="TblPR_DailyHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblPR_Daily);javascript:togglebox1(tblPR_Monthy);">Daily Processing</A></td>
							</tr>
						</table>
						
						<table id="tblPR_Daily" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink1" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_dailyprocess_estate.aspx" target="middleFrame" text="Daily Process"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink5" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_dailyrollback_estate.aspx" target="middleFrame" text="Rollback Daily Process"></asp:hyperlink></td>
							</tr>
							
						</table>
						
						
						<table id="Table6" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
					    --%>
                        <table id="TblPT_MonthlyHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblPR_Monthy);">Proses Bulanan</A></td>
							</tr>
						</table>
						
						<table id="tblPR_Monthy" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
				            <%--
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink4" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_saldoawalpinjaman_estate.aspx"  target="middleFrame" text="Saldo Awal Pinjaman/Gaji Kecil" ></asp:hyperlink></td>
							</tr>
											
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink5" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_hkprocess_estate.aspx" target="middleFrame" text="Proses HK" ></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink2" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_gajibesarprocess_estate.aspx"  target="middleFrame" text="Proses Gaji Besar" ></asp:hyperlink></td>
							</tr>
				
				
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink14" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_riceration_estate.aspx" target="middleFrame" text="Proses Catu Beras" ></asp:hyperlink></td>
							</tr>
							
								
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink1" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_pinjamanprocess_estate.aspx"  target="middleFrame" text="Proses Pinjaman/Gaji Kecil" ></asp:hyperlink></td>
							</tr>
							
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink6" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_THRProcess_estate.aspx"  target="middleFrame" text="Proses THR" ></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink6b" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_BonusProcess_estate.aspx"  target="middleFrame" text="Proses Bonus" ></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink6c" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_rapel_estate.aspx"  target="middleFrame" text="Proses Rapel Upah" ></asp:hyperlink></td>
							</tr>
							--%>
							<%--
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink6" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_THRProcess_estate.aspx"  target="middleFrame" text="Proses PPH21" ></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink6" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_THRProcess_estate.aspx"  target="middleFrame" text="Proses Rapel" ></asp:hyperlink></td>
							</tr>
							
							
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink6" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_THRProcess_estate.aspx"  target="middleFrame" text="Proses Bonus" ></asp:hyperlink></td>
							</tr>
							
							
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink3" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_Process_estate.aspx"  target="middleFrame" text="Proses Tutup Buku Payroll" ></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="HyperlinkStatus" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_Status_estate.aspx"  target="middleFrame" text="Confirm Payroll" ></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink7" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_PPH21_estate.aspx"  target="middleFrame" text="Proses PPH21" ></asp:hyperlink></td>
							</tr>
							--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink1N" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_payrollprocess_estate.aspx"  target="middleFrame" text="Proses Payroll" ></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink2N" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_stkissueprocess_estate.aspx"  target="middleFrame" text="Proses Stock Issue BKM" ></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="Hyperlink3N" runat="server" NavigateUrl="/en/PR/mthend/PR_mthend_spkprocess_estate.aspx"  target="middleFrame" text="Proses Goods Receive BAPP" ></asp:hyperlink></td>
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

