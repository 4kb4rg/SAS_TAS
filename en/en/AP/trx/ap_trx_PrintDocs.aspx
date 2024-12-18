<%@ Page Language="vb" src="../../../include/AP_trx_PrintDocs.aspx.vb" Inherits="AP_trx_PrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<head>
    <title>Print Documents</title> 
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();document.frmMain.txtTrxID.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain runat="server">
    <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    <asp:Label id=lblBankFrom visible=false runat=server />
    <asp:Label id=lblBankTo visible=false runat=server />
		<table id=tblMain width=100% border=0 cellspacing="1" cellpadding="1"  runat=server>
			<tr>
				<td colspan=2 class="mt-h"><asp:Label id=lblDocName runat=server/></td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			
			<tr>
				<td width=35%>Tanda Terima Tagihan No. : </td>
				<td width=55%><asp:textbox id=txtTrxID maxlength=128 width=50% runat=server />
				              <asp:Label id=lblErrTrxID forecolor=red visible=false text="Please enter Tanda Terima Tagihan No." runat=server/></TD>
				<td colspan=2>&nbsp;</td>
			</tr>
			<TR>
				<TD>Supplier Code :</TD>
				<TD><asp:DropDownList width=90% id=ddlSuppCode runat=server />
					<input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'ddlSuppCode', 'False');" CausesValidation=False runat=server />
					<asp:Label id=lblSuppCode text="Please Select Supplier Code" forecolor=red visible=false runat=server />
				</TD>
				<td colspan=2>&nbsp;</td>
			</TR>
			
			<tr ><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton ID=PrintBtn onclick=PreviewBtn_Click CausesValidation=false ImageUrl="../../images/butt_print.gif" AlternateText="Print " Runat=server />
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
			
		</table>
    </form>
</body>
</html>
