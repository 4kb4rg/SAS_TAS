<%@ Page Language="vb" src="../../include/reports/IN_StdRpt_StkIssueList.aspx.vb" Inherits="IN_StdRpt_StkIssueList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory - Stock Issue Listing</title>
            <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>INVENTORY - STOCK ISSUE LISTING</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:IN_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:label id=lblBillPartyCode visible=false runat=server /></td>
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr style="display:none;">
					<td>Stock Issue ID From :</td>
					<td><asp:textbox id="txtStkIssueIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtStkIssueIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>
				<tr>
					<td>Issue Type :</td>
					<td><asp:DropDownList id="lstStkIssueType" size="1" width="50%" runat="server" /></td>
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td><asp:label id=lblProdTypeCode runat=server /></td>
					<td>
					<asp:DropDownList ID="txtProdType" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All	</asp:ListItem>
					<asp:ListItem Value="10">BARANG UMUM	</asp:ListItem>
					<asp:ListItem Value="20">BAHAN AGRONOMY	</asp:ListItem>
					<asp:ListItem Value="31">S.PART KENDARAAN	</asp:ListItem>
					<asp:ListItem Value="32">S.PART ALAT BERAT	</asp:ListItem>
					<asp:ListItem Value="33">S.PART MESIN MESIN	</asp:ListItem>
					<asp:ListItem Value="34">S.PART MESIN PABRIK	</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdBrandCode runat=server /></td>
					<td>
					<asp:DropDownList ID="txtProdBrand" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All	</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblProdModelCode runat=server /></td>
					<td>
					<asp:DropDownList ID="txtProdModel" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All</asp:ListItem>
					<asp:ListItem Value="LLN">Lain-Lain</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:label id=lblProdCatCode runat=server /></td>
					<td>
					<asp:DropDownList ID="txtProdCat" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All	</asp:ListItem>
					<asp:ListItem Value="1001">BBM	</asp:ListItem>
					<asp:ListItem Value="1002">PELUMAS	</asp:ListItem>
					<asp:ListItem Value="1003">BARANG ELEKTRONIK/ELEKTRIK	</asp:ListItem>
					<asp:ListItem Value="1004">ACCU & KELENGKAPANNYA	</asp:ListItem>
					<asp:ListItem Value="1005">CAT & KELENGKAPANNYA	</asp:ListItem>
					<asp:ListItem Value="1006">PIPA & KELENGKAPANNYA	</asp:ListItem>
					<asp:ListItem Value="1007">BAHAN & ALAT PERTUKANGAN	</asp:ListItem>
					<asp:ListItem Value="1008">BARANG ADMINISTRASI	</asp:ListItem>
					<asp:ListItem Value="1009">OBAT-OBATAN	</asp:ListItem>
					<asp:ListItem Value="1010">BARANG UMUM LAINNYA	</asp:ListItem>
					<asp:ListItem Value="2001">PUPUK	</asp:ListItem>
					<asp:ListItem Value="2002">HERBISIDA, PESTISIDA, INSEC, RODENTISIDA	</asp:ListItem>
					<asp:ListItem Value="2003">KACANGAN	</asp:ListItem>
					<asp:ListItem Value="2004">PERALATAN SEMPROT	</asp:ListItem>
					<asp:ListItem Value="2005">PERKAKAS PEMELIHARAAN & PANEN	</asp:ListItem>
					<asp:ListItem Value="2006">PERALATAN LABORATORIUM	</asp:ListItem>
					<asp:ListItem Value="3101">SUKU CADANG JEEP/PICK UP	</asp:ListItem>
					<asp:ListItem Value="3102">SUKU CADANG BOX VAN / MINI BUS	</asp:ListItem>
					<asp:ListItem Value="3103">SUKU CADANG TRUCK / BUS	</asp:ListItem>
					<asp:ListItem Value="3104">SUKU CADANG KAPAL	</asp:ListItem>
					<asp:ListItem Value="3201">SUKU CADANG TRAILER	</asp:ListItem>
					<asp:ListItem Value="3202">SUKU CADANG TRACTOR	</asp:ListItem>
					<asp:ListItem Value="3203">SUKU CADANG EXCAVATOR	</asp:ListItem>
					<asp:ListItem Value="3204">SUKU CADANG GRADER	</asp:ListItem>
					<asp:ListItem Value="3205">SUKU CADANG LOADER	</asp:ListItem>
					<asp:ListItem Value="3206">SUKU CADANG TLB	</asp:ListItem>
					<asp:ListItem Value="3207">SUKU CADANG COMPACTOR	</asp:ListItem>
					<asp:ListItem Value="3208">SUKU CADANG BULDOZER	</asp:ListItem>
					<asp:ListItem Value="3209">SUKU CADANG ALAT BERAT LAINNYA	</asp:ListItem>
					<asp:ListItem Value="3301">SUKU CADANG MESIN LISTRIK	</asp:ListItem>
					<asp:ListItem Value="3302">SUKU CADANG MESIN AIR	</asp:ListItem>
					<asp:ListItem Value="3303">SUKU CADANG MESIN LAINNYA	</asp:ListItem>
					<asp:ListItem Value="3401">S.C LOADING RAMP	</asp:ListItem>
					<asp:ListItem Value="3402">S.C STERILIZER	</asp:ListItem>
					<asp:ListItem Value="3403">S.C THRESHER	</asp:ListItem>
					<asp:ListItem Value="3404">S.C PRESSER	</asp:ListItem>
					<asp:ListItem Value="3405">S.C NUT & KERNEL STATION	</asp:ListItem>
					<asp:ListItem Value="3406">S.C CLARIFICATION STATION	</asp:ListItem>
					<asp:ListItem Value="3407">S.C BOILER	</asp:ListItem>
					<asp:ListItem Value="3408">S.C ENGINE ROOM (TURBINE PLANT)	</asp:ListItem>
					<asp:ListItem Value="3409">S.C STORAGE TANK	</asp:ListItem>
					<asp:ListItem Value="3410">S.C EFFLUENT TREATMENT PLANT	</asp:ListItem>
					<asp:ListItem Value="3411">S.C EMPTY BUNCH TREATMENT PLANT	</asp:ListItem>
					<asp:ListItem Value="3412">S.C POWER PLANT ( DIESEL)	</asp:ListItem>
					<asp:ListItem Value="3413">S.C WATER TREATMENT PLANT	</asp:ListItem>
					<asp:ListItem Value="3414">S.C UMUM MESIN PABRIK	</asp:ListItem>
					<asp:ListItem Value="3415">S.C WEIGHBRIDGE	</asp:ListItem>
					<asp:ListItem Value="7001">PERSEDIAAN BARANG ASSET	</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdMatCode runat=server /></td>
					<td>
					<asp:DropDownList ID="txtProdMaterial" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All	</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblStkAnaCode runat=server /></td>
					<td>	
					<asp:DropDownList ID="txtStkAna" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All</asp:ListItem>
					</asp:DropDownList></td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>											
				<tr>
					<td>Item Code :</td>
					<td><asp:textbox id="txtItemCode" maxlength=20 width="50%" runat="server" /> 					    
					     <input type=button value=" ... " id="Find2" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');" CausesValidation=False runat=server />
						 <asp:Label id=lblErrItemCode forecolor=red visible=false text="Item code cannot be blank for detail report type"  runat=server/>(blank for all)
					     </td>					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblAccCode runat=server /></td>
						<td colspan=4>
                        <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" maxlength=20 width="20.5%"/>
						<input id="FindAcc" runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" type="button" value=" ... " />
                        <asp:TextBox ID="txtAccName" runat="server" BackColor="Transparent" BorderStyle="None" Font-Bold="False"   MaxLength="10" >(blank for all)</asp:TextBox>
						<asp:Label id=lblErrAccCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehType runat=server /></td>
					<td>
					<asp:DropDownList ID="txtVehType" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All	</asp:ListItem>
					<asp:ListItem Value="100">BENGKEL	</asp:ListItem>
					<asp:ListItem Value="101">WORKSHOP MILL	</asp:ListItem>
					<asp:ListItem Value="160">MOBIL SUPERVISI	</asp:ListItem>
					<asp:ListItem Value="170">MOBIL KECIL	</asp:ListItem>
					<asp:ListItem Value="180">BUS/AMBULANCE	</asp:ListItem>
					<asp:ListItem Value="209">ATV	</asp:ListItem>
					<asp:ListItem Value="210">TRAKTOR	</asp:ListItem>
					<asp:ListItem Value="220">DUMP TRUCK/LIGHT TRUCK	</asp:ListItem>
					<asp:ListItem Value="230">TRUCK TANKI	</asp:ListItem>
					<asp:ListItem Value="310">GREADER	</asp:ListItem>
					<asp:ListItem Value="320">COMPACTOR	</asp:ListItem>
					<asp:ListItem Value="330">EXCAVATOR	</asp:ListItem>
					<asp:ListItem Value="340">BACKHOE LOADER	</asp:ListItem>
					<asp:ListItem Value="350">DOZER	</asp:ListItem>
					<asp:ListItem Value="360">HOOKLIFT AND BINS	</asp:ListItem>
					<asp:ListItem Value="410">CRANE	</asp:ListItem>
					<asp:ListItem Value="420">WHEEL LOADER	</asp:ListItem>
					<asp:ListItem Value="430">FORKLIFT	</asp:ListItem>
					<asp:ListItem Value="510">TRAILER/TANGKI AIR	</asp:ListItem>
					<asp:ListItem Value="520">AGRICULTURAL ATTACHMENTS	</asp:ListItem>
					<asp:ListItem Value="610">SPEED BOAT	</asp:ListItem>
					<asp:ListItem Value="620">PONTOON/BARGE	</asp:ListItem>
					<asp:ListItem Value="710">KAPAL TERBANG KECIL	</asp:ListItem>
					<asp:ListItem Value="819">ELECTRICITY SUPPLY	</asp:ListItem>
					<asp:ListItem Value="820">POMPA AIR	</asp:ListItem>
					<asp:ListItem Value="821">POMPA AIR BIBITAN	</asp:ListItem>
					<asp:ListItem Value="829">WATER SUPPLY	</asp:ListItem>
					<asp:ListItem Value="830">COMPOUND	</asp:ListItem>
					<asp:ListItem Value="900">CHAIN SAW	</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td><asp:label id=lblVehCode runat=server /></td>
						<td colspan=4>
                        <asp:TextBox ID="txtVehCode" runat="server" AutoPostBack="True" maxlength=20 width="190px"/>
						<input id="FindVeh" runat="server" causesvalidation="False" onclick="javascript:PopVehAct('frmMain', 'txtVehName', 'txtVehCode', 'False');" type="button" value=" ... " />
                        <asp:TextBox ID="txtVehName" runat="server" BackColor="Transparent" BorderStyle="None" Font-Bold="False"   MaxLength="10" >(blank for all)</asp:TextBox>
						<asp:Label id=lblErrVehCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehExpCode runat=server /></td>
					<td>
					<asp:DropDownList ID="txtVehExpCode" size="1" width="50%" runat="server" >
					<asp:ListItem Value="">All	</asp:ListItem>
					</asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr style="display:none;">
					<td><asp:label id=lblBlkType runat=server /></td>
					<td><asp:DropDownList id="lstBlkType" AutoPostBack=true width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr id=TrBlkGrp style="display:none;">
					<td><asp:label id=lblBlkGrp runat=server /></td>
					<td><asp:textbox id="txtBlkGrp" maxlength="8" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>										
				</tr>										
				<tr id=TrBlk style="display:none;">
					<td><asp:label id=lblBlkCode runat=server /></td>
					<td><asp:textbox id="txtBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr id=TrSubBlk style="display:none;">
					<td><asp:label id=lblSubBlkCode runat=server /></td>
					<td><asp:textbox id="txtSubBlkCode" maxlength=15 width="50%" runat="server" /> (blank for all)</td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>						
				<tr style="display:none;">
					<td><asp:Label ID=lblBatchNo Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtBatchNo" MaxLength="2" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>	
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>	
														
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>					
				</tr>				
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
