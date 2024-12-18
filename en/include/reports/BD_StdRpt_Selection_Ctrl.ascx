<%@ Control Language="VB"  Inherits="BD_STDRPT_SELECTION_CTRL" src="../../../include/Reports/BD_StdRpt_Selection_Ctrl.ascx.vb" %>
<link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
<input type=Hidden id=hidUserLoc runat="server" NAME="hidUserLoc"/>
<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />			
<asp:Label id=lblCropTag visible=false runat=server />
<asp:Label id=lblVehicleTag visible=false runat=server />
<asp:Label id=lblActivityTag visible=false runat=server />

<table cellspacing="0" cellpadding="0" width=100% border="0" id="TABLE1" class="font9Tahoma" runat="server">
	<tr>
		<td>
			<table id=tblRptSelect1 cellspacing="0" cellpadding="0" border="0" align="left" class="font9Tahoma">
				<tr>
					<td valign="center"></td>
					<td width="1"></td>
					<td></td>
					<td width="1"></td>
					<td></td>
					<td width="1"></td>
					<td></td>
				</tr>
			</table>
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>
			<table width=100% cellspacing="0" cellpadding="1" border="0" align="left" ID="tblRptSelect2" class="font9Tahoma" runat=server>
				<tr>
					<td>Report Name : </td>
					<td colspan=2><asp:DropDownList id="lstRptname" width=80% size=1 AutoPostBack=true OnSelectedIndexchanged=CheckRptName runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr id=TrLoc>
					<td valign=top>Select the location you want for reporting : </td>
					<td colspan=2>
						<table width=50% cellpadding=0 cellspadding=0 class="font9Tahoma">
							<tr>
								<td><asp:CheckBox text=All id="cbLocAll" OnCheckedChanged=Check_Clicked AutoPostBack=true class="font9Tahoma" runat=server /></td>
								<td><asp:CheckBoxList id=cblUserLoc OnSelectedIndexChanged="LocCheckList" RepeatColumns="5" RepeatDirection="Horizontal" class="font9Tahoma" AutoPostBack=True runat=server /></td>
							</tr>
						</table>
					</td>
					<td><asp:label id=lblLocation runat=server /></td>
				</tr>	
				<tr id="TrPeriod">
					<td>Period Name : </td>
					<td><asp:DropDownList id="lstPeriodName" size=1 width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>																
				<tr class="font9Tahoma">
					<td width=40%>Select number of decimal(s) for reporting : </td>
					<td width=20%><asp:DropDownList id=lstDecimal width=40% runat=server>
										<asp:ListItem text="0" value="0" />
										<asp:ListItem text="1" value="1" />
										<asp:ListItem text="2" value="2" />
										<asp:ListItem text="3" value="3" />
										<asp:ListItem text="4" value="4" />									
										<asp:ListItem text="5" value="5" />
								  </asp:DropDownList></td>
					<td width=20%>&nbsp;</td>					
					<td width=20%>&nbsp;</td>				
				</tr>			
				<tr>
					<td colspan=4><asp:Label id=lblUserLoc forecolor=red  visible=false Text="You must select at least one location." runat=server /></td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id=lblPeriod forecolor=red  visible=false Text="You must select a Period." runat=server /></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
