<%@ Page Language="vb" src="../../../include/PR_mthend_payrollprocess_estate.aspx.vb" Inherits="PR_mthend_payrollprocess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>

<html>
	<head>
		<title>Proses Payroll</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet"
            type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id=frmProcess class="main-modul-bg-app-list-pu" runat=server onsubmit="ShowLoading()">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor=red runat=server />
			<table border="0" cellpadding="0" cellspacing="0" width="100%">

 
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
                    <tr>
					<td colspan=3 class="font9Tahoma" width="100%" >
                        PROSES PAYROLL</td>
				    </tr>
                                    <tr>
					<td colspan=3 class="font9Tahoma" width="100%" >
                        <hr style="width :100%" />   
                        </td>
				    </tr>
				<tr valign=top>
					<td height=25 width=20%>&nbsp;</td>
					<td width=50%>	&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonth" width="20%" OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged" AutoPostBack="true" runat=server>
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
									<asp:DropDownList id=ddlyear width="20%" maxlength="20" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3 style="height: 19px">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3>
					<asp:ImageButton id=btnGenerate onclick=btnGenerate_Click UseSubmitBehavior="false" AlternateText="Proses Trial Data" imageurl="../../images/butt_process.gif" CausesValidation=False runat=server />
					<asp:ImageButton id=btnPosted onclick=btnPosted_Click UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_post.gif" CausesValidation=False runat=server />
					<asp:ImageButton ID=btnRefresh  onclick=btnRefreh_Click UseSubmitBehavior="false" AlternateText="Refresh Data"  ImageUrl="../../images/butt_refresh.gif"  CausesValidation=False runat=server />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:ImageButton id=btnConfirm onclick=btnConfirm_Click UseSubmitBehavior="false" AlternateText="Confirm Data"  imageurl="../../images/butt_confirm.gif" CausesValidation=False runat=server />
					</td>
				</tr>
				    <tr>
                    <td colspan="3" rowspan="">
                        <br />
                    </td>
                </tr>
				
				<tr>
                    <td colspan="3">
       				     <div id="divnotmatch" visible="False" runat="server">
       				     <table border="0" cellspacing="1" cellpadding="1" width="100%">
			        	  <tr>
					      <td colspan=6 width=100% >
							<asp:Label id=lblnotmatch visible=true Text="[Warning], Silakan dicek !!! " ForeColor=red runat=server />
						  </td>
				           </tr>
				           <tr>
					       <td colspan=6 height="10%">			
					       <div id="div1" style="height: 200px;width:800;overflow: auto;">			
						       <asp:DataGrid id=dgValidasi
								AutoGenerateColumns="false" width="100%" runat="server"
								GridLines=none
								Cellpadding="1"
								Pagerstyle-Visible="False"
								AllowSorting="false"     class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
								<Columns>
									<asp:TemplateColumn HeaderText="Warning">
										<ItemStyle Width="20%" /> 
										<ItemTemplate>
											<asp:Label Text=<%# Container.DataItem("Error") %> runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Description">
										 <ItemStyle Width="40%" /> 
										<ItemTemplate>
											<asp:Label Text=<%# Container.DataItem("Ket") %> runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								</asp:DataGrid>
                            </div>
					        </td>
				            </tr>
			                </table>   				
       				        
					    
                        </div>
                      </td>
                </tr>
				
                <tr>
                 <td colspan="3">
					   	<igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
                        <DefaultTabStyle Height="22px">
                        </DefaultTabStyle>
                        <HoverTabStyle CssClass="ContentTabHover">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                        NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                        FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>
                        <Tabs>
						
						    <%--WB AMPRAH--%>
                            <igtab:Tab Key="AMPRAH" Text="AMPRAH KARYAWAN" Tooltip="AMPRAH KARYAWAN">
                                <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div1" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgAMPRAH" runat="server" AutoGenerateColumns="False" GridLines="Both"  CellPadding="2"  Width="250%"   CssClass="font9Tahoma">
                      
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>
							                                <asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Period") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpName") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpCode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("IDDiv") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Type Kary" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyEmp") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Job") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="HK">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("HKD")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="KG/PKK">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("KgBor")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Golongan">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("CodeGol")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Gapok">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("SubGaji")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Premi Tetap">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("PremiTetap")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Tunj">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Tunjangan")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Catu Beras">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Beras") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="Astek.Tg">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("AstekTg") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="JHT.Tg">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("JHTTg") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Premi">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Premi")  %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Lembur">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Lembur")  %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Gaji.Kotor">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
																<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("GajiKotor") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Pot.GajiKecil">
							                                    <HeaderStyle HorizontalAlign="Center"/> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("PotGajiKecil") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Pot.BPJS">
							                                    <HeaderStyle HorizontalAlign="Center"/> 
								                                <ItemStyle HorizontalAlign="Right" /> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("PotAstek") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Pot.SPSI">
							                                    <HeaderStyle HorizontalAlign="Center"/> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("PotSPSI") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Pot.Mangkir">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("PotMangkir") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Pot.Angsuran">
							                                    <HeaderStyle HorizontalAlign="Center"/> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("PotAngsuran") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Pot.Koperasi">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("PotKoperasi") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>		
						                                    <asp:TemplateColumn HeaderText="Pot.PPH21">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("PotPPH21") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>
						                                    <asp:TemplateColumn HeaderText="Pot.Denda">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Pot_1") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>
						                                    <asp:TemplateColumn HeaderText="Pot.Materai">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Pot_2") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>
						                                    <asp:TemplateColumn HeaderText="Pot.Pinjaman">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Pot_3") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>
						                                    <asp:TemplateColumn HeaderText="Pot.BPJS lainnya">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Pot_4") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>
						                                    <asp:TemplateColumn HeaderText="Pot.Potongan Bank">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Pot_5") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>
						                                    <asp:TemplateColumn HeaderText="Total.Pot">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TotPot") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>	
						                                    <asp:TemplateColumn HeaderText="Gaji Terima">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TotGaji") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>	
						                                    <asp:TemplateColumn HeaderText="Nomor Rekening">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right" BackColor=yellow/> 
							                                    <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Nrek") %>  runat="server" />
							                                    </ItemTemplate>
						                                    </asp:TemplateColumn>	
						                                </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="5">
                                            <asp:CheckBox id="cbExcelAMPRAH" text=" To Excel "  checked="True" Enabled="False" runat="server" /> 
											<asp:ImageButton id=BtnPreviewdgAMPRAH visible="False" onclick=BtnPreviewdgAMPRAH_Click UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>
											</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>   
                            
							<%--WB RAPEL--%>
							<igtab:Tab Key="RAPEL" Text="RAPEL KARYAWAN" Tooltip="RAPEL KARYAWAN">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div1" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgRAPEL" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2"  Width="250%"    CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>
							                                <asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Period") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpName") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpCode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("IDDiv") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Type Kary" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyEmp") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Job") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Tot.Rapel">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Total")%>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
						                                </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="5">
                                            <asp:CheckBox id="cbExcelRAPEL" text=" To Excel " checked="True" Enabled="False" runat="server" />
											<asp:ImageButton id=BtnPreviewdgRAPEL  visible="False" onclick=BtnPreviewdgRAPEL_Click UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>
											</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab> 
                           						   
							
							<%--WB BONUS--%>
							<igtab:Tab Key="BONUS" Text="BONUS KARYAWAN" Tooltip="BONUS KARYAWAN">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div1" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgBONUS" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>
														 <asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Period") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpName") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpCode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("IDDiv") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Type Kary" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyEmp") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Job") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Tot.Rapel">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Total")%>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
						                                </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="5">
											<asp:CheckBox id="cbExcelBONUS" text=" To Excel " checked="True" Enabled="False" runat="server" />
											<asp:ImageButton id=BtnPreviewdgBONUS  onclick=BtnPreviewdgBONUS_Click visible="False" UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>
											
											</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>  

							<%--WB THR--%>
							<igtab:Tab Key="THR" Text="THR KARYAWAN" Tooltip="THR KARYAWAN">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div1" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgTHR" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>
														 <asp:TemplateColumn HeaderText="Lokasi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("location") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Bulan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("AccMonth") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Tahun" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("AccYear") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="NIK" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpCode") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpName") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Agama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("religion") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Divname") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Jabatan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("jabatan") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Type Karyawan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyEmp") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Tunjangan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyTunj") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Golongan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("CodeGol") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Tanggal Masuk Kerja" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("tmk") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="HK" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("hk") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Natura Beras" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("berasrate") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Rate Golongan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("golrate") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Rate Upah" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("upahrate") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Gaji Pokok" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("gapok") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Beras (KG)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("beraskg") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Beras (Rp)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("berasrp") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Premi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("premi") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Tunjangan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("tunjangan") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="THR" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("thr") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Pot.PPH21" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("daging") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Pot. Bank" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("pinjaman") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															
							                                <asp:TemplateColumn HeaderText="Total">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Total")%>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
						                                </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="%">
											<asp:CheckBox id="cbExcelTHR" text=" To Excel " checked="True" Enabled="False" runat="server" />
											<asp:ImageButton id=BtnPreviewdgTHR onclick=BtnPreviewdgTHR_Click visible="False" UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>											
											</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab> 
 							
                            <%--WB SPT--%>
                            <igtab:Tab Key="SPT" Text="SPT KARYAWAN" Tooltip="SPT KARYAWAN">
                                <ContentPane>
                                     <table border="0" cellspacing="0" cellpadding="0" width="99%">
                                        <tr>
                                            <td colspan="5">
                                              <igtab:UltraWebTab ID="TABPPH21" ThreeDEffect="False" TabOrientation="TopLeft"
												SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
												<DefaultTabStyle Height="22px">
												</DefaultTabStyle>
												<HoverTabStyle CssClass="ContentTabHover">
												</HoverTabStyle>
												<RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
												NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
												FillStyle="LeftMergedWithCenter"></RoundedImage>
												<SelectedTabStyle CssClass="ContentTabSelected">
												</SelectedTabStyle>
													<Tabs>
														<igtab:Tab Key="KRYWN21" Text="SPT BULANAN KARYAWAN" Tooltip="SPT BULANAN KARYAWAN">
															<ContentPane>
																<table border="0" cellspacing="1" cellpadding="1" width="73%">
																	<tr>
																		<td colspan="5">
																			<div id="div1" style="height:500px;width:1040;overflow:auto;">	
																				<asp:DataGrid ID="dgKRYWN21" runat="server" AutoGenerateColumns="False" GridLines="Both"
																					CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
																					<Columns>
																						<asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("RowNo") %> id="lblRowNo" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("Period") %> id="lblPeriod" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpName") %> id="lblNama" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpCode") %> id="lblEmpCode" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("IDDiv") %> id="lblIDDiv" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeGrbJob") %> id="lblCodeGrbJob" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="T" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeTg") %> id="lblCodeTg" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tgl. Masuk" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetLongDate(Container.DataItem("DOJ")) %> id="lblDOJ" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("NPWP") %> id="lblNPWP" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Gapok">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Gapok"), 2), 2)  %> id="lblGapok" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Premi"), 2), 2)  %> id="lblPremi" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi/Tj.Tetap">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiTetap"), 2), 2)  %> id="lblPremiTetap" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiLain"), 2), 2)  %> id="lblPremiLain" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lembur">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Lembur"), 2), 2)  %> id="lblLembur" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Astek">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Astek"), 2), 2) %> id="lblAstek" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Catu Beras">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CatuBeras"), 2), 2) %> id="lblCatuBeras" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TLain"), 2), 2) %> id="lblTLain" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Rapel">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rapel"), 2), 2) %> id="lblRapel" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="THR/Bonus">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("THRBonus"), 2), 2) %> id="lblTHRBonus" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Pajak">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen /> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TPajak"), 2), 2) %> id="lblTPajak" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPDP"), 2), 2) %> id="lblTotPDP" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="By. Jabatan">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJbt"), 2), 2) %> id="lblPotJbt" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="JHT">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right" /> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJHT"), 2), 2) %> id="lblPotJHT" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lainnya">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Potlain"), 2), 2) %> id="lblPotLain" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPOT"), 2), 2) %> id="lblTotPOT" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Pendapatan Netto">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 2) %> id="lblDPP" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="PTKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PTKP"), 2), 2) %> id="lblPTKP" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>		
																						<asp:TemplateColumn HeaderText="PKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PKP"), 2), 2) %> id="lblPKP" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="SBLM NPWP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Non"), 2), 2) %> id="lblPPH21Non" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="NO NPWP +20%">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Selisih"), 2), 2) %> id="lblPPH21Selisih" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="TOTAL PPH21">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21"), 2), 2) %> id="lblPPH21" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>								
																					</Columns>
																				</asp:DataGrid>
																			</div>
																		</td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan="3">
																		<asp:CheckBox id="cbExceldgKRYWN21" text=" To Excel " checked="True" Enabled="False" runat="server" /></td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan=5>
																			<asp:ImageButton id="BtnPreviewdgKRYWN21" onclick=BtnPreviewdgKRYWN21_Click Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" runat="server" />
																		</td>
																	</tr>
																</table>
															</ContentPane>
														</igtab:Tab>         
														
														<%--WB PPH--%>
														<igtab:Tab Key="NONKRYWN21" Text="SPT BULANAN NON KARYAWAN" Tooltip="SPT BULANAN NON KARYAWAN">
															<ContentPane>
																 <table border="0" cellspacing="1" cellpadding="1" width="99%">
																	<tr>
																		<td colspan="5">
																			<div id="div2" style="height:500px;width:1040;overflow:auto;">	
																				<asp:DataGrid ID="dgNONKRYWN21" runat="server" AutoGenerateColumns="False" GridLines="Both"
																					CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
																					<Columns>
																						<asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("RowNo") %> id="lblRowNoPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("Period") %> id="lblPeriodPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpName") %> id="lblNamaPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpCode") %> id="lblEmpCodePPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("IDDiv") %> id="lblIDDivPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeGrbJob") %> id="lblCodeGrbJobPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="T" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeTg") %> id="lblCodeTgPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tgl. Masuk" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetLongDate(Container.DataItem("DOJ")) %> id="lblDOJPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("NPWP") %> id="lblNPWPPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Gapok">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Gapok"), 2), 2)  %> id="lblGapokPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Premi"), 2), 2)  %> id="lblPremiPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi/Tj.Tetap">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiTetap"), 2), 2)  %> id="lblPremiTetapPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiLain"), 2), 2)  %> id="lblPremiLainPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lembur">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Lembur"), 2), 2)  %> id="lblLemburPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Astek">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Astek"), 2), 2) %> id="lblAstekPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Catu Beras">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CatuBeras"), 2), 2) %> id="lblCatuBerasPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TLain"), 2), 2) %> id="lblTLainPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Rapel">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rapel"), 2), 2) %> id="lblRapelPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="THR/Bonus">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("THRBonus"), 2), 2) %> id="lblTHRBonusPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Pajak">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TPajak"), 2), 2) %> id="lblTPajakPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPDP"), 2), 2) %> id="lblTotPDPPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="By. Jabatan">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJbt"), 2), 2) %> id="lblPotJbtPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="JHT">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right" /> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJHT"), 2), 2) %> id="lblPotJHTPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lainnya">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Potlain"), 2), 2) %> id="lblPotLainPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPOT"), 2), 2) %> id="lblTotPOTPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Pendapatan Netto">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 2) %> id="lblDPPPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="PTKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PTKP"), 2), 2) %> id="lblPTKPPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>		
																						<asp:TemplateColumn HeaderText="PKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PKP"), 2), 2) %> id="lblPKPPPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="SBLM NPWP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Non"), 2), 2) %> id="lblPPH21PPHNon" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="NO NPWP +20%">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Selisih"), 2), 2) %> id="lblPPH21PPHSelisih" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="TOTAL PPH21">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21"), 2), 2) %> id="lblPPH21PPH" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>								
																					</Columns>
																				</asp:DataGrid>
																			</div>
																		</td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan="3">
																		<asp:CheckBox id="cbExceldgNONKRYWN21" text=" To Excel " checked="True" Enabled="False" runat="server" /></td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan=5>
																			<asp:ImageButton id="BtnPreviewNONKRYWN21" onclick=BtnPreviewNONKRYWN21_Click Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" runat="server" />
																		</td>
																	</tr>
																</table>
															</ContentPane>
														</igtab:Tab>
																				
														<igtab:Tab Key="KRYWN21" Text="SPT TAHUNAN KARYAWAN" Tooltip="SPT TAHUNAN KARYAWAN">
															<ContentPane>
																<table border="0" cellspacing="1" cellpadding="1" width="73%">
																	<tr>
																		<td colspan="5">
																			<div id="div3" style="height:500px;width:1040;overflow:auto;">	
																				<asp:DataGrid ID="dgKRYWNThn21" runat="server" AutoGenerateColumns="False" GridLines="Both"
																					CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
																					<Columns>
																						<asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("RowNo") %> id="lblRowNoThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("AccYear") %> id="lblPeriodThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpName") %> id="lblNamaThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpCode") %> id="lblEmpCodeThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("IDDiv") %> id="lblIDDivThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeGrbJob") %> id="lblCodeGrbJobThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="T" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeTg") %> id="lblCodeTgThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tgl. Masuk" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetLongDate(Container.DataItem("DOJ")) %> id="lblDOJThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("NPWP") %> id="lblNPWPThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Gapok">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Gapok"), 2), 2)  %> id="lblGapokThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Premi"), 2), 2)  %> id="lblPremiThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi/Tj.Tetap">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiTetap"), 2), 2)  %> id="lblPremiTetapThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiLain"), 2), 2)  %> id="lblPremiLainThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lembur">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Lembur"), 2), 2)  %> id="lblLemburThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Astek">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Astek"), 2), 2) %> id="lblAstekThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Catu Beras">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CatuBeras"), 2), 2) %> id="lblCatuBerasThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TLain"), 2), 2) %> id="lblTLainThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Rapel">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rapel"), 2), 2) %> id="lblRapelThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="THR/Bonus">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("THRBonus"), 2), 2) %> id="lblTHRBonusThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Pajak">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen /> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TPajak"), 2), 2) %> id="lblTPajakThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPDP"), 2), 2) %> id="lblTotPDPThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="By. Jabatan">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJbt"), 2), 2) %> id="lblPotJbtThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="JHT">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right" /> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJHT"), 2), 2) %> id="lblPotJHTThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lainnya">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Potlain"), 2), 2) %> id="lblPotLainThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPOT"), 2), 2) %> id="lblTotPOTThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Pendapatan Netto">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 2) %> id="lblDPPThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="PTKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PTKP"), 2), 2) %> id="lblPTKPThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>		
																						<asp:TemplateColumn HeaderText="PKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PKP"), 2), 2) %> id="lblPKPThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TERHUTANG">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Non"), 2), 2) %> id="lblPPH21NonThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="NO NPWP +20%">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Selisih"), 2), 2) %> id="lblPPH21SelisihThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="TOTAL PPH21">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21"), 2), 2) %> id="lblPPH21Thn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="PPH21 DISETOR">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHDiSetor"), 2), 2) %> id="lblPPH21DiSetorThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="PPH21 KURANG BAYAR">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHKurangBayar"), 2), 2) %> id="lblPPH21KurangBayarThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>								
																					</Columns>
																				</asp:DataGrid>
																			</div>
																		</td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan="3">
																		<asp:CheckBox id="cbExceldgKRYWNThn21" text=" To Excel " checked="True" Enabled="False" runat="server" /></td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan=5>
																			<asp:ImageButton id="btnGenerateThn21" Visible=false ToolTip="Generate journal karyawan" UseSubmitBehavior="false"   runat="server" ImageUrl="../../images/butt_generate.gif"/>
																			<asp:ImageButton id="BtnPreviewThn21" onclick=BtnPreviewThn21_Click Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif"  runat="server" />
																		</td>
																	</tr>
																</table>
															</ContentPane>
														</igtab:Tab>         
														
														<%--WB PPH--%>
														<igtab:Tab Key="NONKRYWN21" Text="SPT TAHUNAN NON KARYAWAN" Tooltip="SPT TAHUNAN NON KARYAWAN">
															<ContentPane>
																 <table border="0" cellspacing="1" cellpadding="1" width="99%">
																	<tr>
																		<td colspan="5">
																			<div id="div4" style="height:500px;width:1040;overflow:auto;">	
																				<asp:DataGrid ID="dgNONKRYWNThn21" runat="server" AutoGenerateColumns="False" GridLines="Both"
																					CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
																					<Columns>
																						<asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("RowNo") %> id="lblRowNoPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("AccYear") %> id="lblPeriodPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpName") %> id="lblNamaPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("EmpCode") %> id="lblEmpCodePPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("IDDiv") %> id="lblIDDivPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeGrbJob") %> id="lblCodeGrbJobPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="T" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("CodeTg") %> id="lblCodeTgPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tgl. Masuk" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetLongDate(Container.DataItem("DOJ")) %> id="lblDOJPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
																							<ItemStyle/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# Container.DataItem("NPWP") %> id="lblNPWPPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Gapok">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Gapok"), 2), 2)  %> id="lblGapokPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Premi"), 2), 2)  %> id="lblPremiPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi/Tj.Tetap">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiTetap"), 2), 2)  %> id="lblPremiTetapPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Premi Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PremiLain"), 2), 2)  %> id="lblPremiLainPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lembur">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Lembur"), 2), 2)  %> id="lblLemburPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Astek">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Astek"), 2), 2) %> id="lblAstekPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Catu Beras">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("CatuBeras"), 2), 2) %> id="lblCatuBerasPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Lain">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TLain"), 2), 2) %> id="lblTLainPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Rapel">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rapel"), 2), 2) %> id="lblRapelPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="THR/Bonus">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("THRBonus"), 2), 2) %> id="lblTHRBonusPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Tj. Pajak">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TPajak"), 2), 2) %> id="lblTPajakPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPDP"), 2), 2) %> id="lblTotPDPPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="By. Jabatan">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJbt"), 2), 2) %> id="lblPotJbtPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="JHT">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right" /> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PotJHT"), 2), 2) %> id="lblPotJHTPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Lainnya">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Potlain"), 2), 2) %> id="lblPotLainPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TOTAL">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotPOT"), 2), 2) %> id="lblTotPOTPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="Pendapatan Netto">
																							<HeaderStyle HorizontalAlign="Center"/> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPP"), 2), 2) %> id="lblDPPPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="PTKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PTKP"), 2), 2) %> id="lblPTKPPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>		
																						<asp:TemplateColumn HeaderText="PKP">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PKP"), 2), 2) %> id="lblPKPPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:TemplateColumn HeaderText="TERHUTANG">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Non"), 2), 2) %> id="lblPPH21PPHNonThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="NO NPWP +20%">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21Selisih"), 2), 2) %> id="lblPPH21PPHSelisihThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="TOTAL PPH21">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right" BackColor=darkseagreen/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPH21"), 2), 2) %> id="lblPPH21PPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>		
																						<asp:TemplateColumn HeaderText="PPH21 DISETOR">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHDiSetor"), 2), 2) %> id="lblPPH21DiSetorPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>	
																						<asp:TemplateColumn HeaderText="PPH21 KURANG BAYAR">
																							<HeaderStyle HorizontalAlign="Center" /> 
																							<ItemStyle HorizontalAlign="Right"/> 
																							<ItemTemplate>
																								<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHKurangBayar"), 2), 2) %> id="lblPPH21KurangBayarPPHThn" runat="server" />
																							</ItemTemplate>
																						</asp:TemplateColumn>									
																					</Columns>
																				</asp:DataGrid>
																			</div>
																		</td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan="3">
																		<asp:CheckBox id="cbExceldgNONKRYWNThn21" text=" To Excel " checked="True" Enabled="False" runat="server" /></td>
																	</tr>
																	<tr>
																		<td colspan=5>&nbsp;</td>
																	</tr>
																	<tr>
																		<td colspan=5>
																			<asp:ImageButton id="btnGenerateNONKRYWNThn21" Visible=false ToolTip="Generate journal non karyawan" UseSubmitBehavior="false"   runat="server" ImageUrl="../../images/butt_generate.gif"/>
																			<asp:ImageButton id="BtnPreviewNONKRYWNThn21" onclick=BtnPreviewNONKRYWNThn21_Click Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif"  runat="server" />
																		</td>
																	</tr>
																</table>
															</ContentPane>
														</igtab:Tab>
													</Tabs>
											</igtab:UltraWebTab>							
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                                         
							<%--WB TRANSIT--%>			 
                            <igtab:Tab Key="TRANSIT" Text="TRANSIT GAJI" Tooltip="TRANSIT GAJI">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div3" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgTransit" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2" Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>      
  														 <asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Period") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpName") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpCode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("IDDiv") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Type Kary" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyEmp") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Job") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="Ket" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Ket") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="COA" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("COA") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Value">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Value")%>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Kode Blok" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Block") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Kode Vehicle" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("vehcode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="JurnalID" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("JournalID") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>															
						                                </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="3">
											 <asp:CheckBox id="cbExcelTransit" text=" To Excel "  checked="True" Enabled="False" runat="server" /> 
											<asp:ImageButton id=BtnPreviewTransit onclick=BtnPreviewTransit_Click visible="False" UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>
											</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>         
                            
                            <%--WB ALOKASI--%>
                            <igtab:Tab Key="ALOKASI" Text="ALOKASI GAJI" Tooltip="ALOKASI GAJI">
                                <ContentPane>
                                     <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div4" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgALOKASI" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2"  Width="250%"     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>
															<asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Period") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpName") %> runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Kode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("EmpCode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("IDDiv") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Type Kary" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TyEmp") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Job" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Job") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="Ket" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Ket") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="COA" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("COA") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="Value">
							                                    <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Value")%>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Kode Blok" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Block") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Kode Vehicle" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("vehcode") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>	
															<asp:TemplateColumn HeaderText="JurnalID" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("JournalID") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>	
						                                </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="3">
                                            <asp:CheckBox id="cbExcelAlokasi" text=" To Excel "  checked="True" Enabled="False" runat="server" /> 
											<asp:ImageButton id=BtnPreviewAlokasi onclick=BtnPreviewAlokasi_Click visible="False" UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>
											
											</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                                    
							<%--WB REKON--%>		
                            <igtab:Tab Key="REKON" Text="REKONSIL" Tooltip="REKONSIL">
                                <ContentPane>
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div5" style="height:300px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid id=dgRecon
                                                        AutoGenerateColumns=false width="90%" runat=server
                                                        GridLines=none Cellpadding=2     CssClass="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
                                                        <Columns>						                                                          
																<asp:TemplateColumn HeaderText="Periode" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Period") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Divisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("IDDiv") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="AMPRAH-GAJI" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Gaji") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn HeaderText="AMPRAH-RAPEL" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Rapel") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															 <asp:TemplateColumn HeaderText="AMPRAH-BONUS" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Bonus") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="AMPRAH-THR" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("THR") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="AMPRAH-MANGKIR" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("Mangkir") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="AMPRAH-Tg.JHT" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("JHTTG") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="AMPRAH-PPH21" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("PPH21") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="AMPRAH-Total" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("TOTAMPRAH") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="SPT-Total" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("SPT") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="SPT-PPH21" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("SPTPPH21") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="RECON-TRANSIT" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("RECON") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="SELISIH-PPH21" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("SELISIHPPH21") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="SELISIH-RECON" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
							                                    <ItemStyle/> 
								                                <ItemTemplate>
								                                    <asp:Label Text=<%# Container.DataItem("SELISIHRECON") %>  runat="server" />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
									        </td>
									    </tr>
									    <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
	                                        <td colspan="3">
											<asp:CheckBox id="cbExcelRecon" text=" To Excel "  checked="True" Enabled="False" runat="server" /> 
											<asp:ImageButton id=BtnPreviewRecon onclick=BtnPreviewRecon_Click UseSubmitBehavior="false" AlternateText="Posted Data"  imageurl="../../images/butt_print_preview.gif" CausesValidation=False runat=server/>
											</td>
                                        </tr>
                                    </table>
                                    
                                    
                                </ContentPane>
                            </igtab:Tab>
                            
			            </Tabs>
                    </igtab:UltraWebTab>
                      </td>
                </tr>
				</table>
                </div>
                </td>
                </tr>
			</table>
		</form>
	</body>
    <script type="text/javascript">  
        function ShowLoading(e) {
            var div = document.createElement('div');
            var img = document.createElement('img');
            img.src = '/../en/images/load2.gif';
            div.innerHTML = "<br />";
            div.style.cssText = 'position: fixed; top: 0%; left: 0%; z-index: auto; width: 100%; height: 100%; text-align: center; background-color:rgba(255, 255, 255, 0.8);';
            div.appendChild(img);
            document.body.appendChild(div);
            return true;
            // These 2 lines cancel form submission, so only use if needed.  
            //window.event.cancelBubble = true;  
            //e.stopPropagation();  
        }
      </script>
	</html>
