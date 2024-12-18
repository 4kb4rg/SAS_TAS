<%@ Page Language="vb" src="../../../include/GL_Setup_VehicleDet.aspx.vb" Inherits="GL_Setup_VehicleDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Vehicle Details</title>
      <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style3
            {
                font-size: 9pt;
                font-family: Tahoma;
                width: 211px;
            }
        </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">

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
                                <td class="style3">
                                  <strong>  <asp:label id="lblTitle" runat="server" /> DETAILS </strong></td>
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
						<asp:Textbox id=txtVehicleCode width=50% maxlength=8 runat=server CssClass="font9Tahoma" />
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
					<td height=25><asp:label id="lblVehType" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlVehType width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrVehType visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>Model : </td>
					<td><asp:textbox id=txtModel maxlength=32 width=50% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>HP/CC : </td>
					<td><asp:textbox id=txtHPCC maxlength=16 width=50% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>Date of Purchase : </td>
					<td><asp:textbox id=txtPurchaseDate maxlength=15 width=50% runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtPurchaseDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:label id=lblDate Text="<br>Date entered should be in the format " forecolor=red Visible=false displaye=dynamic Runat="server"/> 
						<asp:label id=lblFmt forecolor=red Visible=false display=dynamic Runat="server"/>
					<td>&nbsp;</td>						
					</td>
				</tr>
				
				<tr>
					<td height=25>Running in UOM :*</td>
					<td><asp:DropDownList id=ddlUOM width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrUOM visible=false forecolor=red text="Please select Unit of Measurement." display=dynamic runat=server/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id="lblVRA" runat="server" /> :*</td>
					<td colspan=4>
                        <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" MaxLength="15" Width="20%" CssClass="font9Tahoma"></asp:TextBox><input
                            id="FindAcc" runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                            type="button" value=" ... " />
                        <asp:TextBox ID="txtAccName" runat="server" BackColor="Transparent"
                                BorderStyle="None" Font-Bold="True"   MaxLength="10" Width="60%" CssClass="font9Tahoma"></asp:TextBox><br />
						<asp:Label id=lblErrAccCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD height=25 style="width: 19%" valign="top">
                                                &nbsp;Akun:</TD>
											<TD width=80% valign="top">
                                                &nbsp;
                                                <asp:TextBox ID="txtaccDet" runat="server" AutoPostBack="True" MaxLength="15" Width="18.5%" CssClass="font9Tahoma"></asp:TextBox><input
                                                    id="FindAcc_Det" runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtaccDet', 'txtaccNameDet', 'False');"
                                                    type="button" value=" ... " /><asp:TextBox ID="txtaccNameDet" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Bold="True" ForeColor="White" MaxLength="10" Width="60%"></asp:TextBox><br />
                                                &nbsp;
												<asp:Label id=lblErrAccCode2 visible=false forecolor=red runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />&nbsp;</TD>
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
