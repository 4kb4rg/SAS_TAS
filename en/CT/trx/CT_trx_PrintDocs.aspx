<%@ Page Language="vb" src="../../../include/CT_trx_PrintDocs.aspx.vb" Inherits="CT_trx_PrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>Print Documents</title> 
    <Preference:PrefHdl id=PrefHdl runat="server" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>

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
				<td width=40%>From :* </td>
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
			<tr id=TrIssueType>
				<td width=40%>Issue Type : </td>
				<td width=60%><asp:dropdownlist id=ddlIssueType runat=server /></td>
			</tr>
			<tr id=TrDispCost>
				<td colspan=2>		
					<asp:checkboxlist id="cblDisplayCost" runat="server">
						<asp:listitem id=option1 value="Display Unit Price in Canteen Issue Slip." selected runat="server"/>
					</asp:checkboxlist>
				</td>
			</tr>
			<tr><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton id=ibConfirm alternatetext="Confirm" imageurl="../../images/butt_confirm.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
					<asp:Label id=lblPR visible=false text="Purchase Request" runat=server/>
					<asp:Label id=lblSRA visible=false text="Canteen Return Advise" runat=server/>
					<asp:Label id=lblST visible=false text="Canteen Transfer" runat=server/>
					<asp:Label id=lblSI visible=false text="Canteen Issue" runat=server/>
					<asp:Label id=lblFI visible=false text="Fuel Issue" runat=server/>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
