<%@ Page Language="vb" codefile="../../../include/PR_Trx_OutOfDuty_EstateDet.aspx.vb" Inherits="PR_Trx_OutOfDuty_EstateDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>PERMINTAAN TUNJANGAN OPERASIONAL </title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                height: 25px;
                width: 20%;
            }
            .style2
            {
                width: 20%;
            }
            .style4
            {
                height: 25px;
            }
            .style5
            {
                height: 25px;
                width: 21%;
            }
            .style6
            {
                width: 21%;
            }
            .style7
            {
                height: 28px;
                width: 21%;
            }
            .style8
            {
                height: 25px;
                width: 374px;
            }
            .style9
            {
                height: 28px;
                width: 374px;
            }
        </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">
				<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
				<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
				<asp:Label id="lblSelect" visible="false" text="Please select " runat="server" />
				<asp:Label id="lblList" visible="false" text="Select " runat="server" />
			
					<table border=0 cellspacing=2 cellpadding=4 width=100% class="font9Tahoma">
						<tr>
							<td colspan="5">
								<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
							</td>
						</tr>
						<tr>                    
							<td class="mt-h" colspan="5">
							<strong> PERMINTAAN TUNJANGAN OPERASIONAL</strong></td>
						</tr>
						<tr>
							<td colspan=5><hr style="width :100%" /></td>
						</tr>
						<tr>
							<td class="style8">
								Document ID :</td>
							<td style="height: 25px;" colspan="2">                        
								<asp:TextBox ID="txtid" CssClass="mr-h" ReadOnly=true runat="server" 
									MaxLength="8" Width="95%"></asp:TextBox>
							<td class="style5"> Status :</td>
							<td width=25% style="height: 25px"><asp:Label id=lblStatus runat=server /></td>								
						</tr>		
						<tr>				
							<td class="style8">
								Tujuan * :
							</td>					
							<td valign=bottom class="style4" colspan="2">
								<asp:DropDownList width="95%" id="ddlTujuan" CssClass="fontObject" runat=server>						
								<asp:ListItem value="1">Perjalanan Dinas</asp:ListItem>
								<asp:ListItem value="2">Tujuan Lainnya</asp:ListItem>						
							</asp:DropDownList></td>
							<td class="style5">
								Create Created :</td>
							<td class="style4">
								<asp:Label id=lblDateCreated runat=server />
							</td>
						</tr>                                    	
						<tr>
										
							<td class="style8">
								Nama * :
							</td>					
							<td valign=bottom colspan="2">
								<telerik:RadComboBox  CssClass="fontObject" ID="radcmbEmp"
									Runat="server" AllowCustomText="True" 
									EmptyMessage="Pilih Nama Karyawan " Height="200" Width="95%" 
									ExpandDelay="50" Filter="Contains" Sort="Ascending" 
									EnableVirtualScrolling="True">
									<CollapseAnimation Type="InQuart" />
								</telerik:RadComboBox>	
							</td>
							<td class="style6">
								Last Updated :&nbsp;</td>
							<td>
								<asp:Label id=lblLastUpdate runat=server />
							</td>
						</tr>
						<tr>
							<td class="style8">
									Periode Dinas :
							</td>					
							<td width="12%" height="26">
								<telerik:RadDatePicker ID="dtDateFr"  Runat="server" Culture="en-US">
									<DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput> 
								</telerik:RadDatePicker>
							</td>
							<td class="style2">
							</td>
							<td class="style6">
								Update By : 
							</td>
							<td>
								<asp:Label id=lblUpdatedBy runat=server />
							</td>
						</tr>
						<tr>
							<td class="style8">
									Sampai :
							</td>					
							<td width="12%" height="26">
								<telerik:RadDatePicker ID="dtDateTo"  Runat="server" Culture="en-US">
									<DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput> 
								</telerik:RadDatePicker>								
							</td>
							<td class="style2">
							</td>
							<td class="style6">
							</td>
							<td>
							</td>
						</tr>
						<tr>
							<td class="style8" valign="top">
								Keterangan/ Keperluan * :</td>
							<td style="height: 25px;" colspan="2">                        
								<asp:TextBox ID="txtNote"  runat="server"  Width="95%" Height="100px" 
                                    TextMode="MultiLine"  /> 
							<td class="style7">&nbsp;</td>
							<td width=25% style="height: 28px">&nbsp;</td>									
						</tr>		
						<tr>
							<td style="height: 25px;" colspan="5">
								<strong> <u>PEMAKAIAN FASILITAS KANTOR</u> </strong></td>
						</tr>		
						<tr>
							<td class="style8">
								Kendaraan (No.Polisi) :</td>
							<td style="width: 296px; height: 25px;">                        
								<asp:TextBox ID="txtVehCode" runat="server"  Width="100%"></asp:TextBox>
								&nbsp;</td>
							<td class="style1"> &nbsp;</td>
							<td class="style5">&nbsp;</td>								
						</tr>		
						<tr>
							<td class="style9">
								Peralatan Lainnya :</td>
							<td style="height: 28px;" colspan="2">                        
								<asp:TextBox ID=txtOtherVeh runat="server" TextMode="MultiLine" Height="50px"  Width="95%"></asp:TextBox>
							<td class="style7"></td>								
						</tr>		
						<tr>
							<td colspan="2">
								<asp:ImageButton id=btnNew imageurl="../../images/butt_New.gif" alternatetext=Add onclick=NewBtn_Click runat=server />
								<asp:ImageButton id=btnAdd imageurl="../../images/butt_save.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
								<asp:ImageButton id=BtnEdit imageurl="../../images/butt_edit.gif" alternatetext=Add onclick=btnEdit_Click runat=server />
								<asp:ImageButton id=btnConf imageurl="../../images/butt_confirm.gif" alternatetext=Add onclick=btnConfirmed_Click runat=server />
								<asp:ImageButton ID="DelBtn" runat="server" AlternateText=" Delete " CausesValidation="False" ImageUrl="../../images/butt_delete.gif" OnClick="Delete_Click" />
								<asp:ImageButton ID="BackBtn" runat="server" AlternateText="  Back  " CausesValidation="False"	ImageUrl="../../images/butt_back.gif" OnClick="BackBtn_Click" />
							</td>												
							<td class="style2"></td>
							<td class="style6"></td>								
						</tr>				
						</table>

						<table style="width: 100%" class="font9Tahoma">
						<tr>
							<td>
								<asp:DataGrid id=dgLineDet
									AutoGenerateColumns=false width="100%" runat=server
									GridLines=none
									OnDeleteCommand="DEDR_Delete"
									Cellpadding=2 
									OnItemDataBound="dgLineDet_BindGrid" 
									OnItemCommand="GetItem" 
									Pagerstyle-Visible=False
									AllowSorting="True"
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
										<asp:TemplateColumn HeaderText="Claim Document ID">
											<ItemTemplate>
												<asp:Label Text=<%# Container.DataItem("ClaimID") %> id="lblClaimID" runat="server" />
											</ItemTemplate>
										</asp:TemplateColumn>										
										
										<asp:TemplateColumn HeaderText="Tanggal Claim">
											<ItemTemplate>
												<%# objGlobal.GetLongDate(Container.DataItem("ClaimDate")) %>
											</ItemTemplate>
										</asp:TemplateColumn>

										<asp:TemplateColumn HeaderText="Jumlah (Rp)" HeaderStyle-HorizontalAlign=Right>
											<ItemStyle HorizontalAlign="Right" />		
											<ItemTemplate>
												<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Ammount"), 2), 2)%> 													
											</ItemTemplate>
										</asp:TemplateColumn>

										<asp:TemplateColumn HeaderText="Keterangan" HeaderStyle-HorizontalAlign=Right>
											<ItemStyle HorizontalAlign="Right" Width="250px"/>		
											<ItemTemplate>								
												<asp:Label Text=<%# Container.DataItem("Note") %> id="lblNote"  runat="server" />
											</ItemTemplate>
										</asp:TemplateColumn>

										<asp:TemplateColumn HeaderText="Update ID" HeaderStyle-HorizontalAlign=Right>
											<ItemStyle HorizontalAlign="Right" />		
											<ItemTemplate>								
												<asp:Label Text=<%# Container.DataItem("UpdateID") %> id="lblUpdateID"  runat="server" />
											</ItemTemplate>
										</asp:TemplateColumn>

										<asp:TemplateColumn HeaderText="Action" HeaderStyle-HorizontalAlign=Center> 
											<ItemStyle HorizontalAlign="Center" />						
											<ItemTemplate>
												<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
											</ItemTemplate>
										</asp:TemplateColumn>
							
										</Columns>
								</asp:DataGrid>
							</td>
						</tr>	
					
						<td style="height: 23px">&nbsp;</td>
						<tr>
							<td style="height: 28px">

							</td>
						</tr>
						<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/><asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
						<tr>
							<td style="height: 28px">&nbsp;</td>
						</tr>
				    </table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=isNew value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidPMdrLnID value="" runat=server/>


       <br />
        </div>
        </td>
        </tr>
        </table>
		<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

		</form>
	</body>
</html>
