<%@ Page Language="vb" trace=false src="../../../include/CB_trx_InterestAdjDet.aspx.vb" Inherits="cb_trx_InterestAdjDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<HTML>
	<head>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
    </head>
	<%--<HEAD>
		<title>Interest Adjustment Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>--%>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
         <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuCB id="MenuCB" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                 <strong style="text-align: left">INTEREST ADJUSTMENT DETAILS</strong>  </td>
                                     <td class="font9Header" style="text-align: right">
                                    Status&nbsp; : <asp:Label id="lblStatus" runat="server" />| Date Created : <asp:Label id="lblCreateDate" runat="server" />&nbsp;| 
                                    Last Update :<asp:Label id="lblLastUpdate" runat="server" />&nbsp;| Update By :&nbsp; <asp:Label id="lblUpdateBy" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<TR>
					<TD height="25">Interest Code :*</TD>
					<TD width="30%"><asp:label id="lblIntCode" Runat="server" /></TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25">Description :*</TD>
					<TD><asp:textbox id="txtDescription" width="100%" maxlength="32" runat="server" />
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
					<TD><asp:DropDownList width="100%"  autopostback=true id="ddlDepCode" onSelectedIndexChanged=onDepCode_Change runat="server" />
						<asp:RequiredFieldValidator id="rfvDepCode" runat="server" ErrorMessage="Please Select Deposit Code" ControlToValidate="ddlDepCode"
							display="dynamic" />
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Amount :</TD>
					<TD width="30%"><asp:Label id="lblAmount" runat="server" /></TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Total Interest Amount :</TD>
					<TD width="30%"><asp:Label id="lblTotalInt" runat="server" /></TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD height="25" width="20%">Amount Adjusted :*</TD>
					<TD>
						<asp:Textbox id="txtAmountAdj" width="70%" maxlength="22" runat="server" />
						<asp:RequiredFieldValidator id="rfvAmountAdj" display="dynamic" runat="server" ErrorMessage="<br>Please key in Amount Adjusted"
							ControlToValidate="txtAmountAdj" />
						<asp:CompareValidator id="cvAmountAdj" display="dynamic" runat="server" ControlToValidate="txtAmountAdj" Text="<br>The value must whole number or with decimal. "
							Type="Double" Operator="DataTypeCheck" />
						<asp:RegularExpressionValidator id="revAmountAdj" ControlToValidate="txtAmountAdj" ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic" text="Maximum length 19 digits and 2 decimal points. " runat="server" />
						<asp:Label id="lblErrAmountAdj" visible="false" text="Amount Adjusted is invalid." forecolor="red" runat="server" />
					</TD>
				</TR>
				<TR>
					<TD height="25">Remarks :</TD>
					<TD colspan="5" width="80%"><asp:textbox id="txtRemarks" width="100%" maxlength="32" runat="server" /></TD>
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
					</TD>
				</tr>
				<tr>
					<TD colSpan="6">
						&nbsp;</TD>
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
