<%@ Page Language="vb" src="../include/menu_fistp.aspx.vb" Inherits="menu_fistp" %>

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

						<table id="tlbStpFIHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpFI);">General Ledger</A></td>
							</tr>
						</table>
						<table id="tlbStpFI" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFI01" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_AccCls.aspx" text="Kumpulan Kelas Akaun" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFI02" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_AccClsGrp.aspx" text="Kelas Akaun" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI03" runat="server" NavigateUrl="/en/GL/Setup/GL_Setup_ActivityGrp.aspx" target="middleFrame" text="Kumpulan Aktiviti"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI04" runat="server" NavigateUrl="/en/GL/Setup/GL_Setup_Activity.aspx" target="middleFrame" text="Aktiviti"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI05" runat="server" NavigateUrl="/en/GL/Setup/GL_Setup_SubActivity.aspx" target="middleFrame" text="Sub Activiti"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI06" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_ExpenseCode.aspx" text="Expense Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI07" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubGrpCode.aspx" text="Grup Biaya Kendaraan Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI08" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleSubCode.aspx" text="Biaya Kenderaan Code"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI09" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_VehicleType.aspx" text="Jenis Kenderaan"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI10" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Vehicle.aspx" text="Kenderaan"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI11" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_BlockGrp.aspx" text="Divisi"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI12" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Block.aspx" text="Tahun Tanam"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI13" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_SubBlock.aspx" text="Blok"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI13b" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_TBM_TM.aspx" text="Mutasi Blok TBM - TM"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI14" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_ChartOfAccGrp.aspx" text="Group COA"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI15" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_ChartOfAcc.aspx" text="COA"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI16" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_DoubleEntry.aspx" text="Double Entry Setup"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI28" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_PDOType.aspx" text="PDO Group"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI21" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_ALKOHList.aspx" text="Alokasi Overhead"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI22" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_FSList.aspx" text="Financial Statement Report"></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI23" runat="server" target="middleFrame" NavigateUrl="/en/menu/menu_admin_page.aspx" text="Admin"></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI25" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_Budget.aspx" text="Budget"></asp:hyperlink></td>
							</tr>
							
							
							
							<tr height="20" visible=false>
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI17" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_BalanceSheet.aspx" text="Balance Sheet"></asp:hyperlink></td>
							</tr>
							<tr height="20" visible=false>
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI18" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_ProfitLoss.aspx" text="Profit and Loss"></asp:hyperlink></td>
							</tr>
							<tr height="20" visible=false>
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI19" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_COGS.aspx" text="COGS Account Setup Page 1"></asp:hyperlink></td>
							</tr>
							<tr height="20" visible=false>
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI20" runat="server" target="middleFrame" NavigateUrl="/en/GL/Setup/GL_Setup_COGS2.aspx" text="COGS Account Setup Page 2"></asp:hyperlink></td>
							</tr>
						</table>

						
						<table id="tlbSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						
						
						<table id="tlbStpAPHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpAP);">Account Payable</A></td>
							</tr>
						</table>
						<table id="tlbStpAP" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpAP01" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" text="Supplier"></asp:hyperlink></td>
							</tr>
							

						</table>

						<table id="tlbSpc2" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>

						<table id="tlbStpARHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpAR);">Account Receivable</A></td>
							</tr>
						</table>
						<table id="tlbStpAR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpAR01" runat="server" target="middleFrame" NavigateUrl="/en/BI/setup/BI_setup_BillPartyList.aspx" text="Customer"></asp:hyperlink></td>
							</tr>
							

						</table>


						<table id="tlbSpc3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>

						<table id="tlbStpCBHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpCB);">Cash Bank Management</A></td>
							</tr>
						</table>
						<table id="tlbStpCB" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpCB01" runat="server" NavigateUrl="/en/HR/setup/HR_setup_BankList.aspx" target="middleFrame" text="Bank" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpCB02" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" text="Supplier"></asp:hyperlink></td>
							</tr>
						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpCB03" runat="server" target="middleFrame" NavigateUrl="/en/BI/setup/BI_setup_BillPartyList.aspx" text="Customer"></asp:hyperlink></td>
							</tr>

                            <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpCB04" runat="server" NavigateUrl="/en/CM/setup/CM_Setup_CurrencyList.aspx" target="middleFrame" text="Currency" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpCB05" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ExchangeRate.aspx" target="middleFrame" text="Exchange Rate" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpCB06" runat="server" NavigateUrl="/en/HR/setup/HR_setup_BilyetGiro.aspx" target="middleFrame" text="Cheque & Bilyet Giro" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpCB07" runat="server" NavigateUrl="/en/HR/setup/HR_setup_StaffList.aspx" target="middleFrame" text="Staff" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>

						<table id="tlbSpc4" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>

						<table id="tlbStpFAHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20" >
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left" width="5" height="8"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpFA);">Fixed Asset</A></td>
							</tr>
						</table>
						<table id="tlbStpFA" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">

							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA01" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetClass.aspx" target="middleFrame" text="Asset Clasification" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA02" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetGrp.aspx" target="middleFrame" text="Asset Group" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA03" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetReg.aspx" target="middleFrame" text="Asset Registration Header" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA04" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetReglnList.aspx" target="middleFrame" text="Asset Registration Line" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA05" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetPermitList.aspx" target="middleFrame" text="Asset Permission" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA06" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetMaster.aspx" target="middleFrame" text="Asset Master" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpFA07" runat="server" NavigateUrl="/en/FA/setup/FA_setup_AssetItem.aspx" target="middleFrame" text="Asset Item" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>


						</table>
						
                        <table id="tblSpc5" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						    <tr>
							    <td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
						    </tr>
					    </table>
    					
					    <table id="tblTXHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						    <tr height="20" >
							    <td width="20"></td>
							    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
							    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblTX);">Tax Management</A></td>
						    </tr>
					    </table>
					    <table id="tblTX" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
						    width="100%" border="0" runat="server">
						    <tr height="20">
							    <td width="20"></td>
							    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpTX01" runat="server" NavigateUrl="/en/TX/Setup/TX_Setup_TaxObjectRateList.aspx" target="middleFrame" text="Tax Object Rate"></asp:hyperlink></td>
						    </tr>
						    <tr height="20">
							    <td width="20"></td>
							    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpTX02" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_TGCode_Estate.aspx" target="middleFrame" text="PTKP"></asp:hyperlink></td>
						    </tr>
						    <tr height="20">
							    <td width="20"></td>
							    <td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0"></td>
							    <td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpTX03" runat="server" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" target="middleFrame" text="Supplier"></asp:hyperlink></td>
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

