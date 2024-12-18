<%@ Page Language="vb" src="../../../include/PR_setup_PrmiLainDet_Estate.aspx.vb" Inherits="PR_setup_PrmiLainDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<html>
	<head>
		<title>Premi Lain </title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
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
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">
                      <strong> PREMI LAIN SETUP</strong></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td style="width: 320px; height: 25px;">
                        ID :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtid" CssClass="mr-h" ReadOnly=true runat="server" MaxLength="8" Width="78%"></asp:TextBox>
                    &nbsp;<td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Status :</td>
					<td width=25% style="height: 25px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 25px;">
                        Keterangan :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtket" CssClass="mr-h" runat="server"  Width="78%"></asp:TextBox>
                    &nbsp;<td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Date Created : </td>
					<td width=25% style="height: 25px"><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 25px;">
                        IDLN :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtidln" ReadOnly=true runat="server" MaxLength="8" Width="45%"/> 
					&nbsp;<td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 28px">Last Updated :&nbsp;</td>
					<td width=25% style="height: 28px"><asp:Label id=lblLastUpdate runat=server /></td>									
				</tr>		
				<tr>
					<td style="width: 320px; height: 25px;">
                        Periode Start-End (mmyyyy) :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtpstart" runat="server" MaxLength="6" Width="45%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>-<asp:TextBox
                            ID="txtpend" runat="server" MaxLength="6" Width="50%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox><td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Update By : </td>
					<td width=25% style="height: 25px"><asp:Label id=lblUpdatedBy runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 28px;">
                        Rate  :</td>
					<td style="width: 296px; height: 28px;">                        
                        <asp:TextBox ID=txtrate runat="server"  Width="80%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox>&nbsp;
					<td width=20% style="height: 28px">&nbsp;</td>
					<td width=25% style="height: 28px"></td>								
				</tr>		
				<tr>
					<td style="width: 320px">
					<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
					</td>
					<td style="width: 296px"></td>
					<td width=20%></td>
					<td width=25%></td>								
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
							    <asp:TemplateColumn HeaderText="IDLN">
									<ItemTemplate>
										<asp:LinkButton ID="lbID" runat="server" CommandName="Item" Text='<%# Container.DataItem("IDLN") %>'></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								
							    <asp:TemplateColumn HeaderText="Periode Start">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("PeriodeStart") %> id="lblPeriodeStart" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Periode End">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("PeriodeEnd") %> id="lblPeriodeEnd" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
														
								
								<asp:TemplateColumn HeaderText="Rate" HeaderStyle-HorizontalAlign=Right>
								    <ItemStyle HorizontalAlign="Right" />		
									<ItemTemplate>								
									    <asp:Label Text=<%# Container.DataItem("Rate") %> id="lblRate"  runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>					
									<ItemTemplate>
										<asp:HiddenField ID="hidjid" Value=<%# Container.DataItem("ID") %> runat=server />
										<asp:HiddenField ID="hidjidln" Value=<%# Container.DataItem("IDLN") %> runat=server />
										<asp:HiddenField ID="hidjpstart" Value=<%# Container.DataItem("PeriodeStart") %> runat=server />
										<asp:HiddenField ID="hidjpend" Value=<%# Container.DataItem("PeriodeEnd") %> runat=server />
										<asp:HiddenField ID="hidjrate" Value=<%# Container.DataItem("Rate") %> runat=server />
										
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
                        <asp:ImageButton ID="DelBtn" runat="server" AlternateText=" Delete " CausesValidation="False"
                            ImageUrl="../../images/butt_delete.gif" OnClick="Delete_Click" />
                        <asp:ImageButton ID="BackBtn" runat="server" AlternateText="  Back  " CausesValidation="False"
                            ImageUrl="../../images/butt_back.gif" OnClick="BackBtn_Click" />
					</td>
				</tr>
				<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/><asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<tr>
					<td style="height: 28px">
                                            &nbsp;</td>
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


		</form>
	</body>
</html>
