<%@ Page Language="vb" Src="../../../include/PR_trx_RKBDet.aspx.vb" Inherits="PR_trx_RKBDet" %>
<%@ Register TagPrefix="UserControl" TagName="MenuPRTrx" Src="../../menu/menu_prtrx.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>RENCANA KERJA BULANAN DETAIL</title>
	<Preference:PrefHdl ID="PrefHdl" runat="server" />
	<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style> 
</head>
<body>
   
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
    <table border=0 cellpadding="0" cellspacing="0" style="width: 96%" class="font9Tahoma">
			<tr>
            <td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist"> 
				<table border="0" cellspacing="1"  width="99%" id="TABLE1" class="font9Tahoma">
					<tr>
					<td class="mt-h" colspan="5" style="height: 21px">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                   <strong>DETAIL RENCANA KERJA BULANAN </strong> </td>
                                <td class="font9Header" style="text-align: right">
                        Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl Buat :
                        <asp:Label id=lblDateCreated runat=server />&nbsp;|
                        Tgl Update :<asp:Label id=lblLastUpdate runat=server />&nbsp;|
                        Diupdate :<asp:Label id=lblupdatedby runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
					</tr>
					<tr>
							<td colspan=5>&nbsp;</td>
					</tr>
					<tr>
							<td colspan=5 align="center"></td>
					</tr>
					<tr>
						<td height="25" style="width: 100px">
							No.RKB  
						</td>
						<td style="width: 200px">
							<asp:Label ID="LblIDM" runat="server" Width="100%"></asp:Label></td>
					   <td style="height: 28px">
						</td>
							
						<td style="height: 28px">
						
						</td>
						<td style="width: 350px; height: 28px;">
						</td>
					</tr>
					<tr>
						<td height="25">
							Periode : *</td>
						<td style="width: 200px">
						<asp:Label id=lblPeriod Visible="false" runat=server />
						<asp:DropDownList ID="ddlEmpMonth" runat="server" Width="60%">
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
						</asp:DropDownList><asp:DropDownList id="ddlyear" width="40%" runat=server></asp:DropDownList>
						</td>
						<td style="height: 28px">
						</td>
							
						<td colspan="2" rowspan="3">
						
						    <table border=0 class="font9Tahoma" align="left" style="background-color:#FFFFFF" >
                                <tr style="background-color:#FFCC00">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        SKUB-B</td>
                                    <td>
                                        SKU-H</td>
                                    <td>
                                        PHL</td>
                                    <td>
                                        Kontr</td>
                                    <td>
                                        Rekanan</td>
									<td>
                                        Lain-Lain</td>
                                    <td>
                                        Total</td>
                                </tr>
                                <tr/>			
                                    <td>
                                        a.</td>
                                    <td>
                                        Hk Efektif</td>
                                    <td>
							<asp:Label ID="lb_a_skub" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_a_skuh" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_a_phl" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_a_ktk" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_a_rkn" runat="server" Width="100%"></asp:Label></td>
									<td>
							<asp:Label ID="lb_a_lln" runat="server" Width="100%"></asp:Label></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        b.</td>
                                    <td>
                                        Tenaga kerja per hari</td>
                                    <td>
							<asp:Label ID="lb_b_skub" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_b_skuh" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_b_phl" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_b_ktk" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_b_rkn" runat="server" Width="100%"></asp:Label></td>
									<td>
							<asp:Label ID="lb_b_lln" runat="server" Width="100%"></asp:Label></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        c.</td>
                                    <td>
                                        Tenaga kerja tersedia</td>
                                    <td>
							<asp:TextBox ID="lb_c_skub" runat="server" BackColor="#CCCCCC" ForeColor="Black" width=50px ></asp:TextBox></td>
                                    <td>
							<asp:TextBox ID="lb_c_skuh" runat="server" BackColor="#CCCCCC" ForeColor="Black" width=50px></asp:TextBox></td>
                                    <td>
							<asp:TextBox ID="lb_c_phl" runat="server" BackColor="#CCCCCC" ForeColor="Black" width=50px></asp:TextBox></td>
                                    <td>
							<asp:Label ID="lb_c_ktk" runat="server" width=50px></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_c_rkn" runat="server" width=50px></asp:Label></td>
									<td>
							<asp:Label ID="lb_c_lln" runat="server" width=50px></asp:Label></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        d.</td>
                                    <td>
                                        Selisih tenaga kerja</td>
                                    <td>
							<asp:Label ID="lb_d_skub" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_d_skuh" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_d_phl" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_d_ktk" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_d_rkn" runat="server" Width="100%"></asp:Label></td>
									<td>
							<asp:Label ID="lb_d_lln" runat="server" Width="100%"></asp:Label></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        e.</td>
                                    <td>
                                        Total (Rp)</td>
                                    <td>
							<asp:Label ID="lb_e_skub" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_e_skuh" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_e_phl" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_e_ktk" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_e_rkn" runat="server" Width="100%"></asp:Label></td>
									<td>
							<asp:Label ID="lb_e_lln" runat="server" Width="100%"></asp:Label></td>
                                    <td>
							<asp:Label ID="lb_e_tot" runat="server" Width="100%"></asp:Label></td>
                                </tr>
                            </table>
						
						</td>
					</tr>
					<tr>
						<td height="25">
							Divisi:*</td>
						 <td style="width: 200px">
							<asp:DropDownList ID="ddldivisicode" runat="server" Width="100%"/>
							<asp:Label ID="lbldivisicode" Visible="false" runat="server" Width="100%" /></td>
						<td style="height: 28px">
						</td>
							
					</tr>
					<tr>
						<td height="25">
							Keterangan :</td>
						 <td style="width: 200px">
							<asp:TextBox ID="txtnotes" runat="server" MaxLength="128" Width="100%"></asp:TextBox></td>
						<td style="height: 28px">
						</td>
							
					</tr>
              
           
					<tr>
						<td colspan="5"><asp:Label ID="lblErrMessage" Visible="false" Text="" ForeColor="red" runat="server" /></td>
					</tr>
            
			       
					<tr>
						<td colspan="5" style="height: 26px">
						<asp:Button id=Newbtn CssClass="button-small" Text="New"  OnClick="BtnNewBK_OnClick"  runat="server"/>
						<asp:Button id=SaveBtn CssClass="button-small" Text="Save" OnClick="BtnSaveBK_OnClick" runat="server"/>
                        <asp:Button id=BackBtn CssClass="button-small" Text="Back" OnClick="BtnBackBK_onClick" runat="server"/>
						<asp:Button id=ConfmBtn CssClass="button-small" Text="Confirm" OnClick="BtnConfm_onClick" runat="server"/>
						<asp:Button id=ReActBtn CssClass="button-small" Text="Re-Active" OnClick="BtnReAct_onClick" runat="server"/>
					    <asp:Button id=DelBtn CssClass="button-small" Text="Delete" OnClick="BtnDelete_onClick" runat="server"/>
						
						<asp:Button id=PrintBtn CssClass="button-small" Text="Print" OnClick="BtnPrint_onClick" runat="server"/>
						<asp:Button id=RefreshBtn CssClass="button-small" Text="Refresh" OnClick="BtnRefreshBK_onClick" runat="server"/>
						<asp:Button id=CopyBtn CssClass="button-small" Text="Copy Dari RKB :"  OnClick=Copybtn_Click runat="server" />&nbsp;
                        <asp:DropDownList ID="ddlrkbbefore" class="font9Tahoma"  runat="server" Width="100px"/>
						</td>
					</tr>
				
					<tr>
						<td colspan="5" class="font9Tahoma" style="background-color:#FFCC00">
							<strong>Gaji dan Upah</strong> &nbsp;<asp:LinkButton id="shGU" OnClick="shGU_Click"  Text="Show/Hide" runat="server"/>
						</td>					
					</tr>
                        
					<tr id="TRGaji" runat=server>
						<td colspan="5">
									<div id="div4" style="height: 300px;width:1000px;overflow: auto;">	
											<asp:DataGrid ID="dgjob" runat="server" 
												 AllowSorting="True" 
												 AutoGenerateColumns="False"
												 CellPadding="2" 
												 GridLines="None" 
												 PagerStyle-Visible="False" 
												 Width="200%" 
												 ShowFooter=True 
												 OnItemDataBound="dgjob_BindGrid" 
                                                 OnDeleteCommand="dgjob_Delete" 
									 			 >
												<PagerStyle Visible="False" />
												<AlternatingItemStyle CssClass="mr-r" />
												<ItemStyle CssClass="mr-l" />
												<HeaderStyle CssClass="mr-h" />
												<Columns>
												 <asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
														 <asp:LinkButton id="Approve" CommandName="Approve" Text="Approve" visible=False runat="server"/>
														 </ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="No">
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>
										
										            <asp:TemplateColumn HeaderText="AccCode">
														<ItemTemplate>
														<asp:Label id=dgjobacc Text='<%# Container.DataItem("AccCode") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
													
										            <asp:TemplateColumn HeaderText="Kategori">
														<ItemTemplate>
														<asp:Label id=dgjobcat Text='<%# Container.DataItem("SubCatID") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Pekerjaan">
														<ItemTemplate>
														<asp:Label id=dgjoblnk Text='<%# Container.DataItem("job") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
												 
													<asp:TemplateColumn HeaderText="Blok" >
														<ItemTemplate>
														<asp:Label ID="dgjobblk" runat="server" Text='<%#Container.DataItem("BlockCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Keterangan" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobnote" runat="server" Text='<%#Container.DataItem("Ket")%>'></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Prd.Bln lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobprdbl" runat="server" Text='<%#Container.DataItem("PeriodeBl")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Real.Bln lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobhasilbl" runat="server" Text='<%#Container.DataItem("HasilBl") %>'></asp:Label>
														 
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Vol.Kerja">
														<ItemTemplate>
														<asp:TextBox ID="dgjobvol" runat="server" Text='<%#Container.DataItem("hasil")%>' width=50px></asp:TextBox>
														</ItemTemplate>
														
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="UOM" >
														<ItemTemplate>
														<asp:Label ID="dgjobuom" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="Hk/Unit" >
														<ItemTemplate>
														<asp:TextBox ID="dghkunit" runat="server" Text='<%#Container.DataItem("hkunit")%>' width=50px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn>
												   
												   <asp:TemplateColumn HeaderText="HK.SKUB" >
														<ItemTemplate>
														<asp:Label ID="dghkskub" runat="server" Text='<%#Container.DataItem("hkskub")%>'></asp:Label>
														</ItemTemplate>
												   
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
                                                   </asp:TemplateColumn>
												   
													<asp:TemplateColumn HeaderText="HK.SKUH" >
														<ItemTemplate>
														<asp:Label ID="dghkskuh" runat="server" Text='<%#Container.DataItem("hkskuh")%>'></asp:Label>
														</ItemTemplate>
												   
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
                                                    </asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="HK.PHL" >
														<ItemTemplate>
														<asp:Label ID="dghkphl" runat="server" Text='<%#Container.DataItem("hkphl")%>'></asp:Label>
														</ItemTemplate>
												   
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
													</asp:TemplateColumn>
												   
												  
												  
												   <asp:TemplateColumn HeaderText="Rp/Unit">
														<ItemTemplate>
														<asp:TextBox ID="dgrpunit" runat="server" Text='<%#Container.DataItem("RpUnit")%>' width=70px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn> 
												   
												   <asp:TemplateColumn HeaderText="Rp.Gaji.SKUB">
														<ItemTemplate>
														<asp:Label ID="dgrpgskub" runat="server" Text='<%#FormatNumber(Container.DataItem("RpGajiSKUB"))%>'></asp:Label>
														</ItemTemplate>
												   
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
													</asp:TemplateColumn>
												  
												   <asp:TemplateColumn HeaderText="Rp.Gaji.SKUH">
														<ItemTemplate>
														<asp:Label ID="dgrpgskuh" runat="server" Text='<%#FormatNumber(Container.DataItem("RpGajiSKUH"))%>'></asp:Label>
														</ItemTemplate>
												   
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
													</asp:TemplateColumn>
												  
												   <asp:TemplateColumn HeaderText="Rp.Gaji.PHL">
														<ItemTemplate>
														<asp:Label ID="dgrpgphl" runat="server" Text='<%#FormatNumber(Container.DataItem("RpGajiPHL"))%>'></asp:Label>
														</ItemTemplate>
												  
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
													</asp:TemplateColumn>
												   
												  
												  
													<asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:HiddenField ID="hidemptype" Value=<%# Container.DataItem("EmpType") %> runat=server />
														 <asp:HiddenField ID="hidrkbln" Value=<%# Container.DataItem("RKBCodeLn") %> runat=server />
														 <asp:HiddenField ID="hidrkbty" Value=<%# Container.DataItem("RKBType") %> runat=server />
														 <asp:HiddenField ID="hidcat" Value=<%# Container.DataItem("CatID") %> runat=server />
														 <asp:HiddenField ID="hidscat" Value=<%# Container.DataItem("SubCatID") %> runat=server />
														 <asp:HiddenField ID="hidket" Value=<%# Container.DataItem("Ket") %> runat=server />
														 <asp:HiddenField ID="hidpoid" Value=<%# Container.DataItem("Poid") %> runat=server />
														 <asp:HiddenField ID="hidhkkontr" Value=<%# Container.DataItem("HKKontr") %> runat=server />
														 <asp:HiddenField ID="hidRpRekanan" Value=<%# Container.DataItem("RpRekanan") %> runat=server />
														 <asp:HiddenField ID="hidRpLain" Value=<%# Container.DataItem("RpLain") %> runat=server />
														 </ItemTemplate>
													</asp:TemplateColumn>
																											
												 </Columns>
											</asp:DataGrid>
									</div>
						</td>
					</tr>
					<tr id="TRGaji2" runat=server>
						<td colspan="5" style="background-color:#FFFFFF" >
							<table border="0" class="font9Tahoma" cellspacing="1" cellpadding="1" width="100%" >
												   <tr>
												   <td>
												   Kategori:<asp:DropDownList ID="ddljobkat"  OnSelectedIndexChanged="ddljobkat_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Sub.Kategori:<asp:DropDownList ID="ddljobskat"  OnSelectedIndexChanged="ddljobskat_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Pekerjaan:<GG:AutoCompleteDropDownList  ID="ddljob" OnSelectedIndexChanged="ddljob_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="300px"/>&nbsp;
												   Blok:<GG:AutoCompleteDropDownList ID="ddljobblk" runat="server" class="font9Tahoma" Width="100px"/>
												   Ty.Kary:<asp:DropDownList ID="ddlEmpType" runat="server" class="font9Tahoma" Width="100px">
															<asp:ListItem Value="B">SKUB</asp:ListItem>
															<asp:ListItem Value="H">SKUH</asp:ListItem>
															<asp:ListItem Value="L" selected >PHL</asp:ListItem>
															</asp:DropDownList>
												   <br>
												   <asp:Button id=addjob CssClass="button-small" Text="Add Gaji"  OnClick="BtnAdduph_OnClick"  runat="server"/>
												   <asp:Button id=savejob CssClass="button-small" Text="Save Gaji"  OnClick="BtnSaveuph_OnClick" runat="server"/>
                                                   <asp:Button id=deljob CssClass="button-small" Text="Del All Gaji" OnClick="BtnDeluph_OnClick" runat="server"/>
                                          
												   </td>
												   </tr>
						   </table>
						</td>					
					</tr>
					<tr>
						<td colspan="5" class="font9Tahoma" style="background-color:#FFCC00">
							<strong>Prestasi/Lembur/Premi</strong> &nbsp;<asp:LinkButton id="shlb" OnClick="shlb_Click"  Text="Show/Hide" runat="server"/>
						</td>					
					</tr>
					
					<tr id="TRLembur" runat=server>
						<td colspan="5">
									<div id="div4" style="height: 300px;width:1000px;overflow: auto;">	
											<asp:DataGrid ID="dgjoblbr" runat="server" 
												 AllowSorting="True" 
												 AutoGenerateColumns="False"
												 CellPadding="2" 
												 GridLines="None" 
												 PagerStyle-Visible="False" 
												 Width="200%" 
												 OnItemDataBound="dgjoblbr_BindGrid" 
												 OnDeleteCommand="dgjoblbr_Delete" 
												 >
												<PagerStyle Visible="False" />
												<AlternatingItemStyle CssClass="mr-r" />
												<ItemStyle CssClass="mr-l" />
												<HeaderStyle CssClass="mr-h" />
												<Columns>
												 <asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:LinkButton id="Deletelbr" CommandName="Delete" Text="Delete" runat="server"/>
														 <asp:LinkButton id="Approvelbr" CommandName="Approve" Text="Approve" visible=False  runat="server"/>
														 </ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="No">
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="COA">
														<ItemTemplate>
														<asp:Label ID="lbcoalbr" runat="server" Text='<%#Container.DataItem("AccCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Pekerjaan">
														<ItemTemplate>
														<asp:Label id=dgjoblnk Text='<%# Container.DataItem("job") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
												 
													<asp:TemplateColumn HeaderText="Blok" >
														<ItemTemplate>
														<asp:Label ID="dgjobblklbr" runat="server" Text='<%#Container.DataItem("BlockCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Keterangan" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobnotelbr" runat="server" Text='<%#Container.DataItem("Ket")%>'></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Prd.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobprdbllbr" runat="server" Text='<%#Container.DataItem("PeriodeBl")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Real.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobhasilbllbr" runat="server" Text='<%#Container.DataItem("HasilBl")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													
													
													<asp:TemplateColumn HeaderText="Vol.Kerja" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobvollbr" runat="server" Text='<%#Container.DataItem("hasil")%>' width=50px></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="UOM" >
														<ItemTemplate>
														<asp:Label ID="dgjobuomlbr" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="Hk/Unit" >
														<ItemTemplate>
														<asp:TextBox ID="dghkunitlbr" runat="server" Text='<%#Container.DataItem("hkunit")%>' width=50px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn>
												   <asp:TemplateColumn HeaderText="HK.SKUB" >
														<ItemTemplate>
														<asp:Label ID="dghkskublbr" runat="server" Text='<%# Container.DataItem("hkskub")%>'></asp:Label>
														</ItemTemplate>
												   </asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="HK.SKUH" >
														<ItemTemplate>
														<asp:Label ID="dghkskuhlbr" runat="server" Text='<%#Container.DataItem("hkskuh")%>'></asp:Label>
														</ItemTemplate>
												   </asp:TemplateColumn>
												   <asp:TemplateColumn HeaderText="HK.PHL" >
														<ItemTemplate>
														<asp:Label ID="dghkphllbr" runat="server" Text='<%#Container.DataItem("hkphl")%>'></asp:Label>
														</ItemTemplate>
												   </asp:TemplateColumn>
												   
												  
												   <asp:TemplateColumn HeaderText="Rp/Unit">
														<ItemTemplate>
														<asp:TextBox ID="dgrpunitlbr" runat="server" Text='<%#Container.DataItem("RpUnit")%>'></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn> 
												  
												   <asp:TemplateColumn HeaderText="Rp.Lmbr.SKUB">
														<ItemTemplate>
														<asp:Label ID="dgrplskublbr" runat="server" Text='<%# FormatNumber(Container.DataItem("RpLemburSKUB"))%>'></asp:Label>
														</ItemTemplate>
												   
												   <FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
												   </FooterTemplate>
												   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
													</asp:TemplateColumn>
												   
												   <asp:TemplateColumn HeaderText="Rp.Lmbr.SKUH">
														<ItemTemplate>
														<asp:Label ID="dgrplskuhlbr" runat="server" Text='<%# FormatNumber(Container.DataItem("RpLemburSKUH"))%>'></asp:Label>
														</ItemTemplate>
														
														<FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
													   </FooterTemplate>
													   <FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
					
												   </asp:TemplateColumn> 
												  
												   <asp:TemplateColumn HeaderText="Rp.Lmbr.PHL">
														<ItemTemplate>
														<asp:Label ID="dgrplphllbr" runat="server" Text='<%# FormatNumber(Container.DataItem("RpLemburPHL"))%>'></asp:Label>
														</ItemTemplate>
														
														<FooterTemplate >
														<asp:Label ID=lbTotal2 runat=server />
														</FooterTemplate>
														<FooterStyle BorderWidth=1px BorderStyle=Outset  HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
				
												   </asp:TemplateColumn> 
												  
												  
													<asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:HiddenField ID="hidrkblnlbr" Value=<%# Container.DataItem("RKBCodeLn") %> runat=server />
														 <asp:HiddenField ID="hidrkbtylbr" Value=<%# Container.DataItem("RKBType") %> runat=server />
														 <asp:HiddenField ID="hidcatlbr" Value=<%# Container.DataItem("CatID") %> runat=server />
														 <asp:HiddenField ID="hidscatlbr" Value=<%# Container.DataItem("SubCatID") %> runat=server />
														 <asp:HiddenField ID="hidketlbr" Value=<%# Container.DataItem("Ket") %> runat=server />
														 <asp:HiddenField ID="hidpoidlbr" Value=<%# Container.DataItem("Poid") %> runat=server />
														 <asp:HiddenField ID="hidhkkontrlbr" Value=<%# Container.DataItem("HKKontr") %> runat=server />
														 <asp:HiddenField ID="hidRpRekananlbr" Value=<%# Container.DataItem("RpRekanan") %> runat=server />
														 <asp:HiddenField ID="hidRpLainlbr" Value=<%# Container.DataItem("RpLain") %> runat=server />
														 </ItemTemplate>
													</asp:TemplateColumn>
																											
												 </Columns>
											</asp:DataGrid>
									</div>
						</td>
					</tr>
					<tr id="TRLembur2" runat=server>
						<td colspan="5" style="background-color:#FFFFFF" >
							<table border="0" class="font9Tahoma" cellspacing="1" cellpadding="1" width="100%" >
												   <tr>
												   <td>
												   Kategori:<asp:DropDownList ID="ddljobkatlbr"  OnSelectedIndexChanged="ddljobkatlbr_OnSelectedIndexChanged"  AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Sub.Kategori:<asp:DropDownList ID="ddljobskatlbr"  OnSelectedIndexChanged="ddljobskatlbr_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Pekerjaan:<GG:AutoCompleteDropDownList  ID="ddljoblbr" OnSelectedIndexChanged="ddljoblbr_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="300px"/>&nbsp;
												   Blok:<GG:AutoCompleteDropDownList ID="ddljobblklbr" runat="server" class="font9Tahoma" Width="100px"/>
												   Ty.Kary:<asp:DropDownList ID="ddlEmpTypelbr" runat="server" class="font9Tahoma" Width="100px">
															<asp:ListItem Value="B">SKUB</asp:ListItem>
															<asp:ListItem Value="H">SKUH</asp:ListItem>
															<asp:ListItem Value="L" selected >PHL</asp:ListItem>
															</asp:DropDownList><br>
												   <asp:Button id=addjoblbr CssClass="button-small" Text="Add Lembur" OnClick="BtnAddlbr_OnClick" runat="server"/>
												   <asp:Button id=savejoblbr CssClass="button-small" Text="Save Lembur" OnClick="BtnSavelbr_OnClick" runat="server"/>
                                                   <asp:Button id=deljoblbr CssClass="button-small" Text="Del All Lembur" OnClick="BtnDellbr_OnClick" runat="server"/>
                                          
												   </td>
												   </tr>
						   </table>
						</td>					
					</tr>
					<tr>
						<td colspan="5" class="font9Tahoma" style="background-color:#FFCC00">
							<strong>Kontraktor</strong> &nbsp;<asp:LinkButton id="shkt" OnClick="shkt_Click"  Text="Show/Hide" runat="server"/>
						</td>					
					</tr>
					<tr id="TRKontraktor" runat=server>
						<td colspan="5">
									<div id="div4" style="height: 300px;width:1000px;overflow: auto;">	
											<asp:DataGrid ID="dgjobktk" runat="server" 
												 AllowSorting="True" 
												 AutoGenerateColumns="False"
												 CellPadding="2" 
												 GridLines="None" 
												 PagerStyle-Visible="False" 
												 Width="200%" 
												 OnItemDataBound="dgjobktk_BindGrid" 
												 OnDeleteCommand="dgjobktk_Delete" 
												 >
												<PagerStyle Visible="False" />
												<AlternatingItemStyle CssClass="mr-r" />
												<ItemStyle CssClass="mr-l" />
												<HeaderStyle CssClass="mr-h" />
												<Columns>
												 <asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:LinkButton id="Deletektk" CommandName="Delete" Text="Delete" runat="server"/>
														 <asp:LinkButton id="Approvektk" CommandName="Approve" Text="Approve" visible=False  runat="server"/>
														 </ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="No">
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="COA">
														<ItemTemplate>
														<asp:Label ID="lbcoaktk" runat="server" Text='<%#Container.DataItem("AccCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Pekerjaan">
														<ItemTemplate>
														<asp:LinkButton id=dgjoblnkktk Text='<%# Container.DataItem("job") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
												 
													<asp:TemplateColumn HeaderText="Blok" >
														<ItemTemplate>
														<asp:Label ID="dgjobblkktk" runat="server" Text='<%#Container.DataItem("BlockCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Supplier" >
														<ItemTemplate>
														<asp:label ID="dgjobsupktk" runat="server" Text='<%#Container.DataItem("SupplierCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="No.PO" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobpoktk" runat="server" Text='<%#Container.DataItem("POid")%>'></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Keterangan" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobnotektk" runat="server" Text='<%#Container.DataItem("Ket")%>'></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Prd.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobprdblktk" runat="server" Text='<%#Container.DataItem("PeriodeBl")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Real.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobhasilblktk" runat="server" Text='<%#Container.DataItem("HasilBl")%>'></asp:Label>
														 
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Vol.Kerja" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobvolktk" runat="server" Text='<%#Container.DataItem("hasil")%>' width=50px></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="UOM" >
														<ItemTemplate>
														<asp:Label ID="dgjobuomktk" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
												
												   <asp:TemplateColumn HeaderText="Kontraktor" >
														<ItemTemplate>
														<asp:TextBox ID="dghkkontrktk" runat="server" Text='<%#Container.DataItem("HKKontr")%>' width=50px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn>
												   
											
												   <asp:TemplateColumn HeaderText="Rp/Unit">
														<ItemTemplate>
														<asp:TextBox ID="dgrpunitktk" runat="server" Text='<%#Container.DataItem("RpUnit")%>' width=70px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn> 
												   
												  
												   <asp:TemplateColumn HeaderText="Rp.Kontraktor">
														<ItemTemplate>
														<asp:Label ID="dgrpkontraktk" runat="server" Text='<%# FormatNumber(Container.DataItem("RpKontraktor"))%>'></asp:Label>
														</ItemTemplate>
												   </asp:TemplateColumn>  
													<asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:HiddenField ID="hidrkblnktk" Value=<%# Container.DataItem("RKBCodeLn") %> runat=server />
														 <asp:HiddenField ID="hidrkbtyktk" Value=<%# Container.DataItem("RKBType") %> runat=server />
														 <asp:HiddenField ID="hidcatktk" Value=<%# Container.DataItem("CatID") %> runat=server />
														 <asp:HiddenField ID="hidscatktk" Value=<%# Container.DataItem("SubCatID") %> runat=server />
														 <asp:HiddenField ID="hidhkunitktk" Value=<%# Container.DataItem("HKUnit") %> runat=server />
														 <asp:HiddenField ID="hidhksubktk" Value=<%# Container.DataItem("HKSKUB") %> runat=server />
														 <asp:HiddenField ID="hidhkskuhktk" Value=<%# Container.DataItem("HKSKUH") %> runat=server />
														 <asp:HiddenField ID="hidhkphlktk" Value=<%# Container.DataItem("HKPHL") %> runat=server />
														 <asp:HiddenField ID="hidRpLainktk" Value=<%# Container.DataItem("RpLain") %> runat=server />
														 <asp:HiddenField ID="hidRpRekananktk" Value=<%# Container.DataItem("RpRekanan") %> runat=server />
												         
                                          
														 </ItemTemplate>
													</asp:TemplateColumn>
																											
												 </Columns>
											</asp:DataGrid>
									</div>
						</td>
					</tr>
					<tr id="TRKontraktor2" runat=server> 
						<td colspan="5" style="background-color:#FFFFFF" >
							<table border="0" class="font9Tahoma" cellspacing="1" cellpadding="1" width="100%" >
												   <tr>
												   <td>
												   Kategori:<asp:DropDownList ID="ddljobkatktk"  OnSelectedIndexChanged="ddljobkatktk_OnSelectedIndexChanged"  AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Sub.Kategori:<asp:DropDownList ID="ddljobskatktk"  OnSelectedIndexChanged="ddljobskatktk_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Pekerjaan:<GG:AutoCompleteDropDownList  ID="ddljobktk" OnSelectedIndexChanged="ddljobktk_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="300px"/>&nbsp;
												   Blok:<GG:AutoCompleteDropDownList ID="ddljobblkktk" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Supplier:<GG:AutoCompleteDropDownList ID="ddljobsupktk" OnSelectedIndexChanged="ddljobsupktk_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   No.PO:<GG:AutoCompleteDropDownList ID="ddljobpoktk" OnSelectedIndexChanged="ddljobpoktk_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Keterangan:<asp:TextBox ID="txtketktk" runat="server" class="font9Tahoma" Width="300px"/><br>
												   <asp:Button id=addjobktk CssClass="button-small" Text="Add Kontraktor" OnClick="BtnAddktk_OnClick"  runat="server"/>
												   <asp:Button id=savejobktk CssClass="button-small" Text="Save list"  OnClick="BtnSavektk_OnClick" runat="server"/>
                                                   <asp:Button id=deljobktk CssClass="button-small" Text="Delete list" OnClick="BtnDelktk_OnClick" runat="server"/>
												   
												   </td>
												   </tr>
						   </table>
						</td>					
					</tr>
					<tr>
						<td colspan="5" class="font9Tahoma" style="background-color:#FFCC00">
							<strong>Rekanan</strong> &nbsp;<asp:LinkButton id="shrk" OnClick="shrk_Click"  Text="Show/Hide" runat="server"/>
						</td>					
					</tr>
					<tr id="TRRekanan" runat=server>
						<td colspan="5">
									<div id="div4" style="height: 300px;width:1000px;overflow: auto;">	
											<asp:DataGrid ID="dgjobrkn" runat="server" 
												 AllowSorting="True" 
												 AutoGenerateColumns="False"
												 CellPadding="2" 
												 GridLines="None" 
												 PagerStyle-Visible="False" 
												 Width="200%" 
												 OnItemDataBound="dgjobrkn_BindGrid" 
												 OnDeleteCommand="dgjobrkn_Delete" 
												 >
												<PagerStyle Visible="False" />
												<AlternatingItemStyle CssClass="mr-r" />
												<ItemStyle CssClass="mr-l" />
												<HeaderStyle CssClass="mr-h" />
												<Columns>
												 <asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:LinkButton id="Deleterkn" CommandName="Delete" Text="Delete" runat="server"/>
														 <asp:LinkButton id="Approverkn" CommandName="Approve" Text="Approve" visible=False runat="server"/>
														 </ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="No">
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="COA">
														<ItemTemplate>
														<asp:Label ID="lbcoarkn" runat="server" Text='<%#Container.DataItem("AccCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Pekerjaan">
														<ItemTemplate>
														<asp:LinkButton id=dgjoblnkrkn Text='<%# Container.DataItem("job") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
												 
													<asp:TemplateColumn HeaderText="Blok" >
														<ItemTemplate>
														<asp:Label ID="dgjobblkrkn" runat="server" Text='<%#Container.DataItem("BlockCode")%>' ></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Supplier" >
														<ItemTemplate>
														<asp:label ID="dgjobsuprkn" runat="server" Text='<%#Container.DataItem("SupplierCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Barang" >
														<ItemTemplate>
														<asp:label ID="dgjobbarangrkn" runat="server" Text='<%#Container.DataItem("Itemcode")%>'></asp:label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													
													<asp:TemplateColumn HeaderText="Keterangan" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobnoterkn" runat="server" Text='<%#Container.DataItem("Ket")%>'></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Prd.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobprdblrkn" runat="server" Text='<%#Container.DataItem("PeriodeBl")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Real.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobhasilblrkn" runat="server" Text='<%#Container.DataItem("HasilBl")%>'></asp:Label>
														 
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Vol.Kerja" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobvolrkn" runat="server" Text='<%#Container.DataItem("hasil")%>' width=50px></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="UOM" >
														<ItemTemplate>
														<asp:Label ID="dgjobuomrkn" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
												  
												   
												   <asp:TemplateColumn HeaderText="Rekanan" >
														<ItemTemplate>
														<asp:TextBox ID="dghkkontrrkn" runat="server" Text='<%#Container.DataItem("HKKontr")%>' width=50px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn>
												   
											
												   <asp:TemplateColumn HeaderText="Rp/Unit">
														<ItemTemplate>
														<asp:TextBox ID="dgrpunitrkn" runat="server" Text='<%#Container.DataItem("RpUnit")%>' width=100px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn> 
												   
												  
												   <asp:TemplateColumn HeaderText="Rp.Rekanan">
														<ItemTemplate>
														<asp:label  ID="dgrprekanrkn" runat="server" Text='<%# FormatNumber(Container.DataItem("RpRekanan"))%>'></asp:label>
														</ItemTemplate>
												   </asp:TemplateColumn>  
													<asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:HiddenField ID="hidrkblnrkn" Value=<%# Container.DataItem("RKBCodeLn") %> runat=server />
														 <asp:HiddenField ID="hidrkbtyrkn" Value=<%# Container.DataItem("RKBType") %> runat=server />
														 <asp:HiddenField ID="hidcatrkn" Value=<%# Container.DataItem("CatID") %> runat=server />
														 <asp:HiddenField ID="hidscatrkn" Value=<%# Container.DataItem("SubCatID") %> runat=server />
														 <asp:HiddenField ID="dghkunitrkn" Value=<%# Container.DataItem("HKUnit") %> runat=server />
														 <asp:HiddenField ID="hidhksubrkn" Value=<%# Container.DataItem("HKSKUB") %> runat=server />
														 <asp:HiddenField ID="hidhkskuhrkn" Value=<%# Container.DataItem("HKSKUH") %> runat=server />
														 <asp:HiddenField ID="hidhkphlrkn" Value=<%# Container.DataItem("HKPHL") %> runat=server />
														 <asp:HiddenField ID="hidpoidrkn" Value=<%# Container.DataItem("poid") %> runat=server />
														 <asp:HiddenField ID="hidRpLainrkn" Value=<%# Container.DataItem("RpLain") %> runat=server />
														
														 </ItemTemplate>
													</asp:TemplateColumn>
																											
												 </Columns>
											</asp:DataGrid>
									</div>
						</td>
					</tr>
					<tr id="TRRekanan2" runat=server>
						<td colspan="5" style="background-color:#FFFFFF" >
							<table border="0" class="font9Tahoma" cellspacing="1" cellpadding="1" width="100%" >
												   <tr>
												   <td>
												   Kategori:<asp:DropDownList ID="ddljobkatrkn"  OnSelectedIndexChanged="ddljobkatrkn_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Sub.Kategori:<asp:DropDownList ID="ddljobskatrkn"  OnSelectedIndexChanged="ddljobskatrkn_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Pekerjaan:<GG:AutoCompleteDropDownList  ID="ddljobrkn" OnSelectedIndexChanged="ddljobrkn_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="300px"/>&nbsp;
												   Blok:<GG:AutoCompleteDropDownList ID="ddljobblkrkn" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Supplier:<GG:AutoCompleteDropDownList ID="ddljobsuprkn" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Barang:<GG:AutoCompleteDropDownList ID="ddljobitemrkn" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Keterangan:<asp:TextBox ID="txtketrkn" runat="server" class="font9Tahoma" Width="300px"/><br>
												   <asp:Button id=addjobrkn CssClass="button-small" Text="Add Rekanan" OnClick="BtnAddrkn_OnClick" runat="server"/>
												   <asp:Button id=savejobrkn CssClass="button-small" Text="Save Rekanan"  OnClick="BtnSaverkn_OnClick" runat="server"/>
                                                   <asp:Button id=deljobrkn CssClass="button-small" Text="Del All Rekanan" OnClick="BtnDelrkn_OnClick" runat="server"/>
                                          
												   </td>
												   </tr>
						   </table>
						</td>					
					</tr>
					<tr>
						<td colspan="5" class="font9Tahoma" style="background-color:#FFCC00">
							<strong>Lain-Lain</strong> &nbsp;<asp:LinkButton id="shln" OnClick="shln_Click"  Text="Show/Hide" runat="server"/>
						</td>					
					</tr>
					<tr id="TRLain" runat=server>
						<td colspan="5">
									<div id="div4" style="height: 300px;width:1000px;overflow: auto;">	
											<asp:DataGrid ID="dgjoblln" runat="server" 
												 AllowSorting="True" 
												 AutoGenerateColumns="False"
												 CellPadding="2" 
												 GridLines="None" 
												 PagerStyle-Visible="False" 
												 Width="200%" 
												 OnItemDataBound="dgjoblln_BindGrid" 
												 OnDeleteCommand="dgjoblln_Delete" 
												 >
												<PagerStyle Visible="False" />
												<AlternatingItemStyle CssClass="mr-r" />
												<ItemStyle CssClass="mr-l" />
												<HeaderStyle CssClass="mr-h" />
												<Columns>
												 <asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:LinkButton id="Deletelln" CommandName="Delete" Text="Delete" runat="server"/>
														 <asp:LinkButton id="Approvelln" CommandName="Approve" Text="Approve" visible=False runat="server"/>
														 </ItemTemplate>
													</asp:TemplateColumn>
													
												   <asp:TemplateColumn HeaderText="No">
														<ItemTemplate>
															<%# Container.ItemIndex + 1 %>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="COA">
														<ItemTemplate>
														<asp:Label ID="lbcoalln" runat="server" Text='<%#Container.DataItem("AccCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Pekerjaan">
														<ItemTemplate>
														<asp:LinkButton id=dgjoblnklln Text='<%# Container.DataItem("job") %>'  runat=server />
														</ItemTemplate>
													</asp:TemplateColumn>
												 
													<asp:TemplateColumn HeaderText="Blok" >
														<ItemTemplate>
														<asp:Label ID="dgjobblklln" runat="server" Text='<%#Container.DataItem("BlockCode")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Keterangan" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobnotelln" runat="server" Text='<%#Container.DataItem("Ket")%>'></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Prd.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobprdbllln" runat="server" Text='<%#Container.DataItem("PeriodeBl")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Real.Bulan lalu" >
														<ItemTemplate>
														 <asp:Label ID="dgjobhasilbllln" runat="server" Text='<%#Container.DataItem("HasilBl")%>'></asp:Label>
														 
														</ItemTemplate>
													</asp:TemplateColumn>
													
													<asp:TemplateColumn HeaderText="Vol.Kerja" >
														<ItemTemplate>
														<asp:TextBox ID="dgjobvollln" runat="server" Text='<%#Container.DataItem("hasil")%>' width=50px></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateColumn>

													<asp:TemplateColumn HeaderText="UOM" >
														<ItemTemplate>
														<asp:Label ID="dgjobuomlln" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													
												
												   <asp:TemplateColumn HeaderText="Rp/Unit">
														<ItemTemplate>
														<asp:TextBox ID="dgrpunitlln" runat="server" Text='<%#Container.DataItem("RpUnit")%>' width=70px></asp:TextBox>
														</ItemTemplate>
												   </asp:TemplateColumn> 
												   
												  
												   <asp:TemplateColumn HeaderText="Rp.Lain-Lain">
														<ItemTemplate>
														<asp:label ID="dgrplainlln" runat="server" Text='<%# FormatNumber(Container.DataItem("RpLain"))%>' width=100px></asp:label>
														</ItemTemplate>
												   </asp:TemplateColumn>  
													<asp:TemplateColumn>					
														 <ItemTemplate>
														 <asp:HiddenField ID="hidrkblnlln" Value=<%# Container.DataItem("RKBCodeLn") %> runat=server />
														 <asp:HiddenField ID="hidrkbtylln" Value=<%# Container.DataItem("RKBType") %> runat=server />
														 <asp:HiddenField ID="hidcatlln" Value=<%# Container.DataItem("CatID") %> runat=server />
														 <asp:HiddenField ID="hidscatlln" Value=<%# Container.DataItem("SubCatID") %> runat=server />
														 <asp:HiddenField ID="dghkunitlln" Value=<%# Container.DataItem("HKUnit") %> runat=server />
														 <asp:HiddenField ID="hidhksublln" Value=<%# Container.DataItem("HKSKUB") %> runat=server />
														 <asp:HiddenField ID="hidhkskuhlln" Value=<%# Container.DataItem("HKSKUH") %> runat=server />
														 <asp:HiddenField ID="hidhkphllln" Value=<%# Container.DataItem("HKPHL") %> runat=server />
														 <asp:HiddenField ID="dghkkontrlln" Value=<%# Container.DataItem("HKKontr") %> runat=server />
														 <asp:HiddenField ID="hidpoidlln" Value=<%# Container.DataItem("poid") %> runat=server />
														 <asp:HiddenField ID="dgrprekanlln" Value=<%# Container.DataItem("RpRekanan") %> runat=server />
														
														 </ItemTemplate>
													</asp:TemplateColumn>
																											
												 </Columns>
											</asp:DataGrid>
									</div>
						</td>
					</tr>
				    <tr id="TRLain2" runat=server>
						<td colspan="5" style="background-color:#FFFFFF" >
							<table border="0" class="font9Tahoma" cellspacing="1" cellpadding="1" width="100%" >
												   <tr>
												   <td>
												   Kategori:<asp:DropDownList ID="ddljobkatlln"  OnSelectedIndexChanged="ddljobkatlln_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Sub.Kategori:<asp:DropDownList ID="ddljobskatlln"  OnSelectedIndexChanged="ddljobskatlln_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="100px"/>&nbsp;
												   Pekerjaan:<GG:AutoCompleteDropDownList  ID="ddljoblln" OnSelectedIndexChanged="ddljoblln_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="font9Tahoma" Width="300px"/>&nbsp;
												   Blok:<GG:AutoCompleteDropDownList ID="ddljobblklln" runat="server" class="font9Tahoma" Width="100px"/><br>
												   Keterangan:<asp:TextBox ID="txtketlln" runat="server" class="font9Tahoma" Width="300px"/>
												   <br>   
												   <asp:Button id=addjoblln CssClass="button-small" Text="Add Lain-Lain" OnClick="BtnAddlln_OnClick"  runat="server"/>
												   <asp:Button id=savejoblln CssClass="button-small" Text="Save Lain-Lain"  OnClick="BtnSavelln_OnClick" runat="server"/>
                                                   <asp:Button id=deljoblln CssClass="button-small" Text="Del All Lain-Lain" OnClick="BtnDellln_OnClick" runat="server"/>
                                          
												   </td>
												   </tr>
						   </table>
						</td>					
					</tr>
				</table>
				</div>
			</td>
        </tr>
    </table>
        <input type="hidden" id="isNew" value="" runat="server" />
        <input type="hidden" id="hid_div" value="" runat="server" />
		<input type="hidden" id="hid_status" value="" runat="server" />
    </form>
</body>
</html>
