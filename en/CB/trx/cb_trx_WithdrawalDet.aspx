
<%@ Page Language="vb" trace=false src="../../../include/CB_trx_WithdrawalDet.aspx.vb" Inherits="cb_trx_WithdrawalDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<HTML>
	<HEAD>
		<title>Withdrawal Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</HEAD>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
			<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuCB id="MenuCB" runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                   <strong> WITHDRAWAL DETAILS</strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id="lblStatus" runat="server" />&nbsp;| Date Created : <asp:Label id="lblCreateDate" runat="server" />&nbsp;| Last Update :<asp:Label id="lblLastUpdate" runat="server" />&nbsp;|&nbsp; 
                                    Update By : <asp:Label id="lblUpdateBy" runat="server" /></td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<TR>
					<TD height="25">Withdrawal Code :*</TD>
					<TD width="30%"><asp:label id="lblWdrCode" Runat="server" /></TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25">Description :*</TD>
					<TD><asp:textbox id="txtDescription" CssClass="font9Tahoma"   width="100%" maxlength="32" runat="server" />
						<asp:RequiredFieldValidator id="rfvDescription" runat="server" ErrorMessage="Please key in Description" ControlToValidate="txtDescription"
							display="dynamic" />
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25">Deposit Code :*</TD>
					<TD><asp:DropDownList id="ddlDepCode" CssClass="font9Tahoma"   autopostback=true onSelectedIndexChanged=onDepCode_Change width="100%" runat="server" />
						<asp:RequiredFieldValidator id="rfvDepCode" runat="server" ErrorMessage="Please Select Deposit Code" ControlToValidate="ddlDepCode"
							display="dynamic" />
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Bilyet No :</TD>
					<TD width="30%"><asp:Label id="lblBilyetNo" runat="server" /></TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Account No :</TD>
					<TD width="30%"><asp:Label id="lblAccountNo" runat="server" /></TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Amount :</TD>
					<TD width="30%"><asp:Label id="lblAmount" runat="server" /></TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Rate :*</TD>
					<TD>
						<asp:Textbox id="txtRate" width="70%" CssClass="font9Tahoma"   maxlength="22" runat="server" />
						<asp:RequiredFieldValidator id="rfvRate" display="dynamic" runat="server" ErrorMessage="<br>Please key in Rate"
							ControlToValidate="txtRate" />
						<asp:CompareValidator id="cvRate" display="dynamic" runat="server" ControlToValidate="txtRate" Text="The value must >= 0. " 
							ValueToCompare="0" Operator="GreaterThanEqual" Type="Double"/>
						<asp:RegularExpressionValidator id="revRate" ControlToValidate="txtRate" ValidationExpression="^(\-|)\d{1,2}(\.\d{1,5}|\.|)$"
							Display="Dynamic" text="Maximum length 2 digits and 5 decimal points. " runat="server" />
						<asp:Label id="lblErrRate" visible="false" text="Rate is invalid." forecolor="red" runat="server" />
					</TD>
				</TR>
				<TR>
					<TD height="25">Remarks :</TD>
					<TD colspan="5" width="80%"><asp:textbox id="txtRemarks" CssClass="font9Tahoma"   width="100%" maxlength="32" runat="server" /></TD>
					<TD>&nbsp;</TD>
				</TR>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<TD colSpan="6">
						<asp:ImageButton ID="SaveBtn" CausesValidation="True" onclick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif"
							AlternateText="Save" Runat="server" />
						<asp:ImageButton ID="ConfirmBtn" CausesValidation="False" onclick="ConfirmBtn_Click" ImageUrl="../../images/butt_confirm.gif"
							AlternateText="Confirm" Runat="server" />
						<asp:ImageButton ID="CancelledBtn" CausesValidation="False" onclick="CancelledBtn_Click" ImageUrl="../../images/butt_cancel.gif"
							AlternateText="Cancelled" Runat="server" />
						<asp:ImageButton ID="UndeleteBtn" CausesValidation="False" onclick="UndeleteBtn_Click" ImageUrl="../../images/butt_undelete.gif"
							AlternateText="Undelete" Runat="server" />
						<asp:ImageButton ID="DeleteBtn" CausesValidation="False" onclick="DeleteBtn_Click" ImageUrl="../../images/butt_delete.gif"
							AlternateText="Delete" Runat="server" />
						<asp:ImageButton ID="BackBtn" CausesValidation="False" onclick="BackBtn_Click" ImageUrl="../../images/butt_back.gif"
							AlternateText="Back" Runat="server" />
					    <br />
					</TD>
				</tr>
				<tr>
					<TD colSpan="6">
					<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." forecolor=red runat=server />
					</TD>
				</tr>
			</TABLE>
			<INPUT type="hidden" id="lbhStatus" runat="server">
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</HTML>
