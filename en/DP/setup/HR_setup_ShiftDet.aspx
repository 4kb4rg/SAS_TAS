<%@ Page Language="vb" src="../../../include/HR_setup_ShiftDet.aspx.vb" Inherits="HR_setup_ShiftDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%><html>
	<head>
		<title>Shift Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">


        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">SHIFT DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% valign=top>Shift Code :* </td>
					<td width=30% valign=top>
						<asp:Textbox id=txtShiftCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Shift Code"
								ControlToValidate=txtShiftCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtShiftCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another holiday schedule code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20% valign=top>Status : </td>
					<td width=25% valign=top><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td>Description :*</td>
					<td>
						<asp:Textbox id=txtDescription width=100% maxlength=128 runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td>Allowance & Deduction Code :*</td>
					<td><asp:DropDownList id=ddlAD width=80% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlAD','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrAD visible=false forecolor=red text="<br>Please select Allowance & Deduction code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top></td>
					<td align="left" valign=top></td>
					<td>&nbsp;</td>
					<td valign=top height=25>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
					<td colspan="5">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
										<TR class="mb-c">
											<TD vAlign="top" width=20% height=25>Shift :</TD>
											<TD vAlign="top" width=30% align=left>
												<asp:DropDownList id=ddlShift width=100% runat=server />
												<asp:Label id=lblErrShift forecolor=red visible=false text="<br>Please enter shift." runat=server/>
											</TD>
											<TD width=5%>&nbsp;</TD>
											<TD vAlign="top" width=20%>Allowance *:</TD>
											<TD vAlign="top" width=25% align=left>
												<asp:Textbox id=txtAllowance width=100% maxlength=22 runat=server/>
												<!-- Added BY ALIM -->
												<asp:CompareValidator id="ChckAllowance" runat="server" 
												display=dynamic ControlToValidate="txtAllowance" Text="The value must be numeric." 
												Type="double" Operator="DataTypeCheck"/> 
												<asp:RangeValidator id="rvAllowance"
												ControlToValidate="txtAllowance"
												MinimumValue="0"
												MaximumValue="9999999999999999999.99"
												Type="Double"
												EnableClientScript="True"
												Text="Exceeded Value"
												runat="server"/>
												<!-- End of Added BY ALIM -->
												
												<asp:Label id=lblErrAllowance forecolor=red visible=false text="<br>Please enter allowance." runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD vAlign="top" colspan=5 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
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
								<asp:TemplateColumn ItemStyle-Width="60%" HeaderText="Shift">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ShiftLnId") %> visible=false id="lblCode" runat="server" />
										<asp:Label Text=<%# objHRSetup.mtdGetShiftName(Container.DataItem("Shift")) %> id="lblShift" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Allowance">
									<ItemTemplate>
										<!-- Remarked BY ALIM 
										<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAllowance" runat="server" />
										-->
										<!-- Modified BY ALIM -->
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAllowance_New" runat="server" />
										<!-- End of Modified BY ALIM -->
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
					<td>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=shiftcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblActiveShift visible=false text="" runat=server/>
				<tr>
					<td>
                                            &nbsp;</td>
				</tr>
				</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
