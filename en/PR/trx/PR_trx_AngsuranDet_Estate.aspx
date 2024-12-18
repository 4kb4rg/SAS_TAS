<%@ Page Language="vb" src="../../../include/PR_trx_AngsuranDet_Estate.aspx.vb" Inherits="PR_trx_AngsuranDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%> 
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<html>
	<head>
		<title>ReSign Details</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		
		function hitungbulan()
        {
        var e = document.getElementById("ddlMonth_start");
        var strUser = e.options[e.selectedIndex].value;
        
        var f = document.getElementById("ddlyear");
        var stryr = f.options[f.selectedIndex].value;
              
        var doc = document.frmMain;
        var bln = doc.txtangsuranBln.value;
        var tot = parseFloat(doc.txtangsuranTot.value);
        var mn_start = strUser;
        var yr_start = stryr;
        var c = parseFloat(mn_start) + parseFloat(bln);
            
        if (bln != '')
        {
            if (c > 12)
            {
                if (parseFloat(c-12-1)==0)
                {
                doc.lblmonth_end.value = parseFloat(12)
                doc.lblyear_end.value = parseFloat(yr_start)
                }
                else
                {
                doc.lblmonth_end.value = parseFloat(c-12-1)
                doc.lblyear_end.value = parseFloat(yr_start) + 1
                }
            }
            else
            {
                doc.lblmonth_end.value = parseFloat(c-1)
                doc.lblyear_end.value = parseFloat(yr_start)
            }
             
             if (doc.txtangsuranTot.value != '')
             {
                doc.txtangsuran.value = parseFloat(round(tot/bln));
                doc.hidangsuran.value = doc.txtangsuran.value
                
             }
        }
        else
        exit; 
       
        }
		
		</script>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
			<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma"> 
				<tr>
					<td colspan="5">
						<UserControl:MenuPRTrx ID="MenuPRTrx" runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">
                        DETAIL ANGSURAN KARYAWAN</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
										
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Kode Angsuran</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:label id="lblidM" runat="server" /></td>
					<td style="width: 30px"></td>
					<td>Tgl Buat : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>				
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Divisi </td>
					<td align="left" style="height: 26px; width: 461px;">
						<GG:AutoCompleteDropDownList id=ddldivisicode width="45%" runat=server OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" AutoPostBack=true/>
                        <asp:label id="lbldivisi" runat="server" /></td>
					<td style="width: 30px; height: 26px;"></td>
					<td width=20% style="height: 26px">Status : </td>
					<td width=25% style="height: 26px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        NIK</td>
					<td align="left" style="height: 26px; width: 461px;"><GG:AutoCompleteDropDownList id=ddlempcode width="100%" runat=server />
                        <asp:label id="lblempcode" runat="server" />
                    </td>
					<td style="width: 30px; height: 26px;"></td>
					<td style="height: 26px">Tgl update : </td>
					<td style="height: 26px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tipe Angsuran</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <GG:AutoCompleteDropDownList ID="ddltype" runat="server" Width="100%" >
													<asp:ListItem Value="B">Gudang/Alat Panen</asp:ListItem>
													<asp:ListItem Value="K">Kantin/Koperasi</asp:ListItem>
													<asp:ListItem Value="L">Listrik</asp:ListItem>
													<asp:ListItem Value="S">SPSI</asp:ListItem>
													<asp:ListItem Value="H">PPH21</asp:ListItem>
													<asp:ListItem Value="T">PPH21 (THR)</asp:ListItem>
													<asp:ListItem Value="D">Potongan Denda</asp:ListItem>
													<asp:ListItem Value="M">Potongan Materai</asp:ListItem>
													<asp:ListItem Value="P">Potongan Pinjaman</asp:ListItem>
													<asp:ListItem Value="J">Potongan BPJS lainnya</asp:ListItem>
													<asp:ListItem Value="U">Potongan Lainnya</asp:ListItem>
						</GG:AutoCompleteDropDownList>
				    </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px">Diupdate :</td>
					<td style="height: 26px"><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Keterangan</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtket" runat="server" Width="100%" /></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tot.Angsuran (Rp)</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtangsuranTot" runat="server" Width="35%" onkeypress="return isNumberKey(event)"  onkeyup="javascript:hitungbulan()" style="text-align:right;" ></asp:TextBox></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Tempo</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtangsuranBln" runat="server" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event)" onkeyup="javascript:hitungbulan()" style="text-align:right;" ></asp:TextBox>
                        Bulan</td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Angsuran/Bulan (Rp)</td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:TextBox ID="txtangsuran" runat="server"  ReadOnly=true CssClass = "mr-h" Width="35%" style="text-align:right;" ></asp:TextBox>
                      <asp:HiddenField ID="hidangsuran" runat="server"></asp:HiddenField>
                        </td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

                <tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Periode Mulai </td>
					<td align="left" style="height: 26px; width: 461px;">
                        <asp:DropDownList id="ddlMonth_start" width="45%" runat=server onchange="javascript:hitungbulan()">
										<asp:ListItem value="01">January</asp:ListItem>
										<asp:ListItem value="02">February</asp:ListItem>
										<asp:ListItem value="03">March</asp:ListItem>
										<asp:ListItem value="04">April</asp:ListItem>
										<asp:ListItem value="05">May</asp:ListItem>
										<asp:ListItem value="06">June</asp:ListItem>
										<asp:ListItem value="07">July</asp:ListItem>
										<asp:ListItem value="08">Augustus</asp:ListItem>
										<asp:ListItem value="09">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>&nbsp;
                        <asp:DropDownList id="ddlyear" width="45%" runat=server/></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 181px;">
                        Periode Selesai </td>
					<td align="left" style="height: 26px; width: 461px;"><asp:TextBox id=lblmonth_end ReadOnly=true width="10%" maxlength="2" runat="server" />/<asp:TextBox id=lblyear_end width="20%" ReadOnly=true maxlength="4" runat="server" /></td>
					<td style="width: 30px ;height: 26px;"></td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				
				<td colspan="5" style="height: 23px"></td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=NewBtn AlternateText="  New  " imageurl="../../images/butt_new.gif" onclick=NewBtn_Click CommandArgument=new runat=server />
					    <asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=SaveBtn_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText="Delete" imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=DelBtn_Click CommandArgument=Del runat=server />&nbsp;
						<asp:ImageButton id=ApvBtn AlternateText=" Approve " imageurl="../../images/butt_Approve.gif" CausesValidation=False onclick=ApvBtn_Click CommandArgument=Apv runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				
				<td colspan="5" style="height: 23px"><asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." ForeColor=red runat="server" /></td>
				<tr>
					<td colspan=6>
                    <igtab:UltraWebTab ID="TAB" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" Width=100% runat="server">
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
                            <%--List Angsuran--%>
                            <igtab:Tab Key="Tab_Angsur" Text="Daftar Angsuran" Tooltip="Daftar Angsuran">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgAngsur" runat="server" 
                                                     AutoGenerateColumns="False" 
													 OnItemCommand="GetItem"
													 OnDeleteCommand=ANGSUR_DEDR_Delete 
                                                     GridLines="Both"
                                                     CellPadding="2" 
                                                     Width="100%" >
                                                    <AlternatingItemStyle Cssclass="font9Tahoma" />
                                                    <ItemStyle Cssclass="font9Tahoma" />
                                                    <HeaderStyle Cssclass="font9Tahoma" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Kode Angsuran">
                                                            <ItemTemplate>
															<asp:Label ID="dgAngsur_KrdCode" runat="server" Text='<%# Container.DataItem("KrdCode") %>' Visible="false"></asp:Label>
															<asp:LinkButton ID="lnkdgAngsur_KrdCode" runat="server" CommandName="item" Text='<%# Container.DataItem("KrdCode") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Type">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_CodeItem" Text='<%# Container.DataItem("typekrd_desc") %>' runat="server" /> 
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Tot.Angsuran">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_TotalRp" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalRp"),2),0) %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Tempo">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_Tempo" Text='<%# Container.DataItem("TotalBulan") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Angsuran">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_Angsuran" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("KrdPerBulan"),2),0) %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Periode Mulai">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_TahunMulai" Text='<%# Container.DataItem("BulanMulai") & "/" & Container.DataItem("TahunMulai") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Periode Selesai">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_TahunSelesai" Text='<%# Container.DataItem("BulanSelesai") & "/" & Container.DataItem("TahunSelesai") %>' runat="server" />
                                                             </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Sisa Angsuran">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_SisaKredit" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("SisaKredit"),2),0) %>' runat="server" />
                                                             </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Status">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgAngsur_Status" Text='<%# Container.DataItem("Status") %>' runat="server" />
                                                             </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                    
                                                        <asp:TemplateColumn HeaderText="Tgl Update" HeaderStyle-HorizontalAlign="Center" SortExpression="UpdateDate">
								                        	<ItemTemplate>
										                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                        </ItemTemplate>
									                        <ItemStyle HorizontalAlign="Center" Width="7%"/>
								                            </asp:TemplateColumn>
								
								                        <asp:TemplateColumn HeaderText="Diupdate" HeaderStyle-HorizontalAlign="Center" SortExpression="UserName">
									                        <ItemTemplate>
										                    <%# Container.DataItem("uName") %>
									                        </ItemTemplate>
								                            <ItemStyle HorizontalAlign="Center" Width="5%"/>
								                        </asp:TemplateColumn>
														
														<asp:TemplateColumn>
															<ItemTemplate>
															<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" Width="10%"/>
							                            </asp:TemplateColumn>
								                     
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                      </table>
                                </ContentPane>
                            </igtab:Tab>
                         
                            <igtab:TabSeparator>
                            </igtab:TabSeparator>
                            
                            <%--List Pembayaran--%>
                            <igtab:Tab Key="Tab_Bayar" Text="Daftar Pembayaran" Tooltip="Daftar Pembayaran">
                                <ContentPane>
									<table border="0" cellspacing="1" cellpadding="1" width="99%" >
                                        <tr>                    
											<td class="font9Tahoma" colspan="5">
											Pembayaran Manual</td>
										</tr>
										<tr>
											<td colspan=6><hr size="1" noshade></td>
										</tr>
										
										<tr>
											<td align="left" class="font9Tahoma" style="height: 26px; width: 181px;">
											Kode Angsuran</td>
											<td align="left" style="height: 26px; width: 461px;">
											<asp:DropDownList id="ddlpay_krdCode" runat="server" OnSelectedIndexChanged="ddlpay_krdCode_OnSelectedIndexChanged" AutoPostBack="true"/></td>
											<td style="width: 30px"></td>
											<td></td>
											<td></td>								
										</tr>	

										<tr>
											<td align="left" class="font9Tahoma" style="height: 26px; width: 181px;">
											Tgl Bayar (hari/bulan/tahun): * </td>
											<td align="left" class="font9Tahoma" style="height: 26px; width: 461px;">
											<asp:TextBox ID="txtWPDate" class="font9Tahoma" runat="server" MaxLength="10" />
											<a href="javascript:PopCal('txtWPDate');"><asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
					                        </td>
											<td style="width: 30px"></td>
											<td></td>
											<td></td>								
										</tr>	

										<tr>
											<td align="left" class="font9Tahoma" style="height: 26px; width: 181px;">
											Sisa Kredit </td>
											<td align="left" class="font9Tahoma" style="height: 26px; width: 461px;">
											<asp:Textbox id="ddlpay_sisa" runat="server" ReadOnly=true CssClass = "mr-h" style="text-align:right;"/>
											Periode Mulai   : <asp:label id="lblpay_prdawl" runat="server" /> 
											Periode Selesai : <asp:label id="lblpay_prd" runat="server" />
											</td>
											
											<td style="width: 30px" ></td>
											<td></td>
											<td></td>								
										</tr>	
										
										<tr>
											<td align="left" class="font9Tahoma" style="height: 26px; width: 181px;">
											Pembayaran *</td>
											<td align="left" style="height: 26px; width: 461px;">
											<asp:Textbox id="ddlpay_pay" runat="server" onkeypress="return isNumberKey(event)" /></td>
											<td style="width: 30px"></td>
											<td></td>
											<td></td>								
										</tr>	
										
										<tr>
											<td align="left" style="height: 26px; width: 181px;">
											<asp:ImageButton id=PayBtn AlternateText="  Save  " imageurl="../../images/butt_add.gif" onclick=PayBtn_Click CommandArgument=Save runat=server /></td>
											<td align="left" style="height: 26px; width: 461px;">
											</td>
											<td style="width: 30px"></td>
											<td></td>
											<td></td>								
										</tr>	
                                    </table>
								
                                    <table border="0" class="font9Tahoma" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgBayar" runat="server" 
                                                    AutoGenerateColumns="False" 
													OnDeleteCommand=DEDR_Delete 
                                                    GridLines="Both"
                                                    CellPadding="2" Width="100%"> 
                                          
                                                    <AlternatingItemStyle Cssclass="font9Tahoma" />
                                                    <ItemStyle Cssclass="font9Tahoma" />
                                                    <HeaderStyle Cssclass="font9Tahoma" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Kode Angsuran">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBayar_KrdCode" Text='<%# Container.DataItem("KrdCode") %>' runat="server" />    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Bulan">
                                                            <ItemTemplate>
                                                             <asp:Label ID="dgBayar_AccMonth" Text='<%# Container.DataItem("AccMonth") %>' runat="server" />    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Tahun">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBayar_AccYear" Text='<%# Container.DataItem("AccYear") %>' runat="server" />    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Tgl Bayar">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBayar_PayDate" Text='<%# objGlobal.GetLongDate(Container.DataItem("PayDate")) %>' runat="server" />    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                                                                        
                                                        
                                                        <asp:TemplateColumn HeaderText="Bayar">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBayar_Amount" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"),2),0) %>' runat="server" />    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                        </asp:TemplateColumn>
														
														 <asp:TemplateColumn HeaderText="Tgl Update" HeaderStyle-HorizontalAlign="Center" >
								                        	<ItemTemplate>
										                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                        </ItemTemplate>
									                        <ItemStyle HorizontalAlign="Center" Width="7%"/>
								                            </asp:TemplateColumn>
								
								                        <asp:TemplateColumn HeaderText="Diupdate" HeaderStyle-HorizontalAlign="Center" >
									                        <ItemTemplate>
										                    <%# Container.DataItem("UpdateID") %>
									                        </ItemTemplate>
								                            <ItemStyle HorizontalAlign="Center" Width="5%"/>
								                        </asp:TemplateColumn>
														
														<asp:TemplateColumn>
															<ItemTemplate>
															<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" Width="10%"/>
							                            </asp:TemplateColumn>		
                                                        
                                                      </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                   </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                            <igtab:TabSeparator>
                            </igtab:TabSeparator>
                         
                             <%--List Billing--%>
                            <igtab:Tab Key="Tab_bill" Text="Daftar Tagihan" Tooltip="Daftar Tagihan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgBil" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" Width="100%" >
                                                    <AlternatingItemStyle Cssclass="font9Tahoma" />
                                                    <ItemStyle Cssclass="font9Tahoma" />
                                                    <HeaderStyle Cssclass="font9Tahoma" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Bulan">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBil_AccMonth" Text='<%# Container.DataItem("AccMonth") %>' runat="server" />    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Tahun">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBil_AccYearx" Text='<%# Container.DataItem("AccYear") %>' runat="server" />       
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Saldo Awal">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBil_SaldoAwal" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("SaldoAwal"),2),0) %>' runat="server" />       
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                       </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Penambahan">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBil_Penambahan" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Penambahan"),2),0) %>' runat="server" />       
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Pelunasan">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBil_Pelunasan" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Pelunasan"),2),0) %>' runat="server" />       
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                        </asp:TemplateColumn>
                                                                                
                                                        <asp:TemplateColumn HeaderText="Sisa Angsuran">
                                                            <ItemTemplate>
                                                            <asp:Label ID="dgBil_Sisa" Text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Sisa"),2),0) %>' runat="server" />       
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                        </asp:TemplateColumn>                       
                                                        
                                                    </Columns>
                                                </asp:DataGrid>
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
			<input type="hidden" id="isNew" value="" runat="server" />
			<Input type=hidden id=idMDR value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
			</form>
	</body>
</html>
