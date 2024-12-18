<%@ Page Language="vb" src="../../../include/PopUpWinCalendar.aspx.vb" Inherits="PopUpWinCal" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>
<html>
<head>
    <title>CALENDAR</title> 
</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />

<body leftmargin="2" topmargin="2">
    <form runat="server">
        <asp:Calendar 
			id="Calendar1" runat="server" 
			OnSelectionChanged="Calendar1_SelectionChanged" 
			OnDayRender="Calendar1_dayrender" 
			showtitle="true" 
			SelectionMode="Day" 
			BackColor="#ffffff" 
			FirstDayOfWeek="Monday" 
			BorderColor="NAVY" 
			ForeColor="#00000" 
			Height="60" Width="120">
		<TodayDayStyle BackColor="#FFFFC0"></TodayDayStyle>
		<DayStyle Font-Size="8pt" Font-Names="Arial"></DayStyle>
		<DayHeaderStyle Font-Size="10pt" Font-Underline="True" Font-Names="Arial" BorderStyle="None" BackColor="#E0E0E0"></DayHeaderStyle>
		<SelectedDayStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#0099FF"></SelectedDayStyle>
		<OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
		<TitleStyle CssClass=mb-t></TitleStyle>
        </asp:Calendar>
        <asp:Literal id="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
