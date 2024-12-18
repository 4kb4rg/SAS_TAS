<%@ Page Language="vb" src="../../../include/PD_trx_ProdDet_Estate.aspx.vb" Inherits="PD_trx_ProdDet_Estate"%> 
<%@ Register TagPrefix="UserControl" TagName="MenuPDTrx" Src="../../menu/menu_pdtrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %><html>
<head>
    <title>Pengiriman TBS Detail</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
                function callNetto2() {
				var doc = document.frmMain;
				var a = parseFloat(doc.TxtBruto2.value);
				var b = parseFloat(doc.TxtTarra2.value);
				
				doc.TxtNetto2.value = (a - b);
				
				if (doc.TxtNetto2.value == 'NaN') 
					doc.TxtNetto2.value = 0;
				var d = doc.TxtNetto2.value;
			
			    if (doc.TxtPot.value != 'NaN' && doc.TxtPot.value != '' )
			    {
				    var c = parseFloat(doc.TxtPot.value)
				    //doc.TxtPotKg.value = ((c / 100) * d).toFixed(0);
					
					//alert(doc.chkpot.checked)
					
					if (doc.chkpot.checked)
					    doc.TxtPotKg.value = c
						else
						doc.TxtPotKg.value = round(round((c / 100) * d)).toFixed(0);
				}
				else
				{
				    doc.TxtPotKg.value = 0;
				}
				
				
			    var f = parseFloat(doc.TxtPotKg.value)
			    doc.TxtNetto.value = d - f;		
			    if (doc.TxtNetto.value == 'NaN') 
					doc.TxtNetto.value = 0;
							
			}
			
			function callNetto1() {
				var doc = document.frmMain;
				var a = parseFloat(doc.TxtBruto1.value);
				var b = parseFloat(doc.TxtTarra1.value);
				doc.TxtNetto1.value = (a - b);
				
				if (doc.TxtNetto1.value == 'NaN') 
					doc.TxtNetto1.value = 0;
			}
			
			function callPotongan() {
				var doc = document.frmMain;
				var a = parseFloat(doc.TxtPotBM.value);
				var b = parseFloat(doc.TxtPotBB.value);
				var c = parseFloat(doc.TxtPotSP.value);
				var d = parseFloat(doc.TxtPotAR.value);
				var e = parseFloat(doc.TxtPotPL.value);
				
				doc.TxtPot.value = (a + b + c + d + e);
				
				if (doc.TxtPot.value == 'NaN') 
					doc.TxtPot.value = 0;		
					
				//var x = parseFloat(doc.TxtNetto2.value)
				//var y = parseFloat(doc.TxtPot.value)
				
				//if (doc.chkpot.checked)
				//doc.TxtPotKg.value = y
				//else
				//doc.TxtPotKg.value = (y / 100) * x
				
				callNetto2()
				
			}
			
    </script>
    
    <style type="text/css">
        .style1
        {
            width: 428px;
        }
    </style>
    
</head>
<body>
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
     
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">

        <table border="0" cellspacing="0" cellpadding="2" width="99%" id="TABLE1" class="font9Tahoma">
            <tr>
                <td colspan="5">
                    <UserControl:MenuPDTrx ID="MenuPDTrx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="mt-h" colspan="5">
                    DETAIL PENGIRIMAN TBS</td>
            </tr>
            <tr>
                <td colspan="5">
                    <hr size="1" noshade>
                </td>
            </tr>
            <tr>
                <td height="25" style="width: 155px">
                    No.SPB :
                </td>
                <td style="width: 293px">
                    <asp:Label ID="lblSPB" runat="server" />
               </td>
                <td>
                    &nbsp;</td>
                <td width="15%">
                    Periode :
                </td>
                <td style="width: 330px">
                    <asp:Label ID="lblPeriod" runat="server" /></td>
            </tr>
            <tr>
                <td style="height: 27px; width: 155px;">
                    No.Referensi :
                </td>
                <td style="width: 293px; height: 27px">
                    <asp:TextBox ID="txtNoRef" runat="server" MaxLength="25" Width="100%"></asp:TextBox>
				</td>
                <td style="height: 27px">
                    &nbsp;</td>
                <td style="height: 27px">
                    Status :
                </td>
                <td style="height: 27px; width: 330px;">
                    <asp:Label ID="lblStatus" runat="server" /></td>
            </tr>
            <tr>
                <td style="height: 29px; width: 155px;">
                    Tanggal Timbang : 
                </td>
                <td style="width: 293px; height: 29px;">
                    <asp:TextBox ID="txtWPDate" runat="server" MaxLength="10" Width="30%"></asp:TextBox>
                    <a href="javascript:PopCal('txtWPDate');">
                    <asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
					
				</td>
                <td style="height: 29px">
                    &nbsp;</td>
                <td style="height: 29px">
                    Tgl Buat :
                </td>
                <td style="width: 330px; height: 29px;">
                    <asp:Label ID="lblDateCreated" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 155px">
                    Divisi : </td>
                <td style="width: 293px; height: 28px">
                    <asp:DropDownList ID="ddldivisi" runat="server" Width="50%" />
				</td>
                <td>
                    &nbsp;</td>
                <td>
                    Tgl Update :
                </td>
                <td style="width: 330px;">
                    <asp:Label ID="lblLastUpdate" runat="server" /></td>
            </tr>
            
            <tr>
                <td style="height:28px; width: 155px;">
                    No.Polisi - Supir :
				</td>
                <td style="width: 293px; height: 28px;">
                    <asp:TextBox ID="txtnopolisi" runat="server" MaxLength="10" Width="24%"></asp:TextBox> - <asp:TextBox ID="txtsupir" runat="server" MaxLength="10" Width="50%"></asp:TextBox>
				</td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="height: 28px">
                    Diupdate:
                </td>
                <td style="width: 330px; height: 28px;"><asp:Label ID="lblUpdatedBy" runat="server" /></td>
            </tr>
			
			<tr>
                <td style="height:28px; width: 155px;">
                    Jam Masuk-Keluar :</td>
                <td style="width: 293px; height: 28px;">
                    <asp:TextBox ID="txtmasuk" runat="server" MaxLength="5" Width="24%"></asp:TextBox> - <asp:TextBox ID="txtkeluar" runat="server" MaxLength="5" Width="24%"></asp:TextBox>
				</td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="height: 28px"></td>
                <td style="width: 330px; height: 28px;"></td>
            </tr>
            
            <tr>
                <td style="height:28px; width: 155px;">
                    Tujuan Pabrik :</td>
                <td style="width: 293px; height: 28px;">
                    <asp:DropDownList ID="ddlMill" runat="server" Width="50%"/></td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="height: 28px"></td>
                <td style="width: 330px; height: 28px;"></td>
            </tr>
            
            
            <tr>
                <td colspan="5" style="height: 107px; width: 995px;">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" style="height: 67px" class="font9Tahoma">
					<tr>
                            <td valign="top" style="width: 50%;border-right: #009edb 1px dotted; border-top: #009edb 1px dotted;
                                            border-left: #009edb 1px dotted; border-bottom: #009edb 1px dotted; background-color: transparent;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma">
                                    <tr>
                                        <td colspan="3" align="center" style="border-right: #009edb 1px dotted; border-top: #009edb 1px dotted;
                                            border-left: #009edb 1px dotted; border-bottom: #009edb 1px dotted; background-color: transparent;">
                                            Timbangan Pabrik
                                         </td>
                                    </tr>
									
                                    <tr>
                                        <td style="width: 124px">
                                            Bruto</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtBruto2" runat="server" MaxLength="10" Width="90px" onkeyup="callNetto2()" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Tarra</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtTarra2" runat="server" MaxLength="10" Width="90px" onkeyup="callNetto2()" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Netto</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtNetto2" CssClass="mr-h" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" ReadOnly=true></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px; height: 24px;">
                                            Potongan Kg</td>
                                        <td style="width: 41px; height: 24px;">
                                            <asp:TextBox ID="TxtPotKg" runat="server" MaxLength="10" Width="90px" onkeyup="callNetto2()" onkeypress="return isNumberKey(event)" ReadOnly=true CssClass="mr-h"></asp:TextBox></td>
                                        <td style="width: 151px; height: 24px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Kg di Terima</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtNetto"  runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" ReadOnly=true CssClass="mr-h"></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Total Janjang</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtJJG" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td style="width: 151px">
                                            Buah Kecil &nbsp; &nbsp;<asp:TextBox ID="TxtBKecil" runat="server" MaxLength="10" Width="80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            BJR/Komidel</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtBJR" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td style="width: 151px">
                                            Buah Besar &nbsp;<asp:TextBox ID="TxtBBesar" runat="server" MaxLength="10" Width="80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" style="border-right: #009edb 1px dotted; border-top: #009edb 1px dotted;
                                            border-left: #009edb 1px dotted; border-bottom: #009edb 1px dotted; background-color: transparent;">
                                            Potongan</td>
                                    </tr>
									<tr>
                                        <td style="width: 124px; height: 19px">
                                            Type Pot </td>
                                        <td style="width: 41px; height: 19px">
                                            <asp:CheckBox ID="chkpot" runat="server" Text=" KG" /></td>
                                        <td style="width: 151px; height: 19px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px; height: 19px">
                                            Buah Mentah</td>
                                        <td style="width: 41px; height: 19px">
                                            <asp:TextBox ID="TxtPotBM" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callPotongan()" ></asp:TextBox></td>
                                        <td style="width: 151px; height: 19px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px; height: 24px;">
                                            Buah Busuk</td>
                                        <td style="width: 41px; height: 24px;">
                                            <asp:TextBox ID="TxtPotBB" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callPotongan()"></asp:TextBox></td>
                                        <td style="width: 151px; height: 24px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Sampah/Pasir</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtPotSP" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callPotongan()"> </asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Air</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtPotAR" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callPotongan()"></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Potongan Lain</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtPotPL" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callPotongan()" ></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            Total Potongan</td>
                                        <td style="width: 41px">
                                            <asp:TextBox ID="TxtPot" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" ReadOnly=true CssClass="mr-h"></asp:TextBox></td>
                                        <td style="width: 151px">
                                        </td>
                                    </tr>
                                </table>
							</td>
							<td valign="top" style="width: 49%;border-right: green 1px dotted; border-top: green 1px dotted;
                                            border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma">
                                    <tr>
                                        <td colspan="3" align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                              border-left:green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;">
                                            Timbangan Kebun</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 85px">
                                            Bruto</td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="TxtBruto1" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callNetto1()"></asp:TextBox></td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 85px">
                                            Tarra</td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="TxtTarra1" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)" onkeyup="callNetto1()"></asp:TextBox></td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 85px">
                                            Netto</td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="TxtNetto1" runat="server" MaxLength="10" Width="90px" ReadOnly=true CssClass="mr-h"></asp:TextBox></td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
									<tr>
                                        <td style="width: 85px">
                                            JJG</td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="TxtJJgKBN" runat="server" MaxLength="10" Width="90px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                </table>
							</td>
					</tr>
					</table>
                </td>
            </tr>
			<tr>
                <td colspan="5" style="height: 30px;">
				<asp:Label ID="lblErrMessage" Visible="false" ForeColor="red" runat="server" />
                </td>
            </tr>
			 
            <tr>
                <td colspan="5">
					<asp:ImageButton ID="NewBtn" OnClick="NewBtn_Click" AlternateText="  New  "  ImageUrl="../../images/butt_new.gif" runat="server" />
                    <asp:ImageButton ID="SaveBtn" OnClick="SaveBtn_Click" AlternateText="  Save  "  ImageUrl="../../images/butt_save.gif" runat="server" />
                    <asp:ImageButton ID="DeleteBtn" OnClick="DeleteBtn_Click" AlternateText=" Close "  ImageUrl="../../images/butt_delete.gif" runat="server" />
                    <asp:ImageButton ID="BackBtn" OnClick="BackBtn_Click" AlternateText="  Back  " CausesValidation="False" ImageUrl="../../images/butt_back.gif" runat="server" />
					<asp:ImageButton ID="GenBtn" OnClick="GenBtn_Click" AlternateText="  Generate BJR/KG Blok  " CausesValidation="False" ImageUrl="../../images/butt_generate.gif" runat="server" />
                </td>
            </tr>
			 
            <tr>
                <td colspan="5">
                                            &nbsp;</td>
            </tr>
            </table>


            <table style="width: 100%" class="font9Tahoma">
            <tr>
                <td colspan="6" style="height: 42px; width: 995px;">
                    <table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
                        <tr>
                            <td align="center" style="border-right: #009edb 1px dotted; border-top: #009edb 1px dotted;
                                border-left: #009edb 1px dotted; border-bottom: #009edb 1px dotted; background-color: #009edb" colspan="8" >
                                Detail Block
                              </td>
                        </tr>
                        <tr>
							<td style="width: 82px">
								Tgl.Panen</td>
							<td style="width: 429px">	
								<asp:TextBox ID="txtWPProd" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
							    <a href="javascript:PopCal('txtWPProd');">
								<asp:Image ID="btnSelDate2" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
						    </td>
                            <td style="width: 82px">
                                Block</td>
                            <td class="style1"><asp:DropDownList ID="ddlblock_det" runat="server" Width="50%"/></td>
                            <td>
                                &nbsp;Janjang</td>
                            <td style="width: 96px">
                                <asp:TextBox ID="txtjjg_det" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"  Width="50px"></asp:TextBox></td>
						    <td style="width: 130px">
                                &nbsp;Brondolan</td>
                            <td style="width: 96px">
                                <asp:TextBox ID="txtbrd_det" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"  Width="90px"></asp:TextBox></td>
                            <td style="width: 190px">
							  <asp:ImageButton ID="ImageButton1" OnClick="AddBlock_Click"  AlternateText="  Add  "  ImageUrl="../../images/butt_add.gif" runat="server" />
							</td>
                        </tr>
                        
                        <tr>
                            <td colspan="9">
                                <asp:DataGrid ID="dgOvtDet" 
								    AutoGenerateColumns="false" 
									width="50%" 
									runat="server"
									GridLines = none
									Cellpadding = "2"
									OnDeleteCommand=DEDR_Delete 
									OnEditCommand="DEDR_Edit"
									OnUpdateCommand="DEDR_Update"
									OnCancelCommand="DEDR_Cancel"
									OnItemDataBound=KeepRunningSum 
									ShowFooter=True 
									AllowPaging="False" 
									Allowcustompaging="False" 
									Pagerstyle-Visible="False"
                                    class="font9Tahoma">	
							 
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
									
									<asp:TemplateColumn HeaderText="Tgl.Panen">
                                            <ItemTemplate>
                                                <%# objGlobal.GetShortDate(strDateFormat,Container.DataItem("Dateprod")) %>
                                            </ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="lbldate_ed" Text='<%# objGlobal.GetShortDate(strDateFormat,Container.DataItem("Dateprod")) %>' runat="server"/>
												<asp:label id="lblDupMsg" Text="Date already exist" Visible = false forecolor=red Runat="server"/>
							    			</EditItemTemplate>
                                        </asp:TemplateColumn>
										
                                        <asp:TemplateColumn HeaderText="Block">
                                            <ItemTemplate>
                                                <%# Container.DataItem("CodeBlok") %>
                                            </ItemTemplate>
											<EditItemTemplate>
												<asp:Label ID="lblblock_ed" Text='<%# Container.DataItem("CodeBlok") %>' runat="server"/>
												<asp:Label ID="lblSPB" visible=False runat="server" Text='<%# Container.DataItem("CodeSPB") %>'></asp:Label>
							    			</EditItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Janjang" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# objGlobal.GetIDDecimalSeparator_FreeDigit((Container.DataItem("JJg")),0) %>
                                            </ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="lblQty_ed" Text='<%# Container.DataItem("JJg") %>' width=50px onkeypress="javascript:return isNumberKey(event)" runat="server"/>
							    			</EditItemTemplate>
										<FooterTemplate >
									    <asp:Label ID=lbTotal runat=server />
										</FooterTemplate>
										<ItemStyle HorizontalAlign="Right" />
										<FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                        </asp:TemplateColumn>
										
										<asp:TemplateColumn HeaderText="Brondolan" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# objGlobal.GetIDDecimalSeparator_FreeDigit((Container.DataItem("Brondolan")),2) %>
                                            </ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="lblBrd_ed" Text='<%# Container.DataItem("Brondolan") %>' width=70px onkeypress="javascript:return isNumberKey(event)" runat="server"/>
							    			</EditItemTemplate>
										<FooterTemplate >
									    <asp:Label ID=lbTotal runat=server />
										</FooterTemplate>
										<ItemStyle HorizontalAlign="Right" />
										<FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                        </asp:TemplateColumn>
										
										<asp:TemplateColumn HeaderText="BJR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# (Container.DataItem("Bjr")) %>
                                            </ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="lblBjr_ed" Text='<%# Container.DataItem("Bjr") %>' width=50px onkeypress="javascript:return isNumberKey(event)" runat="server"/>
							    			</EditItemTemplate>
										<ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
										
										<asp:TemplateColumn HeaderText="KG" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# Container.DataItem("Kg") %>
                                            </ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="lblKg_ed" Text='<%# Container.DataItem("Kg") %>' width=70px runat="server"/>
							    			</EditItemTemplate>
										<FooterTemplate >
									    <asp:Label ID=lbTotal runat=server />
										</FooterTemplate>
										<ItemStyle HorizontalAlign="Right" />
										<FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                        </asp:TemplateColumn>
										
										<asp:TemplateColumn>
                                            <ItemTemplate>
												<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>								
											</ItemTemplate>	
											<EditItemTemplate>
												<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
												<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
												<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
											</EditItemTemplate>
                                        </asp:TemplateColumn>
														
                                    </Columns>
                                    <PagerStyle Visible="False" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
            </tr>
            <tr>
                <td colspan="5">
                    <hr size="1" noshade>
                </td>
            </tr>
        </table>
        <input type="hidden" id="isNew" value="" runat="server" />
        &nbsp;&nbsp;

        <br />
        </div>
        </td>
        </tr>
        </table>


    </form>
</body>
</html>
