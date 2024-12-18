<%@ Page Language="vb" trace=false codefile="../../../include/WM_WM_trx_WeighBridgeTicketList.aspx.vb" Inherits="WM_WeighBridgeTicketList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_wmtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>    
    
<html>
	<head>
		<title>Weighing Management - WeighBridge Ticket List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body onsubmit="ShowLoading()">
	        <form id=frmMain class="main-modul-bg-app-list-pu" runat=server >
            
                <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
                    <tr>
                        <td style="width: 100%; height: 800px" valign="top">
                            <div class="kontenlist">
                                <asp:Label id=SortExpression visible=false runat=server />
                                <asp:Label id=SortCol visible=false runat=server />
                                <asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
                                <table border=0 cellspacing=1 cellpadding=1 width=100%>
                                    <tr>
                                        <td colspan=6><UserControl:MenuWMTrx id=MenuWMTrx runat=server /></td>
                                    </tr>
                                    <tr>
                                        <td class="font9Tahoma" colspan="3"><strong> WEIGHBRIDGE TICKET LIST</strong></td>
                                        <td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
                                    </tr>
                                    <tr>
                                        <td colspan=6><hr style="width :100%" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan=6 width=100% class="font9Tahoma">
                                            <table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
                                                <tr style="background-color:#FFCC00">
                                                    <td width="10%" valign=bottom>Location :<BR><asp:DropDownList id="srchlocation" width=100% runat=server class="font9Tahoma" /></td>	
                                                    <td width="15%" valign=bottom>Supplier/Customer :<BR>
                                                        <telerik:RadComboBox   CssClass="fontObject" ID="radcmbCust"
                                                            Runat="server" AllowCustomText="True" 
                                                            EmptyMessage="Plese Select Customer " Height="200" Width="100%" 
                                                            ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                                                            EnableVirtualScrolling="True">
                                                            <CollapseAnimation Type="InQuart" />
                                                        </telerik:RadComboBox>	
                                                    </td>
                                                    <td width="15%" valign=bottom>Date From :<BR>
                                                        <telerik:RadDatePicker ID="srchDateIn" Runat="server" Culture="en-US"> 
                                                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                                            <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        </telerik:RadDatePicker>                                                        
                                                        <asp:Label id=lblErrDateInMsg visible=false text="<br>Date Format should be in " forecolor=red runat=server/>						
                                                        <asp:Label id=lblErrDateIn forecolor=red visible=false runat=server/>
                                                    </td>
                                                    <td width="15%" valign=bottom>Date To :<BR>
                                                        <telerik:RadDatePicker ID="srchDateInTo" Runat="server" Culture="en-US"> 
                                                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                                            <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        </telerik:RadDatePicker>                                                                                                                                                                        
                                                        <asp:Label id=lblErrDateInMsgTo visible=false text="<br>Date Format should be in " forecolor=red runat=server/>						
                                                        <asp:Label id=lblErrDateInTo forecolor=red visible=false runat=server/>
                                                    </td>	                                								
                                                    <td width=10% valign=bottom>Period :<BR>
                                                        <asp:DropDownList id="ddlMonth" width=100% runat=server>
                                                            <asp:ListItem value="0" Selected>All</asp:ListItem>
                                                            <asp:ListItem value="1">1</asp:ListItem>
                                                            <asp:ListItem value="2">2</asp:ListItem>										
                                                            <asp:ListItem value="3">3</asp:ListItem>
                                                            <asp:ListItem value="4">4</asp:ListItem>
                                                            <asp:ListItem value="5">5</asp:ListItem>
                                                            <asp:ListItem value="6">6</asp:ListItem>
                                                            <asp:ListItem value="7">7</asp:ListItem>
                                                            <asp:ListItem value="8">8</asp:ListItem>
                                                            <asp:ListItem value="9">9</asp:ListItem>
                                                            <asp:ListItem value="10">10</asp:ListItem>
                                                            <asp:ListItem value="11">11</asp:ListItem>
                                                            <asp:ListItem value="12">12</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <td width=10% valign=bottom><BR>
                                                        <asp:DropDownList id="ddlyear" width=100% runat=server>
                                                        </asp:DropDownList>
                                                    <td width="10%" valign=bottom>	Status :<BR>
                                                        <asp:DropDownList id="ddlStatusGen" width=100% runat=server>
                                                            <asp:ListItem value="" Selected>All</asp:ListItem>
                                                            <asp:ListItem value="0">UnCheck</asp:ListItem>
                                                            <asp:ListItem value="1">Check</asp:ListItem>										
                                                            <asp:ListItem value="2">Invoice</asp:ListItem>
                                                            <asp:ListItem value="3">Paid</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="15%"valign=bottom align=right><asp:Button id=SearchBtn Text="Generate" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            
                                <table border=0 cellspacing=1 cellpadding=1 width=100%>
                                    <tr>
                                        <td style="height: 24px;" colspan="5">
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
                                                    <igtab:Tab Key="TICKET" Text="WB TICKET" Tooltip="WB TICKET">
                                                        <ContentPane>
                                                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <div id="div1" style="height:520px;width:1100;overflow:auto;">	                                                                            
                                                                            <asp:DataGrid ID="dgTicketList" runat="server" AutoGenerateColumns="False" GridLines="Both" 
                                                                                CellPadding="2" OnItemDataBound="dgTicketList_BindGrid" Width="300%">
                                                                                <AlternatingItemStyle CssClass="mr-r" />
                                                                                <ItemStyle CssClass="mr-l" />
                                                                                <HeaderStyle CssClass="mr-h" />
                                                                                <Columns>
                                                                                    
                                                                                    <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                       <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkisInvoice" runat="server" />
                                                                                            <asp:Label Text='<%# Container.DataItem("IsVerify") %>' id="lblIsVerify" Visible=false runat="server" />
                                                                                            <asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblVerifyInfo" Visible="False" runat="server" />
                                                                                            <asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblNoUrut"  Visible="false" runat="server" />
                                                                                            
                                                                                        </ItemTemplate>
                                                           
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Supplier/Customer" HeaderStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%#Container.DataItem("NamaSupplier")%>' id="lblNamaSpl" runat="server" />
                                                                                            <asp:Label Text='<%# Container.DataItem("KodeSupplier") %>' id="lblKodeSpl" Visible=false runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Product" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>' id="lblProd" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Tanggal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%#objGlobal.GetLongDate(Container.DataItem("TglMasuk"))%>' id="lblTglMsuk" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Jam Masuk" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%#Format(Container.DataItem("JamMasuk"), "HH:mm:ss")%>' id="lblJamMasuk" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Jam Keluar" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%#Format(Container.DataItem("JamKeluar"), "HH:mm:ss")%>' id="lblJamKeluar" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="No. Polisi" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("NoPolisi")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Kapal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("NmKapal")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Supplier Ref" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("SupRefNo")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>                                                            
                                                                                    <asp:TemplateColumn HeaderText="No. Tiket" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("KodeSlipTimbangan")%>
                                                                                            <asp:Label ID="lblTicketNo" Visible="False" Text='<%# Container.DataItem("KodeSlipTimbangan") %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="No. Surat Pengantar" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("NoSuratPengantar")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="No. Kontrak" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNoKontrak" Text='<%# Container.DataItem("NoKontrak") %>' Visible=False runat="server" />
                                                                                            <asp:Label ID="lblNoKontrakWB" Text='<%# Container.DataItem("NoKontrakWB") %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Pricing" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("PricingMtd")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Group Buah" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItem("KomidelKode")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Bruto (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label1" Text='<%# FormatNumber(Container.DataItem("Bruto"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Tarra (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label2" Text='<%# FormatNumber(Container.DataItem("Tarra"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Gross (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNetto1" Text='<%# FormatNumber(Container.DataItem("NetWeight1"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>                                                                
                                                                                    </asp:TemplateColumn>    
                                                                                    <asp:TemplateColumn HeaderText="WJB" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPotJK" Text='<%# FormatNumber(Container.DataItem("Pot_WajibKg"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>

                                                                                    <asp:TemplateColumn HeaderText="Air" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPotAir" Text='<%# FormatNumber(Container.DataItem("Pot_AirKg"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>

                                                                                    <asp:TemplateColumn HeaderText="Brondol" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPotBrd" Text='<%# FormatNumber(Container.DataItem("Pot_BJRKecil"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>

                                                                                    <asp:TemplateColumn HeaderText="Tot Pot (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPotWB" Text='<%# FormatNumber(Container.DataItem("Potongan"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>

                                                                                    <asp:TemplateColumn HeaderText="Tot Pot (%)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPotTotal" Text='<%# FormatNumber(Container.DataItem("PotTotal"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>

                                                                                    <asp:TemplateColumn HeaderText="QTY JANJANG" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label16" Text='<%# FormatNumber(Container.DataItem("JanjangTotal"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>

                                                                                    <asp:TemplateColumn HeaderText="Buah <br> Menginap" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  >
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkIsInap" runat="server" Visible="false"   Enabled="false" />
                                                                                            <asp:Label ID="lblIsBuahInap" Visible="false" Text='<%# Container.DataItem("IsBuahInap") %>' runat="server" />
                                                                                            <asp:Label ID="lblDescBuahInap" Visible="true" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                
                                                                                    <asp:TemplateColumn HeaderText="Hrg/Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblHrgBB" Text='<%# Container.DataItem("HargaBuahBesar") %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                
                                                                                    <asp:TemplateColumn HeaderText="Netto (Kg)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label18" Text='<%# FormatNumber(Container.DataItem("KGBuahBesar"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                
                                                                                    <asp:TemplateColumn HeaderText="Nominal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label19" Text='<%# FormatNumber(Container.DataItem("KGBuahBesarDiBayar"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>        

                                                                                    <asp:TemplateColumn HeaderText="Tambahan <br>Harga " HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("TambahanHarga"), 2) %>' id="lblTambahanHarga" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>                                                            

                                                                                    <asp:TemplateColumn HeaderText="DPP (Rp)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDPP" Text='<%# FormatNumber(Container.DataItem("DPP"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Rate (%)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRate" Text='<%# FormatNumber(Container.DataItem("RatePPH"), 2) %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                    </asp:TemplateColumn>                                                            
                                                                                    <asp:TemplateColumn HeaderText="PPH (Rp)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPPH" Text='<%# FormatNumber(Container.DataItem("PPH"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>                                                           
                                                                                    </asp:TemplateColumn>    
                                                                                    <asp:TemplateColumn HeaderText="NOMINAL DIBAYAR" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNomDibayar" Text='<%# FormatNumber(Container.DataItem("NominalDibayar"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>                                                           
                                                                                    </asp:TemplateColumn>   

                                                                                    <asp:TemplateColumn HeaderText="Hrg/Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblKGOB" Text='<%# FormatNumber(Container.DataItem("KGOngkosBongkar"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Trip" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTripOB" Text='<%# FormatNumber(Container.DataItem("TripOngkosBongkar"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Total" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTotOB" Text='<%# FormatNumber(Container.DataItem("TotalOngkosBongkar"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Hrg/Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblKGOL" Text='<%# FormatNumber(Container.DataItem("KGOngkosLapangan"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Trip" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTripOL" Text='<%# FormatNumber(Container.DataItem("TripOngkosLapangan"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Total" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTotOL" Text='<%# FormatNumber(Container.DataItem("TotalOngkosLapangan"), 2) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Invoice ID" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblInvoiceID" Text='<%# Container.DataItem("KodeSliptTBS") %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Realisasi Bayar TBS" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label33" Text='<%# Container.DataItem("PaymentID") %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align=right colspan="6">
                                                                            <asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
                                                                            <asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
                                                                            <asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=5>                                                                                     
                                                                            <asp:CheckBox ID="chkisAll" CssClass="font9Tahoma" Text="Check ALL" OnCheckedChanged="CheckAll" AutoPostBack="true" Font-Bold="true" runat="server" />
                                                                                        
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=5>                                                                                                                                                                                                
                                                                        <asp:Button id="btnSavInv" Text="SAVE" OnClick="btnSaveInv_Click" CssClass="button-small" runat="server" />
                                                                        
                                                                    </td>
                                                                </tr>  
                                                                <tr>
                                                                    <td colspan=5>&nbsp;</td>
                                                                </tr>                                                                                                                              
                                                                <tr>
                                                                    <td colspan="3">
                                                                        
                                                                        <asp:CheckBox id="cbExcelTicket" text=" Export To Excel" checked="false" Visible=false class="font9Tahoma" runat="server" />
                                                                        <asp:ImageButton id="TicketPrintPrev" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnTicketPrintPrev_Click" runat="server" />
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=5>&nbsp;</td>
                                                                </tr>
          
                                                                <tr>
                                                                    <td colspan="3">
                                                                    <asp:Label id=lblErrRefresh ForeColor=red Font-Italic=true visible=false runat=server />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentPane>
                                                    </igtab:Tab>
                                                    
                                                    <%--WB PPH--%>
                                                    <igtab:Tab Key="PPH" Text="WB SUMMARY & TAX" Tooltip="WB SUMMARY & TAX">
                                                        <ContentPane>
                                                            <table border="0" cellspacing="1" cellpadding="1" width="99%" class="font9Tahoma">
                                                                   <tr>
                                                                    <td colspan="5">
                                                                        <div id="div2" style="height:500px;width:1100;overflow:auto;">		                                                                            		
                                                                            <asp:DataGrid ID="dgPPHList" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                                CellPadding="2" OnItemDataBound="dgPPHList_BindGrid" Width="150%">
                                                                                <AlternatingItemStyle CssClass="mr-r" />
                                                                                <ItemStyle CssClass="mr-l" />
                                                                                <HeaderStyle CssClass="mr-h" />
                                                                                <Columns>
                                                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblNoUrut" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Tanggal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center >
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# objGlobal.GetLongDate(Container.DataItem("TglMasuk")) %>' id="lblTglMasuk" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Nama" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("NamaSupplier") %>' id="lblNamaSpl" runat="server" />
                                                                                            <asp:Label Text='<%# Container.DataItem("KodeSupplier") %>' id="lblKodeSpl" Visible=false runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Kategori" HeaderStyle-HorizontalAlign=Center >
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("StatusSupplier") %>' id="lblStatusSpl" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="NPWP" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("NPWP") %>' id="lblNPWP" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("PKSNetWeight"), 0) %>' id="lblPKSNetWeight" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("KGFFB"), 0) %>' id="lblKGBuah" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Rp" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("KGDiBayar"), 0) %>' id="lblKGDibayar" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Tambahan <br>Harga" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("TambahanHarga"), 0) %>' id="lblTambahanHarga" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                
                                                                                    <asp:TemplateColumn HeaderText="DPP (Rp)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("DPP"), 0) %>' id="lblDPPAmountPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Rate (%)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("RatePPH"), 2) %>' id="lblRatePPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>                                                            
                                                                                    <asp:TemplateColumn HeaderText="PPH (Rp)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("PPH"), 0) %>' id="lblPPHAmountPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="NOMINAL <Br> DIBAYAR (Rp)" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("RpDibayar"), 0) %>' id="lblAmountDibayar" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>                                                            
                                                                                    <asp:TemplateColumn HeaderText="Hrg/Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("KGOngkosBongkar"), 0) %>' id="lblOBKGPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Trip" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("TripOngkosBongkar"), 0) %>' id="lblOBTripPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Total" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("TotalOngkosBongkar"), 0) %>' id="lblOBTotalPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Hrg/Kg" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("KGOngkosLapangan"), 0) %>' id="lblOLKGPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Trip" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("TripOngkosLapangan"), 0) %>' id="lblOLTripPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Total" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# FormatNumber(Container.DataItem("TotalOngkosLapangan"), 0) %>' id="lblOLTotalPPH" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="No. Jurnal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("JournalID") %>' id="lblNoJurnal" runat="server" />
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
                                                                    <asp:CheckBox id="cbExcelPPH" text=" Export To Excel" checked="false" Visible=false runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=5>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=5>
                                                                        <asp:ImageButton id="btnGenerate" Visible=false ToolTip="Generate journal" UseSubmitBehavior="false" OnClick="btnGenerate_Click"  runat="server" ImageUrl="../../images/butt_generate.gif"/>
                                                                        <asp:ImageButton id="PPHPrintPrev" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPPHPrintPrev_Click" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                    <asp:Label id=lblErrGenerate ForeColor=red Font-Italic=true visible=false runat=server />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentPane>
                                                    </igtab:Tab>
                                                    
                                                    <%--FFB PRICE--%>
                                                    <igtab:Tab Key="FFBPRICE" Text="FFB PRICE" Tooltip="FFB PRICE">
                                                        <ContentPane>
                                                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                                                <tr>
                                                                    <td colspan="5">
                                                                                
                                                                            <asp:DataGrid ID="dgFFBPriceList" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                                CellPadding="2" OnItemDataBound="dgFFBPriceList_BindGrid" Width="100%">
                                                                                <AlternatingItemStyle CssClass="mr-r" />
                                                                                <ItemStyle CssClass="mr-l" />
                                                                                <HeaderStyle CssClass="mr-h" />
                                                                                <Columns>
                                                                                    <asp:TemplateColumn HeaderText="Source" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("Name") %>' id="lblNamaSpl" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Tanggal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center >
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# objGlobal.GetLongDate(Container.DataItem("InDate")) %>' id="lblTglMasuk" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Buah Besar" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label7" Text='<%# FormatNumber(Container.DataItem("BuahBesarPrice"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Buah Sedang" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label8" Text='<%# FormatNumber(Container.DataItem("BuahSedangPrice"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Buah Kecil" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label9" Text='<%# FormatNumber(Container.DataItem("BuahKecilPrice"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <asp:TemplateColumn HeaderText="Brondolan" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label9" Text='<%# FormatNumber(Container.DataItem("Brondolan"), 0) %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=5>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            
                                                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                                            <tr>
                                                                <td>DISBUN PRICE</td>
                                                            </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <div id="div5" style="height:500px;width:500;overflow:auto;">				
                                                                            <asp:DataGrid ID="dgFFBPriceListDet" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                                CellPadding="2" OnItemDataBound="dgFFBPriceList_BindGrid" Width="100%">
                                                                                <AlternatingItemStyle CssClass="mr-r" />
                                                                                <ItemStyle CssClass="mr-l" />
                                                                                <HeaderStyle CssClass="mr-h" />
                                                                                <Columns>
                        
                                                                                    <asp:TemplateColumn HeaderText="Tanggal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center >
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# objGlobal.GetLongDate(Container.DataItem("InDate")) %>' id="lblTglMasuk" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Umur Tanam" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# Container.DataItem("BlkCode") %>' id="lblBlkcode" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Harga" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label8" Text='<%# FormatNumber(Container.DataItem("Price"), 0) %>' runat="server" />
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
                                                            </table>
                                                        </ContentPane>
                                                    </igtab:Tab>
                                                    
                                                    <igtab:Tab Key="FFBOB" Text="ONGKOS BONGKAR PRICE" Tooltip="ONGKOS BONGKAR PRICE">
                                                        <ContentPane>
                                                            <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <div id="div6" style="height:400px;width:500;overflow:auto;">				
                                                                            <asp:DataGrid ID="dgFFFBOB" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                                CellPadding="2" OnItemDataBound="dgFFFBOB_BindGrid" Width="100%">
                                                                                <AlternatingItemStyle CssClass="mr-r" />
                                                                                <ItemStyle CssClass="mr-l" />
                                                                                <HeaderStyle CssClass="mr-h" />
                                                                                <Columns>
                                                                                    <asp:TemplateColumn HeaderText="Tanggal" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center >
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Text='<%# objGlobal.GetLongDate(Container.DataItem("InDate")) %>' id="lblTglMasuk" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Ongkos Bongkar" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblOB" Text='<%# FormatNumber(Container.DataItem("Ongkos_Bongkar"), 0) %>' runat="server" />
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
                                                            </table>
                                                            
                                                        </ContentPane>
                                                    </igtab:Tab>
                                                    
                                                   
                                                </Tabs>
                                            </igtab:UltraWebTab>
                                        </td>
                                    </tr>	
                            
                                    <tr>
                                        <td colspan=5>&nbsp;</td>
                                    </tr>
                                </table>	                    
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
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
