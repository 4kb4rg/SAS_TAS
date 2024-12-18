
<%@ Page Language="vb" trace=false src="../../../include/CB_trx_DepositDet.aspx.vb" Inherits="cb_trx_DepositDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Deposit Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
                <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
		<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0"  class="font9Tahoma">
			<tr>
				<td colspan="6"><UserControl:MenuCB id=MenuCB runat="server" /></td>
			</tr>
			<tr>
				<td  colspan="6">
                    <table cellpadding="0" cellspacing="0" class="style1"  class="font9Tahoma">
                        <tr>
                            <td  class="font9Tahoma">
                              <strong> DEPOSIT DETAILS </strong> </td>
                            <td class="font9Header">
                                Status : <asp:Label id="lblStatus" runat="server"/>| Date Created : <asp:Label id="lblCreateDate" runat="server"/>
                                | Last Update : <asp:Label id="lblLastUpdate" runat="server"/>|&nbsp; Update By :<asp:Label id="lblUpdateBy" runat="server"/>
                            </td>
                        </tr>
                    </table>
                </td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" /></td>
			</tr>			
				
				<TR>
					<TD height=25>Deposit Code :*</TD>
					<TD width="30%"><asp:label id=lblDepCode Runat="server"/></TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Description :*</TD>
					<TD><asp:textbox id=txtDescription width=100% maxlength=32 runat=server />
						<asp:RequiredFieldValidator 
							id="rfvDescription" 
							runat="server" 
							ErrorMessage="Please key in Description" 
							ControlToValidate="txtDescription" 
							display="dynamic"/>
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Bilyet No :*</TD>
					<TD><asp:textbox id=txtBilyetNo width=100% maxlength=32 runat=server/>
					<asp:RequiredFieldValidator 
							id="rfvBilyetNo" 
							runat="server" 
							ErrorMessage="Please key in Bilyet No" 
							ControlToValidate="txtBilyetNo" 
							display="dynamic"/>
					</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Account No :*</TD>
					<TD><asp:textbox id=txtAccountNo width=100% maxlength=32 runat=server/>
					<asp:RequiredFieldValidator 
							id="rfvAccountNo" 
							runat="server" 
							ErrorMessage="Please key in Account No" 
							ControlToValidate="txtAccountNo" 
							display="dynamic"/>
					</TD>		
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Bank :*</TD>
					<TD><asp:DropDownList id=ddlBank width=100%  runat=server />
						<asp:RequiredFieldValidator 
							id="rfvBankCode" 
							runat="server" 
							ErrorMessage="Please Select Bank Code" 
							ControlToValidate="ddlBank" 
							display="dynamic"/></td>
							
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Starting Date :*</TD>
					<TD>
						<asp:TextBox id=txtStartDate runat=server width=70% maxlength=10 />                       
						<a href="javascript:PopCal('txtStartDate');">
						<asp:Image id="btnSelStartDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:Label id=lblErrStartDate visible=False forecolor=red text="Invalid date format." runat=server />
						<asp:RequiredFieldValidator 
							id="rfvStartDate" 
							runat="server" 
							ErrorMessage="Please key in Starting Date" 
							ControlToValidate="txtStartDate" 
							display="dynamic"/>
					</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<td height="25">Ending Date :*</td>
					<TD><asp:TextBox id=txtEndDate  runat=server width=70% maxlength=10 />
						<a href="javascript:PopCal('txtEndDate');"><asp:Image id="btnSelEndDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrEndDate visible=False forecolor=red text="Invalid date format." runat=server />
						<asp:RequiredFieldValidator 
							id="rfvEndDate" 
							runat="server" 
							ErrorMessage="Please key in Ending Date" 
							ControlToValidate="txtEndDate" 
							display="dynamic"/>
					</TD>
					<td>&nbsp;</td>
				</TR>
				
				<TR>
					<TD height=25 width=20%>Amount :*</TD>
					<TD>
						<asp:Textbox id=txtAmount width=70% maxlength=22 runat=server/>
						<asp:RequiredFieldValidator id="rfvAmount" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Amount" 
							ControlToValidate="txtAmount" />																
						<asp:CompareValidator id="cvAmount" display="dynamic" runat="server" 
							ControlToValidate="txtAmount" Text="The value must greater then 0. " 
							ValueToCompare="0" Operator="GreaterThan" Type="Double"/>
							<asp:RegularExpressionValidator id="revAmount" 
							ControlToValidate="txtAmount"
							ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
							<asp:Label id=lblErrAmount visible=false text="Amount is invalid." forecolor=red runat=server/>
					</TD>
				</TR>		
				
				<TR>
					<TD height=25 width=20%>Rate :*</TD>
					<TD>
						<asp:Textbox id=txtRate width=70% maxlength=22 runat=server/>
						<asp:RequiredFieldValidator id="rfvRate" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Rate" 
							ControlToValidate="txtRate" />																
						<asp:CompareValidator id="cvRate" display="dynamic" runat="server" 
							ControlToValidate="txtRate" Text="The value must >= 0. " 
							ValueToCompare="0" Operator="GreaterThanEqual" Type="Double"/>
							<asp:RegularExpressionValidator id="revRate" 
							ControlToValidate="txtRate"
							ValidationExpression="^(\-|)\d{1,2}(\.\d{1,5}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 2 digits and 5 decimal points. "
							runat="server"/>
							<asp:Label id=lblErrRate visible=false text="Rate is invalid." forecolor=red runat=server/>
					</TD>
				</TR>		
				
				<TR>
					<TD height=25 width=20%>Currency Rate :*</TD>
					<TD>
						<asp:Textbox id=txtCurRate width=70% maxlength=22 runat=server/>
						<asp:RequiredFieldValidator id="rfvCurRate" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Currency Rate" 
							ControlToValidate="txtCurRate" />																
						<asp:CompareValidator id="cvCurRate" display="dynamic" runat="server" 
							ControlToValidate="txtCurRate" Text="The value must greater then 0. " 
							ValueToCompare="0" Operator="GreaterThan" Type="Double"/>
							<asp:RegularExpressionValidator id="revCurRate" 
							ControlToValidate="txtCurRate"
							ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
							<asp:Label id=lblErrCurRate visible=false text="Currency Rate is invalid." forecolor=red runat=server/>
					</TD>
				</TR>		
				
				<TR>
					<TD height=25>Currency :*</TD>
					<TD><asp:DropDownList width=100% id=ddlCurrency runat=server />
						<asp:RequiredFieldValidator 
							id="rfvCurrency" 
							runat="server" 
							ErrorMessage="Please Select Currency" 
							ControlToValidate="ddlCurrency" 
							display="dynamic"/>
					</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25 width="20%">Conversion Amount :</TD>
					<TD width="30%"><asp:Label id=lblConvAmount runat=server /></TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Type :*</TD>
					<TD><asp:DropDownList width=100% id=ddlType runat=server>
					    <asp:ListItem value="">Please Select Deposit Type</asp:ListItem>
						<asp:ListItem value="1">Type I (ARO Principal + Interest)</asp:ListItem>
						<asp:ListItem value="2">Type II (ARO Principal,Interest Transfer To)</asp:ListItem>
						</asp:DropDownList>
						<asp:RequiredFieldValidator 
							id="rfvType" 
							runat="server" 
							ErrorMessage="Please Select Type" 
							ControlToValidate="ddlType" 
							display="dynamic"/>
					</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<TR>
					<TD height=25>Chart of Account :*</TD>
					<TD><asp:DropDownList id="ddlAccCode" Width=100% runat=server />
					<asp:RequiredFieldValidator 
							id="rfvAccCode" 
							runat="server" 
							ErrorMessage="Please Select Chart of Account" 
							ControlToValidate="ddlAccCode" 
							display="dynamic"/>	
					</TD>
					<TD>&nbsp;</TD>
				</TR>
				
				
				<TR>
					<TD height=25>Remarks :</TD>
					<TD colspan=5 width=80%><asp:textbox id=txtRemarks width=100% maxlength=32 runat=server/></TD>
					<TD>&nbsp;</TD>
				</TR>
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<TD colSpan="6">
					<asp:ImageButton ID=SaveBtn CausesValidation=True onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=ConfirmBtn CausesValidation=True onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=CancelledBtn CausesValidation=False onclick=CancelledBtn_Click ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancelled Runat=server />
					<asp:ImageButton ID=UndeleteBtn CausesValidation=False onclick=UndeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=DeleteBtn CausesValidation=False onclick=DeleteBtn_Click ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />			
					<asp:ImageButton ID=BackBtn CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
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
			</table>
			<INPUT type="hidden" id="lbhStatus" runat="server">
                                                    </div>
            </td>
        </tr>
        </table>
		</form>
	</body>
</html>
