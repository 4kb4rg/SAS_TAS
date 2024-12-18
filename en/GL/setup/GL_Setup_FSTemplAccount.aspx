<%@ Page Language="vb" src="../../../include/gl_setup_FSTemplAccount.aspx.vb" Inherits="gl_setup_FSTemplAccount" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Financial Statement Template Account Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 20%;
                height: 26px;
            }
            .style2
            {
                height: 26px;
            }
        </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=pop_Account class="main-modul-bg-app-list-pu"  runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblPlsSelectEither visible=false text="Please select either " runat=server />
			<asp:Label id=lblOnly visible=false text=" only." runat=server />
			<asp:Label id=lblOr visible=false text=" OR " runat=server />
			<asp:Label id=lblPlsSelect visible=false text="Please select " runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblReportCode visible=false runat=server />
			<asp:label id=lblRowId visible=false runat=server />
			<asp:label id=lblStmtType visible=false runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5"><asp:label id=lblStmtName runat=server /> : Add/Delete <asp:label id=lblTitle runat=server/> for <asp:label id=lblRowDesc runat=server />				
				</tr>
				<tr>
					<td colspan=5>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr class="font9Tahoma">
					<td colspan="5" >
						<TABLE id="tblSelection" width="100%" class="sub-add" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
										<tr>
										    <TD valign="top" class="style1">
                                                General COA From :*</TD>
											<TD valign="top" width=40% align=left class="style2">
												<asp:DropDownList id=ddlAccount width=100% runat=server CssClass="font9Tahoma" />
											</TD>
											<td align=center valign=top width=6% class="style2"> To </td>
										    <TD vAlign="top" width=40% align=left class="style2">
												<asp:DropDownList id=ddlAccount1 width=100% runat=server CssClass="font9Tahoma" />
											</TD>
										</tr>
										<TR class="mb-c">
											<TD valign=top align=left colspan=5>
                                                &nbsp;<asp:Label id=lblErrNotSelect visible=false forecolor=red display=dynamic runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD valign="top"  height=25>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
											</TD>
											<td></td>
											<td></td>
											<TD valign="top"  height=25 align="right">
												<asp:ImageButton id=btnDellAll imageurl="../../images/butt_rollback.gif" alternatetext="Delete All" onclick=btnDeleteAll_Click runat=server /></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				
				<tr>
					<td colspan="5">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
						    OnEditCommand=DEDR_EDIT
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma">
							
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
							    <asp:TemplateColumn HeaderText="" ItemStyle-Width="20%">
			                        <ItemTemplate>
			                            <asp:LinkButton id="lbAccCode" CommandName=Edit runat="server"/>
			                            <asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" Visible=false runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
		                        
								<asp:TemplateColumn ItemStyle-Width="50%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblAccDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
                                
								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
                
                <tr>
					<td colspan="5" >
						<TABLE id="tblDetail" visible=false class="mb-c" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server>
						    <tr>
					            <td class="mt-h" colspan="5"> : Add/Delete COA Detail for <asp:label id=lblCOAGeneral runat=server /> (<asp:label id=lblCOAGeneralDescr runat=server />)</td>				
				            </tr>
				            <tr>
					            <td colspan=5><hr size="1" noshade></td>
				            </tr>
						    
						    <tr>
						        <TD>Option Type :*</td>
						        <td><asp:DropDownList id=rblDetail width=50% AutoPostBack=true OnSelectedIndexChanged=rblDetail_Change runat=server CssClass="font9Tahoma" /></TD>
						        <td>&nbsp;</td>
						        <td>&nbsp;</td>
						        <td>&nbsp;</td>
						    </tr>
							<tr>
							    <TD valign="top" style="width: 20%">
                                    COA Detail :*</TD>
								<TD valign="top" width=40% align=left>
									<asp:DropDownList id=ddlCOAFrom width=100% runat=server CssClass="font9Tahoma" />
								</TD>
								<td align=center valign=top width=6%> <asp:label id=lblOptDetail runat=server /> </td>
							    <TD vAlign="top" width=40% align=left>
									<asp:DropDownList id=ddlCOATo width=100% runat=server CssClass="font9Tahoma" />
								</TD>
							</tr>
							<tr>
							    <TD valign="top" style="width: 20%">
                                    Ref. No :*</TD>
								<TD valign="top" width=40% align=left>
									<asp:textbox id= "txtRefNo" width="50%" maxlength="32" runat="server" CssClass="font9Tahoma" />
								</TD>
								<td align=center valign=top width=6%> <asp:label id=lblPersentase Text="Persentase :" Visible="false" runat=server /> </td>
							    <TD vAlign="top" width=40% align=left>
									<asp:textbox id= "txtPersentase" width="50%" maxlength="32" Text=100 Visible="false" runat="server" CssClass="font9Tahoma" />
								</TD>
							</tr>
							<tr>
							    <TD valign="top" style="width: 20%">
                                    Ref. Type :*</TD>
								<TD valign="top" width=40% align=left>
									<asp:DropDownList id="ddlRefType" AutoPostback="true" Width="50%" runat="server" CssClass="font9Tahoma" >
                                        <asp:ListItem value="A" Selected = "True" >Activa</asp:ListItem>
                                        <asp:ListItem value="P">Pasiva</asp:ListItem>
				                    </asp:DropDownList>
								</TD>
							</tr>
							<TR class="mb-c">
								<TD valign=top align=left colspan=5>
                                    &nbsp;<asp:Label id=lblErrNotSelectDetail visible=false forecolor=red display=dynamic runat=server/>
								</TD>
							</TR>
							<TR class="mb-c">
								<TD valign="top"  height=25>
									<asp:ImageButton id=btnAddDetail imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAddDetail_Click runat=server />
								</TD>
								<td></td>
								<td></td>
								<TD valign="top"  height=25 align="right">
									<asp:ImageButton id=btnRollDetail imageurl="../../images/butt_rollback.gif" alternatetext="Delete All" onclick=btnDeleteDetailAll_Click runat=server /></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				
				<tr>
					<td colspan="5">
						<asp:DataGrid id=dgLineDetail
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_DeleteDetail
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma">
							
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
							    <asp:TemplateColumn HeaderText="General COA Code" ItemStyle-Width="10%">
			                        <ItemTemplate>
			                            <asp:Label Text=<%# Container.DataItem("COAGeneral") %> id="lblIDCOAGeneral" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>				
							    <asp:TemplateColumn HeaderText="COA Code" ItemStyle-Width="25%">
			                        <ItemTemplate>
			                            <asp:Label Text=<%# Container.DataItem("Option1") %> id="lblOption1" runat="server" /><br />
			                            <asp:Label Text=<%# Container.DataItem("OptDescr1") %> id="lblCOAFromDesc" runat="server" />
			                        </ItemTemplate>
		                        </asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccOptionDescr") %> id="lblAccOptDesc" runat="server" />
										<asp:Label Text=<%# Container.DataItem("AccOption") %> id="lblAccOption" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COA Code/Location" ItemStyle-Width="25%">
									<ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("Option2") %> id="lblOption2" runat="server" /><br />
										<asp:Label Text=<%# Container.DataItem("OptDescr2") %> id="lblCOAToDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ref. No" HeaderStyle-HorizontalAlign=Center ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("RefNo") %> id="lblRefNo" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ref. Type" HeaderStyle-HorizontalAlign=Center ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("RefTypeDescr") %> id="lblRefTypeDescr" runat="server" />
										<asp:Label Text=<%# Container.DataItem("RefType") %> id="lblRefType" Visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="4%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>

			</table>
		</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
