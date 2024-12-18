<%@ Page Language="vb" src="../../../include/CB_trx_PrintDocs.aspx.vb" Inherits="CB_trx_PrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<head>
    <title>Print Documents</title> 
               <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
    <style type="text/css">
        .style1
        {
            width: 25%;
        }
        .style2
        {
            width: 58%;
        }
    </style>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();document.frmMain.txtChequeNo.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 150px" class="font9Tahoma">
		<tr>
             <td style="width: 50%; height: 500px" valign="top">
			    <div class="kontenlist">  
    <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    <asp:Label id=lblBankFrom visible=false runat=server />
    <asp:Label id=lblBankTo visible=false runat=server />
		<table id=tblMain width=100% border=0 cellspacing="1" cellpadding="1"  runat=server class="font9Tahoma">
			<tr>
				<td colspan=2 class="mt-h"><asp:Label id=lblDocName runat=server/></td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
			</tr>
			
			<tr>
				<td class="style1">Cheque/Bilyet Giro No. : </td>
				<td class="style2"><asp:textbox id=txtChequeNo CssClass="font9Tahoma" maxlength=128 width=50% runat=server />
				              <asp:ImageButton ImageAlign=AbsBottom ID=btnGetCG onclick=GetCGBtn_Click CausesValidation=False ImageUrl="../../images/icn_next.gif" AlternateText="Get Data" Runat=server />
				              <asp:Label id=lblErrCheque forecolor=red visible=false text="Please enter Cheque/Bilyet Giro No." runat=server/></TD>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">Cheque/Bilyet Giro No. To Print : </td>
				<td class="style2"><asp:textbox id=txtChequeNoToPrint CssClass="font9Tahoma" width=50% runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<TR>
				<TD height=25 class="style1">Payment Type : </TD>
				<TD class="style2"><asp:DropDownList width=100% id=ddlPayType runat=server>
						<asp:ListItem value="0">Cheque</asp:ListItem>
						<asp:ListItem value="1">Cash</asp:ListItem>
						<asp:ListItem value="2">Other Party</asp:ListItem>
						<asp:ListItem value="3">Bilyet Giro</asp:ListItem>
					</asp:DropDownList>
				</TD>
				<TD>&nbsp;</TD>
			</TR>
			<TR>
				<TD class="style1">Bank From :</TD>
				<TD class="style2"><asp:DropDownList width=100% id=ddlBank AutoPostBack=true OnSelectedIndexChanged=onSelect_Change runat=server />
					<asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/></TD>
				<td>&nbsp;</td>
			</TR>
			<tr>
				<TD height=25 class="style1">Bank To :</TD>
				<TD class="style2"><asp:DropDownList width=100% id=ddlBankTo AutoPostBack=true OnSelectedIndexChanged=onSelect_Change runat=server />
					<asp:Label id=lblErrBankTo forecolor=red visible=false text="Please select Bank Code"  runat=server/></TD>
				<td>&nbsp;</td>
			</tr>
			<TR>
				<TD height=25 class="style1">Bank Account No. :</TD>
				<TD class="style2"><asp:Textbox id=txtBankAccNo CssClass="font9Tahoma" width=50% maxlength=32 runat=server />
					<asp:Label id=lblErrBankAccNo forecolor=red visible=false text="Please enter Bank Account No." runat=server/></TD>
				<td>&nbsp;</td>
			</TR>
			<tr>
				 <td class="style1">Currency Code :</td>
					<td class="style2"><asp:DropDownList id=ddlCurrency width=100% AutoPostBack=true OnSelectedIndexChanged=CurrencyChanged runat=server/></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">Exchange Rate : </td>
				<td class="style2"><asp:textbox id=txtExRate CssClass="font9Tahoma" text="1" width=50% runat=server />
				<asp:Label id=lblErrExRate text="Exchange rate for this date has not been created." forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">Total Amount : </td>
				<td class="style2"><asp:textbox id=txtTtlAmount CssClass="font9Tahoma" width=50% runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">Amount To Print : </td>
				<td class="style2"><asp:textbox id=txtAmtToPrint CssClass="font9Tahoma" width=50% runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">Biaya Administrasi : </td>
				<td class="style2"><asp:textbox id=txtBiaya CssClass="font9Tahoma" width=50% runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="style1">&nbsp;</td>
				<td class="style2"><asp:CheckBox id="chkDeduct" Text="  Deduct from total amount?" Checked=false runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=3><asp:label id=lblConfirmErr text="<BR>control amount must equal to zero and have data transaction" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr ><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton ID=PrintChequeBtn onclick=PreviewChequeBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_cheque.gif" AlternateText="Print Cheque" Runat=server />
					<asp:ImageButton ID=PrintSlipBtn  onclick=PreviewSlipBtn_Click CausesValidation=false ImageUrl="../../images/butt_print_slip.gif" AlternateText="Print Slip" Runat=server />
					<asp:ImageButton ID=PrintTransferBtn onclick=btnPreviewTransfer_Click CausesValidation=false ImageUrl="../../images/butt_print_slip_transfer.gif" AlternateText="Print Slip Transfer" Runat=server />
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
			<tr>
				<td colspan=2 align=center>
 					            &nbsp;</td>
			</tr>
		</table>
		<Input type=hidden id=hidSupplierCode value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
    </form>
</body>
</html>
