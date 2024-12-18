<%@ Control Language="VB" Inherits="PU_STDRPT_SELECTION_CTRL_RPT" src="../../../include/Reports/PU_StdRpt_Selection_Ctrl_RPT.ascx.vb" %>
<link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
<input type=Hidden id=hidUserLoc runat="server" NAME="hidUserLoc"/>
<input type=Hidden id=hidCompList runat="server" NAME="hidCompList"/>

<table cellspacing="0" cellpadding="0" width=100% border="0" class="font9Tahoma">
	<tr>
		<td>
			<table id=tblRptSelect1 cellspacing="0" cellpadding="0" border="0" align="left" class="font9Tahoma">
				<tr>
					<td valign="middle"></td>
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
				<tr>
					<td valign=top>Select the Company you want for reporting : </td>
					<td colspan=2>
						<table width=50% cellpadding=0 cellspadding=0 class="font9Tahoma">
							<tr>
								<td><asp:CheckBox text=All id="cbCompAll"  runat=server /></td>
								<td><asp:CheckBoxList id=cblCompList RepeatColumns="5" RepeatDirection="Horizontal" class="font9Tahoma" runat=server /></td>
							</tr>
						</table>
					</td>
					<td><asp:label id=Label1 runat=server /></td>
				</tr>		
				<tr>
					<td valign=top>Select the location you want for reporting : </td>
					<td colspan=2>
						<table width=50% cellpadding=0 cellspadding=0>
							<tr>
								<td><asp:CheckBox text=All id="cbLocAll" OnCheckedChanged=Check_Clicked AutoPostBack=true class="font9Tahoma" runat=server /></td>
								<td><asp:CheckBoxList id=cblUserLoc OnSelectedIndexChanged="LocCheckList" RepeatColumns="5" RepeatDirection="Horizontal" AutoPostBack=True class="font9Tahoma" runat=server /></td>
							</tr>
						</table>
					</td>
					<td><asp:label id=lblLocation runat=server /></td>
				</tr>		
				<tr id="TrMthYr">
					<td>Accounting Period : </td>
					<td>
						<asp:DropDownList id="lstAccMonth" size=1 width=40% class="font9Tahoma" runat=server />
						<asp:DropDownList id="lstAccYear" size=1 width=40% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod class="font9Tahoma" runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>																
				<tr id=TRDocDateFromTo>		
					<td>From :</td>
					<td><asp:TextBox id="txtDateFrom" maxlength="10" width=60% runat="server"/>
  								  <a href="javascript:PopCal('RptSelect_txtDateFrom');">
								  <asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>				
					<td colspan=2>To : &nbsp;
								  <asp:TextBox id="txtDateTo" maxlength="10" width=30% runat="server"/>
  								  <a href="javascript:PopCal('RptSelect_txtDateTo');">
								  <asp:Image id="btnSelDateTo" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>
				</tr>		
				<tr>
					<td width=40%>Select number of decimal(s) for reporting : </td>
					<td width=20%><asp:DropDownList id=lstDecimal width=40% class="font9Tahoma" runat=server>
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
			</table>
		</td>
	</tr>
</table>
