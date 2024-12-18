<%@ Page Language="vb" src="../../../include/system_config_sysloc.aspx.vb" Inherits="system_config_sysloc"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>System Location Configuration</title>
		<Script language="JavaScript">
		function CA() {
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if ((e.name != 'cbFullRights') && (e.type=='checkbox'))
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
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body onload="javascript:CCA();">
		<form id=frmMain runat=server>
		<TABLE cellSpacing="0" cellPadding="3" width="100%" border="0">
			<tr>
				<td colspan="2">
					<UserControl:MenuSYS id=MenuSYS runat="server" />
				</td>
			</tr>
			<TR>
				<TD class="mt-h" colspan="2">SYSTEM CONFIGURATION</TD>
			</TR>
			<tr>
				<td colspan=2><hr size="1" noshade></td>
			</tr>
			<TR>
				<TD><asp:label id="lblComp" runat="server" /> :</TD>
				<TD><asp:Label id=lblCompany runat=server /></TD>
			</TR>
			<TR>
				<TD Width=20%>Select <asp:label id="lblLocation" runat="server" /> :</TD>
				<TD Width=80%>
					<asp:DropDownList id=ddlLocation width=30% runat=server />
					<asp:Label id=lblNoLocError forecolor=red text="There is no location associated with the company." runat=server />
					<asp:Label id=lblLocError forecolor=red text="Please select one location." runat=server />
				</TD>
			</TR>
			<TR>
				<TD valign=top>No of Months :</TD>
				<TD>
					<asp:TextBox id=txtDoc Width=10% maxlength=2 runat=server />
					<asp:RangeValidator display=dynamic id="numericDocRetain"
						ControlToValidate="txtDoc"
						MinimumValue="1"
						MaximumValue="99"
						Type="Integer"
						EnableClientScript="true"
						Text="The value must be from 1 to 99. "
						runat="server"/>
				<br />Please indicate how long you wish to retain the application historical/transaction data. 
				The data will not be purged/removed from the application if you leave it blank.
				</TD>
			</TR>
			<TR>
				<TD colSpan="2">
					&nbsp;
				</TD>
			</TR>
			<TR>
				<TD colSpan="2">
					<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td width="100%" colspan="7">Note : To check all the checkboxes (activate module, access rights, automate posting), tick on the <u>Check ALL</u> checkbox.</td>
					</tr>
					<tr>
						<td colspan=5>&nbsp;</td>
					</tr>
					<tr>
						<td width="5%">&nbsp;</td>
						<td width="4%">&nbsp;</td>
						<td width="59%"><asp:CheckBox onclick="javascript:CA();" id=cbFullRights text=" Check ALL" forecolor=red textalign=right runat=server /></td>
						<td width="6%">&nbsp;</td>
						<td width="5%">&nbsp;</td>
						<td width="17%">&nbsp;</td>
						<td width="4%">&nbsp;</td>
					</tr>
					<tr>
						<td colspan=5>&nbsp;</td>
					</tr>
						<tr>
							<td width="68%" colspan="3">Note : To grant the application access right , click the respective checkbox on left hand side.</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbIN text=" Activate Inventory Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="91%" colspan=5>
								<asp:CheckBox onclick="javascript:CCA();" id=cbINProdMaster textalign=right runat=server />
							</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%">
								<asp:CheckBox onclick="javascript:CCA();" id=cbINItem textalign=right runat=server />
							</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbINDirChrg textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbMiscItem text=" Miscellaneous Item" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id="cbINPR" text=" Purchase Requisition" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkRtnAdv text=" Stock Return Advice" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkTransfer text=" Stock Transfer" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkIsu text=" Stock Issue" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkRcv text=" Stock Receive" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkRtn text=" Stock Return" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbStkAdj text=" Stock Adjustment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFuelIsu text=" Fuel Issue" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtInv text=" Download/Upload Inventory Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwIN text=" Download Inventory Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbINMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbCT text=" Activate Canteen Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTItem text=" Canteen Item" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTPR text=" Purchase Requisition" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTRcv text=" Canteen Receive" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTRtnAdv text=" Canteen Return Advice" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTIsu text=" Canteen Issue" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTRtn text=" Canteen Return" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTAdj text=" Canteen Adjustment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTTransfer text=" Canteen Transfer" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtCT text=" Download/Upload Canteen Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwCT text=" Download Canteen Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCTMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						

						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbWS text=" Activate Workshop Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSProdMaster text=" Product Type, Brand, Model, Category, Material and, Stock Analysis, Workshop Item Master" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSWorkMaster text=" Work Code, Workshop Service" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSItem text=" Workshop Item" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSDirChrg text=" Direct Charge Item" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSJob text=" Job Registration" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSMechHr text=" Mechanic Hour" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSDN text=" Debit Note" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtWS text=" Download/Upload Workshop Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwWS text=" Download Workshop Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWSMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbPU text=" Activate Purchasing Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUSupp text=" Supplier" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUPO text=" Purchase Order" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUGoodsRcv text=" Goods Receive" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUGRN text=" Goods Return Note" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUDA text=" Dispatch Advice" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtPU text=" Download/Upload Purchasing Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwPU text=" Download Purchasing Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPUMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbAP text=" Activate Account Payable Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPInvoice text=" Invoice Receive" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPDN text=" Debit Note" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPCN text=" Credit Note" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPCrtJrn text=" Creditor Journal" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPPay text=" Payment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwAP text=" Download Account Payable Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAPMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbHR text=" Activate Human Resource Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRCompany textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRFunc text=" Function, Position, Level, Religion, IC Type, Race" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSkill text=" Skill, Qualification, Subject" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREval text=" Evaluation, Career Progress, Salary Scheme, Salary Grade, Gang" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRBank text=" Bank Format, Bank, Tax Branch, EPF, Tax, Socso, MPOB Bonus, Shift" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRHoliday text=" General Public Holiday, Holiday Schedule" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRCP textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpDet text=" Employee Details" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpPR text=" Employee Payroll" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpEmploy text=" Employee Employment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHRSat text=" Employee Statutory/Others" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpFam text=" Employee Family" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpQlf text=" Employee Qualification" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbHREmpSkill text=" Employee Skill" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbGenEmpCode text=" Generate Employee Code" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtHR text=" Download/Upload Human Resource Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>

						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbPR text=" Activate Payroll Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="91%" colspan=5><asp:CheckBox onclick="javascript:CCA();" id=cbPRAD text=" Allowance and Deduction Group, Allowance and Deduction, Attendance Incentive, Harvesting Incentive, Load, Route" textalign=right runat=server /></td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRSal text=" Attendance" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRContract text=" Contractor" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRAttdTrx text=" Daily Attendance, Batch Attendance" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRTripTrx text=" Trip" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRRatePay text=" Piece Rate Payment, Contract Payment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRADTrx text=" Allowance and Deduction, Wages Payment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtPR text=" Download/Upload Payroll Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDwPR text=" Download Payroll Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Year End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPRYearEnd text=" Year End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>

						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbBI text=" Activate Account Receivable Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup / Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBillParty textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIInvoice text=" Invoice" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBINote text=" Debit Note, Credit Note" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIReceipt text=" Receipt" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIJournal text=" Debtor Journal" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtBI text=" Download/Upload Billing Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwBI text=" Download Billing Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBIMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>

						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbPD text=" Activate Production Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup / Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbEstProd text=" Estate Production" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPOMProd text="POM Storage, POM Production, POM Statistic " textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtDwPD text=" Download Production Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPDMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbGL text=" Activate General Ledger Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAccCls textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAct textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbVehExp textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbVeh textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBlk textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbExp textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbAccount textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbEntrySetup text=" Double Entry Setup" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbBalSheetSetup text=" Balance Sheet Report Setup" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbProfLossSetup text=" Profit and Loss Report Setup" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbJrn text=" Journal Entry, Posting" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbVehUsg text=" Vehicle Usage" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtGL text=" Download/Upload General Ledger Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtUp text=" Download/Upload Modules Transaction Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbGLMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>

						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbRC text=" Activate Reconciliation Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbRCDA text=" Dispatch Advice" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbRCJrn text=" Journal" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Interface</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbReadInterRC text=" Read Interface From 3rd Party Application" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbSendInterRC text=" Send Interface To 3rd Party Application" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbDtRC text=" Download/Upload Reconciliation Reference Files" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbWM text=" Activate Weighing Management Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMTransport text=" Transport Master" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMTicket text=" WeighBridge Ticket" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMFFBAssessment text=" FFB Assessment" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbWMDataTransfer text=" Download/Upload Data" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>

						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbPM text=" Activate Mill Production Management Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMMasterSetup text=" Master Setup" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMDailyProd text=" Daily Production" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMCPOStore text=" CPO Storage" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMPKStore text=" Palm Kernel Storage" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMOilLoss text=" Oil Loss" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMOilQuality text=" Oil Quality" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMKernelQuality text=" Kernel Quality" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMProdKernel text=" Produced Kernel" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMDispKernel text=" Dispatched Kernel" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMWater text=" Water Quality" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMNutFibre text=" Nut To Fibre Ratio Analysis" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMDayEnd text=" Day End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbPMMthEnd text=" Month End Process" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbCM text=" Activate Palm Product Contract Management Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMMasterSetup text=" Master Setup" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMContractReg text=" Contract Registration" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMContractMatch text=" Contract Matching" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMMPOB text=" MPOB Price " textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Data Transfer</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbCMDataTransfer text=" Download/Upload Data" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbNU text=" Activate Nursery Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUMasterSetup text=" Master Setup" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUWorkAccDist text=" Working Account Distribution" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUSeedRcv text=" Seed Receive" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUSeedPlant text=" Seed Planting" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUDblTurn text=" Double Turn " textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUTransplanting text=" Seedlings Transplanting " textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUDispatch text=" Seedlings Dispatch " textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUCulling text=" Culling " textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbNUMonthEnd text=" Month End" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>	
						
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbBudgeting text=" Activate Budgeting Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbFA text=" Activate Fixed Asset Access Rights" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Setup</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAClassSetup text=" Asset Classification" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAGroupSetup text=" Asset Group" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFARegSetup text=" Asset Registration" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAPermissionSetup text=" Asset Permission" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Transaction</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>		
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAAddition text=" Asset Addition" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFADepreciation text=" Asset Depreciation" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFADisposal text=" Asset Disposal" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAWriteOff text=" Asset Write Off" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2">Month End</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAGenDepreciation text=" Generate Depreciation" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>	
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
							<td width="59%"><asp:CheckBox onclick="javascript:CCA();" id=cbFAMonthEnd text=" Month End" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
							
						<tr class="mr-h">
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2">Administration Access Right</td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbAccPeriod text=" Accounting Period" textalign=right runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
												
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2">
					<HR width="100%" SIZE="1">
				</TD>
			</TR>
			<TR>
				<TD colSpan="2">
					<table border="0" cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td width="100%" colspan="6">
								Note : Automate the modules' transaction posting to General Ledger. 
								If module is set to inactive, the automate process will be ignored.
							</td>
						</tr>
						<tr>
							<td colspan=5>&nbsp;</td>
						</tr>
						<TD colspan=5>
							<asp:ImageButton id=SaveBtn imageurl="../../images/butt_save.gif" alternatetext=Save onclick=SaveBtn_Click runat=server /> 
							<asp:ImageButton id=DeleteBtn causesvalidation="false" imageurl="../../images/butt_delete.gif" alternatetext=Delete onclick=DelBtn_Click runat=server />
							<asp:ImageButton id=BackBtn causesvalidation="false" imageurl="../../images/butt_back.gif" alternatetext=Back onclick=BackBtn_Click runat=server />
						</TD>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTIN text=" Automate Inventory Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTCT text=" Automate Canteen Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTWS text=" Automate Workshop Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTPU text=" Automate Purchasing Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTAP text=" Automate Account Payable Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTPR text=" Automate Payroll Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTPD text=" Automate Production Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTBI text=" Automate Billing Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
						<tr>
							<td width="5%">&nbsp;</td>
							<td width="63%" class="NormalBold" colspan="2"><asp:CheckBox onclick="javascript:CCA();" id=cbADTGL text=" Automate General Ledger Posting" textalign=right visible=false checked=true runat=server /></td>
							<td width="6%">&nbsp;</td>
							<td width="5%">&nbsp;</td>
							<td width="17%">&nbsp;</td>
							<td width="4%">&nbsp;</td>
						</tr>
					</table>
				</TD>
			</TR>
			<TR>
				<TD colspan=2>&nbsp;</TD>
			</TR>
			<TR>
				<TD colspan=2>
				</TD>
			</TR>
			<input type=hidden id=hidCompName value="" runat=server />
			<input type=hidden id=hidCompCode value="" runat=server />
			<input type=hidden id=hidLocCode runat=server />
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select one " runat=server />
			<asp:label id=lblMaster visible=false text=" Master" runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblAnd visible=false text=" and " runat=server />
			<asp:label id=lblNationality visible=false text=" Nationality" runat=server />
			<asp:label id=lblPosition visible=false text="Position" runat=server />
			<asp:label id=lblICType visible=false text="IC Type and Race" runat=server />
			<asp:label id=lblEvaluation visible=false text="Evaluation" runat=server />
			<asp:label id=lblSalary visible=false text="Salary Scheme, Salary Grade and Overtime" runat=server />
			<asp:label id=lblAD visible=false text="Allowance and Deduction Group, Allowance and Deduction, Attendance Incentive, Harvesting Incentive" runat=server />
			<asp:Label id=lblINARExt visible=false runat=server />
			<asp:Label id=lblCTARExt visible=false runat=server />
			<asp:Label id=lblWSARExt visible=false runat=server />
			<asp:Label id=lblPUARExt visible=false runat=server />
			<asp:Label id=lblAPARExt visible=false runat=server />
			<asp:Label id=lblPRARExt visible=false runat=server />
			<asp:Label id=lblHRARExt visible=false runat=server />
			<asp:Label id=lblBIARExt visible=false runat=server />
			<asp:Label id=lblPDARExt visible=false runat=server />
			<asp:Label id=lblGLARExt visible=false runat=server />
			<asp:Label id=lblWMARExt visible=false runat=server />
			<asp:Label id=lblPMARExt visible=false runat=server />
			<asp:Label id=lblCMARExt visible=false runat=server />
			<asp:Label id=lblNUARExt visible=false runat=server />
			<asp:Label id=lblFAARExt visible=false runat=server />
			<asp:Label id=lblADARExt visible=false runat=server />
		</TABLE>
		</form>
	</body>
</html>
