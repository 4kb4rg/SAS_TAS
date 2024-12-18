<%@ Page Language="vb" src="../../../include/PR_setup_BlokBJRList_Estate.aspx.vb" Inherits="PR_setup_BlokBJRList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>BJR List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmLocList runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="SortExpression" Visible="False" Runat="server" />



		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR BJR</strong><hr style="width :100%" />   
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
								<td height="26" valign=bottom width="15%">Tahun Tanam :<BR><asp:DropDownList id="srcttanam" width="100%" runat=server></asp:DropDownList></td>
								<td height="26" valign=bottom width="17%">Periode :<BR><asp:DropDownList ID="srcpmonth" runat="server" Width="50%">
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
                                    </asp:DropDownList>
                                <asp:DropDownList id="srcpyear" width="40%" runat=server></asp:DropDownList>
                                </td>
								<td width="15%" height="26" valign=bottom>Status :<BR>
									<asp:DropDownList id="ddlStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" valign=bottom>Diupdate :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
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
						            <asp:DataGrid id=dgLine runat=server
							                    AutoGenerateColumns=False width=100% 
							                    GridLines=None 
							                    Cellpadding=2 
							                    AllowPaging=True 
							                    Pagesize=15 
							                    OnItemDataBound=OnDataBound  
							                    OnPageIndexChanged=OnPageChanged 
							                    Pagerstyle-Visible=False 
							                    OnSortCommand=Sort_Grid  
							                    AllowSorting=True
							                    ShowFooter=True
                                                            class="font9Tahoma">
								
							                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							                    <Columns>
															
								                    <asp:TemplateColumn>
                                                       <ItemTemplate>
                                                       <asp:HyperLink runat="server" ID="LinkColumn" NavigateUrl="" Text="View Details"></asp:HyperLink>
                                                       </ItemTemplate>
                                                    </asp:TemplateColumn>
								
							                    <%--	<asp:HyperLinkColumn HeaderText="ID" SortExpression="periodestart" 
									                    DataNavigateUrlField="periodestart" 
									                    DataNavigateUrlFormatString="PR_setup_BlokBJRDet_Estate.aspx?periodestart={0}" 
									                    DataTextField="periodestart" />	--%>
							
							                       <asp:TemplateColumn HeaderText="Periode Start" SortExpression="periodeend">
									                    <ItemTemplate>
									                    <asp:Label ID=periodestart Text='<%#Container.DataItem("periodestart")%>' runat=server/>	
									                    </ItemTemplate>
								                    </asp:TemplateColumn>
								
								
								                    <asp:TemplateColumn HeaderText="Periode End" SortExpression="periodeend">
									                    <ItemTemplate>
									                    <asp:Label ID=periodeend Text='<%#Container.DataItem("periodeend")%>' runat=server/>	
									                    </ItemTemplate>
								                    </asp:TemplateColumn>
								
												
								                    <asp:TemplateColumn HeaderText="Tahun Tanam" SortExpression="blkcode">
									                    <ItemTemplate>
										                    <asp:Label ID=blkcode Text='<%#Container.DataItem("blkcode")%>' runat=server/>										
									                    </ItemTemplate>
								                    </asp:TemplateColumn>	
								
													
								                    <asp:TemplateColumn HeaderText="BJR">
									                    <ItemTemplate >
										                    <asp:Label ID=Bjr Text='<%#Container.DataItem("bjr")%>' Visible=true runat=server/>										
									                    </ItemTemplate>
									                    <ItemStyle HorizontalAlign="Right" />
									                    <HeaderStyle HorizontalAlign="Right" />
								                    </asp:TemplateColumn>		
															
								                    <asp:TemplateColumn HeaderText="Tgl Update" SortExpression="UpdateDate">
									                    <ItemTemplate>
										                    <%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
									                    </ItemTemplate>
									                    <ItemStyle HorizontalAlign="Center" />
									                    <HeaderStyle HorizontalAlign="Center" />
								                    </asp:TemplateColumn>									
					
								                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
									                    <ItemTemplate>
										                    <asp:Label ID =Status Text='<%#objHRSetup.mtdGetDeptStatus(Container.DataItem("Status"))%>' Visible=true runat=server/>
										                    <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
									                    </ItemTemplate>
									                    <ItemStyle HorizontalAlign="Center" />
									                    <HeaderStyle HorizontalAlign="Center" />
								                    </asp:TemplateColumn>									
								                    <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
									                    <ItemTemplate>
										                    <%# Container.DataItem("UserName") %>
									                    </ItemTemplate>
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
					            <asp:ImageButton id=NewDeptBtn onClick=NewDeptBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Block" runat="server"/>
						        &nbsp;<asp:Button ID="Button1" runat="server" Text="Copy Dari Periode :"  OnClick="Copybtn_Click" class="button-small" />&nbsp;
                                <asp:DropDownList ID="ddlbeforemonth" runat="server" Width="100px">
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
                                </asp:DropDownList>
						        <asp:DropDownList id="ddlbeforeyear" width="70px" runat=server/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
						        <asp:Label id=SortCol Visible=False Runat="server" />
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
		</FORM>
	</body>
</html>
