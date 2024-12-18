<%@ Page Language="vb" src="../../../include/WS_Trx_Mechanic_List.aspx.vb" Inherits="WS_Mechanic" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSTrx" src="../../menu/menu_wstrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mechanic Hour Maintenance</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server"/>
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:label id="curStatus" Visible="False" Runat="server"/>
			<asp:label id="sortcol" Visible="False" Runat="server"/>


<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuWSTrx id=menuWStrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>MECHANIC HOUR MAINTENANCE</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
                            <td>
                            <table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td width="20%" height=25><asp:label id=lblCompanyTag runat=server /></td>
					        <td width="80%><asp:label id=lblCompName runat=server /></td>
                            </table>
                            </td>
				        </tr>
   				        <tr>
                            <td>
                            <table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25><asp:label id=lbllocationTag runat=server /> </td>
					        <td><asp:label id=lblLocName runat=server /></td>
                            </table>
                            </td>
				        </tr>
   				        <tr>
                            <td>
                            <table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td height=25><asp:label id=lblDate text="Entry Date :" runat=server /></td>
					        <td><b><asp:label id=lbldatedisp runat=server /></b></td>
                            </table>
                            </td>
				        </tr>
   				        <tr>
                            <td>
                            <table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="6">&nbsp;</td>
                            </table>
                            </td>
				        </tr>
   				        <tr>
                            <td>
                            <table cellpadding="4" cellspacing="0" style="width: 100%">
					        <td colspan="6">Please click on the Employee for which you want to maintain the mechanic hour. </td>
                            </table>
                            </td>
				        </tr>
						 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="dgMechanic"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
						                AllowPaging="True" 
						                Allowcustompaging="False"
						                OnEditCommand="DEDR_Edit"
						                Pagesize="15" 
						                Pagerstyle-Visible="False"
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="True"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
						                <Columns>
							                <asp:TemplateColumn HeaderText="Employee Code" SortExpression="Mst.EmpCode">
								                <ItemTemplate>
									                <asp:LinkButton id=EmpCode text=<%# Container.DataItem("EmpCode") %> CommandName=Edit runat="server"/>
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Employee Name" SortExpression="Mst.EmpName">
								                <ItemTemplate>
									                <asp:LinkButton ID="LinkButton1"  text=<%# Container.DataItem("EmpName") %> CommandName=Edit runat=server />
								                </ItemTemplate>
							                </asp:TemplateColumn>
						                </Columns>
					                </asp:DataGrid><br>
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
					            <asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" alternatetext=Back onclick=btnBack_Click runat=server />
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


			</FORM>
		</body>
</html>
