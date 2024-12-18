<%@ Page Language="vb" Src="../../../include/PR_trx_KTNDet_New_Estate.aspx.vb" Inherits="PR_trx_KTNDet_New_Estate" %>
<%@ Register TagPrefix="UserControl" TagName="MenuPRTrx" Src="../../menu/menu_prtrx.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>BUKU KERJA DETAIL</title>
     <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        </style>
</head>
<body>
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

        <asp:Label ID="lblErrMessage" Visible="false" Text="" ForeColor="red" runat="server" />
        <table border="0" cellspacing="0" cellpadding="0" width="99%" class="font9Tahoma">
            <tr>
                <td colspan="5">
                    <UserControl:MenuPRTrx ID="MenuPRTrx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="mt-h" colspan="5">
                    <table cellpadding="0" cellspacing="0" class="style1" >
                        <tr>
                            <td class="font9Tahoma">
                              <strong> BUKU KERJA SPK </strong>  </td>
                            <td class="font9Header" style="text-align: right">
                    Periode :
                    <asp:Label ID="lblPeriod" runat="server" />&nbsp;|
                    Status :
                    <asp:Label ID="lblStatus" runat="server" />&nbsp;|
                    Tgl Buat :
                    <asp:Label ID="lblDateCreated" runat="server" />&nbsp;|
                    Tgl Update :
                    <asp:Label ID="lblLastUpdate" runat="server" />&nbsp;|
                    Diupdate :
                    <asp:Label ID="lblupdatedby" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
            </tr>
            <tr>
                <td class="mt-h" colspan="3">
                    &nbsp;</td>
                <td colspan="2" align="right">
                    <asp:Label ID="lblTracker" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    No.BKM 
                </td>
                <td style="width: 347px">
                    <asp:Label ID="LblIDM" runat="server" Width="100%"></asp:Label></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px">
                </td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Tanggal (hari/bulan/tahun): *</td>
                <td style="width: 347px"> 
                    <asp:TextBox ID="txtWPDate"  CssClass="font9Tahoma" runat="server" MaxLength="10" Width="50%" />
                    <a href="javascript:PopCal('txtWPDate');"><asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                    <asp:Label ID="lblwpdate" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Kategori : *</td>
                <td style="width: 347px">
                    <GG:AutoCompleteDropDownList ID="ddlbkcategory" CssClass="font9Tahoma" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbkcategory_OnSelectedIndexChanged" />
                    <asp:Label ID="lblbkcategory" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Sub Kategori : *</td>
                <td style="width: 347px">
                    <GG:AutoCompleteDropDownList ID="ddlbksubcategory" CssClass="font9Tahoma" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbksubcategory_OnSelectedIndexChanged" />
                    <asp:Label ID="lblbksubcategory" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                </td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td height="25" style="width: 206px">
                    SPK :</td>
                <td style="width: 347px">
				<asp:DropDownList ID="ddlreg_bor" CssClass="font9Tahoma" runat="server" Width="100%" >
                    <asp:ListItem Selected="True" Value="S">SPK</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblregbor" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px"></td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px;">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td height="25" style="width: 206px">
                    Divisi Kerja:*</td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddldivisicode" AutoPostBack="true"  CssClass="font9Tahoma" runat="server" Width="100%"
                        OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" />
                    <asp:Label ID="lbldivisicode" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Supplier :*</td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddlsupplier" AutoPostBack="true" CssClass="font9Tahoma" runat="server" Width="100%"
                        OnSelectedIndexChanged="ddlsupplier_OnSelectedIndexChanged" />
					<asp:Label ID="lblsupplier" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td colspan="2">
                </td>
                
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    POID :*</td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddlpoid" CssClass="font9Tahoma" runat="server" Width="100%"/>
					<asp:Label ID="lblpoid" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                </td>
                <td style="width: 236px">
                 </td>
            </tr>
            
            
            <tr>
                <td height="25" style="width: 206px">
                    <asp:Label ID="Label1" runat="server" Width="100%">Keterangan :</asp:Label></td>
                <td colspan="4">
                    <asp:TextBox ID="txtnotes" CssClass="font9Tahoma" runat="server" MaxLength="128" Width="99%"></asp:TextBox></td>
            </tr>
            
           
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="5" style="height: 26px">
                    <asp:ImageButton ID="NewBtn2" OnClick="BtnNewBK_OnClick" ImageUrl="../../images/butt_new.gif"
                        AlternateText="New" runat="server" />
                    <asp:ImageButton ID="SaveBtn2" OnClick="BtnSaveBK_OnClick" ImageUrl="../../images/butt_save.gif"
                        AlternateText="Save" runat="server" />
                    <asp:ImageButton ID="BackBtn2" AlternateText="  Back  " OnClick="BtnBackBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_back.gif" runat="server" />	
					<asp:ImageButton ID="VerBtn2" AlternateText="  Verified  " OnClick="BtnVerBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_verified.gif" runat="server" />
					<asp:ImageButton ID="ConfBtn2" AlternateText="  Confirm  " OnClick="BtnConfBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_confirm.gif" runat="server" />
					<asp:ImageButton ID="ReActBt" AlternateText="  ReActive   " OnClick="BtnReActBK_onClick"
                         ImageUrl="../../images/butt_reactive.gif" runat="server" />	
                    
                    <br />
                </td>
            </tr>
			
            <tr>
                <td colspan="5" style="height: 36px">
                    <hr style="width :100%" /> 
                </td>
            </tr>
            
            <%--Detail--%> 
            
            <tr>
                <td style="height: 24px;" colspan="5">
                    <igtab:UltraWebTab Visible="false" ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" runat="server">
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
                            <%-- Civil,Umum,Kemanan--%>
                            <igtab:Tab Key="NRW" Text="Pekerjaan" Tooltip="Pekerjaan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgNRW" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" 
                                                    OnItemDataBound="dgNRW_BindGrid" 
                                                    OnDeleteCommand="dgNRW_Delete" 
                                                    Width="100%" ShowFooter=true class="font9Tahoma">
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
													    <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgNRW_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:Label ID="dgNRW_lbl_ed" Text='<%# Container.DataItem("empdet") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgNRW_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgNRW_ddl_jb" Width="270px" OnSelectedIndexChanged="dgNRW_ddl_jb_OnSelectedIndexChanged"
                                                                  CssClass="font9Tahoma"  AutoPostBack="true" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>

                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgNRW_hid_cb" Value='<%# Container.DataItem("codeblok") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgNRW_ddl_cb" Width="90px" CssClass="font9Tahoma" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgNRW_txt_hk" Text='<%# Container.DataItem("jjgjob") %>' Width="30px" style="text-align:right;"  
                                                                   CssClass="font9Tahoma" onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgNRW_lbl_ft_totHa runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>

                                                        <asp:TemplateColumn HeaderText="Hasil Kerja">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgNRW_txt_hs" Text='<%# Container.DataItem("hsljob") %>' Width="50px" style="text-align:right;" 
                                                                   CssClass="font9Tahoma" onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgNRW_lbl_ft_totHs runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgNRW_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px"
                                                                    runat="server" />
                                                              </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn>
                                                             <ItemTemplate>
                                                                <asp:HiddenField ID="dgBHNALL_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                                <asp:HiddenField ID="dgNRW_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                         
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 100%">
											    Divisi:&nbsp;<GG:AutoCompleteDropDownList ID="NRW_src_ddl_divisi" OnSelectedIndexChanged="NRW_src_ddl_divisi_OnSelectedIndexChanged" Width="120px" AutoPostBack=true CssClass="font9Tahoma" runat="server" />&nbsp;
                                                Karyawan :&nbsp;<GG:AutoCompleteDropDownList ID="NRW_src_ddl_emp" Width="200px" CssClass="font9Tahoma" runat="server" />&nbsp;
											    <asp:ImageButton ID="Btn_HKJ_RWT_add" OnClick="NRWT_add_onClick" runat="server" AlternateText="+ Hasil Kerja"  ImageUrl="../../images/butt_add.gif" />
                                                <asp:ImageButton ID="ImageButton8" OnClick="NRW_refresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />
												<asp:ImageButton ID="Btn_NRW_clear" OnClick="NRW_clear_onClick" runat="server" AlternateText="+ Clear list"  ImageUrl="../../images/butt_clear.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                           
                            <%--Bahan--%>
                            <igtab:Tab Key="BHNALL" Text="Pemakaian Bahan" Tooltip="Pemakaian bahan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgBHNALL" runat="server" 
                                                 AutoGenerateColumns="False" 
                                                 GridLines="Both" 
                                                 CellPadding="2" 
                                                 OnItemDataBound="dgBHNALL_BindGrid" 
                                                 OnDeleteCommand="dgBHNALL_Delete" 
                                                 Width="100%"  class="font9Tahoma"
                                                 >
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
													    <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgBHNALL_lbl_ec" Text='<%# Container.DataItem("empdet") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:label ID="dgBHNALL_lbl_jb" Width="120px" Text='<%# Container.DataItem("job") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgNRW_hid_cb" Value='<%# Container.DataItem("codeblok") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgNRW_ddl_cb" Width="90px" CssClass="font9Tahoma" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Bahan">
                                                            <ItemTemplate>
                                                                  <GG:AutoCompleteDropDownList ID="dgBHNALL_ddl_itm" Width="250px" OnSelectedIndexChanged="dgBHNALL_ddl_itm_OnSelectedIndexChanged" AutoPostBack=true CssClass="font9Tahoma" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBHNALL_txt_Qty" Text='<%# Container.DataItem("Qty") %>' Width="50px" style="text-align:right;" 
                                                                  CssClass="font9Tahoma"  onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgBHNALL_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px"
                                                                    runat="server" />
                                                                     </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                               <asp:HiddenField ID="dgBHNALL_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                               <asp:HiddenField ID="dgBHNALL_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                               <asp:HiddenField ID="dgBHNALL_hid_cb" Value='<%# Container.DataItem("codeblok") %>' runat="Server" />
                                                               <asp:HiddenField ID="dgBHNALL_hid_itm" Value='<%# Container.DataItem("codeitem") %>' runat="Server" />
                                                               <asp:HiddenField ID="dgBHNALL_hid_uom" Value='<%# Container.DataItem("uom") %>' runat="Server" />
                                                             
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 100%">
                                            <GG:AutoCompleteDropDownList ID="dgBHNALL_idd" Width="500px" CssClass="font9Tahoma" runat="server" />
                                            <asp:ImageButton ID="ImageButton6" OnClick="BHNALL_add_onClick" runat="server" AlternateText="+ Pemakaian Bahan" ImageUrl="../../images/butt_add.gif" />
                                            <asp:ImageButton ID="ImageButton13" OnClick="BHNALL_refresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />
                                        
                                            </td>
                                             <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                           
                            </Tabs>
                    </igtab:UltraWebTab>
                </td>
            </tr>
             <tr>
                <td colspan="5">
                    <hr style="width :100%" /> 
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 26px">
                    <asp:ImageButton ID="NewBtn" OnClick="BtnNewBK_OnClick" ImageUrl="../../images/butt_new.gif"
                        AlternateText="New" runat="server" />
                    <asp:ImageButton ID="SaveBtn" OnClick="BtnSaveBK_OnClick" ImageUrl="../../images/butt_save.gif"
                        AlternateText="Save" runat="server" />
                    <asp:ImageButton ID="BackBtn" AlternateText="  Back  " OnClick="BtnBackBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_back.gif" runat="server" />
					<asp:ImageButton ID="VerBtn" AlternateText="  Verified  " OnClick="BtnVerBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_verified.gif" runat="server" />
					<asp:ImageButton ID="ConfBtn" AlternateText="  Confirm  " OnClick="BtnConfBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_confirm.gif" runat="server" />		
                    <br />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp;<input type="hidden" id="isNew" value="" runat="server" />
        <input type="hidden" id="hid_cat" value="" runat="server" />
        <input type="hidden" id="hid_subcat" value="" runat="server" />
        <input type="hidden" id="hid_div" value="" runat="server" />
        <input type="hidden" id="txt_hid_emp" value="" runat="server" />
        <input type="hidden" id="hid_status" value="" runat="server" />

      </div>
      </td>
      </tr>
      </table>            
    </form>
</body>
</html>
