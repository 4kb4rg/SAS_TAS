<%@ Page Language="vb" CodeFile="~/include/GL_Financial_Statement.vb" Inherits="GL_Financial_Statement" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>Monthly Closing - Trial</title>
    <style type="text/css">
        .font9 {
            font-size: 9pt;
        }

        a {
            text-decoration: none;
        }


        hr {
            width: 1368px;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            margin-left: 0px;
        }

        .auto-style1 {
            height: 25px;
        }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 30px; /* Location of the box */
            left: 0;
            top: 0;
            width: 80%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: Transparent; /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: white;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 80%;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.6s
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {
                top: -700px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        @keyframes animatetop {
            from {
                top: -700px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }
             
         .modal-header {
                    font-size:25px;                    
                    font-family:Arial Narrow;
                    padding: 2px 1px;
                    Height:10px;
                              
                }

        .modal-body {
            padding-top:20px;
            padding-bottom:20px;
            padding-left:20px;
            padding-right:20px;
            background-color: white;
            color: black;
        }

        .modal-footer {
            padding: 2px 5px;
            background-color: green;
            color: white;
        }
    </style>
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="frmMain" runat="server">
        <div class="kontenlist">
            <table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma">

                <tr>
                    <td class="mt-h" colspan="5">
                        <strong>FINANCIAL STETEMENT REPORT</strong></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <hr style="width: 100%" />
                    </td>
                </tr>

                <tr>
                    <td colspan="5" style="height: 25px">&nbsp;</td>
                </tr>
                <tr>
                    <td width="40%" height="25">Accounting Period :</td>
                    <td width="30%">
                        <asp:DropDownList ID="ddlAccMonth" runat="server" />
                        / 
					<asp:DropDownList ID="ddlAccYear" AutoPostBack="False" runat="server" />
                    </td>
                    <td width="5%">&nbsp;</td>
                    <td width="15%">&nbsp;</td>
                    <td width="10%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="40%" height="25">Report Name :</td>
                    <td width="30%">
                        <asp:DropDownList ID="ddlRptType" runat="server" Width="100%" CssClass="font9Tahoma" />
                    </td>
                    <td width="5%">&nbsp;</td>
                    <td width="15%">&nbsp;</td>
                    <td width="10%">&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="3">
                        <asp:RadioButton ID="optGroup" Text="Group Display Based on Template " AutoPostBack="true" OnCheckedChanged="optGroup_CheckedChanged" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:RadioButton ID="optExpand" Text="Expand Display Based on Account " AutoPostBack="true" OnCheckedChanged="optExpand_CheckedChanged" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="auto-style1">
                        <asp:Label ID="lblErrProcess" Visible="false" ForeColor="red" Text="There are some errors when processing month end." runat="server" />
                    </td>
                </tr>

                <tr>
                    <td colspan="5">
                        <asp:Button Text="Preview" ID="btnProceed" OnClick="btnGenerate_Click" runat="server" class="button-small" />
                        <asp:Button Text="Print" ID="myBtn" runat="server" class="button-small" />
                        
                        &nbsp;
                    </td>
                </tr>

                <tr>
                    <td colspan="5"></td>
                </tr>

                <tr>
                    <td style="height: 24px;" colspan="5">

                        <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                            SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor="black" runat="server">
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
                                <%--NERACA REKAP--%>
                                <igtab:Tab Key="fs01" Text="TEMPLATE 1" Tooltip="TEMPLATE 1" NextRow="true">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                            <tr>
                                                <td style="text-align:right">
                                                  <asp:ImageButton id="BtnExportTmpl1" OnClick="btnExcelTemplate1_Click" UseSubmitBehavior="false"  imageurl="../../images/Excel.PNG" Width="25px" Height="30px"     CausesValidation=False     runat=server/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div4" style="height: 500px; width: 1020; overflow: auto;">
                                                        <asp:DataGrid ID="dgFS1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                            CellPadding="3" CellSpacing="3" Width="100%">
                                                            <AlternatingItemStyle CssClass="mr-l" />
                                                            <ItemStyle CssClass="mr-l" />
                                                            <HeaderStyle CssClass="mr-h" />
                                                            <Columns>
                                                                <asp:TemplateColumn Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrowid" Visible="false" Text='<%# Container.DataItem("RowID") %>' runat="server" />
                                                                        <asp:Label ID="lblFBold" Visible="false" Text='<%# Container.DataItem("FBold") %>' runat="server" />
                                                                        <asp:Label ID="lblSpace" Visible="false" Text='<%# Container.DataItem("FSpace") %>' runat="server" />
                                                                        <asp:Label ID="lblUnderLine" Visible="false" Text='<%# Container.DataItem("FUnderline") %>' runat="server" />
                                                                        <asp:Label ID="lblFont" Visible="false" Text='<%# Container.DataItem("FFont") %>' runat="server" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn  HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="60%">
                                                                    <HeaderTemplate>                                                                        
                                                                        <asp:Label ID="lblDescription" Text="DESCRIPTION" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" Text='<%# Container.DataItem("Description") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="REF. NO" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRefFNo" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="AMOUNT"  HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotBlnIniLrNew" Text='<%# FormatNumber(Container.DataItem("Amount"), 0)%>' runat="server" />
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

                                <%--NERACA DETAIL--%>
                                <igtab:Tab Key="fs02" Text="TEMPLATE 2" Tooltip="TEMPLATE 2">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                            <tr>
                                              <td style="text-align:right">
                                                        <asp:ImageButton id="BtnExportTmpl2" OnClick="btnExcelTemplate2_Click" UseSubmitBehavior="false"  imageurl="../../images/Excel.PNG" Width="25px" Height="30px"     CausesValidation=False     runat=server/>
                                                        <asp:ImageButton id="BtnExportNeraca" OnClick="btnExcelTemplateNeraca_Click" UseSubmitBehavior="false"  imageurl="../../images/Excel.PNG" Width="25px" Height="30px"     CausesValidation=False     runat=server/>
                                                </td>
                                            </tr>
                                            <tr id="trLRKomparatif" runat="server">

                                                <td colspan="5">
                                                    <div id="div1" style="height: 500px; width: 1020; overflow: auto;">
                                                        <asp:DataGrid ID="dgFS2" runat="server" AutoGenerateColumns="False" Visible="true" GridLines="Both"
                                                             CellPadding="3" CellSpacing="3" Width="100%">
                                                            <AlternatingItemStyle CssClass="mr-l" />
                                                            <ItemStyle CssClass="mr-l" />
                                                            <HeaderStyle CssClass="mr-h" />
                                                            <Columns>
                                                                <asp:TemplateColumn Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrowid" Visible="false" Text='<%# Container.DataItem("RowID") %>' runat="server" />
                                                                        <asp:Label ID="lblFBold" Visible="false" Text='<%# Container.DataItem("FBold") %>' runat="server" />
                                                                        <asp:Label ID="lblSpace" Visible="false" Text='<%# Container.DataItem("FSpace") %>' runat="server" />
                                                                        <asp:Label ID="lblUnderLine" Visible="false" Text='<%# Container.DataItem("FUnderline") %>' runat="server" />
                                                                        <asp:Label ID="lblFont" Visible="false" Text='<%# Container.DataItem("FFont") %>' runat="server" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn  HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" Text='<%# Container.DataItem("Description") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="HEAD OFFICE" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmountLoc1" Text='<%# FormatNumber(Container.DataItem("AmountLoc1"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="PMKS" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmountLoc2" Text='<%# FormatNumber(Container.DataItem("AmountLoc2"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="TOTAL" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotBlnIniLrNew" Text='<%# FormatNumber(Container.DataItem("Amount"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trNeracaKomparatif" runat="server">
         
                                                <td colspan="5">
                                                    <div id="div1A" style="height: 500px; width: 1020; overflow: auto;">
                                                        <asp:DataGrid ID="dgFSNrcDetail" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                             CellPadding="3" CellSpacing="3" Width="150%">
                                                            <AlternatingItemStyle CssClass="mr-l" />
                                                            <ItemStyle CssClass="mr-l" />
                                                            <HeaderStyle CssClass="mr-h" />
                                                            <Columns>
                                                                <asp:TemplateColumn Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrowid" Visible="false" Text='<%# Container.DataItem("RowID") %>' runat="server" />
                                                                        <asp:Label ID="lblFBold" Visible="false" Text='<%# Container.DataItem("FBold") %>' runat="server" />
                                                                        <asp:Label ID="lblSpace" Visible="false" Text='<%# Container.DataItem("FSpace") %>' runat="server" />
                                                                        <asp:Label ID="lblUnderLine" Visible="false" Text='<%# Container.DataItem("FUnderline") %>' runat="server" />
                                                                        <asp:Label ID="lblFont" Visible="false" Text='<%# Container.DataItem("FFont") %>' runat="server" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="DESCRIPTION" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="35%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" Text='<%# Container.DataItem("Description") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="SALDO AWAL <br> HO" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcAwalHO" Text='<%# FormatNumber(Container.DataItem("AmountLoc1_Beg"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="MUTASI <br> HO" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcMutasiHO" Text='<%# FormatNumber(Container.DataItem("AmountLoc1_Mut"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="AMOUNT <br> HO" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcTotalHO" Text='<%# FormatNumber(Container.DataItem("AmountLoc1_End"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>


                                                                <asp:TemplateColumn HeaderText="SALDO AWAL <br> PABRIK" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcAwalMill" Text='<%# FormatNumber(Container.DataItem("AmountLoc2_Beg"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="MUTASI <br> PABRIK" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcMutasiMill" Text='<%# FormatNumber(Container.DataItem("AmountLoc2_Mut"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="AMOUNT <br> PABRIK" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcTotalMill" Text='<%# FormatNumber(Container.DataItem("AmountLoc2_End"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="TOTAL <br> SALDO AWAL" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcTotalAwal" Text='<%# FormatNumber(Container.DataItem("AmountTotal_Beg"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="TOTAL <br> MUTASI" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcTotalMutasi" Text='<%# FormatNumber(Container.DataItem("AmountTotal_Mut"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="SALDO <br> AKHIR" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNrcTotalEnd" Text='<%# FormatNumber(Container.DataItem("AmountTotal_End"), 0)%>' runat="server" />
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

                                <%--COGS--%>
                                <igtab:Tab Key="COGS" Text="TEMPLATE 3" Tooltip="TEMPLATE 3">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="73%">
                                            <tr>
                                              <td style="text-align:right">
                                                        <asp:ImageButton id="BtnExportTmplte3" OnClick="btnExcelTemplate3_CLick" UseSubmitBehavior="false"  imageurl="../../images/Excel.PNG" Width="25px" Height="30px"     CausesValidation=False     runat=server/>                                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div6" style="height: 500px; width: 1020; overflow: auto;">
                                                        <asp:DataGrid ID="dgFS3" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                             CellPadding="3" CellSpacing="3" Width="100%">
                                                            <AlternatingItemStyle CssClass="mr-l" />
                                                            <ItemStyle CssClass="mr-l" />
                                                            <HeaderStyle CssClass="mr-h" />
                                                            <Columns>
                                                                <asp:TemplateColumn Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrowid" Visible="false" Text='<%# Container.DataItem("RowID") %>' runat="server" />
                                                                        <asp:Label ID="lblFBold" Visible="false" Text='<%# Container.DataItem("FBold") %>' runat="server" />
                                                                        <asp:Label ID="lblSpace" Visible="false" Text='<%# Container.DataItem("FSpace") %>' runat="server" />
                                                                        <asp:Label ID="lblUnderLine" Visible="false" Text='<%# Container.DataItem("FUnderline") %>' runat="server" />
                                                                        <asp:Label ID="lblFont" Visible="false" Text='<%# Container.DataItem("FFont") %>' runat="server" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="DESCRIPTION" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" Text='<%# Container.DataItem("Description") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmountLoc1" Text='<%# FormatNumber(Container.DataItem("AmountBulanLalu"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmountLoc2" Text='<%# FormatNumber(Container.DataItem("AmountBlnIni"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <asp:TemplateColumn HeaderText="S.D BULAN INI" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotBlnIniLrNew" Text='<%# FormatNumber(Container.DataItem("AmountSDBulanIni"), 0)%>' runat="server" />
                                                                    </ItemTemplate>
                                                                           
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">&nbsp;</td>
                                            </tr>

                                        </table>
                                    </ContentPane>
                                </igtab:Tab>
                            </Tabs>
                        </igtab:UltraWebTab>

                    </td>
                </tr>
                <tr>
                    <td colspan="6"></td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td height="25" align="right">
                        <asp:Label ID="lblTotalBalance" Visible="false" runat="server" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <asp:Label ID="lblBalance" Text="0" Visible="false" runat="server" /></td>
                </tr>

            </table>
        </div>

        <div id="myModal" class="modal" runat="server">
            <div class="modal-content" runat="server" style="left: 0px; top: 0px" id="Div2">
                <div class="modal-header" runat="server">
                    <span class="close"></span>
                </div>
                <div class="Font9Tahoma" runat="server" id="Div3">
                    <table style="width: 99%" class="font9Tahoma">
                        <tr>
                            <td style="width: 190px" valign="top">Report Name :</td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlRptPrint" runat="server" Width="100%" CssClass="font9Tahoma">
                                    <asp:ListItem value="1" Selected = "True" >Please Select Report</asp:ListItem>
                                    <asp:ListItem value="L/RNEW">Laba/Rugi</asp:ListItem>
                                    <asp:ListItem value="NRCNEW">Neraca</asp:ListItem>
                                </asp:DropDownList>
                        </tr>
                        <tr>
                            <td style="width: 190px" valign="top"></td>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="BtnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" class="button-small" />
                                <asp:Button ID="BtnClose" runat="server" Text="Close" class="button-small" /></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>

        <script language="javascript">
            // Get the modal
            var modal = document.getElementById('myModal');
            // Get the button that opens the modal
            var btn = document.getElementById('myBtn');
            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];
            // When the user clicks the button, open the modal
            btn.onclick = function () {
                modal.style.display = "block";
                return false;
            }
            // When the user clicks anywhere outside of the modal, close it
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                    return false;
                }
            }
        </script>
    </form>
</body>
</html>
