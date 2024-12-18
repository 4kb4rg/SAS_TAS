<%@ Page Language="vb" src="../../../include/HR_trx_EmployeePayDet_Estate.aspx.vb" Inherits="HR_trx_EmployeePayDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRTrx" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Payrol Details</title>
                 <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
 	    <style type="text/css">
            .style1
            {
                height: 9px;
            }
        </style>
 	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 470px" valign="top" class="font9Tahoma">
			    <div class="kontenlist"> 

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component."  ForeColor=red runat=server />
			<asp:Label id=lblRedirect visible=false runat=server />
			
			<table border=0 cellspacing=0 cellpadding=2 width=99% id="TABLE1" class="font9Tahoma">
				<tr>
					<td colspan="5"><UserControl:MenuHRTrx id=MenuHRTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5"><strong>PAYROLL HISTORY DETAIL</strong> </td>
				</tr>
				<tr>
					<td colspan=5 class="style1">  <hr style="width :100%" /></td>
				</tr>
				<tr>
					<td width=15% height=25>
                        ID :
                        </td>
					<td style="width: 425px"><asp:Label id=Lblpayhistid runat=server />
                        </td>
					<td>&nbsp;</td>
					<td width=15%></td>
					<td style="width: 267px"></td>
					
				</tr>
				<tr>
					<td height=25>
                        NIK :
                    </td>
					<td style="width: 425px; height: 27px"><asp:Label id=Lblcodeemp runat=server />-<asp:Label id=Lblemptype runat=server /></td>
					<td >&nbsp;</td>
					<td >Status : </td>
					<td style="width: 267px;"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td style="height: 25px">
                        Periode :</td>
					<td style="width: 425px; height: 25px;"><asp:TextBox ID="txtperiodeAwl" MaxLength=6 Width="30%" runat="server"/> s/d <asp:TextBox ID="txtperiodeaAhr" MaxLength=6 Width="30%" runat="server"/></td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">Tgl Buat : </td>
					<td style="width: 267px"><asp:Label id=lblCreateDate runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Code Salary :</td>
					<td style="width: 425px; height: 27px">
					    <asp:DropDownList ID="ddlcodesalary" OnSelectedIndexChanged="ddlcodesalaryChanged" AutoPostBack="True" runat="server" Width="100%" />
					<td >&nbsp;</td>
					<td >Tgl update : </td>
					<td style="width: 267px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Golongan</td>
					<td style="width: 425px; height: 27px;">
                        <asp:DropDownList ID="ddlcodegol" runat="server" Width="100%" OnSelectedIndexChanged="ddlcodegolChanged" AutoPostBack="True"  />
                    </td>
					<td >&nbsp;</td>
					<td >Diupdate : </td>
					<td style="width: 267px"><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Gaji Pokok :</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtbasicsalary" Width="40%" runat="server"></asp:TextBox>&nbsp;
						<asp:Button id=SetBtn OnClick=SetBtn_Click  text=" set " runat=server  />
                    
					<td >&nbsp;</td>
					<td colspan="2" rowspan="6" ></td>
				</tr>
				<tr>
					<td height=25>
                        Premi Tetap :</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtpremisalary" Width="40%" runat="server"></asp:TextBox>
					<td >&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        Tunjangan :</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txttunjsalary" Width="40%" runat="server"></asp:TextBox>
					<td >&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        Upah Harian :</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtupah" Width="40%" runat="server"></asp:TextBox>
					<td >&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        Min HK :</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtminhk" Width="40%" ReadOnly="True" CssClass="mr-h" runat="server"></asp:TextBox>
					<td >&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        Pinjaman :</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtsmallsalary" Width="40%" runat="server"></asp:TextBox>
					<td >&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 81px"></td>
					<td  colspan=5 style="height: 81px">
                        <table style="width: 100%" class="font9Tahoma">
                            <tr>
                                <td style="width: 100px; height: 26px">
						Beras</td>
                                <td style="width: 100px; height: 26px">
                                    <asp:TextBox ID="txtBeras"  runat="server"></asp:TextBox></td>
                                <td style="width: 100px; height: 26px">
						Lembur</td>
                                <td style="width: 100px; height: 26px">
                                    <asp:TextBox ID="txtOvrTmRate" ReadOnly="True" runat="server"></asp:TextBox></td>
                                <td style="width: 100px; height: 26px">
						Makan</td>
                                <td style="width: 100px; height: 26px">
                                    <asp:TextBox ID="txtmakan"  runat="server"></asp:TextBox></td>
                                <td style="width: 100px; height: 26px">
						Transport   
                                </td>
                                <td style="width: 100px; height: 26px">
                                    <asp:TextBox ID="txttrans"  runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 25px">

						Pot.Mangkir</td>
                                <td style="width: 100px; height: 25px">
                                    <asp:TextBox ID="txtpotmangkir"  runat="server"></asp:TextBox></td>
                                <td style="width: 100px; height: 25px">
						Pot.SPSI</td>
                                <td style="width: 100px; height: 25px">
                                    <asp:TextBox ID="txtSPSIRate" runat="server"></asp:TextBox></td>
                                <td style="width: 100px; height: 25px">
						Pot.Lain</td>
                                <td style="width: 100px; height: 25px">
                                    <asp:TextBox ID="txtpotlainRate" ReadOnly="True" runat="server"></asp:TextBox></td>
                                <td style="width: 100px; height: 25px">
                                </td>
                                <td style="width: 100px; height: 25px">
                                </td>
                            </tr>
                        </table>
                        
					</td>
				</tr>
				<tr>
					<td height=25></td>
					<td colspan=5>
						     <asp:CheckBox ID="chkisgol" runat="server" Text=" Golongan " />&nbsp;
							 <asp:CheckBox ID="chkisberas" runat="server" Text=" Catu Beras " />&nbsp;
                             <asp:CheckBox ID="chkisastek" runat="server" Text=" BPJS-JKK" />&nbsp;
							 <asp:CheckBox ID="chkisastekJKM" runat="server" Text=" BPJS-JKM" />&nbsp;
							 <asp:CheckBox ID="chkisastekJHT" runat="server" Text=" BPJS-JHT" />&nbsp;
							 <asp:CheckBox ID="chkisbpjs" runat="server" Text=" BPJS-JPK" />&nbsp;
							 <asp:CheckBox ID="chkisjp" runat="server" Text=" BPJS-JP" />&nbsp;
                             <asp:CheckBox ID="chkisasteknberas" runat="server" Text=" AstekNBeras " visible=false/>&nbsp;
							 <asp:CheckBox ID="chkismakan" runat="server" Text=" Makan " />&nbsp;
							 <asp:CheckBox ID="chkistrans" runat="server" Text=" Transport " />&nbsp;
							 <asp:CheckBox ID="chkisbonus" runat="server" Text=" Bonus " /> &nbsp;
							 <asp:CheckBox ID="chkisspsi"  runat="server" Text=" SPSI" />&nbsp;
							 
				    </td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade>
                        &nbsp;</td>
				</tr>
				<tr >
					<td colspan=5 style="height: 28px">
					Tunj Detail:&nbsp;<asp:DropDownList ID="ddlcodetnj" runat="server" Width="20%" />&nbsp;
                    Nominal : &nbsp;<asp:TextBox ID="txtnominal"  onkeypress="javascript:return isNumberKey(event)" runat="server"/>&nbsp;
                    <asp:ImageButton id=AddBtn  OnClick="BtnAddTnj_OnClick" AlternateText="  Add Tunj " CausesValidation=False imageurl="../../images/butt_add.gif"  runat=server />
					</td>
				</tr>

				<tr >
					<td colspan=7 >
					
					 <asp:DataGrid ID="dgTnj" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" 
                                                    OnItemDataBound="dgTnj_BindGrid" 
													OnEditCommand="DEDR_Edit"
													OnUpdateCommand="DEDR_Update"
													OnCancelCommand="DEDR_Cancel"
													OnDeleteCommand="DEDR_Delete"
                                                    Width="80%"  CssClass="font9Tahoma"
                                                    ShowFooter=True>
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />

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
                                                        <asp:TemplateColumn HeaderText="Tunjangan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgTnj_lbl_ed" Width="120px" Text='<%# Container.DataItem("TunjDesc") %>' runat="server" />
																<asp:HiddenField ID="dgTnj_hid_payid" Value='<%# Container.DataItem("PayHistID") %>' runat="Server" />
																<asp:HiddenField ID="dgTnj_hid_idtnj" Value='<%# Container.DataItem("idTnj") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        
                                                                                                               
                                                        <asp:TemplateColumn HeaderText="Nominal (Rp)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgTnj_txt_rp" Text='<%# Container.DataItem("Nominal") %>' Width="100%" style="text-align:right;" 
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
															<EditItemTemplate>
																<asp:TextBox id="NominalRP" width=100% 
																Text='<%# trim(Container.DataItem("Nominal")) %>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
															</EditItemTemplate>
                                                            <FooterTemplate >
									                                <asp:Label ID=dgTnj_lbl_ft_totrp runat=server />
									                        </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
														
														<asp:TemplateColumn HeaderText="Tgl update" SortExpression="A.UpdateDate">
														<ItemTemplate>
															<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
														</ItemTemplate>
														<EditItemTemplate >
															<asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
																Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>'
																runat="server"/>
															<asp:TextBox id="CreateDate" Visible=False
																Text='<%# Container.DataItem("CreateDate") %>'
																runat="server"/>
														</EditItemTemplate>
													</asp:TemplateColumn>
													
					
														<asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
															<ItemTemplate>
																<%# Container.DataItem("UserName") %>
															</ItemTemplate>
															<EditItemTemplate >
																<asp:TextBox id="UserName" Readonly=TRUE size=8 
																	Text='<%# Session("SS_USERID") %>' Visible=False runat="server"/>
															</EditItemTemplate>
														</asp:TemplateColumn>	
														
													<asp:TemplateColumn>					
														<ItemTemplate>
															<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
															<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
															<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
														</EditItemTemplate>
													</asp:TemplateColumn>

                                                    </Columns>
                                                </asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade>
                        &nbsp;</td>
				</tr>

				<tr >
					<td colspan=5 style="height: 28px">
					<asp:ImageButton id=SaveBtn OnClick=SaveBtn_Click AlternateText="  Save  " CausesValidation=False imageurl="../../images/butt_save.gif" runat=server  />
                    <asp:ImageButton id=DelBtn OnClick=DelBtn_Click AlternateText="  Delete  " CausesValidation=False imageurl="../../images/butt_delete.gif"  runat=server  />
                    <asp:ImageButton id=BackBtn OnClick=BackBtn_Click AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_close.gif"  runat=server />
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
