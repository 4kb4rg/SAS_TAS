<%@ Page Language="vb" src="../../../include/PR_setup_AktivitiDet_Estate.aspx.vb" Inherits="PR_setup_AktivitiDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Aktiviti Details</title>
        <Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                height: 16px;
            }
        </style>
	</head>
	<body>
		
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
           <table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td   colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                    <strong>DETAIL AKTIVITI</strong>
                                </td>
                                <td class="font9Header" style="text-align: right">
                                    Tgl buat :&nbsp; <asp:Label id=lblDateCreated runat=server />| Status : <asp:Label id=lblStatus runat=server />
                                    | Tgl update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Di update : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                          <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6 class="style2"></td>
				</tr>
										
				<tr>
					<td width=20% >
                        Kode Aktiviti</td>
					<td width=30% >
						<asp:Textbox id=txtid maxlength=15 width="40%" CssClass="font9Tahoma"  runat=server  />
					<td style="width: 79px">&nbsp;</td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>								
				</tr>				
				
				<tr>
					<td align="left">
                        Kategori</td>
					<td align="left"><GG:AutoCompleteDropDownList ID="ddl_kat" CssClass="font9Tahoma"  runat="server" Width="100%" OnSelectedIndexChanged="ddl_kat_OnSelectedIndexChanged" AutoPostBack=true/>
                    </td>
					<td style="width: 79px">&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 22px">
                        Sub Kategori </td>
					<td align="left" style="height: 22px"><GG:AutoCompleteDropDownList ID="ddl_subkat" CssClass="font9Tahoma"  runat="server" Width="100%" OnSelectedIndexChanged="ddl_subkat_OnSelectedIndexChanged" AutoPostBack=true />
                    </td>
					<td style="width: 79px; height: 22px;">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 22px">
                        Sub Sub Kategori </td>
					<td align="left" style="height: 22px"><asp:TextBox ID="txtsubsubcat" CssClass="font9Tahoma"  runat="server" MaxLength="3" Width="40%"></asp:TextBox>
                    </td>
					<td style="width: 79px; height: 22px;">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
				</tr>
				
				<tr>
					<td align="left" style="height: 28px">
                        Deskripsi</td>
					<td align="left" style="height: 28px">
                    <asp:TextBox ID="txtdesc" CssClass="font9Tahoma"  runat="server" MaxLength="100" Width="100%"></asp:TextBox></td>
					<td style="width: 79px; height: 28px;">&nbsp;</td>
					<td style="height: 28px"></td>
					<td style="height: 28px"></td>
				</tr>
				<tr>
					<td>
                        UOM</td>
					<td>                        
                    <asp:Textbox id=txtuom maxlength=64 width="40%" CssClass="font9Tahoma"  runat=server></asp:Textbox></td>
                    <td style="width: 79px">&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td align="left" style="height: 23px">
                        Norma</td>
					<td align="left" style="height: 23px">
                        <asp:TextBox ID="txtnorma" CssClass="font9Tahoma"  runat="server" MaxLength="100" Width="100%"></asp:TextBox></td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px"></td>
					<td style="height: 23px"></td>
				</tr>
				
				
				<tr>
					<td align="left" style="height: 23px">
                        Initial Premi</td>
					<td align="left" style="height: 23px">
                       <asp:DropDownList width=100% id=ddlInitPremi  CssClass="font9Tahoma"  runat=server>
                       </asp:DropDownList> 
                    </td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px"></td>
					<td style="height: 23px"></td>
				</tr>
			
				
                <tr>
                <td colspan="5" style="height: 23px">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					&nbsp;
					</td>
				</tr>
			
			    <tr>
                	<td colspan="5">
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="2" border="0" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<td width=20% height=25>
                                                Divisi :*</TD>
											<td style="width: 30%"><GG:AutoCompleteDropDownList id=ddl_divisi  OnSelectedIndexChanged="ddl_divisi_OnSelectedIndexChanged" AutoPostBack=true width=100%  CssClass="font9Tahoma"  runat=server /></TD>
											<td align=center style="width: 5%"> </td>
											<td width=15%>
                                                Tipe</td>
											<td style="width: 30%">
                                                <asp:RadioButtonList ID="rbtype" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    RepeatDirection="Horizontal" Width="66px" CssClass="font9Tahoma" >
                                                    <asp:ListItem Selected="True" Value="K">Kerja</asp:ListItem>
                                                    <asp:ListItem Value="U">Upah</asp:ListItem>
                                                </asp:RadioButtonList></TD>
										</TR>
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Kode Tahun Tanam :*</TD>
											<td style="width: 30%; height: 26px"><GG:AutoCompleteDropDownList id=ddl_subblok  width=100% CssClass="font9Tahoma"  runat=server /></TD>
											<td align=center style="height: 26px; width: 5%;"> </td>
											<td width=15% style="height: 26px">
                                                Distribusi</td>
											<td style="width: 30%; height: 26px;">
                                                <asp:RadioButtonList ID="rbdistribusi" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" RepeatDirection="Horizontal" Width="66px" CssClass="font9Tahoma">
                                                    <asp:ListItem Selected="True" Value="Y">Ya</asp:ListItem>
                                                    <asp:ListItem Value="T">Tidak</asp:ListItem>
                                                </asp:RadioButtonList></TD>
										</TR>
										<TR class="mb-c">
											<td width=20% height=25>
                                                Tahun Tanam</TD>
											<td style="width: 30%">
                                                <asp:RadioButtonList ID="rbtahuntanam" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" RepeatDirection="Horizontal" Width="86px">
                                                    <asp:ListItem Selected="True" Value="Y">Ya</asp:ListItem>
                                                    <asp:ListItem Value="N">Tidak</asp:ListItem>
                                                </asp:RadioButtonList><asp:TextBox ID="txtyr" CssClass="font9Tahoma"  runat="server" MaxLength="4" onkeypress="javascript:return isNumberKey(event)"
                                                    Width="37%"></asp:TextBox></TD>
											<td align=center style="width: 5%"> </td>
											<td width=15%>
                                                </td>
											<td style="width: 30%"></TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% height=25>
                                                COA Transit *</TD>
											<td style="width: 30%">
                                                <asp:TextBox ID="txtAccTransit" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Transit"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccTransit', 'txtAccName_Transit', 'False');"
                                                        size="" type="button" value=" ... " /><asp:TextBox ID="txtAccName_Transit"  CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox></TD>
											<td align=center style="width: 5%"> </td>
											<td width=15%>
                                                Alokasi JHT</td>
											<td style="width: 30%">
                                                <asp:TextBox ID="txtAccJHT" CssClass="font9Tahoma"  runat="server" Font-Bold="True" Font-Size="X-Small"
                                                    MaxLength="32" Width="28%"></asp:TextBox><input id="Find_JHT" CssClass="font9Tahoma"  runat="server" causesvalidation="False"
                                                        onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccJHT', 'txtAccName_JHT', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_JHT" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox>
                                                </TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% height=25>
                                                Alokasi Upah *</TD>
											<td style="width: 30%"><GG:AutoCompleteDropDownList id=ddl_coa_upah width=100% OnSelectedIndexChanged="ddl_coa_upah_OnSelectedIndexChanged" AutoPostBack=true CssClass="font9Tahoma"  runat=server /></TD>
											<td align=center style="width: 5%"> </td>
											<td width=15%>
                                                Alokasi Obat</td>
											<td style="width: 30%">
                                                <asp:TextBox ID="txtAccObat" CssClass="font9Tahoma"   runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Obat"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccObat', 'txtAccName_Obat', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Obat" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox>
                                                </TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Alokasi Premi</TD>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtAccPremi" CssClass="font9Tahoma"  runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Premi"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccPremi', 'txtAccName_Premi', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Premi" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox>
                                            </TD>
											<td align=center style="height: 26px; width: 5%;"> </td>
											<td width=15% style="height: 26px">
                                                Alokasi Thr/Bonus</td>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtAccTHR" CssClass="font9Tahoma"  runat="server" Font-Bold="True" Font-Size="X-Small"
                                                    MaxLength="32" Width="28%"></asp:TextBox><input id="Find_THR" runat="server" causesvalidation="False"
                                                        onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccTHR', 'txtAccName_Bonus', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Bonus" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox>
                                                </TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Alokasi Lembur</TD>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtAccLembur" CssClass="font9Tahoma"  runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Lembur"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccLembur', 'txtAccName_Lembur', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Lembur" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox>
                                                </TD>
											<td align=center style="height: 26px; width: 5%;"> </td>
											<td width=15% style="height: 26px">
                                                Alokasi Beras</td>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtAccBeras" CssClass="font9Tahoma"  runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Beras"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccBeras', 'txtAccName_Beras', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Beras" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="32" Width="60%"></asp:TextBox>
                                                </TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Alokasi Astek</TD>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtAccAstek" CssClass="font9Tahoma"  runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Astek"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccAstek', 'txtAccName_Astek', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Astek" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="100" Width="60%"></asp:TextBox>
                                                </TD>
											<td align=center style="height: 26px; width: 5%;"> </td>
											<td width=15% style="height: 26px">
                                                Alokasi Pemborong </td>
											<td style="width: 30%; height: 26px;">
												<asp:TextBox ID="txtAccPemborong" CssClass="font9Tahoma"  runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Pemborong"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccPemborong', 'txtAccName_Pemborong', 'False');"
                                                        type="button" value=" ... " />
														<asp:TextBox ID="txtAccName_Pemborong" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="100" Width="60%"></asp:TextBox>
											</TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Alokasi Bahan</TD>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtAccBahan"  CssClass="font9Tahoma" runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Bahan"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccBahan', 'txtAccName_Bahan', 'False');"
                                                        type="button" value=" ... " /><asp:TextBox ID="txtAccName_Bahan" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="100" Width="60%"></asp:TextBox>
                                                </TD>
											<td align=center style="height: 26px; width: 5%;"> </td>
											<td width=15% style="height: 26px">
                                               Alokasi Kendaraan </td>
											<td style="width: 30%; height: 26px;">
											<asp:TextBox ID="txtAccKendaraan" CssClass="font9Tahoma"  runat="server" Font-Bold="True"
                                                    Font-Size="X-Small" MaxLength="32" Width="28%"></asp:TextBox><input id="Find_Kendaraan"
                                                        runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccKendaraan', 'txtAccName_Kendaraan', 'False');"
                                                        type="button" value=" ... " />
														<asp:TextBox ID="txtAccName_Kendaraan" CssClass="font9Tahoma"  runat="server" AutoPostBack="True" Font-Bold="False" Font-Size="X-Small"
                                                    MaxLength="100" Width="60%"></asp:TextBox>
											</TD>
										</TR>
										
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Periode COA</TD>
											<td style="width: 30%; height: 26px;">
                                                <asp:TextBox ID="txtperiodestart" CssClass="font9Tahoma"  runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"  Width="28%" Font-Bold="True"></asp:TextBox>
											    s/d  
											    <asp:TextBox ID="txtperiodeend" CssClass="font9Tahoma"  runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event)"  Width="28%" Font-Bold="True"></asp:TextBox>
                                                &nbsp;
                                                </TD>
											<td align=center style="height: 26px; width: 5%;"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
											
											</TD>
										</TR>
										
										<TR class="mb-c">
											<TD colspan=6>
												<asp:Label id=lblErrSelectBoth visible=False forecolor=Red text="<br>Please select either Account Group Code OR Account Code only." display=dynamic runat=server Width="100%"/>&nbsp;
											</TD>
										</TR>
										<TR class="mb-c">
											<TD colspan=6 >
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
                                            &nbsp;</TD>    
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="5">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2 
							OnItemDataBound="dgLineDet_BindGrid" 
							OnItemCommand="GetItem" 
							OnDeleteCommand="dgLineDet_Delete"  
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma" >
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
							    
								<asp:TemplateColumn HeaderText="Periode">
									<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("PeriodeStart") + " - " + Container.DataItem("PeriodeEnd") %> id="lblperiode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
							    <asp:TemplateColumn HeaderText="Tahun Tanam">
									<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("CodeBlk") %> id="lblttnm" runat="server" Visible=false/>
									<asp:LinkButton ID="lbID" runat="server" CommandName="Item" Text='<%# Container.DataItem("CodeBlk") %>'></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								                               
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("CodeBlkGrp") %> id="lbldivisi" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
														
								<asp:TemplateColumn HeaderText="COA Transit">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Transit") %> id="lbltransit" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="COA Gaji/Upah">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AlokasiGajiUpah") %> id="lblalo" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Deskripsi">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lbldesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>					
									<ItemTemplate>
									    <asp:HiddenField ID="hidjob" Value=<%# Container.DataItem("CodeJob") %> runat=server />
										<asp:HiddenField ID="hidpstart" Value=<%# Container.DataItem("PeriodeStart") %> runat=server />
										<asp:HiddenField ID="hidpend" Value=<%# Container.DataItem("PeriodeEnd") %> runat=server />
										
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
					
								</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				 <tr>
                <td colspan="5" style="height: 23px">&nbsp;</td>
				</tr>
				
				<Input Type=Hidden id=BlokCode runat=server />&nbsp;
				<Input Type=Hidden id=isNew runat=server />&nbsp;
				<Input Type=Hidden id=PStart runat=server />&nbsp;
				<Input Type=Hidden id=PEnd runat=server />&nbsp;
				
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
     	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
