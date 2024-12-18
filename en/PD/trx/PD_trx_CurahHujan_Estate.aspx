<%@ Page Language="vb" Debug="true"  src="../../../include/PD_trx_CurahHujan_Estate.aspx.vb" Inherits="PD_trx_CurahHujan_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Daftar Curah Hujan</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="false" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:label id="lblValidate" visible="false" text="Please enter " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="false" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPDTrx id=MenuPDTrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR CURAH HUJAN</strong><hr style="width :100%" />   
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
								<td height="26" valign=bottom style="width: 18%">
                                Periode : <br />
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="65%">
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
                                    </asp:DropDownList><asp:DropDownList id="ddlyear" width="25%" runat=server></asp:DropDownList>
								</td>
								<td height="26" valign=bottom style="width: 10%">
                                     Divisi :<BR><asp:DropDownList id="ddlDiv" width=100% runat=server />
							    </td>
								<td height="26" valign=bottom colspan="3">
                                    &nbsp;<BR></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="EventData"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    GridLines = none
						                    Cellpadding = "2"
						                    OnEditCommand="DEDR_Edit"
						                    OnUpdateCommand="DEDR_Update"
						                    OnCancelCommand="DEDR_Cancel"
						                    OnDeleteCommand="DEDR_Delete"
						                    AllowPaging="True" 
						                    Allowcustompaging="False" 
						                    OnPageIndexChanged="OnPageChanged"
						                    Pagerstyle-Visible="False"
						                    OnSortCommand="Sort_Grid" 
						                    AllowSorting="True"
                                                            class="font9Tahoma">
								
							                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>		
					                    <Columns>					
					                    <asp:TemplateColumn HeaderText="Tanggal" SortExpression="CH_Date">
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("CH_Date")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="CH_Date" width=100% MaxLength="64"
								                    Text='<%#  objGlobal.GetShortDate(strDateFormat,Container.DataItem("CH_Date")) %>' runat="server"/>
							                    <asp:RequiredFieldValidator id=validatehol display=Dynamic runat=server 
								                    ControlToValidate=CH_Date />
			   			                        <asp:label id="lblDupMsg" Text="Date already exist" Visible = false forecolor=red Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					
					                    <asp:TemplateColumn HeaderText="Divisi">
						                    <ItemTemplate>
							                    <%# Container.DataItem("BlkGrpCode") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:Label ID="lblBlkGrpCode" Text='<%# Container.DataItem("BlkGrpCode") %>' runat="server" Visible=false />
							                    <asp:DropDownList id="ddlBlkGrpCode" width=100% runat="server"/>
							                    <asp:RequiredFieldValidator id=validatediv display=Dynamic runat=server 
								                    ControlToValidate=ddlBlkGrpCode />
			   			                        <asp:label id="lblDivMsg" Text="Please Select" Visible = false forecolor=red Runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>
					
					                    <asp:TemplateColumn HeaderText="Pagi" >
						                    <ItemTemplate>
							                    <%# Container.DataItem("CH_pagi") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="CH_pagi" width=100% Text='<%# Container.DataItem("CH_pagi")%>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>		
					
					                    <asp:TemplateColumn HeaderText="Siang" >
						                    <ItemTemplate>
							                    <%# Container.DataItem("CH_siang") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="CH_siang" width=100% Text='<%# Container.DataItem("CH_siang")%>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>	
					
					                    <asp:TemplateColumn HeaderText="Sore" >
						                    <ItemTemplate>
							                    <%# Container.DataItem("CH_sore") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="CH_sore" width=100% Text='<%# Container.DataItem("CH_sore")%>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>	
					
					                    <asp:TemplateColumn HeaderText="Malam" >
						                    <ItemTemplate>
							                    <%# Container.DataItem("CH_malam") %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:TextBox id="CH_malam" width=100%  Text='<%# Container.DataItem("CH_malam")%>' onkeypress="javascript:return isNumberKey(event)" runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>	
					
										
					                    <asp:TemplateColumn HeaderText="Tgl update">
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>
					
					                    <asp:TemplateColumn HeaderText="Status" >
						                    <ItemTemplate>
							                    <%# objHR.mtdGetDeptCodeStatus(Container.DataItem("Status")) %>
						                    </ItemTemplate>
						                    <EditItemTemplate>
							                    <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                    <asp:TextBox id="Status" Readonly=TRUE Visible = False
								                    Text='<%# Container.DataItem("Status")%>' runat="server"/>
						                    </EditItemTemplate>
					                    </asp:TemplateColumn>

					                    <asp:TemplateColumn HeaderText="Diupdate" >
						                    <ItemTemplate>
							                    <%# Container.DataItem("UserName") %>
						                    </ItemTemplate>
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
                                            <PagerStyle Visible="False" />
					                    </asp:DataGrid><BR>
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
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Curah Hujan" runat="server"/>&nbsp;
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


			</Form>
		</body>
</html>
