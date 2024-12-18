<%@ Page Language="vb" src="../../../include/DP_trx_BeneficiaryList.aspx.vb" Inherits="DP_BeneficiaryList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHR" src="../../menu/menu_hrtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Employee List</title>
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body onload="javascript:document.frmEmpList.txtEmpCode.focus();">
	    <form id=frmEmpList runat=server >
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHR id=MenuHR runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>MEMBER LIST</strong><hr style="width :100%" />   
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
								    <td width="15%" height="26">Member Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
								<td width="20%" height="26">Name :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server"/></td>
								<td valign=bottom width=12%>Unit :<BR><asp:DropDownList id="ddlUnit" width=100% runat=server /></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
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
							            AutoGenerateColumns=false width=100% runat=server
							            GridLines=none 
							            Cellpadding=2 
							            AllowPaging=True 
							            Allowcustompaging=False 
							            Pagesize=15 
							            OnPageIndexChanged=OnPageChanged 
							            Pagerstyle-Visible=False 
							            OnDeleteCommand=DEDR_Delete 
							            OnSortCommand=Sort_Grid  
							            OnItemCommand=EmpLink_Click 
							            AllowSorting=True
                                                                    class="font9Tahoma">
								
							                                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:TemplateColumn HeaderText="Pension No" SortExpression="NoPesp">
									            <ItemTemplate>
										            <asp:Label id=lblEmpCode text='<%# Container.DataItem("NoPesp") %>' visible=false runat=server/>
										            <asp:LinkButton id=lbEmpCode text='<%# Container.DataItem("NoPesp") %>' runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Name" SortExpression="Nam">
									            <ItemTemplate>
										            <asp:Label id=lblEmpName text='<%# Container.DataItem("Nam") %>' visible=false runat=server/>
										            <asp:LinkButton id=lbEmpName CommandName=Item text='<%# Container.DataItem("Nam") %>' runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Member No" SortExpression="NoPes">
									            <ItemTemplate>
										            <asp:Label id=lblNoPes text='<%# Container.DataItem("NoPes") %>' visible=false runat=server/>
										            <asp:LinkButton id=lbNoPes text='<%# Container.DataItem("NoPes") %>' runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Unit" SortExpression="Unit" >
									            <ItemTemplate>
										            <asp:label id=lblEmpStatus text=<%# Container.DataItem("Unit") %> runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Benefit" SortExpression="Benefit" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									            <ItemTemplate>
										            <%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Benefit"), 2), 2)%>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								
								            <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
									            <ItemTemplate>
										            <asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									            </ItemTemplate>
								            </asp:TemplateColumn>																
								            <asp:TemplateColumn ItemStyle-HorizontalAlign=center>									
									            <ItemTemplate>
										            <comment>Modified By BHL</comment>										
										            <asp:label id=lblEmpGender text=<%# Container.DataItem("sex") %> visible=false runat=server />
										            <comment>End Modified</comment>
									            </ItemTemplate>
								            </asp:TemplateColumn>									
							            </Columns>
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
					            <asp:ImageButton id=btnBnfDet OnClick="BeneficiaryDet_Click" imageurl="../../images/butt_new.gif" AlternateText="Beneficiary Detail" runat="server"/>
						        <asp:ImageButton id=btnBnfTrx OnClick="BeneficiaryTrx_Click" imageurl="../../images/butt_new.gif" AlternateText="Beneficiary Transaction" runat="server"/>
						        <asp:ImageButton id=btnBnfAD OnClick="BeneficiaryAD_Click" imageurl="../../images/butt_new.gif" AlternateText="Beneficiary Allowance & Deduction" runat="server"/>
						        <asp:ImageButton id=btnBnfMov OnClick="BeneficiaryMov_Click" imageurl="../../images/butt_new.gif" AlternateText="Moving Pension Fund" runat="server"/>
						        <asp:ImageButton id=btnBenOvr OnClick="BeneficiaryOvr_Click" imageurl="../../images/butt_new.gif" AlternateText="Benefit Overview" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false/>
						        <asp:Label id=lblRedirect visible=false runat=server/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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
		</form>
	</body>
</html>
