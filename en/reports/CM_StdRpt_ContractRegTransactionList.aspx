<%@ Page Language="vb" src="../../include/reports/CM_StdRpt_ContractRegtransactionList.aspx.vb" Inherits="CM_StdRpt_ContractRegTransactionList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Contract Management - Contract Registration Transaction Listing</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>CONTRACT MANAGEMENT - CONTRACT REGISTRATION TRANSACTION LISTING</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
						<asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
						<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /> 
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">
				
				<tr>
					<td width="15%">Contract No : </td>
					<td width="35%"><asp:textbox id="txtContractNo" maxlength=20  width="50%" runat="server" /> (blank for all)</td>
					<td width="4%">&nbsp;</td>
					<td width="35%">&nbsp;</td>
				</tr>
				<tr>
					<td width=20%>Contract Type : </td>
					<td width=30%>
						<asp:RadioButtonList id=rdContractType repeatdirection=horizontal onSelectedIndexChanged=onChange_ContractType autopostback=true runat=server>
						<asp:ListItem id=item1 value=1 text=Purchase runat=server />
						<asp:ListItem id=item2 Value=2 text=Sales runat=server selected />
						</asp:RadioButtonList>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
														
				<tr ID="trUpdateDate" >
					<td>Contract Date From : </td>
					<td><asp:TextBox id="txtDateFrom" maxlength="10"  width="50%" runat="server"/>
  						<a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
  					</td>				
					<td>To : </td>
					<td>
						<asp:TextBox id="txtDateTo"  maxlength="10"  width="50%" runat="server"/>
  						<a href="javascript:PopCal('txtDateTo');">
						<asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
					</td>
				</tr>
				<tr>
					<td>Product : </td>
					<td><asp:DropDownList id="ddlProduct" maxlength=8  width="50%" runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id=TrBillParty runat=server>
					<td><asp:label id=lblBillParty runat=server /> Code : </td>
					<td><asp:textbox id=txtBillParty runat=server maxlength=8  width=50% /> (blank for all)</td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr id=TrSupplier runat=server>
					<td>Seller Code : </td>
					<td><asp:textbox id=txtSupplier runat=server maxlength=8  width=50% /> (blank for all)</td>
					<td colspan=2>&nbsp;</td>
				</tr>

				<tr>
					<td>Currency Code : </td>
					<td><asp:DropDownList id="ddlCurrency" width="50%" runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	

				<tr>
					<td>Price Basis : </td>
					<td><asp:DropDownList id="ddlPriceBasis" maxlength=8  width="50%" runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Delivery Month : </td>
					<td>
						<asp:DropDownList id="ddlDelMonth" width="24%" runat=server>
							<asp:ListItem text="Jan" value="1" />
							<asp:ListItem text="Feb" value="2" />
							<asp:ListItem text="Mar" value="3" />
							<asp:ListItem text="Apr" value="4" />
							<asp:ListItem text="May" value="5" />
							<asp:ListItem text="Jun" value="6" />
							<asp:ListItem text="Jul" value="7" />
							<asp:ListItem text="Aug" value="8" />
							<asp:ListItem text="Sep" value="9" />
							<asp:ListItem text="Oct" value="10" />
							<asp:ListItem text="Nov" value="11" />
							<asp:ListItem text="Dec" value="12" />
							<asp:ListItem text="All" value="" Selected />																	
						</asp:DropDownList>&nbsp;										
					<asp:DropDownList id="ddlDelYear" width="24%" runat="server" />
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="lstStatus"  width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>										
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" Visible="False" />
                    </td>
				</tr>				
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:Label id="LocTag" visible="false" runat="server" />
		<asp:Label id="lblAccount" visible="false" runat="server" />
		<asp:Label id="lblBlock" visible="false" runat="server" />
	</body>
</HTML>
