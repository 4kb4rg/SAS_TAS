<%@ Page Language="vb" src="../../../include/PR_Trx_AttdCheckrolllist_Estate.aspx.vb" Inherits="PR_trx_AttdList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">


hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
    margin-bottom: 0px;
}
        </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
            &nbsp;<asp:Label id=SortExpression visible=false runat=server />
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border="0" cellspacing="0" cellpadding=2 width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> DAFTAR ABSENSI KARYAWAN</strong></td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c" style="height: 56px" class="font9Tahoma">						     							
						<table width="100%" cellspacing="0" cellpadding="3" border="0" class="font9Tahoma">
							<tr style="background-color:#FFCC00">
								 <td height="26" style="width: 15%">Periode :<BR>
									<asp:DropDownList id="ddlMonth" width="50%" runat=server>
										<asp:ListItem value="1">January</asp:ListItem>
										<asp:ListItem value="2">February</asp:ListItem>
										<asp:ListItem value="3">March</asp:ListItem>
										<asp:ListItem value="4">April</asp:ListItem>
										<asp:ListItem value="5">May</asp:ListItem>
										<asp:ListItem value="6">June</asp:ListItem>
										<asp:ListItem value="7">July</asp:ListItem>
										<asp:ListItem value="8">Augustus</asp:ListItem>
										<asp:ListItem value="9">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id="ddlyear" width="47%" runat=server></asp:DropDownList>
								</td>
								<td height="26" style="width: 10%">NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
                                <td height="26" style="width: 10%">Nama :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server" /></td>
                                <td height="26" style="width: 10%">Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat="server" /></td>
								<td height="26" style="width: 15%">KeMandoran :<BR><asp:DropDownList id="ddlmandor" width=100% runat="server" /></td>
								<td height="26" style="width: 10%">Tipe :<BR><asp:DropDownList id="ddlEmpType" width=100% runat="server" /></td>
                                <td height="26" style="width: 8%">View :<BR><asp:DropDownList id="ddlview" width=100% runat="server">
                                <asp:ListItem value="0" Selected=True>Code</asp:ListItem>
								<asp:ListItem value="1">Hk</asp:ListItem>
								</asp:DropDownList>
                                </td>
                                <td height="26" width="10%"  valign=bottom align=left><asp:Button id=SearchBtn CssClass="button-small" Text="Search" OnClick=srchBtn_Click runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6 width=100%>	
					<div id="divgd" style="width:800;overflow: auto;">				
						<asp:DataGrid id=dgLine runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							OnItemDataBound=dgLineBindGrid 
						    OnItemCommand=OnCommand_Redirect 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False class="font9Tahoma">								
							<HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
							<Columns>								
						    	<%--<asp:BoundColumn Visible=False HeaderText="Employee Code" DataField="EmpCode" />
								<asp:TemplateColumn HeaderText="Employee Code" >
									<ItemTemplate>
									<asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server/>
									<%# Container.DataItem("EmpCode") %>
									</ItemTemplate>
									<ItemStyle Width=15% />
									</asp:TemplateColumn>--%>
									
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
										<asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server/>
										<asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>' visible=false runat=server/>
										<asp:Label id=lblEmpType text='<%# Container.DataItem("CodeEmpty") %>' visible=false runat=server/>
										<%# Container.DataItem("EmpName") %> &nbsp;-&nbsp;<%#Container.DataItem("EmpCode")  %>									
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Divisi" >
									<ItemTemplate>
										<asp:label id=lbldivisi visible="true" text=<%# Container.DataItem("IDDiv")%> runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText=" 1" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_1 CommandArgument=01 Text='<%# Container.DataItem("_1") %>' ForeColor=<%# GetAtt(Container.DataItem("_C1")) %>   BackColor=<%# GetWeekEnd("01") %> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 2" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate >
										<asp:LinkButton id=lbl_2 CommandArgument=02  Text='<%# Container.DataItem("_2") %>' ForeColor=<%# GetAtt(Container.DataItem("_C2")) %> BackColor=<%# GetWeekEnd("02") %> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 3" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_3 CommandArgument=03  Text='<%# Container.DataItem("_3") %>' ForeColor=<%# GetAtt(Container.DataItem("_C3")) %> BackColor=<%# GetWeekEnd("03")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 4" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_4 CommandArgument=04  Text='<%# Container.DataItem("_4") %>' ForeColor=<%# GetAtt(Container.DataItem("_C4")) %> BackColor=<%# GetWeekEnd("04")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 5" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_5 CommandArgument=05  Text='<%# Container.DataItem("_5") %>' ForeColor=<%# GetAtt(Container.DataItem("_C5")) %> BackColor=<%# GetWeekEnd("05") %> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 6" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_6 CommandArgument=06  Text='<%# Container.DataItem("_6") %>' ForeColor=<%# GetAtt(Container.DataItem("_C6")) %> BackColor=<%# GetWeekEnd("06")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 7" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_7 CommandArgument=07  Text='<%# Container.DataItem("_7") %>' ForeColor=<%# GetAtt(Container.DataItem("_C7")) %> BackColor=<%# GetWeekEnd("07")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 8" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_8 CommandArgument=08  Text='<%# Container.DataItem("_8") %>' ForeColor=<%# GetAtt(Container.DataItem("_C8")) %> BackColor=<%# GetWeekEnd("08")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" 9" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_9 CommandArgument=09  Text='<%# Container.DataItem("_9") %>' ForeColor=<%# GetAtt(Container.DataItem("_C9")) %> BackColor=<%# GetWeekEnd("09") %> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="10" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_10 CommandArgument=10  Text='<%# Container.DataItem("_10") %>' ForeColor=<%# GetAtt(Container.DataItem("_C10")) %> BackColor=<%# GetWeekEnd("10")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="11" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_11 CommandArgument=11  Text='<%# Container.DataItem("_11") %>' ForeColor=<%# GetAtt(Container.DataItem("_C11")) %> BackColor=<%# GetWeekEnd("11")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="12" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_12 CommandArgument=12  Text='<%# Container.DataItem("_12") %>' ForeColor=<%# GetAtt(Container.DataItem("_C12")) %> BackColor=<%# GetWeekEnd("12") %> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="13" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_13 CommandArgument=13  Text='<%# Container.DataItem("_13") %>' ForeColor=<%# GetAtt(Container.DataItem("_C13")) %> BackColor=<%# GetWeekEnd("13") %> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="14" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_14 CommandArgument=14  Text='<%# Container.DataItem("_14") %>' ForeColor=<%# GetAtt(Container.DataItem("_C14")) %> BackColor=<%# GetWeekEnd("14")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="15" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_15 CommandArgument=15  Text='<%# Container.DataItem("_15") %>' ForeColor=<%# GetAtt(Container.DataItem("_C15")) %> BackColor=<%# GetWeekEnd("15")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="16" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_16 CommandArgument=16  Text='<%# Container.DataItem("_16") %>' ForeColor=<%# GetAtt(Container.DataItem("_C16")) %> BackColor=<%# GetWeekEnd("16")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="17" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_17 CommandArgument=17  Text='<%# Container.DataItem("_17") %>' ForeColor=<%# GetAtt(Container.DataItem("_C17")) %> BackColor=<%# GetWeekEnd("17")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="18" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_18 CommandArgument=18  Text='<%# Container.DataItem("_18") %>' ForeColor=<%# GetAtt(Container.DataItem("_C18")) %> BackColor=<%# GetWeekEnd("18")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="19" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_19 CommandArgument=19  Text='<%# Container.DataItem("_19") %>' ForeColor=<%# GetAtt(Container.DataItem("_C19")) %> BackColor=<%# GetWeekEnd("19")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="20" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_20 CommandArgument=20 Text='<%# Container.DataItem("_20") %>' ForeColor=<%# GetAtt(Container.DataItem("_C20")) %> BackColor=<%# GetWeekEnd("20")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="21" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_21 CommandArgument=21  Text='<%# Container.DataItem("_21") %>' ForeColor=<%# GetAtt(Container.DataItem("_C21")) %> BackColor=<%# GetWeekEnd("21")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="22" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_22 CommandArgument=22  Text='<%# Container.DataItem("_22") %>' ForeColor=<%# GetAtt(Container.DataItem("_C22")) %> BackColor=<%# GetWeekEnd("22")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="23" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_23 CommandArgument=23  Text='<%# Container.DataItem("_23") %>' ForeColor=<%# GetAtt(Container.DataItem("_C23")) %> BackColor=<%# GetWeekEnd("23")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="24" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_24 CommandArgument=24  Text='<%# Container.DataItem("_24") %>' ForeColor=<%# GetAtt(Container.DataItem("_C24")) %>  BackColor=<%# GetWeekEnd("24")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="25" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_25 CommandArgument=25  Text='<%# Container.DataItem("_25") %>' ForeColor=<%# GetAtt(Container.DataItem("_C25")) %> BackColor=<%# GetWeekEnd("25")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="26" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_26 CommandArgument=26  Text='<%# Container.DataItem("_26") %>' ForeColor=<%# GetAtt(Container.DataItem("_C26")) %>  BackColor=<%# GetWeekEnd("26")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="27" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_27 CommandArgument=27  Text='<%# Container.DataItem("_27") %>' ForeColor=<%# GetAtt(Container.DataItem("_C27")) %> BackColor=<%# GetWeekEnd("27")%> Visible=<%# isDaysInMonth("27")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="28" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_28 CommandArgument=28  Text='<%# Container.DataItem("_28") %>' ForeColor=<%# GetAtt(Container.DataItem("_C28")) %> BackColor=<%# GetWeekEnd("28")%> Visible=<%# isDaysInMonth("28")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="29" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_29 CommandArgument=29  Text='<%# Container.DataItem("_29") %>' ForeColor=<%# GetAtt(Container.DataItem("_C29")) %>  BackColor=<%# GetWeekEnd("29")%> Visible=<%# isDaysInMonth("29")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="30" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_30 CommandArgument=30  Text='<%# Container.DataItem("_30") %>' ForeColor=<%# GetAtt(Container.DataItem("_C30")) %> BackColor=<%# GetWeekEnd("30")%> Visible=<%# isDaysInMonth("30")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="31" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbl_31 CommandArgument=31  Text='<%# Container.DataItem("_31") %>' ForeColor=<%# GetAtt(Container.DataItem("_C31")) %>  BackColor=<%# GetWeekEnd("31") %> Visible=<%# isDaysInMonth("31")%> runat=server />
									</ItemTemplate>
								<ItemStyle BorderWidth=1 BorderStyle=Solid />
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</div>
					</td>
				</tr>
				<tr>
				<td align="left" width=50% style="height: 26px">
                        <%-- <asp:ImageButton id=AttdBtn OnClick="AttdBtn_Click" imageurl="../../images/butt_daily_attendance.gif" AlternateText="Daily Attendance" runat="server" ImageAlign="Left"/>--%>
                        <asp:ImageButton id=RefrehBtn OnClick="RefrehBtn_Click" imageurl="../../images/butt_refresh.gif" AlternateText="Refresh" runat="server" ImageAlign="Left"/>
                        <!--&nbsp;<asp:ImageButton id=GenerateBtn OnClick="GenerateBtn_Click" imageurl="../../images/butt_generate.gif" AlternateText="Generate Attendance" runat="server" ImageAlign="Left"/>-->
						&nbsp;<asp:Button id=ScanBtn Text="Data Finger Scan" CssClass="button-small" OnClick=ScanBtn_Click runat="server" />
						&nbsp;<asp:Button id=ExpBtn Text="Data Exception" CssClass="button-small" OnClick=ExpBtn_Click runat="server" />
                        <!--<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print runat="server"/>-->
                        </td>
					<td align=right width=50% style="height: 26px">
                        &nbsp;<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
			         	<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
			         	<asp:Label id=lblPageCount visible=false text=1 runat=server/>	
			         	<asp:label id=lblTotalDept visible=false text=0 runat=server />			         	
					</td>
				</tr>
                </table>
                </div>
                </td>
                </tr>
			</table>
		</FORM>
	</body>
</html>
