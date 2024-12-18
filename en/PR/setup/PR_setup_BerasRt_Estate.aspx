<%@ Page Language="vb" src="../../../include/PR_setup_BerasRt_Estate.aspx.vb" Inherits="PR_setup_BerasRt_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Harga Beras List</title>
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
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>DAFTAR HARGA BERAS / BULAN</strong><hr style="width :100%" />   
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
								<td width="15%" height="26" valign=bottom>Kode Beras :<BR><asp:TextBox id=srchDeptCode width=100% maxlength="8" runat="server"/></td>
								<%--<td width="40%" height="26" valign=bottom><asp:label id="lblDesc" runat="server" >Decsription</asp:label> :<BR><asp:TextBox id=srchName width=100% maxlength="128" runat="server"/></td>--%>
								<td height="26" valign=bottom style="width: 15%">Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Diupdate :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
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
					                <asp:TemplateColumn HeaderText="Kode Beras" >
						                <ItemTemplate>
							                <%#Container.DataItem("BerasCode")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="BerasCode" MaxLength="8" width=95%
								                Text='<%# trim(Container.DataItem("BerasCode")) %>' ReadOnly=true runat="server"/><BR>
							                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								                ControlToValidate=BerasCode />
							                <asp:RegularExpressionValidator id=revCode 
								                ControlToValidate="BerasCode"
								                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                Display="Dynamic"
								                text="Alphanumeric without any space in between only."
								                runat="server"/>
							                <asp:label id="lblDupMsg" Text="Code already exist" Visible = false forecolor=red Runat="server"/>		
						                </EditItemTemplate>
					                </asp:TemplateColumn>	

					                <asp:TemplateColumn HeaderText="Periode Start" >
						                <ItemTemplate>
							                <%#Trim(Container.DataItem("PeriodeStart"))%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="pstart" width=100% MaxLength="6"
								                Text='<%# trim(Container.DataItem("PeriodeStart")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validatepstart display=Dynamic runat=server 
								                ControlToValidate=pstart />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Periode End" >
						                <ItemTemplate>
							                <%#Trim(Container.DataItem("PeriodeEnd"))%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="pend" width=100% MaxLength="6"
								                Text='<%# trim(Container.DataItem("PeriodeEnd")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validatepend display=Dynamic runat=server 
								                ControlToValidate=pend />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Harga Beras Beli" >
						                <ItemTemplate>
							                <%#Container.DataItem("BerasRate")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="BerasRate" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("BerasRate")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateBs display=Dynamic runat=server 
								                ControlToValidate=BerasRate />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Harga Beras BKSPPS" >
						                <ItemTemplate>
							                <%#Container.DataItem("BerasBkspps")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="BerasBkspps" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("BerasBkspps")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateBerasBkspps display=Dynamic runat=server 
								                ControlToValidate=BerasBkspps />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Harga Beras Lembur" >
						                <ItemTemplate>
							                <%#Container.DataItem("BerasLembur")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="BerasLembur" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("BerasLembur")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateBerasLembur display=Dynamic runat=server 
								                ControlToValidate=BerasLembur />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Harga Beras Tunj" >
						                <ItemTemplate>
							                <%#Container.DataItem("BerasTunj")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="BerasTunj" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("BerasTunj")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateBerasTunj display=Dynamic runat=server 
								                ControlToValidate=BerasTunj />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Harga Astek" >
						                <ItemTemplate>
							                <%#Container.DataItem("BerasAstek")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="BerasAstek" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("BerasAStek")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateBerasAstek display=Dynamic runat=server 
								                ControlToValidate=BerasAstek />															
						                </EditItemTemplate>
					                </asp:TemplateColumn>
										
					                <asp:TemplateColumn HeaderText="Tgl update" >
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
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Beras Rate Code" runat="server"/>
					        	<asp:ImageButton id=btnGen OnClick="DEDR_Gen" imageurl="../../images/butt_generate.gif" AlternateText="Generate Gol SKUB" runat="server"/>
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



			<input id="isNew" type="hidden" runat="server" />
			</Form>
		</body>
</html>
