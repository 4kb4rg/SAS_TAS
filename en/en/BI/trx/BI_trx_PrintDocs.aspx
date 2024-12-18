<%@ Page Language="vb" src="../../../include/BI_trx_PrintDocs.aspx.vb" Inherits="BI_trx_PrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
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
<body onload="javascript:self.focus();document.frmMain.txtFromId.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain runat="server">
    <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<table id=tblMain width=100% border=0 runat=server>
			<tr>
				<td colspan=2 class="mt-h"><asp:Label id=lblDocName runat=server/></td>
			</tr>
			<tr>
				<td colspan=2><hr size="1" noshade></td>
			</tr>
			<tr>
				<td width=40%>From : </td>
				<td width=60%>
					<asp:TextBox id=txtFromId width=50% maxlength=20 onkeypress="javascript:keypress()" runat=server />
					<asp:RequiredFieldValidator id=rfvFrom display=dynamic runat=server 
						ErrorMessage="<br>Please enter document ID." 
						ControlToValidate=txtFromId />					
					<asp:RegularExpressionValidator id=revFrom 
						ControlToValidate="txtFromId"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr>
				<td width=40%>To : </td>
				<td width=60%>
					<asp:TextBox id=txtToId width=50% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revTo 
						ControlToValidate="txtToId"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
					<asp:Label id=lblErrCode visible=false forecolor=red text="Invalid code given.<br>" runat=server/>
				</td>
			</tr>	
			<tr>
				<td width=40%>Billing Type : </td>
				<td width=60%><asp:dropdownlist id=ddlBillType runat=server /></td>
			</tr>
			<tr><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton id=ibConfirm alternatetext="Confirm" imageurl="../../images/butt_confirm.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
					<asp:Label id=lblDN visible=false text="Billing Debit Note" runat=server/>
					<asp:Label id=lblCN visible=false text="Billing Credit Note" runat=server/>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
