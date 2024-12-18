<%@ Page Language="vb" src="../../../include/PR_setup_SalList_Estate.aspx.vb" Inherits="PR_setup_SalList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Status Karyawan List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width="60%">DAFTAR SETTING UPAH KARYAWAN</td>
					<td colspan="2" align=right width="40%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td style="background-color:#FFCC00" >
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td height="26" valign=bottom style="width: 21%">Kode Status :<BR><asp:TextBox id=txtSalCode width=100% maxlength="8" runat="server" /></td>
								<td height="26" valign=bottom style="width: 20%">Tipe Karyawan :<BR><asp:DropDownList id=txtEmpCode width=100% runat="server" /></td>
								<td height="26" valign=bottom style="width: 20%">
                                    Periode :<br />
                                    <asp:DropDownList ID="srcpmonth" runat="server" Width="50%">
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="srcpyear" runat="server" Width="40%">
                                    </asp:DropDownList></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" valign=bottom>Diupdate :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgLine runat=server
							AutoGenerateColumns=False width=100% 
							GridLines=None 
							Cellpadding=2 
							AllowPaging=True 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid  
							AllowSorting=True>								
							<HeaderStyle CssClass="mr-h" />						
							<ItemStyle CssClass="mr-l" />							
							<AlternatingItemStyle CssClass="mr-r" />
							<Columns>
								<asp:BoundColumn Visible=False HeaderText="Kode Status" DataField="SalaryCode" />								
								<asp:HyperLinkColumn HeaderText="Kode Status" SortExpression="SalaryCode" 
									DataNavigateUrlField="SalaryCode" 
									DataNavigateUrlFormatString="PR_setup_Saldet_Estate.aspx?SalaryCode={0}" 
									DataTextField="SalaryCode" />	
																
								<asp:TemplateColumn HeaderText="Tipe Karyawan" SortExpression="DescEmpty">
									<ItemTemplate>
										<%#Container.DataItem("DescEmpty")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Periode Start" >
									<ItemTemplate>
										<%#Container.DataItem("PeriodeStart")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Periode End" >
									<ItemTemplate>
										<%#Container.DataItem("PeriodeEnd")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Periode End" >
									<ItemTemplate>
										<%#Container.DataItem("PeriodeEnd")%>
									</ItemTemplate>
								</asp:TemplateColumn>
															
								<asp:TemplateColumn HeaderText="Tgl Update" SortExpression="A.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>			
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
									<ItemTemplate>
										<asp:Label ID =Status Text='<%#objHRSetup.mtdGetDeptStatus(Container.DataItem("Status"))%>' Visible=true runat=server/>
									</ItemTemplate>
								</asp:TemplateColumn>									
								<asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
										<asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
									</ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>	
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server"
							AutoPostBack="True" 
							onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" width="80%" ColSpan=6>
						<asp:ImageButton id=NewSalBtn onClick=NewSalBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Sallary Code" runat="server"/>
						<asp:ImageButton id=btnGen OnClick="DEDR_Gen" imageurl="../../images/butt_generate.gif" AlternateText="Generate Gol SKUB" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						<asp:Label id=SortCol Visible=False Runat="server" />			
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
