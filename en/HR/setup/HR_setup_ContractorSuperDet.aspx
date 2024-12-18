<%@ Page Language="vb" trace="false" src="../../../include/HR_setup_ContractorSuperDet.aspx.vb" Inherits="HR_setup_ContSuperDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contractor Supervision Details</title>
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<Input Type=Hidden id=hidSuppCode runat=server />
			<Input Type=Hidden id=hidEmpCode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>		
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuHRSetup id=MenuHRSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6"><strong>CONTRACTOR SUPERVISION DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25>Supplier Code :* </td>
					<td width=30% height=25>
						<asp:DropDownList id=ddlSuppCode width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please select Supplier Code"
							ControlToValidate=ddlSuppCode />
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try again." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15% valign=top>Status : </td>
					<td width=25% valign=top><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Allowance :*</td>
						<!-- Modified BY ALIM MaxLength = 22 -->
					<td><asp:textbox id=txtAllowance width=100% maxlength=22 runat=server />
						<asp:RequiredFieldValidator id=validateAllow display=Dynamic runat=server 
							ErrorMessage="Please enter Allowance"
							ControlToValidate=txtAllowance />
						<asp:RegularExpressionValidator 
								id=revAllow
								ControlToValidate="txtAllowance"
								ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 2 decimal points. "
								runat="server"/>
						<!-- End of Modified BY ALIM -->		
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>(to be given per employee manday)</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD width=20% height=25>Employee Code :*</TD>
											<TD width=80%><asp:DropDownList id=ddlEmpCode width=100% runat=server />
														  <asp:Label id=lblErrEmpCode visible=false forecolor=red text="Please select one Employee" runat=server/>											
											</td>
										</TR>
										<TR class="mb-c">
											<TD colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />&nbsp;</TD>
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
							AllowSorting="True" CssClass="font9Tahoma">
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
								<asp:TemplateColumn ItemStyle-Width="30%" HeaderText="Employee Code">
									<ItemTemplate>
										<asp:Label id="lblEmpCode" Text='<%# Container.DataItem("EmpCode") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>							
								<asp:TemplateColumn ItemStyle-Width="50%" HeaderText="Employee Name">
									<ItemTemplate>
										<asp:Label id="lblEmpName" Text='<%# Container.DataItem("EmpName") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>							
								<asp:TemplateColumn ItemStyle-Width="20%" ItemStyle-HorizontalAlign=Right>
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
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
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
