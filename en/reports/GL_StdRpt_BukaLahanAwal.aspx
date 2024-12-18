<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_BukaLahanAwal.aspx.vb" Inherits="GL_StdRpt_BukaLahanAwal" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Managerial Report (Biaya Pembukaan Lahan dan Pembiayaan Awal)</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1"  class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - MANAGERIAL REPORT (BIAYA PEMBUKAAN LAHAN DAN PEMELIHARAAN AWAL)</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:GL_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2"  class="font9Tahoma" runat=server >	
				<!-- request from Consultant to add these criteria fields 11 Jun 2007 -->
				<tr>
					<td width=25%><asp:label id="lblLevel" runat="server" /> Level : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlLevel" width="45%" autopostback=true runat="server" />						
					</td>			
				</tr>
				<tr id=TrBlkGrp runat=server visible=false>
					<td width=25%><asp:label id="lblBlkGrp" runat="server" /> : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlBlkGrp" width="45%" runat="server"/> (blank for all)
					</td>			
				</tr>	
				<tr id=TrBlk runat=server visible=false>
					<td width=25%><asp:label id="lblBlk" runat="server" /> : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlBlk" width="45%" runat="server"/> (blank for all)
					</td>			
				</tr>		
				<tr id=TrSubBlk runat=server visible=false>
					<td width=25%><asp:label id="lblSubBlk" runat="server" /> : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlSubBlk" width="45%" runat="server"/> (blank for all)
					</td>			
				</tr>	
				<tr id=TrLoc runat=server visible=false>
					<td width=25%><asp:label id="lblLocCode" visible=false runat="server" /></td>
				</tr>	
				<tr id=TrThnTnm runat=server visible=false>
					<td width=25%><asp:label id="lblThnTnm" runat="server" /></td>
					<td colspan=2>
						<asp:DropDownList id="ddlThnTnm" width="45%" runat="server"/> (blank for all)
					</td>
				</tr>
				<!-- ==================================================================================================== -->													
				<tr>
					<td colspa=3>&nbsp;</td>
				</tr>				
								
			</table>
			
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table3"  class="font9Tahoma" runat=server>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>					
				</tr>
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblHidCostLevel" visible="false" runat="server" />
	</body>
</HTML>