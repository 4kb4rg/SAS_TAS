<%@ Page Language="vb" Trace="False" Src="../../../include/WS_Trx_MechanicHour_Detail.aspx.vb" Inherits="WS_TRX_MECHANIC_HOUR_DETAIL" %>
<%@ Register TagPrefix="UserControl" TagName="MenuWSTrx" Src="../../menu/menu_WSTrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
	<head>
		<title>Workshop Mechanic Hour Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" RunAt="Server" />
	</head>
	<body>
		<form ID="frmMain" RunAt="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:label ID="lblCode" Visible="False" Text=" Code" RunAt="Server" />
			<asp:label ID="lblSelect" Visible="False" Text="Select " RunAt="Server" />
			<asp:label ID="lblPleaseSelect" Visible="False" Text="Please select " RunAt="Server" />
			<asp:Label ID="lblMechHourID" Visible="False" RunAt="Server"/>
			<table ID="tblMain" Border="0" Width="100%" CellSpacing="0" CellPadding="1" RunAt="Server" class="font9Tahoma">
				<tr>
					<td ColSpan="6"><UserControl:MenuWSTrx ID="menuWSTrx" RunAt="Server" /></td>
				</tr>
				<tr>
					<td Class="mt-h" ColSpan="6">WORKSHOP MECHANIC HOUR DETAILS</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr Size="1" NoShade></td>
				</tr>
				<tr>
					<td Width="20%" Height="25">Working Date :*</td>
					<td Width="30%">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
							    <td Width="50%"><asp:TextBox ID="txtWorkingDate" Width="100%" MaxLength="10" RunAt="Server" /></td>
						        <td Width="50%">&nbsp;<a HRef="javascript:PopCal('txtWorkingDate');"><asp:Image ID="imgWorkingDate" ImageUrl="../../Images/calendar.gif" RunAt="Server" /></a></td>
						    </tr>
						</table>
					    <asp:Label ID="lblWorkingDateErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td Width="5%">&nbsp;</td>
					<td Width="15%">Period :</td>
					<td Width="25%"><asp:Label ID="lblPeriod" RunAt="Server" />&nbsp;</td>
					<td Width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Employee Code (Mechanic) :*</td>
					<td>
					    <asp:DropDownList ID="ddlEmpCode" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblEmpCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td>
					    <asp:Label ID="lblStatusText" RunAt="Server" />
					    <asp:Label ID="lblStatus" Visible="False" RunAt="Server" />
			            <asp:label ID="lblDeleteDependency" Visible="False" Text="0" RunAt="Server" />&nbsp;
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label ID="lblCreateDate" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label ID="lblUpdateDate" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label ID="lblUserName" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td ColSpan="6">&nbsp;</td>
				</tr>
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
				    <td ColSpan="6">
				        <table ID="tblAdd" Border="0" Width="100%" CellSpacing="0" CellPadding="4" RunAt="Server">
				            <tr Class="mb-c">
				                <td Width="20%" Height="25">Job ID :*</td>
					            <td Width="80%">
					                <asp:DropDownList ID="ddlJobID" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlJobID_OnSelectedIndexChanged" RunAt="Server" />
					                <asp:Label ID="lblJobIDErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
				                <td Width="20%" Height="25"><asp:Label ID="lblWorkCodeTag" RunAt="Server" /> :*</td>
					            <td Width="80%">
					                <asp:DropDownList ID="ddlWorkCode" Width="100%" RunAt="Server" />
					                <asp:Label ID="lblWorkCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
				                <td Width="20%" Height="25">Time Spent :*</td>
					            <td Width="80%">
					                <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							            <tr>
							                <td Width="10%"><asp:TextBox ID="txtHourSpent" Width="100%" MaxLength="2" RunAt="Server" /></td>
							                <td Width="2%" Align="Center">:</td>
							                <td Width="10%"><asp:TextBox ID="txtMinuteSpent" Width="100%" MaxLength="2" RunAt="Server" /></td>
						                    <td Width="78%" Align="Left">&nbsp;HH:MM</td>
						                </tr>
						            </table>
						            <asp:RegularExpressionValidator ID="revHourSpent" 
													                ControlToValidate="txtHourSpent"
													                ValidationExpression="^\d{1,2}$"
													                Display="Dynamic"
													                Text = "<br>Maximum length 2 digits and 0 decimal points"
													                RunAt="Server"/>
                                    <asp:RegularExpressionValidator ID="revMinuteSpent" 
													                ControlToValidate="txtMinuteSpent"
													                ValidationExpression="^[0-9]$|^[0-5][0-9]$"
													                Display="Dynamic"
													                Text = "<br>Maximum length 2 digits and 0 decimal points"
													                RunAt="Server"/>
					                <asp:Label ID="lblTimeSpentErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
				                <td Width="20%" Height="25">Remark :</td>
					            <td Width="80%">
					                <asp:TextBox ID="txtRemark" Width="100%" MaxLength="128" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td ColSpan=2><asp:ImageButton Text="Add" ID="ibAdd" ImageURL="../../images/butt_add.gif" OnClick="ibAdd_OnClick" RunAt="Server" />
                                            </td>
				            </tr>
				            <tr>
					            <td ColSpan=2>&nbsp;</td>
				            </tr>
				        </table>
				    </td>
				</tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
				<tr ID="trMechHour">
					<td> 
						<asp:DataGrid ID="dgMechHour"
							OnItemDataBound="dgMechHour_OnItemDataBound"
							AutoGenerateColumns="False" 
							Width="100%" 
							RunAt="Server"
							GridLines = "None"
							Cellpadding = "2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="dgMechHour_OnDeleteCommand"
							AllowSorting="True"
                            class="font9Tahoma">	
							 
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
								<asp:TemplateColumn HeaderText="Job ID">
									<ItemStyle Width="12%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("JobID")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Work Code">
									<ItemStyle Width="12%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("WorkCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Time Spent">
									<ItemStyle Width="12%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("HourSpent"))%>:<%# Trim(Container.DataItem("MinuteSpent")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Original Amount">
									<ItemStyle Width="16%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(RoundNumber(Container.DataItem("OriginalAmount"), 0)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Remark">
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("Remark")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Debit Note ID">
									<ItemStyle Width="18%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("DNID")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle HorizontalAlign="Right" Width="10%"/>
									<ItemTemplate>
										<asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" CausesValidation="False" RunAt="Server" />
										<asp:Label Text='<%# Trim(Container.DataItem("DNID")) %>' ID="lblDNID" Visible="False" RunAt="Server" />
										<asp:Label Text='<%# Trim(Container.DataItem("MechHourLnID")) %>' ID="lblMechHourLnID" Visible="False" RunAt="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td><asp:Label ID="lblActionResult" Visible="False" ForeColor="Red" RunAt="Server" />&nbsp;</td>
				</tr>
				<tr>
					<td align="left">
						<asp:ImageButton ID="ibSave"   AlternateText="Save"   OnClick="ibSave_OnClick"   ImageURL="../../images/butt_save.gif"   CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibDelete" AlternateText="Delete" OnClick="ibDelete_OnClick" ImageURL="../../images/butt_delete.gif" CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibNew"    AlternateText="New"    OnClick="ibNew_OnClick"    ImageURL="../../images/butt_new.gif"    CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibBack"   AlternateText="Back"   OnClick="ibBack_OnClick"   ImageURL="../../images/butt_back.gif"   CausesValidation="False" RunAt="Server" />
					</td>
				</tr>
				<tr>
					<td align="left">
                                            &nbsp;</td>
				</tr>
			</table>

        <br />
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>