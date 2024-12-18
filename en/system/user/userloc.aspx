<%@ Page Language="vb" src="../../../include/system_user_userloc.aspx.vb" Inherits="system_user_userloc"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

	<head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<title>User Location Access Rights</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Script language="JavaScript">
		function CA() {
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if ((e.name != 'cbFullRights') && (e.type=='checkbox') && (e.isDisabled==false))
					e.checked = document.frmMain.cbFullRights.checked;
			}
		}

		function CCA() {
			var TotalBoxes = 0;
			var TotalOn = 0;
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if ((e.name != 'cbFullRights') && (e.type=='checkbox')) {
					TotalBoxes++;
					if (e.checked) {
						TotalOn++;
					}
				}
			}
			if (TotalBoxes==TotalOn)
				{document.frmMain.cbFullRights.checked=true;}
			else
				{document.frmMain.cbFullRights.checked=false;}
		}
		</Script>		
	</head>
	<body onload="javascript:CCA();">
	<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 2000px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<table border=0 cellspacing="0" cellpadding=1 width="100%" class="font9Tahoma" >
		<tr>
			<td colspan="6">
				<UserControl:MenuSYS id=MenuSYS runat="server" />
			</td>
		</tr>
		<tr>
			<td class="font9Tahoma" colspan="6"><strong>USER LOCATION ACCESS RIGHTS</strong> </td>
		</tr>
		<tr>
			<td colspan=6><hr style="width :100%" /></td>
		</tr>
		<tr>
			<td colspan="6">Set User Location Access Right</td>
		</tr>
		<tr>
			<td colspan="6">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="6">
				<table border="0" cellpadding="4" cellspacing="0" width="100%" class="font9Tahoma" >
					<tr>
						<td width="9%" colspan="2">User ID :</td>
						<td width="41%">
							<asp:Label id=lblUserId runat=server />
						</td>
						<td width="13%" colspan="2">&nbsp;</td>
						<td width="37%">&nbsp;</td>
					</tr>
					<tr>
						<td colspan="2">Location :</td>
						<td>
							<asp:DropDownList id=ddlLocation autopostback=true onselectedindexchanged=onSelect_Location width=90% runat=server />
							<asp:Label id=lblErrLocation visible=false forecolor=red text="<br>No available location to add." runat=server/>
						</td>
						<td colspan="2">Access Expire on :</td>
						<td>
							<asp:textbox id=txtAccessExpire width=30% maxlength=10 runat=server /> 
							<a href="javascript:PopCal('txtAccessExpire');"><asp:Image id="btnSelPlantDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
							<asp:Label id=lblAccessExpire forecolor=red runat=server />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan=6>&nbsp;</td>
		</tr>
		<tr>
			<td colspan=6><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan="6">
				<table border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma" >
					<tr>
						<td colspan=3 colspan="3">Note : To grant all access rights, tick on the <u>Full Rights</u> checkbox.</td>
					</tr>
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CA();" id=cbFullRights text=" Full Rights" forecolor=red textalign=right runat=server /></td>
					</tr>
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					<tr>
						<td colspan=3>Note : To grant individual user access right , tick on the respective checkbox.</td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Inventory Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbINProdMaster text=" Product Type" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbINProdBrand text=" Product Brand" textalign=right runat=server />
							</td>
					</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%">
								<asp:CheckBox onclick="javascript:CCA();" id=cbINProdModel text=" Product Model" textalign=right runat=server />
							</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%">
								<asp:CheckBox onclick="javascript:CCA();" id=cbINProdCategory text=" Product Category" textalign=right runat=server />
							</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%">
								<asp:CheckBox onclick="javascript:CCA();" id=cbINProdMaterial text=" Product Material" textalign=right runat=server />
							</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%">
								<asp:CheckBox onclick="javascript:CCA();" id=cbINStockAnalysis  text=" Stock Analysis" textalign=right runat=server />
							</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%">
								<asp:CheckBox onclick="javascript:CCA();" id=cbINItemMaster text=" Stock Item Master" textalign=right runat=server />
							</td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbINItem text=" Stock Item" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbINDirChrg text=" Direct Charge Item" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbMiscItem text=" Miscellaneous Item" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id="cbINPR" text=" Purchase Request" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkRtnAdv text=" Stock Return Advice" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkTransfer text=" Stock Transfer" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkIsu text=" Stock Issue" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkRcv text=" Stock Receive" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkRtn text=" Stock Return" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkAdj text=" Stock Adjustment" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFuelIsu text=" Fuel Issue" textalign=right runat=server /></td>
					</tr>
					<!-- Millware; Assign item to Machine as a Preventive Maintenance transaction. PRM 24 Jul 2006 -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbItemToMachine text=" Assign Item To Machine" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkTransferInternal text=" Internal Stock Transfer" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtInv text=" Download/Upload Inventory Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbINMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Taxation Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTMaster text=" Tax Object Rate" textalign=right runat=server /></td>
					</tr>					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTItem text=" Supplier" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTPR text=" Tax Verification" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTRcv text=" Tax verified" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTRtnAdv text=" Pph 21" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbCTIsu text=" Pph 22" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbCTRtn text=" Pph 23" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbCTAdj text=" Pph 26" textalign=right runat=server /></td>
					</tr>
					
					<!--tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbCTTransfer text=" Canteen Transfer" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtCT text=" Download/Upload Canteen Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>						
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>
					</tr-->
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Workshop Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<!-- Millware; hide Workshop Item & Item Master. PRM 19 Jul 2006 -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSProdMaster text=" Product Type" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSProdBrand text=" Product Brand" textalign=right runat=server /></td>
						</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSProdModel text=" Product Model" textalign=right runat=server /></td>

						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSProdCategory text=" Product Category" textalign=right runat=server /></td>

						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSProdMaterial text=" Product Material" textalign=right runat=server /></td>

						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSStockAnalysis text=" Stock Analysis" textalign=right runat=server /></td>

						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSItemMaster text=" Workshop Item Master" textalign=right runat=server /></td>

						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSWorkMaster text=" Work Code" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSWorkshopService text=" Workshop Service" textalign=right runat=server /></td>

						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSItem text=" Workshop Item" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSPart text=" Workshop Item Part" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSDirChrg text=" Direct Charge Item" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSDirChrgMaster text=" Direct Charge Item Master" textalign=right runat=server /></td>
						</tr>
					<!-- Minamas CR-MNS0701010011; Preventive Maintenance 05 Oct 2006 PRM-->
				    <!-- start -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSMillProcDitr text=" Monthly Mill Process Distribution" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSCalMachine text=" Calendarized Machine" textalign=right runat=server /></td>
					</tr>				    
				    <!-- end -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSJob text=" Job Registration" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSMechHr text=" Mechanic Hour" textalign=right runat=server /></td>
					</tr>
					<!--<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSDN text=" Debit Note" textalign=right runat=server /></td>
					</tr>-->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtWS text=" Download/Upload Workshop Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>						
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
				
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2"><asp:Label id=cbNU text=" Activate Nursery Access Rights" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUMasterItem text=" Nursery Item Master" textalign=right runat=server /></td>
					</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUItem text=" Nursery Item" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUMasterSetup text=" Nursery Batch" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUCullType text=" Culling Type" textalign=right runat=server /></td>

						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUWorkAccDist text=" Working Account Distribution" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUSeedRcv text=" Seed Receive" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUSeedPlant text=" Seed Planting" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUDblTurn text=" Double Turn " textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUTransplanting text=" Seedlings Transplanting " textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUDispatch text=" Seedlings Dispatch " textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUCulling text=" Culling " textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUSeedIssue text=" Seedlings Issue" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtNu text=" Download/Upload Nursery Setup Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUMonthEnd text=" Month End" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Purchasing Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUSupp text=" Supplier" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUPelimpahan text=" Pelimpahan" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPURPH text=" RPH" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUPO text=" Purchase Order" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUGoodsRcv text=" Goods Receive" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUGRN text=" Goods Return" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUDA text=" Dispatch Advice" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtPU text=" Download/Upload Purchasing Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>						
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>


					<!-- start to add new cash & bank module. PRM 17 Mar 2006 -->
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Cash And Bank Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBPayment text=" Payment" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBReceipt text=" Receipt" textalign=right runat=server /></td>
					</tr>
					
					
					<!-- start to add new cash bank module. SMN 31 Aug 2006 -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBCashBank text=" Cash Bank" textalign=right runat=server /></td>
					</tr>
					<!-- end  -->
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBDeposit text=" Bank Reconsiliation" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBInterAdj text=" Saldo Bank" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBWithdrawal text=" Withdrawal" textalign=right runat=server /></td>
					</tr>
					
					<!-- start to add new cash bank module. SMN 31 Aug 2006 -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBCashFlow text=" Treasury Cash Flow Report" textalign=right runat=server /></td>
					</tr>
					<!-- end  -->
					
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>						
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCBMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>
					
					<!-- end -->
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Account Payable Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPInvoice text=" Invoice Receive" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPDN text=" Debit Note" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPCN text=" Credit Note" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPCrtJrn text=" Creditor Journal" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPPay text=" Payment" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Human Resource Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRDepartment text="Department Code" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRCompany text="Department Code, Department, Nationality" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRNationality text=" Nationality" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRFunc text=" Function" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRPosition text=" Position" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRLevel text=" Level" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRReligion text=" Religion" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRICType text=" IC Type" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRRace text=" Race" textalign=right runat=server /></td>

						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSkill text=" Skill" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRQualification text=" Qualification" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSubject text=" Subject" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREval text=" Evaluation" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRCPCode text=" CP Code" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSalScheme text=" Employee Category" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSalGrade text=" Salary Grade" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRShift text=" Shift" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRGang text=" Gang" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRBankFormat text=" Bank Format" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRBank text=" Bank" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRJamsostek text=" Jamsostek" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRTaxBranch text=" Tax Branch" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRTax text=" Tax" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRContSuper text=" Contractor Supervision" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRHoliday text=" Holiday Schedule" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRPublicHoliday text=" Public Holiday" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRPOH text=" Point Of Hired" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRCP text=" Career Progress" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpDet text=" Employee Details" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpPR text=" Employee Payroll" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpEmploy text=" Employee Employment" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSat text=" Employee Statutory/Others" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpFam text=" Employee Family" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpQlf text=" Employee Qualification" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpSkill text=" Employee Skill" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbGenEmpCode text=" Generate Employee Code" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtHR text=" Download/Upload Human Resource Reference Files" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Payroll Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRADGroup text=" Allowance and Deduction Group" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRAD text=" Allowance and Deduction" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRDenda text=" Denda" textalign=right runat=server /></td>
						</tr>
						<tr id=TrHarvInc runat=server>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRHarvInc text=" Harvesting Incentive" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRLoad text=" Load" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRRoute text=" Route" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMedical text=" Medical" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRAirBus text=" AirBus" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMaternity text=" Maternity" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRPensiun text=" Pensiun" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRRelocation text=" Relocation" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRSal text=" Attendance" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRContract text=" Contractor" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRRice text=" Rice Ration, Incentive, Quota Incentive" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRIncentive text=" Incentive" textalign=right runat=server /></td>
						</tr>
					<!-- New screen Employee Evaluation,Standard Evaluation & SalaryIncrease Minamas DIAN 26 June 2006-->
					<!-- start -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPREmployeeEvaluation text=" Employee Evaluation" textalign=right runat=server /></td>

					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRStandardEvaluation text=" Standard Evaluation" textalign=right runat=server /></td>

					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRSalaryIncrease text=" Salary Increase" textalign=right runat=server /></td>

					</tr>
					<!-- end -->
					<!-- New screen Transport Incentive Minamas DIAN FS 2.19 millware Ph1 part 1 20 July 2006-->
						<!-- start -->
						<tr id=TrTranInc runat=server>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRTranInc text=" Transport Incentive" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<!-- end -->
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRPaySetup text=" Payroll Setup" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRDailyAttd text=" Daily Attendance" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRHarvAttd text=" Harvester Production Attendance" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRWeekly text=" Weekly Attendance" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRAttdTrx text=" Attendance Checkroll" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRTripTrx text=" Trip" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRRatePay text=" Piece Rate Payment" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRContractPay text=" Contract Payment" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRADTrx text=" Allowance and Deduction" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRWagesPay text=" Wages Payment" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRContCheckroll text=" Contractor Checkroll" textalign=right runat=server /></td>
					</tr>
					<!-- New screen Work Performance Minamas PRM 19 Apr 2006-->
					<!-- start -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRWorkPerformance text=" Work Performance" textalign=right runat=server /></td>
					</tr>
					<!-- end -->
					<!-- New screen Work Performance Contractor Minamas PRM 28 Jun 2006-->
					<!-- start -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRWPContractor text=" Work Performance Contractor" textalign=right runat=server /></td>
					</tr>
					<!-- end -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtPR text=" Download/Upload Payroll Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDwPR text=" Download Statutory" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDwPRWages text=" Download/Upload Wages" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDwPRBankAuto text=" Download Bank Auto-Credit Files" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDwAttdInterface text=" Attendance System Interface" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthRice text=" Rice Ration Process" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthRapel text=" Rapel Process" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthBonus text=" Bonus Process" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthTHR text=" THR Process" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthDaily text=" Daily Process/Rollback" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthPayroll text=" Payroll Process/Rollback" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthTransfer text=" Transfer Interface" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Year End</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRYearEnd text=" Year End Process" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Account Receivable Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup / Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBillParty text=" Bill Party"  textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIInvoice text=" Invoice" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBINote text=" Debit Note" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBICreditNote text=" Credit Note" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIJournal text=" Debtor Journal" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIReceipt text=" Receipt" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtBI text=" Download/Upload Billing Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>						
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbBIMthEnd onclick="javascript:CCA();" text=" Month End Process" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Fixed Asset Access Rights</td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAClassSetup text=" Asset Classification" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAGroupSetup text=" Asset Group" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFARegSetup text=" Asset Registration Header" textalign=right runat=server /></td>
					</tr>	
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFARegLine text=" Asset Registration Line" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAPermissionSetup text=" Asset Permission" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAAssetMasterSetup text=" Asset Master" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAAssetItemSetup text=" Asset Item" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>		
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAAddition text=" Asset Addition" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFADepreciation text=" Asset Depreciation" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFADisposal text=" Asset Disposal" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAWriteOff text=" Asset Write Off" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAdtdw text=" Download Reference Files" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAdtup text=" Upload Reference Files" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAGenDepreciation text=" Generate Depreciation" textalign=right runat=server /></td>
					</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAMonthEnd text=" Month End" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2"><asp:Label id=cbWM text=" Weighing Management Access Rights" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMTransport text=" Transport Master" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMTicket text=" WeighBridge Ticket" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMFFBAssessment text=" FFB Assessment" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMDataTransfer text=" Download/Upload Data" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2"><asp:Label  id=cbCM text=" Contract Management Access Rights" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMMasterSetup text=" Currency" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMExchangeRate text=" Exchange Rate" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMContractQuality text=" Contract Quality" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMClaimQuality text=" Claim Quality" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMContractReg text=" Contract Registration" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMContractMatch text=" Contract Matching" textalign=right runat=server /></td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMGenDNCN text=" Genereate Debit or Credit Note from Production MPOB Price " textalign=right runat=server visible="false"/></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMContractDOReg text=" DO Registration" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMDataTransfer text=" Download/Upload Data" textalign=right runat=server /></td>
					</tr>					
										
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">
							Production Access Rights
							<asp:label id=cbPM text=" Mill Production Management Access Rights" textalign=right visible=false runat=server />
						</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMMasterSetup text=" Ullage - Volume Table Master" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMVolConvMaster text=" Ullage - Volume Conversion Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMAvgCapConvMaster text=" Ullage - Average Capacity Conversion Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMCPOPropertyMaster text=" CPO Properties Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMStorageTypeMaster text=" Storage Type Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMStorageAreaMaster text=" Storage Area Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMProcessingLineMaster text=" Processing Line Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMMachineMaster text=" Machine Master" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMAcceptableOilQuality text=" Acceptable Oil Quality" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMAcceptableKernelQuality text=" Acceptable Kernel Quality" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMTestSample text=" Test Sample" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMHarvestingInterval text=" Harvesting Interval" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMMill text=" Mill" textalign=right runat=server /></td>
					</tr>
					<!--SMN, 28 SEP 2006-->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMMachineCriteria text=" Machine Criteria" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbEstProd text=" Estate Production" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPOMProd text=" POM Storage" textalign=right runat=server /></td>
						<!--<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbMPOBPrice text=" MPOB Price" textalign=right runat=server visible = "false"/></td>-->
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPOMStorage text=" POM Storage" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPOMStat text=" POM Statistic" textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMDailyProd text=" Daily Production" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMCPOStore text=" CPO Storage" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMPKStore text=" Palm Kernel Storage" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMOilLoss text=" Oil Loss" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMOilQuality text=" Oil Quality" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMKernelQuality text=" Kernel Quality" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMProdKernel text=" Produced Kernel" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMDispKernel text=" Dispatched Kernel" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMWater text=" Water Quality" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMNutFibre text=" Nut To Fibre Ratio Analysis" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbYearPlantYield text=" Year of Planting Yield" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id="cbPMKernelLoss" text=" Kernel Loss" textalign="right"
										runat="server" /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id="cbPMWastedWaterQuality" text=" Wasted Water Quality"
										textalign="right" runat="server" /></td>
					</tr>
					

					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMDayEnd text=" Day End Process" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%">
							<asp:CheckBox onclick="javascript:CCA();" id=cbPDMthEnd text=" Month End Process" textalign=right runat=server />
							<asp:CheckBox onclick="javascript:CCA();" id=cbPMMthEnd text=" Month End Process" textalign=right visible=false runat=server />
						</td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">General Ledger Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Setup</td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAccClsGrp textalign=right runat=server /></td>
						</tr>	
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAccCls text=" Account Class" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbActGrp textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAct text=" Activity" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbSubAct textalign=right runat=server /></td>
						</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbVehExpGrp textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbVehExp text=" Vehicle Expense Code" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbVeh text=" Vehicle" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbVehType text=" Vehicle Type" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBlkGrp text=" Block Group" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBlk text=" Block" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbSubBlk textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbExp text=" Expense Code" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAccountGrp textalign=right runat=server /></td>
						</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbAccount text=" Chart of Account Group, Chart of Account" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbEntrySetup text=" Double Entry Setup" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbBalSheetSetup text=" Balance Sheet Report Setup" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbProfLossSetup text=" Profit and Loss Report Setup" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbGLCOGS text=" Cost of Goods Sold Setup" textalign=right runat=server /></td>
					</tr>
					<!-- ALIM 2 Feb 2007 -->
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbFSSetup text=" Financial Statement Report Setup" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbJrn text=" Journal Entry" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbPosting text=" Posting" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbJrnAdj text=" Journal Adjustment" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbVehUsg text=" Vehicle Usage" textalign=right runat=server /></td>
					</tr>
					<!-- Millware; Actual station running hours as a Preventive Maintenance transaction. PRM 24 Jul 2006 -->
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbRunHour text=" Actual Station Running Hour" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtGL text=" Download/Upload General Ledger Reference Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtUp text=" Download/Upload Modules Transaction Files" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Month End</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbGLGCDist text=" General Charges Distribution" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbGLJrnMthEnd text=" Journal Adjustment Process" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbGLMthEnd text=" Month End Process" textalign=right runat=server /></td>
					</tr>

					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Reconciliation Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Transaction</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbRCDA text=" Dispatch Advice" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbRCJrn text=" Journal" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Interface</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbReadInterRC text=" Read Interface From 3rd Party Application" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox id=cbSendInterRC text=" Send Interface To 3rd Party Application" textalign=right runat=server /></td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2">Data Transfer</td>

					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="90%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtRC text=" Download/Upload Reconciliation Reference Files" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>	
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Budgeting Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbBudgeting text=" Budgeting" textalign=right runat=server /></td>
					</tr>
					
					<tr>
						<td colspan=3>&nbsp;</td>
					</tr>
					
					<tr class="mr-h">
						<td width="5%">&nbsp;</td>
						<td width="95%" class="NormalBold" colspan="2">Administration Access Rights</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="95%" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbAccPeriod text=" Accounting Period" textalign=right runat=server /></td>
					</tr>
					<tr>
							<td width="5%">&nbsp;</td>
							<td width="95%" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbPeriodCfg text=" Period Configuration" textalign=right runat=server /></td>
					</tr>							
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="6">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="6">
				<asp:ImageButton id=SaveBtn imageurl="../../images/butt_save.gif" AlternateText=Save onClick=SaveBtn_Click CommandArgument=Save runat=server />
				<asp:ImageButton id=DelBtn imageurl="../../images/butt_delete.gif" AlternateText= onClick=SaveBtn_Click CommandArgument=Del runat=server />
				<asp:ImageButton id=BackBtn imageurl="../../images/butt_back.gif" AlternateText=Back onClick=BackBtn_Click runat=server />
			</td>
		</tr>
		<input type=hidden id=hidUserId runat=server />
		<input type=hidden id=hidLocation runat=server />
		<input type=hidden id=hidAccPeriod runat=server />
		<input type=hidden id=hidPeriodCfg runat=server />
		<asp:Label id=lblErrInvalid visible=false Text="<br>Date format should be in " runat=server />
		<asp:Label id=lblErrEarlyThen visible=false Text="<br>Access expire date must not early then today." runat=server />
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
		
		<asp:label id=lblMaster visible=false text=" Master" runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblAnd visible=false text=" and " runat=server />
		<asp:label id=lblNationality visible=false text=" Nationality" runat=server />
		<asp:label id=lblPosition visible=false text="Position" runat=server />
		<asp:label id=lblICType visible=false text="IC Type" runat=server />
		<asp:label id=lblEvaluation visible=false text="Evaluation" runat=server />
		<asp:label id=lblSalary visible=false text=" " runat=server />
		<asp:label id=lblBank visible=false text=" Bank " runat=server />
		<asp:label id=lblTax visible=false text=" Tax" runat=server />
		<asp:label id=lblAD visible=false text=" Allowance and Deduction" runat=server />
		
		</table>
                </div>
            </td>
            </tr>
            </table>
	</form>
</body>

</html>
