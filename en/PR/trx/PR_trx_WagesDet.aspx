<%@ Page Language="vb" src="../../../include/PR_trx_WagesDet.aspx.vb" Inherits="PR_trx_WagesDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Wages Payment Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript" >
		// added BY ALIM
		function addCommas(number) {
			return outputDollars(Math.floor(number-0) + '') + outputCents(number - 0);
		}

		function outputDollars(number) {
			if (number.length <= 3)
				return (number == '' ? '0' : number);
			else {
				var mod = number.length%3;
				var output = (mod == 0 ? '' : (number.substring(0,mod)));
				for (i=0 ; i < Math.floor(number.length/3) ; i++) {
					if ((mod ==0) && (i ==0))
						output+= number.substring(mod+3*i,mod+3*i+3);
					else
						output+= '.' + number.substring(mod+3*i,mod+3*i+3);
				}
				return (output);
			}
		}

		function outputCents(amount) {
			amount = Math.round( ( (amount) - Math.floor(amount) ) *100);
			return (amount < 10 ? ',0' + amount : ',' + amount);
		}

		// end of Added BY ALIM
	
		function ShowOutstandingAmount() {
			var doc = document.frmMain;
			var outamt = parseFloat(doc.hidOutstandingAmount.value);
			// Remarked BY ALIM
			//document.getElementById('currOutstandingAmount').innerHTML = round(outamt, 5);
			// Modified BY ALIM
			document.getElementById('currOutstandingAmount').innerHTML = addCommas(round(outamt, 2));
			
		}
		
		function CalOutstandingAmount() {
			var doc = document.frmMain;
			var a = parseFloat(doc.hidTotalAmount.value);
			var b = parseFloat(doc.txtActualPayment.value);
			document.getElementById('currOutstandingAmount').innerHTML = a - b;
			if (document.getElementById('currOutstandingAmount').innerHTML == 'NaN') {
				document.getElementById('currOutstandingAmount').innerHTML = '';
			}
			else {
				//Remarked BY ALIM
				//document.getElementById('currOutstandingAmount').innerHTML = round(document.getElementById('currOutstandingAmount').innerHTML, 5);  
				// End of Remarked BY ALIM
				// Modified BY ALIM
				
				//document.getElementById('currOutstandingAmount').innerHTML = round(document.getElementById('currOutstandingAmount').innerHTML, 2);
				document.getElementById('currOutstandingAmount').innerHTML = addCommas(round(document.getElementById('currOutstandingAmount').innerHTML, 2));
				// End of Modified BY ALIM
				//document.hidOutstandingAmount.value = document.getElementById('currOutstandingAmount').innerHTML;
				
			}
		}
		</script>	
	</head>
	<body onload="javascript:ShowOutstandingAmount();">
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
               <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6"><strong>WAGES PAYMENT DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td height=25>Employee Code : </td>
					<td><asp:Label id=lblEmpCode runat=server/></td>
					<td>&nbsp;</td>
					<td>Period : </td>
					<td><asp:Label id=lblPeriod runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Name : </td>
					<td><asp:Label id=lblEmpName runat=server/></td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25>Department Code : </td>
					<td width=30%><asp:Label id=lblDeptCode runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Date Created : </td>
					<td width=25%><asp:Label id=lblDateCreated runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Terminate Date :</td>
					<td><asp:Label id=lblTerminateDate runat=server /></td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Pay Mode :</td>
					<td><asp:Label id=lblPayModeTag runat=server /><asp:Label id=lblPayMode visible=false runat=server /></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:Label id=lblBankTag text='Bank' runat=server /><asp:Label id=lblAccountTag runat=server /> Code :*</td>
					<td>
					    <asp:dropdownlist id=ddlAccCode width=80% runat=server />
					    <Input Type="Button" Value=" ... " ID="btnFindAccCode" OnClick="javascript:findcode('frmMain','','ddlAccCode','1','','','','','','','','','','','','','','');" CausesValidation="False" RunAt="Server" />
						<asp:RequiredFieldValidator id=rfvAccCode display=Dynamic runat=server 
							ControlToValidate=ddlAccCode />
						<asp:dropdownlist id=ddlBank width=100% runat=server />
						<asp:RequiredFieldValidator id=rfvBank display=Dynamic runat=server 
							ErrorMessage="Please select a Bank."
							ControlToValidate=ddlBank />
					</td>
					<td>&nbsp;</td>
					<td>Print Date : </td>
					<td><asp:Label id=lblPrintDate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr id=trChequeNo runat=server>
					<td height=25>Cheque No : </td>
					<td><asp:Label id=lblChequeNo runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>Amount : </td>
					<td><asp:Label id=lblTotalAmount runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Actual Payment :*</td>
					<td>
						<!-- Modified BY ALIM maxlength=22,width=75%,RegularExpValidator-->		
						<asp:textbox id=txtActualPayment OnKeyUp="javascript:CalOutstandingAmount();" width=75% maxlength=22 runat=server />
						<asp:RequiredFieldValidator id=rfvActPayment display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Actual Payment."
							ControlToValidate=txtActualPayment />
						<asp:RegularExpressionValidator id=revActPayment 
							ControlToValidate="txtActualPayment"
							ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
						<asp:label id=lblErrRange forecolor=red text="<br>The value is out of acceptable range." visible=false runat=server />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25>Outstanding Amount : </td>
					<td><span id=currOutstandingAmount name=currOutstandingAmount></span></td>
					<td colspan=4>&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6" height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" height=25>
						<asp:label id=lblErrEntrySetup visible=false forecolor=red text="Please complete Double Entry Setup before process wages." runat=server />
						<asp:label id=lblErrPaySetup visible=false forecolor=red text="Please complete Payroll Setup before process wages." runat=server />
					</td>
				</tr>			
				<tr>
					<td colspan="6">
						<asp:ImageButton id=btnPaid AlternateText="  Paid  " imageurl="../../images/butt_paid.gif" onclick=Button_Click CommandArgument=Paid runat=server />
						<asp:ImageButton id=btnVoid AlternateText="  Void  " imageurl="../../images/butt_void.gif" onclick=Button_Click CommandArgument=Void runat=server />
						<asp:ImageButton id=btnBack AlternateText="  Back  " imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
						<asp:label id=lblBankAccCode visible=false runat=server />
						<asp:label id=lblHiddenSts visible=false text="0" runat=server/>
						<input type=hidden id=hidLastOutAmt name=hidLastOutAmt runat=server />
						<input type=hidden id=hidAmount name=hidAmount runat=server />
						<input type=hidden id=hidTotalAmount name=hidTotalAmount runat=server />
						<input type=hidden id=hidOutstandingAmount name=hidOutstandingAmount runat=server/>
						<input type=hidden id=hidOutPayEmpADCode name=hidOutPayEmpADCode runat=server />
						<input type=hidden id=hidCompleteSetup name=hidSetupSts value="no" runat=server />
						<input type=hidden id=hidCRAccCode name=hidCRAccCode runat=server />
						<input type=hidden id=hidCompleteDoubleEntry name=hidDoubleEntrySts value="no" runat=server />
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
