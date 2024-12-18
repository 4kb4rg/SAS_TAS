<%@ Page Language="vb" trace=false src="../../../include/GL_trx_Budget_Item_list.aspx.vb" Inherits="GL_trx_Budget_Item_list" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Budget Pemakaian Bahan List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
	    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id="SortExpression" visible="false" runat="server" />
			<asp:Label id="SortCol" visible="false" runat="server" />
			<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />

            		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
		 
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>BUDGET PEMAKAIAN BAHAN LIST</strong><hr style="width :100%" />   
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
								<td width="8%">Periode :<br><asp:DropDownList id="lstAccYear" width="100%" runat="server"/></td>
								<td width="8%" height="26">Divisi :<br><asp:DropDownList id="srchDiv" width="100%" runat="server"  OnSelectedIndexChanged="srchDiv_OnSelectedIndexChanged" AutoPostBack=true /></td>
								<td width="8%" height="26">T.Tanam:<br><asp:DropDownList id="srchTTnm" width="100%" runat="server" OnSelectedIndexChanged="srchTTnm_OnSelectedIndexChanged" AutoPostBack=true /></td>
								<td width="15%">Sub Kategori :<br><asp:DropDownList id="srchSubCat" width="100%" runat="server" OnSelectedIndexChanged="srchTTnm_OnSelectedIndexChanged" AutoPostBack=true  /></td>
								<td width="15%">Kode Account :<BR><asp:TextBox id="srchAccCode" width=100% maxlength="20" runat="server"/></td>							
								<td width="15%">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="15%">Update Oleh :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%"valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						<asp:DataGrid id="dgList"
							AutoGenerateColumns="false" width="100%" runat="server"
							GridLines="none" 
							Cellpadding="2" 
							AllowPaging="True" 
							Allowcustompaging="False" 
							Pagesize="15" 
							OnPageIndexChanged="OnPageChanged"
							Pagerstyle-Visible="False" 
							OnDeleteCommand="DEDR_Delete" 
							OnSortCommand="Sort_Grid"  
							AllowSorting="True"
                                        class="font9Tahoma">
								
							            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							<Columns>
							  	
								<asp:TemplateColumn HeaderText="Divisi">
									<ItemTemplate>
										<%#Container.DataItem("CodeBlkgrp")%>
									</ItemTemplate>
								</asp:TemplateColumn>	
								
								<asp:TemplateColumn HeaderText="T.Tanam">
									<ItemTemplate>
										<%#Container.DataItem("CodeBlk")%>
									</ItemTemplate>
								</asp:TemplateColumn>	
														
								<asp:HyperLinkColumn HeaderText="Acc Code" 
									SortExpression="AccCode" 
									DataNavigateUrlField="TrxID" 
									DataNavigateUrlFormatString="gl_trx_BudgetDetails_Item.aspx?TrxID={0}" 
									DataTextField="AccCode" />
									
							    <asp:TemplateColumn HeaderText="Deskripsi">
									<ItemTemplate>
										<%#Container.DataItem("accdesc")%>
									</ItemTemplate>
								</asp:TemplateColumn>		
								
								<asp:TemplateColumn HeaderText="Item Code" SortExpression="ItemCode">
									<ItemTemplate>
										<%#Container.DataItem("itemdesc")%>
									</ItemTemplate>
								</asp:TemplateColumn>										
															
								<asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
									<ItemTemplate>
										<%#Container.DataItem("TotalAmount")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Total Fisik" SortExpression="TotalFisik">
									<ItemTemplate>
										<%#Container.DataItem("TotalFisik")%>
									</ItemTemplate>
								</asp:TemplateColumn>
														
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="B.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Status" SortExpression="B.Status">
									<ItemTemplate>
										<asp:Label id="lblStatus" text='<%# objGLSetup.mtdGetActSatus(Container.DataItem("Status")) %>' runat=server/>	
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="U.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Label id="lblTrxID" visible="false" text='<%# Container.DataItem("TrxID") %>' runat=server/>	
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
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
						<asp:ImageButton id="NewBtn" onClick="NewBtn_Click" imageurl="../../images/butt_generate.gif" AlternateText="Generate Budget" runat="server"/>
						<asp:ImageButton id="ibPrint" imageurl="../../images/butt_print.gif" AlternateText="Print" visible="false" runat="server"/>
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

		</form>
	</body>
</html>
