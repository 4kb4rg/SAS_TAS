<%@ Page Language="vb" src="../../../include/GL_Setup_TemplAccount.aspx.vb" Inherits="GL_Setup_TemplAccount" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Template Account Details</title>
               <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=pop_Account class="main-modul-bg-app-list-pu" runat="server">

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
			<asp:label id=lblRowId visible=false runat=server />
			<asp:label id=lblStmtType visible=false runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td  colspan="5"><asp:label id=lblStmtName runat=server /> : Add/Delete <asp:label id=lblTitle runat=server/> for <asp:label id=lblRowDesc runat=server />				
				</tr>
				<tr>
					<td colspan=5>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan="5">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>

							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD valign="top" width=20%><asp:label id="lblAccGrp" runat="server" /> :*</TD>
											<TD valign="top" width=30% align=left>
												<asp:DropDownList id=ddlAccGrp width=100% runat=server />
											</TD>
											<td align=center valign=top width=5%> OR </td>
											<td valign=top align=left width=15%><asp:label id="lblAccount" runat="server" /> :*</td>
											<TD vAlign="top" width=30% align=left>
												<asp:DropDownList id=ddlAccount width=100% runat=server />
											</TD>
										</TR>
										<TR class="mb-c">
											<TD valign=top align=left colspan=5>
												<asp:Label id=lblErrSelectBoth visible=false forecolor=red display=dynamic runat=server/>
												<asp:Label id=lblErrNotSelect visible=false forecolor=red display=dynamic runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD valign="top" colspan=2 height=25>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
											&nbsp;</TD>
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
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma">
							
						<HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<Columns>						
								<asp:TemplateColumn ItemStyle-Width="20%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
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

			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
