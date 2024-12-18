<%@ Page Language="vb" codefile="../../../include/PU_trx_PODet.aspx.vb" Inherits="PU_PODet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Purchase Order Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style3
            {
                height: 26px;
                width: 22%;
            }
            .style7
            {
                height: 111px;
                width: 22%;
            }
            .style8
            {
                height: 10px;
                width: 22%;
            }
            .style9
            {
                height: 25px;
                width: 22%;
            }
            .style10
            {
                width: 22%;
            }
            .style11
            {
                width: 85px;
            }
            </style>
	</head>
	<script language="javascript">

	    function pageLoad() {
	        nqty = $find("<%=txtQtyOrder.ClientID%>");
	        nharga = $find("<%=txtCost.ClientID%>");
	        nsubtotal = $find("<%=txtTtlCost.ClientID%>");
	       

	    }

	    ; (function () {
	        var hitung = window.hitung = {};

	        hitung.valueChanged = function (sender, args) {
	            var subtotal = nqty.get_value() * nharga.get_value()
	            nsubtotal.set_value(subtotal);	          
	        }

	        var bagi = window.bagi = {};

	        bagi.valueChanged = function (sender, args) {
	            var unitcost = nsubtotal.get_value()/nqty.get_value()
	            nharga.set_value(unitcost);	          
	        }
                         
	    })();

	        function ChkCreditTerm() {
				var doc = document.frmMain;
				var arrSupplier, sngSupplier, sngEnterSupplier;
				var strDisplay = "none";
				//var x = doc.txtCost.value;
				//sngEnterUnitCost = parseFloat(x.toString().replace(/,/gi,""));
				
				if (doc.ddlSuppCode.selectedIndex > 0) {
				    arrSupplier = doc.ddlSuppCode.options[doc.ddlSuppCode.selectedIndex].text.split(" - ");
				    doc.txtCreditTerm.value = arrSupplier[1].replace(" days","");
				}
				
				//errUnmatchCost.style.display = strDisplay;
				
				//unitcostArr = Split(doc.lstItem.SelectedItem.Text, " $");
				//SelPOUnitCost = parseFloat(Trim(unitcostArr(1)));

				//If (parseFloat(Trim(doc.txtCost.Text)) <> SelPOUnitCost) 
				//doc.errUnmatchCost.Visible = True;
			}		
			function calUnitCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder.value);
				var b = parseFloat(doc.txtTtlCost.value);
				doc.txtCost.value = (b / a);
				if (doc.txtCost.value == 'NaN')
					doc.txtCost.value = '';
				else
				    doc.txtCost.value = format_number(round(doc.txtCost.value, 2), 2);
			}			
			function calTtlCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder.value);
				var b = parseFloat(doc.txtCost.value);
				doc.txtTtlCost.value = (a * b);
				if (doc.txtTtlCost.value == 'NaN')
					doc.txtTtlCost.value = '';
				else
				    doc.txtTtlCost.value = format_number(round(doc.txtTtlCost.value, 2), 2);
			}
			function calTtlQtyCost() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder.value);
				var b = parseFloat(doc.txtCost.value);
				doc.txtTtlCost.value = (a * b);
				if (doc.txtTtlCost.value == 'NaN')
					doc.txtTtlCost.value = '';
				else
				    doc.txtTtlCost.value = round(doc.txtTtlCost.value, 2);
			}	
			
			function calAddDiscount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.hidTtlAmount.value);
				var b = parseFloat(doc.txtAddDisc.value);
				doc.txtTtlAftDisc.value = a - b;
				if (doc.txtAddDisc.value == 'NaN')
				    doc.txtTtlAftDisc.value = doc.hidTtlAmount.value
				else
				    doc.txtTtlAftDisc.value = round(doc.txtTtlAftDisc.value, 2);
			}
			function format_number(pnumber,decimals){
	            if (isNaN(pnumber)) { return 0};
	            if (pnumber=='') { return 0};
            	
	            var snum = new String(pnumber);
	            var sec = snum.split('.');
	            var whole = parseFloat(sec[0]);
	            var result = '';
            	
	            if(sec.length > 1){
		            var dec = new String(sec[1]);
		            dec = String(parseFloat(sec[1])/Math.pow(10,(dec.length - decimals)));
		            dec = String(whole + Math.round(parseFloat(dec))/Math.pow(10,decimals));
		            var dot = dec.indexOf('.');
		            if(dot == -1){
			            dec += '.'; 
			            dot = dec.indexOf('.');
		            }
		            while(dec.length <= dot + decimals) { dec += '0'; }
		            result = dec;
	            } else{
		            var dot;
		            var dec = new String(whole);
		            dec += '.';
		            dot = dec.indexOf('.');		
		            while(dec.length <= dot + decimals) { dec += '0'; }
		            result = dec;
	            }	
	            return result;
            }
            
	</script>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1000px" valign="top">
			    <div class="kontenlist">  

			<asp:Label id=lblHidStatus visible=false runat=server />
			<asp:Label id=lblHidStatusEdited visible=false runat=server />
			<asp:Label id=lblHidPOLnID visible=false runat=server />
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />
			<asp:label id=lblSelectListLoc visible=false text="Please Select Purchase Requisition Ref " runat="server"/>
			<asp:label id=lblSelectListItem visible=false text="Please Select " runat="server" />
			<asp:label id=lblPR visible=false text="PR " runat="server" />
			<asp:label id=lblPRRef visible=false text="PR Ref. " runat="server" />
			<input type=hidden id=hidOrgQtyOrder runat=server />
			<input type=hidden id=hidTtlAmount runat=server/>	
			<input type=hidden id=hidAddDisc runat=server/>					 					 	
            <input type=hidden id=hidTtlAmtAftDisc runat=server/>		
            <input type=hidden id=hidOriCost value=0 runat=server/>		
            <input type=hidden id=hidOriCostOA value=0 runat=server/>		
            <input type=hidden id=hidPPN value=0 runat=server/>	
            <input type=hidden id=hidPPNOA value=0 runat=server/>	            
            <input type=hidden id=hidGetRefNo value=0 runat=server/>	            
            <input type=hidden id=hidBlkCode value="" runat=server/>	            
            <input type=hidden id=hidRetensi value=0 runat=server/>	            
            <input type=hidden id=hidSuppType value="" runat=server/>	            
                                        			 				 	
			    <table border="0" cellspacing="0" cellpadding="0" width="100%"  class="font9Tahoma">
				    <tr>
					<td colspan="5" style="height: 25px"><UserControl:MenuPU id=menuPU runat="server" />
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr  class="font9Tahoma">
                                <td>
                        <strong>PURCHASE ORDER DETAILS</strong></td>
                                <td class="font9Header" style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				    </tr>			
    <tr>
		<td height="25" class="style10">Purchase Order ID :</td>
		<td width="40%"><asp:Label id=lblPOId runat=server /></td>
		<td width="5%">&nbsp;</td>
		<td width="15%">Period :&nbsp;&nbsp;</td>
		<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
					
	</tr>
				                                        <tr>
		<td height=25 class="style10">Purchase Order Date :</td>
		<td><asp:TextBox id=txtDateCreated width=20% maxlength="10" CssClass="fontObject"  runat="server"/>
			<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
			<asp:RequiredFieldValidator	id="rfvDateCreated" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
			<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
			<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
		</td>
		<td>&nbsp;</td>
		<td>Status :</td>
		<td> <asp:Label id=lblStatus runat=server />
                                                            </td>
					
	</tr>	
				                        <tr>
		<td height="25" class="style10">Purchase Order Type :</td>
		<td><asp:label id=lblPOTypeName runat=server />
			<asp:label id=lblPOType visible=false runat=server /></td>
		<td>&nbsp;</td>
		<td>Date Created :</td>
		<td> <asp:Label id=lblCreateDate runat=server /></td>
					
	</tr>			
				                                                    <tr>
		<td height="25" class="style10">Purchase Order Level :*</td>
		<td><asp:DropDownList id="ddlPOLevel" width=100%  CssClass="fontObject" runat=server 
                  />
			<asp:Label id=lblErrPRLevel visible=false forecolor=red text="<br>Please select PR Level." runat=server/>
			<asp:RequiredFieldValidator id="RequiredFieldValidator7" 
										runat="server" 
										ErrorMessage="Please Specify Supplier Code" 
										ControlToValidate="ddlPOLevel" 
										display="dynamic"/>
		</td>
		<td>&nbsp;</td>
		<td>
            PO Created :</td>
		<td> <asp:Label id=lblPOCreated runat=server /></td>					    
	</tr>			

				                            <tr>
		<td height="25" class="style10">Dept Code :*</td>
		<td><asp:DropDownList id="ddlDeptCode" width=100% runat=server 
                CssClass="fontObject" />
			<asp:Label id=lblDeptCode text="Please Select Dept Code" forecolor=red visible=false runat=server />
		</td>								
		<td>&nbsp;</td>
		<td>Updated By : </td>
		<td> <asp:Label id=lblUpdateBy runat=server /></td>			
	</tr>	
				                                                <tr>
		<td height="25" class="style10">Supplier Code :*</td>
		<td><asp:TextBox ID="txtSupCode"  CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15" Width="88%"></asp:TextBox>
			<!--<input type=button value=" ... " id="Find" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','ddlPPN','txtPPNInit', 'False');"  causesvalidation="False" runat="server"/>-->
            <asp:Button ID="FindSupplierButton" CssClass="button-small" runat="server" Text="..." OnClientClick = "javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','ddlPPN','txtPPNInit', 'False');" CausesValidation="false" />
			<asp:Label id=lblSuppCode text="Please Select Supplier Code" forecolor=red visible=false runat=server />
			<asp:RequiredFieldValidator id="validateSuppCode" 
										runat="server" 
										ErrorMessage="Please Specify Supplier Code" 
										ControlToValidate="txtSupCode" 
										display="dynamic"/>&nbsp;</td>	
		<td>&nbsp;</td>
		<td>Last Update :</td>
		<td><asp:Label id=lblUpdateDate runat=server /></td>	
	</tr>	
				        <tr>
		<td class="style10"></td>
		<td><asp:Label ID="WarningGantiSupplier" Visible=false Text="Jika ingin mengganti supplier terlebih dahulu delete semua item di PO!" runat="server" ForeColor="Red"></asp:Label></td>
		<td colspan="3"></td>
	</tr>		
				                <tr>
		<td height="25" class="style10"></td>
		<td><asp:TextBox ID="txtSupName" CssClass="fontObject"  runat="server" BackColor="Transparent" 
                BorderStyle="None" Font-Bold="True" MaxLength="10" Width="90%" ></asp:TextBox></td>
		<td>&nbsp;</td>
		<td>Print Date :&nbsp;&nbsp;</td>
		<td> <asp:Label id=lblPrintDate runat=server /> 
                                    </td>
	</tr>
				                        <tr>
		<td height="25" class="style10">PPN :*</td>
		<td><asp:DropDownList id="ddlPPN" width=20% enabled="false"  CssClass="fontObject" runat=server>												
			</asp:DropDownList>
		</td>	
		<td>&nbsp;</td>
		<td><asp:TextBox id=txtPPNInit CssClass="fontObject" runat=server text="" width=0% BackColor="Transparent" BorderStyle="None" /></td>
		<td>&nbsp;</td>		
	</tr>			
				                        <tr>
		<td height="25" class="style10">Credit Term :*</td>
		<td><asp:TextBox id="txtCreditTerm" width=20%  CssClass="fontObject" runat=server /> Day(s)
			<asp:Label id=lblErrCreditTerm text="Credit Term cannot be empty" forecolor=red visible=false runat=server />
		</td>	
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
				                <tr id=trRetensi runat="server">
		<td height="25" class="style10">Retensi :</td>
		<td><asp:TextBox id=txtRetensi text="0" width=20% maxlength=20  CssClass="fontObject" runat=server /> %</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>		
				                <tr>
		<td height="25" class="style10">Currency Code :</td>
		<td><asp:DropDownList id=ddlCurrency width=100% AutoPostBack="True" onSelectedIndexChanged="CurrencyChanged"  CssClass="fontObject" runat=server/></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
				                    <tr>
		<td class="style3">Exchange Rate :</td>
		<td style="height: 26px"><asp:TextBox id=txtExRate text="1" width=20% maxlength=20  CssClass="fontObject" runat=server />
			<asp:Label id=lblErrExRate text="Exchange rate for this date has not been created." forecolor=red visible=false runat=server />
		</td>
		<td style="height: 26px">&nbsp;</td>
		<td style="height: 26px">&nbsp;</td>
	</tr>			
                                                                <tr>
        <td class="style9">
            Tempat Penyerahan :</td>
        <td style="height: 25px">
            <asp:DropDownList id=DDLLocPenyerahan width="100%"  CssClass="fontObject" runat=server 
                AutoPostBack="False" onSelectedIndexChanged="LocIndexChanged" 
                /><br />
            <asp:Label id=lblTptPenyerahan text="Please Select Tempat Penyerahan" forecolor=Red visible=False runat=server /></td>
        <td style="height: 25px">
        </td>
        <td style="height: 25px">
        </td>
        <td style="height: 25px">
        </td>
    </tr>
				                    <tr><td height="25" class="style10">Centralized :</td>
		<td><asp:CheckBox id="chkCentralized" Text="  Yes" checked=true AutoPostBack=true OnCheckedChanged=Centralized_Type  CssClass="font9Tahoma" runat=server /></td>
		<td>&nbsp;</td>
		<td></td>
		<td><asp:DropDownList id="ddlPOIssued" width=100% Enabled=false  CssClass="fontObject" runat=server /> 
			<asp:Label id=lblPOIssued text="Please Select PO Issued Location" forecolor=red visible=false runat=server />
		</td>		
	</tr>		
                                                    <tr>
        <td height="25" class="style10">
            VAT Credited :</td>
        <td>
            <asp:CheckBox id="chkVATCredited" Text="No" AutoPostBack=false  CssClass="font9Tahoma" runat=server />
            
                                                        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
								
				    <tr>
					    <td class="style10">&nbsp;</td>
				    </tr>
                </table>
                <table width="100%" class="font9Tahoma" cellspacing="0" cellpadding="1" border="0" align="center"  CssClass="font9Tahoma" runat=server >
                <tr>
               <td>
			    <tr>
					<td colspan="5">
						<table id=tblPOLine width="100%" class="sub-Add" cellspacing="0" cellpadding="1" border="0" align="center" runat=server >
							<tr>						
								<td>
									<table border="0" width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
										<tr>
											<td class="style9">&nbsp;</td>
											<td width="80%" style="height: 25px"><asp:Label id=lblErrManySelectDoc forecolor=black visible=true text="Note : Please select Purchase Requisition ID to add the line items or enter Purchase Requisition Ref. Information" runat=server/></td>
										</tr>	
										<tr id=Centralized_Yes runat="server">
											<td valign="top" class="style9">Purchase Requisition ID :*</td>
											<td style="height: 25px" valign="top" >
                                                <asp:TextBox ID="txtPRID_Loc" runat="server" AutoPostBack="False" MaxLength="15"
                                                    Width="50%" CssClass="fontObject"></asp:TextBox>
                                                <input id="BtnViewPR" class="button-small" runat="server" causesvalidation="False"
                                                        onclick="javascript:PopPR('frmMain', '', 'txtItemCode','TxtItemName','txtCost','txtQtyOrder','txtPRID_Loc','txtTtlCost','lblPRLocCode','txtAddNote','lblPRLnID','ddlUOM','False');"
                                                        type="button" value=" ... " /><br />
											    <asp:Label id=lblErrDdlPRID forecolor=red text="Please select one PRID" runat=server /></td>
										</tr>
										<tr id=Centralized_No Visible = false runat="server">
											<td class="style9">Purchase Requisition ID :*</td>
											<td style="height: 25px" ><asp:TextBox id=txtPRID width=50% maxlength=32 CssClass="fontObject" runat=server />
											     <asp:Label id=lblErrTxtPRID forecolor=red text="Please insert PRID Or Invalid PRID" runat=server /></td>
										</tr>
										<tr>
											<td height="25" class="style10">Purchase Requisition <asp:label id="lblLocCode" runat="server" /> :</td>
											<td><asp:TextBox id="lblPRLocCode" width="5%" runat=server BorderStyle="None" />
												<asp:TextBox id="lblPRLnID" width="0%" runat=server BorderStyle="None" BackColor="Transparent"  />
												</td>
										</tr>
                                        <tr>
                                            <td height="25" class="style10">Purchase Requisition Ref. <asp:label id="lblLocation" runat="server" /> :</td>
                                            <td><asp:DropDownList id=ddlPRRefLocCode width="50%" runat=server 
                                                    AutoPostBack="False" onSelectedIndexChanged="LocIndexChanged" 
                                                    CssClass="fontObject"/><br />
                                                <asp:Label id=lblErrPRRef forecolor=red text="Please select PR ref. location" runat=server />
                                                <asp:Label id=lblErrRef forecolor=red visible=false runat=server />
                                            </td>
                                        </tr>
										<tr>
											<td height="25" class="style10">PR/PO Ref. No. :</td>
											<td><asp:TextBox id=txtPRRefId width=50% maxlength=32 CssClass="fontObject" runat=server />
											    <asp:ImageButton ImageAlign=AbsBottom ID=btnGetRef onclick=GetRefNoBtn_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Get Data PO" Visible=false Runat=server />  
							                    <asp:label id=lblRefNoErr Visible=False forecolor=red Runat="server" /></td>
										</tr>
										
										<tr>
										    <td class="style10">&nbsp;</td>
										</tr>
										<tr>
											<td class="style9"><asp:label id="lblStockItem" runat="server" /> :*</td>
											<td style="height: 25px" ><asp:DropDownList id=ddlItemCode width=50% CssClass="fontObject" runat=server />
											    <input type=button value=" ... " id="FindIN" Visible=false onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False class="button-small"  runat=server />
                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="fontObject"  AutoPostBack="False" MaxLength="15"
                                                    Width="50%"></asp:TextBox>
                                                <input id="FindTxt" class="button-small"  runat="server" causesvalidation="False"
                                                        onclick="javascript:PopPOItem_New('frmMain', '', 'txtItemCode','TxtItemName','txtCost','ddlUOM', 'False');"
                                                        type="button" value=" ... " />
    										    <input type=button value=" ... " id="FindDC" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
    										    <input type=button value=" ... " id="FindWS" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
    										    <input type=button value=" ... " id="FindNU" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'True');" CausesValidation=False runat=server />
												<asp:Label id=lblErrItem forecolor=red text="Please select one Stock Item" runat=server />
												<asp:Label id=lblErrItemExist forecolor=red text="Stock Item already exist." runat=server />
											</td>
										</tr>
										<tr>
											<td height="25" class="style10">Item Description :</td>
											<td><asp:TextBox id=TxtItemName width=50% maxlength=128  CssClass="fontObject" runat=server />
											</td>
										</tr>
										<tr>
											<td height="25" class="style10">Quantity Order :*</td>
									        <td >
                                                <telerik:RadNumericTextBox ID="txtQtyOrder" ClientEvents-OnValueChanged="hitung.valueChanged" Runat="server">     </telerik:RadNumericTextBox>
												<asp:label id=lblErrQtyOrder text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:label id=lblValidationQtyOrder text="The PO Quantity Order value can not greather than the PR Quantity Request value !" Visible=False forecolor=red Runat="server" />
											</td>
											<td width="1%">&nbsp;</td>
											<td>&nbsp;</td>	
											<td>&nbsp;</td>	
										</tr>
										<tr>
											<td height="25" class="style10">Unit Cost :*</td>
											<td>
                                                 <telerik:RadNumericTextBox ID="txtCost" ClientEvents-OnValueChanged="hitung.valueChanged" Runat="server">     </telerik:RadNumericTextBox>
												<asp:label id=lblErrCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
								        <tr>
											<td height="25" class="style10">Total Cost :*</td>
											<td>
	                                            <telerik:RadNumericTextBox ID="txtTtlCost" ClientEvents-OnValueChanged="bagi.valueChanged"  Runat="server">     </telerik:RadNumericTextBox>
												<asp:label id=lblErrTtlCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
								        <tr style="visibility:hidden"> 
										    <td class="style10">Potongan/Discount :</td>
					                        <td><asp:TextBox id=txtDiscount width=10% maxlength=5   CssClass="fontObject" runat=server />
					                            <asp:label id=lblDiscount text="%" Runat="server" />
 
												<asp:label id=lblErrDiscount text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
												&nbsp;&nbsp;&nbsp;
												
												
												<asp:CheckBox id="chkPPN" enabled=false visible=false runat="server"></asp:CheckBox>
												
					                        </td>
										</tr>
										<tr>
										    <td class="style7">Additional Note :</td>
					                        <td style="height: 111px"><textarea rows=6 id=txtAddNote cols=63  CssClass="fontObject" runat=server></textarea></td>
										</tr>
										<tr>
										    <td class="style10">Purchase UOM :</td>
					                        <td><asp:DropDownList id=ddlUOM width=50% runat=server CssClass="fontObject" /></td>
										</tr>
                                        <tr>
										    <td class="style10"><asp:Label id=lblBlock runat=server text="Job Location"/></td>
					                        <td><asp:DropDownList id=ddlBlock width=50% runat=server CssClass="fontObject" /></td>
										</tr>
										
										<tr>
										    <td class="style10">&nbsp;</td>
										</tr>
										<tr>
										    <td class="style10"><u><b>Pajak</b></u></td>
										</tr>
										<tr>
											<td class="style10">PBB-KB :*</td>
											<td><asp:TextBox id=txtPBBKB width=10% maxlength=5   CssClass="fontObject" runat=server />
											    <asp:label id=lblPercent text="%" Runat="server" />
		 
												&nbsp;&nbsp;&nbsp;
												<asp:Label id=lblPBBKBRate runat=server text="Rate"/> 
												&nbsp;<asp:TextBox id=txtPBBKBRate width=8% maxlength=5  CssClass="font9Tahoma" runat=server />
											    <asp:label id=Label1 text="% (Sumatera 5%, Kalimantan 7.5%)" Runat="server" />
	 
												<asp:label id=lblErrPBBKB text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										<tr>
											<td class="style8">PPh 21 :*</td>
											<td style="height: 10px"><asp:TextBox id=txtPPH21 width=10% maxlength=5  CssClass="fontObject" runat=server />
											    <asp:label id=Label7 text="%" Runat="server" />
 
												<asp:label id=lblerrPPH21 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />		
												&nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkGrossUp21" checked=false AutoPostBack=true OnCheckedChanged=chkGrossUP21_Changed  CssClass="font9Tahoma" runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=Label9 runat=server text="Gross Up"/>										
											</td>
										</tr>
								        <tr>
											<td class="style10">PPh 22 :*</td>
											<td><asp:TextBox id=txtPPN22 width=10% maxlength=5  CssClass="fontObject" runat=server />
											    <asp:label id=lblPercent2 text="%" Runat="server" />
		 
												<asp:label id=lblErrPPN223 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										<tr>
											<td class="style10">PPh 23 :*</td>
											<td><asp:TextBox id=txtPPH23 width=10% maxlength=5  CssClass="fontObject" runat=server 
                                                    />
											    <asp:label id=Label2 text="%" Runat="server" />
 
												<asp:label id=Label3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<asp:Label id=lblerrPPH23 ForeColor=red Visible=false runat=server text="Please insert PPH 23"/>												
												&nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkGrossUp" checked=false AutoPostBack=true OnCheckedChanged=chkGrossUP_Changed runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=Label4 runat=server text="Gross Up"/>
											</td>
										</tr>
										<tr>
										    <td class="style10"><asp:Label id=lblSurat runat=server text="Surat/Pajak/BBN Kendaraan Bermotor"/></td>
										    <td><asp:CheckBox id="chkSurat" checked=false AutoPostBack=true OnCheckedChanged=chkSuratChanged runat="server"></asp:CheckBox></td>
										</tr>
										
										<tr>
										    <td class="style10">&nbsp;</td>
										</tr>
										<tr>
										    <td class="style10">&nbsp;</td>
										</tr>
										<tr>
										    <td class="style10"><u><b>Ongkos Angkut</b></u></td>
										</tr>
										<tr>
											<td class="style10">Transporter :</td>
											<td>
                                                <asp:TextBox ID="txtTransCode" runat="server" AutoPostBack="False"  CssClass="fontObject" Width="50%"></asp:TextBox>
                                                <input id="Find1" class="button-small"  runat="server" causesvalidation="False"
                                                        onclick="javascript:PopSupplier_New('frmMain', '', 'txtTransCode','txtTransName','txtBlank', 'ddlPPNTransport', 'txtPPNInitTransport', 'False');" type="button" value=" ... " />
                                                <asp:TextBox ID="txtTransName" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Bold="True" MaxLength="10" Width="45%" CssClass="fontObject"></asp:TextBox>&nbsp;
												<asp:Label id=lblErrTransporter forecolor=red text="Please select one transporter" runat=server />												
											</td>								
										</tr>
										<tr>
										    <td class="style10">PPN :</td>
					                        <td colspan=6 height="25">
					                            <asp:DropDownList id="ddlPPNTransport" width=10% enabled=false BorderStyle="None"  CssClass="fontObject" runat=server></asp:DropDownList>
					                            <asp:TextBox ID="txtBlank" runat="server" AutoPostBack="False" BackColor="Transparent" BorderStyle="None" Height="8px" MaxLength="15" Width="0%"></asp:TextBox>
					                            <asp:TextBox ID="txtPPNInitTransport" CssClass="fontObject" runat="server" AutoPostBack="False" BackColor="Transparent" BorderStyle="None" Height="8px" MaxLength="15" Width="0%"></asp:TextBox>
					                        </td>
				                        </tr>	
										<tr>
										    <td class="style10">Amount :</td>
					                        <td width="25%"><asp:TextBox id=txtAmtTransportFee width=25%  CssClass="fontObject"  runat=server />
					                            &nbsp;&nbsp;&nbsp;
												<asp:CheckBox id="chkPPNTransport" checked=false Visible=false runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=lblPPNTransport runat=server Visible=false text="PPN"/>
					                        </td>
										</tr>
										<tr>
											<td class="style10">PPh 23 :*</td>
											<td><asp:TextBox id=txtPPH23OA width=10% maxlength=5  CssClass="fontObject" runat=server />
											    <asp:label id=Label5 text="%" Runat="server" />
			 
												<asp:label id=Label6 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
												<asp:Label id=lblerrPPH23OA ForeColor=red Visible=false runat=server text="Please insert PPH 23"/>						
												&nbsp;&nbsp;&nbsp;						
												
												<asp:CheckBox id="chkGrossUpOA" checked=false AutoPostBack=true OnCheckedChanged=chkGrossUPOA_Changed runat="server"></asp:CheckBox>
												&nbsp;<asp:Label id=Label8 runat=server text="Gross Up"/>
											</td>
										</tr>
					                    <tr>
					                        <td colspan=6 style="height: 21px">&nbsp;</td>				
				                        </tr>		
										<tr>
											<td colspan=2 height="25"><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add UseSubmitBehavior="false" Runat="server" />
                                                &nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>	
				</tr>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <tr>
					<td colspan=5><br />
						<asp:DataGrid id=dgPODet
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines=none
							Cellpadding="2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="DEDR_Delete"
							OnCancelCommand="DEDR_Cancel"
							OnEditCommand="DEDR_Edit"
                            OnItemDataBound="dgLine_BindGrid"
							AllowSorting="True"  >	
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>	
							<Columns>
								<asp:BoundColumn Visible=False DataField="POLnId" />
								<asp:BoundColumn Visible=False DataField="ItemCode" />
								<asp:BoundColumn Visible=False DataField="QtyOrder" />
								<asp:BoundColumn Visible=False DataField="QtyReceive" />
								<asp:BoundColumn Visible=False DataField="PRLocCode" />
								<asp:BoundColumn Visible=False DataField="PRRefLocCode" />
								<asp:TemplateColumn HeaderText="PR ID <br>PR Location">
									<ItemStyle Width="10%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRID") %> id="lblPRId" runat="server" /> <br>
										<asp:Label Text=<%# Container.DataItem("PRLocCode") %> id="lblPRLocCode" runat="server" />
										<asp:Label Text=<%# Container.DataItem("POLnId") %> id="lblPOLnId" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("PPN") %> id="lblPPNStatus" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("PPNTransport") %> id="lblPPNTransporter" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("GetRefNo") %> id="lblGetRefNo" visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("PRLnID") %> id="lblPRLnID" runat="server" /> <br>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PR/PO Ref. No <br>PR Ref. Loc">
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRRefID") %> id="lblPRRefId" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("PRRefLocCode") %> id="lblPRRefLocCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item <br>Add. Note">
									<ItemStyle Width="14%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItemCode" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
										<asp:Label Text=<%# Container.DataItem("ItemCode") %> id="lblItem" visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Order Qty <br>Purch. UOM">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="8%"  HorizontalAlign="Right" />								
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyOrder"), 2), 2) %> id="lblQtyOrder" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("PurchaseUOM") %> id="lblUOMCode" runat="server" /><br>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost <br>Discount">
								    <HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="8%"  HorizontalAlign="Right" />								
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CostToDisplay"), 2), 2) %> id="lblCost" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Discount"), 2) %> id="lblDiscount" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PBBKB <br>PPH 22">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="5%"  HorizontalAlign="Right" />								
									<ItemTemplate> 
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PBBKB"), 2) %> id="lblPBBKB" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPN22"), 2) %> id="lblPPN22" runat="server" />
										<asp:Label Text=<%# Container.DataItem("PBBKBRate") %> id="lblPBBKBRate" visible=false runat="server" /><br>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item Amount <br>Item PPN">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="10%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ItemNetAmountToDisplay"), 2), 2) %> id="lblAmount" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ItemPPNAmountToDisplay"), 2), 2) %> id="lblPPN" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="OA Amount <br> OA PPN">
								    <HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="12%" HorizontalAlign="Right"/> 
									<ItemTemplate>
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("AmtTransportFee"), 2) %> id="lblTransportFee" runat="server" /><br>
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPNAmtTransportFee"), 2) %> id="lblPPNTransportFee" runat="server" /><br>
									    <asp:Label Text=<%# Container.DataItem("Transporter") %> id="lblTransporter" Visible="false" runat="server" />
									    <asp:label text= '<%# Container.DataItem("TransporterName") %>' Visible=True id="lblTransporterName" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Total Amount">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="10%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 2) %> id="lblTotalAmount" runat="server" /><br />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToEdit"), 2), 2) %> id="lblAmountToEdit" Visible=false runat="server" />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToEdit"), 2), 2) %> id="lblPPNToEdit" Visible=false runat="server" />
										<asp:Label Text=<%# Container.DataItem("Status") %> id="lblPOLnStatus" runat="server" /><br>
									</ItemTemplate>							
								</asp:TemplateColumn>									
								<asp:TemplateColumn>		
									<ItemStyle Width="5%" HorizontalAlign="Right" /> 
									<ItemTemplate>
									    <asp:label text= '<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPH23"), 2) %>' Visible=False id="lblPPH23" runat="server" />
							            <asp:label text= '<%# Container.DataItem("GrossUp") %>' Visible=False id="lblGrossUp" runat="server" />
							            <asp:label text= '<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPH23Transport"), 2) %>' Visible=False id="lblPPH23OA" runat="server" />
							            <asp:label text= '<%# Container.DataItem("GrossUpTransport") %>' Visible=False id="lblGrossUpOA" runat="server" />
							            <asp:label text= '<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("PPH21"), 2) %>' Visible=False id="lblPPH21" runat="server" />
							            <asp:label text= '<%# Container.DataItem("GrossUp21") %>' Visible=False id="lblGrossUp21" runat="server" />
								        <asp:label text= '<%# Container.DataItem("OtherName") %>' Visible=False id="lblOtherName" runat="server" />
                                        <asp:label text= '<%# Container.DataItem("BlkCode") %>' Visible=False id="lblBlkCode" runat="server" />                                        
										<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False runat="server"/>
										<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>						
							</Columns>										
						</asp:DataGrid>
					</td>	
			 
				                    <TR>
					<TD colspan=2></TD>
					<TD colspan=2 height=25>
                        <hr style="width :100%" />
                                        </TD>
					<td>&nbsp;</td>					
				</TR>
				                    <TR>
					<td colspan=2></td>
					<TD height=25 align=left>Total Amount :</TD>
					<TD Align=right><asp:Label ID=lblTotalAmount Runat=server />&nbsp;</TD>
				</TR>
				  <TR  style="visibility:hidden">
					<td colspan=2></td>
					<TD height=25 align=left  >Additional Discount :</TD>
					<td Align=right>&nbsp;<asp:TextBox id=txtAddDisc text="0" maxlength=18 
                            style="text-align:right" OnKeyUp="javascript:calAddDiscount();" 
                            runat=server Width="147px" />
						<asp:RegularExpressionValidator id="RegularExpressionValidator4" 
							ControlToValidate="txtAddDisc"
							ValidationExpression="\d{1,15}\.\d{0,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 18 digits."
							runat="server"/>
						<asp:RequiredFieldValidator 
							id="RequiredFieldValidator4" 
							runat="server" 
							ErrorMessage="Please Specify Additional Discount" 
							ControlToValidate="txtAddDisc" 
							display="dynamic"/>
						<asp:label id=lblerrAddDisc text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
					</td>
				</TR>
				                    <TR  style="visibility:hidden">
					<td colspan=2></td>
					<TD height=25 align=left>Total After Discount :</TD>
					<TD Align=right><asp:TextBox ID=txtTtlAftDisc text="0" ForeColor=black style="text-align:right" ReadOnly=true CssClass="font9Tahoma" Runat=server /></TD>
				</TR>
				    <tr>
					    <td height="25">&nbsp;</td>	
					    <td colspan="4">
                            &nbsp;</td>
				    </tr>
				    <tr>
					    <td colspan=5 height="25">
                            <table class="style1" class="font9Tahoma">
                                <tr class="font9Tahoma" >
                                    <td class="style11" >
                                        Remarks :</td>
                                    <td>
                            <asp:TextBox id="txtRemark" maxlength="128" width="99%" 
                                runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
				    </tr>	
				                    <tr>
				    <td colspan=2 height="25">
				        <asp:Label id=lblErrGR visible=True Text="All/Some item or POID already have received." forecolor=red runat=server />
				    </td>
				</tr>
				                    <tr>
					<td colspan=2 height="25">
				        <asp:CheckBox id="chkPrinted" Text="  Mark as printed" checked=false runat=server />
				    </td>		
				</tr>	
					
				                    <tr>
					<td align="left" colspan="5">
					    <asp:ImageButton id="btnNew" UseSubmitBehavior="false" onClick="btnNew_Click" imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
						<asp:ImageButton id="btnSave" UseSubmitBehavior="false" onClick="btnSave_Click" ImageUrl="../../images/butt_save.gif" AlternateText=Save CausesValidation=False runat="server" />
						<asp:ImageButton id="btnConfirm" UseSubmitBehavior="false" onClick="btnConfirm_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm CausesValidation=False runat="server" />
						<asp:ImageButton id="btnPrint" UseSubmitBehavior="false" onClick="btnPreview_Click" ImageUrl="../../images/butt_print.gif" AlternateText=Print CausesValidation=False runat="server" />
						<asp:ImageButton id="btnCancel" UseSubmitBehavior="false" onclick="btnCancel_Click" ImageUrl="../../images/butt_cancel.gif" Text=" Cancel " Runat=server />
						<asp:ImageButton id="btnDelete" UseSubmitBehavior="false" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" UseSubmitBehavior="false" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
						<asp:ImageButton id="btnEdited" UseSubmitBehavior="false" onClick="btnEdited_Click" ImageUrl="../../images/butt_edit.gif" AlternateText=Edit CausesValidation=False runat="server" />
						<asp:ImageButton id="btnAddendum" UseSubmitBehavior="false" onClick="btnAddendum_Click" ImageUrl="../../images/butt_gen_addendum.gif" AlternateText="Generate Addendum" CausesValidation=False runat="server" />
					</td>
					
				</tr>		
				                    <tr>
					    <td colspan=4>
 					            &nbsp;</td>
				    </tr>
				                    <tr id="tblSPK" visible=false>
					<td colspan=5>
						<asp:DataGrid id=dgSPK
							AutoGenerateColumns="false" width="30%" runat="server"
							GridLines=none
							Cellpadding="1"
							Pagerstyle-Visible="False"
							AllowSorting="false" class="font9Tahoma">	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
                                                    <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
							<Columns>
								<asp:HyperLinkColumn HeaderText="SPK Created" 
									SortExpression="POID" 
									DataNavigateUrlField="POID" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POID={0}"
									DataTextFormatString="{0:c}"
									DataTextField="POID" />	
							</Columns>
						</asp:DataGrid>
                        <br />

             <telerik:RadInputManager  ID="RadInputManager1" runat="server">
                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior1" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtPBBKB" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             
        
                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtPBBKBRate" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             


                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior3" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtPPH21" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             


                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior4" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtPPN22" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             

                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior5" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtPPH23" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             


                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior6" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtAmtTransportFee" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             


                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior7" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtPPH23OA" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             



                     
                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior11" >
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtDiscount" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             
            </telerik:RadInputManager>
					</td>
				</tr>
                </td>	
  </tr>
			    </table>
                </div>
            </td>
        </tr>
        </table>
		</form>
	</body>
</html>
