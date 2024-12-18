<%@ Page Language="vb" trace=false src="../../include/reports/WM_StdRpt_Daily_FFBRcv_Summary_Report.aspx.vb" Inherits="WM_StdRpt_Daily_FFBRcv_Summ" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WM_STDRPT_SELECTION_CTRL" src="../include/reports/WM_StdRpt_Selection_Ctrl.ascx"%>
 
<HTML>
	<HEAD>
		<title>Weighing Management - Daily FFB Received Summary Report</title>
        <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />		
			<asp:Label id="lblLocation" visible="false" runat="server" />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> WEIGHING MANAGEMENT - DAILY FFB RECEIVED SUMMARY REPORT</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:WM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" class="font9Tahoma" ID="Table1">		
				<tr>
					<td width=15%>
                        Report Type</td>
					<td width=35%>
                        <asp:DropDownList AutoPostBack=true  ID="lstAccType" CssClass="fontObject" runat="server" Width="100%" OnSelectedIndexChanged="lstAccType_SelectedIndexChanged">
                            <asp:ListItem Selected="" Value="0">Gross By Day</asp:ListItem>
                            <asp:ListItem Value="1">Weighing Historical Payment</asp:ListItem>
                        </asp:DropDownList></td>
					<td width=15%>&nbsp;</td>										
					<td width=35%>&nbsp;</td>										
				</tr>						
                <tr>
                    <td width="15%">
                        <asp:Label ID="lblSup" runat="server">Supplier :</asp:Label></td>
                    <td width="35%">
                        <asp:TextBox ID="txtSupplier" CssClass="fontObject" runat="server" MaxLength="16" Width="27%"></asp:TextBox><asp:TextBox
                            ID="txtSupName" runat="server" CssClass="fontObject" AutoPostBack="False" MaxLength="15" Width="65%"></asp:TextBox>&nbsp;<asp:Button
                                ID="FindSupplierButton" runat="server" CausesValidation="false" OnClientClick="javascript:PopSupplier_New('frmMain','','txtSupplier','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                                Text="..." Width="24px" /></td>
                    <td width="15%">
                    </td>
                    <td width="35%">
                    </td>
                </tr>
                <tr id="RowDate">
                    <td width="15%">
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="srchDateIn" runat="server" MaxLength="10" Width="25%" CssClass="fontObject" Visible="False"></asp:TextBox>
                        <a href="javascript:PopCal('srchDateIn');">
                        <asp:Image
                            ID="btnSelDateFrom" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/en/images/calendar.gif" Visible="False" /></a>
                        <asp:Label ID="lblTo" runat="server" Visible="False">To</asp:Label>
                        <a href="javascript:PopCal('srchDateTo');">
                        <asp:TextBox ID="srchDateTo" runat="server" MaxLength="10" Width="25%" CssClass="fontObject"  Visible="False"></asp:TextBox></a>
                        <asp:Image
                            ID="btnSelDateTo" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/en/images/calendar.gif" Visible="False" /><br />
                        <asp:Label ID="lblErrDateInMsg" runat="server" ForeColor="Red" Text="Date Format should be in "
                            Visible="False"></asp:Label><asp:Label ID="lblErrDateIn" runat="server" ForeColor="red"
                                Visible="false"></asp:Label></td>
                    <td width="15%">
                    </td>
                    <td width="35%">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="cbExcel" runat="server" AutoPostBack="True" Checked="false" Text=" Export To Excel" /></td>
                </tr>
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />
                        <asp:TextBox ID="txtPPNInit" runat="server" BackColor="Transparent" BorderStyle="None"
                            ForeColor="Transparent" Text="" Width="4%"></asp:TextBox><asp:TextBox ID="txtPPN"
                                runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"
                                ForeColor="Transparent" Width="3%"></asp:TextBox><asp:TextBox ID="TextBox1" runat="server"
                                    BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Transparent"
                                    Width="4%"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" ForeColor="Transparent" Width="3%"></asp:TextBox></td>
				</tr>
				
				            <tr>
                <td style="height: 24px;" colspan="5">
 
                    <table border="0" cellspacing="1" cellpadding="1" width="99%">
                        <tr>
                            <td colspan="5">
                                <div id="div1" style="height:250px;width:1000;overflow:auto;">	
                                    <asp:DataGrid ID="dgTicketList" runat="server" AutoGenerateColumns="False" GridLines="Both" 
                                    CellPadding="2" OnItemDataBound="dgTicketList_BindGrid" Width="300%">
                                    <AlternatingItemStyle CssClass="mr-r" />
                                    <ItemStyle CssClass="mr-l" />
                                    <HeaderStyle CssClass="mr-h" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left>
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItem("NoUrut") %>' id="lblNoUrut" runat="server" />
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
                                                <asp:Label Text='<%# Container.DataItem("ProductCode") %>' id="lblProd" runat="server" />
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
                                                <asp:Label ID="lblNetto1" Text='<%# FormatNumber(Container.DataItem("Tarra"), 0) %>' runat="server" />
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
                                                <asp:Label ID="Label32" Text='<%# Container.DataItem("KodeSliptTBS") %>' runat="server" />
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
                    </table>
 
                </td>
            </tr>					
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
