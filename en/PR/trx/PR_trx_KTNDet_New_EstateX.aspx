<%@ Page Language="vb" Src="../../../include/PR_trx_KTNDet_New_Estate.aspx.vb" Inherits="PR_KTNDet_New_Estate" %>
<%@ Register TagPrefix="UserControl" TagName="MenuPRTrx" Src="../../menu/menu_prtrx.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>BUKU KERJA KONTANAN DETAIL</title>
</head>
<body>
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <form id="frmMain" runat="server">
        <asp:Label ID="lblErrMessage" Visible="false" Text="" ForeColor="red" runat="server" />
        <table border="0" cellspacing="1" cellpadding="1" width="99%">
            <tr>
                <td colspan="6">
                    <UserControl:MenuPRTrx ID="MenuPRTrx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="mt-h" colspan="3">
                    DETAIL BUKU KERJA KONTANAN </td>
                <td colspan="3" align="right">
                    <asp:Label ID="lblTracker" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr size="1" noshade>
                </td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    NO. 
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
                    <asp:TextBox ID="txtWPDate" runat="server" MaxLength="10" Width="50%" />
                    <a href="javascript:PopCal('txtWPDate');"><asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                    <asp:Label ID="lblwpdate" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Periode :
                </td>
                <td style="width: 236px">
                    <asp:Label ID="lblPeriod" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Kategori : *</td>
                <td style="width: 347px">
                    <GG:AutoCompleteDropDownList ID="ddlbkcategory" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbkcategory_OnSelectedIndexChanged" />
                    <asp:Label ID="lblbkcategory" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Status :
                </td>
                <td style="width: 236px">
                    <asp:Label ID="lblStatus" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Sub Kategori : *</td>
                <td style="width: 347px">
                    <GG:AutoCompleteDropDownList ID="ddlbksubcategory" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbksubcategory_OnSelectedIndexChanged" />
                    <asp:Label ID="lblbksubcategory" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                </td>
                <td style="width: 192px">
                    Tgl Buat :</td>
                <td style="width: 236px">
                    <asp:Label ID="lblDateCreated" runat="server" /></td>
            </tr>
            
            <tr>
                <td height="25" style="width: 206px">
                    Regular/Borongan</td>
                <td style="width: 347px"><GG:AutoCompleteDropDownList ID="ddlreg_bor" runat="server" Width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbkregbor_OnSelectedIndexChanged" >
                    <asp:ListItem Selected="True" Value="R">Regular</asp:ListItem>
                    <asp:ListItem Value="B">Borongan</asp:ListItem>
                </GG:AutoCompleteDropDownList>
                <asp:Label ID="lblregbor" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px"></td>
                <td style="width: 192px">
                    Tgl Update :</td>
                <td style="width: 236px;">
                    <asp:Label ID="lblLastUpdate" runat="server" /></td>
            </tr>
            
            <tr>
                <td height="25" style="width: 206px">
                    Divisi Kerja:*</td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddldivisicode" AutoPostBack="true" runat="server" Width="100%"
                        OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" />
                    <asp:Label ID="lbldivisicode" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Diupdate :
                </td>
                <td style="width: 236px">
                    <asp:Label ID="lblupdatedby" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    <asp:Label ID="lbltxtmandor" runat="server" Width="100%" >Mandor :*</asp:Label></td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddlMandorCode" AutoPostBack="true" runat="server" Width="100%"
                        OnSelectedIndexChanged="ddlMandorCode_OnSelectedIndexChanged" />
                    <asp:DropDownList ID="ddlkcscode" runat="server" Width="100%" Visible="False" />
					<asp:DropDownList ID="ddltrncode" runat="server" Width="100%" Visible="False" />
					<asp:Label ID="lblMandorCode" Visible="false" runat="server" Width="100%" />
                    <asp:Label ID="lblkcscode" Visible="false" runat="server" Width="100%" />
                    <asp:Label ID="lblnopolisi" Visible="false" runat="server" Width="100%" />
                    <asp:Label ID="lblsupir" Visible="false" runat="server" Width="100%" />
				     <div id="divnopol" visible="False" runat="server">
                    <table border="0" cellspacing="1" cellpadding="1" width="100%">
                     <tr>
                        <td style="width: 30%">No.Polisi :</td><td style="width: 70%"><asp:TextBox ID="txtnopolisi" runat="server" Width="100%" Visible=false/></td>
                     </tr>
                     </table>
                    </div>     
               	
                    <div id="divtrx" visible="False" runat="server">
                    <table border="0" cellspacing="1" cellpadding="1" width="100%">
                     <tr>
                        <td style="width: 30%">Supir/Kenek :</td><td style="width: 70%"><asp:DropDownList ID="ddlsupir" runat="server" Width="100%"/></td>
                     </tr>
                     </table>
                    </div>     
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                </td>
                <td style="width: 236px;">
                    &nbsp;</td>
            </tr>
            
            
            
            <tr>
                <td height="25" style="width: 206px">
                    <asp:Label ID="Label1" runat="server" Width="100%">Keterangan :</asp:Label></td>
                <td colspan="5">
                    <asp:TextBox ID="txtnotes" runat="server" MaxLength="128" Width="99%"></asp:TextBox></td>
            </tr>
            
           
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="5" style="height: 26px">
                    <asp:ImageButton ID="ImageButton2" OnClick="BtnNewBK_OnClick" ImageUrl="../../images/butt_new.gif"
                        AlternateText="New" runat="server" />
                    <asp:ImageButton ID="SaveBtn2" OnClick="BtnSaveBK_OnClick" ImageUrl="../../images/butt_save.gif"
                        AlternateText="Save" runat="server" />
                    <asp:ImageButton ID="ImageButton4" AlternateText="  Back  " OnClick="BtnBackBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_back.gif" runat="server" />
                </td>
            </tr>
           
            <tr>
                <td colspan="6" style="height: 36px">
                    <hr size="1" noshade>
                </td>
            </tr>
            
            <%--Detail--%> 
            
            <tr>
                <td style="height: 24px;" colspan="5">
                    <igtab:UltraWebTab Visible="false" ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" runat="server">
                        <DefaultTabStyle  Height="22px">
                        </DefaultTabStyle>
                        <HoverTabStyle CssClass="ContentTabHover">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>
                        <Tabs>
                           
                            <%--Panen--%>
                             <igtab:Tab Key="PN" Text="Panen" Tooltip="Panen">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgPN" runat="server" 
                                                     AutoGenerateColumns="False" 
                                                    OnItemDataBound="dgPN_BindGrid" 
                                                    OnDeleteCommand="dgPN_Delete"  
                                                    GridLines="Both" CellPadding="2" PageSize="6" ShowFooter=true
                                                    Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Nama">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgPN_lbl_ed" Width="120px" Text='<%# Container.DataItem("empdet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="dgPN_ddl_jb"  Width="90px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                                                                               
                                                        <asp:TemplateColumn HeaderText="Sub Pekerjaan" >
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="dgPN_ddl_subsubcat"  Width="90px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgPN_hid_cb" Value='<%# Container.DataItem("codeblok") %>'
                                                                    runat="Server" />
                                                                <asp:DropDownList ID="dgPN_ddl_cb" Width="150px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                         <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_hk" Width="30px" Text='<%# Container.DataItem("hkjob")%>' style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="3%" />
															<FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totHk runat=server />
									                        </FooterTemplate>
									                         <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                                                                                
                                                        <asp:TemplateColumn HeaderText="JJG" Visible=false>
                                                            <ItemTemplate >
                                                                <asp:TextBox ID="dgPN_txt_jg"  Text='<%# Container.DataItem("jjg") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)"  Width="50px" runat="server"  />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totHs runat=server />
									                        </FooterTemplate>
									                         <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                      
                                                      <asp:TemplateColumn HeaderText="BJR">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_bjr"  CssClass="mr-h" Text='<%# Container.DataItem("bjr") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)"   Width="35px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Hasil">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_hs" Text='<%# Container.DataItem("hsljob") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totHs runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                         <asp:TemplateColumn HeaderText="KKK(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_drc" Text='<%# Container.DataItem("drs_drc") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                          </asp:TemplateColumn>
                                                        
                                                         <asp:TemplateColumn HeaderText="KKK(Kg)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_kkk" CssClass="mr-h" Text='<%# Container.DataItem("drs_kkk") %>' ReadOnly=true style="text-align:right;" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totkkk runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="SLAB">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_slab" Text='<%# Container.DataItem("drs_slab") %>' style="text-align:right;" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totslab runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="LM">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_lm" Text='<%# Container.DataItem("drs_lm") %>' style="text-align:right;" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totlm runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                        
                                                        <asp:TemplateColumn HeaderText="GT">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_gt" Text='<%# Container.DataItem("drs_gt") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgPN_lbl_ft_totgt runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="TPH">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_tph" Text='<%# Container.DataItem("tph") %>' style="text-align:right;" 
                                                                Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="No.Karcis">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_karcis" Text='<%# Container.DataItem("karcis") %>' style="text-align:right;" 
                                                                Width="80px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                                                                                                                  
                                                         <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgPN_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px"
                                                                    runat="server" />
                                                             </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateColumn>
                                                                                                            
                                                        <asp:TemplateColumn HeaderText="Rot">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_rot" Text='<%# Container.DataItem("rotasi") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)"  Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Denda" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_txt_dnd"  Width="50px" Text='<%# Container.DataItem("denda") %>' style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Buah Mentah" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_PB_Dmentah"  Text='<%# Container.DataItem("pb_dnd_mentah") %>'
                                                                onkeypress="javascript:return isNumberKey(event)"  Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Buah Tinggal" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_PB_Dtinggal"  Text='<%# Container.DataItem("pb_dnd_tinggal") %>'
                                                                 onkeypress="javascript:return isNumberKey(event)" Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="B.Tdk TPH" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_PB_Dtph"  Text='<%# Container.DataItem("pb_dnd_tph") %>'
                                                                  onkeypress="javascript:return isNumberKey(event)" Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Tngkai. Panjang" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_PB_Dpjg"  Text='<%# Container.DataItem("pb_dnd_pjng") %>'
                                                                 onkeypress="javascript:return isNumberKey(event)" Width="20px"  runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Plpah Sengkleh" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_PB_DSengkleh"  Text='<%# Container.DataItem("pb_dnd_sengkleh") %>'
                                                                  onkeypress="javascript:return isNumberKey(event)" Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Non Basis" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_PB_Dbasis"  Text='<%# Container.DataItem("pb_dnd_basis") %>' style="text-align:right;" 
                                                                 onkeypress="javascript:return isNumberKey(event)"  Width="30px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                     
                                                        </asp:TemplateColumn>
                                                             <asp:TemplateColumn HeaderText="Kutip Kotor" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPN_KB_Dkk"  Text='<%# Container.DataItem("kb_dnd_ktpktr") %>' Width="50px" style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Brndl Kotor" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPN_KB_Dbk"  Checked='<%# Cbool(Container.DataItem("kb_dnd_brdktr")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="B.Tdk TPH" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPN_KB_Dtph"  Checked='<%# Cbool(Container.DataItem("kb_dnd_tph")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                   
                                                        <asp:TemplateColumn>
                                                             <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                                <asp:HiddenField ID="dgPN_hid_subsubcat" Value='<%# Container.DataItem("idsubsubcat") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPN_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPN_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPN_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPN_hid_jbnm" Value='<%# Container.DataItem("job") %>' runat="Server" />
                                                                
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 100%">
                                                Divisi:&nbsp;<GG:AutoCompleteDropDownList ID="PN_src_ddl_divisi" OnSelectedIndexChanged="PN_src_ddl_divisi_OnSelectedIndexChanged" Width="120px" AutoPostBack=true runat="server" />&nbsp;
                                                Karyawan :&nbsp;<GG:AutoCompleteDropDownList ID="PN_src_ddl_emp" Width="200px" runat="server" />&nbsp;
                                                <asp:TextBox ID="PN_add_txt" Width="20px" MaxLength=1 onkeypress="javascript:return isNumberKey(event)" runat="server" /> 
                                                <asp:ImageButton ID="ImageButton11" OnClick="PN_add_onClick" runat="server" AlternateText="+ Pemanen" ImageUrl="../../images/butt_add.gif" />
                                                <asp:ImageButton ID="ImageButton12" OnClick="PN_refresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />
                                                <asp:ImageButton ID="Pn_btnclear" OnClick="PN_clear_onClick" runat="server" AlternateText="+ Clear list"  ImageUrl="../../images/butt_clear.gif" />
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
                                                  
	
                            <%--Borongan Pekerja--%>
                             <igtab:Tab Key="BOR" Text="Pekerjaan/Karyawan Borongan" Tooltip="Pekerjaan/Karyawan Borongan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgBOR" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" 
                                                    OnItemDataBound="dgBOR_BindGrid" 
                                                    OnDeleteCommand="dgBOR_Delete" Width="100%" 
                                                    ShowFooter=true>
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Nama">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgBOR_lbl_ed" Width="100px" Text='<%# Container.DataItem("EmpDet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgBOR_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgBOR_ddl_jb" Width="130px" runat="server" OnSelectedIndexChanged="dgBOR_ddl_jb_OnSelectedIndexChanged" AutoPostBack=true />
                                                           </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgBOR_hid_cb" Value='<%# Container.DataItem("codeblok") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgBOR_ddl_cb" Width="150px" runat="server" />
                                                              </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
														
														  <asp:TemplateColumn HeaderText="JJG" >
                                                            <ItemTemplate >
                                                                <asp:TextBox ID="dgBOR_txt_jjg"  Text='<%# Container.DataItem("jjg") %>' style="text-align:right;" 
                                                                onkeypress="javascript:return isNumberKey(event)"  Width="50px" runat="server"  />
                                                            </ItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgBOR_lbl_ft_totjjg runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>
                                                      
                                                      <asp:TemplateColumn HeaderText="BJR">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_txt_bjr"  CssClass="mr-h" Text='<%# Container.DataItem("bjr") %>' style="text-align:right;" 
                                                                    ReadOnly="false" Width="35px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                        </asp:TemplateColumn>
                                                                                                              
                                                        <asp:TemplateColumn HeaderText="Hasil">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_txt_hs" Text='<%# Container.DataItem("hsljob") %>' Width="50px" style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                             <FooterTemplate >
									                                <asp:Label ID=dgBOR_lbl_ft_totHs runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        
														 <asp:TemplateColumn HeaderText="TPH">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_txt_tph" Text='<%# Container.DataItem("tph") %>' style="text-align:right;" 
                                                                Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="No.Karcis">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_txt_karcis" Text='<%# Container.DataItem("karcis") %>' style="text-align:right;" 
                                                                Width="80px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
														
                                                         <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgBOR_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px" runat="server" />
                                                                 </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Rot">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_txt_rot" Text='<%# Container.DataItem("rotasi") %>' Width="20px" style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                      
                                                        <asp:TemplateColumn HeaderText="Denda lain" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_txt_dnd" Text='<%# Container.DataItem("denda") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        
                                                       <asp:TemplateColumn HeaderText="Kutip Kotor" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBOR_KB_Dkk"  Text='<%# Container.DataItem("kb_dnd_ktpktr") %>' Width="50px" style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Brndl Kotor" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgBOR_KB_Dbk"  Checked='<%# Cbool(Container.DataItem("kb_dnd_brdktr")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="B.Tdk TPH" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgBOR_KB_Dtph"  Checked='<%# Cbool(Container.DataItem("kb_dnd_tph")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                   
                                                   
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                                <asp:HiddenField ID="dgBOR_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgBOR_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
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
                                                Divisi:&nbsp;<GG:AutoCompleteDropDownList ID="BOR_src_ddl_divisi" OnSelectedIndexChanged="BOR_src_ddl_divisi_OnSelectedIndexChanged" Width="120px" AutoPostBack=true runat="server" />&nbsp;
                                                Karyawan :&nbsp;<GG:AutoCompleteDropDownList ID="BOR_src_ddl_emp" Width="200px" runat="server" />&nbsp;
                                                <asp:TextBox ID="BOR_add_txt" Width="20px" MaxLength=1 onkeypress="javascript:return isNumberKey(event)" runat="server" /> 
                                                <asp:ImageButton ID="ImageButton1" OnClick="BOR_add_onClick" runat="server" AlternateText="+ Karyawan"  ImageUrl="../../images/butt_add.gif" />
                                                <asp:ImageButton ID="ImageButton10"  OnClick="BOR_refresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" />
                                                <asp:ImageButton ID="Bor_btnclear" OnClick="BOR_clear_onClick" runat="server" AlternateText="+ Clear list"  ImageUrl="../../images/butt_clear.gif" />
                                            
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
                    <hr size="1" noshade>
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
                </td>
            </tr>
        </table>
        &nbsp; &nbsp;<input type="hidden" id="isNew" value="" runat="server" />
        <input type="hidden" id="hid_cat" value="" runat="server" />
        <input type="hidden" id="hid_subcat" value="" runat="server" />
        <input type="hidden" id="hid_div" value="" runat="server" />
        <input type="hidden" id="txt_hid_emp" value="" runat="server" />
        <asp:TextBox Visible=false ID="txt_hid_emp2" runat="server" Width="50%" AutoPostBack =true />
                    
    </form>
</body>
</html>
