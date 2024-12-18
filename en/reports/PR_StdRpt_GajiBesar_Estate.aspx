<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_GajiBesar_Estate.aspx.vb" Inherits="PR_StdRpt_GajiBesar_Estate" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl_Estate.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Detailed Employee Payslip</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="3">PAYROLL - ADVANCE PAYMENT LISTING</td>
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
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="0" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td colspan="7">
					&nbsp;
					</td>
				</tr>
				
				<tr>
					<td colspan=7><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				
				<tr>
					<td style="height: 24px; width: 162px;">Divisi :</td>
					<td width=39% style="height: 24px"><GG:AutoCompleteDropDownList id="ddldivisi" width="100%" runat=server/></td>					
					<td width=4% style="height: 24px"></td>	
					<td width=40% style="height: 24px"></td>					
				</tr>
				<tr>
					<td style="width: 162px">Mandor :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddlmandor" width="100%" runat=server /></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td style="width: 162px">Karyawan :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddlempcode" width="100%" runat=server/></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td style="width: 162px">Tipe Karyawan :</td>
					<td width=39%><GG:AutoCompleteDropDownList id="ddlemptype" width="100%" runat=server/></td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>
				<tr>
					<td style="width: 162px">Periode :</td>
					<td width=39%><asp:DropDownList id="ddlMonth" width="30%" runat=server>
										<asp:ListItem value="1">Januari</asp:ListItem>
										<asp:ListItem value="2">Februari</asp:ListItem>
										<asp:ListItem value="3">Maret</asp:ListItem>
										<asp:ListItem value="4">April</asp:ListItem>
										<asp:ListItem value="5">Mei</asp:ListItem>
										<asp:ListItem value="6">Juni</asp:ListItem>
										<asp:ListItem value="7">Juli</asp:ListItem>
										<asp:ListItem value="8">Augustus</asp:ListItem>
										<asp:ListItem value="9">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">Desember</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id="ddlyear" width="20%" runat=server></asp:DropDownList>
					</td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>

	            <tr>
					<td style="width: 162px">Type Report :</td>
					<td width=39%><asp:DropDownList id="ddlTypeRpt" width="30%" runat=server>
										<asp:ListItem value="1">Detail</asp:ListItem>
										<asp:ListItem value="2">Rekap</asp:ListItem>
									
									</asp:DropDownList>
									<asp:DropDownList id="DropDownList2" width="20%" runat=server></asp:DropDownList>
					</td>					
					<td width=4%></td>	
					<td width=40%></td>					
				</tr>

                <tr>
                    <td style="width: 162px">
                        <asp:CheckBox ID="cbExcel" runat="server" Checked="false" Text=" Export To Excel" /></td>
                </tr>
				<tr>
					<td style="width: 162px"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />
                    
					</td>					
					
				</tr>	
				<tr>
				<td colspan="7" ><asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." forecolor="Red" runat="server" /></td>
				</tr>
                 <tr>
					<td colspan="7" style="height:25px">
                        <hr style="width :100%" /> 
                     </td>
				</tr>
			
			</table>
			
			<table cellspacing="0" cellpadding="0" border="0" width="100%" id="TABLEDET" >
			<tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted;background-color: transparent;" colspan="7">
                                View Detail Gaji Besar 
                              </td>
            </tr>
			 <tr>
					<td colspan="7" style="height:25px">&nbsp;</td>
			</tr>
			
			<tr class="mb-t">
							        <td width="10%" height="26" valign=bottom>Periode :<BR>
							        <asp:DropDownList id="ddlbulan" width="55%" runat=server>
										<asp:ListItem value="01">January</asp:ListItem>
										<asp:ListItem value="02">February</asp:ListItem>
										<asp:ListItem value="03">March</asp:ListItem>
										<asp:ListItem value="04">April</asp:ListItem>
										<asp:ListItem value="05">May</asp:ListItem>
										<asp:ListItem value="06">June</asp:ListItem>
										<asp:ListItem value="07">July</asp:ListItem>
										<asp:ListItem value="08">Augustus</asp:ListItem>
										<asp:ListItem value="09">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddltahun width="40%" runat="server" />
							        </td>
									<td width="10%" height="26" valign=bottom>Divisi :<BR><asp:DropDownList id="ddlDiv" width=100% runat="server" /></td>							
									<td width="10%" height="26" valign=bottom>Emp Type    :<BR><asp:DropDownList id="ddlType" width=100% runat="server" /></td>
								    <td width="10%" height="26" valign=bottom>Grp Jabatan :<BR><asp:DropDownList id="ddlGrp" width=100% runat="server" /></td>
							    	<td width="10%" height="26" valign=bottom>Jabatan     :<BR><asp:DropDownList id="ddlJbt" width=100% runat="server" /></td>
								    <td width="8%" height="26" valign=bottom>
								    </td>
							    </tr>
				<tr class="mb-t">
								    <td colspan=7  height="26" valign=bottom>
								    <asp:Button id=SearchBtn Text="View Detail "  OnClick="ViewBtn_OnClick" runat="server"/>
									<asp:Button id=SearchBtn2 Text="View Rekap "  OnClick="ViewRkpBtn_OnClick" runat="server"/>
								    <asp:Button id=SubmitBtn Text="Export Excel"  OnClick="ExportBtn_OnClick" runat="server"/>
								    </td>
				</tr>
				<tr>
					<td colspan=7 style="height: 5px">&nbsp;</td>
				</tr>
				
            <tr>
				<td colspan="7">
					 <div id="divgd" style="width:1050px;height:400px;overflow: auto;">
					
							  <asp:DataGrid ID="dgpay" runat="server" 
                               CellPadding="1" GridLines="None" widht="100%" 
							   OnItemDataBound="dgpay_BindGrid" 
							   AutoGenerateColumns=false >
                              <AlternatingItemStyle CssClass="mr-r" />
                              <ItemStyle CssClass="mr-l" />
                              <HeaderStyle CssClass="mr-h" />
                              <Columns>
                                <asp:TemplateColumn HeaderText="NIK">
									<ItemTemplate>
										<asp:label ID="dgnik" Width="120px"  Text='<%#Container.DataItem("empcode")%>' runat="server" /> 
										<asp:label ID="dgid"  Width="100px"  visible=false Text='<%#Container.DataItem("ID")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama">
									<ItemTemplate>
									<asp:label ID="dgnama" Width="150px"   Text='<%#Container.DataItem("empname")%>' runat="server" /> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ket">
									<ItemTemplate>
									<asp:label ID="dgket" Width="200px"   Text='<%#Container.DataItem("Ket")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
									<asp:label ID="dgtype" Width="50px"  Text='<%#Container.DataItem("codeempty")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
									<asp:label ID="dgdivisi" Width="50px"  Text='<%#Container.DataItem("idDiv")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Group">
									<ItemTemplate>
									<asp:label ID="dggroup" Width="50px"  Text='<%#Container.DataItem("codegrpjob")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Jabatan">
									<ItemTemplate>
									<asp:label ID="dgjabatan" Width="120px"  Text='<%#Container.DataItem("Jabatan")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								<asp:TemplateColumn HeaderText="HK">
									<ItemTemplate>
									<asp:label ID="dgt1hk" Width="50px"  Text='<%# Container.DataItem("T1HK") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								
								<asp:TemplateColumn HeaderText="Rate Hk">
									<ItemTemplate>
									<asp:label ID="dgratet1" Width="50px"  Text='<%# Container.DataItem("UpahT1Hk")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
										
								
								<asp:TemplateColumn HeaderText="Upah Hk">
									<ItemTemplate>
									<asp:label ID="dgupaht1" Width="80px"  Text='<%# Container.DataItem("GajiT1Hk")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								
								<asp:TemplateColumn HeaderText="Bor">
									<ItemTemplate>
									<asp:label ID="dgt1kg" Width="50px"  Text='<%# Container.DataItem("T1Kg")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								<asp:TemplateColumn HeaderText="Rate Bor">
									<ItemTemplate>
									<asp:label ID="dgratet1bor" Width="50px"  Text='<%# Container.DataItem("UpahT1Kg") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
										
								
								<asp:TemplateColumn HeaderText="Upah Bor">
									<ItemTemplate>
									<asp:label ID="dgupaht1bor" Width="80px"  Text='<%# Container.DataItem("GajiT1Kg")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								
								<asp:TemplateColumn HeaderText="Gaji Gol">
									<ItemTemplate>
									<asp:label ID="dggajigol" Width="80px"  Text='<%# Container.DataItem("GajiGol") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Gaji HK">
									<ItemTemplate>
									<asp:label ID="dggajihk" Width="80px"  Text='<%# Container.DataItem("GajiHk") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Gaji Bor">
									<ItemTemplate>
									<asp:label ID="dgupahbor" Width="80px"  Text='<%# Container.DataItem("GajiBor")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Premi Tatap">
									<ItemTemplate>
									<asp:label ID="dgptetap" Width="80px"  Text='<%# Container.DataItem("PremiTetap")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Makan">
									<ItemTemplate>
									<asp:label ID="dgmakan" Width="80px"  Text='<%#Container.DataItem("makan")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Beras">
									<ItemTemplate>
									<asp:label ID="dgberas" Width="80px"  Text='<%#Container.DataItem("beras")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Lembur">
									<ItemTemplate>
									<asp:label ID="dglmbr" Width="80px"  Text='<%#Container.DataItem("lembur")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Premi">
									<ItemTemplate>
									<asp:label ID="dgppremi" Width="80px"  Text='<%#Container.DataItem("Premi")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Sub.Gaji">
									<ItemTemplate>
									<asp:label ID="dgsgaji" Width="80px"  Text='<%# Container.DataItem("SubGaji") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pot JHT">
									<ItemTemplate>
									<asp:label ID="dgpastek" Width="50px"  Text='<%# Container.DataItem("potastek")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pot.Pinjaman">
									<ItemTemplate>
									<asp:label ID="dgppjm" Width="80px"  Text='<%# Container.DataItem("potGajiKecil")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pot.Kantin">
									<ItemTemplate>
									<asp:label ID="dgpkantin" Width="80px"  Text='<%# Container.DataItem("potkantin")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pot.Pribadi">
									<ItemTemplate>
									<asp:label ID="dgpangsur" Width="80px"  Text='<%# Container.DataItem("potangsur")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pot.Mangkir">
									<ItemTemplate>
									<asp:label ID="dgpmangkir" Width="80px"  Text='<%# Container.DataItem("potmangkir")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Total.Gaji">
									<ItemTemplate>
									<asp:label ID="dgptotgaji" Width="90px"  Text='<%# Container.DataItem("Totgaji")%>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								
								</Columns>
                            </asp:DataGrid>
							</div>
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
