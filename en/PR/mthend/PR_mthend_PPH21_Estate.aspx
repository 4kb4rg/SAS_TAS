<%@ Page Language="vb" src="../../../include/PR_MthEnd_PPH21_Estate.aspx.vb" Inherits="PR_mthend_PPH21_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRMthEnd" src="../../menu/menu_PRMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
    
    
<html>
	<head>
		<title>Payroll Month End Process</title>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<%--<tr>
				<td colspan=5 align=center><UserControl:MenuPRMthEnd id=MenuPRMthEnd runat="server" /></td>
			</tr>--%>
			<tr>
				<td class="mt-h" colspan=5>
                    PROSES PPH21</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 width=20%>Period :</td>
					<td width=50%>	<asp:DropDownList id="ddlMonth" width="20%" runat=server>
										<asp:ListItem value="1">January</asp:ListItem>
										<asp:ListItem value="2">February</asp:ListItem>
										<asp:ListItem value="3">March</asp:ListItem>
										<asp:ListItem value="4">April</asp:ListItem>
										<asp:ListItem value="5">May</asp:ListItem>
										<asp:ListItem value="6">June</asp:ListItem>
										<asp:ListItem value="7">July</asp:ListItem>
										<asp:ListItem value="8">August</asp:ListItem>
										<asp:ListItem value="9">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddlyear width="20%" maxlength="4" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25></td> 
				<td width=50%></td>
				<td colspan=2>&nbsp; &nbsp;</td>
			</tr>
			<tr>
				<td colspan=4 style="height: 19px">&nbsp;<asp:Label ID="lblNoRecord" runat="server" Font-Italic=true ForeColor="red" Text="No Record Created"
                            Visible="false"></asp:Label><asp:Label ID="lblSuccess" runat="server" ForeColor="blue"
                                Text="Process Success" Visible="false"></asp:Label><asp:Label ID="lblFailed" runat="server"
                                    ForeColor="red" Text="Process Failed" Visible="false"></asp:Label>&nbsp;
                 </td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_process.gif" AlternateText="Proceed PPH21" OnClick="btnProceed_Click" runat="server" />
					<asp:ImageButton ID=btnRefresh  UseSubmitBehavior="false" AlternateText="Refresh Data" onclick="btnRefresh_Click"  ImageUrl="../../images/butt_refresh.gif"  CausesValidation=False Runat=server />
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
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
                            <igtab:Tab Key="KRYWN" Text="SPT BULANAN KARYAWAN" Tooltip="SPT BULANAN KARYAWAN">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div1" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgKRYWN" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2" OnItemDataBound="dgKRYWN_BindGrid" Width="250%">
                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                        <ItemStyle CssClass="mr-l" />
                                                        <HeaderStyle CssClass="mr-h" />
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
                                            <asp:CheckBox id="cbExcel" text=" Export To Excel" Visible=false checked="false" runat="server" /></td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=5>
                                                <asp:ImageButton id="BtnPreview" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPreview_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>         
                            
                            <%--WB PPH--%>
                            <igtab:Tab Key="NONKRYWN" Text="SPT BULANAN NON KARYAWAN" Tooltip="SPT BULANAN NON KARYAWAN">
                                <ContentPane>
                                     <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div2" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgNONKRYWN" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2" OnItemDataBound="dgNONKRYWN_BindGrid" Width="250%">
                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                        <ItemStyle CssClass="mr-l" />
                                                        <HeaderStyle CssClass="mr-h" />
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
                                            <asp:CheckBox id="cbExcelNONKRYWN" text=" Export To Excel" Visible=false checked="false" runat="server" /></td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=5>
                                                <asp:ImageButton id="BtnPreviewNONKRYWN" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="BtnPreviewNONKRYWN_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                                                    
                            <igtab:Tab Key="KRYWN" Text="SPT TAHUNAN KARYAWAN" Tooltip="SPT TAHUNAN KARYAWAN">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div3" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgKRYWNThn" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2" OnItemDataBound="dgKRYWNThn_BindGrid" Width="250%">
                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                        <ItemStyle CssClass="mr-l" />
                                                        <HeaderStyle CssClass="mr-h" />
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
                                            <asp:CheckBox id="cbExcelThn" text=" Export To Excel" Visible=false checked="false" runat="server" /></td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=5>
                                                <asp:ImageButton id="btnGenerateThn" Visible=false ToolTip="Generate journal karyawan" UseSubmitBehavior="false" OnClick="btnGenerateThn_Click"  runat="server" ImageUrl="../../images/butt_generate.gif"/>
                                                <asp:ImageButton id="BtnPreviewThn" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPreviewThn_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>         
                            
                            <%--WB PPH--%>
                            <igtab:Tab Key="NONKRYWN" Text="SPT TAHUNAN NON KARYAWAN" Tooltip="SPT TAHUNAN NON KARYAWAN">
                                <ContentPane>
                                     <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div4" style="height:500px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid ID="dgNONKRYWNThn" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                        CellPadding="2" OnItemDataBound="dgNONKRYWNThn_BindGrid" Width="250%">
                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                        <ItemStyle CssClass="mr-l" />
                                                        <HeaderStyle CssClass="mr-h" />
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
                                            <asp:CheckBox id="cbExcelNONKRYWNThn" text=" Export To Excel" Visible=false checked="false" runat="server" /></td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=5>
                                                <asp:ImageButton id="btnGenerateNONKRYWNThn" Visible=false ToolTip="Generate journal non karyawan" UseSubmitBehavior="false" OnClick="btnGenerateNONKRYWNThn_Click"  runat="server" ImageUrl="../../images/butt_generate.gif"/>
                                                <asp:ImageButton id="BtnPreviewNONKRYWNThn" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPreviewNONKRYWNThn_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                        

                            
                            
                            
                            <igtab:Tab Key="REKON" Text="REKONSIL PPH Vs GL" Tooltip="REKONSIL PPH Vs GL">
                                <ContentPane>
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div5" style="height:300px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid id=dgReconAmt
                                                        AutoGenerateColumns=false width="90%" runat=server
                                                        GridLines=none Cellpadding=2>
                                                        <HeaderStyle CssClass="mr-h"/>
                                                        <ItemStyle CssClass="mr-l"/>
                                                        <AlternatingItemStyle CssClass="mr-r"/>
                                                        <Columns>						
                                                            <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="AKUN" HeaderStyle-Font-Bold=true >
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCodeRec" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                			<asp:TemplateColumn ItemStyle-Width="50%" HeaderText="DESKRIPSI" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescRec" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="JUMLAH" HeaderStyle-Font-Bold=true>
                                                                <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %> id="lblAmtRec" runat="server" />
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
                                            <asp:CheckBox id="cbExcelRecAmt" text=" Export To Excel" Visible=false checked="false" runat="server" /></td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=5>
                                                <asp:ImageButton id="BtnPreviewRecAmt" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPreviewRecAmt_Click" runat="server" />
                                            </td>
                                        </tr>       
                                    </table>
                                    
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td colspan="5">
                                                <div id="div8" style="height:300px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid id=dgReconAmtCr
                                                        AutoGenerateColumns=false width="90%" runat=server
                                                        GridLines=none Cellpadding=2>
                                                        <HeaderStyle CssClass="mr-h"/>
                                                        <ItemStyle CssClass="mr-l"/>
                                                        <AlternatingItemStyle CssClass="mr-r"/>
                                                        <Columns>						
                                                            <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="AKUN PENGURANG" HeaderStyle-Font-Bold=true >
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCodeRec" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                			<asp:TemplateColumn ItemStyle-Width="50%" HeaderText="DESKRIPSI" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescRec" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="JUMLAH" HeaderStyle-Font-Bold=true>
                                                                <HeaderStyle HorizontalAlign="Center" /> 
								                                <ItemStyle HorizontalAlign="Right"/> 
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %> id="lblAmtRec" runat="server" />
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
                                            <asp:CheckBox id="cbExcelRecAmtCr" text=" Export To Excel" Visible=false checked="false" runat="server" /></td>
                                        </tr>
                                        <tr>
	                                        <td colspan=5>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=5>
                                                <asp:ImageButton id="BtnPreviewRecAmtCr" Visible=false AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="btnPreviewRecAmtCr_Click" runat="server" />
                                            </td>
                                        </tr>       
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            
                            <igtab:Tab Key="COA" Text="REKONSIL COA SETTING" Tooltip="REKONSIL COA SETTING">
                                <ContentPane>
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td colspan="6">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width:20%" Align=left>Akun :</td>
                                            <td width=80%>
                                                <asp:DropDownList id=ddlAccCode width=85% runat=server />
                                                <input type=Button id=btnFind value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode', 'False');" runat=server/>
                                                <asp:Label id=lblErrAccCode visible=false forecolor=red runat=server/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAddDR imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server /></td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="6">&nbsp;</td>
                                        </tr>
                                    </table>
                                   
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                         <tr>
                                            <td colspan="5">
                                                <div id="div6" style="height:200px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid id=dgReconAcc
                                                        AutoGenerateColumns=false width="90%" runat=server
                                                        GridLines=none Cellpadding=2
                                                        OnDeleteCommand=DEDR_Delete >
                                                        <HeaderStyle CssClass="mr-h"/>
                                                        <ItemStyle CssClass="mr-l"/>
                                                        <AlternatingItemStyle CssClass="mr-r"/>
                                                        <Columns>						
                                                            <asp:TemplateColumn ItemStyle-Width="35%" HeaderText="AKUN" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                			
                                                            <asp:TemplateColumn ItemStyle-Width="55%" HeaderText="DESKRIPSI" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>

                                                            <asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>	
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                                    
									        </td>
									    </tr>
                                    </table>
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td colspan="6">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width:20%" Align=left>Akun Pengurang :</td>
                                            <td width=80%>
                                                <asp:DropDownList id=ddlAccCodeCr width=85% runat=server />
                                                <input type=Button id=btnFindCr value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCodeCr', 'False');" runat=server/>
                                                <asp:Label id=lblErrAccCodeCr visible=false forecolor=red runat=server/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAddCr imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAddCr_Click runat=server /></td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="6">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <table width="75%" cellspacing="0" cellpadding="0" border="0" align="center">
                                         <tr>
                                            <td colspan="5">
                                                <div id="div7" style="height:200px;width:1040;overflow:auto;">	
                                                    <asp:DataGrid id=dgReconAccCr
                                                        AutoGenerateColumns=false width="90%" runat=server
                                                        GridLines=none Cellpadding=2
                                                        OnDeleteCommand=DEDR_DeleteCr >
                                                        <HeaderStyle CssClass="mr-h"/>
                                                        <ItemStyle CssClass="mr-l"/>
                                                        <AlternatingItemStyle CssClass="mr-r"/>
                                                        <Columns>						
                                                            <asp:TemplateColumn ItemStyle-Width="35%" HeaderText="AKUN PENGURANG" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                			
                                                            <asp:TemplateColumn ItemStyle-Width="55%" HeaderText="DESKRIPSI" HeaderStyle-Font-Bold=true>
                                                                <ItemTemplate>
                                                                    <asp:Label Text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>

                                                            <asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>	
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                                    
									        </td>
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
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." ForeColor=red runat=server />
		</form>
	</body>
</html>
