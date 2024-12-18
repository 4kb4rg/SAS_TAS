<%@ Page Language="vb" src="../../../include/PR_setup_AktivitiList_Estate.aspx.vb" Inherits="PR_setup_AktivitiList_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Aktiviti List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblValidate" visible="false" text="Please enter " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />


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
							<td><strong>DAFTAR AKTIVITI</strong><hr style="width :100%" />   
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
								<td height="26" valign=bottom width="75px">
                                    Kode Aktiviti :<BR><asp:TextBox id=srchJobCode width=100% maxlength="20" runat="server"/></td>
                                <td height="26" valign=bottom width="100px">
                                    Kategori :<BR><GG:AutoCompleteDropDownList id=srchcatid width=100% runat="server" OnSelectedIndexChanged="srchcatid_OnSelectedIndexChanged" AutoPostBack=true/></td>
                                <td height="26" valign=bottom width="250px">
                                    Sub kategori :<BR><GG:AutoCompleteDropDownList id=srchsubcatid width=100% runat="server"/></td>                                
								<td height="26" valign=bottom>
                                    Deskripsi :<BR><asp:TextBox id=srchDesc width=100% maxlength="50" runat="server"/></td>
								<td height="26" valign=bottom>
                                    Alokasi Gaji :<BR><asp:TextBox id=srchAcc width=100% maxlength="50" runat="server"/></td>
								<td height="20" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td height="26" valign=bottom ><asp:TextBox id=srchUpdateBy visible=False width=100% maxlength="20" runat="server"/>
								<asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgaktiviti"
						                    AutoGenerateColumns="false" width="100%" runat="server"
						                    GridLines = none
						                    Cellpadding = "2"
						                    AllowPaging="True" 
						                    Allowcustompaging="False"
						                    Pagesize="15" 
						                    OnPageIndexChanged="OnPageChanged"
						                    OnItemCommand=JobLink_Click 
						                    Pagerstyle-Visible="False"
						                    OnSortCommand="Sort_Grid" 
						                    AllowSorting="True"
                                                            class="font9Tahoma">
								
							                                <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                                <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                                <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>			
					                    <Columns>					
					
					                    <%--<asp:BoundColumn Visible=False HeaderText="Kode Aktiviti" DataField="JobCode" />								
					                    <asp:HyperLinkColumn HeaderText="Kode Aktiviti" SortExpression="JobCode" 
									                    DataNavigateUrlField="JobCode" 
									                    DataNavigateUrlFormatString="PR_setup_AktivitiDet_Estate.aspx?JobCode={0}" 
									                    DataTextField="JobCode" />--%>	
									
					                    <asp:TemplateColumn SortExpression="JobCode" HeaderText="Kode Aktiviti">
						                    <ItemTemplate>
							                    <asp:LinkButton id=lnkjob  Text='<%# Container.DataItem("JobCode") %>' runat=server /> 
						                         <asp:HiddenField Id=hidjob Value='<%# Container.DataItem("JobCode") %>' runat=server />
						                    </ItemTemplate>				
					                    </asp:TemplateColumn>
					
					                    <asp:TemplateColumn SortExpression="CatName" HeaderText="Kategori">
						                    <ItemTemplate>
							                    <%#Container.DataItem("CatName")%>
						                    </ItemTemplate>				
					                    </asp:TemplateColumn>	
					
					                    <asp:TemplateColumn SortExpression="subcatname" HeaderText="Sub Kategori">
						                    <ItemTemplate>
							                    <%#Container.DataItem("subcatname")%>
						                    </ItemTemplate>				
					                    </asp:TemplateColumn>	
					
					                    <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
						                    <ItemTemplate>
							                    <%#Container.DataItem("Description")%>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>	
					
					                    <asp:TemplateColumn HeaderText="Tgl update" SortExpression="A.UpdateDate">
						                    <ItemTemplate>
							                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>
					
					                    <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
						                    <ItemTemplate>
							                    <%# objHR.mtdGetFunctionStatus(Container.DataItem("Status")) %>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>
					
					                    <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
						                    <ItemTemplate>
							                    <%# Container.DataItem("UserName") %>
						                    </ItemTemplate>
					                    </asp:TemplateColumn>					
					
				                        <asp:TemplateColumn>
						                    <ItemTemplate>
						                    <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
						                    <asp:Label id=lbljobcode Text='<%# Trim(Container.DataItem("JobCode")) %>' Visible=False runat=server />
						                    </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" />
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
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Job Code" runat="server"/>
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
