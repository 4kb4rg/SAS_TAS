<%@ Page Language="vb" src="../../../include/HR_trx_MutasiPenghasilanList_Estate.aspx.vb" Inherits="HR_trx_MutasiPenghasilanList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mutasi List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmLocList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuHRSetup id=MenuHRSetup runat="server" /></td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR MUTASI/KOREKSI PENGAHASILAN KARYAWAN</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="15%" height="26" valign=bottom>
                                    NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td height="26" valign=bottom style="width: 17%">
                                    Nama&nbsp; :<BR><asp:TextBox id=txtEmpName width=100% maxlength="20" runat="server" /></td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" valign=bottom>Diupdate :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
								<asp:BoundColumn Visible=False HeaderText="Kode Mutasi" DataField="MutasiPenghasilanCode" />								
								<asp:HyperLinkColumn HeaderText="Kode Mutasi" SortExpression="MutasiPenghasilanCode" 
									DataNavigateUrlField="MutasiPenghasilanCode" 
									DataNavigateUrlFormatString="HR_trx_MutasiPenghasilanDet_Estate.aspx?MutasiPenghasilanCode={0}" 
									DataTextField="MutasiPenghasilanCode" />			
								
									
								
								<asp:TemplateColumn HeaderText="NIK" >
									<ItemTemplate>
										<asp:Label ID = ecode Text='<%#Container.DataItem("EmpCode")%>' Visible=true runat=server/>										
									</ItemTemplate>
								</asp:TemplateColumn>	
								
								<asp:TemplateColumn HeaderText="Nama" >
									<ItemTemplate>
										<asp:Label ID = ename Text='<%#Container.DataItem("EmpName")%>' Visible=true runat=server/>										
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Tanggal Pindah" >
									<ItemTemplate>
										<asp:Label ID = mdate Text='<%#objGlobal.GetLongDate(Container.DataItem("Tgl_Pindah"))%>' Visible=true runat=server/>										
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" >
									<ItemTemplate>
										<asp:Label ID =Status Text='<%#Active(Container.DataItem("Status"))%>' Visible=true runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Tgl Update" SortExpression="MutasiPenghasilanUpdateDate">
									<ItemTemplate>
										<%#objGlobal.GetLongDate(Container.DataItem("MutasiPenghasilanUpdateDate"))%>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateColumn>		
															
																							
								<asp:TemplateColumn HeaderText="Diupdate" SortExpression="UName">
									<ItemTemplate>
										<%#Container.DataItem("UName")%>
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
						<asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Block" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						<asp:Label id=SortCol Visible=False Runat="server" />			
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
