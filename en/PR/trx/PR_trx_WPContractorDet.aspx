<%@ Page Language="vb" src="../../../include/PR_trx_WPContractorDet.aspx.vb" Inherits="PR_trx_WPContractorDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Work Performance Contractor Entry Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">        

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<asp:label id=lblSelect visible=false text="Select " runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6"><strong> WORK PERFORMANCE CONTRACTOR ENTRY DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=6>
                         <hr style="width :100%" />
                    </td>
				</tr>
					<td width=20% height=25>WP Contractor ID : </td>
					<td width=30%><asp:Label id=lblWPTrxID runat=server/></td>
					<td>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25>Date :*</td>
					<td width=30%><asp:Textbox id=txtWPDate width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtWPDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:Label id=lblErrWPDate text="Please enter Date of Work Performance." visible=false forecolor=red runat=server />
						<asp:Label id=lblErrWPDateFmt visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrWPDateFmtMsg visible=false text="<br>Date format should be in " runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Supplier/Contractor Code :*</td>
					<td><asp:DropDownList id=ddlContractor width=100% runat=server/> 
						<asp:Label id=lblErrContractor visible=false forecolor=red text="Please select one Contractor." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>

				<tr>
					<td height=25>Function/Chart of Account :*</td>
					<td><asp:Dropdownlist id=ddlAccCode width=100% AutoPostBack=true runat=server/>
						<asp:Label id=lblErrAccCode visible=false forecolor=red text="Please select one Function/Account Code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
							
				<tr class=mb-t>
					<td colspan=6>
						<table id="tblSelection" width="100%" cellspacing="0" cellpadding="4" border="0" align="center" class="sub-Add" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% class="font9Tahoma">
										<tr>
											<td valign=top height=25 width=20%><asp:label id="lblBlock" runat="server" /> :</td>
											<td valign=top width=80% colspan=5>
													<asp:Dropdownlist id=ddlBlock OnSelectedIndexChanged=onSelect_Block width=50% AutoPostBack=true runat=server/>
													<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
											</td>
										</tr>
										<tr>
											<td height=25>To Date Work Productivity :</td>
											<td><asp:Label id=lblToDateWP runat=server/></td>
										</tr>
										<tr>
											<td height=25>UOM :</td>
											<td><asp:Label id=lblUOM runat=server/></td>
										</tr>
										
										<tr valign=top>
											<td width=20%>Work Productivity :*</td>
											<td width=25%>
												<asp:Textbox id=txtWorkProductivity width=50% maxlength=3 runat=server/>
												<asp:Label id=lblErrWorkProductivity visible=false forecolor=red text="Please enter Work Productivity." runat=server/>
												<asp:RangeValidator id="rvWorkProductivity"
													ControlToValidate="txtWorkProductivity"
													MinimumValue="0"
													MaximumValue="999"
													Type="Double"
													EnableClientScript="True"
													Text="Maximum length 9 digits."
													runat="server"/>
											</td>
										</tr>
										<tr valign=top>
											<td width=20%>HK :</td>
											<td width=25%>
												<asp:Textbox id=txtHariKerja width=50% maxlength=3 runat=server/>
												<asp:Label id=lblErrHariKerja visible=false forecolor=red text="Please enter Hari Kerja." runat=server/>
												<asp:RangeValidator id="rvHariKerja"
													ControlToValidate="txtHariKerja"
													MinimumValue="0"
													MaximumValue="999"
													Type="Double"
													EnableClientScript="True"
													Text="Maximum length 9 digits."
													runat="server"/>
											</td>
										</tr>
										<tr class="mb-c">											
											<td colspan=6 height=25>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
												&nbsp;<asp:Label id=lblErrDupl visible=false forecolor=red text="Function & Block already exists." runat=server/>
												<asp:Label id=lblErrExceeding visible=false forecolor=red text="Exceeding Block Total Area." runat=server/>
											</td>
										</tr>									
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
							Pagerstyle-Visible=False
							AllowSorting="True"  CssClass="font9Tahoma">
							
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
								<asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" HeaderText="Function" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCodeDesc") %>  id=AccCodeDesc runat="server" />
										<asp:Label Text=<%# Container.DataItem("AccCode") %>  Visible=False id=AccCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="30%" HeaderText="Block Code" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCodeDesc") %>  id="BlkCodeDesc" runat="server" />
										<asp:Label Text=<%# Container.DataItem("BlkCode") %>  Visible=False id=BlkCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="UOM" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("UOMCodeDesc") %>  id="UOMCodeDesc" runat="server" />
										<asp:Label Text=<%# Container.DataItem("UOMCode") %>  Visible=False id=UOMCode runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Work Productivity" ItemStyle-HorizontalAlign=Center HeaderStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("WorkProductivity")) %> id="lblWorkProductivity" runat="server" />  <!-- Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="HK" ItemStyle-HorizontalAlign=Center HeaderStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("HariKerja")) %> id="lblHariKerja" runat="server" />  <!-- Modified BY ALIM -->
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server CausesValidation=False />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				
				<tr>
					
					<td colspan=6 height=25><hr size="1" noshade></td>
									
				</tr>	
				<tr>
					<td Width="20%"></td>
					<td Width="30%"></td>
					<td Width="15%">Total :</td>
					<td Width="15%" align="center"><asp:label id="lblTotalWP" runat="server" />
					</td>
					<td Width="15%" align="center"><asp:label id="lblTotalHK" runat="server" /></td>
					<td Width="5%"></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>									
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					&nbsp;</td>
				</tr>
				<Input Type=Hidden id=WPTrxID runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:label id=lblCloseExist visible=false text="no" runat=server/>
				<asp:label id=lblTotArea visible=false text=0 runat=server/>	
			</table>
          </div>
        </td>
        </tr>
        </table>      
		</form>
	</body>
</html>
