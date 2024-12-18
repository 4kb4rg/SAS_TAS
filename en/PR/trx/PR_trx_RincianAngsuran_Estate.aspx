<%@ Page Language="vb" src="../../../include/PR_trx_RincianAngsuran_Estate.aspx.vb" Inherits="PR_trx_RincianAngsuran_Estate"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Rincian Angsuran List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmMain.txtEmpName.focus();">
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			

		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>RINCIAN ANGSURAN</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="15%" height="26" valign=bottom>
                                    NIK :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="8" runat="server" /></td>
								<td height="26" valign=bottom style="width: 17%">
								    Nama :<BR><asp:TextBox id=txtEmpName width=100% maxlength="15" runat="server" /></td>
                                <td height="26" width="15%" >
                                    Periode : <br />
                                    <asp:DropDownList ID="ddlEmpMonth" runat="server" Width="73%">
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList id="ddlyear" width="26%" runat=server></asp:DropDownList>
								</td>
								<td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/>
								</td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						             <asp:DataGrid id=dgEmpList
							            AutoGenerateColumns=False width=100% runat=server
							            GridLines=None 
							            Cellpadding=2 
							            AllowPaging=True 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnSortCommand=Sort_Grid  
						   	            AllowSorting=True
                                       			             class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							            <Columns>
								            <asp:BoundColumn Visible=False HeaderText="NIK" DataField="CodeEmp" />								
								            <asp:HyperLinkColumn HeaderText="NIK" 
									            DataNavigateUrlField="CodeEmp" 
									            DataNavigateUrlFormatString="PR_trx_AngsuranDet_Estate.aspx?CodeEmp={0}" 
									            DataTextField="CodeEmp" />	
							
								            <asp:TemplateColumn HeaderText="Nama" >
									            <ItemTemplate>
										            <asp:Label ID =lbename Text='<%#Container.DataItem("EmpName")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>	
								
								            <asp:TemplateColumn HeaderText="Divisi">
									            <ItemTemplate>
										            <asp:Label ID =lbedate Text='<%#Container.DataItem("IDDiv")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
												
								            <asp:TemplateColumn HeaderText="Jabatan" >
									            <ItemTemplate>
										            <asp:Label ID =lbket Text='<%#Container.DataItem("jabatan")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Jns.Pinjaman" >
									            <ItemTemplate>
										            <asp:Label ID =lbket Text='<%#Container.DataItem("Tipe")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Pinjaman" >
								                <HeaderStyle HorizontalAlign="Right" /> 
								                <ItemStyle HorizontalAlign="Right"/> 
									            <ItemTemplate>
										            <asp:Label ID =lbtotagsr Text='<%#Container.DataItem("Totalpinjaman")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Byr.SD.Bln.Lalu" >
								                <HeaderStyle HorizontalAlign="Right" /> 
								                <ItemStyle HorizontalAlign="Right"/> 
									            <ItemTemplate>
										            <asp:Label ID =lbtotagsr Text='<%#Container.DataItem("TotalBayarSDBlnLalu")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Angsuran" >
								                <HeaderStyle HorizontalAlign="Right" /> 
								                <ItemStyle HorizontalAlign="Right"/> 
									            <ItemTemplate>
										            <asp:Label ID =lbtotagsr Text='<%#Container.DataItem("Angsuran")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Tot.Byr.SD.Bln.Ini" >
								                <HeaderStyle HorizontalAlign="Right" /> 
								                <ItemStyle HorizontalAlign="Right"/> 
									            <ItemTemplate>
										            <asp:Label ID =lbtempo Text='<%#Container.DataItem("TotalBayarSDBlnIni")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn HeaderText="Sisa Pinjaman" >
								                <HeaderStyle HorizontalAlign="Right" /> 
								                <ItemStyle HorizontalAlign="Right"/> 
									            <ItemTemplate>
										            <asp:Label ID =lbrcicilan Text='<%#Container.DataItem("TotalSisa")%>' runat=server/>										
									            </ItemTemplate>
								            </asp:TemplateColumn>
															
								            <asp:TemplateColumn HeaderText="Awal Angsuran" >
									            <ItemTemplate>
										            <asp:Label ID=lstatus Text='<%#Container.DataItem("periodemulai")%>'  runat=server/>
									            </ItemTemplate>
									            <ItemStyle HorizontalAlign="Center" />
									            <HeaderStyle HorizontalAlign="Center" />
								            </asp:TemplateColumn>											
							            </Columns>
							             <PagerStyle Visible="False" />
          				            </asp:DataGrid></td>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td style="width: 100%">&nbsp;</td>
									<td><img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" commandargument="prev" imageurl="../../../images/btprev.png" onClick="btnPrevNext_Click" /></td>
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
						            <asp:Label id=lblPageCount visible=false text=1 runat=server/>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=PreviewBtn OnClick="PreviewBtn_Click" imageurl="../../images/butt_print_preview.gif" AlternateText="Preview" runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
                                                       
                            </td>
                        </tr>
					</table>
				</div>
				</td>
		        <table cellpadding="0" cellspacing="0" style="width: 20px">
			        <tr>
				        <td>&nbsp;</td>
			        </tr>
		        </table>
				</td>
			</tr>
		</table>



			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
		</form>
	</body>
</html>
