<%@ Page Language="vb" Src="../../../include/PR_trx_RKHDet_New_Estate.aspx.vb" Inherits="PR_RKHDet_New_Estate" %>
<%@ Register TagPrefix="UserControl" TagName="MenuPRTrx" Src="../../menu/menu_prtrx.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>RENCANA KERJA HARIAN DETAIL</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">   
        <asp:Label ID="lblErrMessage" Visible="false" Text="" ForeColor="red" runat="server" />
        <table border="0" cellspacing="1" cellpadding="1" width="99%" class="font9Tahoma">
            <tr>
                <td colspan="6">
                    <UserControl:MenuPRTrx ID="MenuPRTrx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="mt-h" colspan="3">
                  <strong>  DETAIL RENCANA KERJA HARIAN</strong></td>
                <td colspan="3" align="right">
                    <asp:Label ID="lblTracker" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr size="1" noshade>
                </td>
            </tr>
            <tr>
                <td height="25" style="width: 15%">
                    No.RKH  
                </td>
                <td style="width: 30%">
                    <asp:Label ID="LblIDM" runat="server" Width="100%"></asp:Label></td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 15%">
                    Periode :
                </td>
                <td style="width: 30%">
                    <asp:Label ID="lblPeriod" runat="server" /></td>
            </tr>
            <tr class="font9Tahoma">
                <td height="25" style="width: 15%">
                    Tanggal (hari/bulan/tahun): *</td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtWPDate" runat="server" MaxLength="10" Width="50%" />
                    <a href="javascript:PopCal('txtWPDate');"><asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                    <asp:Label ID="lblwpdate" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 15%">
                    Status :
                </td>
                <td style="width: 30%">
                    <asp:Label ID="lblStatus" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 15%">
                    Divisi:*</td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddldivisicode" AutoPostBack="true" runat="server" Width="100%"/>
                    <asp:Label ID="lbldivisicode" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 15%">
                    Tgl Buat :</td>
                <td style="width: 30%">
                    <asp:Label ID="lblDateCreated" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 15%">
                    <asp:Label ID="Label1" runat="server" Width="100%">Keterangan :</asp:Label></td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtnotes" runat="server" MaxLength="128" Width="99%"></asp:TextBox></td>
                <td style="width: 10%">
                </td>
                <td style="width: 15%">
                    Tgl Update :</td>
                <td style="width:30%">
                    <asp:Label ID="lblLastUpdate" runat="server" /></td>
            </tr>
            
            <tr>
                <td style="width: 15%; height: 25px;">
                    </td>
                <td style="width: 30%; height: 25px;">
                </td>
                <td style="width: 10%; height: 25px;"></td>
                <td style="width: 15%; height: 25px;">
                    Diupdate :
                </td>
                <td style="width: 30%; height: 25px;">
                    <asp:Label ID="lblupdatedby" runat="server" /></td>
            </tr>
            
           
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="5" style="height: 26px">
                    <asp:ImageButton ID="ImageButton2" OnClick="BtnNewBK_OnClick" ImageUrl="../../images/butt_new.gif"
                        AlternateText="New" runat="server" />
                    <asp:ImageButton ID="SaveBtn" OnClick="BtnSaveBK_OnClick" ImageUrl="../../images/butt_save.gif"
                        AlternateText="Save" runat="server" />
                    <asp:ImageButton ID="ImageButton4" AlternateText="  Back  " OnClick="BtnBackBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_back.gif" runat="server" />
                </td>
            </tr>
           
           <tr> 
               <td colspan="6">
                    <table id="tblSelection" width="100%" cellspacing="0" cellpadding="0" border="0" runat=server>
                    <tr>
                        <td colspan="6" style="height: 36px">
                            <hr size="1" noshade>
                        </td>
                    </tr>
                    
           
                    <tr>
                        <td style="height: 24px;" colspan="5">
                            <igtab:UltraWebTab ID="TABBKTOT" ThreeDEffect="False" TabOrientation="TopLeft"
                                SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" Width=100% runat="server">
                                <DefaultTabStyle ForeColor="transparent" Height="22px" >
                                </DefaultTabStyle>
                                <HoverTabStyle CssClass="ContentTabHover">
                                </HoverTabStyle>
                                <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                                    NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                                    FillStyle="LeftMergedWithCenter"></RoundedImage>
                                <SelectedTabStyle CssClass="ContentTabSelected">
                                </SelectedTabStyle>
                                <Tabs>
                                    <%--Perawatan--%>
                                    <igtab:Tab Key="HKJ_RWT" Text="Pekerjaan" Tooltip="Pekerjaan">
                                        <ContentPane>
                                            <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                                <tr>
                                                   <td width=10%>Kategori</td>
                                                   <td width=15%>Sub Kategori</td>
                                                   <td width=25%>Pekerjaan</td>
                                                   <td width=15%>Blok</td>
                                                   <td width=8%>Rotasi</td>
                                                   <td width=12%>Vol Kerja</td>
                                                   <td width=8%>HK</td>
                                                   
                                                </tr>
                                                <tr>
                                                   <td width=10%><asp:DropDownList ID="ddljobkat" OnSelectedIndexChanged="ddljobkat_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%"/></td>
                                                   <td width=15%><asp:DropDownList ID="ddljobskat" OnSelectedIndexChanged="ddljobskat_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%"/></td>
                                                   <td width=25%><GG:AutoCompleteDropDownList OnSelectedIndexChanged="ddljob_OnSelectedIndexChanged" ID="ddljob" AutoPostBack="true" runat="server" Width="100%"/></td>
                                                   <td width=15%><GG:AutoCompleteDropDownList ID="ddljobblk" runat="server" Width="100%"/></td>
                                                   <td width=8%><asp:TextBox ID="txtjobrot" runat="server" Width="100%"/></td>
                                                   <td width=12%><asp:TextBox ID="txtjobhasil" runat="server" Width="65%" onkeypress="return isNumberKey(event)"></asp:TextBox><asp:Label ID="lbljobuom" runat="server" Width="30%"></asp:label></td>
                                                   <td width=8%><asp:TextBox ID="txtjobhk" runat="server" Width="65%" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td height="25" style="width: 185px">
                                                        <asp:ImageButton ID="Btn_HKJ_RWT_add" OnClick="HKJ_JOB_add_onClick" runat="server" AlternateText="+ Hasil Kerja"
                                                            ImageUrl="../../images/butt_add.gif" />
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
                                         <igtab:TabSeparator>
                                    </igtab:TabSeparator>
                            
                                    <%--Bahan--%>
                                    <igtab:Tab Key="HKJ_BHN" Text="Pemakaian Bahan" Tooltip="Pemakaian bahan">
                                        <ContentPane>
                                            <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                             <tr>
                                                   <td width=25%>Pekerjaan+Blok</td>
                                                   <td width=25%>Item</td>
                                                   <td width=15%>Qty</td>
                                                   <td width=15%>UOM</td>
                                                   <td width=15%></td>
                                                </tr>
                                                <tr>
                                                   <td width=25%><asp:DropDownList ID="ddlbhnjob" OnSelectedIndexChanged="ddlbhnjob_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%"/></td>
                                                   <td width=25%><GG:AutoCompleteDropDownList ID="ddlbhn" OnSelectedIndexChanged="ddlbhn_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%"/></td>
                                                   <td width=15%><asp:TextBox ID="txtbhnqty" runat="server" Width="100%" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                   <td width=15%><asp:Label ID="lblbhnuom" runat="server" Width="100%"></asp:label></td>
                                                   <td width=15%></td>
                                                </tr>
                                                <tr>
                                                    <td height="25" style="width: 185px">
                                                        <asp:ImageButton ID="Btn_HKJ_BHN_add" OnClick="HKJ_BHN_add_onClick" runat="server" AlternateText="+ Pemakaian Bahan"
                                                            ImageUrl="../../images/butt_add.gif" />
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
                        <td colspan="5" align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                            border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent">
                         Pekerjaan
                        </td>
                    </tr>
                        
                    <tr>
                        <td colspan="5">
                             <asp:DataGrid ID="dgjob" runat="server" 
                                     AllowSorting="True" 
                                     AutoGenerateColumns="False"
                                     CellPadding="2" 
                                     GridLines="None" 
                                     PagerStyle-Visible="False" 
                                     Width="100%" 
                                     OnItemDataBound="dgjob_BindGrid" 
                                     OnItemCommand="dgjob_GetItem" 
                                     OnDeleteCommand="dgjob_Delete"  CssClass="font9Tahoma"
                                     >
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
                                        
                                                                      
                                        <asp:TemplateColumn HeaderText="Pekerjaan" SortExpression="job">
                                            <ItemTemplate>
                                            <asp:LinkButton id=dgjoblnk Text='<%# Container.DataItem("job") %>'  runat=server />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                     
                                        <asp:TemplateColumn HeaderText="Blok" >
                                            <ItemTemplate>
                                            <asp:Label ID="dgjobblk" runat="server" Text='<%#Container.DataItem("CodeBlok")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Vol Kerja" >
                                            <ItemTemplate>
                                            <asp:Label ID="dgjobvol" runat="server" Text='<%#Container.DataItem("hasil")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="UOM" >
                                            <ItemTemplate>
                                            <asp:Label ID="dgjobuom" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Rotasi" >
                                            <ItemTemplate>
                                            <asp:Label ID="dgjobrot" runat="server" Text='<%#Container.DataItem("rotasi")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                                                                                                                   
                                         <asp:TemplateColumn HeaderText="HK" >
                                            <ItemTemplate>
                                            <asp:Label ID="dgjobhk" runat="server" Text='<%#Container.DataItem("hk")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        
                                        
                                        <asp:TemplateColumn>					
									         <ItemTemplate>
									         <asp:HiddenField ID="hidjob" Value=<%# Container.DataItem("CodeAloJob") %> runat=server />
									         <asp:HiddenField ID="hidcat" Value=<%# Container.DataItem("CatID") %> runat=server />
										     <asp:HiddenField ID="hidscat" Value=<%# Container.DataItem("SubCatID") %> runat=server />
										     <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
									         </ItemTemplate>
								        </asp:TemplateColumn>
                                                                                                
                                     </Columns>
                                </asp:DataGrid>
                        </td>
                    </tr>
            
                    <tr>
                        <td colspan="5" align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                            border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent">
                         Pemakaian Bahan
                        </td>
                    </tr>
                        
                    <tr>
                        <td colspan="5">
                             <asp:DataGrid ID="dgbhn" runat="server" 
                                     AllowSorting="True" 
                                     AutoGenerateColumns="False"
                                     CellPadding="2" 
                                     GridLines="None" 
                                     PagerStyle-Visible="False" 
                                     Width="100%" 
                                     OnItemDataBound="dgbhn_BindGrid" 
                                     OnItemCommand="dgbhn_GetItem"   CssClass="font9Tahoma"
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
                                                                            
                                                                      
                                        <asp:TemplateColumn HeaderText="Pekerjaan + Bahan">
                                            <ItemTemplate>
                                            <asp:LinkButton id=lnkbhn Text='<%# Container.DataItem("job") %>'  runat=server />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                     
                                        <asp:TemplateColumn HeaderText="Item" >
                                            <ItemTemplate>
                                                <%#Container.DataItem("item")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Qty" >
                                            <ItemTemplate>
                                               <asp:Label ID="dgbhnqty" runat="server" Text='<%#Container.DataItem("qty")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="UOM" >
                                            <ItemTemplate>
                                            <asp:Label ID="dgbhnuom" runat="server" Text='<%#Container.DataItem("uom")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn>					
									         <ItemTemplate>
									         <asp:HiddenField ID="hidbhnjob" Value=<%# Container.DataItem("CodeJob2") %> runat=server />
									         <asp:HiddenField ID="hidbhnblk" Value=<%# Container.DataItem("CodeBlok") %> runat=server />
										     <asp:HiddenField ID="hidbhnitm" Value=<%# Container.DataItem("CodeItem") %> runat=server />
										     <asp:LinkButton id="bhnDelete" CommandName="Delete" Text="Delete" runat="server"/>
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
        &nbsp; &nbsp;<input type="hidden" id="isNew" value="" runat="server" />
        &nbsp;
        <input type="hidden" id="hid_div" value="" runat="server" />
        &nbsp;
    </div>
    </td>
    </tr>
    </table>                
    </form>
</body>
</html>
