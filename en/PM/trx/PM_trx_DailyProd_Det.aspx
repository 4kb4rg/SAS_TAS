<%@ Page Language="vb" codefile="../../../include/PM_trx_DailyProd_Det.aspx.vb" Inherits="PM_DailyProd_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<html>
	<head>
		<title>Mill Production - Daily Production Details</title>
               <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function calEBAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtEBBackLog.value);
				var b = parseFloat(doc.txtEBTodayProduction.value);
				var c = parseFloat(doc.txtEBSent.value);
	
				doc.txtEBTodayRestan.value = a + b - c;
				
				if (doc.txtEBTodayRestan.value == 'NaN'){
					doc.txtEBTodayRestan.value = '0';
				}
			}

			function calExtractionRate() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtFFBProcessed.value);				
				var b = parseFloat(doc.txtCPOProduced.value);
				var c = parseFloat(doc.txtPKProduced.value);
				
				var w = parseFloat(doc.txtFFBInternal.value);
				var x = parseFloat(doc.txtFFBAssociate.value);
				var y = parseFloat(doc.txtFFBExternal.value);
				var z = parseFloat(doc.txtFFBSupplier.value);
				
				var d = parseFloat(doc.txtBacklogBunches.value);
				var e = parseFloat(doc.txtFFBProcessed.value); 
				var f = parseFloat(doc.txtSemiProcessed.value); 
				var g = parseFloat(doc.txtUnprocessed.value); 
				
				if (a == 0) {
					doc.txtOER.value = '';
					doc.txtKER.value = '';
				}
				else {
					doc.txtOER.value = (b / (a *  ((d + w + x + y + z) / (e + f + g)))) * 100.0  ;
					if (doc.txtOER.value == 'NaN')
						doc.txtOER.value = '';
					else
						doc.txtOER.value = round(doc.txtOER.value, 2);
						doc.txtKER.value = (c / (a *  ((d + w + x + y + z) / (e + f + g)))) * 100.0  ;
					if (doc.txtKER.value == 'NaN')
						doc.txtKER.value = '';
					else
						doc.txtKER.value = round(doc.txtKER.value, 2);
				}	
			}			
			
		
		</script>
	    <style type="text/css">
            .style1
            {
                height: 25px;
            }
        </style>
	</head>
	<body>
		<form id="frmMain" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<input type=hidden id=hidCPOProduced value="0" runat=server />
			<input type=hidden id=hidPKProduced value="0" runat=server />
			<table cellpadding="2" cellspacing=0 width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuPDTrx id=MenuPDTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3">DAILY PRODUCTION DETAILS</td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
			<tr>
				<td class="font9Header" colspan=6 style="height: 16px">&nbsp;Status : 
                    <asp:Label id=lblStatus runat=server /> | Date Created : 
                    <asp:Label id=lblCreateDate runat=server />| Last Update : 
                    <asp:Label id=lblLastUpdate runat=server />| 
                                Update By : <asp:Label id=lblUpdateBy runat=server />
                            </td>
			</tr>
				<tr>
					<td width="25%" height=25>Tanggal Produksi :*</td>
					<td width="25%">
						<asp:TextBox id="txtdate" runat="server" width=70% maxlength="20"/>                       
						<a href="javascript:PopCal('txtdate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:RequiredFieldValidator 
							id="rfvDate" 
							runat="server"  
							ControlToValidate="txtdate" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:label id=lblDate Text="<br>Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
						<asp:label id=lblFmt forecolor=red Visible=false Runat="server"/> 
						<asp:label id="lblDupMsg" Text="Production for today already exist" Visible=false forecolor=red Runat="server"/>								
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="25%">&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Jumlah Rebusan :*</td>
					<td><telerik:RadNumericTextBox ID="RadNumericTextBox16"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Start Processing Time :</td>
					<td width="25%">
						
						<asp:TextBox id="txtStartHour" runat="server" width=15% maxlength="2"></asp:TextBox>:
						<asp:TextBox id="txtStartMnt" runat="server" Width=15% maxlength="2" ></asp:TextBox> (HH:MM) 
						<asp:label id=lblErrStartTime Text="<br>Field cannot be blank" forecolor=red Visible=false Runat="server"/> 
						<asp:RangeValidator id="rvStartHour"
							ControlToValidate="txtStartHour"
							MinimumValue="0"
							MaximumValue="23"
							Type="integer"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
								
						<asp:RangeValidator id="rvStartMnt"
							ControlToValidate="txtStartMnt"
							MinimumValue="0"
							MaximumValue="59"
							Type="integer"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>	
					</td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				<tr>
					<td class="style1">Rata2 Rebusan :</td>
					<td class="style1"><telerik:RadNumericTextBox ID="RadNumericTextBox17"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
					</td>
					<td width="5%" class="style1"></td>
					<td width="15%" class="style1">Start Processing Date :</td>
					<td width="25%" class="style1">
						<asp:TextBox id="txtStartDate" runat="server" width=70% maxlength="20" />                       
						<a href="javascript:PopCal('txtStartDate');">
						<asp:Image id="btnSelStartDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:label id=lblStartDate Text="<br>Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
						<asp:label id=lblStartFmt forecolor=red Visible=false Runat="server"/> 
						</td>
					<td width="5%" class="style1"></td>
				</tr>
				<tr>
					<td height=25>Lorry Belum Olah :*</td>
					<td><telerik:RadNumericTextBox ID="RadNumericTextBox18"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">End Processing Time :</td>
					<td width="25%">
						<asp:TextBox id="txtEndHour" runat="server" width=15% maxlength="2"></asp:TextBox>:
						<asp:TextBox id="txtEndMnt" runat="server" Width=15% maxlength="2" ></asp:TextBox> (HH:MM) 
						<asp:label id=lblErrEndTime Text="<br>Field cannot be blank" forecolor=red Visible=false Runat="server"/> 
						<asp:RangeValidator id="rvEndHour"
							ControlToValidate="txtEndHour"
							MinimumValue="00"
							MaximumValue="23"
							Type="integer"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>	
						<asp:RangeValidator id="rvEndMnt"
							ControlToValidate="txtEndMnt"
							MinimumValue="00"
							MaximumValue="59"
							Type="integer"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>	
						
					</td>
					<td width="5%">&nbsp;</td>
				</tr>
							
				
				<tr>
					<td height=25>Penerimaan TBS - Internal (KG) :</td>
					<td><telerik:RadNumericTextBox ID="RadNumericTextBox19"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                    
					</td>
					<td>&nbsp;</td>
					<td>End Processing Date :</td>
					<td>
						<asp:TextBox id="txtEndDate" runat="server" width=70% maxlength="20" />                       
						<a href="javascript:PopCal('txtEndDate');">
						<asp:Image id="btnSelEndDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:label id=lblEndtDate Text="<br>Field cannot be blank" forecolor=red Visible=false Runat="server"/> 
						<asp:label id=lblEndFmt forecolor=red Visible=false Runat="server"/> 
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Peneriaman TBS - Associate (KG) :</td>
					<td><telerik:RadNumericTextBox ID="RadNumericTextBox20"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                       
					</td>
					<td>&nbsp;</td>
					<td>Jam Efektif :</td>
					<td>
						<telerik:RadNumericTextBox ID="RadNumericTextBox21"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
						</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Penrimaan TBS - External (KG) :</td>
					<td><telerik:RadNumericTextBox ID="RadNumericTextBox22"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                       
					</td>
					<td>&nbsp;</td>
					<td>Breakdown :</td>
					<td>
						<asp:TextBox id="txtBrokHour" runat="server" width=15% maxlength="2"></asp:TextBox>:
						<asp:TextBox id="txtBrokMnt" runat="server" Width=15% maxlength="2"></asp:TextBox> (HH:MM) 
						<asp:label id=lblErrBrokTime Text="<br>Please Check Value" forecolor=red Visible=false Runat="server"/> 
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="25%" height=25>Potongan (KG) :</td>
					<td width="25%">
						
						<telerik:RadNumericTextBox ID="RadNumericTextBox23"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="25%">&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width="25%" height=25>Total Penerimaan TBS (KG) :</td>
					<td width="25%">
						<telerik:RadNumericTextBox ID="RadNumericTextBox24"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                   
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="25%">
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				
				<tr >
					<td width="25%" height=25>&nbsp;</td>
					<td width="25%">
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="25%">
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				<tr>
					<td width="25%" height=25>Buah Olah Netto :</td>
					<td width="25%"><telerik:RadNumericTextBox ID="RadNumericTextBox25"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Restan Olah Netto :</td>
					<td width="25%"><telerik:RadNumericTextBox ID="RadNumericTextBox26"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				<tr>
					<td width="25%" height=25>Buah Olah Bruto :</td>
					<td width="25%"><telerik:RadNumericTextBox ID="RadNumericTextBox27"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Restan Olah Bruto</td>
					<td width="25%"><telerik:RadNumericTextBox ID="RadNumericTextBox28"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> </td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				

				<tr>
				    <td width="25%" height=25>Kapasitas Olah :</td>
					<td width="25%">
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="25%">
						&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				<!--
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td width="5%">&nbsp;</td>
					
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td width="5%">&nbsp;</td>
					
					<td width="5%">&nbsp;</td>
				</tr>		
				-->
						
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete" Visible=False CausesValidation=False />
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
			</table>

            <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft" Width="100%"
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
                                <igtab:Tab Key="FFB" Text="CPO PROSES & STORAGE" Tooltip="LIST CPO STRORAGE">                      
                                    <ContentPane>
                                         <table border="0" cellspacing="1" cellpadding="1" width="99%"  class="font9Tahoma">    
	                                                            <tr>
					                                                <td height=25>Saldo Awal CPO (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox29"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>           
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					                                  
				                                                </tr>
	                                                            <tr>
					                                                <td height=25>CPO Dispatched (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="txtDRTotalAmount"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>           
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					                                  
				                                                </tr>
				
				                                                <tr>
					                                                <td height=25>CPO Produced (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox1"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>  
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
				 
					
				                                                </tr>
				
				                                                <tr>
					                                                <td height=25>OER Netto (%) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox2"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>  </td>
					                                                <td width="5%">&nbsp;</td>
					 
				                                                </tr>

				                                                <tr>
					                                                <td height=25>OER Bruto (%) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox31"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>  </td>
					                                                <td width="5%">&nbsp;</td>
					 
				                                                </tr>
                                                                				
				                                                <tr>
					                                                <td height=25>CPO Retur (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox3"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>  </td>
					                                                <td width="5%">&nbsp;</td>
					
				                                                </tr>  
				
   
                                            </tr>
 
	                                        <tr>
                                                <td colspan="6">
                                                <asp:DataGrid id=dgStorageCPO
							                    AutoGenerateColumns=False width=100% runat=server 
							                    Cellpadding=2 
							                    AllowPaging=True 
							                    PageSize=10				 
                                                ShowFooter=false
							                    AllowSorting="true"  >
								
                                                <HeaderStyle CssClass="mr-h"/>
                                                <ItemStyle CssClass="mr-l"/>
                                                <AlternatingItemStyle CssClass="mr-r"/>
							
							                    <Columns>
 
								
								                <asp:TemplateColumn HeaderText="Storage">
								                <ItemStyle Width="6%" HorizontalAlign="Right"/>
									                <ItemTemplate>
										                <%#Container.DataItem("TankiCode")%>
									                </ItemTemplate>									
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Sounding(cm)">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>                                                                        
										                <%# FormatNumber(Container.DataItem("SoundingCm"), 2)%>
									                </ItemTemplate>                                      
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Sounding (mm)">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>
										                <%#FormatNumber(Container.DataItem("soundingmm"), 2)%>                                                                            
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Suhu <br> ('c)">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>                                                                            
										                <%#FormatNumber(Container.DataItem("Temperatur"), 2)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Massa  <br>Jenis">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>
										                <%# FormatNumber(Container.DataItem("BeratJenis"), 2)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Product <br> (Liter)">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>
										                <%#FormatNumber(Container.DataItem("ProductLiter"), 0)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Cincin">
								                <ItemStyle Width="6%" HorizontalAlign="Center" />
									                <ItemTemplate>
										                <%# FormatNumber(Container.DataItem("Cincin"), 0)%>
                                                                            
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />

								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Fraksi">
								                <ItemStyle Width="6%" HorizontalAlign="Center" />
									                <ItemTemplate>                                                                            
										                <%#FormatNumber(Container.DataItem("Fraksi"), 0)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
    							                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="F. Koreksi">
								                <ItemStyle Width="4%" HorizontalAlign="Center" />
									                <ItemTemplate>
										                <%#FormatNumber(Container.DataItem("SelisihSuhu"), 7)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>

								                <asp:TemplateColumn HeaderText="Volume Storage <br> (Liter)">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>
										                <%#FormatNumber(Container.DataItem("TotalProductLiter"), 0)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Volume Storage (Kg)">
								                <ItemStyle Width="6%" HorizontalAlign="Right" />
									                <ItemTemplate>
										                <%#FormatNumber(Container.DataItem("TotalProductKG"),0)%>
									                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
								                </asp:TemplateColumn>
								
								                                    
							                </Columns>
                                            <PagerStyle Visible="False" />
                                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" Font-Italic="False" Font-Names="Arial Narrow"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#000000" />
							
						                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        </table>
                                    </ContentPane>
                                </igtab:Tab>

                                <igtab:Tab Key="DISP" Text="KERNEL PROSES & STORAGE" Tooltip="LIST KERNEL">                      
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%"  class="font9Tahoma">
				                                                <tr>
					                                                <td>PK Saldo Awal (KG) :</td>
					                                                <td height=25><telerik:RadNumericTextBox ID="RadNumericTextBox30"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                        
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
					
				                                                </tr>
				                                                <tr>
					                                                <td>PK Dispatched (KG) :</td>
					                                                <td height=25><telerik:RadNumericTextBox ID="RadNumericTextBox4"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                        
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
					
				                                                </tr>
                                                                <tr>
					                                                <td height=25>PK Produced (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox5"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
					 
				                                                </tr>
				                                                <tr>
					                                                <td height=25>KER Netto (%) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox6"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> </td>
					                                                <td width="5%">&nbsp;</td>
 
				                                                </tr>
				                                                <tr>
					                                                <td height=25>KER Bruto (%) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox32"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>  </td>
					                                                <td width="5%">&nbsp;</td>
					 
				                                                </tr>
				                                                <tr>
					                                                <td height=25>PK Retur (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox7"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> </td>
					                                                <td width="5%">&nbsp;</td>
	 
				                                                </tr>                                         
                                            <tr>
                                                <td colspan="5">
                                               
                                                    <div id="div1" style="height:300px;width:95%;overflow:auto;">				
                                                         
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentPane>
                                 </igtab:Tab>      
                                 
                                 <igtab:Tab Key="OTH" Text="PRODUK LAINNYA" Tooltip="LAIN-LAIN">                      
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%"  class="font9Tahoma">
				                                                <tr>
					                                                <td height=25>Saldo Awal Cangkang (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox8"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> </td>
					                                                <td width="5%">&nbsp;</td>
					
					                                                <td width="15%">Saldo Awal Abu Jankos  : </td>
					                                                <td width="25%">
						                                                <telerik:RadNumericTextBox ID="RadNumericTextBox9"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
 
						                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
				                                                </tr>

				                                                <tr>
					                                                <td>Cangkang Dispatched (KG) :</td>
					                                                <td height=25><telerik:RadNumericTextBox ID="RadNumericTextBox10"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>                      
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
					                                                <td>Abu Jankos Dispatched (KG) :</td>
					                                                <td width="25%">
						                                                <telerik:RadNumericTextBox ID="RadNumericTextBox11"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
						        
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
				                                                </tr>
                                                                <tr>
					                                                <td height=25>Cangkang Produced (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox12"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
					                                          
					                                                </td>
					                                                <td width="5%">&nbsp;</td>
					
					
					                                                <td width="15%">Abu Jankos Produced (KG) : </td>
					                                                <td width="25%">
						                                               <telerik:RadNumericTextBox ID="RadNumericTextBox13"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> 
			 
						                                                </td>
					                                                <td width="5%">&nbsp;</td>
				                                                </tr>
				                                                <tr>
					                                                <td height=25>Cangkang Retur (KG) :</td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox14"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> </td>
					                                                <td width="5%">&nbsp;</td>

					                                                <td width="15%"><u>Abu Jankos Retur (KG)</u></td>
					                                                <td><telerik:RadNumericTextBox ID="RadNumericTextBox15"  CssClass="font9Tahoma"   Runat="server" LabelWidth="64px">     
                                                                        <NumberFormat ZeroPattern="n"></NumberFormat>
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox> </td>
					                                                <td width="5%">&nbsp;</td>
				                                                </tr>                                         
                                            <tr>
                                                <td colspan="5">
                                               
                                                    <div id="div2" style="height:300px;width:95%;overflow:auto;">				
                                                         
                                                     </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                        </table><table border="0" cellspacing="1" cellpadding="1" width="99%">
                                         
                                            <tr>
                                                <td colspan="5">
                                               
                                                    <div id="div7" style="height:300px;width:95%;overflow:auto;">				
                                                        
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
                                 <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
		</form>
	</body>
</html>
