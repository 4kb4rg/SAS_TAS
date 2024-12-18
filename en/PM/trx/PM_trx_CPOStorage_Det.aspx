<%@ Page Language="vb" Src="../../../include/PM_trx_CPOStorage_Det.aspx.vb" Inherits="PM_Trx_CPOStorage_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>CPO Storage Transaction Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="lblLocation" runat="server" Visible="False"/>
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<asp:Label id="lblTransDate" runat="server" Visible="False"/>
			<asp:Label id="lblStorageAreaCode" runat="server" Visible="False"/>
			<table cellSpacing="0" cellPadding="2" width="100%" border="0">
 				<tr>
					<td colspan="6"><UserControl:MenuPDTrx id=MenuPD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="50%">CPO STORAGE DETAILS</td>
					<td colspan="3" align=right width="50%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="20%" height=25>Transaction Date :*</td>
					<td width="30%">
						<asp:TextBox id="txtDate" runat="server" width=70% maxlength="20" /> 
						 <a href="javascript:PopCal('txtDate');"> <asp:Image id="btnSelTransDate" runat="server" ImageUrl="../../Images/calendar.gif"/> </a> <BR>
						<asp:RequiredFieldValidator 
							id="rfvDate" 
							runat="server"  
							ControlToValidate="txtDate" 
							text = "Field cannot be blank<br>"
							display="dynamic"/>
						<asp:label id=lblDate Text ="Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
						 <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Period :</td>
					<td width=25%><asp:Label id="lblPeriod" runat="server"/>
								  <asp:Label id="txtAccMonth" runat="server" Visible="False"/>
								  <asp:Label id="txtAccYear" runat="server" Visible="False"/>
					</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Storage Area :*</td>
					<td><asp:DropDownList id="lstStorageArea" width=100% runat="server" />
						<asp:RequiredFieldValidator 
							id="rfvStorageArea" 
							runat="server"  
							ControlToValidate="lstStorageArea" 
							text = "Please select Storage Area"
							display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Temperature (&#176;C) :*</td>
					<td><asp:TextBox id="txtTemperature" runat="server" width=70% maxlength="6" />
						<asp:RequiredFieldValidator 
							id="rfvTemperature" 
							runat="server"  
							ControlToValidate="txtTemperature" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revTemperature" 
							ControlToValidate="txtTemperature"
							ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
							Display="Dynamic"
							text = "Maximum length 3 digits and 2 decimal points"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/>
						<asp:Label id="txtCreateDate" runat="server" Visible="False"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>CPO Ullage (MM) :*</td>
					<td><asp:TextBox id="txtCPOUllage" runat="server" width=70% maxlength="21" />
						<asp:RequiredFieldValidator 
							id="rfvCPOUllage" 
							runat="server"  
							ControlToValidate="txtCPOUllage" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOUllage" 
							ControlToValidate="txtCPOUllage"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>CPO Weight (KG) :*</td>
					<td><asp:TextBox id="txtCPOWeight" runat="server" width=70% maxlength="21" />
						<asp:Button id="btnCalculateCPOWeight" Text="Calculate" runat="server"/> <BR>
						<asp:Label id="rfvCPOWeight" TEXT="Field cannot be blank" forecolor=red runat="server" Visible="False"/>
						<asp:Label id="revCPOWeight" text="Maximum length 15 digits and 5 decimal points" forecolor=red Visible=False runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td><asp:Label id="lblCalcErr" runat="server" Visible="False" Text='Calculation Error' forecolor=red /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><asp:label id="lblDupMsg"  Text="Transaction for selected date and storage area already exist" Visible=false forecolor=red Runat="server"/></td>
				</tr>
				<tr>
					<td colspan="6" height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" Visible=False CausesValidation=False />
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation=False />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
