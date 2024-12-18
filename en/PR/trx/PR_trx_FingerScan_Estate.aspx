<%@ Page Language="vb" src="../../../include/PR_trx_FingerScan_Estate.aspx.vb" Inherits="PR_trx_FingerScan_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FingerScan List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
            &nbsp;<asp:Label id=SortExpression visible=false runat=server />
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 400px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR ABSENSI FINGER SCAN KARYAWAN</strong><hr style="width :100%" />   
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
								<td height="26" style="width: 35%">Tanggal :<BR>
									<asp:TextBox ID="txtDateStart" runat="server" MaxLength="10" Width="30%"></asp:TextBox>
                                    <a href="javascript:PopCal('txtDateStart');">
                                    <asp:Image ID="btnDStart" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
									s/d 
									<asp:TextBox ID="txtDateEnd" runat="server" MaxLength="10" Width="30%"></asp:TextBox>
                                    <a href="javascript:PopCal('txtDateEnd');">
                                    <asp:Image ID="btnDEnd" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
								</td>
								<td height="26" style="width: 10%">NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
                                <td height="26" style="width: 10%">Nama :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server" /></td>
								<td height="26" style="width: 10%">Type :<BR><asp:DropDownList id="ddlEmpType" width=100% runat="server" /></td>
                                <td height="26" style="width: 10%">Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat="server" /></td>
                                <td height="26" width="10%"  valign=bottom align=left><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							</td>
						</tr>
					<tr>
				           <td colspan=7 >	
				           <table width="100%" cellspacing="0" cellpadding="2" border="0" class="font9Tahoma" >
				           <tr class="mr-h" >
						        <td style="width: 10%">NIK</td>
						        <td style="width: 23%">Nama</td>
						        <td style="width: 8%">Divisi</td>
						        <td style="width: 12%">Tanggal</td>
						        <td style="width: 8%">IN  1</td>
						        <td style="width: 8%">OUT 1</td>
						        <td style="width: 8%">IN  2</td>
						        <td style="width: 8%">OUT 2</td>
						        <td style="width: 8%">IN  3</td>
						        <td style="width: 10%">OUT 3</td>
				           </tr>
				           </table>
				           </td>
				        </tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
 			
						                <asp:DataGrid id=dgLine runat=server
							                AutoGenerateColumns=false width=100% 
							                GridLines=none 
							                Cellpadding=2 
							                AllowPaging=False 
							                Allowcustompaging=False 
							                OnItemDataBound="on_BindGrid" 
							                ShowHeader="False"
							                Pagerstyle-Visible=False
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>							
							                <Columns>				                          
                                            <asp:TemplateColumn ItemStyle-Width=10%>
                                                <ItemTemplate>
                                                    <%#Container.DataItem("EmpCode")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                 <asp:TemplateColumn ItemStyle-Width=23%>
                                                <ItemTemplate>
                                                    <%#Container.DataItem("EmpName")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                 <asp:TemplateColumn ItemStyle-Width=8%>
                                                <ItemTemplate>
                                                    <%#Container.DataItem("idDiv")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                            
							                <asp:TemplateColumn ItemStyle-Width=12%>
                                                <ItemTemplate>
                                                    <%# objGlobal.GetLongDate(Container.DataItem("DateIn")) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                <asp:TemplateColumn ItemStyle-Width=8%>
                                                <ItemTemplate>
                                                    <asp:label ID="dgin" Text= '<%# format(Container.DataItem("JamMasuk"),"HH:mm") %>' runat="server" /> 
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                <asp:TemplateColumn ItemStyle-Width=8%>
                                                <ItemTemplate>
                                                    <asp:label ID="dgout2"  Text= '<%# format(Container.DataItem("JamIstrahat_Out"),"HH:mm") %>' runat="server" /> 
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                <asp:TemplateColumn ItemStyle-Width=8%>
                                                <ItemTemplate>
                                                    <asp:label ID="dgin2" Text= '<%# format(Container.DataItem("JamIstrahat_In"),"HH:mm") %>' runat="server" /> 
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                            
							                <asp:TemplateColumn ItemStyle-Width=8%>
                                                <ItemTemplate>
                                                    <asp:label ID="dgout" Text= '<%# format(Container.DataItem("JamPulang"),"HH:mm") %>' runat="server" /> 
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                <asp:TemplateColumn ItemStyle-Width=8%>
                                                <ItemTemplate>
                                                    <asp:label ID="dgin3" Text= '<%# format(Container.DataItem("JamPulang1"),"HH:mm") %>' runat="server" /> 
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
							                <asp:TemplateColumn ItemStyle-Width=10%>
                                                <ItemTemplate>
                                                    <asp:label ID="dgout3" Text= '<%# format(Container.DataItem("JamLembur_In"),"HH:mm")%>' runat="server" /> 
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
							
                            							
							                </Columns>
						                </asp:DataGrid>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>
							
							</td>
						</tr>
						<tr>
							<td>
					           <asp:ImageButton id=RefrehBtn  imageurl="../../images/butt_refresh.gif" AlternateText="Refresh" onClick="RefrehBtn_Click" runat="server"/>
                               <asp:ImageButton id=GenerateBtn imageurl="../../images/butt_generate.gif" AlternateText="Generate Attendance" onClick="GenerateBtn_Click" runat="server"/>
						       <asp:ImageButton id=ExportBtn onClick="ExportBtn_OnClick" runat="server" imageurl="../../images/butt_export_excel.gif" />
							</td>
						</tr>
                        <tr>
                            <td>
 					            <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
			         	        <asp:Label id=lblPageCount visible=false text=1 runat=server/>	
			         	        <asp:label id=lblTotalDept visible=false text=0 runat=server />	
						        <asp:HiddenField ID=ref runat=server />                      
                            </td>
                        </tr>




					</table>
				</div>
				</td>
		        <table cellpadding="0" cellspacing="0" style="width: 20px">
			        <tr>
				        <td>&nbsp;</td>
			        </tr>
		        </table>
				</td>
			</tr>
		</table>



		</FORM>
	</body>
</html>
