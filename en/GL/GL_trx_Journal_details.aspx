<%@ Page Language="vb" trace="False" src="../../../include/GL_Trx_Journal_Details.aspx.vb" Inherits="GL_Journal_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Journal Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
			function gotFocusCR() {
			    var doc = document.frmMain;
			    var dbAmt = parseFloat(doc.hidTtlDBAmt.value);
			    var crAmt = parseFloat(doc.hidTtlCRAmt.value);
			    var dbDBCR = doc.hidDBCR.value;
			    var diffAmt = dbAmt+crAmt;
			    doc.txtDRTotalAmount.value = '';
                
                if (dbDBCR == '')
                    if (diffAmt > 0) 
                        doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2) + round(doc.hidCtrlAmtFig.value, 2);
                    else
                        doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2);
                else
                    if (dbDBCR == 'CR') 
                         doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2) + round(doc.hidCtrlAmtFig.value, 2);
                    else
                        doc.txtCRTotalAmount.value = round(doc.hidDBAmt.value, 2);
                    
	        }
	        function gotFocusDR() {
			    var doc = document.frmMain;
			    var dbAmt = parseFloat(doc.hidTtlDBAmt.value);
			    var crAmt = parseFloat(doc.hidTtlCRAmt.value);
			    var dbDBCR = doc.hidDBCR.value;
			    var diffAmt = dbAmt+crAmt;
			    doc.txtCRTotalAmount.value = '';
			    
			    if (dbDBCR == '') 
	                if (diffAmt > 0) 
	                    doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2);
	                else
                        doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2) + (round(doc.hidCtrlAmtFig.value, 2)*-1);
                else
                    if (dbDBCR == 'DR') 
                        doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2) + (round(doc.hidCtrlAmtFig.value, 2)*-1);
                    else
                        doc.txtDRTotalAmount.value = round(doc.hidCRAmt.value, 2);
	        }
	        
	        function lostFocusCR() {
			    var doc = document.frmMain;
			    var x = doc.txtCRTotalAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtCRTotalAmount.value = toCurrency(round(dbAmt, 2));
	        }
	        
	        function lostFocusDR() {
			    var doc = document.frmMain;
			    var x = doc.txtDRTotalAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtDRTotalAmount.value = toCurrency(round(dbAmt, 2));
	        }
	        
	        function toCurrency(num) {
              num = num.toString().replace(/\$|\,/g, '')
              if (isNaN(num)) num = "0";
              sign = (num == (num = Math.abs(num)));
              num = Math.floor(num * 100 + 0.50000000001);
              cents = num % 100;
              num = Math.floor(num / 100).toString();
              if (cents < 10) cents = '0' + cents;

              for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3))
              }

              return (((sign) ? '' : '-') + num + '.' + cents)
            }
            
            function calTaxPriceCR() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmountCR.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));	
				var d = (a * (10/100));
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				var newnumberPPN = new Number(d+'').toFixed(parseInt(0));
				
				if (doc.hidTaxPPN.value == '0')
				    doc.txtCRTotalAmount.value = newnumber;
				else
				    doc.txtCRTotalAmount.value = newnumberPPN;
				    
				if (doc.txtCRTotalAmount.value == 'NaN') 
					doc.txtCRTotalAmount.value = '';
				else
				    doc.txtCRTotalAmount.value = doc.txtCRTotalAmount.value;
			}
			
			function calTaxPriceDR() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmountDR.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));	
				var d = (a * (10/100));
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				var newnumberPPN = new Number(d+'').toFixed(parseInt(0));
				
				if (doc.hidTaxPPN.value == '0')
				    doc.txtDRTotalAmount.value = newnumber;
				else
				    doc.txtDRTotalAmount.value = newnumberPPN;
				    
				if (doc.txtDRTotalAmount.value == 'NaN') 
					doc.txtDRTotalAmount.value = '';
				else
				    doc.txtDRTotalAmount.value = doc.txtDRTotalAmount.value;
			}
			
			function gotFocusDPPCR() {
			    var doc = document.frmMain;
			    doc.txtDPPAmountDR.value = '';
			    doc.txtDRTotalAmount.value = '';
	        }
	        function gotFocusDPPDR() {
			    var doc = document.frmMain;
			    doc.txtDPPAmountCR.value = '';
			    doc.txtCRTotalAmount.value = '';
	        }
		</script>		
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
                        
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
                        
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
                        
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            .style2
            {
                width: 166px;
            }
            .style3
            {
                width: 13%;
            }
            .style5
            {
                width: 222px;
                height: 11px;
            }
            .style6
            {
                height: 11px;
            }
            .style7
            {
                width: 13%;
                height: 11px;
            }
            .style8
            {
                width: 222px;
                height: 4px;
            }
            .style9
            {
                height: 4px;
            }
            .style10
            {
                width: 13%;
                height: 4px;
            }
        </style>
	</head>
	
	<body >
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		<asp:label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id="issueType" Visible="False" Runat="server" />
		<asp:label id="lblStsHid" Visible="False" Runat="server"/>
		<asp:label id="blnShortCut" Visible="False" Runat="server"/>
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id="blnUpdate" Visible="False" Runat="server"/>
		<asp:label id=lblTxLnID visible=false runat=server />

		<table border=0 width="100%" cellspacing="0" cellpadding="2" class="font9Tahoma">
        <tr>
        <td>
   
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                    <table cellpadding="0" cellspacing="2" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                              <strong> JOURNAL DETAILS</strong> </td>
                            <td class="font9Header" style="text-align: right">
                                Period :&nbsp; | <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=Status runat=server />&nbsp;| Date Created : <asp:Label id=CreateDate runat=server />&nbsp;| Last Update : <asp:Label id=UpdateDate runat=server />&nbsp;| Updated By : <asp:Label id=UpdateBy runat=server />&nbsp;| Print Date : <asp:Label id=lblPrintDate visible="false" runat=server />&nbsp;| <asp:Label ID="lblSKBStartDate" runat="server" Visible="False"></asp:Label>&nbsp;: <asp:Label ID="LblIsSKBActive" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>			
			<tr>
				<td height=15 style="width: 222px">Journal ID :</td>
				<td width="40%"><asp:label id=lblTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td class="style3">&nbsp;</td>
				<td width="20%"><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=15 style="width: 222px">Description :*</td>
				<td><asp:Textbox id="txtDesc" Width=100% MaxLength=128 runat=server CssClass="fontObject"/>
					<asp:RequiredFieldValidator 
						id="validateDesc" 
						runat="server" 
						ErrorMessage="Please enter Journal Description" 
						EnableClientScript="True"
						ControlToValidate="txtDesc" 
						display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td class="style3">&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=15 style="width: 222px">Transaction Type :</td>
				<td><asp:DropDownList id="lstTxType" Width=50% AutoPostBack=true OnSelectedIndexChanged="TxType_Change" runat=server CssClass="fontObject"/>
				</td>
				<td>&nbsp;</td>
			    <td class="style3">&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=15 style="width: 222px">Received From :</td>
				<td><asp:DropDownList id="ddlReceiveFrom" CssClass="font9Tahoma" Width=50% AutoPostBack=true OnSelectedIndexChanged="RcvFrom_Change"  runat=server>
						<asp:ListItem value="1">Head Quarter</asp:ListItem>
						<asp:ListItem value="2">Others</asp:ListItem>
						<asp:ListItem value="3">Data Transfer</asp:ListItem>
						<asp:ListItem value="4">Account Receiveable</asp:ListItem>
						<asp:ListItem value="5">Staff Advance</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>&nbsp;</td>	
				<td class="style3">&nbsp;</td>
				<td>&nbsp;</td>	
				<td>&nbsp;</td>	
			</tr>
			<tr>
				<td height=15 style="width: 222px">Document Ref. No. :*</td>
				<td><asp:Textbox id="txtRefNo" maxlength="32" Width=100% runat=server CssClass="fontObject"/>	
					<asp:RequiredFieldValidator 
						id="validateRef" 
						runat="server" 
						ErrorMessage="Please enter document reference number" 
						EnableClientScript="True"
						ControlToValidate="txtRefNo" 
						display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td class="style3">&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td style="width: 222px">Document Ref. Date :* </td>
				<td><asp:TextBox id=txtDate Width=25% maxlength=10 runat=server CssClass="fontObject"/>
					<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please enter document date" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text ="<BR>Date entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/>
				</td>
				<td>&nbsp;</td>
				<td class="style3">&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td style="width: 222px">Document Amount :*</td>
				<td> <asp:Textbox id="txtAmt" Text=0 width=50% style="text-align:right" maxlength=22 runat=server Font-Bold=true CssClass="fontObject"/>	
				</td>
				<td>&nbsp;</td>
				<td class="style3">&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style5"><asp:label id="lblSupplier" text="Supplier Code :*" Visible=false Runat="server"/></TD>
				<td class="style6">
                    <asp:TextBox ID="txtSupCode" runat="server" AutoPostBack="False" MaxLength="15" Width="50%" Visible="False" CssClass="fontObject"></asp:TextBox>
                    <input id="btnFind" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                        type="button" value=" ... " visible="false" />&nbsp;<asp:ImageButton ID="btnGet" runat="server" AlternateText="Get Data"
                            CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/icn_next.gif"
                            OnClick="GetSupplierBtn_Click" ToolTip="Click For Get Data" Visible="False" />
                    <asp:TextBox ID="txtSupName" runat="server" BackColor="Transparent"
                            BorderStyle="None" Font-Bold="True" ForeColor="White" MaxLength="10" Width="99%" CssClass="fontObject"></asp:TextBox><br />
					<asp:Label id=lblErrSupplier forecolor=red Text="Please select Supplier Code" visible=false runat=server/>
				</td>
				<td class="style6"></td>
				<td class="style7"></td>
				<td class="style6"></td>				
				<td class="style6"></td>
			</tr>
			<tr>
				<td class="style8"><asp:label id="lblFromLocCode" text="From Location :*" Visible=false Runat="server"/></TD>
				<td class="style9"><asp:DropDownList width=100% id=ddlFromLocCode Visible=false runat=server CssClass="fontObject"/>
					<asp:Label id=lblErrFromLocCode forecolor=red visible=false text="Please select Location Code"  runat=server/>
				</td>
				<td class="style9"></td>
				<td class="style10"></td>
				<td class="style9"></td>				
				<td class="style9"></td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblRefNo" text="Reference No/Staff Advance :" Visible=false Runat="server"/></td>
				<td><asp:DropDownList id="ddlRefNo" Width="100%" Visible=false runat=server CssClass="fontObject"/>
				    <asp:label id=lblRefNoErr Visible=False forecolor=red Runat="server" /></td>
				<td>&nbsp;</td>
				<td class="style3">&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
                 </td>
        </tr>
            </table>
            <table border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
			<tr>
				<td colspan="6">
					<table id="tblAdd" border="0" width="100%" cellspacing="0" cellpadding="2" runat="server" class="sub-Add">
						<tr class="mb-c">
							<td height=15 style="width: 166px">Line Description :*</td>
							<td colspan=4><asp:TextBox id=txtDescLn Width=95% maxlength=128 runat=server CssClass="fontObject"/>	
											<asp:label id=lblDescErr text="Please enter Line Description" Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowChargeTo" class="mb-c">
							<td height=15 style="width: 166px">Charge To :*</td>
							<td colspan=4>
								<asp:DropDownList id="ddlLocation" Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server CssClass="fontObject"/> 
								<asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>
						<tr class="mb-c">
							<td class="style2"><asp:label id="lblAccCodeTag" Runat="server"/></td>
							<td colspan=4>
                                <asp:TextBox ID="txtAccCode" runat="server" CssClass="fontObject" AutoPostBack="True" MaxLength="15" OnTextChanged="onSelect_StrAccCode"
                                    Width="22%"></asp:TextBox>
                                <input id="Find" runat="server" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                  class="button-small"   type="button" value=" ... " />&nbsp;<asp:Button 
                                    ID="BtnGetData" CssClass="button-small" runat="server" Font-Bold="True"
                                        OnClick="CallCheckVehicleUse" Text="Click Here" 
                                    ToolTip="Click For Refresh COA " Width="70px" />&nbsp;
                                <asp:TextBox ID="txtAccName" CssClass="fontObject" runat="server" 
                                    BackColor="Transparent" BorderStyle="None"
                                    Font-Bold="True" MaxLength="10" Width="55%"></asp:TextBox><br />
									   	  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>						
						<tr id="RowChargeLevel" class="mb-c">
							<td height="15" style="width: 166px">Charge Level :* </td>
							<td colspan=4><asp:DropDownList id="ddlChargeLevel" Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server CssClass="fontObject"/> </td>
						</tr>
						<tr id="RowPreBlk" class="mb-c">
							<td style="width: 166px; height: 49px"><asp:label id=lblPreBlkTag Runat="server"/> </td>
							<td colspan=4 style="height: 49px"><asp:DropDownList id="ddlPreBlock" Width=95% runat=server CssClass="fontObject"/>
										  <asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowBlk" class="mb-c">
							<td style="width: 166px; height: 49px"><asp:label id=lblBlkTag Runat="server"/></td>
							<td colspan=4 style="height: 49px"><asp:DropDownList id="lstBlock" Width=95% runat=server CssClass="fontObject"/>
										  <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>		
						<tr class="mb-c">
							<td height=15 style="width: 166px"><asp:label id="lblVehTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstVehCode" Width=95% runat=server CssClass="fontObject"/>
										  <asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr class="mb-c">
							<td height=15 style="width: 166px"><asp:label id="lblVehExpTag" Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstVehExp" Width=95% runat=server CssClass="fontObject"/>
										  <asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowSPL" visible=false class="mb-c">
	                        <td height=15 style="width: 166px"><asp:label id="lblSupplierDet" text="Supplier Code :*" Runat="server"/></TD>
	                        <td colspan=4>
                                <asp:TextBox ID="txtSupCodeDet" runat="server" AutoPostBack="False" MaxLength="15" Width="22%" CssClass="fontObject"></asp:TextBox>
                                <input id="btnFindDet" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmMain','','txtSupCodeDet','txtSupNameDet','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                                    type="button" value=" ... " />&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Get Data"
                                        CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/icn_next.gif"
                                        OnClick="GetSupplierDetBtn_Click" ToolTip="Click For Get Data" />&nbsp;
                                <asp:TextBox ID="txtSupNameDet" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Bold="True" ForeColor="White" MaxLength="10" Width="64%" CssClass="fontObject"></asp:TextBox><br />
		                        <asp:Label id=lblErrSupplierDet forecolor=red Text="Please select Supplier Code" visible=false runat=server/>
	                        </td>
                        </tr>
                        <tr id="RowFP" visible=false class="mb-c">
							<td height=15 style="width: 166px">Tax Invoice/Faktur Pajak No. :</td>
							<td colspan=4><asp:TextBox id=txtFakturPjkNo Width=22% maxlength=19 runat=server CssClass="fontObject"/>	
											<asp:label id=lblErrFakturPjk text="Please enter tax invoice/faktur pajak no." Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr id="RowFPDate" visible=false class="mb-c">
							<td height=15 style="width: 166px">Tax Invoice Date/Tgl. Faktur Pajak :</td>
							<td colspan=4><asp:TextBox id=txtFakturDate width=22% maxlength="10" runat="server" CssClass="fontObject"/>
							            <a href="javascript:PopCal('txtFakturDate');"><asp:Image id="Image1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					                    <asp:label id=lblDateFaktur Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					                    <asp:label id=lblFmtFaktur  forecolor=red Visible = false Runat="server"/></td>
						</tr>
						<tr id="RowPO" visible=false class="mb-c">
							<td height=15 style="width: 166px">Purchase Order ID</td>
							<td colspan=4><asp:DropDownList id="lstPOID" Width=95% runat=server CssClass="fontObject"/>
							          <asp:label id=lblPOIDErr Visible=False forecolor=red Runat="server" />
						    </td>
						</tr>
						<tr id="RowTax" visible=false class="mb-c">
							<td height=15 style="width: 166px"><asp:label id=lblTaxObject Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstTaxObject" Width=95% AutoPostBack=True OnSelectedIndexChanged=lstTaxObject_OnSelectedIndexChanged runat=server CssClass="fontObject"/>
									  <asp:label id=lblTaxObjectErr Visible=False forecolor=red Runat="server" />
						    </td>
						</tr>
						<tr id="RowTaxAmt" visible=false class="mb-c">
						    <td style="width: 166px">DPP Amount (DR) : </td>
						    <td width="30%"><asp:Textbox id="txtDPPAmountDR" style="text-align:right" CssClass="fontObject" Text=0 Width=95% maxlength=22 OnKeyUp="javascript:calTaxPriceDR();" onFocus="javascript:gotFocusDPPDR();" OnBlur="javascript:lostFocusDPPDR();" runat=server />
								<asp:Label id=lblTwoAmountDPP visible=false forecolor=red text="<BR>Please enter either DR or CR DPP amount" runat=server/>
						    </td>
						    <td style="width: 5%">&nbsp;</td>
							<td width="15%">DPP Amount (CR) :</td>
							<td width="30%"><asp:Textbox id="txtDPPAmountCR" style="text-align:right" CssClass="fontObject" Text=0 Width=87% maxlength=22 OnKeyUp="javascript:calTaxPriceCR();" onFocus="javascript:gotFocusDPPCR();" OnBlur="javascript:lostFocusDPPCR();" runat=server />
							</td>
						</tr>
						<tr class="mb-c">
							<td style="width: 166px">Total Amount (DR) :</td>
							<td width="30%"><asp:Textbox id="txtDRTotalAmount" Text=0 CssClass="fontObject" style="text-align:right" Font-Bold=true  Width=95% maxlength=22 runat=server />
								<asp:Label id=lblTwoAmount visible=false forecolor=red text="<BR>Please enter either DR or CR total amount" runat=server/>
							</td>
							<td style="width: 5%">&nbsp;</td>
							<td width="15%">Total Amount (CR) :</td>
							<td width="30%"><asp:Textbox id="txtCRTotalAmount" Text=0 style="text-align:right" CssClass="fontObject" Font-Bold=true   Width=87% maxlength=22 runat=server />
							</td>
						</tr>
						<tr class="mb-c">
							<td class="style2"></td>						
							<td Colspan=2>
								<asp:label id=lblerror text="<br>Number generated is too big!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lblStock text="<br>Not enough quantity in hand!" Visible=False forecolor=red Runat="server" />
								<asp:label id=lbleither text="<br>Please key in either Meter Reading OR Quantity to issue" Visible=False forecolor=red Runat="server" />
							</td>
							<td></td>						
							<td></td>						
						</tr>
						<tr class="mb-c">
							<td Colspan=3><asp:ImageButton AlternateText="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" /> 
										  <asp:ImageButton AlternateText="Save" id="Update" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnAdd_Click" Runat="server" />&nbsp;</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>	
						</tr>
					</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr 
                        text="Document must contain transaction to Confirm!" Visible=False 
                        forecolor=Red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan="6"> 
					<asp:DataGrid id="dgTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"             						
						AllowSorting="True"  >
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>                         					
					<Columns>
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="3%"/>
						<ItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</EditItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Line Description">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("SPLInfo") %> id="Label1" runat="server" />
							<asp:label text=<%# Container.DataItem("SPLCode") %> id="lblSPLCode" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("SPLName") %> id="lblSPLName" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("SPLFaktur") %> id="lblSPLFaktur" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("SPLFakturDate") %> id="lblSPLFakturDate" Visible=false runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Charge To">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("LocCode") %> id="lblLocCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Acc Descr">
						<ItemStyle width="17%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccDescr") %> id="lblAccDescr" runat="server" /><br />
							<asp:label text= '<%# Container.DataItem("TaxObject") %>' id="lblTaxObject" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="8%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" /><br />
                            <asp:label text=<%# Container.DataItem("BlkDesc") %> id="LblBlkDesc" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("VehExpCode") %> id="lblVehExpCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
				
					<asp:TemplateColumn HeaderText="Total">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="10%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblAmount" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %>  runat="server" />
							<asp:label id="lblAccTx" runat="server" />
							<asp:label id="lblAmt" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %> visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>										
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="Center" />							
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("JrnLineID") %> Visible=False id="lblID" runat="server" />
							<asp:label text= '<%# Container.DataItem("TaxLnID") %>' Visible=False id="lblTaxLnID" runat="server" />
							<asp:label text= '<%# Container.DataItem("TaxRate") %>' Visible=False id="lblTaxRate" runat="server" />
							<asp:label text= '<%# Container.DataItem("DPPAmount") %>' Visible=False id="lblDPPAmount" runat="server" />
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label text=<%# Container.DataItem("JrnLineID") %> Visible=False id="lblID" runat="server" />
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=6>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr class="font9Tahoma">
				<td colspan=4>&nbsp;</td>								
				<td height=15 align=right> <strong>Total Amount : <asp:label id="lblTotAmtFig" text="0" runat="server" /></strong> </td>						
				<td>&nbsp;</td>					
			</tr>
			<tr class="font9Tahoma">
				<td colspan=4>&nbsp;</td>								
				<td align=right><strong>Control Amount : <asp:label id="lblCtrlAmtFig" runat="server" /></strong> </td>
				<td>&nbsp;</td>					
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr class="font9Tahoma">
				<td ColSpan="6">
					<asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
					<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
				</td>
			</tr>
			<tr class="font9Tahoma">
				<td align="left" colspan="6">
				    <asp:ImageButton id="NewBtn"  UseSubmitBehavior="false" onClick=NewBtn_Click     imageurl="../../images/butt_new.gif"     CausesValidation=False  AlternateText="New"     runat=server/>
					<asp:ImageButton id="Save"    UseSubmitBehavior="false" onClick=btnSave_Click    ImageURL="../../images/butt_save.gif"                            AlternateText="Save"    runat="server"  visible=False />
					<asp:ImageButton ID="Confirm" UseSubmitBehavior="false" onclick=btnConfirm_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Visible=false Runat=server />
					<asp:ImageButton ID="Edited" runat="server" AlternateText="Edit" 
                        CausesValidation="False" ImageUrl="../../images/butt_edit.gif" 
                        onclick="btnEdited_Click" UseSubmitBehavior="false" />
					<asp:ImageButton id="Delete"  UseSubmitBehavior="false" onClick=btnDelete_Click  ImageURL="../../images/butt_delete.gif"  CausesValidation=False  AlternateText="Delete"  runat="server"  />
					<asp:ImageButton id="Undelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="Undelete"  runat="server"  visible=False />
					<asp:ImageButton id="Print"   UseSubmitBehavior="false" onClick=btnPrint_Click   ImageURL="../../images/butt_print.gif"   CausesValidation=False  AlternateText="Print"   runat="server"  visible=False />
					<asp:ImageButton id="Back"    UseSubmitBehavior="false" onClick=btnBack_Click    ImageURL="../../images/butt_back.gif"    CausesValidation=False  AlternateText="Back"    runat="server" />
				</td>
			</tr>		
			<tr>
				<td colspan=5>
					&nbsp;</td>
			</tr>
			<tr id=TrLink class="font9Tahoma" runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal text="View Journal Predictions" causesvalidation=false runat=server /> 
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgViewJournal
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
                         OnItemDataBound="dgLine_BindGrid" 
						AllowSorting="false" class="font9Tahoma">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
                     
						<Columns>
							<asp:TemplateColumn HeaderText="COA Code">
							    <ItemStyle Width="20%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("ActCode") %> id="lblCOACode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Description">
							     <ItemStyle Width="40%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblCOADescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Debet">
							    <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
							    <ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountDB"), 2), 2) %> id="lblAmountDB" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>									
						    <asp:TemplateColumn HeaderText="Credit">
						        <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
								<ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCR"), 2), 2) %> id="lblAmountCR" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>		
						    <asp:TemplateColumn>		
								<ItemStyle HorizontalAlign="Right" /> 									
								<ItemTemplate>
									
								</ItemTemplate>
							</asp:TemplateColumn>							
						</Columns>
					</asp:DataGrid>

                     <br />

                     <telerik:RadInputManager  ID="RadInputManager1" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type" KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtDPPAmountDR" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                    </telerik:RadInputManager>

                    <telerik:RadInputManager  ID="RadInputManager2" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type" KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtDPPAmountCR" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                    </telerik:RadInputManager>

                    <telerik:RadInputManager  ID="RadInputManager3" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2"   KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtDRTotalAmount" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                    </telerik:RadInputManager>

                    <telerik:RadInputManager  ID="RadInputManager4" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2"   KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtCRTotalAmount" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                                                                                                                           
                    </telerik:RadInputManager>

                    <telerik:RadInputManager  ID="RadInputManager5" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2"   KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtAmt" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                                                                                                                           
                    </telerik:RadInputManager>

				</td>
			</tr>	
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr class="font9Tahoma">
			    <td style="width: 222px">&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidCtrlAmtFig value="" runat=server/>
			<Input type=hidden id=hidDBCR value="" runat=server/>
			<Input type=hidden id=hidTtlDBAmt value="0" runat=server/>
			<Input type=hidden id=hidTtlCRAmt value="0" runat=server/>
			<Input type=hidden id=hidDBAmt value="0" runat=server/>
			<Input type=hidden id=hidCRAmt value="0" runat=server/>
			
			<Input type=hidden id=hidNPWPNo value="" runat=server />
			<Input type=hidden id=hidTaxObjectRate value=0 runat=server />
			<Input type=hidden id=hidCOATax value=0 runat=server />
			<Input type=hidden id=hidTaxStatus value=1 runat=server />
			<Input type=hidden id=hidHadCOATax value=0 runat=server />
			<Input type=hidden id=hidFFBSpl value="0" runat=server />
			<Input type=hidden id=hidTaxPPN value="0" runat=server />
			
            <br />
            <asp:TextBox ID="txtPPN" CssClass="font9Tahoma" runat="server" BackColor="Transparent" BorderStyle="None"
                Width="9%"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server" BackColor="Transparent"
                    BorderStyle="None" Width="9%"></asp:TextBox><asp:TextBox ID="txtPPNInit" runat="server"
                        BackColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
