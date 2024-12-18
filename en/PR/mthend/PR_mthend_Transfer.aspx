
<%@ Page Language="vb" src="../../../include/PR_mthend_Transfer.aspx.vb" Inherits="PR_mthend_Transfer" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll Process</title>
	</HEAD>
	<body>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
		<form id=frmMain runat=server>
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan="2">
						<UserControl:MenuPRMthEnd id="MenuPRMthEnd" runat="server" /><STRONG></STRONG>
					</td>
				</tr>
				<tr>
					<td colspan="2" class="mt-h" width="100%" style="HEIGHT: 19px"><STRONG>TRANSFER 
							INTERFACE</STRONG></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<P class="HTBodyText"><SPAN style="mso-ansi-language: EN-US">This Process will generate 
								interface payroll data in text base automatically</SPAN></P>
					</td>
				</tr>
				<tr>
					<td height=25 width=20%"></td>
					<td></td>
				</tr>
				<tr valign="top">
					<td height=25 width=20%" >Company Code :</td>
					<td>
						<asp:TextBox id="txtCompanyCode" runat="server" Width="50%" MaxLength="25" >AIPA</asp:TextBox></td>
				</tr>
				<tr valign="top">
					<td height=25 width=20%" >No. Registrasi BCA :</td>
					<td>
						<asp:TextBox id="txtRegBCA" runat="server" Width="50%" MaxLength="25" >003500010AIPA</asp:TextBox>
					</td>
				</tr>
				<tr valign="top">
					<td height=25 width=20%" >Bank Acc. for Payment :</td>
					<td>
						<asp:TextBox id="txtBankAcc" runat="server" Width="50%" MaxLength="25" >441000003500</asp:TextBox>
					</td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr valign=Top>				
					<td height=25>Process Date :*</td>
					<td>
						<asp:Textbox id="txtProDate" width=25% maxlength=10 runat=server/>
						<asp:Label id=lblErrProcessDate visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrProcessDateDesc visible=false text="<br>Date format " runat=server/>
						<a href="javascript:PopCal('txtProDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
					</td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr valign="top">
					<td height=25 width=20%">Periode :</td>
					<td>Month :&nbsp;
						<asp:DropDownList id="ddlMonth" runat="server"></asp:DropDownList>&nbsp;Year :
						<asp:DropDownList id="ddlYear" runat="server"></asp:DropDownList></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td height=25 width=20%"></td>
					<td>
						<asp:RadioButton id="rbPro1" runat="server" Text="Payroll (without THR/Bonus)" GroupName="pro" ></asp:RadioButton>
						&nbsp;
					</td>
				</tr>
				<tr>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:RadioButton id="rbPro2" runat="server" Text="Allowance (THR/Bonus)" GroupName="pro"></asp:RadioButton>
					</td>
				</tr>
				<tr>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:RadioButton id="rbPro3" runat="server" Text="ALL (Payroll + Allowance)" GroupName="pro" Checked="True"></asp:RadioButton>
					</td>
				</tr>
				<tr>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:ImageButton id="btnGenerate" onclick="btnGenerate_Click" imageurl="../../images/butt_process.gif"
							alternatetext="Generate" runat="server" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>