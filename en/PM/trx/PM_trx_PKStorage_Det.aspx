<%@ Page Language="vb" Src="../../../include/PM_trx_PKStorage_Det.aspx.vb" Inherits="PM_Trx_PKStorage_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>PK Storage Transaction Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<asp:Label id="lblTransDate" runat="server" Visible="False"/>
			<asp:Label id="lblStorageAreaCode" runat="server" Visible="False"/>
			<table cellSpacing="0" cellPadding="2" width="100%" border="0">
 				<tr>
					<td colspan="6"><UserControl:MenuPDTrx id=MenuPD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="50%">PK STORAGE DETAILS</td>
					<td colspan="3" align=right width="50%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width="20%" height=25>Transaction Date :*</td>
					<td width="30%">
						<asp:TextBox id="txtdate" runat="server" width=70% maxlength="20"/>                       
						<a href="javascript:PopCal('txtdate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a><BR>
						<asp:RequiredFieldValidator 
							id="rfvDate" 
							runat="server"  
							ControlToValidate="txtdate" 
							text = "Field cannot be blank<br>"
							display="dynamic"/>
						<asp:label id=lblDate Text ="Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
						<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Period :</td>
					<td width=25%>
						<asp:Label id="lblPeriod" runat="server"/>
						<asp:Label id="txtAccMonth" runat="server" Visible="False"/>
						<asp:Label id="txtAccYear" runat="server" Visible="False"/>
					</td>
				</tr>
				<tr>
					<td height=25>Storage Area :*</td>
					<td><asp:DropDownList id="lstStorageArea" width=100% runat="server" size="1" />
						<asp:RequiredFieldValidator id="validateStorageArea" runat="server" 
							display="dynamic" 
							ControlToValidate="lstStorageArea"
							Text="Please select storage area." />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>PK Ullage (MM) :*</td>
					<td><asp:TextBox id="txtPKUllage" runat="server" width=70% maxlength="21" />                       
						<asp:RequiredFieldValidator 
							id="rfvPKUllage" 
							runat="server"  
							ControlToValidate="txtPKUllage" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPKUllage" 
							ControlToValidate="txtPKUllage"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
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
					<td height=25>PK Weight (KG) :*</td>
					<td><asp:TextBox id="txtPKWeight" runat="server" width=70% maxlength="21"/> 
						<asp:Button id="btnCalculatePKWeight" Text="Calculate" runat="server" />
						<asp:Label id="rfvPKWeight" TEXT="Field cannot be blank" forecolor=red runat="server" Visible="False"/>
						<asp:Label id="revPKWeight" text="Maximum length 15 digits and 5 decimal points" forecolor=red visible=false runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td><asp:Label id="lblCalcErr" runat="server" Visible="False" Text='Calculation Error' forecolor=red /></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><asp:label id="lblDupMsg"  Text="Transaction for selected date and storage area already exist" Visible=false forecolor=red Runat="server"/></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" Visible=False CausesValidation=False />
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
