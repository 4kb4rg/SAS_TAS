<%@ Page Language="vb" src="../../../include/WS_Trx_Mechanic_Date.aspx.vb" Inherits="WS_MechanicDate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSTrx" src="../../menu/menu_wstrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Product Type List</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <Form runat="server">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		    <Table Width=100% border="0" cellspacing="4" cellpadding="1" >
   				<tr>
					<td colspan="6">
						<UserControl:MenuWSTrx id=menuWStrx runat="server" />
					</td>
				</tr>
   				<tr>
					<td class="mt-h" colspan="4">MECHANIC HOUR MAINTENANCE</td>
				</tr>
				<tr>
					<td colspan=4><hr size="1" noshade></td>
				</tr>
   				<tr>
					<td colspan="2">
						<asp:label id=lblCompany runat=server /> : 
					</td>
					<td colspan="2">
						<asp:label id=lblCompName runat=server />
					</td>
				</tr>
   				<tr>
					<td colspan="2">
						<asp:label id=lbllocation runat=server /> : 
					</td>
					<td colspan="2">
						<asp:label id=lblLocName runat=server />
					</td>
				</tr>
   				<tr>
					<td colspan="4">
						&nbsp;
					</td>
				</tr>
   				<tr>
					<td colspan="4">
						Please Click on a date for which you want to maintain the mechanic hour.
					</td>
				</tr>
				</Table>
				
				<HeaderStyle CssClass="mr-h" />
				<ItemStyle CssClass="mr-l" />
				<AlternatingItemStyle CssClass="mr-r" />
				<asp:Calendar id="Cal" runat="server" Font-Size="13pt"
					FirstDayOfWeek="Monday"
					DayNameFormat="Full"
					ShowDayHeader="True"
					ShowGridLines="True"
					ShowNextPrevMonth="True"
					ShowTitle="True"
					nextprevstyle-CssClass="mr-h"
					nextprevstyle-forecolor="White"
					nextprevstyle-font-bold="True"
					nextprevstyle-font-size="Large"
					TitleFormat="MonthYear"
					TitleStyle-CssClass="mr-h"
					TitleStyle-ForeColor="White"
					TitleStyle-Font-Size="Large"
					TitleStyle-Font-Bold="True"
					dayheaderstyle-CssClass="mr-l"
					SelectedDayStyle-Font-Bold="True"
					SelectedDayStyle-BackColor="Blue"
					SelectedDayStyle-ForeColor="Red"
					todaydaystyle-backcolor="LightBlue"
					OnSelectionChanged="Cal_SelectionChanged"
					>
				<OtherMonthDayStyle ForeColor="gray"/>
				</asp:Calendar>
			</Form>

		</body>
</html>
