<%@ Page Language="vb" Src="../../../include/WS_Trx_Job_List.aspx.vb" Inherits="WS_TRX_JOB_LIST" %>
<%@ Register TagPrefix="UserControl" TagName="MenuWSTrx" Src="../../menu/menu_WSTrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
	<head>
		<title>Workshop Job List</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--	<preference:PrefHdl ID=PrefHdl RunAt="Server" />--%>
	<body>
		<form ID=frmMain Name=frmMain RunAt="Server" class="main-modul-bg-app-list-pu">
			<asp:Label ID="lblErrMessage" Visible="False" Text="Error while initiating component." RunAt="Server" />
			<asp:Label ID="lblOrderBy" Visible="False" RunAt="Server" />
			<asp:Label ID="lblOrderDir" Visible="False" RunAt="Server" />
			
	<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuWSTrx ID="menuWSTrx" RunAt="Server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>WORKSHOP JOB LIST</strong><hr style="width :100%" />   
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
								<td Width="12%" Height="26">Job ID :<br><asp:TextBox ID=txtSearchJobID Width="100%" MaxLength="20" RunAt="Server"/></td>
								<td Width="12%" Height="26"><asp:Label ID="lblSearchVehCode" RunAt="Server" /> :<br><asp:TextBox ID="txtSearchVehCode" Width="100%" RunAt="Server" /></td>
								<td Width="15%" Height="26">Job Start Date :
								    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
								        <tr>
								            <td Width="99%"><asp:TextBox ID="txtSearchJobStartDate" Width="100%" MaxLength="10" RunAt="Server" /></td>
								            <td>&nbsp;<a HRef="javascript:PopCal('txtSearchJobStartDate');"><asp:Image ID="imgSearchJobStartDate" ImageUrl="../../Images/calendar.gif" RunAt="Server" /></a></td>
								        </tr>
								    </table>
					                <asp:Label ID=lblSearchJobStartDateErr ForeColor="Red" RunAt="Server" />
								</td>
								<td Width="12%" Height="26">Job Type :<br><asp:DropDownList ID="ddlSearchJobType" Width="100%" RunAt="Server" /></td>
								<td Width="12%" Height="26">&nbsp;</td>
								<td Width="12%" Height="26">Status :<br><asp:DropDownList ID="ddlSearchStatus" Width="100%" RunAt="Server" /></td>
								<td Width="15%" Height="26">Last Updated By :<br><asp:TextBox ID=txtSearchUpdateBy Width="100%" MaxLength="128" RunAt="Server"/></td>
								<td Width="10%" Height="26" vAlign=Bottom Align=Right><asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_OnClick" RunAt="Server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid ID="dgJob"
							            AutoGenerateColumns="False" 
							            Width="100%" 
							            RunAt="Server"
							            OnDeleteCommand="dgJob_OnDeleteCommand"
							            OnItemDataBound="dgJob_OnItemDataBound"
							            OnPageIndexChanged="dgJob_OnPageIndexChanged"
							            OnSortCommand="dgJob_OnSortCommand" 
							            GridLines=None
							            CellPadding="2"
							            AllowPaging="True" 
							            AllowCustomPaging="False"
							            PageSize="15" 
							            PagerStyle-Visible="False"
							            AllowSorting="True"
                                                    class="font9Tahoma">
								
							                        <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                        <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                        <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							            <Columns>
								            <asp:HyperLinkColumn
									            HeaderText="Job ID"
									            DataNavigateUrlField="JobID"
									            DataNavigateUrlFormatString="ws_trx_job_detail.aspx?jobid={0}"
									            DataTextField="JobID"
									            DataTextFormatString="{0:c}"
									            SortExpression="J.JobID">
									            <ItemStyle Width="12%"/>
								            </asp:HyperLinkColumn>
								            <asp:TemplateColumn HeaderText="Vehicle Code" SortExpression="J.VehCode">
									            <ItemStyle Width="12%"/>
									            <ItemTemplate>
										            <%# Container.DataItem("VehCode") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Job Start Date" SortExpression="J.JobStartDate">
									            <ItemStyle Width="15%"/>
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("JobStartDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Job Type" SortExpression="J.JobType">
									            <ItemStyle Width="12%"/>
									            <ItemTemplate>
										            <%# objWSTrx.mtdGetJobType(IIf(IsNumeric(Container.DataItem("JobType")), Container.DataItem("JobType"), 0)) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Last Update" SortExpression="J.UpdateDate">
									            <ItemStyle Width="12%"/>
									            <ItemTemplate>
										            <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Status" SortExpression="J.Status">
									            <ItemStyle Width="12%"/>
									            <ItemTemplate>
										            <%# objWSTrx.mtdGetJobStatus(IIf(IsNumeric(Container.DataItem("Status")), Container.DataItem("Status"), 0)) %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="Updated By" SortExpression="USR.UserName">
									            <ItemStyle Width="15%"/>
									            <ItemTemplate>
										            <%# Container.DataItem("UserName") %>
									            </ItemTemplate>
								            </asp:TemplateColumn>
								            <asp:TemplateColumn HeaderText="" >
									            <ItemStyle Width="10%" HorizontalAlign="Right" />
									            <ItemTemplate>
										            <asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" RunAt="Server" />
										            <asp:Label ID="lblJobID" Text=<%# Container.DataItem("JobID") %> Visible="False" RunAt="Server" />
										            <asp:Label ID="lblStatus" Text=<%# Container.DataItem("Status") %> Visible="False" RunAt="Server" />
										            <asp:Label ID="lblJobStockCount" Text=<%# Container.DataItem("JobStockCount") %> Visible="False" RunAt="Server" />
										            <asp:Label ID="lblMechHourCount" Text=<%# Container.DataItem("MechHourCount") %> Visible="False" RunAt="Server" />
									            </ItemTemplate>
								            </asp:TemplateColumn>
							            </Columns>
						            </asp:DataGrid>
						            <br><asp:Label ID=lblActionResult text="Insufficient Stock In Inventory to Perform Operation!" Visible="False" forecolor=red RunAt="Server" />
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
					            <asp:ImageButton ID="ibNewInternalJob"      AlternateText="New Internal Job"       OnClick="ibNewInternalJob_OnClick"      ImageURL="../../images/butt_new_internaljob.gif"  RunAt="Server" />
						        <asp:ImageButton ID="ibNewStaffJob"         AlternateText="New Staff Job"          OnClick="ibNewStaffJob_OnClick"         ImageURL="../../images/butt_new_staffjob.gif"     RunAt="Server" />
						        <asp:ImageButton ID="ibNewExternalPartyJob" AlternateText="New External Party Job" OnClick="ibNewExternalPartyJob_OnClick" ImageURL="../../images/butt_new_externaljob.gif"  RunAt="Server" />
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
