<%@ Page Language="vb" src="../../../include/HR_setup_GangDet.aspx.vb" Inherits="HR_setup_GangDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Gang Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">

			<asp:Label id=lblPlsSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100%  class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">GANG DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% valign=top>Gang Code :* </td>
					<td width=30% valign=top>
						<asp:Textbox id=txtGangCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Gang Code"
							ControlToValidate=txtGangCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtGangCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try again." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20% valign=top>Status : </td>
					<td width=25% valign=top><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td valign=top>Description :*</td>
					<td valign=top>
						<asp:textbox id=txtDescription width=100% maxlength=128 runat=server />
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="Please enter Description"
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td valign=top height=25>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td valign=top>Gang Level :*</td>
					<td>
						<asp:DropDownList id=ddlGangLevel width=100% OnSelectedIndexChanged=GangLevel_Changed AutoPostBack=True runat=server/>
						<asp:Label id=lblErrGangLevel visible=false forecolor=red text="Please select Gang Level." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top height=25>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td valign=top><asp:label id="lblBlkGrp" runat="server" /> :*</td>
					<td>
						<asp:DropDownList id=ddlDivision width=100% runat=server/>
						<asp:Label id=lblErrDivision visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top height=25>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td>Gang Leader :* </td>
					<td>
						<asp:DropDownList id=ddlGangLeader width=100% OnSelectedIndexChanged=GangLeader_Changed AutoPostBack=True runat=server/>
						<asp:Label id=lblErrGangLeader visible=false forecolor=red text="Please select Gang Leader." runat=server/>
						<asp:Label id=lblErrCheckGangLeader visible=false forecolor=red text="This Gang Leader already used. Please select another Gang Leader." runat=server/>
					</td>
					<td colspan= 3>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>	
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >			
				<tr>
					<td colspan="5">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD vAlign="top" width=20% height=25>Gang Member :</TD>
											<TD vAlign="top" width=80% align=left>
												<asp:DropDownList id=ddlGangMember width=100% runat=server />
												<asp:Label id=lblErrGangMember visible=false forecolor=red text="Gang Leader Cannot Be Gang Member." runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
                                            </TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
				<tr>
					<td>
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
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
								<asp:TemplateColumn ItemStyle-Width="80%" HeaderText="Gang Member">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("GangMember") %> visible=false id="lblCode" runat="server" />
										<asp:Label Text=<%# Container.DataItem("GangMemberName") %> id="lblGangMember" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" CausesValidation=False imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=gangcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblFlagBindGang visible=false text=1 runat=server/>
				<tr>
					<td>
                                            &nbsp;</td>
				</tr>
				</table>


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
