<%@ Page Language="vb" src="../../../include/PR_trx_BKMEmp_Estate.aspx.vb" Inherits="PR_trx_BKMEmp_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>KeMandoran Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">

              <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
            &nbsp;
            &nbsp;&nbsp;
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5" style="height: 23px">
                       <strong>ASIL PEKERJAAN KARYAWAN </strong></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   
                            </td>
				</tr>
						
                <tr>
					<td width="15%" height="25px">
                        Periode</td>
					<td style="width: 425px; height: 27px">
					<asp:DropDownList id="ddlMonth" width="25%" OnSelectedIndexChanged="ddlEmpDiv_OnSelectedIndexChanged" AutoPostBack=true CssClass="font9Tahoma" runat=server>
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
									<asp:DropDownList id="ddlyear" width="15%" runat=server></asp:DropDownList>
					</td>
					<td style="height: 27px">&nbsp;</td>
					<td style="height: 27px"></td>
					<td style="height: 27px"></td>
				</tr>
				
				<tr>
					<td width="15%" height="25px">
                        Divisi</td>
					<td style="width: 425px; height: 27px">
					<GG:AutoCompleteDropDownList id=ddlEmpDiv width="100%" CssClass="font9Tahoma"  runat=server OnSelectedIndexChanged="ddlEmpDiv_OnSelectedIndexChanged" AutoPostBack=true/></td>
					<td style="height: 27px">&nbsp;</td>
					<td style="height: 27px"></td>
					<td style="height: 27px; width: 267px;"></td>							
				</tr>				
				
				<tr>
					<td width="15%" height="25px">
                        Nama </td>
					<td style="width: 425px; height: 27px">
						<GG:AutoCompleteDropDownList id=ddlEmpCode width="100%" runat=server AutoPostBack=true/></td>
					<td style="height: 27px">&nbsp;</td>
					<td style="height: 27px"td>
					<td style="height: 27px"></td>								
				</tr>	
				
				

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SearchBtn AlternateText="  Search  " imageurl="../../images/butt_search.gif" onclick=SearchBtn_onClick  runat=server />
                        &nbsp;
			                                					
			            <asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." ForeColor=red runat="server" />&nbsp;
                        &nbsp;
                    </td>
				</tr>
				
				<tr>
					<td colspan=5>
					<table border="0" cellspacing="0" cellpadding="0" width="98%" class="font9Tahoma" >
					<tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;
                                width: 100%;">
                                Finger Print
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            <asp:DataGrid id=dgfinger runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=False 
							Allowcustompaging=False 
						    Pagerstyle-Visible=False class="font9Tahoma">
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>										
							<Columns>								
														
								 <asp:TemplateColumn HeaderText="Tanggal" >
                                    <ItemTemplate>
                                        <%# objGlobal.GetLongDate(Container.DataItem("DateIn")) %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="in1" >
                                    <ItemTemplate>
                                        <asp:label ID="dgin" Text= '<%# format(Container.DataItem("JamMasuk"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="out1" >
                                    <ItemTemplate>
                                        <asp:label ID="dgout2"  Text= '<%# format(Container.DataItem("JamIstrahat_Out"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="in2" >
                                    <ItemTemplate>
                                       <asp:label ID="dgin2" Text= '<%# format(Container.DataItem("JamIstrahat_In"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="out2" >
                                    <ItemTemplate>
                                     <asp:label ID="dgout" Text= '<%# format(Container.DataItem("JamPulang"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>

						
						        <asp:TemplateColumn HeaderText="in3" >
                                    <ItemTemplate>
                                      <asp:label ID="dgin3" Text= '<%# format(Container.DataItem("JamPulang1"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="out3" >
                                    <ItemTemplate>
                                     <asp:label ID="dgout3" Text= '<%# format(Container.DataItem("JamLembur_In"),"HH:mm")%>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                
							</Columns>
						</asp:DataGrid>
                            </td>
                        </tr>
						
						<tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;
                                width: 100%;">
                                Exception Absensi
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            <asp:DataGrid id=dgExp runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=False 
							Allowcustompaging=False 
						    Pagerstyle-Visible=False class="font9Tahoma">
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>							
							<Columns>								
														
								 <asp:TemplateColumn HeaderText="Tanggal" >
                                    <ItemTemplate>
                                        <%# objGlobal.GetLongDate(Container.DataItem("AttDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="in" >
                                    <ItemTemplate>
                                        <asp:label ID="dgin" Text= '<%# format(Container.DataItem("JamIN"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="out" >
                                    <ItemTemplate>
                                        <asp:label ID="dgout2"  Text= '<%# format(Container.DataItem("JamOUT"),"HH:mm") %>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Ket" >
                                    <ItemTemplate>
                                       <%#Container.DataItem("Ket")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="PEA" >
                                    <ItemTemplate>
                                     <%#Container.DataItem("PEA")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
							</Columns>
						</asp:DataGrid>
                            </td>
                        </tr>
						
                        <tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;
                                width: 100%;">
                                Koreksi Absensi
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            <asp:DataGrid id=dgkoreksi runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=False 
							Allowcustompaging=False 
							OnItemDataBound=dgkoreksi_BindGrid 
							OnDeleteCommand=dgkoreksi_Delete 
						    Pagerstyle-Visible=False class="font9Tahoma">
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>								
							<Columns>								
						
							<%--
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
										<%# Container.DataItem("EmpName") %>									
									</ItemTemplate>
								</asp:TemplateColumn>
								
							    <asp:TemplateColumn HeaderText="Divisi" >
									<ItemTemplate>
										<asp:label id=lbldivisi visible="false" text=<%# Container.DataItem("IDDiv")%> runat="server" />
										<asp:label id=lblempcode visible="false" text=<%# Container.DataItem("EmpCode")%> runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>--%>
								
								<asp:TemplateColumn HeaderText="Att.ID" >
                                    <ItemTemplate>
                                        <asp:Label ID="dgkoreksi_Attid" runat="server" Text='<%#Container.DataItem("AttID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								
								 <asp:TemplateColumn HeaderText="Tanggal" >
                                    <ItemTemplate>
                                        <asp:Label ID="dgkoreksi_attdate" runat="server" Text='<%#objGlobal.GetLongDate(Container.DataItem("AttDate"))%>'></asp:Label>
										<asp:label id=lblempcode visible="false" text=<%# Container.DataItem("EmpCode")%> runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="HK Awal" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("hk_awal")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="HK Koreksi" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("hk_koreksi")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Ket" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("Ket")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Tgl Update" >
                                    <ItemTemplate>
                                     <asp:Label ID="dgkoreksi_udate" runat="server" Text='<%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

						
						        <asp:TemplateColumn HeaderText="Diupdate" >
                                    <ItemTemplate>
                                     <asp:Label ID="dgkoreksi_uid" runat="server" Text='<%#Container.DataItem("uname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id=dgkoreksi_lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
                                 </asp:TemplateColumn>	
                                
							</Columns>
						</asp:DataGrid>
                            </td>
                        </tr>
						
                        <tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;
                                width: 100%;">
                                Absensi
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            <asp:DataGrid id=dgabsen runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							OnItemDataBound=dgabsen_BindGrid 
						    OnItemCommand=dgabsen_OnCommand
							Pagerstyle-Visible=False class="font9Tahoma">
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>								
							<Columns>								
						
							<%--		<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
										<%# Container.DataItem("EmpName") %>									
									</ItemTemplate>
								</asp:TemplateColumn>
								
							<asp:TemplateColumn HeaderText="Divisi" >
									<ItemTemplate>
										<asp:label id=lbldivisi visible="false" text=<%# Container.DataItem("IDDiv")%> runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>--%>
								
								<asp:TemplateColumn HeaderText=" 1" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
									    <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=false runat=server/>
										<asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>' visible=false runat=server/>
										<asp:Label id=lblEmpType text='<%# Container.DataItem("CodeEmpty") %>' visible=false runat=server/>
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
                            </td>
                        </tr>
                        
						
                        
                        <tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;
                                width: 100%;">
                                Pekerjaan
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="5">
                             <asp:DataGrid ID="dgbkm" runat="server" 
                              AllowSorting="True" 
                             AutoGenerateColumns="False"
                             CellPadding="2" 
                             GridLines="None" 
                             PagerStyle-Visible="False" 
                             Width="100%" 
                             OnItemDataBound="dgbkm_Bind" 
                             OnItemCommand=dgbkm_OnCommand class="font9Tahoma">
                            <PagerStyle Visible="False" />
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>		
                            <Columns>
                               <%-- <asp:TemplateColumn HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbID" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                
                                <asp:TemplateColumn HeaderText="Tanggal" SortExpression="BKMDate">
                                    <ItemTemplate>
                                        <asp:Label ID="bkmdate" runat="server" Text='<%#objGlobal.GetLongDate(Container.DataItem("bkmdate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="No.BKM" SortExpression="BKMCode">
                                    <ItemTemplate>
                                    <asp:LinkButton id=lnkbkmcode CommandArgument='<%# Container.DataItem("bkmcode") %>'  Text='<%# Container.DataItem("bkmcode") %>'  runat=server />
                                    <asp:Label ID="lblbkmcode" Visible=false runat="server" Text='<%#Container.DataItem("bkmcode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                             
                             <asp:TemplateColumn HeaderText="Divisi" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("divisi")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Kategori" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("cat")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Blok" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("codeblok")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                             
                                                                                        
                                <asp:TemplateColumn HeaderText="Type" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("ty")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="HK" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("hk")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Hasil" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("hasil")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Premi Lain" >
                                    <ItemTemplate>
									<asp:Label ID="lblpremi"  runat="server" Text='<%#Container.DataItem("Rp")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="UOM" >
                                    <ItemTemplate>
                                        <%#Container.DataItem("uom")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                                                                        
                             </Columns>
                        </asp:DataGrid>
                            </td>
                        </tr>
                        
                        
                    </table>
                   </td>
				</tr>
			</table>
            &nbsp;
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
