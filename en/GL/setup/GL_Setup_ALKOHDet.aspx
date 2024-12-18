<%@ Page Language="vb" src="../../../include/GL_Setup_ALKOHDet.aspx.vb" Inherits="GL_Setup_ALKOHDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Alokasi Overhead Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"    runat="server">
                  <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=tbcode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> <asp:label id="lblTitle" runat="server" /> DETAILS </strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                          <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblVehCode" runat="server" /> :* </td>
					<td width=30%>
						<asp:Textbox id=txtVehicleCode width=50% maxlength=8 runat=server CssClass="font9Tahoma"/>
						<input type="hidden" runat="server" id="hidRecStatus">
						<input type="hidden" runat="server" id="hidOriVehCode">
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ControlToValidate=txtVehicleCode />
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehDesc" runat="server" /> :*</td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id="lblVRA" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlAccCode width=90% runat=server  CssClass="font9Tahoma" /> 
						<input type=Button id=btnFind value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode', 'False');" runat=server/>
						<asp:Label id=lblErrAccCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>Alokasi ke Unit Lain ? :</td>
					<td><asp:checkbox id=chkunit OnCheckedChanged="Check_Clicked" AutoPostBack="True" runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td height=25>Alokasi Plasma ? :</td>
					<td><asp:checkbox id=chkplasma runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td class="mt-h">COA Alokasi (DR):*</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<TABLE id="TABLE1" width="100%" class="font9Tahoma" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0"  class="font9Tahoma">
										<TR class="mb-c">
											<TD width=20% height=25>
                                                &nbsp;Akun:</TD>
											<TD width=80%>
												<asp:DropDownList id=ddlAccCode3 width=85% runat=server CssClass="font9Tahoma" />
												<input type=Button id=btnFind3 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode3', 'False');" runat=server/>
												<asp:Label id=lblErrAccCode3 visible=false forecolor=red runat=server/>
											</TD>
											
										</TR>
										<tr>
										    <TD width=20% height=25>
                                                &nbsp;Tipe Blok:</TD>
											<TD width=80%>
											    <asp:DropDownList id="ddlBlkType" width=85% runat=server  CssClass="font9Tahoma">
										            <asp:ListItem value="1" selected>Mature</asp:ListItem>
										            <asp:ListItem value="2">Immature</asp:ListItem>
													<asp:ListItem value="3">None</asp:ListItem>
									            </asp:DropDownList>
												<asp:Label id=lblErrBlkType visible=false forecolor=red runat=server/>
											</TD>
										</tr>
										<tr>
										    <TD width=20% height=25>
                                                &nbsp;Inti/Plasma:</TD>
											<TD width=80%>
											    <asp:DropDownList id="ddlIP" width=85% runat=server  CssClass="font9Tahoma">
										            <asp:ListItem value="I" selected>Inti</asp:ListItem>
										            <asp:ListItem value="P">Plasma</asp:ListItem>
													<asp:ListItem value="">None</asp:ListItem>
									            </asp:DropDownList>
												<asp:Label id=lblErrIP visible=false forecolor=red runat=server/>
											</TD>
										</tr>
										<TR class="mb-c">
											<TD vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAddDR imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAddDR_Click runat=server />&nbsp;</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>		
				
				<tr>
					<td colspan="6">
						<asp:DataGrid id=dgLineDRDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_DeleteDR 
							Pagerstyle-Visible=False
							AllowSorting="True" class="font9Tahoma">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
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
								<asp:TemplateColumn ItemStyle-Width="35%" HeaderText="Akun" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="55%" HeaderText="Deskripsi">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-Width="55%" HeaderText="Tipe Block">
									<ItemTemplate>
										<asp:Label Text=<%# objGLSetup.mtdGetPlantedAreaBlkType(Container.DataItem("BlkType")) %> id="lblBlkDesc" runat="server" />
									    <asp:Label ID="lblBlkType" Visible=false Text='<%# Container.DataItem("BlkType") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="55%" HeaderText="Inti/Plasma">
									<ItemTemplate>
										<asp:Label ID="lblIPType" Text=<%# Container.DataItem("IPType") %> runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr id="TRUnit" runat=server>
				    <td colspan="6">
						<TABLE id="TABLE9" width="100%" class="font9Tahoma" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
						<tr>
						<td class="mt-h">COA Alokasi UNIT (DR):*</td>
						</tr>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma" >
										<TR class="mb-c">
											<TD width=20% height=25>
                                                &nbsp;Unit:</TD>
											<TD width=80%>
												<asp:DropDownList id=ddlunit width=85% OnSelectedIndexChanged="DDListUnit" AutoPostBack="True" runat=server  CssClass="font9Tahoma"/>
											</TD>
											
										</TR>
										<TR class="mb-c">
											<TD width=20% height=25>
                                                &nbsp;Akun:</TD>
											<TD width=80%>
												<asp:TextBox id=txtAccCode3 width=85% runat=server CssClass="font9Tahoma"/>
											</TD>
											
										</TR>
										<tr>
										    <TD width=20% height=25>
                                                &nbsp;Periode:</TD>
											<TD width=80%>
												<asp:TextBox id=txtyr maxlength=4  onkeypress="javascript:return isNumberKey(event)" runat=server CssClass="font9Tahoma"/>
											</TD>
										</tr>
										<tr>
										    <TD width=20% height=25>
                                                &nbsp;Total Area:</TD>
											<TD width=80%>
												<asp:TextBox id=txtarea  onkeypress="javascript:return isNumberKey(event)" runat=server CssClass="font9Tahoma"/>
											</TD>
										</tr>
										<TR class="mb-c">
											<TD vAlign="top" colspan=2 height=25>
											<asp:Button text="Copy dari Periode :" id=CopyBtn onclick=CopyBtnKlik runat=server />&nbsp;
											<asp:textbox id=txtyr2 maxlength=4 onkeypress="javascript:return isNumberKey(event)" runat=server CssClass="font9Tahoma" />
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<tr>
							<td colspan="6">
							<asp:DataGrid id=dgLineUnit
								AutoGenerateColumns=false width="100%" runat=server
								GridLines=none
								Cellpadding=2
								Pagerstyle-Visible=False
								AllowSorting="True" class="font9Tahoma"
								OnEditCommand="dgLineUnit_Edit"
								OnUpdateCommand="dgLineUnit_Update"
								OnCancelCommand="dgLineUnit_Cancel" >
								<HeaderStyle CssClass="mr-h"/>
								<ItemStyle CssClass="mr-l"/>
								<AlternatingItemStyle CssClass="mr-r"/>
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
									<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Unit" >
										<ItemTemplate>
											<%# Container.DataItem("Unit") %> 
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="id1" runat="server" Enabled=false Text='<%# trim(Container.DataItem("Unit")) %>'	Width="100%" CssClass="font9Tahoma"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
								
									<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="AccCode">
										<ItemTemplate>
											<%# Container.DataItem("AccCode") %> 
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="id2" runat="server" Enabled=false Text='<%# trim(Container.DataItem("AccCode")) %>'	Width="100%" CssClass="font9Tahoma"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Tot.Area" >
										<ItemTemplate>
											<%# Container.DataItem("TotalArea") %> 
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="id3" runat="server"  Text='<%# trim(Container.DataItem("TotalArea")) %>'	Width="100%" CssClass="font9Tahoma"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn HeaderText="Periode" >
										<ItemTemplate>
											<%# Container.DataItem("AccMonth") %>-<%# Container.DataItem("AccYear") %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="id4" CssClass="font9Tahoma" runat="server" MaxLength="2" Enabled=false Text='<%# trim(Container.DataItem("AccMonth")) %>'></asp:TextBox>
											<asp:TextBox ID="id5" CssClass="font9Tahoma" runat="server" MaxLength="2" Enabled=false Text='<%# trim(Container.DataItem("AccYear")) %>'></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn ItemStyle-Width=12%>
											<ItemTemplate >
											<asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
											<asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
											</EditItemTemplate>
									</asp:TemplateColumn>	
								</Columns>
							</asp:DataGrid>
							</td>
							</tr>
						</TABLE>
					</td>
                </tr>				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td class="mt-h">COA Biaya:*</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD width=20% height=25>
                                                &nbsp;Akun:</TD>
											<TD width=80%>
												<asp:DropDownList id=ddlAccCode2 width=85% runat=server  CssClass="font9Tahoma"/>
												<input type=Button id=btnFind2 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode2', 'False');" runat=server/>
												<asp:Label id=lblErrAccCode2 visible=false forecolor=red runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />&nbsp;<asp:Button 
                                                    ID="Issue11" class="button-small" 
                        runat="server" Text="Add"  />
				                            </TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							
						</TABLE>
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
							AllowSorting="True" class="font9Tahoma">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>

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
								<asp:TemplateColumn ItemStyle-Width="35%" HeaderText="Akun" >
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="55%" HeaderText="Deskripsi">
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
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
                        <br />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
