<%@ Control Language="vb" AutoEventWireup="false" src="../../../include/popUpCalendar.ascx.vb" Inherits="popUpCalendar" %>

	<SCRIPT LANGUAGE="JavaScript" TYPE="text/javascript">
			
		var isIE = document.all?true:false;
		if (!isIE) document.captureEvents(Event.MOUSEMOVE);

		function getMousePosition(e) {
			var _x;
			var _y;
			if (!isIE) {
				_x = e.pageX;
				_y = e.pageY;
			}
			if (isIE) {
				_x = event.clientX + document.body.scrollLeft;
				_y = event.clientY + document.body.scrollTop;
			}
			document.frmMain.x.value=_x;
			document.frmMain.y.value=_y;
			return true;
			}
			
    </SCRIPT>

<asp:panel id="pnlCalendar" style="Z-INDEX: 100; LEFT: 0px; POSITION: absolute; TOP: 0px" runat="server" Height="88px" Width="152px">

	<asp:Calendar id="Calendar1" runat="server" Height="86" Width="145" NextMonthText="<IMG src='../../images/monthright.gif' border='0'>" PrevMonthText="<IMG src='../../images/monthleft.gif' border='0'>" BackColor="White" BorderColor="Black" BorderStyle="Solid">
		<TodayDayStyle BackColor="#FFFFC0"></TodayDayStyle>
		<DayStyle Font-Size="8pt" Font-Names="Arial"></DayStyle>
		<DayHeaderStyle Font-Size="10pt" Font-Underline="True" Font-Names="Arial" BorderStyle="None" BackColor="#E0E0E0"></DayHeaderStyle>
		<SelectedDayStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="Navy"></SelectedDayStyle>
		<TitleStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="Navy"></TitleStyle>
		<OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
	</asp:Calendar>
</asp:panel>
