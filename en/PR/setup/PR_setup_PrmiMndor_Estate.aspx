<%@ Page Language="vb" src="../../../include/PR_setup_PrmiMndor_Estate.aspx.vb" Inherits="PR_setup_PrmiMndor_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<html>
	<head>
		<title>ASTEK Details</title>
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
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
                        PREMI MANDOR SETUP</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td style="width: 320px; height: 25px;">
                        ID :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtid" CssClass="mr-h" ReadOnly=true runat="server" MaxLength="64" Width="78%"></asp:TextBox>
                    &nbsp;<td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Status :</td>
					<td width=25% style="height: 25px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 25px;">
                        Periode Start-End (mmyyyy) :</td>
					<td style="width: 296px; height: 25px;">                        
                        <asp:TextBox ID="txtpstart" runat="server" MaxLength="6" Width="45%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>-<asp:TextBox
                            ID="txtpend" runat="server" MaxLength="6" Width="50%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox><td width=5% style="height: 25px">&nbsp;</td>
					<td width=20% style="height: 25px"> Date Created : </td>
					<td width=25% style="height: 25px"><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px; height: 28px;">
                        Mandor Panen [%] :</td>
					<td style="width: 296px; height: 28px;">                        
                        <asp:TextBox ID=txtPMP runat="server" MaxLength="64" Width="80%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox>&nbsp;
                        <td width=5% style="height: 28px">&nbsp;</td>
					<td width=20% style="height: 28px">Last Updated :&nbsp;</td>
					<td width=25% style="height: 28px"><asp:Label id=lblLastUpdate runat=server /></td>								
				</tr>		
				<tr>
					<td style="width: 320px">
                        Mandor Brondolan [%] :</td>
					<td style="width: 296px">                        
                        <asp:TextBox ID=txtPMB runat="server" MaxLength="64" Width="80%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox>&nbsp;
                        <td width=5%>&nbsp;</td>
					<td width=20%>Update By :</td>
					<td width=25%><asp:Label id=lblUpdatedBy runat=server /></td>								
				</tr>				
				<tr>
					<td style="width: 320px;">
                        Mandor KCS [%]</td>
					<td align="left" style="width: 296px">
                        <asp:Textbox id=txtPKP maxlength=64 width="80%" runat=server onkeypress="javascript:return isNumberKey(event)">0</asp:Textbox></td>
					<td width=5% >&nbsp;</td>
					<td width=20%></td>
					<td width=25%></td>								
				</tr>
				<tr>
					<td align="left" style="width: 320px">
                        Mandor Traksi Kebun</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtPMT" runat="server" MaxLength="64" Width="80%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox></td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>		
				<tr>
					<td align="left" style="width: 320px">
                        Krani Timbangan [Rp/Ton]</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtPKT" runat="server" MaxLength="64" Width="80%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox></td>
					<td>&nbsp;</td>					
				</tr>
				
				<tr>
					<td align="left" style="width: 320px">
                        Mandor Sipil [Rp/Hari]</td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtSPL" runat="server" MaxLength="64" Width="80%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox></td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>			
	
				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td align="left" style="width: 320px">
                        <b>Mandor Rawat</b></td>
					<td align="left" style="width: 296px">
                        &nbsp;</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>			
				<tr>
                	<td colspan="5">
						<table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="2" border="0" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
									    <TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Initital Premi</TD>
											<td style="width: 30%; height: 26px;"><asp:TextBox ID="txtInit" MaxLength="8" width=50% runat="server"></asp:TextBox></TD>
											<td width=5% align=center style="height: 26px"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
                                                &nbsp;</TD>
										</TR>
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Description</TD>
											<td style="width: 30%; height: 26px;"><asp:TextBox ID="txtDesc" width=100% runat="server"></asp:TextBox></TD>
											<td width=5% align=center style="height: 26px"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
                                                &nbsp;</TD>
										</TR>
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Jenis Lahan</TD>
											<td style="width: 30%; height: 26px;">
											    <asp:DropDownList width=100% id=ddlLahan autopostback=false runat=server>
					                                <asp:ListItem value="1" Selected>DATAR</asp:ListItem>
					                                <asp:ListItem value="2">BERBUKIT</asp:ListItem>
					                                <asp:ListItem value="3">RAWA</asp:ListItem>
					                                <asp:ListItem value="9">OTHERS</asp:ListItem>
				                                </asp:DropDownList>
				                            </td>
											<td width=5% align=center style="height: 26px"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
                                                &nbsp;</TD>
										</TR>
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Basis</TD>
											<td style="width: 30%; height: 26px;">
											    <asp:TextBox ID="txtBasis" runat="server" MaxLength="64" Width="50%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox></TD>
											<td width=5% align=center style="height: 26px"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
                                                &nbsp;</TD>
										</TR>
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                UOM</TD>
											<td style="width: 30%; height: 26px;">
											    <asp:TextBox ID="txtUOM" runat="server" Width="50%"></asp:TextBox></TD>
											<td width=5% align=center style="height: 26px"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
                                                &nbsp;</TD>
										</TR>
										<TR class="mb-c">
											<td width=20% style="height: 26px">
                                                Rate</TD>
											<td style="width: 30%; height: 26px;">
											    <asp:TextBox ID="txtRate" runat="server" MaxLength="64" Width="50%" onkeypress="javascript:return isNumberKey(event)">0</asp:TextBox></TD>
											<td width=5% align=center style="height: 26px"> </td>
											<td width=15% style="height: 26px">
                                                </td>
											<td style="width: 30%; height: 26px;">
                                                &nbsp;</TD>
										</TR>
										<TR class="mb-c">
											<TD colspan=6 >
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
                                            </TD>    
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<td colspan="2" style="height: 23px">&nbsp;</td>
				<tr>
					<td align="left" style="width: 320px">
                        <b>Mandor Rawat Listing</b></td>
					<td align="left" style="width: 296px">
                        &nbsp;</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>		
				<tr>
					<td colspan="5">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							OnDeleteCommand="DEDR_Delete"
							OnCancelCommand="DEDR_Cancel"
							OnEditCommand="DEDR_Edit"
							Cellpadding=2 
							Pagerstyle-Visible=False
							AllowSorting="True" class="font9Tahoma">
							
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
							    <asp:TemplateColumn HeaderText="Initial Premi">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("RwtInit") %> id="lblRwtInit" runat="server" />
									    <asp:Label Text=<%# Container.DataItem("PMdrLnID") %> id="lblPMdrLnID" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
							    <asp:TemplateColumn HeaderText="Description">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("RwtDescr") %> id="lblRwtDescr" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
														
								<asp:TemplateColumn HeaderText="Jenis Lahan">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("RwtLahan") %> id="lblRwtLahan" Visible=false runat="server" />
									    <asp:Label Text=<%# Container.DataItem("RwtLahanDescr") %> id="lblIDRwtLahan" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Basis">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("RwtBasis") %> id="lblRwtBasis" Visible=false runat="server" />
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("RwtBasis"), 2), 2) %> id="lblIDRwtBasis" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								 <asp:TemplateColumn HeaderText="UOM">
									<ItemTemplate>
									   <asp:Label Text=<%# Container.DataItem("RwtUOM") %> id="lblRwtUOM" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rate" HeaderStyle-HorizontalAlign=Right>
								    <ItemStyle HorizontalAlign="Right" />		
									<ItemTemplate>								
									    <asp:Label Text=<%# Container.DataItem("RwtRate") %> id="lblRwtRate" Visible=false runat="server" />
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("RwtRate"), 2), 2) %> id="lblIDRwtRate" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn>					
									<ItemTemplate>
									    <asp:LinkButton id="lbEdit" CommandName="Edit" Text="Edit" CausesValidation=False runat="server"/>
										<asp:LinkButton id="lbCancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
										<asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
					
								</Columns>
						</asp:DataGrid>
					</td>
				</tr>	
               
				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
                        <asp:ImageButton ID="SaveBtn" runat="server" AlternateText="  Save  " CommandArgument="Save"
                            ImageUrl="../../images/butt_save.gif" OnClick="Button_Click" />
                        <asp:ImageButton ID="DelBtn" runat="server" AlternateText=" Delete " CausesValidation="False"
                            CommandArgument="Del" ImageUrl="../../images/butt_delete.gif" OnClick="Button_Click" />
                        <asp:ImageButton ID="UnDelBtn" runat="server" AlternateText="Undelete" CommandArgument="UnDel"
                            ImageUrl="../../images/butt_undelete.gif" OnClick="Button_Click" />
                        <asp:ImageButton ID="BackBtn" runat="server" AlternateText="  Back  " CausesValidation="False"
                            ImageUrl="../../images/butt_back.gif" OnClick="BackBtn_Click" />
					</td>
				</tr>
				<asp:Label id=lblNoRecord visible=false text="Premi Beras details not found." runat=server/><asp:Label id=lblHiddenSts visible=false text="0" runat=server/></table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=isNew value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidPMdrLnID value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
